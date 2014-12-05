using System;
using System.IO;
using System.Text;
namespace Mono.Cecil.PE
{
	internal class RsrcReader
	{
		internal static ResourceDirectory ReadResourceDirectory(byte[] b, uint baseAddress)
		{
			MemoryStream input = new MemoryStream(b);
			return RsrcReader.ReadResourceDirectory(new BinaryReader(input), baseAddress);
		}
		private static ResourceDirectory ReadResourceDirectory(BinaryReader dr, uint baseAddress)
		{
			ResourceDirectory resourceDirectory = RsrcReader.ReadResourceDirectoryTable(dr);
			int num = (int)(resourceDirectory.NumNameEntries + resourceDirectory.NumIdEntries);
			for (int i = 0; i < num; i++)
			{
				resourceDirectory.Entries.Add(RsrcReader.ReadResourceEntry(dr, baseAddress));
			}
			return resourceDirectory;
		}
		private static ResourceEntry ReadResourceEntry(BinaryReader dr, uint baseAddress)
		{
			ResourceEntry resourceEntry = new ResourceEntry();
			uint num = dr.ReadUInt32();
			uint num2 = dr.ReadUInt32();
			long position = dr.BaseStream.Position;
			if ((num & 2147483648u) != 0u)
			{
				dr.BaseStream.Position = (long)((ulong)(num & 2147483647u));
				StringBuilder stringBuilder = new StringBuilder();
				int num3;
				while ((num3 = dr.Read()) > 0)
				{
					stringBuilder.Append((char)num3);
				}
				resourceEntry.Name = stringBuilder.ToString();
			}
			else
			{
				resourceEntry.Id = num;
			}
			if ((num2 & 2147483648u) != 0u)
			{
				dr.BaseStream.Position = (long)((ulong)(num2 & 2147483647u));
				resourceEntry.Directory = RsrcReader.ReadResourceDirectory(dr, baseAddress);
			}
			else
			{
				dr.BaseStream.Position = (long)((ulong)num2);
				uint num4 = dr.ReadUInt32();
				uint count = dr.ReadUInt32();
				uint codePage = dr.ReadUInt32();
				uint reserved = dr.ReadUInt32();
				resourceEntry.CodePage = codePage;
				resourceEntry.Reserved = reserved;
				dr.BaseStream.Position = (long)((ulong)(num4 - baseAddress));
				resourceEntry.Data = dr.ReadBytes((int)count);
			}
			dr.BaseStream.Position = position;
			return resourceEntry;
		}
		private static ResourceDirectory ReadResourceDirectoryTable(BinaryReader dr)
		{
			return new ResourceDirectory
			{
				Characteristics = dr.ReadUInt32(),
				TimeDateStamp = dr.ReadUInt32(),
				MajorVersion = dr.ReadUInt16(),
				MinVersion = dr.ReadUInt16(),
				NumNameEntries = dr.ReadUInt16(),
				NumIdEntries = dr.ReadUInt16()
			};
		}
	}
}

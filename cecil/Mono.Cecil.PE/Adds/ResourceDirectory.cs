using Mono.Collections.Generic;
using System;
namespace Mono.Cecil.PE
{
	public class ResourceDirectory
	{
		private readonly Collection<ResourceEntry> _entries = new Collection<ResourceEntry>();
		public Collection<ResourceEntry> Entries
		{
			get
			{
				return this._entries;
			}
		}
		public ushort NumNameEntries
		{
			get;
			set;
		}
		public ushort NumIdEntries
		{
			get;
			set;
		}
		public ushort MinVersion
		{
			get;
			set;
		}
		public ushort MajorVersion
		{
			get;
			set;
		}
		public uint Characteristics
		{
			get;
			set;
		}
		public uint TimeDateStamp
		{
			get;
			set;
		}
		public ushort SortEntries()
		{
			this._entries.Sort(EntryComparer.INSTANCE);
			ushort num = 0;
			ushort result;
			while ((int)num < this._entries.Count)
			{
				if (this._entries[(int)num].Name == null)
				{
					result = num;
					return result;
				}
				num += 1;
			}
			result = 0;
			return result;
		}
	}
}

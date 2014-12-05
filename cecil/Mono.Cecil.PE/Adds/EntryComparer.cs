using System;
using System.Collections.Generic;
namespace Mono.Cecil.PE
{
	internal class EntryComparer : IComparer<ResourceEntry>
	{
		internal static readonly EntryComparer INSTANCE = new EntryComparer();
		public int Compare(ResourceEntry x, ResourceEntry y)
		{
			int result;
			if (x.Name != null && y.Name == null)
			{
				result = -1;
			}
			else
			{
				if (x.Name == null && y.Name != null)
				{
					result = 1;
				}
				else
				{
					if (x.Name == null)
					{
						result = (int)(x.Id - y.Id);
					}
					else
					{
						result = string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
					}
				}
			}
			return result;
		}
	}
}

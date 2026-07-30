using System;
using System.Collections.Generic;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000033 RID: 51
	internal class ContractionComparer : IComparer<Contraction>
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000051F4 File Offset: 0x000033F4
		public int Compare(Contraction c1, Contraction c2)
		{
			char[] source = c1.Source;
			char[] source2 = c2.Source;
			int num = ((source.Length > source2.Length) ? source2.Length : source.Length);
			for (int i = 0; i < num; i++)
			{
				if (source[i] != source2[i])
				{
					return (int)(source[i] - source2[i]);
				}
			}
			if (source.Length != source2.Length)
			{
				return source.Length - source2.Length;
			}
			return c1.Index - c2.Index;
		}

		// Token: 0x040003DE RID: 990
		public static readonly ContractionComparer Instance = new ContractionComparer();
	}
}

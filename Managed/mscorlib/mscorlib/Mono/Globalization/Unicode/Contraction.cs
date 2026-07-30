using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000032 RID: 50
	internal class Contraction
	{
		// Token: 0x06000101 RID: 257 RVA: 0x000051CD File Offset: 0x000033CD
		public Contraction(int index, char[] source, string replacement, byte[] sortkey)
		{
			this.Index = index;
			this.Source = source;
			this.Replacement = replacement;
			this.SortKey = sortkey;
		}

		// Token: 0x040003DA RID: 986
		public int Index;

		// Token: 0x040003DB RID: 987
		public readonly char[] Source;

		// Token: 0x040003DC RID: 988
		public readonly string Replacement;

		// Token: 0x040003DD RID: 989
		public readonly byte[] SortKey;
	}
}

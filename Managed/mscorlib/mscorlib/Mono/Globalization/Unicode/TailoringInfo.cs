using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000031 RID: 49
	internal class TailoringInfo
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000051A8 File Offset: 0x000033A8
		public TailoringInfo(int lcid, int tailoringIndex, int tailoringCount, bool frenchSort)
		{
			this.LCID = lcid;
			this.TailoringIndex = tailoringIndex;
			this.TailoringCount = tailoringCount;
			this.FrenchSort = frenchSort;
		}

		// Token: 0x040003D6 RID: 982
		public readonly int LCID;

		// Token: 0x040003D7 RID: 983
		public readonly int TailoringIndex;

		// Token: 0x040003D8 RID: 984
		public readonly int TailoringCount;

		// Token: 0x040003D9 RID: 985
		public readonly bool FrenchSort;
	}
}

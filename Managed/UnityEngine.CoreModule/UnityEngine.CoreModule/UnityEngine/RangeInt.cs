using System;

namespace UnityEngine
{
	// Token: 0x020001B0 RID: 432
	public struct RangeInt
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x000204B8 File Offset: 0x0001E6B8
		public int end
		{
			get
			{
				return this.start + this.length;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000204D7 File Offset: 0x0001E6D7
		public RangeInt(int start, int length)
		{
			this.start = start;
			this.length = length;
		}

		// Token: 0x0400064F RID: 1615
		public int start;

		// Token: 0x04000650 RID: 1616
		public int length;
	}
}

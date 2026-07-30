using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000234 RID: 564
	internal struct BMPAlloc
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x000445B4 File Offset: 0x000427B4
		public bool Equals(BMPAlloc other)
		{
			return this.page == other.page && this.pageLine == other.pageLine && this.bitIndex == other.bitIndex;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000445F4 File Offset: 0x000427F4
		public bool IsValid()
		{
			return this.page >= 0;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00044614 File Offset: 0x00042814
		public override string ToString()
		{
			return string.Format("{0},{1},{2}", this.page, this.pageLine, this.bitIndex);
		}

		// Token: 0x04000798 RID: 1944
		public static readonly BMPAlloc Invalid = new BMPAlloc
		{
			page = -1
		};

		// Token: 0x04000799 RID: 1945
		public int page;

		// Token: 0x0400079A RID: 1946
		public ushort pageLine;

		// Token: 0x0400079B RID: 1947
		public byte bitIndex;

		// Token: 0x0400079C RID: 1948
		public OwnedState ownedState;
	}
}

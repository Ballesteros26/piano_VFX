using System;

namespace Mono.Net
{
	// Token: 0x02000050 RID: 80
	internal struct CFRange
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00004649 File Offset: 0x00002849
		public CFRange(int loc, int len)
		{
			this.Location = (IntPtr)loc;
			this.Length = (IntPtr)len;
		}

		// Token: 0x04000746 RID: 1862
		public IntPtr Location;

		// Token: 0x04000747 RID: 1863
		public IntPtr Length;
	}
}

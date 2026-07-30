using System;

namespace System.Drawing
{
	// Token: 0x020000A2 RID: 162
	internal struct IconInfo
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00015D96 File Offset: 0x00013F96
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00015DA1 File Offset: 0x00013FA1
		public bool IsIcon
		{
			get
			{
				return this.fIcon == 1;
			}
			set
			{
				this.fIcon = (value ? 1 : 0);
			}
		}

		// Token: 0x04000609 RID: 1545
		private int fIcon;

		// Token: 0x0400060A RID: 1546
		public int xHotspot;

		// Token: 0x0400060B RID: 1547
		public int yHotspot;

		// Token: 0x0400060C RID: 1548
		public IntPtr hbmMask;

		// Token: 0x0400060D RID: 1549
		public IntPtr hbmColor;
	}
}

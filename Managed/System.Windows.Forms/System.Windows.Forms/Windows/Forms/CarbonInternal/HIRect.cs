using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004B7 RID: 1207
	internal struct HIRect
	{
		// Token: 0x06004C1A RID: 19482 RVA: 0x0012EEB8 File Offset: 0x0012D0B8
		public HIRect(int x, int y, int w, int h)
		{
			this.origin = new CGPoint(x, y);
			this.size = new CGSize(w, h);
		}

		// Token: 0x0400296C RID: 10604
		public CGPoint origin;

		// Token: 0x0400296D RID: 10605
		public CGSize size;
	}
}

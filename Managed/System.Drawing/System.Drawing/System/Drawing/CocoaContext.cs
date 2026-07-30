using System;

namespace System.Drawing
{
	// Token: 0x020000AB RID: 171
	internal struct CocoaContext : IMacContext
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x00016262 File Offset: 0x00014462
		public CocoaContext(IntPtr ctx, int width, int height)
		{
			this.ctx = ctx;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00016279 File Offset: 0x00014479
		public void Synchronize()
		{
			MacSupport.CGContextSynchronize(this.ctx);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00016286 File Offset: 0x00014486
		public void Release()
		{
			MacSupport.CGContextRestoreGState(this.ctx);
		}

		// Token: 0x0400062F RID: 1583
		public IntPtr ctx;

		// Token: 0x04000630 RID: 1584
		public int width;

		// Token: 0x04000631 RID: 1585
		public int height;
	}
}

using System;

namespace System.Drawing
{
	// Token: 0x020000AA RID: 170
	internal struct CarbonContext : IMacContext
	{
		// Token: 0x06000A32 RID: 2610 RVA: 0x00016223 File Offset: 0x00014423
		public CarbonContext(IntPtr port, IntPtr ctx, int width, int height)
		{
			this.port = port;
			this.ctx = ctx;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00016242 File Offset: 0x00014442
		public void Synchronize()
		{
			MacSupport.CGContextSynchronize(this.ctx);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001624F File Offset: 0x0001444F
		public void Release()
		{
			MacSupport.ReleaseContext(this.port, this.ctx);
		}

		// Token: 0x0400062B RID: 1579
		public IntPtr port;

		// Token: 0x0400062C RID: 1580
		public IntPtr ctx;

		// Token: 0x0400062D RID: 1581
		public int width;

		// Token: 0x0400062E RID: 1582
		public int height;
	}
}

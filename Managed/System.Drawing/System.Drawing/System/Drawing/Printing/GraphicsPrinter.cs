using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000D8 RID: 216
	internal class GraphicsPrinter
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x00018605 File Offset: 0x00016805
		internal GraphicsPrinter(Graphics gr, IntPtr dc)
		{
			this.graphics = gr;
			this.hDC = dc;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0001861B File Offset: 0x0001681B
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x00018623 File Offset: 0x00016823
		internal Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
			set
			{
				this.graphics = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001862C File Offset: 0x0001682C
		internal IntPtr Hdc
		{
			get
			{
				return this.hDC;
			}
		}

		// Token: 0x0400073D RID: 1853
		private Graphics graphics;

		// Token: 0x0400073E RID: 1854
		private IntPtr hDC;
	}
}

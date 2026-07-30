using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004B4 RID: 1204
	internal struct CGSize
	{
		// Token: 0x06004C17 RID: 19479 RVA: 0x0012EE80 File Offset: 0x0012D080
		public CGSize(int w, int h)
		{
			this.width = (float)w;
			this.height = (float)h;
		}

		// Token: 0x04002966 RID: 10598
		public float width;

		// Token: 0x04002967 RID: 10599
		public float height;
	}
}

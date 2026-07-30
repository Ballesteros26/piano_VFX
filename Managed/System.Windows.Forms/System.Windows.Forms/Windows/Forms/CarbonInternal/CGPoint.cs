using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004B6 RID: 1206
	internal struct CGPoint
	{
		// Token: 0x06004C19 RID: 19481 RVA: 0x0012EEA4 File Offset: 0x0012D0A4
		public CGPoint(int x, int y)
		{
			this.x = (float)x;
			this.y = (float)y;
		}

		// Token: 0x0400296A RID: 10602
		public float x;

		// Token: 0x0400296B RID: 10603
		public float y;
	}
}

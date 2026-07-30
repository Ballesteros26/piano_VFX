using System;

namespace System.Drawing
{
	// Token: 0x020000A8 RID: 168
	internal struct Rect
	{
		// Token: 0x06000A31 RID: 2609 RVA: 0x000161F0 File Offset: 0x000143F0
		public Rect(float x, float y, float width, float height)
		{
			this.origin.x = x;
			this.origin.y = y;
			this.size.width = width;
			this.size.height = height;
		}

		// Token: 0x04000625 RID: 1573
		public CGPoint origin;

		// Token: 0x04000626 RID: 1574
		public CGSize size;
	}
}

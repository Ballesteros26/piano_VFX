using System;

namespace System.Drawing.Internal
{
	// Token: 0x020000F1 RID: 241
	internal struct GPRECTF
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x0001A601 File Offset: 0x00018801
		internal GPRECTF(float x, float y, float width, float height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0001A620 File Offset: 0x00018820
		internal GPRECTF(RectangleF rect)
		{
			this.X = rect.X;
			this.Y = rect.Y;
			this.Width = rect.Width;
			this.Height = rect.Height;
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0001A656 File Offset: 0x00018856
		internal SizeF SizeF
		{
			get
			{
				return new SizeF(this.Width, this.Height);
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001A669 File Offset: 0x00018869
		internal RectangleF ToRectangleF()
		{
			return new RectangleF(this.X, this.Y, this.Width, this.Height);
		}

		// Token: 0x0400081B RID: 2075
		internal float X;

		// Token: 0x0400081C RID: 2076
		internal float Y;

		// Token: 0x0400081D RID: 2077
		internal float Width;

		// Token: 0x0400081E RID: 2078
		internal float Height;
	}
}

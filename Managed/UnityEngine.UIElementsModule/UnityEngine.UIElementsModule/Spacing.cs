using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004F RID: 79
	internal struct Spacing
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00007F20 File Offset: 0x00006120
		public float horizontal
		{
			get
			{
				return this.left + this.right;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00007F40 File Offset: 0x00006140
		public float vertical
		{
			get
			{
				return this.top + this.bottom;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007F5F File Offset: 0x0000615F
		public Spacing(float left, float top, float right, float bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007F80 File Offset: 0x00006180
		public static Rect operator +(Rect r, Spacing a)
		{
			r.x -= a.left;
			r.y -= a.top;
			r.width += a.horizontal;
			r.height += a.vertical;
			return r;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007FEC File Offset: 0x000061EC
		public static Rect operator -(Rect r, Spacing a)
		{
			r.x += a.left;
			r.y += a.top;
			r.width = Mathf.Max(0f, r.width - a.horizontal);
			r.height = Mathf.Max(0f, r.height - a.vertical);
			return r;
		}

		// Token: 0x040000E6 RID: 230
		public float left;

		// Token: 0x040000E7 RID: 231
		public float top;

		// Token: 0x040000E8 RID: 232
		public float right;

		// Token: 0x040000E9 RID: 233
		public float bottom;
	}
}

using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000023 RID: 35
	internal class Color
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00005D68 File Offset: 0x00003F68
		public Color(RTF rtf)
		{
			this.red = -1;
			this.green = -1;
			this.blue = -1;
			this.num = -1;
			lock (rtf)
			{
				if (rtf.Colors == null)
				{
					rtf.Colors = this;
				}
				else
				{
					Color colors = rtf.Colors;
					while (colors.next != null)
					{
						colors = colors.next;
					}
					colors.next = this;
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005E04 File Offset: 0x00004004
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00005E0C File Offset: 0x0000400C
		public int Red
		{
			get
			{
				return this.red;
			}
			set
			{
				this.red = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005E18 File Offset: 0x00004018
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00005E20 File Offset: 0x00004020
		public int Green
		{
			get
			{
				return this.green;
			}
			set
			{
				this.green = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005E2C File Offset: 0x0000402C
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00005E34 File Offset: 0x00004034
		public int Blue
		{
			get
			{
				return this.blue;
			}
			set
			{
				this.blue = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005E40 File Offset: 0x00004040
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00005E48 File Offset: 0x00004048
		public int Num
		{
			get
			{
				return this.num;
			}
			set
			{
				this.num = value;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005E54 File Offset: 0x00004054
		public static Color GetColor(RTF rtf, int color_number)
		{
			Color color;
			lock (rtf)
			{
				color = Color.GetColor(rtf.Colors, color_number);
			}
			return color;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005EA0 File Offset: 0x000040A0
		private static Color GetColor(Color start, int color_number)
		{
			if (color_number == -1)
			{
				return start;
			}
			Color color = start;
			while (color != null && color.num != color_number)
			{
				color = color.next;
			}
			return color;
		}

		// Token: 0x0400006B RID: 107
		private int red;

		// Token: 0x0400006C RID: 108
		private int green;

		// Token: 0x0400006D RID: 109
		private int blue;

		// Token: 0x0400006E RID: 110
		private int num;

		// Token: 0x0400006F RID: 111
		private Color next;
	}
}

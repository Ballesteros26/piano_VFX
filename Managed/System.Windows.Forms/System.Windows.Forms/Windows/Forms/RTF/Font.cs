using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000025 RID: 37
	internal class Font
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00005F24 File Offset: 0x00004124
		public Font(RTF rtf)
		{
			this.rtf = rtf;
			this.num = -1;
			this.name = string.Empty;
			lock (rtf)
			{
				if (rtf.Fonts == null)
				{
					rtf.Fonts = this;
				}
				else
				{
					Font fonts = rtf.Fonts;
					while (fonts.next != null)
					{
						fonts = fonts.next;
					}
					fonts.next = this;
				}
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005FBC File Offset: 0x000041BC
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005FC4 File Offset: 0x000041C4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005FD0 File Offset: 0x000041D0
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00005FD8 File Offset: 0x000041D8
		public string AltName
		{
			get
			{
				return this.alt_name;
			}
			set
			{
				this.alt_name = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005FE4 File Offset: 0x000041E4
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00005FEC File Offset: 0x000041EC
		public int Num
		{
			get
			{
				return this.num;
			}
			set
			{
				Font.DeleteFont(this.rtf, value);
				this.num = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006004 File Offset: 0x00004204
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000600C File Offset: 0x0000420C
		public int Family
		{
			get
			{
				return this.family;
			}
			set
			{
				this.family = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006018 File Offset: 0x00004218
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006020 File Offset: 0x00004220
		public CharsetType Charset
		{
			get
			{
				return this.charset;
			}
			set
			{
				this.charset = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000602C File Offset: 0x0000422C
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006034 File Offset: 0x00004234
		public int Pitch
		{
			get
			{
				return this.pitch;
			}
			set
			{
				this.pitch = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006040 File Offset: 0x00004240
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006048 File Offset: 0x00004248
		public int Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006054 File Offset: 0x00004254
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000605C File Offset: 0x0000425C
		public int Codepage
		{
			get
			{
				return this.codepage;
			}
			set
			{
				this.codepage = value;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006068 File Offset: 0x00004268
		public static bool DeleteFont(RTF rtf, int font_number)
		{
			lock (rtf)
			{
				Font fonts = rtf.Fonts;
				Font font = null;
				while (fonts != null && fonts.num != font_number)
				{
					font = fonts;
					fonts = fonts.next;
				}
				if (fonts != null)
				{
					if (fonts == rtf.Fonts)
					{
						rtf.Fonts = fonts.next;
					}
					else if (font != null)
					{
						font.next = fonts.next;
					}
					else
					{
						rtf.Fonts = fonts.next;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006120 File Offset: 0x00004320
		public static Font GetFont(RTF rtf, int font_number)
		{
			Font font;
			lock (rtf)
			{
				font = Font.GetFont(rtf.Fonts, font_number);
			}
			return font;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000616C File Offset: 0x0000436C
		public static Font GetFont(Font start, int font_number)
		{
			if (font_number == -1)
			{
				return start;
			}
			Font font = start;
			while (font != null && font.num != font_number)
			{
				font = font.next;
			}
			return font;
		}

		// Token: 0x04000071 RID: 113
		private string name;

		// Token: 0x04000072 RID: 114
		private string alt_name;

		// Token: 0x04000073 RID: 115
		private int num;

		// Token: 0x04000074 RID: 116
		private int family;

		// Token: 0x04000075 RID: 117
		private CharsetType charset;

		// Token: 0x04000076 RID: 118
		private int pitch;

		// Token: 0x04000077 RID: 119
		private int type;

		// Token: 0x04000078 RID: 120
		private int codepage;

		// Token: 0x04000079 RID: 121
		private Font next;

		// Token: 0x0400007A RID: 122
		private RTF rtf;
	}
}

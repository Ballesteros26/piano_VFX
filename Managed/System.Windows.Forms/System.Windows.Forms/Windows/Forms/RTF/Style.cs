using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200002F RID: 47
	internal class Style
	{
		// Token: 0x06000173 RID: 371 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		public Style(RTF rtf)
		{
			this.num = -1;
			this.type = StyleType.Paragraph;
			this.based_on = 222;
			this.next_par = -1;
			lock (rtf)
			{
				if (rtf.Styles == null)
				{
					rtf.Styles = this;
				}
				else
				{
					Style styles = rtf.Styles;
					while (styles.next != null)
					{
						styles = styles.next;
					}
					styles.next = this;
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000DF84 File Offset: 0x0000C184
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000DF8C File Offset: 0x0000C18C
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000DF98 File Offset: 0x0000C198
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		public StyleType Type
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		public bool Additive
		{
			get
			{
				return this.additive;
			}
			set
			{
				this.additive = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		public int BasedOn
		{
			get
			{
				return this.based_on;
			}
			set
			{
				this.based_on = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		public StyleElement Elements
		{
			get
			{
				return this.elements;
			}
			set
			{
				this.elements = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		public bool Expanding
		{
			get
			{
				return this.expanding;
			}
			set
			{
				this.expanding = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000E004 File Offset: 0x0000C204
		public int NextPar
		{
			get
			{
				return this.next_par;
			}
			set
			{
				this.next_par = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000E010 File Offset: 0x0000C210
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000E018 File Offset: 0x0000C218
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

		// Token: 0x06000184 RID: 388 RVA: 0x0000E024 File Offset: 0x0000C224
		public void Expand(RTF rtf)
		{
			if (this.num == -1)
			{
				return;
			}
			if (this.expanding)
			{
				throw new Exception("Recursive style expansion");
			}
			this.expanding = true;
			if (this.num != this.based_on)
			{
				rtf.SetToken(TokenClass.Control, Major.ParAttr, Minor.StyleNum, this.based_on, "\\s");
				rtf.RouteToken();
			}
			StyleElement styleElement = this.elements;
			while (styleElement != null)
			{
				rtf.TokenClass = styleElement.TokenClass;
				rtf.Major = styleElement.Major;
				rtf.Minor = styleElement.Minor;
				rtf.Param = styleElement.Param;
				rtf.Text = styleElement.Text;
				rtf.RouteToken();
			}
			this.expanding = false;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000E0E8 File Offset: 0x0000C2E8
		public static Style GetStyle(RTF rtf, int style_number)
		{
			Style style;
			lock (rtf)
			{
				style = Style.GetStyle(rtf.Styles, style_number);
			}
			return style;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000E134 File Offset: 0x0000C334
		public static Style GetStyle(Style start, int style_number)
		{
			if (style_number == -1)
			{
				return start;
			}
			Style style = start;
			while (style != null && style.num != style_number)
			{
				style = style.next;
			}
			return style;
		}

		// Token: 0x040004D7 RID: 1239
		public const int NoStyleNum = 222;

		// Token: 0x040004D8 RID: 1240
		public const int NormalStyleNum = 0;

		// Token: 0x040004D9 RID: 1241
		private string name;

		// Token: 0x040004DA RID: 1242
		private StyleType type;

		// Token: 0x040004DB RID: 1243
		private bool additive;

		// Token: 0x040004DC RID: 1244
		private int num;

		// Token: 0x040004DD RID: 1245
		private int based_on;

		// Token: 0x040004DE RID: 1246
		private int next_par;

		// Token: 0x040004DF RID: 1247
		private bool expanding;

		// Token: 0x040004E0 RID: 1248
		private StyleElement elements;

		// Token: 0x040004E1 RID: 1249
		private Style next;
	}
}

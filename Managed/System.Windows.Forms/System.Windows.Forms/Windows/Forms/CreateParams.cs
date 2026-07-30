using System;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates the information needed when creating a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B2 RID: 178
	public class CreateParams
	{
		/// <summary>Gets or sets the control's initial text.</summary>
		/// <returns>The control's initial text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0002CFA4 File Offset: 0x0002B1A4
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0002CFAC File Offset: 0x0002B1AC
		public string Caption
		{
			get
			{
				return this.caption;
			}
			set
			{
				this.caption = value;
			}
		}

		/// <summary>Gets or sets the name of the Windows class to derive the control from.</summary>
		/// <returns>The name of the Windows class to derive the control from.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0002CFB8 File Offset: 0x0002B1B8
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x0002CFC0 File Offset: 0x0002B1C0
		public string ClassName
		{
			get
			{
				return this.class_name;
			}
			set
			{
				this.class_name = value;
			}
		}

		/// <summary>Gets or sets a bitwise combination of class style values.</summary>
		/// <returns>A bitwise combination of the class style values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0002CFCC File Offset: 0x0002B1CC
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0002CFD4 File Offset: 0x0002B1D4
		public int ClassStyle
		{
			get
			{
				return this.class_style;
			}
			set
			{
				this.class_style = value;
			}
		}

		/// <summary>Gets or sets a bitwise combination of extended window style values.</summary>
		/// <returns>A bitwise combination of the extended window style values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0002CFE0 File Offset: 0x0002B1E0
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0002CFE8 File Offset: 0x0002B1E8
		public int ExStyle
		{
			get
			{
				return this.ex_style;
			}
			set
			{
				this.ex_style = value;
			}
		}

		/// <summary>Gets or sets the initial left position of the control.</summary>
		/// <returns>The numeric value that represents the initial left position of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0002CFF4 File Offset: 0x0002B1F4
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x0002CFFC File Offset: 0x0002B1FC
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>Gets or sets the top position of the initial location of the control.</summary>
		/// <returns>The numeric value that represents the top position of the initial location of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002D008 File Offset: 0x0002B208
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x0002D010 File Offset: 0x0002B210
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>Gets or sets the initial width of the control.</summary>
		/// <returns>The numeric value that represents the initial width of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0002D01C File Offset: 0x0002B21C
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x0002D024 File Offset: 0x0002B224
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>Gets or sets the initial height of the control.</summary>
		/// <returns>The numeric value that represents the initial height of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002D030 File Offset: 0x0002B230
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x0002D038 File Offset: 0x0002B238
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Gets or sets a bitwise combination of window style values.</summary>
		/// <returns>A bitwise combination of the window style values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002D044 File Offset: 0x0002B244
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0002D04C File Offset: 0x0002B24C
		public int Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		/// <summary>Gets or sets additional parameter information needed to create the control.</summary>
		/// <returns>The <see cref="T:System.Object" /> that holds additional parameter information needed to create the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0002D058 File Offset: 0x0002B258
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x0002D060 File Offset: 0x0002B260
		public object Param
		{
			get
			{
				return this.param;
			}
			set
			{
				this.param = value;
			}
		}

		/// <summary>Gets or sets the control's parent.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the window handle of the control's parent.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002D06C File Offset: 0x0002B26C
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x0002D074 File Offset: 0x0002B274
		public IntPtr Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002D080 File Offset: 0x0002B280
		internal bool IsSet(WindowStyles Style)
		{
			return (this.style & (int)Style) == (int)Style;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002D090 File Offset: 0x0002B290
		internal bool IsSet(WindowExStyles ExStyle)
		{
			return (this.ex_style & (int)ExStyle) == (int)ExStyle;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002D0A0 File Offset: 0x0002B2A0
		internal static bool IsSet(WindowExStyles ExStyle, WindowExStyles Option)
		{
			return (Option & ExStyle) == Option;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002D0A8 File Offset: 0x0002B2A8
		internal static bool IsSet(WindowStyles Style, WindowStyles Option)
		{
			return (Option & Style) == Option;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002D0B0 File Offset: 0x0002B2B0
		internal bool HasWindowManager
		{
			get
			{
				if (this.control == null)
				{
					return false;
				}
				Form form = this.control as Form;
				return form != null && form.window_manager != null;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
		internal WindowExStyles WindowExStyle
		{
			get
			{
				return (WindowExStyles)this.ex_style;
			}
			set
			{
				this.ex_style = (int)value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0002D100 File Offset: 0x0002B300
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x0002D108 File Offset: 0x0002B308
		internal WindowStyles WindowStyle
		{
			get
			{
				return (WindowStyles)this.style;
			}
			set
			{
				this.style = (int)value;
			}
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B1A RID: 2842 RVA: 0x0002D114 File Offset: 0x0002B314
		public override string ToString()
		{
			return string.Format("CreateParams {{'{0}', '{1}', 0x{2:X}, 0x{3:X}, {{{4}, {5}, {6}, {7}}}}}", new object[] { this.class_name, this.caption, this.class_style, this.ex_style, this.x, this.y, this.width, this.height });
		}

		// Token: 0x04000856 RID: 2134
		private string caption;

		// Token: 0x04000857 RID: 2135
		private string class_name;

		// Token: 0x04000858 RID: 2136
		private int class_style;

		// Token: 0x04000859 RID: 2137
		private int ex_style;

		// Token: 0x0400085A RID: 2138
		private int x;

		// Token: 0x0400085B RID: 2139
		private int y;

		// Token: 0x0400085C RID: 2140
		private int height;

		// Token: 0x0400085D RID: 2141
		private int width;

		// Token: 0x0400085E RID: 2142
		private int style;

		// Token: 0x0400085F RID: 2143
		private object param;

		// Token: 0x04000860 RID: 2144
		private IntPtr parent;

		// Token: 0x04000861 RID: 2145
		internal Menu menu;

		// Token: 0x04000862 RID: 2146
		internal Control control;
	}
}

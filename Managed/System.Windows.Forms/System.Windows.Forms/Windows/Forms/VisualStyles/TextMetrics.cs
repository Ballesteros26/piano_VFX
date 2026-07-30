using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Provides basic information about the font specified by a visual style for a particular element.</summary>
	// Token: 0x0200052C RID: 1324
	public struct TextMetrics
	{
		/// <summary>Gets or sets the ascent of characters in the font.</summary>
		/// <returns>The ascent of characters in the font.</returns>
		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06004DA6 RID: 19878 RVA: 0x00135894 File Offset: 0x00133A94
		// (set) Token: 0x06004DA7 RID: 19879 RVA: 0x0013589C File Offset: 0x00133A9C
		public int Ascent
		{
			get
			{
				return this.ascent;
			}
			set
			{
				this.ascent = value;
			}
		}

		/// <summary>Gets or sets the average width of characters in the font.</summary>
		/// <returns>The average width of characters in the font.</returns>
		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06004DA8 RID: 19880 RVA: 0x001358A8 File Offset: 0x00133AA8
		// (set) Token: 0x06004DA9 RID: 19881 RVA: 0x001358B0 File Offset: 0x00133AB0
		public int AverageCharWidth
		{
			get
			{
				return this.average_char_width;
			}
			set
			{
				this.average_char_width = value;
			}
		}

		/// <summary>Gets or sets the character used to define word breaks for text justification.</summary>
		/// <returns>The character used to define word breaks for text justification.</returns>
		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06004DAA RID: 19882 RVA: 0x001358BC File Offset: 0x00133ABC
		// (set) Token: 0x06004DAB RID: 19883 RVA: 0x001358C4 File Offset: 0x00133AC4
		public char BreakChar
		{
			get
			{
				return this.break_char;
			}
			set
			{
				this.break_char = value;
			}
		}

		/// <summary>Gets or sets the character set of the font.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.VisualStyles.TextMetricsCharacterSet" /> values that specifies the character set of the font.</returns>
		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06004DAC RID: 19884 RVA: 0x001358D0 File Offset: 0x00133AD0
		// (set) Token: 0x06004DAD RID: 19885 RVA: 0x001358D8 File Offset: 0x00133AD8
		public TextMetricsCharacterSet CharSet
		{
			get
			{
				return this.char_set;
			}
			set
			{
				this.char_set = value;
			}
		}

		/// <summary>Gets or sets the character to be substituted for characters not in the font.</summary>
		/// <returns>The character to be substituted for characters not in the font.</returns>
		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06004DAE RID: 19886 RVA: 0x001358E4 File Offset: 0x00133AE4
		// (set) Token: 0x06004DAF RID: 19887 RVA: 0x001358EC File Offset: 0x00133AEC
		public char DefaultChar
		{
			get
			{
				return this.default_char;
			}
			set
			{
				this.default_char = value;
			}
		}

		/// <summary>Gets or sets the descent of characters in the font.</summary>
		/// <returns>The descent of characters in the font.</returns>
		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06004DB0 RID: 19888 RVA: 0x001358F8 File Offset: 0x00133AF8
		// (set) Token: 0x06004DB1 RID: 19889 RVA: 0x00135900 File Offset: 0x00133B00
		public int Descent
		{
			get
			{
				return this.descent;
			}
			set
			{
				this.descent = value;
			}
		}

		/// <summary>Gets or sets the horizontal aspect of the device for which the font was designed.</summary>
		/// <returns>The horizontal aspect of the device for which the font was designed.</returns>
		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06004DB2 RID: 19890 RVA: 0x0013590C File Offset: 0x00133B0C
		// (set) Token: 0x06004DB3 RID: 19891 RVA: 0x00135914 File Offset: 0x00133B14
		public int DigitizedAspectX
		{
			get
			{
				return this.digitized_aspect_x;
			}
			set
			{
				this.digitized_aspect_x = value;
			}
		}

		/// <summary>Gets or sets the vertical aspect of the device for which the font was designed.</summary>
		/// <returns>The vertical aspect of the device for which the font was designed.</returns>
		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x00135920 File Offset: 0x00133B20
		// (set) Token: 0x06004DB5 RID: 19893 RVA: 0x00135928 File Offset: 0x00133B28
		public int DigitizedAspectY
		{
			get
			{
				return this.digitized_aspect_y;
			}
			set
			{
				this.digitized_aspect_y = value;
			}
		}

		/// <summary>Gets or sets the amount of extra leading that the application adds between rows.</summary>
		/// <returns>The amount of extra leading (space) required between rows. </returns>
		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x00135934 File Offset: 0x00133B34
		// (set) Token: 0x06004DB7 RID: 19895 RVA: 0x0013593C File Offset: 0x00133B3C
		public int ExternalLeading
		{
			get
			{
				return this.external_leading;
			}
			set
			{
				this.external_leading = value;
			}
		}

		/// <summary>Gets or sets the first character defined in the font.</summary>
		/// <returns>The first character defined in the font.</returns>
		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x00135948 File Offset: 0x00133B48
		// (set) Token: 0x06004DB9 RID: 19897 RVA: 0x00135950 File Offset: 0x00133B50
		public char FirstChar
		{
			get
			{
				return this.first_char;
			}
			set
			{
				this.first_char = value;
			}
		}

		/// <summary>Gets or sets the height of characters in the font.</summary>
		/// <returns>The height of characters in the font.</returns>
		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06004DBA RID: 19898 RVA: 0x0013595C File Offset: 0x00133B5C
		// (set) Token: 0x06004DBB RID: 19899 RVA: 0x00135964 File Offset: 0x00133B64
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

		/// <summary>Gets or sets the amount of leading inside the bounds set by the <see cref="P:System.Windows.Forms.VisualStyles.TextMetrics.Height" /> property. </summary>
		/// <returns>The amount of leading inside the bounds set by the <see cref="P:System.Windows.Forms.VisualStyles.TextMetrics.Height" /> property.</returns>
		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06004DBC RID: 19900 RVA: 0x00135970 File Offset: 0x00133B70
		// (set) Token: 0x06004DBD RID: 19901 RVA: 0x00135978 File Offset: 0x00133B78
		public int InternalLeading
		{
			get
			{
				return this.internal_leading;
			}
			set
			{
				this.internal_leading = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the font is italic.</summary>
		/// <returns>true if the font is italic; otherwise, false.</returns>
		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06004DBE RID: 19902 RVA: 0x00135984 File Offset: 0x00133B84
		// (set) Token: 0x06004DBF RID: 19903 RVA: 0x0013598C File Offset: 0x00133B8C
		public bool Italic
		{
			get
			{
				return this.italic;
			}
			set
			{
				this.italic = value;
			}
		}

		/// <summary>Gets or sets the last character defined in the font.</summary>
		/// <returns>The last character defined in the font.</returns>
		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06004DC0 RID: 19904 RVA: 0x00135998 File Offset: 0x00133B98
		// (set) Token: 0x06004DC1 RID: 19905 RVA: 0x001359A0 File Offset: 0x00133BA0
		public char LastChar
		{
			get
			{
				return this.last_char;
			}
			set
			{
				this.last_char = value;
			}
		}

		/// <summary>Gets or sets the width of the widest character in the font.</summary>
		/// <returns>The width of the widest character in the font.</returns>
		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x001359AC File Offset: 0x00133BAC
		// (set) Token: 0x06004DC3 RID: 19907 RVA: 0x001359B4 File Offset: 0x00133BB4
		public int MaxCharWidth
		{
			get
			{
				return this.max_char_width;
			}
			set
			{
				this.max_char_width = value;
			}
		}

		/// <summary>Gets or sets the extra width per string that may be added to some synthesized fonts.</summary>
		/// <returns>The extra width per string that may be added to some synthesized fonts.</returns>
		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x001359C0 File Offset: 0x00133BC0
		// (set) Token: 0x06004DC5 RID: 19909 RVA: 0x001359C8 File Offset: 0x00133BC8
		public int Overhang
		{
			get
			{
				return this.overhang;
			}
			set
			{
				this.overhang = value;
			}
		}

		/// <summary>Gets or sets information about the pitch, technology, and family of a physical font.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.TextMetricsPitchAndFamilyValues" /> values that specifies the pitch, technology, and family of a physical font.</returns>
		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x001359D4 File Offset: 0x00133BD4
		// (set) Token: 0x06004DC7 RID: 19911 RVA: 0x001359DC File Offset: 0x00133BDC
		public TextMetricsPitchAndFamilyValues PitchAndFamily
		{
			get
			{
				return this.pitch_and_family;
			}
			set
			{
				this.pitch_and_family = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the font specifies a horizontal line through the characters.</summary>
		/// <returns>true if the font has a horizontal line through the characters; otherwise, false.</returns>
		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06004DC8 RID: 19912 RVA: 0x001359E8 File Offset: 0x00133BE8
		// (set) Token: 0x06004DC9 RID: 19913 RVA: 0x001359F0 File Offset: 0x00133BF0
		public bool StruckOut
		{
			get
			{
				return this.struck_out;
			}
			set
			{
				this.struck_out = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the font is underlined.</summary>
		/// <returns>true if the font is underlined; otherwise, false.</returns>
		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x001359FC File Offset: 0x00133BFC
		// (set) Token: 0x06004DCB RID: 19915 RVA: 0x00135A04 File Offset: 0x00133C04
		public bool Underlined
		{
			get
			{
				return this.underlined;
			}
			set
			{
				this.underlined = value;
			}
		}

		/// <summary>Gets or sets the weight of the font.</summary>
		/// <returns>The weight of the font.</returns>
		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x00135A10 File Offset: 0x00133C10
		// (set) Token: 0x06004DCD RID: 19917 RVA: 0x00135A18 File Offset: 0x00133C18
		public int Weight
		{
			get
			{
				return this.weight;
			}
			set
			{
				this.weight = value;
			}
		}

		// Token: 0x04002BE2 RID: 11234
		private int ascent;

		// Token: 0x04002BE3 RID: 11235
		private int average_char_width;

		// Token: 0x04002BE4 RID: 11236
		private char break_char;

		// Token: 0x04002BE5 RID: 11237
		private TextMetricsCharacterSet char_set;

		// Token: 0x04002BE6 RID: 11238
		private char default_char;

		// Token: 0x04002BE7 RID: 11239
		private int descent;

		// Token: 0x04002BE8 RID: 11240
		private int digitized_aspect_x;

		// Token: 0x04002BE9 RID: 11241
		private int digitized_aspect_y;

		// Token: 0x04002BEA RID: 11242
		private int external_leading;

		// Token: 0x04002BEB RID: 11243
		private char first_char;

		// Token: 0x04002BEC RID: 11244
		private int height;

		// Token: 0x04002BED RID: 11245
		private int internal_leading;

		// Token: 0x04002BEE RID: 11246
		private bool italic;

		// Token: 0x04002BEF RID: 11247
		private char last_char;

		// Token: 0x04002BF0 RID: 11248
		private int max_char_width;

		// Token: 0x04002BF1 RID: 11249
		private int overhang;

		// Token: 0x04002BF2 RID: 11250
		private TextMetricsPitchAndFamilyValues pitch_and_family;

		// Token: 0x04002BF3 RID: 11251
		private bool struck_out;

		// Token: 0x04002BF4 RID: 11252
		private bool underlined;

		// Token: 0x04002BF5 RID: 11253
		private int weight;
	}
}

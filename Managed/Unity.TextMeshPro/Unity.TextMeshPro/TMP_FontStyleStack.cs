using System;

namespace TMPro
{
	// Token: 0x02000034 RID: 52
	public struct TMP_FontStyleStack
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000F564 File Offset: 0x0000D764
		public void Clear()
		{
			this.bold = 0;
			this.italic = 0;
			this.underline = 0;
			this.strikethrough = 0;
			this.highlight = 0;
			this.superscript = 0;
			this.subscript = 0;
			this.uppercase = 0;
			this.lowercase = 0;
			this.smallcaps = 0;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		public byte Add(FontStyles style)
		{
			if (style <= FontStyles.Strikethrough)
			{
				switch (style)
				{
				case FontStyles.Bold:
					this.bold += 1;
					return this.bold;
				case FontStyles.Italic:
					this.italic += 1;
					return this.italic;
				case (FontStyles)3:
					break;
				case FontStyles.Underline:
					this.underline += 1;
					return this.underline;
				default:
					if (style == FontStyles.Strikethrough)
					{
						this.strikethrough += 1;
						return this.strikethrough;
					}
					break;
				}
			}
			else
			{
				if (style == FontStyles.Superscript)
				{
					this.superscript += 1;
					return this.superscript;
				}
				if (style == FontStyles.Subscript)
				{
					this.subscript += 1;
					return this.subscript;
				}
				if (style == FontStyles.Highlight)
				{
					this.highlight += 1;
					return this.highlight;
				}
			}
			return 0;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		public byte Remove(FontStyles style)
		{
			if (style <= FontStyles.Strikethrough)
			{
				switch (style)
				{
				case FontStyles.Bold:
					if (this.bold > 1)
					{
						this.bold -= 1;
					}
					else
					{
						this.bold = 0;
					}
					return this.bold;
				case FontStyles.Italic:
					if (this.italic > 1)
					{
						this.italic -= 1;
					}
					else
					{
						this.italic = 0;
					}
					return this.italic;
				case (FontStyles)3:
					break;
				case FontStyles.Underline:
					if (this.underline > 1)
					{
						this.underline -= 1;
					}
					else
					{
						this.underline = 0;
					}
					return this.underline;
				default:
					if (style == FontStyles.Strikethrough)
					{
						if (this.strikethrough > 1)
						{
							this.strikethrough -= 1;
						}
						else
						{
							this.strikethrough = 0;
						}
						return this.strikethrough;
					}
					break;
				}
			}
			else
			{
				if (style == FontStyles.Superscript)
				{
					if (this.superscript > 1)
					{
						this.superscript -= 1;
					}
					else
					{
						this.superscript = 0;
					}
					return this.superscript;
				}
				if (style == FontStyles.Subscript)
				{
					if (this.subscript > 1)
					{
						this.subscript -= 1;
					}
					else
					{
						this.subscript = 0;
					}
					return this.subscript;
				}
				if (style == FontStyles.Highlight)
				{
					if (this.highlight > 1)
					{
						this.highlight -= 1;
					}
					else
					{
						this.highlight = 0;
					}
					return this.highlight;
				}
			}
			return 0;
		}

		// Token: 0x04000194 RID: 404
		public byte bold;

		// Token: 0x04000195 RID: 405
		public byte italic;

		// Token: 0x04000196 RID: 406
		public byte underline;

		// Token: 0x04000197 RID: 407
		public byte strikethrough;

		// Token: 0x04000198 RID: 408
		public byte highlight;

		// Token: 0x04000199 RID: 409
		public byte superscript;

		// Token: 0x0400019A RID: 410
		public byte subscript;

		// Token: 0x0400019B RID: 411
		public byte uppercase;

		// Token: 0x0400019C RID: 412
		public byte lowercase;

		// Token: 0x0400019D RID: 413
		public byte smallcaps;
	}
}

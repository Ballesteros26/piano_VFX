using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000040 RID: 64
	internal struct FontStyleStack
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00019C34 File Offset: 0x00017E34
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

		// Token: 0x060001A7 RID: 423 RVA: 0x00019C88 File Offset: 0x00017E88
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
				case FontStyles.Bold | FontStyles.Italic:
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

		// Token: 0x060001A8 RID: 424 RVA: 0x00019D9C File Offset: 0x00017F9C
		public byte Remove(FontStyles style)
		{
			if (style <= FontStyles.Strikethrough)
			{
				switch (style)
				{
				case FontStyles.Bold:
				{
					bool flag = this.bold > 1;
					if (flag)
					{
						this.bold -= 1;
					}
					else
					{
						this.bold = 0;
					}
					return this.bold;
				}
				case FontStyles.Italic:
				{
					bool flag2 = this.italic > 1;
					if (flag2)
					{
						this.italic -= 1;
					}
					else
					{
						this.italic = 0;
					}
					return this.italic;
				}
				case FontStyles.Bold | FontStyles.Italic:
					break;
				case FontStyles.Underline:
				{
					bool flag3 = this.underline > 1;
					if (flag3)
					{
						this.underline -= 1;
					}
					else
					{
						this.underline = 0;
					}
					return this.underline;
				}
				default:
					if (style == FontStyles.Strikethrough)
					{
						bool flag4 = this.strikethrough > 1;
						if (flag4)
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
					bool flag5 = this.superscript > 1;
					if (flag5)
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
					bool flag6 = this.subscript > 1;
					if (flag6)
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
					bool flag7 = this.highlight > 1;
					if (flag7)
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

		// Token: 0x04000354 RID: 852
		public byte bold;

		// Token: 0x04000355 RID: 853
		public byte italic;

		// Token: 0x04000356 RID: 854
		public byte underline;

		// Token: 0x04000357 RID: 855
		public byte strikethrough;

		// Token: 0x04000358 RID: 856
		public byte highlight;

		// Token: 0x04000359 RID: 857
		public byte superscript;

		// Token: 0x0400035A RID: 858
		public byte subscript;

		// Token: 0x0400035B RID: 859
		public byte uppercase;

		// Token: 0x0400035C RID: 860
		public byte lowercase;

		// Token: 0x0400035D RID: 861
		public byte smallcaps;
	}
}

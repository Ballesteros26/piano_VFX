using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000204 RID: 516
	internal class LineTag
	{
		// Token: 0x06001FC2 RID: 8130 RVA: 0x000773B8 File Offset: 0x000755B8
		public LineTag(Line line, int start)
		{
			this.line = line;
			this.Start = start;
			this.link_font = null;
			this.is_link = false;
			this.link_text = null;
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x000773E4 File Offset: 0x000755E4
		public int Ascent
		{
			get
			{
				return this.ascent;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x000773EC File Offset: 0x000755EC
		// (set) Token: 0x06001FC5 RID: 8133 RVA: 0x000773F4 File Offset: 0x000755F4
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
			set
			{
				this.back_color = value;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x00077400 File Offset: 0x00075600
		public Color ColorToDisplay
		{
			get
			{
				if (this.IsLink)
				{
					return Color.Blue;
				}
				return this.color;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x0007741C File Offset: 0x0007561C
		// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x00077424 File Offset: 0x00075624
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x00077430 File Offset: 0x00075630
		public int Descent
		{
			get
			{
				return this.descent;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x00077438 File Offset: 0x00075638
		public int End
		{
			get
			{
				return this.start + this.Length;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x00077448 File Offset: 0x00075648
		public Font FontToDisplay
		{
			get
			{
				if (this.IsLink)
				{
					if (this.link_font == null)
					{
						this.link_font = new Font(this.font.FontFamily, this.font.Size, this.font.Style | 4);
					}
					return this.link_font;
				}
				return this.font;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x000774A8 File Offset: 0x000756A8
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x000774B0 File Offset: 0x000756B0
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				if (this.font != value)
				{
					this.link_font = null;
					this.font = value;
					this.height = this.Font.Height;
					XplatUI.GetFontMetrics(Hwnd.GraphicsContext, this.Font, out this.ascent, out this.descent);
					this.line.recalc = true;
				}
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x00077514 File Offset: 0x00075714
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x0007751C File Offset: 0x0007571C
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

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x00077528 File Offset: 0x00075728
		public virtual bool IsTextTag
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0007752C File Offset: 0x0007572C
		public int Length
		{
			get
			{
				int num;
				if (this.next != null)
				{
					num = this.next.start - this.start;
				}
				else
				{
					num = this.line.text.Length - (this.start - 1);
				}
				return (num <= 0) ? 0 : num;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x00077588 File Offset: 0x00075788
		// (set) Token: 0x06001FD3 RID: 8147 RVA: 0x00077590 File Offset: 0x00075790
		public Line Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x0007759C File Offset: 0x0007579C
		// (set) Token: 0x06001FD5 RID: 8149 RVA: 0x000775A4 File Offset: 0x000757A4
		public LineTag Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x000775B0 File Offset: 0x000757B0
		// (set) Token: 0x06001FD7 RID: 8151 RVA: 0x000775B8 File Offset: 0x000757B8
		public LineTag Previous
		{
			get
			{
				return this.previous;
			}
			set
			{
				this.previous = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x000775C4 File Offset: 0x000757C4
		// (set) Token: 0x06001FD9 RID: 8153 RVA: 0x000775CC File Offset: 0x000757CC
		public int Shift
		{
			get
			{
				return this.shift;
			}
			set
			{
				this.shift = value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x000775D8 File Offset: 0x000757D8
		// (set) Token: 0x06001FDB RID: 8155 RVA: 0x000775E0 File Offset: 0x000757E0
		public int Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x000775EC File Offset: 0x000757EC
		public int TextEnd
		{
			get
			{
				return this.start + this.TextLength;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x000775FC File Offset: 0x000757FC
		public int TextLength
		{
			get
			{
				int num;
				if (this.next != null)
				{
					num = this.next.start - this.start;
				}
				else
				{
					num = this.line.TextLengthWithoutEnding() - (this.start - 1);
				}
				return (num <= 0) ? 0 : num;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x00077654 File Offset: 0x00075854
		public float Width
		{
			get
			{
				if (this.Length == 0)
				{
					return 0f;
				}
				return this.line.widths[this.start + this.Length - 1] - ((this.start == 0) ? 0f : this.line.widths[this.start - 1]);
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x000776B8 File Offset: 0x000758B8
		public float X
		{
			get
			{
				if (this.start == 0)
				{
					return (float)this.line.X;
				}
				return (float)this.line.X + this.line.widths[this.start - 1];
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x00077700 File Offset: 0x00075900
		// (set) Token: 0x06001FE1 RID: 8161 RVA: 0x00077708 File Offset: 0x00075908
		public bool IsLink
		{
			get
			{
				return this.is_link;
			}
			set
			{
				this.is_link = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x00077714 File Offset: 0x00075914
		// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x0007771C File Offset: 0x0007591C
		public string LinkText
		{
			get
			{
				return this.link_text;
			}
			set
			{
				this.link_text = value;
			}
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x00077728 File Offset: 0x00075928
		public LineTag Break(int pos)
		{
			LineTag lineTag = new LineTag(this.line, pos);
			lineTag.CopyFormattingFrom(this);
			lineTag.next = this.next;
			this.next = lineTag;
			lineTag.previous = this;
			if (lineTag.next != null)
			{
				lineTag.next.previous = lineTag;
			}
			return lineTag;
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0007777C File Offset: 0x0007597C
		public bool Combine(LineTag other)
		{
			if (!this.Equals(other))
			{
				return false;
			}
			this.next = other.next;
			if (this.next != null)
			{
				this.next.previous = this;
			}
			return true;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x000777BC File Offset: 0x000759BC
		public void CopyFormattingFrom(LineTag other)
		{
			this.Font = other.font;
			this.color = other.color;
			this.back_color = other.back_color;
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x000777F0 File Offset: 0x000759F0
		public void Delete()
		{
			if (this.previous == null && this.next == null)
			{
				return;
			}
			if (this.next == null)
			{
				this.previous.next = null;
				return;
			}
			this.next.previous = null;
			for (LineTag lineTag = this.next; lineTag != null; lineTag = lineTag.next)
			{
				lineTag.Start -= this.Length;
			}
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x00077864 File Offset: 0x00075A64
		public virtual void Draw(Graphics dc, Color color, float x, float y, int start, int end)
		{
			TextBoxTextRenderer.DrawText(dc, this.line.text.ToString(start, end).Replace("\r", string.Empty), this.FontToDisplay, color, x, y, false);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x000778A8 File Offset: 0x00075AA8
		public virtual void Draw(Graphics dc, Color color, float xoff, float y, int start, int end, string text)
		{
			Rectangle rectangle;
			this.Draw(dc, color, xoff, y, start, end, text, out rectangle, false);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000778CC File Offset: 0x00075ACC
		public virtual void Draw(Graphics dc, Color color, float xoff, float y, int drawStart, int drawEnd, string text, out Rectangle measuredText, bool measureText)
		{
			if (measureText)
			{
				int num = (int)this.line.widths[drawStart] + (int)xoff;
				int num2 = (int)this.line.widths[drawEnd] - (int)this.line.widths[drawStart];
				int num3 = (int)y;
				int num4 = (int)TextBoxTextRenderer.MeasureText(dc, this.Text(), this.FontToDisplay).Height;
				measuredText..ctor(num, num3, num2, num4);
			}
			else
			{
				measuredText = default(Rectangle);
			}
			while (drawStart < drawEnd)
			{
				int num5 = text.IndexOf("\t", drawStart);
				if (num5 == -1)
				{
					num5 = drawEnd;
				}
				TextBoxTextRenderer.DrawText(dc, text.Substring(drawStart, num5 - drawStart).Replace("\r", string.Empty), this.FontToDisplay, color, xoff + this.line.widths[drawStart], y, false);
				if (!this.line.document.multiline && num5 != drawEnd)
				{
					TextBoxTextRenderer.DrawText(dc, "\u0013", this.FontToDisplay, color, xoff + this.line.widths[num5], y, true);
				}
				drawStart = num5 + 1;
			}
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x000779FC File Offset: 0x00075BFC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is LineTag))
			{
				return false;
			}
			if (obj == this)
			{
				return true;
			}
			LineTag lineTag = (LineTag)obj;
			return lineTag.IsTextTag == this.IsTextTag && this.IsLink == lineTag.IsLink && !(this.LinkText != lineTag.LinkText) && (this.font.Equals(lineTag.font) && this.color.Equals(lineTag.color));
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x00077AA0 File Offset: 0x00075CA0
		public static LineTag FindTag(Line line, int pos)
		{
			LineTag tags = line.tags;
			if (pos == 0)
			{
				return tags;
			}
			while (tags != null)
			{
				if (tags.start <= pos && pos < tags.End)
				{
					return LineTag.GetFinalTag(tags);
				}
				tags = tags.next;
			}
			return null;
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x00077AF0 File Offset: 0x00075CF0
		public static bool FormatText(Line line, int formatStart, int length, Font font, Color color, Color backColor, FormatSpecified specified)
		{
			bool flag = false;
			if ((FormatSpecified.Font & specified) == FormatSpecified.Font && font.Height != line.height)
			{
				flag = true;
			}
			line.recalc = true;
			if (length > line.text.Length)
			{
				length = line.text.Length;
			}
			LineTag lineTag = line.tags;
			int num = formatStart + length;
			if (formatStart == 1 && length == lineTag.Length)
			{
				LineTag.SetFormat(lineTag, font, color, backColor, specified);
				return flag;
			}
			if (formatStart == 1 && length == 0)
			{
				line.tags.Break(1);
				LineTag.SetFormat(line.tags, font, color, backColor, specified);
				return flag;
			}
			LineTag lineTag2 = LineTag.FindTag(line, formatStart - 1);
			if (lineTag2.End == formatStart && length == 0 && lineTag2.Next != null && lineTag2.Next.Length == 0)
			{
				LineTag.SetFormat(lineTag2.Next, font, color, backColor, specified);
				return flag;
			}
			while (lineTag2.End == formatStart && lineTag2.Next != null)
			{
				lineTag2 = lineTag2.Next;
			}
			lineTag = lineTag2.Break(formatStart);
			if (lineTag.Length == 0)
			{
				LineTag.SetFormat(lineTag, font, color, backColor, specified);
				return flag;
			}
			if (length == 0)
			{
				lineTag.Break(formatStart);
				LineTag.SetFormat(lineTag, font, color, backColor, specified);
				return flag;
			}
			while (lineTag != null && lineTag.End <= num)
			{
				LineTag.SetFormat(lineTag, font, color, backColor, specified);
				lineTag = lineTag.next;
			}
			if (lineTag != null && lineTag.End == num)
			{
				return flag;
			}
			LineTag lineTag3 = LineTag.FindTag(line, num - 1);
			if (lineTag3 != null)
			{
				lineTag3.Break(num);
				LineTag.SetFormat(lineTag3, font, color, backColor, specified);
			}
			return flag;
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x00077CB8 File Offset: 0x00075EB8
		public int GetCharIndex(int x)
		{
			int i = this.start;
			int num = i + this.Length;
			int num2 = this.line.TextLengthWithoutEnding();
			if (this.Length == 0)
			{
				return i - 1;
			}
			if (num2 == 0)
			{
				return 0;
			}
			if ((float)x < this.line.widths[i])
			{
				if (i == 1 && (float)x > this.line.widths[1] / 2f)
				{
					return i;
				}
				return i - 1;
			}
			else
			{
				if ((float)x > this.line.widths[num2])
				{
					return num2;
				}
				while (i < num - 1)
				{
					int num3 = (num + i) / 2;
					float num4 = this.line.widths[num3];
					if (num4 < (float)x)
					{
						i = num3;
					}
					else
					{
						num = num3;
					}
				}
				float num5 = this.line.widths[num] - this.line.widths[i];
				if ((float)x - this.line.widths[i] >= num5 / 2f)
				{
					return num;
				}
				return i;
			}
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00077DBC File Offset: 0x00075FBC
		public static LineTag GetFinalTag(LineTag tag)
		{
			LineTag lineTag = tag;
			while (lineTag.Length == 0 && lineTag.next != null && lineTag.next.Length == 0)
			{
				lineTag = lineTag.next;
			}
			return lineTag;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00077E00 File Offset: 0x00076000
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00077E08 File Offset: 0x00076008
		internal virtual int MaxHeight()
		{
			return this.font.Height;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00077E18 File Offset: 0x00076018
		private static void SetFormat(LineTag tag, Font font, Color color, Color back_color, FormatSpecified specified)
		{
			if ((FormatSpecified.Font & specified) == FormatSpecified.Font)
			{
				tag.Font = font;
			}
			if ((FormatSpecified.Color & specified) == FormatSpecified.Color)
			{
				tag.color = color;
			}
			if ((FormatSpecified.BackColor & specified) == FormatSpecified.BackColor)
			{
				tag.back_color = back_color;
			}
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00077E50 File Offset: 0x00076050
		public virtual SizeF SizeOfPosition(Graphics dc, int pos)
		{
			if (pos >= this.line.TextLengthWithoutEnding() && this.line.document.multiline)
			{
				return SizeF.Empty;
			}
			string text = this.line.text.ToString(pos, 1);
			switch (text.get_Chars(0))
			{
			case '\t':
				if (this.line.document.multiline)
				{
					SizeF sizeF = TextBoxTextRenderer.MeasureText(dc, " ", this.font);
					sizeF.Width *= 8f;
					return sizeF;
				}
				break;
			case '\n':
			case '\r':
				break;
			case '\v':
			case '\f':
				goto IL_00BC;
			default:
				goto IL_00BC;
			}
			return TextBoxTextRenderer.MeasureText(dc, "\r", this.font);
			IL_00BC:
			return TextBoxTextRenderer.MeasureText(dc, text, this.font);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00077F28 File Offset: 0x00076128
		public virtual string Text()
		{
			return this.line.text.ToString(this.start - 1, this.Length);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x00077F48 File Offset: 0x00076148
		public override string ToString()
		{
			if (this.Length > 0)
			{
				return string.Format("{0} Tag starts at index: {1}, length: {2}, text: {3}, font: {4}", new object[]
				{
					base.GetType(),
					this.start,
					this.Length,
					this.Text(),
					this.font.ToString()
				});
			}
			return string.Format("Zero Length tag at index: {0}", this.start);
		}

		// Token: 0x0400115B RID: 4443
		private Font font;

		// Token: 0x0400115C RID: 4444
		private Color color;

		// Token: 0x0400115D RID: 4445
		private Color back_color;

		// Token: 0x0400115E RID: 4446
		private Font link_font;

		// Token: 0x0400115F RID: 4447
		private bool is_link;

		// Token: 0x04001160 RID: 4448
		private string link_text;

		// Token: 0x04001161 RID: 4449
		private int start;

		// Token: 0x04001162 RID: 4450
		private int height;

		// Token: 0x04001163 RID: 4451
		private int ascent;

		// Token: 0x04001164 RID: 4452
		private int descent;

		// Token: 0x04001165 RID: 4453
		private int shift;

		// Token: 0x04001166 RID: 4454
		private Line line;

		// Token: 0x04001167 RID: 4455
		private LineTag next;

		// Token: 0x04001168 RID: 4456
		private LineTag previous;
	}
}

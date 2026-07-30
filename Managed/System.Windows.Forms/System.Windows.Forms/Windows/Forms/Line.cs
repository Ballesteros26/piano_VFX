using System;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000203 RID: 515
	internal class Line : ICloneable, IComparable
	{
		// Token: 0x06001F98 RID: 8088 RVA: 0x000763E4 File Offset: 0x000745E4
		internal Line(Document document, LineEnding ending)
		{
			this.document = document;
			this.color = LineColor.Red;
			this.left = null;
			this.right = null;
			this.parent = null;
			this.text = null;
			this.recalc = true;
			this.alignment = document.alignment;
			this.ending = ending;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0007643C File Offset: 0x0007463C
		internal Line(Document document, int LineNo, string Text, Font font, Color color, LineEnding ending)
			: this(document, ending)
		{
			this.space = ((Text.Length <= Line.DEFAULT_TEXT_LEN) ? Line.DEFAULT_TEXT_LEN : (Text.Length + 1));
			this.text = new StringBuilder(Text, this.space);
			this.line_no = LineNo;
			this.ending = ending;
			this.widths = new float[this.space + 1];
			this.tags = new LineTag(this, 1);
			this.tags.Font = font;
			this.tags.Color = color;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000764D8 File Offset: 0x000746D8
		internal Line(Document document, int LineNo, string Text, HorizontalAlignment align, Font font, Color color, LineEnding ending)
			: this(document, ending)
		{
			this.space = ((Text.Length <= Line.DEFAULT_TEXT_LEN) ? Line.DEFAULT_TEXT_LEN : (Text.Length + 1));
			this.text = new StringBuilder(Text, this.space);
			this.line_no = LineNo;
			this.ending = ending;
			this.alignment = align;
			this.widths = new float[this.space + 1];
			this.tags = new LineTag(this, 1);
			this.tags.Font = font;
			this.tags.Color = color;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0007657C File Offset: 0x0007477C
		internal Line(Document document, int LineNo, string Text, LineTag tag, LineEnding ending)
			: this(document, ending)
		{
			this.space = ((Text.Length <= Line.DEFAULT_TEXT_LEN) ? Line.DEFAULT_TEXT_LEN : (Text.Length + 1));
			this.text = new StringBuilder(Text, this.space);
			this.ending = ending;
			this.line_no = LineNo;
			this.widths = new float[this.space + 1];
			this.tags = tag;
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x000765FC File Offset: 0x000747FC
		// (set) Token: 0x06001F9E RID: 8094 RVA: 0x00076604 File Offset: 0x00074804
		internal HorizontalAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (this.alignment != value)
				{
					this.alignment = value;
					this.recalc = true;
				}
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x00076620 File Offset: 0x00074820
		// (set) Token: 0x06001FA0 RID: 8096 RVA: 0x00076628 File Offset: 0x00074828
		internal int HangingIndent
		{
			get
			{
				return this.hanging_indent;
			}
			set
			{
				this.hanging_indent = value;
				this.recalc = true;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x00076638 File Offset: 0x00074838
		// (set) Token: 0x06001FA2 RID: 8098 RVA: 0x00076640 File Offset: 0x00074840
		internal int Height
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

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0007664C File Offset: 0x0007484C
		// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x00076654 File Offset: 0x00074854
		internal int Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
				this.recalc = true;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x00076664 File Offset: 0x00074864
		// (set) Token: 0x06001FA6 RID: 8102 RVA: 0x0007666C File Offset: 0x0007486C
		internal int LineNo
		{
			get
			{
				return this.line_no;
			}
			set
			{
				this.line_no = value;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x00076678 File Offset: 0x00074878
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x00076680 File Offset: 0x00074880
		internal int RightIndent
		{
			get
			{
				return this.right_indent;
			}
			set
			{
				this.right_indent = value;
				this.recalc = true;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x00076690 File Offset: 0x00074890
		internal int Width
		{
			get
			{
				return (int)this.widths[this.text.Length];
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x000766B4 File Offset: 0x000748B4
		// (set) Token: 0x06001FAB RID: 8107 RVA: 0x000766C4 File Offset: 0x000748C4
		internal string Text
		{
			get
			{
				return this.text.ToString();
			}
			set
			{
				int length = this.text.Length;
				this.text = new StringBuilder(value, (value.Length <= Line.DEFAULT_TEXT_LEN) ? Line.DEFAULT_TEXT_LEN : (value.Length + 1));
				if (this.text.Length > length)
				{
					this.Grow(this.text.Length - length);
				}
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x00076730 File Offset: 0x00074930
		internal int X
		{
			get
			{
				if (this.document.multiline)
				{
					return this.align_shift;
				}
				return this.offset + this.align_shift;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x00076764 File Offset: 0x00074964
		internal int Y
		{
			get
			{
				if (!this.document.multiline)
				{
					return this.document.top_margin;
				}
				return this.document.top_margin + this.offset;
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000767A0 File Offset: 0x000749A0
		internal void LinkRecord(StringBuilder linkRecord)
		{
			for (LineTag next = this.tags; next != null; next = next.Next)
			{
				if (next.IsLink)
				{
					linkRecord.Append("L");
				}
				else
				{
					linkRecord.Append("N");
				}
			}
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000767F0 File Offset: 0x000749F0
		internal void ClearLinks()
		{
			for (LineTag next = this.tags; next != null; next = next.Next)
			{
				next.IsLink = false;
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00076820 File Offset: 0x00074A20
		public void DeleteCharacters(int pos, int count)
		{
			bool flag = false;
			if (pos >= this.text.Length)
			{
				return;
			}
			LineTag lineTag = this.FindTag(pos + 1);
			this.text.Remove(pos, count);
			if (lineTag == null)
			{
				return;
			}
			if (pos + count > lineTag.Start + lineTag.Length - 1)
			{
				flag = true;
				int num = count - (lineTag.Start + lineTag.Length - pos - 1);
				lineTag = lineTag.Next;
				while (lineTag != null && num > 0)
				{
					int length = lineTag.Length;
					lineTag.Start -= count - num;
					if (length > num)
					{
						num = 0;
					}
					else
					{
						num -= length;
						lineTag = lineTag.Next;
					}
				}
			}
			else if (lineTag.Length == 0)
			{
				flag = true;
			}
			LineTag lineTag2 = lineTag;
			while (lineTag2 != null && lineTag2.Next != null && lineTag2.Next.Length == 0)
			{
				LineTag lineTag3 = lineTag2;
				lineTag2.Next = lineTag2.Next.Next;
				if (lineTag2.Next != null)
				{
					lineTag2.Next.Previous = lineTag3;
				}
				lineTag2 = lineTag2.Next;
			}
			if (lineTag != null)
			{
				for (lineTag = lineTag.Next; lineTag != null; lineTag = lineTag.Next)
				{
					lineTag.Start -= count;
				}
			}
			this.recalc = true;
			if (flag)
			{
				this.Streamline(this.document.Lines);
			}
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0007699C File Offset: 0x00074B9C
		internal void DrawEnding(Graphics dc, float y)
		{
			if (this.document.multiline)
			{
				return;
			}
			LineTag next = this.tags;
			while (next.Next != null)
			{
				next = next.Next;
			}
			string text = null;
			switch (this.document.LineEndingLength(this.ending))
			{
			case 0:
				return;
			case 1:
				text = "\u0013";
				break;
			case 2:
				text = "\u0013\u0013";
				break;
			case 3:
				text = "\u0013\u0013\u0013";
				break;
			}
			TextBoxTextRenderer.DrawText(dc, text, next.Font, next.Color, (float)this.X + this.widths[this.TextLengthWithoutEnding()] - (float)this.document.viewport_x + (float)this.document.OffsetX, y, true);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00076A70 File Offset: 0x00074C70
		internal LineTag FindTag(int pos)
		{
			if (pos == 0)
			{
				return this.tags;
			}
			LineTag next = this.tags;
			if (pos >= this.text.Length)
			{
				pos = this.text.Length - 1;
			}
			while (next != null)
			{
				if (next.Start - 1 <= pos && pos <= next.Start + next.Length - 1)
				{
					return LineTag.GetFinalTag(next);
				}
				next = next.Next;
			}
			return null;
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00076AF0 File Offset: 0x00074CF0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00076AF8 File Offset: 0x00074CF8
		public LineTag GetTag(int x)
		{
			LineTag next = this.tags;
			if ((float)x < next.X)
			{
				return LineTag.GetFinalTag(next);
			}
			while ((float)x < next.X || (float)x >= next.X + next.Width)
			{
				if (next.Next == null)
				{
					return LineTag.GetFinalTag(next);
				}
				next = next.Next;
			}
			return next;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x00076B68 File Offset: 0x00074D68
		internal void Grow(int minimum)
		{
			int length = this.text.Length;
			if (length + minimum > this.space)
			{
				float[] array;
				if (length + minimum > this.space * 2)
				{
					array = new float[length + minimum * 2 + 1];
					this.space = length + minimum * 2;
				}
				else
				{
					array = new float[this.space * 2 + 1];
					this.space *= 2;
				}
				this.widths.CopyTo(array, 0);
				this.widths = array;
			}
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x00076BF0 File Offset: 0x00074DF0
		public void InsertString(int pos, string s)
		{
			this.InsertString(pos, s, this.FindTag(pos));
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00076C04 File Offset: 0x00074E04
		public void InsertString(int pos, string s, LineTag tag)
		{
			int length = s.Length;
			this.text.Insert(pos, s);
			for (tag = tag.Next; tag != null; tag = tag.Next)
			{
				tag.Start += length;
			}
			this.Grow(length);
			this.recalc = true;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00076C60 File Offset: 0x00074E60
		internal bool RecalculateLine(Graphics g, Document doc)
		{
			int i = 0;
			int num = this.text.Length;
			LineTag lineTag = this.tags;
			int num2 = this.offset;
			int num3 = this.height;
			int num4 = this.ascent;
			this.height = 0;
			this.ascent = 0;
			lineTag.Shift = 0;
			if (this.ending == LineEnding.Wrap)
			{
				this.widths[0] = (float)(this.document.left_margin + this.hanging_indent);
			}
			else
			{
				this.widths[0] = (float)(this.document.left_margin + this.indent);
			}
			this.recalc = false;
			bool flag = false;
			bool flag2 = false;
			int num5 = 0;
			while (i < num)
			{
				while (lineTag.Length == 0)
				{
					lineTag.Shift = (lineTag.Line.ascent - lineTag.Ascent) / 72;
					lineTag = lineTag.Next;
				}
				float width = lineTag.SizeOfPosition(g, i).Width;
				if (char.IsWhiteSpace(this.text.get_Chars(i)))
				{
					num5 = i + 1;
				}
				if (doc.wrap)
				{
					if (num5 > 0 && num5 != num && this.widths[i] + width + 5f > (float)(doc.viewport_width - this.right_indent))
					{
						this.widths[i + 1] = this.widths[i] + width;
						i = num5;
						num = this.text.Length;
						doc.Split(this, lineTag, i);
						this.ending = LineEnding.Wrap;
						num = this.text.Length;
						flag = true;
						flag2 = true;
					}
					else if (i > 1 && this.widths[i] + width > (float)(doc.viewport_width - this.right_indent))
					{
						this.widths[i + 1] = this.widths[i] + width;
						doc.Split(this, lineTag, i);
						this.ending = LineEnding.Wrap;
						num = this.text.Length;
						flag = true;
						flag2 = true;
					}
				}
				if (!flag2)
				{
					i++;
					this.widths[i] = this.widths[i - 1] + width;
					if (i == num)
					{
						Line line = doc.GetLine(this.line_no + 1);
						if (line != null && (this.ending == LineEnding.Wrap || this.ending == LineEnding.None))
						{
							doc.Combine(this.line_no, this.line_no + 1);
							num = this.text.Length;
							flag = true;
						}
					}
				}
				if (i == lineTag.Start - 1 + lineTag.Length)
				{
					lineTag.Height = lineTag.MaxHeight();
					if (lineTag.Height > this.height)
					{
						this.height = lineTag.Height;
					}
					if (lineTag.Ascent > this.ascent)
					{
						LineTag next = this.tags;
						while (next != null && next != lineTag)
						{
							next.Shift = (lineTag.Ascent - next.Ascent) / 72;
							next = next.Next;
						}
						this.ascent = lineTag.Ascent;
					}
					else
					{
						lineTag.Shift = (this.ascent - lineTag.Ascent) / 72;
					}
					lineTag = lineTag.Next;
					if (lineTag != null)
					{
						lineTag.Shift = 0;
						num5 = i;
					}
				}
			}
			while (lineTag != null)
			{
				lineTag.Shift = (lineTag.Line.ascent - lineTag.Ascent) / 72;
				lineTag = lineTag.Next;
			}
			if (this.height == 0)
			{
				this.height = this.tags.Font.Height;
				this.tags.Height = this.height;
				this.tags.Shift = 0;
			}
			if (num2 != this.offset || num3 != this.height || num4 != this.ascent)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x00077030 File Offset: 0x00075230
		internal bool RecalculatePasswordLine(Graphics g, Document doc)
		{
			int i = 0;
			int length = this.text.Length;
			LineTag lineTag = this.tags;
			this.ascent = 0;
			lineTag.Shift = 0;
			this.recalc = false;
			this.widths[0] = (float)(this.document.left_margin + this.indent);
			float width = TextBoxTextRenderer.MeasureText(g, doc.password_char, this.tags.Font).Width;
			bool flag = this.height != lineTag.Font.Height;
			this.height = lineTag.Font.Height;
			lineTag.Height = this.height;
			this.ascent = lineTag.Ascent;
			while (i < length)
			{
				i++;
				this.widths[i] = this.widths[i - 1] + width;
			}
			return flag;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00077114 File Offset: 0x00075314
		internal void Streamline(int lines)
		{
			LineTag lineTag = this.tags;
			LineTag lineTag2 = lineTag.Next;
			while (lineTag.Length == 0 && lineTag2 != null && lineTag2.IsTextTag)
			{
				this.tags = lineTag2;
				this.tags.Previous = null;
				lineTag = lineTag2;
				lineTag2 = lineTag.Next;
			}
			if (lineTag2 == null)
			{
				return;
			}
			while (lineTag2 != null)
			{
				if (lineTag.IsTextTag && lineTag2.Length == 0 && lineTag2.IsTextTag && (lineTag2.Next != null || this.line_no != lines))
				{
					lineTag.Next = lineTag2.Next;
					if (lineTag.Next != null)
					{
						lineTag.Next.Previous = lineTag;
					}
					lineTag2 = lineTag.Next;
				}
				else if (lineTag.Combine(lineTag2))
				{
					lineTag2 = lineTag.Next;
				}
				else
				{
					lineTag = lineTag.Next;
					lineTag2 = lineTag.Next;
				}
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0007720C File Offset: 0x0007540C
		internal int TextLengthWithoutEnding()
		{
			return this.text.Length - this.document.LineEndingLength(this.ending);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0007722C File Offset: 0x0007542C
		internal string TextWithoutEnding()
		{
			return this.text.ToString(0, this.text.Length - this.document.LineEndingLength(this.ending));
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00077264 File Offset: 0x00075464
		public object Clone()
		{
			Line line = new Line(this.document, this.ending);
			line.text = this.text;
			if (this.left != null)
			{
				line.left = (Line)this.left.Clone();
			}
			if (this.left != null)
			{
				line.left = (Line)this.left.Clone();
			}
			return line;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x000772D4 File Offset: 0x000754D4
		internal object CloneLine()
		{
			return new Line(this.document, this.ending)
			{
				text = this.text
			};
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00077300 File Offset: 0x00075500
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is Line))
			{
				throw new ArgumentException("Object is not of type Line", "obj");
			}
			if (this.line_no < ((Line)obj).line_no)
			{
				return -1;
			}
			if (this.line_no > ((Line)obj).line_no)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00077364 File Offset: 0x00075564
		public override bool Equals(object obj)
		{
			return obj != null && obj is Line && (obj == this || this.line_no == ((Line)obj).line_no);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x000773A0 File Offset: 0x000755A0
		public override string ToString()
		{
			return string.Format("Line {0}", this.line_no);
		}

		// Token: 0x04001146 RID: 4422
		internal Document document;

		// Token: 0x04001147 RID: 4423
		internal StringBuilder text;

		// Token: 0x04001148 RID: 4424
		internal float[] widths;

		// Token: 0x04001149 RID: 4425
		internal int space;

		// Token: 0x0400114A RID: 4426
		internal int line_no;

		// Token: 0x0400114B RID: 4427
		internal LineTag tags;

		// Token: 0x0400114C RID: 4428
		internal int offset;

		// Token: 0x0400114D RID: 4429
		internal int height;

		// Token: 0x0400114E RID: 4430
		internal int ascent;

		// Token: 0x0400114F RID: 4431
		internal HorizontalAlignment alignment;

		// Token: 0x04001150 RID: 4432
		internal int align_shift;

		// Token: 0x04001151 RID: 4433
		internal int indent;

		// Token: 0x04001152 RID: 4434
		internal int hanging_indent;

		// Token: 0x04001153 RID: 4435
		internal int right_indent;

		// Token: 0x04001154 RID: 4436
		internal LineEnding ending;

		// Token: 0x04001155 RID: 4437
		internal Line parent;

		// Token: 0x04001156 RID: 4438
		internal Line left;

		// Token: 0x04001157 RID: 4439
		internal Line right;

		// Token: 0x04001158 RID: 4440
		internal LineColor color;

		// Token: 0x04001159 RID: 4441
		private static int DEFAULT_TEXT_LEN;

		// Token: 0x0400115A RID: 4442
		internal bool recalc;
	}
}

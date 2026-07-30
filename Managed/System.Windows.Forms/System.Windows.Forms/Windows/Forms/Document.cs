using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms.RTF;

namespace System.Windows.Forms
{
	// Token: 0x0200031B RID: 795
	internal class Document : IEnumerable, ICloneable
	{
		// Token: 0x060034FB RID: 13563 RVA: 0x000CA59C File Offset: 0x000C879C
		internal Document(TextBoxBase owner)
		{
			this.lines = 0;
			this.owner = owner;
			this.multiline = true;
			this.password_char = string.Empty;
			this.calc_pass = false;
			this.recalc_pending = false;
			this.sentinel = new Line(this, LineEnding.None);
			this.sentinel.color = LineColor.Black;
			this.document = this.sentinel;
			owner.HandleCreated += new EventHandler(this.owner_HandleCreated);
			owner.VisibleChanged += new EventHandler(this.owner_VisibleChanged);
			this.Add(1, string.Empty, owner.Font, owner.ForeColor, LineEnding.None);
			this.undo = new UndoManager(this);
			this.selection_visible = false;
			this.selection_start.line = this.document;
			this.selection_start.pos = 0;
			this.selection_start.tag = this.selection_start.line.tags;
			this.selection_end.line = this.document;
			this.selection_end.pos = 0;
			this.selection_end.tag = this.selection_end.line.tags;
			this.selection_anchor.line = this.document;
			this.selection_anchor.pos = 0;
			this.selection_anchor.tag = this.selection_anchor.line.tags;
			this.caret.line = this.document;
			this.caret.pos = 0;
			this.caret.tag = this.caret.line.tags;
			this.viewport_x = 0;
			this.viewport_y = 0;
			this.offset_x = 0;
			this.offset_y = 0;
			this.crlf_size = 2;
			this.document_id = this.random.Next();
			Document.string_format.Trimming = 0;
			Document.string_format.FormatFlags = 32;
			this.UpdateMargins();
		}

		// Token: 0x1400033D RID: 829
		// (add) Token: 0x060034FD RID: 13565 RVA: 0x000CA7D4 File Offset: 0x000C89D4
		// (remove) Token: 0x060034FE RID: 13566 RVA: 0x000CA7F0 File Offset: 0x000C89F0
		internal event EventHandler CaretMoved;

		// Token: 0x1400033E RID: 830
		// (add) Token: 0x060034FF RID: 13567 RVA: 0x000CA80C File Offset: 0x000C8A0C
		// (remove) Token: 0x06003500 RID: 13568 RVA: 0x000CA828 File Offset: 0x000C8A28
		internal event EventHandler WidthChanged;

		// Token: 0x1400033F RID: 831
		// (add) Token: 0x06003501 RID: 13569 RVA: 0x000CA844 File Offset: 0x000C8A44
		// (remove) Token: 0x06003502 RID: 13570 RVA: 0x000CA860 File Offset: 0x000C8A60
		internal event EventHandler HeightChanged;

		// Token: 0x14000340 RID: 832
		// (add) Token: 0x06003503 RID: 13571 RVA: 0x000CA87C File Offset: 0x000C8A7C
		// (remove) Token: 0x06003504 RID: 13572 RVA: 0x000CA898 File Offset: 0x000C8A98
		internal event EventHandler LengthChanged;

		// Token: 0x14000341 RID: 833
		// (add) Token: 0x06003505 RID: 13573 RVA: 0x000CA8B4 File Offset: 0x000C8AB4
		// (remove) Token: 0x06003506 RID: 13574 RVA: 0x000CA8D0 File Offset: 0x000C8AD0
		internal event EventHandler UIASelectionChanged;

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x000CA8EC File Offset: 0x000C8AEC
		// (set) Token: 0x06003508 RID: 13576 RVA: 0x000CA8F4 File Offset: 0x000C8AF4
		internal Line Root
		{
			get
			{
				return this.document;
			}
			set
			{
				this.document = value;
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x000CA900 File Offset: 0x000C8B00
		internal int Lines
		{
			get
			{
				return this.lines;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x000CA908 File Offset: 0x000C8B08
		internal Line CaretLine
		{
			get
			{
				return this.caret.line;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x000CA918 File Offset: 0x000C8B18
		internal int CaretPosition
		{
			get
			{
				return this.caret.pos;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x000CA928 File Offset: 0x000C8B28
		internal Point Caret
		{
			get
			{
				return new Point((int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X, this.caret.line.Y);
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000CA980 File Offset: 0x000C8B80
		// (set) Token: 0x0600350E RID: 13582 RVA: 0x000CA990 File Offset: 0x000C8B90
		internal LineTag CaretTag
		{
			get
			{
				return this.caret.tag;
			}
			set
			{
				this.caret.tag = value;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x000CA9A0 File Offset: 0x000C8BA0
		// (set) Token: 0x06003510 RID: 13584 RVA: 0x000CA9A8 File Offset: 0x000C8BA8
		internal int CRLFSize
		{
			get
			{
				return this.crlf_size;
			}
			set
			{
				this.crlf_size = value;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000CA9B4 File Offset: 0x000C8BB4
		// (set) Token: 0x06003512 RID: 13586 RVA: 0x000CA9BC File Offset: 0x000C8BBC
		internal bool EnableLinks
		{
			get
			{
				return this.enable_links;
			}
			set
			{
				this.enable_links = value;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000CA9C8 File Offset: 0x000C8BC8
		// (set) Token: 0x06003514 RID: 13588 RVA: 0x000CA9D0 File Offset: 0x000C8BD0
		internal string PasswordChar
		{
			get
			{
				return this.password_char;
			}
			set
			{
				this.password_char = value;
				this.PasswordCache.Length = 0;
				if (this.password_char.Length != 0 && this.password_char.get_Chars(0) != '\0')
				{
					this.calc_pass = true;
				}
				else
				{
					this.calc_pass = false;
				}
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000CAA24 File Offset: 0x000C8C24
		private StringBuilder PasswordCache
		{
			get
			{
				if (this.password_cache == null)
				{
					this.password_cache = new StringBuilder();
				}
				return this.password_cache;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x000CAA44 File Offset: 0x000C8C44
		// (set) Token: 0x06003517 RID: 13591 RVA: 0x000CAA4C File Offset: 0x000C8C4C
		internal int ViewPortX
		{
			get
			{
				return this.viewport_x;
			}
			set
			{
				this.viewport_x = value;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000CAA58 File Offset: 0x000C8C58
		internal int Length
		{
			get
			{
				return this.char_count + this.lines - 1;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000CAA6C File Offset: 0x000C8C6C
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x000CAA74 File Offset: 0x000C8C74
		private int CharCount
		{
			get
			{
				return this.char_count;
			}
			set
			{
				this.char_count = value;
				if (this.LengthChanged != null)
				{
					this.LengthChanged.Invoke(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000CAA9C File Offset: 0x000C8C9C
		// (set) Token: 0x0600351C RID: 13596 RVA: 0x000CAAA4 File Offset: 0x000C8CA4
		internal int ViewPortY
		{
			get
			{
				return this.viewport_y;
			}
			set
			{
				this.viewport_y = value;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000CAAB0 File Offset: 0x000C8CB0
		// (set) Token: 0x0600351E RID: 13598 RVA: 0x000CAAB8 File Offset: 0x000C8CB8
		internal int OffsetX
		{
			get
			{
				return this.offset_x;
			}
			set
			{
				this.offset_x = value;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000CAAC4 File Offset: 0x000C8CC4
		// (set) Token: 0x06003520 RID: 13600 RVA: 0x000CAACC File Offset: 0x000C8CCC
		internal int OffsetY
		{
			get
			{
				return this.offset_y;
			}
			set
			{
				this.offset_y = value;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000CAAD8 File Offset: 0x000C8CD8
		// (set) Token: 0x06003522 RID: 13602 RVA: 0x000CAAE0 File Offset: 0x000C8CE0
		internal int ViewPortWidth
		{
			get
			{
				return this.viewport_width;
			}
			set
			{
				this.viewport_width = value;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x000CAAEC File Offset: 0x000C8CEC
		// (set) Token: 0x06003524 RID: 13604 RVA: 0x000CAAF4 File Offset: 0x000C8CF4
		internal int ViewPortHeight
		{
			get
			{
				return this.viewport_height;
			}
			set
			{
				this.viewport_height = value;
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000CAB00 File Offset: 0x000C8D00
		internal int Width
		{
			get
			{
				return this.document_x;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000CAB08 File Offset: 0x000C8D08
		internal int Height
		{
			get
			{
				return this.document_y;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x000CAB10 File Offset: 0x000C8D10
		internal bool SelectionVisible
		{
			get
			{
				return this.selection_visible;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000CAB18 File Offset: 0x000C8D18
		// (set) Token: 0x06003529 RID: 13609 RVA: 0x000CAB20 File Offset: 0x000C8D20
		internal bool Wrap
		{
			get
			{
				return this.wrap;
			}
			set
			{
				this.wrap = value;
			}
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000CAB2C File Offset: 0x000C8D2C
		internal void UpdateMargins()
		{
			switch (this.owner.actual_border_style)
			{
			case BorderStyle.None:
				this.left_margin = 0;
				this.top_margin = 0;
				this.right_margin = 1;
				break;
			case BorderStyle.FixedSingle:
				this.left_margin = 2;
				this.top_margin = 2;
				this.right_margin = 3;
				break;
			case BorderStyle.Fixed3D:
				this.left_margin = 1;
				this.top_margin = 1;
				this.right_margin = 2;
				break;
			}
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000CABAC File Offset: 0x000C8DAC
		internal void SuspendRecalc()
		{
			if (this.recalc_suspended == 0)
			{
				this.recalc_start = int.MaxValue;
				this.recalc_end = int.MinValue;
			}
			this.recalc_suspended++;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000CABE0 File Offset: 0x000C8DE0
		internal void ResumeRecalc(bool immediate_update)
		{
			if (this.recalc_suspended > 0)
			{
				this.recalc_suspended--;
			}
			if (this.recalc_suspended == 0 && (immediate_update || this.recalc_pending) && (this.recalc_start != 2147483647 || this.recalc_end != -2147483648))
			{
				this.RecalculateDocument(this.owner.CreateGraphicsInternal(), this.recalc_start, this.recalc_end, this.recalc_optimize);
				this.recalc_pending = false;
			}
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000CAC70 File Offset: 0x000C8E70
		internal void SuspendUpdate()
		{
			this.update_suspended++;
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000CAC80 File Offset: 0x000C8E80
		internal void ResumeUpdate(bool immediate_update)
		{
			if (this.update_suspended > 0)
			{
				this.update_suspended--;
			}
			if (immediate_update && this.update_suspended == 0 && this.update_pending)
			{
				this.UpdateView(this.GetLine(this.update_start), 0);
				this.update_pending = false;
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000CACE0 File Offset: 0x000C8EE0
		internal int DumpTree(Line line, bool with_tags)
		{
			int num = 1;
			Console.Write("Line {0} [# {1}], Y: {2}, ending style: {3},  Text: '{4}'", new object[]
			{
				line.line_no,
				line.GetHashCode(),
				line.Y,
				line.ending,
				(line.text == null) ? "undefined" : line.text.ToString()
			});
			if (line.left == this.sentinel)
			{
				Console.Write(", left = sentinel");
			}
			else if (line.left == null)
			{
				Console.Write(", left = NULL");
			}
			if (line.right == this.sentinel)
			{
				Console.Write(", right = sentinel");
			}
			else if (line.right == null)
			{
				Console.Write(", right = NULL");
			}
			Console.WriteLine(string.Empty);
			if (with_tags)
			{
				LineTag lineTag = line.tags;
				int num2 = 1;
				int num3 = 0;
				Console.Write("   Tags: ");
				while (lineTag != null)
				{
					Console.Write("{0} <{1}>-<{2}>", num2++, lineTag.Start, lineTag.End);
					num3 += lineTag.Length;
					if (lineTag.Line != line)
					{
						Console.Write("BAD line link");
						throw new Exception("Bad line link in tree");
					}
					lineTag = lineTag.Next;
					if (lineTag != null)
					{
						Console.Write(", ");
					}
				}
				if (num3 > line.text.Length)
				{
					throw new Exception(string.Format("Length of tags more than length of text on line (expected {0} calculated {1})", line.text.Length, num3));
				}
				if (num3 < line.text.Length)
				{
					throw new Exception(string.Format("Length of tags less than length of text on line (expected {0} calculated {1})", line.text.Length, num3));
				}
				Console.WriteLine(string.Empty);
			}
			if (line.left != null)
			{
				if (line.left != this.sentinel)
				{
					num += this.DumpTree(line.left, with_tags);
				}
			}
			else if (line != this.sentinel)
			{
				throw new Exception("Left should not be NULL");
			}
			if (line.right != null)
			{
				if (line.right != this.sentinel)
				{
					num += this.DumpTree(line.right, with_tags);
				}
			}
			else if (line != this.sentinel)
			{
				throw new Exception("Right should not be NULL");
			}
			for (int i = 1; i <= this.lines; i++)
			{
				if (this.GetLine(i) == null)
				{
					throw new Exception(string.Format("Hole in line order, missing {0}", i));
				}
			}
			if (line == this.Root)
			{
				if (num < this.lines)
				{
					throw new Exception(string.Format("Not enough nodes in tree, found {0}, expected {1}", num, this.lines));
				}
				if (num > this.lines)
				{
					throw new Exception(string.Format("Too many nodes in tree, found {0}, expected {1}", num, this.lines));
				}
			}
			return num;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000CB010 File Offset: 0x000C9210
		private void SetSelectionVisible(bool value)
		{
			bool flag = this.selection_visible;
			this.selection_visible = value;
			if (this.owner.IsHandleCreated && !this.owner.show_caret_w_selection)
			{
				XplatUI.CaretVisible(this.owner.Handle, !this.selection_visible);
			}
			if (this.UIASelectionChanged != null && (this.selection_visible || flag))
			{
				this.UIASelectionChanged.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000CB094 File Offset: 0x000C9294
		private void DecrementLines(int line_no)
		{
			for (int i = line_no; i <= this.lines; i++)
			{
				this.GetLine(i).line_no--;
			}
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000CB0CC File Offset: 0x000C92CC
		private void IncrementLines(int line_no)
		{
			for (int i = this.lines; i >= line_no; i--)
			{
				this.GetLine(i).line_no++;
			}
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000CB104 File Offset: 0x000C9304
		private void RebalanceAfterAdd(Line line1)
		{
			while (line1 != this.document && line1.parent.color == LineColor.Red)
			{
				if (line1.parent == line1.parent.parent.left)
				{
					Line line2 = line1.parent.parent.right;
					if (line2 != null && line2.color == LineColor.Red)
					{
						line1.parent.color = LineColor.Black;
						line2.color = LineColor.Black;
						line1.parent.parent.color = LineColor.Red;
						line1 = line1.parent.parent;
					}
					else
					{
						if (line1 == line1.parent.right)
						{
							line1 = line1.parent;
							this.RotateLeft(line1);
						}
						line1.parent.color = LineColor.Black;
						line1.parent.parent.color = LineColor.Red;
						this.RotateRight(line1.parent.parent);
					}
				}
				else
				{
					Line line2 = line1.parent.parent.left;
					if (line2 != null && line2.color == LineColor.Red)
					{
						line1.parent.color = LineColor.Black;
						line2.color = LineColor.Black;
						line1.parent.parent.color = LineColor.Red;
						line1 = line1.parent.parent;
					}
					else
					{
						if (line1 == line1.parent.left)
						{
							line1 = line1.parent;
							this.RotateRight(line1);
						}
						line1.parent.color = LineColor.Black;
						line1.parent.parent.color = LineColor.Red;
						this.RotateLeft(line1.parent.parent);
					}
				}
			}
			this.document.color = LineColor.Black;
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000CB2AC File Offset: 0x000C94AC
		private void RebalanceAfterDelete(Line line1)
		{
			while (line1 != this.document && line1.color == LineColor.Black)
			{
				if (line1 == line1.parent.left)
				{
					Line line2 = line1.parent.right;
					if (line2.color == LineColor.Red)
					{
						line2.color = LineColor.Black;
						line1.parent.color = LineColor.Red;
						this.RotateLeft(line1.parent);
						line2 = line1.parent.right;
					}
					if (line2.left.color == LineColor.Black && line2.right.color == LineColor.Black)
					{
						line2.color = LineColor.Red;
						line1 = line1.parent;
					}
					else
					{
						if (line2.right.color == LineColor.Black)
						{
							line2.left.color = LineColor.Black;
							line2.color = LineColor.Red;
							this.RotateRight(line2);
							line2 = line1.parent.right;
						}
						line2.color = line1.parent.color;
						line1.parent.color = LineColor.Black;
						line2.right.color = LineColor.Black;
						this.RotateLeft(line1.parent);
						line1 = this.document;
					}
				}
				else
				{
					Line line2 = line1.parent.left;
					if (line2.color == LineColor.Red)
					{
						line2.color = LineColor.Black;
						line1.parent.color = LineColor.Red;
						this.RotateRight(line1.parent);
						line2 = line1.parent.left;
					}
					if (line2.right.color == LineColor.Black && line2.left.color == LineColor.Black)
					{
						line2.color = LineColor.Red;
						line1 = line1.parent;
					}
					else
					{
						if (line2.left.color == LineColor.Black)
						{
							line2.right.color = LineColor.Black;
							line2.color = LineColor.Red;
							this.RotateLeft(line2);
							line2 = line1.parent.left;
						}
						line2.color = line1.parent.color;
						line1.parent.color = LineColor.Black;
						line2.left.color = LineColor.Black;
						this.RotateRight(line1.parent);
						line1 = this.document;
					}
				}
			}
			line1.color = LineColor.Black;
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000CB4CC File Offset: 0x000C96CC
		private void RotateLeft(Line line1)
		{
			Line right = line1.right;
			line1.right = right.left;
			if (right.left != this.sentinel)
			{
				right.left.parent = line1;
			}
			if (right != this.sentinel)
			{
				right.parent = line1.parent;
			}
			if (line1.parent != null)
			{
				if (line1 == line1.parent.left)
				{
					line1.parent.left = right;
				}
				else
				{
					line1.parent.right = right;
				}
			}
			else
			{
				this.document = right;
			}
			right.left = line1;
			if (line1 != this.sentinel)
			{
				line1.parent = right;
			}
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000CB580 File Offset: 0x000C9780
		private void RotateRight(Line line1)
		{
			Line left = line1.left;
			line1.left = left.right;
			if (left.right != this.sentinel)
			{
				left.right.parent = line1;
			}
			if (left != this.sentinel)
			{
				left.parent = line1.parent;
			}
			if (line1.parent != null)
			{
				if (line1 == line1.parent.right)
				{
					line1.parent.right = left;
				}
				else
				{
					line1.parent.left = left;
				}
			}
			else
			{
				this.document = left;
			}
			left.right = line1;
			if (line1 != this.sentinel)
			{
				line1.parent = left;
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000CB634 File Offset: 0x000C9834
		internal void UpdateView(Line line, int pos)
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			if (this.update_suspended > 0)
			{
				this.update_start = Math.Min(this.update_start, line.line_no);
				this.update_pending = true;
				return;
			}
			if (this.RecalculateDocument(this.owner.CreateGraphicsInternal(), line.line_no, line.line_no, true))
			{
				if (line.Y - this.viewport_y >= 0)
				{
					this.owner.Invalidate(new Rectangle(this.offset_x, line.Y - this.viewport_y + this.offset_y, this.viewport_width, this.owner.Height - (line.Y - this.viewport_y)));
				}
				else
				{
					this.owner.Invalidate();
				}
			}
			else
			{
				switch (line.alignment)
				{
				case HorizontalAlignment.Left:
					this.owner.Invalidate(new Rectangle(line.X + ((int)line.widths[pos] - this.viewport_x - 1) + this.offset_x, line.Y - this.viewport_y + this.offset_y, this.viewport_width, line.height + 1));
					break;
				case HorizontalAlignment.Right:
					this.owner.Invalidate(new Rectangle(line.X + this.offset_x, line.Y - this.viewport_y + this.offset_y, (int)line.widths[pos + 1] - this.viewport_x + line.X, line.height + 1));
					break;
				case HorizontalAlignment.Center:
					this.owner.Invalidate(new Rectangle(line.X + this.offset_x, line.Y - this.viewport_y + this.offset_y, this.viewport_width, line.height + 1));
					break;
				}
			}
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000CB824 File Offset: 0x000C9A24
		internal void UpdateView(Line line, int line_count, int pos)
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			if (this.recalc_suspended > 0)
			{
				this.recalc_start = Math.Min(this.recalc_start, line.line_no);
				this.recalc_end = Math.Max(this.recalc_end, line.line_no + line_count);
				this.recalc_optimize = true;
				this.recalc_pending = true;
				return;
			}
			int y = line.Y;
			Line line2 = this.GetLine(line.line_no + line_count);
			if (line2 == null)
			{
				line2 = this.GetLine(this.lines);
			}
			int num = line2.Y + line2.height;
			if (this.RecalculateDocument(this.owner.CreateGraphicsInternal(), line.line_no, line.line_no + line_count, true))
			{
				if (line.Y - this.viewport_y >= 0)
				{
					this.owner.Invalidate(new Rectangle(this.offset_x, line.Y - this.viewport_y + this.offset_y, this.viewport_width, this.owner.Height - (line.Y - this.viewport_y)));
				}
				else
				{
					this.owner.Invalidate();
				}
			}
			else
			{
				int num2 = 0 - this.viewport_x + this.offset_x;
				int num3 = this.viewport_width;
				int num4 = Math.Min(y - this.viewport_y, line.Y - this.viewport_y) + this.offset_y;
				int num5 = Math.Max(num - num4, line2.Y + line2.height - num4);
				this.owner.Invalidate(new Rectangle(num2, num4, num3, num5));
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000CB9C8 File Offset: 0x000C9BC8
		private void ScanForLinks(Line start_line, ref bool link_changed)
		{
			Line line = start_line;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			arrayList.Add(0);
			while (line != null)
			{
				stringBuilder.Append(line.text);
				if (!link_changed)
				{
					line.LinkRecord(stringBuilder2);
				}
				line.ClearLinks();
				arrayList.Add(stringBuilder.Length);
				if (line.ending != LineEnding.Wrap)
				{
					break;
				}
				line = this.GetLine(line.LineNo + 1);
			}
			string[] array = new string[] { "www.", "http:/", "ftp:/", "https:/" };
			int num = 0;
			string text = stringBuilder.ToString();
			int i = 0;
			while (i < text.Length)
			{
				int num2 = this.FirstIndexOfAny(text, array, i, out num);
				if (num2 != -1)
				{
					if (num == 0)
					{
						if (text.Length == num2 + array[0].Length)
						{
							break;
						}
						if (!char.IsLetterOrDigit(text.get_Chars(num2 + array[0].Length)) && "@/~".IndexOf(text.get_Chars(num2 + array[0].Length).ToString()) == -1)
						{
							i = num2 + array[0].Length;
							continue;
						}
					}
					int num3 = text.Length - 1;
					i = text.Length;
					for (int j = num2 + array[num].Length; j < text.Length; j++)
					{
						if (text.get_Chars(j - 1) == '.')
						{
							if (!char.IsLetterOrDigit(text.get_Chars(j)) && "@/~".IndexOf(text.get_Chars(j).ToString()) == -1)
							{
								num3 = j - 1;
								i = j;
								break;
							}
						}
						else if (!char.IsLetterOrDigit(text.get_Chars(j)) && "@-/:~.?=_&".IndexOf(text.get_Chars(j).ToString()) == -1)
						{
							num3 = j - 1;
							i = j;
							break;
						}
					}
					string text2 = text.Substring(num2, num3 - num2 + 1);
					int k;
					for (k = 1; k < arrayList.Count; k++)
					{
						if ((int)arrayList[k] > num2)
						{
							break;
						}
					}
					line = this.GetLine(start_line.LineNo + k - 1);
					LineTag lineTag = line.FindTag(num2 - (int)arrayList[k - 1] + 1);
					if (lineTag.Start != num2 - (int)arrayList[k - 1] + 1)
					{
						if (lineTag == this.CaretTag)
						{
							flag = true;
						}
						lineTag = lineTag.Break(num2 - (int)arrayList[k - 1] + 1);
					}
					lineTag.IsLink = true;
					lineTag.LinkText = text2;
					for (int l = 1; l < text2.Length; l++)
					{
						if ((int)arrayList[k] <= num2 + l)
						{
							line = this.GetLine(start_line.LineNo + k++);
							lineTag = line.FindTag(num2 + l - (int)arrayList[k - 1] + 1);
							lineTag.IsLink = true;
							lineTag.LinkText = text2;
						}
						else if (lineTag.End < num2 + 1 + l - (int)arrayList[k - 1])
						{
							do
							{
								lineTag = lineTag.Next;
							}
							while (lineTag.Length == 0);
							lineTag.IsLink = true;
							lineTag.LinkText = text2;
						}
					}
					if (lineTag.End > num2 + text2.Length + 1 - (int)arrayList[k - 1])
					{
						if (lineTag == this.CaretTag)
						{
							flag = true;
						}
						lineTag.Break(num2 + text2.Length + 1 - (int)arrayList[k - 1]);
					}
					continue;
				}
				IL_044C:
				if (flag)
				{
					this.CaretTag = LineTag.FindTag(this.CaretLine, this.CaretPosition);
					link_changed = true;
				}
				else if (!link_changed)
				{
					line = start_line;
					StringBuilder stringBuilder3 = new StringBuilder();
					while (line != null)
					{
						line.LinkRecord(stringBuilder3);
						if (line.ending != LineEnding.Wrap)
						{
							break;
						}
						line = this.GetLine(line.LineNo + 1);
					}
					if (!stringBuilder3.Equals(stringBuilder2))
					{
						link_changed = true;
					}
				}
				return;
			}
			goto IL_044C;
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000CBEA0 File Offset: 0x000CA0A0
		private int FirstIndexOfAny(string haystack, string[] needles, int start_index, out int term_found)
		{
			term_found = -1;
			int num = -1;
			for (int i = 0; i < needles.Length; i++)
			{
				int num2 = haystack.IndexOf(needles[i], start_index, 3);
				if (num2 > -1)
				{
					if (term_found > -1)
					{
						if (num2 < num)
						{
							num = num2;
							term_found = i;
						}
					}
					else
					{
						num = num2;
						term_found = i;
					}
				}
			}
			return num;
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000CBEFC File Offset: 0x000CA0FC
		private void InvalidateLinks(Rectangle clip)
		{
			for (int i = this.owner.list_links.Count - 1; i >= 0; i--)
			{
				TextBoxBase.LinkRectangle linkRectangle = (TextBoxBase.LinkRectangle)this.owner.list_links[i];
				if (clip.IntersectsWith(linkRectangle.LinkAreaRectangle))
				{
					this.owner.list_links.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000CBF68 File Offset: 0x000CA168
		internal void ScanForLinks(int start, int end, ref bool link_changed)
		{
			LineEnding lineEnding = LineEnding.Rich;
			while (start != 1 && this.GetLine(start - 1).ending == LineEnding.Wrap)
			{
				start--;
			}
			int num = start;
			while (num <= end && num <= this.lines)
			{
				Line line = this.GetLine(num);
				if (lineEnding != LineEnding.Wrap)
				{
					this.ScanForLinks(line, ref link_changed);
				}
				lineEnding = line.ending;
				if (lineEnding == LineEnding.Wrap && num + 1 <= end)
				{
					end++;
				}
				num++;
			}
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000CBFFC File Offset: 0x000CA1FC
		internal void Empty()
		{
			this.document = this.sentinel;
			this.lines = 0;
			this.Add(1, string.Empty, this.owner.Font, this.owner.ForeColor, LineEnding.None);
			this.RecalculateDocument(this.owner.CreateGraphicsInternal());
			this.PositionCaret(0, 0);
			this.SetSelectionVisible(false);
			this.selection_start.line = this.document;
			this.selection_start.pos = 0;
			this.selection_start.tag = this.selection_start.line.tags;
			this.selection_end.line = this.document;
			this.selection_end.pos = 0;
			this.selection_end.tag = this.selection_end.line.tags;
			this.char_count = 0;
			this.viewport_x = 0;
			this.viewport_y = 0;
			this.document_x = 0;
			this.document_y = 0;
			if (this.owner.IsHandleCreated)
			{
				this.owner.Invalidate();
			}
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000CC110 File Offset: 0x000CA310
		internal void PositionCaret(Line line, int pos)
		{
			this.caret.tag = line.FindTag(pos);
			this.MoveCaretToTextTag();
			this.caret.line = line;
			this.caret.pos = pos;
			if (this.owner.IsHandleCreated)
			{
				if (this.owner.Focused)
				{
					if (this.caret.height != this.caret.tag.Height)
					{
						XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
					}
					XplatUI.SetCaretPos(this.owner.Handle, this.offset_x + (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x, this.offset_y + this.caret.line.Y + this.caret.tag.Shift - this.viewport_y + Document.caret_shift);
				}
				if (this.CaretMoved != null)
				{
					this.CaretMoved.Invoke(this, EventArgs.Empty);
				}
			}
			this.caret.height = this.caret.tag.Height;
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000CC26C File Offset: 0x000CA46C
		internal void PositionCaret(int x, int y)
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			this.caret.tag = this.FindCursor(x, y, out this.caret.pos);
			this.MoveCaretToTextTag();
			this.caret.line = this.caret.tag.Line;
			this.caret.height = this.caret.tag.Height;
			if (this.owner.ShowSelection && (!this.selection_visible || this.owner.show_caret_w_selection))
			{
				XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
				XplatUI.SetCaretPos(this.owner.Handle, (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x + this.offset_x, this.offset_y + this.caret.line.Y + this.caret.tag.Shift - this.viewport_y + Document.caret_shift);
			}
			if (this.CaretMoved != null)
			{
				this.CaretMoved.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000CC3D4 File Offset: 0x000CA5D4
		internal void CaretHasFocus()
		{
			if (this.caret.tag != null && this.owner.IsHandleCreated)
			{
				XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
				XplatUI.SetCaretPos(this.owner.Handle, this.offset_x + (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x, this.offset_y + this.caret.line.Y + this.caret.tag.Shift - this.viewport_y + Document.caret_shift);
				this.DisplayCaret();
			}
			if (this.owner.IsHandleCreated && this.SelectionLength() > 0)
			{
				this.InvalidateSelectionArea();
			}
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000CC4D0 File Offset: 0x000CA6D0
		internal void CaretLostFocus()
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			XplatUI.DestroyCaret(this.owner.Handle);
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000CC4F4 File Offset: 0x000CA6F4
		internal void AlignCaret()
		{
			this.AlignCaret(true);
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000CC500 File Offset: 0x000CA700
		internal void AlignCaret(bool changeCaretTag)
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			if (changeCaretTag)
			{
				this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				this.MoveCaretToTextTag();
			}
			if (this.caret.tag.Height > this.caret.tag.Line.Height)
			{
				this.caret.height = this.caret.line.height;
			}
			else
			{
				this.caret.height = this.caret.tag.Height;
			}
			if (this.owner.Focused)
			{
				XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
				XplatUI.SetCaretPos(this.owner.Handle, this.offset_x + (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x, this.offset_y + this.caret.line.Y + this.viewport_y + Document.caret_shift);
				this.DisplayCaret();
			}
			if (this.CaretMoved != null)
			{
				this.CaretMoved.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000CC67C File Offset: 0x000CA87C
		internal void UpdateCaret()
		{
			if (!this.owner.IsHandleCreated || this.caret.tag == null)
			{
				return;
			}
			this.MoveCaretToTextTag();
			if (this.caret.tag.Height != this.caret.height)
			{
				this.caret.height = this.caret.tag.Height;
				if (this.owner.Focused)
				{
					XplatUI.CreateCaret(this.owner.Handle, Document.caret_width, this.caret.height);
				}
			}
			if (this.owner.Focused)
			{
				XplatUI.SetCaretPos(this.owner.Handle, this.offset_x + (int)this.caret.tag.Line.widths[this.caret.pos] + this.caret.line.X - this.viewport_x, this.offset_y + this.caret.line.Y + this.caret.tag.Shift - this.viewport_y + Document.caret_shift);
				this.DisplayCaret();
			}
			if (this.CaretMoved != null)
			{
				this.CaretMoved.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000CC7D4 File Offset: 0x000CA9D4
		internal void DisplayCaret()
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			if (this.owner.ShowSelection && (!this.selection_visible || this.owner.show_caret_w_selection))
			{
				XplatUI.CaretVisible(this.owner.Handle, true);
			}
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000CC830 File Offset: 0x000CAA30
		internal void HideCaret()
		{
			if (!this.owner.IsHandleCreated)
			{
				return;
			}
			if (this.owner.Focused)
			{
				XplatUI.CaretVisible(this.owner.Handle, false);
			}
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000CC870 File Offset: 0x000CAA70
		internal void MoveCaretToTextTag()
		{
			if (this.caret.tag == null || this.caret.tag.IsTextTag)
			{
				return;
			}
			if (this.caret.pos < this.caret.tag.Start)
			{
				this.caret.tag = this.caret.tag.Previous;
			}
			else
			{
				this.caret.tag = this.caret.tag.Next;
			}
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000CC900 File Offset: 0x000CAB00
		internal void MoveCaret(CaretDirection direction)
		{
			bool flag = false;
			switch (direction)
			{
			case CaretDirection.CharForward:
				break;
			case CaretDirection.CharBack:
				goto IL_018F;
			case CaretDirection.LineUp:
				if (this.caret.line.line_no > 1)
				{
					int num = (int)this.caret.line.widths[this.caret.pos];
					this.PositionCaret(num, this.GetLine(this.caret.line.line_no - 1).Y);
					this.DisplayCaret();
				}
				return;
			case CaretDirection.LineDown:
				if (this.caret.line.line_no < this.lines)
				{
					int num2 = (int)this.caret.line.widths[this.caret.pos];
					this.PositionCaret(num2, this.GetLine(this.caret.line.line_no + 1).Y);
					this.DisplayCaret();
				}
				return;
			case CaretDirection.Home:
				if (this.caret.pos > 0)
				{
					this.caret.pos = 0;
					this.caret.tag = this.caret.line.tags;
					this.UpdateCaret();
				}
				return;
			case CaretDirection.End:
				if (this.caret.pos < this.caret.line.TextLengthWithoutEnding())
				{
					this.caret.pos = this.caret.line.TextLengthWithoutEnding();
					this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
					this.UpdateCaret();
				}
				return;
			case CaretDirection.PgUp:
			{
				if (this.caret.line.line_no == 1 && this.owner.richtext)
				{
					this.owner.vscroll.Value = 0;
					Line line = this.GetLine(1);
					this.PositionCaret(line, 0);
				}
				int num3 = this.caret.line.Y + this.caret.line.height - 1 - this.viewport_y;
				int num4;
				LineTag lineTag = this.FindCursor((int)this.caret.line.widths[this.caret.pos], this.viewport_y - this.viewport_height, out num4);
				this.owner.vscroll.Value = Math.Min(lineTag.Line.Y, this.owner.vscroll.Maximum - this.viewport_height);
				this.PositionCaret((int)this.caret.line.widths[this.caret.pos], num3 + this.viewport_y);
				return;
			}
			case CaretDirection.PgDn:
			{
				if (this.caret.line.line_no == this.lines && this.owner.richtext)
				{
					this.owner.vscroll.Value = this.owner.vscroll.Maximum - this.viewport_height + 1;
					Line line2 = this.GetLine(this.lines);
					this.PositionCaret(line2, line2.TextLengthWithoutEnding());
				}
				int num5 = this.caret.line.Y - this.viewport_y;
				int num6;
				LineTag lineTag2 = this.FindCursor((int)this.caret.line.widths[this.caret.pos], this.viewport_y + this.viewport_height, out num6);
				this.owner.vscroll.Value = Math.Min(lineTag2.Line.Y, this.owner.vscroll.Maximum - this.viewport_height);
				this.PositionCaret((int)this.caret.line.widths[this.caret.pos], num5 + this.viewport_y);
				return;
			}
			case CaretDirection.CtrlPgUp:
				this.PositionCaret(0, this.viewport_y);
				this.DisplayCaret();
				return;
			case CaretDirection.CtrlPgDn:
			{
				int num7;
				LineTag lineTag3 = this.FindCursor(0, this.viewport_y + this.viewport_height, out num7);
				Line line3;
				if (lineTag3.Line.line_no > 1)
				{
					line3 = this.GetLine(lineTag3.Line.line_no - 1);
				}
				else
				{
					line3 = lineTag3.Line;
				}
				this.PositionCaret(line3, line3.Text.Length);
				this.DisplayCaret();
				return;
			}
			case CaretDirection.CtrlHome:
				this.caret.line = this.GetLine(1);
				this.caret.pos = 0;
				this.caret.tag = this.caret.line.tags;
				this.UpdateCaret();
				return;
			case CaretDirection.CtrlEnd:
				this.caret.line = this.GetLine(this.lines);
				this.caret.pos = this.caret.line.TextLengthWithoutEnding();
				this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				this.UpdateCaret();
				return;
			case CaretDirection.WordBack:
				if (this.caret.pos > 0)
				{
					this.caret.pos = this.caret.pos - 1;
					while (this.caret.pos > 0 && this.caret.line.text.get_Chars(this.caret.pos) == ' ')
					{
						this.caret.pos = this.caret.pos - 1;
					}
					while (this.caret.pos > 0 && this.caret.line.text.get_Chars(this.caret.pos) != ' ')
					{
						this.caret.pos = this.caret.pos - 1;
					}
					if (this.caret.line.text.ToString(this.caret.pos, 1) == " ")
					{
						if (this.caret.pos != 0)
						{
							this.caret.pos = this.caret.pos + 1;
						}
						else
						{
							this.caret.line = this.GetLine(this.caret.line.line_no - 1);
							this.caret.pos = this.caret.line.text.Length;
						}
					}
					this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				}
				else if (this.caret.line.line_no > 1)
				{
					this.caret.line = this.GetLine(this.caret.line.line_no - 1);
					this.caret.pos = this.caret.line.text.Length;
					this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				}
				this.UpdateCaret();
				return;
			case CaretDirection.WordForward:
			{
				int length = this.caret.line.text.Length;
				if (this.caret.pos < length)
				{
					while (this.caret.pos < length && this.caret.line.text.get_Chars(this.caret.pos) != ' ')
					{
						this.caret.pos = this.caret.pos + 1;
					}
					if (this.caret.pos < length)
					{
						while (this.caret.pos < length && this.caret.line.text.get_Chars(this.caret.pos) == ' ')
						{
							this.caret.pos = this.caret.pos + 1;
						}
					}
					this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
				}
				else if (this.caret.line.line_no < this.lines)
				{
					this.caret.line = this.GetLine(this.caret.line.line_no + 1);
					this.caret.pos = 0;
					this.caret.tag = this.caret.line.tags;
				}
				this.UpdateCaret();
				return;
			}
			case CaretDirection.SelectionStart:
				this.caret.line = this.selection_start.line;
				this.caret.pos = this.selection_start.pos;
				this.caret.tag = this.selection_start.tag;
				this.UpdateCaret();
				return;
			case CaretDirection.SelectionEnd:
				this.caret.line = this.selection_end.line;
				this.caret.pos = this.selection_end.pos;
				this.caret.tag = this.selection_end.tag;
				this.UpdateCaret();
				return;
			case CaretDirection.CharForwardNoWrap:
				flag = true;
				break;
			case CaretDirection.CharBackNoWrap:
				flag = true;
				goto IL_018F;
			default:
				return;
			}
			this.caret.pos = this.caret.pos + 1;
			if (this.caret.pos > this.caret.line.TextLengthWithoutEnding())
			{
				if (!flag)
				{
					if (this.caret.line.line_no < this.lines)
					{
						this.caret.line = this.GetLine(this.caret.line.line_no + 1);
						this.caret.pos = 0;
						this.caret.tag = this.caret.line.tags;
					}
					else
					{
						this.caret.pos = this.caret.pos - 1;
					}
				}
				else
				{
					this.caret.pos = this.caret.pos - 1;
				}
			}
			else if (this.caret.tag.Start - 1 + this.caret.tag.Length < this.caret.pos)
			{
				this.caret.tag = this.caret.tag.Next;
			}
			this.UpdateCaret();
			return;
			IL_018F:
			if (this.caret.pos > 0)
			{
				if ((this.caret.pos = this.caret.pos - 1) > 0 && this.caret.tag.Start > this.caret.pos)
				{
					this.caret.tag = this.caret.tag.Previous;
				}
			}
			else if (this.caret.line.line_no > 1 && !flag)
			{
				this.caret.line = this.GetLine(this.caret.line.line_no - 1);
				this.caret.pos = this.caret.line.TextLengthWithoutEnding();
				this.caret.tag = LineTag.FindTag(this.caret.line, this.caret.pos);
			}
			this.UpdateCaret();
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000CD45C File Offset: 0x000CB65C
		internal void DumpDoc()
		{
			Console.WriteLine("<doc lines='{0}'>", this.lines);
			for (int i = 1; i <= this.lines; i++)
			{
				Line line = this.GetLine(i);
				Console.WriteLine("<line no='{0}' ending='{1}'>", line.line_no, line.ending);
				for (LineTag lineTag = line.tags; lineTag != null; lineTag = lineTag.Next)
				{
					Console.Write("\t<tag type='{0}' span='{1}->{2}' font='{3}' color='{4}'>", new object[]
					{
						lineTag.GetType(),
						lineTag.Start,
						lineTag.Length,
						lineTag.Font,
						lineTag.Color
					});
					Console.Write(lineTag.Text());
					Console.WriteLine("</tag>");
				}
				Console.WriteLine("</line>");
			}
			Console.WriteLine("</doc>");
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000CD54C File Offset: 0x000CB74C
		internal void GetVisibleLineIndexes(Rectangle clip, out int start, out int end)
		{
			if (this.multiline)
			{
				start = this.GetLineByPixel(clip.Top + this.viewport_y - this.offset_y, false).line_no;
				end = this.GetLineByPixel(clip.Bottom + this.viewport_y - this.offset_y, false).line_no;
			}
			else
			{
				start = this.GetLineByPixel(clip.Left + this.viewport_x - this.offset_x, false).line_no;
				end = this.GetLineByPixel(clip.Right + this.viewport_x - this.offset_x, false).line_no;
			}
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000CD5F8 File Offset: 0x000CB7F8
		internal void Draw(Graphics g, Rectangle clip)
		{
			int num;
			int num2;
			this.GetVisibleLineIndexes(clip, out num, out num2);
			this.InvalidateLinks(clip);
			if (this.owner.actual_border_style == BorderStyle.FixedSingle)
			{
				ControlPaint.DrawBorder(g, this.owner.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
			}
			Line line = this.GetLine(num2 - 1);
			if (line != null && clip.Bottom == this.offset_y + line.Y + line.height - this.viewport_y)
			{
				num2--;
			}
			int i = num;
			if (!this.multiline && this.selection_visible && this.owner.ShowSelection)
			{
				g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorHighlight), (float)this.offset_x + this.selection_start.line.widths[this.selection_start.pos] + (float)this.selection_start.line.X - (float)this.viewport_x, (float)(this.offset_y + this.selection_start.line.Y), (float)this.selection_end.line.X + this.selection_end.line.widths[this.selection_end.pos] - ((float)this.selection_start.line.X + this.selection_start.line.widths[this.selection_start.pos]), (float)this.selection_start.line.height);
			}
			while (i <= num2)
			{
				line = this.GetLine(i);
				float num3 = (float)(line.Y - this.viewport_y + this.offset_y);
				LineTag lineTag = line.tags;
				StringBuilder stringBuilder;
				if (!this.calc_pass)
				{
					stringBuilder = line.text;
				}
				else
				{
					if (this.PasswordCache.Length < line.text.Length)
					{
						this.PasswordCache.Append(char.Parse(this.password_char), line.text.Length - this.PasswordCache.Length);
					}
					else if (this.PasswordCache.Length > line.text.Length)
					{
						this.PasswordCache.Remove(line.text.Length, this.PasswordCache.Length - line.text.Length);
					}
					stringBuilder = this.PasswordCache;
				}
				int num4 = stringBuilder.Length + 1;
				int num5 = stringBuilder.Length + 1;
				if (this.selection_visible && this.owner.ShowSelection && i >= this.selection_start.line.line_no && i <= this.selection_end.line.line_no)
				{
					if (i == this.selection_start.line.line_no)
					{
						num4 = this.selection_start.pos + 1;
					}
					else
					{
						num4 = 1;
					}
					if (i == this.selection_end.line.line_no)
					{
						num5 = this.selection_end.pos + 1;
					}
					else
					{
						num5 = stringBuilder.Length + 1;
					}
					if (num5 == num4)
					{
						num4 = stringBuilder.Length + 1;
						num5 = num4;
					}
					else if (this.multiline)
					{
						g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorHighlight), (float)this.offset_x + line.widths[num4 - 1] + (float)line.X - (float)this.viewport_x, num3, line.widths[num5 - 1] - line.widths[num4 - 1], (float)line.height);
					}
				}
				Color color = line.tags.ColorToDisplay;
				while (lineTag != null)
				{
					if (lineTag.Length == 0)
					{
						lineTag = lineTag.Next;
					}
					else if (lineTag.X + lineTag.Width < (float)(clip.Left - this.viewport_x - this.offset_x) && lineTag.X > (float)(clip.Right - this.viewport_x - this.offset_x))
					{
						lineTag = lineTag.Next;
					}
					else
					{
						if (lineTag.BackColor != Color.Empty)
						{
							g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(lineTag.BackColor), (float)this.offset_x + lineTag.X + (float)line.X - (float)this.viewport_x, num3 + (float)lineTag.Shift, lineTag.Width, (float)line.height);
						}
						Color color2 = lineTag.ColorToDisplay;
						if (!this.owner.Enabled)
						{
							Color color3 = lineTag.Color;
							Color colorWindowText = ThemeEngine.Current.ColorWindowText;
							if (color3.R == colorWindowText.R && color3.G == colorWindowText.G && color3.B == colorWindowText.B)
							{
								color2 = ThemeEngine.Current.ColorGrayText;
							}
						}
						int j = lineTag.Start;
						while (j < lineTag.Start + lineTag.Length)
						{
							int num6 = j;
							if (j >= num4 && j < num5)
							{
								color = ThemeEngine.Current.ColorHighlightText;
								j = Math.Min(lineTag.End, num5);
							}
							else if (j < num4)
							{
								color = color2;
								j = Math.Min(lineTag.End, num4);
							}
							else
							{
								color = color2;
								j = lineTag.End;
							}
							Rectangle rectangle;
							lineTag.Draw(g, color, (float)(this.offset_x + line.X - this.viewport_x), num3 + (float)lineTag.Shift, num6 - 1, Math.Min(lineTag.Start + lineTag.Length, j) - 1, stringBuilder.ToString(), out rectangle, lineTag.IsLink);
							if (lineTag.IsLink)
							{
								TextBoxBase.LinkRectangle linkRectangle = new TextBoxBase.LinkRectangle(rectangle);
								linkRectangle.LinkTag = lineTag;
								this.owner.list_links.Add(linkRectangle);
							}
						}
						lineTag = lineTag.Next;
					}
				}
				line.DrawEnding(g, num3);
				i++;
			}
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000CDC3C File Offset: 0x000CBE3C
		private int GetLineEnding(string line, int start, out LineEnding ending)
		{
			if (start >= line.Length)
			{
				ending = LineEnding.Wrap;
				return -1;
			}
			int num = line.IndexOf('\r', start);
			int num2 = line.IndexOf('\n', start);
			if (num != -1 && num2 != -1 && num2 < num)
			{
				ending = LineEnding.Rich;
				return num2;
			}
			if (num != -1)
			{
				if (num + 2 < line.Length && line.get_Chars(num + 1) == '\r' && line.get_Chars(num + 2) == '\n')
				{
					ending = LineEnding.Soft;
					return num;
				}
				if (num + 1 < line.Length && line.get_Chars(num + 1) == '\n')
				{
					ending = LineEnding.Hard;
					return num;
				}
				ending = LineEnding.Limp;
				return num;
			}
			else
			{
				if (num2 != -1)
				{
					ending = LineEnding.Rich;
					return num2;
				}
				ending = LineEnding.Wrap;
				return line.Length;
			}
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000CDD04 File Offset: 0x000CBF04
		private int GetLineEnding(string line, int start, out LineEnding ending, LineEnding type)
		{
			int num = start;
			int num2 = 0;
			do
			{
				num = this.GetLineEnding(line, num + num2, out ending);
				num2 = this.LineEndingLength(ending);
			}
			while ((ending & type) != ending && num != -1);
			return (num != -1) ? num : line.Length;
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000CDD50 File Offset: 0x000CBF50
		internal int LineEndingLength(LineEnding ending)
		{
			switch (ending)
			{
			case LineEnding.Limp:
				break;
			default:
				if (ending == LineEnding.Soft)
				{
					return 3;
				}
				if (ending != LineEnding.Rich)
				{
					return 0;
				}
				break;
			case LineEnding.Hard:
				return 2;
			}
			return 1;
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000CDD90 File Offset: 0x000CBF90
		internal string LineEndingToString(LineEnding ending)
		{
			switch (ending)
			{
			case LineEnding.Limp:
				return "\r";
			default:
				if (ending == LineEnding.Soft)
				{
					return "\r\r\n";
				}
				if (ending != LineEnding.Rich)
				{
					return string.Empty;
				}
				return "\n";
			case LineEnding.Hard:
				return "\r\n";
			}
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000CDDE4 File Offset: 0x000CBFE4
		internal LineEnding StringToLineEnding(string ending)
		{
			if (ending != null)
			{
				if (Document.<>f__switch$mapC == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(4);
					dictionary.Add("\r", 0);
					dictionary.Add("\r\n", 1);
					dictionary.Add("\r\r\n", 2);
					dictionary.Add("\n", 3);
					Document.<>f__switch$mapC = dictionary;
				}
				int num;
				if (Document.<>f__switch$mapC.TryGetValue(ending, ref num))
				{
					switch (num)
					{
					case 0:
						return LineEnding.Limp;
					case 1:
						return LineEnding.Hard;
					case 2:
						return LineEnding.Soft;
					case 3:
						return LineEnding.Rich;
					}
				}
			}
			return LineEnding.None;
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000CDE78 File Offset: 0x000CC078
		internal void Insert(Line line, int pos, bool update_caret, string s)
		{
			this.Insert(line, pos, update_caret, s, line.FindTag(pos));
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000CDE98 File Offset: 0x000CC098
		internal void Insert(Line line, int pos, bool update_caret, string s, LineTag tag)
		{
			int num = 1;
			this.SuspendRecalc();
			int line_no = line.line_no;
			int num2 = this.lines;
			int num3 = s.IndexOf('\0');
			if (num3 != -1)
			{
				s = s.Substring(0, num3);
			}
			LineEnding lineEnding;
			int num4 = this.GetLineEnding(s, 0, out lineEnding, (LineEnding)20);
			if (num4 == s.Length)
			{
				line.InsertString(pos, s, tag);
			}
			else
			{
				line.InsertString(pos, s.Substring(0, num4 + this.LineEndingLength(lineEnding)), tag);
				this.Split(line, pos + (num4 + this.LineEndingLength(lineEnding)));
				line.ending = lineEnding;
				num4 += this.LineEndingLength(lineEnding);
				Line line2 = this.GetLine(line.line_no + 1);
				for (;;)
				{
					int lineEnding2 = this.GetLineEnding(s, num4, out lineEnding, (LineEnding)20);
					if (lineEnding2 == s.Length)
					{
						break;
					}
					string text = s.Substring(num4, lineEnding2 - num4 + this.LineEndingLength(lineEnding));
					this.Add(line_no + num, text, line.alignment, tag.Font, tag.Color, lineEnding);
					Line line3 = this.GetLine(line_no + num);
					line3.ending = lineEnding;
					num++;
					num4 = lineEnding2 + this.LineEndingLength(lineEnding);
				}
				line2.InsertString(0, s.Substring(num4));
			}
			this.ResumeRecalc(false);
			this.CharCount += s.Length;
			this.UpdateView(line, this.lines - num2 + 1, pos);
			if (update_caret)
			{
				Line line4 = this.GetLine(line.line_no + this.lines - num2);
				this.PositionCaret(line4, line4.text.Length);
				this.DisplayCaret();
			}
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000CE04C File Offset: 0x000CC24C
		internal void InsertString(Line line, int pos, string s)
		{
			this.CharCount += s.Length;
			line.InsertString(pos, s);
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000CE074 File Offset: 0x000CC274
		internal void InsertCharAtCaret(char ch, bool move_caret)
		{
			this.caret.line.InsertString(this.caret.pos, ch.ToString(), this.caret.tag);
			this.CharCount++;
			this.undo.RecordTyping(this.caret.line, this.caret.pos, ch);
			this.UpdateView(this.caret.line, this.caret.pos);
			if (move_caret)
			{
				this.caret.pos = this.caret.pos + 1;
				this.UpdateCaret();
				this.SetSelectionToCaret(true);
			}
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000CE120 File Offset: 0x000CC320
		internal void InsertPicture(Line line, int pos, Picture picture)
		{
			int num = 1;
			line.text.Insert(pos, "I");
			PictureTag pictureTag = new PictureTag(line, pos + 1, picture);
			LineTag lineTag = LineTag.FindTag(line, pos);
			pictureTag.CopyFormattingFrom(lineTag);
			lineTag.Break(pos + 1);
			pictureTag.Previous = lineTag;
			pictureTag.Next = lineTag.Next;
			lineTag.Next = pictureTag;
			if (pictureTag.Next == null)
			{
				pictureTag.Next = new LineTag(line, pos + 1);
				pictureTag.Next.CopyFormattingFrom(lineTag);
				pictureTag.Next.Previous = pictureTag;
			}
			for (lineTag = pictureTag.Next; lineTag != null; lineTag = lineTag.Next)
			{
				lineTag.Start += num;
			}
			line.Grow(num);
			line.recalc = true;
			this.UpdateView(line, pos);
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000CE1F0 File Offset: 0x000CC3F0
		internal void DeleteMultiline(Line start_line, int pos, int length)
		{
			Document.Marker marker = default(Document.Marker);
			Document.Marker marker2 = default(Document.Marker);
			int num = this.LineTagToCharIndex(start_line, pos);
			marker.line = start_line;
			marker.pos = pos;
			marker.tag = LineTag.FindTag(start_line, pos);
			this.CharIndexToLineTag(num + length, out marker2.line, out marker2.tag, out marker2.pos);
			this.SuspendUpdate();
			if (marker.line == marker2.line)
			{
				this.DeleteChars(marker.line, pos, marker2.pos - pos);
			}
			else
			{
				this.DeleteChars(marker.line, marker.pos, marker.line.text.Length - marker.pos);
				this.DeleteChars(marker2.line, 0, marker2.pos);
				int num2 = marker.line.line_no + 1;
				if (num2 < marker2.line.line_no)
				{
					for (int i = marker2.line.line_no - 1; i >= num2; i--)
					{
						this.Delete(i);
					}
				}
				this.Combine(marker.line.line_no, num2);
			}
			this.ResumeUpdate(true);
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000CE330 File Offset: 0x000CC530
		public void DeleteChars(Line line, int pos, int count)
		{
			this.CharCount -= count;
			line.DeleteCharacters(pos, count);
			if (pos >= line.TextLengthWithoutEnding())
			{
				LineEnding ending = line.ending;
				this.GetLineEnding(line.text.ToString(), 0, out ending);
				if (ending != line.ending)
				{
					line.ending = ending;
					if (!this.multiline)
					{
						this.UpdateView(line, this.lines, pos);
						this.owner.Invalidate();
						return;
					}
				}
			}
			if (!this.multiline)
			{
				this.UpdateView(line, this.lines, pos);
				this.owner.Invalidate();
			}
			else
			{
				this.UpdateView(line, pos);
			}
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000CE3E4 File Offset: 0x000CC5E4
		public void DeleteChar(Line line, int pos, bool forward)
		{
			if ((pos == 0 && !forward) || (pos == line.text.Length && forward))
			{
				return;
			}
			this.undo.BeginUserAction("Delete");
			if (forward)
			{
				this.undo.RecordDeleteString(line, pos, line, pos + 1);
				this.DeleteChars(line, pos, 1);
			}
			else
			{
				this.undo.RecordDeleteString(line, pos - 1, line, pos);
				this.DeleteChars(line, pos - 1, 1);
			}
			this.undo.EndUserAction();
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000CE474 File Offset: 0x000CC674
		internal void Combine(int FirstLine, int SecondLine)
		{
			this.Combine(this.GetLine(FirstLine), this.GetLine(SecondLine));
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000CE48C File Offset: 0x000CC68C
		internal void Combine(Line first, Line second)
		{
			first.text.Length = first.text.Length - this.LineEndingLength(first.ending);
			LineTag lineTag = first.tags;
			first.ending = second.ending;
			while (lineTag.Next != null)
			{
				lineTag = lineTag.Next;
			}
			int num = lineTag.Start + lineTag.Length - 1;
			lineTag.Next = second.tags;
			lineTag.Next.Previous = lineTag;
			for (lineTag = lineTag.Next; lineTag != null; lineTag = lineTag.Next)
			{
				lineTag.Line = first;
				lineTag.Start += num;
			}
			first.text.Insert(first.text.Length, second.text.ToString());
			first.Grow(first.text.Length);
			second.tags = null;
			this.DecrementLines(first.line_no + 2);
			first.recalc = true;
			first.height = 0;
			first.Streamline(this.lines);
			if (this.caret.line == second)
			{
				this.caret.Combine(first, num);
			}
			if (this.selection_anchor.line == second)
			{
				this.selection_anchor.Combine(first, num);
			}
			if (this.selection_start.line == second)
			{
				this.selection_start.Combine(first, num);
			}
			if (this.selection_end.line == second)
			{
				this.selection_end.Combine(first, num);
			}
			this.Delete(second);
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000CE620 File Offset: 0x000CC820
		internal void Split(int LineNo, int pos)
		{
			Line line = this.GetLine(LineNo);
			LineTag lineTag = LineTag.FindTag(line, pos);
			this.Split(line, lineTag, pos);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000CE648 File Offset: 0x000CC848
		internal void Split(Line line, int pos)
		{
			LineTag lineTag = LineTag.FindTag(line, pos);
			this.Split(line, lineTag, pos);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000CE668 File Offset: 0x000CC868
		internal void Split(Line line, LineTag tag, int pos)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (this.caret.line == line && this.caret.pos >= pos)
			{
				flag = true;
			}
			if (this.selection_start.line == line && this.selection_start.pos > pos)
			{
				flag2 = true;
			}
			if (this.selection_end.line == line && this.selection_end.pos > pos)
			{
				flag3 = true;
			}
			Line line2;
			if (pos == line.text.Length)
			{
				this.Add(line.line_no + 1, string.Empty, line.alignment, tag.Font, tag.Color, line.ending);
				line2 = this.GetLine(line.line_no + 1);
				if (flag)
				{
					this.caret.line = line2;
					this.caret.tag = line2.tags;
					this.caret.pos = 0;
					if (!this.selection_visible)
					{
						this.SetSelectionToCaret(true);
					}
				}
				if (flag2)
				{
					this.selection_start.line = line2;
					this.selection_start.pos = 0;
					this.selection_start.tag = line2.tags;
				}
				if (flag3)
				{
					this.selection_end.line = line2;
					this.selection_end.pos = 0;
					this.selection_end.tag = line2.tags;
				}
				return;
			}
			this.Add(line.line_no + 1, line.text.ToString(pos, line.text.Length - pos), line.alignment, tag.Font, tag.Color, line.ending);
			line2 = this.GetLine(line.line_no + 1);
			line.recalc = true;
			line2.recalc = true;
			if (tag.Next != null && tag.Next.Start - 1 == pos)
			{
				tag = tag.Next;
			}
			if (tag.Start - 1 == pos)
			{
				if (tag == line.tags)
				{
					LineTag lineTag = new LineTag(line, 1);
					lineTag.CopyFormattingFrom(tag);
					line.tags = lineTag;
				}
				if (tag.Previous != null)
				{
					tag.Previous.Next = null;
				}
				line2.tags = tag;
				tag.Previous = null;
				tag.Line = line2;
				int num = tag.Start - 1;
				for (LineTag lineTag = tag; lineTag != null; lineTag = lineTag.Next)
				{
					lineTag.Start -= num;
					lineTag.Line = line2;
				}
			}
			else
			{
				LineTag lineTag = new LineTag(line2, 1);
				lineTag.Next = tag.Next;
				lineTag.CopyFormattingFrom(tag);
				line2.tags = lineTag;
				if (lineTag.Next != null)
				{
					lineTag.Next.Previous = lineTag;
				}
				tag.Next = null;
				for (lineTag = lineTag.Next; lineTag != null; lineTag = lineTag.Next)
				{
					lineTag.Start -= pos;
					lineTag.Line = line2;
				}
			}
			if (flag)
			{
				this.caret.line = line2;
				this.caret.pos = this.caret.pos - pos;
				this.caret.tag = this.caret.line.FindTag(this.caret.pos);
				if (!this.selection_visible)
				{
					this.SetSelectionToCaret(true);
				}
			}
			if (flag2)
			{
				this.selection_start.line = line2;
				this.selection_start.pos = this.selection_start.pos - pos;
				if (this.selection_start.Equals(this.selection_end))
				{
					this.selection_start.tag = line2.FindTag(this.selection_start.pos);
				}
				else
				{
					this.selection_start.tag = line2.FindTag(this.selection_start.pos + 1);
				}
			}
			if (flag3)
			{
				this.selection_end.line = line2;
				this.selection_end.pos = this.selection_end.pos - pos;
				this.selection_end.tag = line2.FindTag(this.selection_end.pos);
			}
			this.CharCount -= line.text.Length - pos;
			line.text.Remove(pos, line.text.Length - pos);
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000CEAC0 File Offset: 0x000CCCC0
		internal void Add(int LineNo, string Text, Font font, Color color, LineEnding ending)
		{
			this.Add(LineNo, Text, this.alignment, font, color, ending);
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000CEAD8 File Offset: 0x000CCCD8
		internal void Add(int LineNo, string Text, HorizontalAlignment align, Font font, Color color, LineEnding ending)
		{
			this.CharCount += Text.Length;
			if (LineNo >= 1 && Text != null)
			{
				Line line = new Line(this, LineNo, Text, align, font, color, ending);
				Line line2 = this.document;
				while (line2 != this.sentinel)
				{
					line.parent = line2;
					int line_no = line2.line_no;
					if (LineNo > line_no)
					{
						line2 = line2.right;
					}
					else if (LineNo < line_no)
					{
						line2 = line2.left;
					}
					else
					{
						this.IncrementLines(line2.line_no);
						line2 = line2.left;
					}
				}
				line.left = this.sentinel;
				line.right = this.sentinel;
				if (line.parent != null)
				{
					if (LineNo > line.parent.line_no)
					{
						line.parent.right = line;
					}
					else
					{
						line.parent.left = line;
					}
				}
				else
				{
					this.document = line;
				}
				this.RebalanceAfterAdd(line);
				this.lines++;
				return;
			}
			if (LineNo < 1)
			{
				throw new ArgumentNullException("LineNo", "Line numbers must be positive");
			}
			throw new ArgumentNullException("Text", "Cannot insert NULL line");
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000CEC10 File Offset: 0x000CCE10
		internal virtual void Clear()
		{
			this.lines = 0;
			this.CharCount = 0;
			this.document = this.sentinel;
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000CEC2C File Offset: 0x000CCE2C
		public virtual object Clone()
		{
			return new Document(null)
			{
				lines = this.lines,
				document = (Line)this.document.Clone()
			};
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000CEC64 File Offset: 0x000CCE64
		private void Delete(int LineNo)
		{
			if (LineNo > this.lines)
			{
				return;
			}
			Line line = this.GetLine(LineNo);
			this.CharCount -= line.text.Length;
			this.DecrementLines(LineNo + 1);
			this.Delete(line);
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000CECB0 File Offset: 0x000CCEB0
		private void Delete(Line line1)
		{
			Line line2;
			if (line1.left == this.sentinel || line1.right == this.sentinel)
			{
				line2 = line1;
			}
			else
			{
				line2 = line1.right;
				while (line2.left != this.sentinel)
				{
					line2 = line2.left;
				}
			}
			Line line3;
			if (line2.left != this.sentinel)
			{
				line3 = line2.left;
			}
			else
			{
				line3 = line2.right;
			}
			line3.parent = line2.parent;
			if (line2.parent != null)
			{
				if (line2 == line2.parent.left)
				{
					line2.parent.left = line3;
				}
				else
				{
					line2.parent.right = line3;
				}
			}
			else
			{
				this.document = line3;
			}
			if (line2 != line1)
			{
				if (this.selection_start.line == line2)
				{
					this.selection_start.line = line1;
				}
				if (this.selection_end.line == line2)
				{
					this.selection_end.line = line1;
				}
				if (this.selection_anchor.line == line2)
				{
					this.selection_anchor.line = line1;
				}
				if (this.caret.line == line2)
				{
					this.caret.line = line1;
				}
				line1.alignment = line2.alignment;
				line1.ascent = line2.ascent;
				line1.hanging_indent = line2.hanging_indent;
				line1.height = line2.height;
				line1.indent = line2.indent;
				line1.line_no = line2.line_no;
				line1.recalc = line2.recalc;
				line1.right_indent = line2.right_indent;
				line1.ending = line2.ending;
				line1.space = line2.space;
				line1.tags = line2.tags;
				line1.text = line2.text;
				line1.widths = line2.widths;
				line1.offset = line2.offset;
				for (LineTag lineTag = line1.tags; lineTag != null; lineTag = lineTag.Next)
				{
					lineTag.Line = line1;
				}
			}
			if (line2.color == LineColor.Black)
			{
				this.RebalanceAfterDelete(line3);
			}
			this.lines--;
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000CEEE4 File Offset: 0x000CD0E4
		internal void InvalidateLinesAfter(Line start)
		{
			this.owner.Invalidate(new Rectangle(0, start.Y - this.viewport_y, this.viewport_width, this.viewport_height - start.Y));
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000CEF24 File Offset: 0x000CD124
		internal void Invalidate(Line start, int start_pos, Line end, int end_pos)
		{
			if (start == end && start_pos == end_pos)
			{
				return;
			}
			if (end_pos == -1)
			{
				end_pos = end.text.Length;
			}
			Line line;
			int num;
			Line line2;
			int num2;
			if (start.line_no < end.line_no)
			{
				line = start;
				num = start_pos;
				line2 = end;
				num2 = end_pos;
			}
			else
			{
				if (start.line_no <= end.line_no)
				{
					if (start_pos < end_pos)
					{
						line = start;
						num = start_pos;
						num2 = end_pos;
					}
					else
					{
						line = end;
						num = end_pos;
						num2 = start_pos;
					}
					int num3 = (int)line.widths[num2];
					if (num2 == line.text.Length + 1)
					{
						num3 = this.viewport_width;
					}
					this.owner.Invalidate(new Rectangle(this.offset_x + (int)line.widths[num] + line.X - this.viewport_x, this.offset_y + line.Y - this.viewport_y, num3 - (int)line.widths[num] + 1, line.height));
					return;
				}
				line = end;
				num = end_pos;
				line2 = start;
				num2 = start_pos;
			}
			this.owner.Invalidate(new Rectangle(this.offset_x + (int)line.widths[num] + line.X - this.viewport_x, this.offset_y + line.Y - this.viewport_y, this.viewport_width, line.height));
			if (line.line_no + 1 < line2.line_no)
			{
				int y = this.GetLine(line.line_no + 1).Y;
				this.owner.Invalidate(new Rectangle(this.offset_x, this.offset_y + y - this.viewport_y, this.viewport_width, line2.Y - y));
			}
			this.owner.Invalidate(new Rectangle(this.offset_x + (int)line2.widths[0] + line2.X - this.viewport_x, this.offset_y + line2.Y - this.viewport_y, (int)line2.widths[num2] + 1, line2.height));
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000CF130 File Offset: 0x000CD330
		internal void ExpandSelection(CaretSelection mode, bool to_caret)
		{
			if (to_caret)
			{
				switch (mode)
				{
				case CaretSelection.Position:
					this.SetSelectionToCaret(false);
					return;
				case CaretSelection.Word:
				{
					int num = this.FindWordSeparator(this.caret.line, this.caret.pos, false);
					int num2 = this.FindWordSeparator(this.caret.line, this.caret.pos, true);
					if (this.caret > this.selection_prev)
					{
						this.Invalidate(this.selection_prev.line, this.selection_prev.pos, this.caret.line, num2);
					}
					else
					{
						this.Invalidate(this.selection_prev.line, this.selection_prev.pos, this.caret.line, num);
					}
					if (this.caret < this.selection_anchor)
					{
						this.selection_start.line = this.caret.line;
						this.selection_start.tag = this.caret.line.FindTag(num + 1);
						this.selection_start.pos = num;
						this.selection_end.line = this.selection_anchor.line;
						this.selection_end.tag = this.selection_anchor.tag;
						this.selection_end.pos = this.selection_anchor.pos;
						this.selection_prev.line = this.caret.line;
						this.selection_prev.tag = this.caret.tag;
						this.selection_prev.pos = num;
						this.selection_end_anchor = true;
					}
					else
					{
						this.selection_start.line = this.selection_anchor.line;
						this.selection_start.pos = this.selection_anchor.height;
						this.selection_start.tag = this.selection_anchor.line.FindTag(this.selection_anchor.height + 1);
						this.selection_end.line = this.caret.line;
						this.selection_end.tag = this.caret.line.FindTag(num2);
						this.selection_end.pos = num2;
						this.selection_prev.line = this.caret.line;
						this.selection_prev.tag = this.caret.tag;
						this.selection_prev.pos = num2;
						this.selection_end_anchor = false;
					}
					break;
				}
				case CaretSelection.Line:
					if (this.caret > this.selection_prev)
					{
						this.Invalidate(this.selection_prev.line, 0, this.caret.line, this.caret.line.text.Length);
					}
					else
					{
						this.Invalidate(this.selection_prev.line, this.selection_prev.line.text.Length, this.caret.line, 0);
					}
					if (this.caret.line.line_no <= this.selection_anchor.line.line_no)
					{
						this.selection_start.line = this.caret.line;
						this.selection_start.tag = this.caret.line.tags;
						this.selection_start.pos = 0;
						this.selection_end.line = this.selection_anchor.line;
						this.selection_end.tag = this.selection_anchor.tag;
						this.selection_end.pos = this.selection_anchor.pos;
						this.selection_end_anchor = true;
					}
					else
					{
						this.selection_start.line = this.selection_anchor.line;
						this.selection_start.pos = this.selection_anchor.height;
						this.selection_start.tag = this.selection_anchor.line.FindTag(this.selection_anchor.height + 1);
						this.selection_end.line = this.caret.line;
						this.selection_end.tag = this.caret.line.tags;
						this.selection_end.pos = this.caret.line.text.Length;
						this.selection_end_anchor = false;
					}
					this.selection_prev.line = this.caret.line;
					this.selection_prev.tag = this.caret.tag;
					this.selection_prev.pos = this.caret.pos;
					break;
				}
			}
			else if (mode != CaretSelection.Word)
			{
				if (mode == CaretSelection.Line)
				{
					this.Invalidate(this.caret.line, 0, this.caret.line, this.caret.line.text.Length);
					this.selection_start.line = this.caret.line;
					this.selection_start.tag = this.caret.line.tags;
					this.selection_start.pos = 0;
					this.selection_end.line = this.caret.line;
					this.selection_end.pos = this.caret.line.text.Length;
					this.selection_end.tag = this.caret.line.FindTag(this.selection_end.pos);
					this.selection_anchor.line = this.selection_end.line;
					this.selection_anchor.tag = this.selection_end.tag;
					this.selection_anchor.pos = this.selection_end.pos;
					this.selection_anchor.height = 0;
					this.selection_prev.line = this.caret.line;
					this.selection_prev.tag = this.caret.tag;
					this.selection_prev.pos = this.caret.pos;
					this.selection_end_anchor = true;
				}
			}
			else
			{
				int num3 = this.FindWordSeparator(this.caret.line, this.caret.pos, false);
				int num4 = this.FindWordSeparator(this.caret.line, this.caret.pos, true);
				this.Invalidate(this.selection_start.line, num3, this.caret.line, num4);
				this.selection_start.line = this.caret.line;
				this.selection_start.tag = this.caret.line.FindTag(num3 + 1);
				this.selection_start.pos = num3;
				this.selection_end.line = this.caret.line;
				this.selection_end.tag = this.caret.line.FindTag(num4);
				this.selection_end.pos = num4;
				this.selection_anchor.line = this.selection_end.line;
				this.selection_anchor.tag = this.selection_end.tag;
				this.selection_anchor.pos = this.selection_end.pos;
				this.selection_anchor.height = num3;
				this.selection_prev.line = this.caret.line;
				this.selection_prev.tag = this.caret.tag;
				this.selection_prev.pos = this.caret.pos;
				this.selection_end_anchor = true;
			}
			this.SetSelectionVisible(!(this.selection_start == this.selection_end));
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000CF8F4 File Offset: 0x000CDAF4
		internal void SetSelectionToCaret(bool start)
		{
			if (start)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
				this.selection_start.line = this.caret.line;
				this.selection_start.tag = this.caret.tag;
				this.selection_start.pos = this.caret.pos;
				this.selection_end.line = this.caret.line;
				this.selection_end.tag = this.caret.tag;
				this.selection_end.pos = this.caret.pos;
				this.selection_anchor.line = this.caret.line;
				this.selection_anchor.tag = this.caret.tag;
				this.selection_anchor.pos = this.caret.pos;
			}
			else
			{
				if (this.selection_end_anchor)
				{
					if (this.selection_start != this.caret)
					{
						this.Invalidate(this.selection_start.line, this.selection_start.pos, this.caret.line, this.caret.pos);
					}
				}
				else if (this.selection_end != this.caret)
				{
					this.Invalidate(this.selection_end.line, this.selection_end.pos, this.caret.line, this.caret.pos);
				}
				if (this.caret < this.selection_anchor)
				{
					this.selection_start.line = this.caret.line;
					this.selection_start.tag = this.caret.tag;
					this.selection_start.pos = this.caret.pos;
					this.selection_end.line = this.selection_anchor.line;
					this.selection_end.tag = this.selection_anchor.tag;
					this.selection_end.pos = this.selection_anchor.pos;
					this.selection_end_anchor = true;
				}
				else
				{
					this.selection_start.line = this.selection_anchor.line;
					this.selection_start.tag = this.selection_anchor.tag;
					this.selection_start.pos = this.selection_anchor.pos;
					this.selection_end.line = this.caret.line;
					this.selection_end.tag = this.caret.tag;
					this.selection_end.pos = this.caret.pos;
					this.selection_end_anchor = false;
				}
			}
			this.SetSelectionVisible(!(this.selection_start == this.selection_end));
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000CFBF0 File Offset: 0x000CDDF0
		internal void SetSelection(Line start, int start_pos, Line end, int end_pos)
		{
			if (this.selection_visible)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
			}
			if (end.line_no < start.line_no || (end == start && end_pos <= start_pos))
			{
				this.selection_start.line = end;
				this.selection_start.tag = LineTag.FindTag(end, end_pos);
				this.selection_start.pos = end_pos;
				this.selection_end.line = start;
				this.selection_end.tag = LineTag.FindTag(start, start_pos);
				this.selection_end.pos = start_pos;
				this.selection_end_anchor = true;
			}
			else
			{
				this.selection_start.line = start;
				this.selection_start.tag = LineTag.FindTag(start, start_pos);
				this.selection_start.pos = start_pos;
				this.selection_end.line = end;
				this.selection_end.tag = LineTag.FindTag(end, end_pos);
				this.selection_end.pos = end_pos;
				this.selection_end_anchor = false;
			}
			this.selection_anchor.line = start;
			this.selection_anchor.tag = this.selection_start.tag;
			this.selection_anchor.pos = start_pos;
			if ((start == end && start_pos == end_pos) || start == null || end == null)
			{
				this.SetSelectionVisible(false);
			}
			else
			{
				this.SetSelectionVisible(true);
				this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
			}
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000CFDA8 File Offset: 0x000CDFA8
		internal void SetSelectionStart(Line start, int start_pos, bool invalidate)
		{
			if (invalidate)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, start, start_pos);
			}
			this.selection_start.line = start;
			this.selection_start.pos = start_pos;
			this.selection_start.tag = LineTag.FindTag(start, start_pos);
			this.selection_anchor.line = start;
			this.selection_anchor.pos = start_pos;
			this.selection_anchor.tag = this.selection_start.tag;
			this.selection_end_anchor = false;
			if (this.selection_end.line != this.selection_start.line || this.selection_end.pos != this.selection_start.pos)
			{
				this.SetSelectionVisible(true);
			}
			else
			{
				this.SetSelectionVisible(false);
			}
			if (invalidate)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
			}
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000CFEBC File Offset: 0x000CE0BC
		internal void SetSelectionStart(int character_index, bool invalidate)
		{
			if (character_index < 0)
			{
				return;
			}
			Line line;
			LineTag lineTag;
			int num;
			this.CharIndexToLineTag(character_index, out line, out lineTag, out num);
			this.SetSelectionStart(line, num, invalidate);
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000CFEE8 File Offset: 0x000CE0E8
		internal void SetSelectionEnd(Line end, int end_pos, bool invalidate)
		{
			if (end == this.selection_end.line && end_pos == this.selection_start.pos)
			{
				this.selection_anchor.line = this.selection_start.line;
				this.selection_anchor.tag = this.selection_start.tag;
				this.selection_anchor.pos = this.selection_start.pos;
				this.selection_end.line = this.selection_start.line;
				this.selection_end.tag = this.selection_start.tag;
				this.selection_end.pos = this.selection_start.pos;
				this.selection_end_anchor = false;
			}
			else if (end.line_no < this.selection_anchor.line.line_no || (end == this.selection_anchor.line && end_pos <= this.selection_anchor.pos))
			{
				this.selection_start.line = end;
				this.selection_start.tag = LineTag.FindTag(end, end_pos);
				this.selection_start.pos = end_pos;
				this.selection_end.line = this.selection_anchor.line;
				this.selection_end.tag = this.selection_anchor.tag;
				this.selection_end.pos = this.selection_anchor.pos;
				this.selection_end_anchor = true;
			}
			else
			{
				this.selection_start.line = this.selection_anchor.line;
				this.selection_start.tag = this.selection_anchor.tag;
				this.selection_start.pos = this.selection_anchor.pos;
				this.selection_end.line = end;
				this.selection_end.tag = LineTag.FindTag(end, end_pos);
				this.selection_end.pos = end_pos;
				this.selection_end_anchor = false;
			}
			if (this.selection_end.line != this.selection_start.line || this.selection_end.pos != this.selection_start.pos)
			{
				this.SetSelectionVisible(true);
				if (invalidate)
				{
					this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
				}
			}
			else
			{
				this.SetSelectionVisible(false);
			}
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000D0150 File Offset: 0x000CE350
		internal void SetSelectionEnd(int character_index, bool invalidate)
		{
			if (character_index < 0)
			{
				return;
			}
			Line line;
			LineTag lineTag;
			int num;
			this.CharIndexToLineTag(character_index, out line, out lineTag, out num);
			this.SetSelectionEnd(line, num, invalidate);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000D017C File Offset: 0x000CE37C
		internal void SetSelection(Line start, int start_pos)
		{
			if (this.selection_visible)
			{
				this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
			}
			this.selection_start.line = start;
			this.selection_start.pos = start_pos;
			this.selection_start.tag = LineTag.FindTag(start, start_pos);
			this.selection_end.line = start;
			this.selection_end.tag = this.selection_start.tag;
			this.selection_end.pos = start_pos;
			this.selection_anchor.line = start;
			this.selection_anchor.tag = this.selection_start.tag;
			this.selection_anchor.pos = start_pos;
			this.selection_end_anchor = false;
			this.SetSelectionVisible(false);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000D025C File Offset: 0x000CE45C
		internal void InvalidateSelectionArea()
		{
			this.Invalidate(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000D029C File Offset: 0x000CE49C
		internal string GetSelection()
		{
			if (this.selection_start.pos == this.selection_end.pos && this.selection_start.line == this.selection_end.line)
			{
				return string.Empty;
			}
			if (this.selection_start.line == this.selection_end.line)
			{
				return this.selection_start.line.text.ToString(this.selection_start.pos, this.selection_end.pos - this.selection_start.pos);
			}
			StringBuilder stringBuilder = new StringBuilder();
			int line_no = this.selection_start.line.line_no;
			int line_no2 = this.selection_end.line.line_no;
			stringBuilder.Append(this.selection_start.line.text.ToString(this.selection_start.pos, this.selection_start.line.text.Length - this.selection_start.pos));
			if (line_no + 1 < line_no2)
			{
				for (int i = line_no + 1; i < line_no2; i++)
				{
					stringBuilder.Append(this.GetLine(i).text.ToString());
				}
			}
			stringBuilder.Append(this.selection_end.line.text.ToString(0, this.selection_end.pos));
			return stringBuilder.ToString();
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000D040C File Offset: 0x000CE60C
		internal void ReplaceSelection(string s, bool select_new)
		{
			int num = this.LineTagToCharIndex(this.selection_start.line, this.selection_start.pos);
			this.SuspendRecalc();
			if (this.selection_start.pos != this.selection_end.pos || this.selection_start.line != this.selection_end.line)
			{
				if (this.selection_start.line == this.selection_end.line)
				{
					this.undo.RecordDeleteString(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
					this.DeleteChars(this.selection_start.line, this.selection_start.pos, this.selection_end.pos - this.selection_start.pos);
					this.selection_start.tag = this.selection_start.line.FindTag(this.selection_start.pos + 1);
				}
				else
				{
					int num2 = this.selection_start.line.line_no;
					int line_no = this.selection_end.line.line_no;
					this.undo.RecordDeleteString(this.selection_start.line, this.selection_start.pos, this.selection_end.line, this.selection_end.pos);
					this.InvalidateLinesAfter(this.selection_start.line);
					this.DeleteChars(this.selection_start.line, this.selection_start.pos, this.selection_start.line.text.Length - this.selection_start.pos);
					this.selection_start.line.recalc = true;
					this.DeleteChars(this.selection_end.line, 0, this.selection_end.pos);
					num2++;
					if (num2 < line_no)
					{
						for (int i = line_no - 1; i >= num2; i--)
						{
							this.Delete(i);
						}
					}
					this.Combine(this.selection_start.line.line_no, num2);
				}
			}
			this.Insert(this.selection_start.line, this.selection_start.pos, false, s);
			this.undo.RecordInsertString(this.selection_start.line, this.selection_start.pos, s);
			this.ResumeRecalc(false);
			Line line = this.selection_start.line;
			int pos = this.selection_start.pos;
			if (!select_new)
			{
				this.CharIndexToLineTag(num + s.Length, out this.selection_start.line, out this.selection_start.tag, out this.selection_start.pos);
				this.selection_end.line = this.selection_start.line;
				this.selection_end.pos = this.selection_start.pos;
				this.selection_end.tag = this.selection_start.tag;
				this.selection_anchor.line = this.selection_start.line;
				this.selection_anchor.pos = this.selection_start.pos;
				this.selection_anchor.tag = this.selection_start.tag;
				this.SetSelectionVisible(false);
			}
			else
			{
				this.CharIndexToLineTag(num, out this.selection_start.line, out this.selection_start.tag, out this.selection_start.pos);
				this.CharIndexToLineTag(num + s.Length, out this.selection_end.line, out this.selection_end.tag, out this.selection_end.pos);
				this.selection_anchor.line = this.selection_start.line;
				this.selection_anchor.pos = this.selection_start.pos;
				this.selection_anchor.tag = this.selection_start.tag;
				this.SetSelectionVisible(true);
			}
			this.PositionCaret(this.selection_start.line, this.selection_start.pos);
			this.UpdateView(line, this.selection_end.line.line_no - line.line_no, pos);
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000D083C File Offset: 0x000CEA3C
		internal void CharIndexToLineTag(int index, out Line line_out, out LineTag tag_out, out int pos)
		{
			int num = 0;
			LineTag lineTag;
			for (int i = 1; i <= this.lines; i++)
			{
				Line line = this.GetLine(i);
				int num2 = num;
				num += line.text.Length;
				if (index <= num)
				{
					lineTag = line.tags;
					while (lineTag != null)
					{
						if (index < num2 + lineTag.Start + lineTag.Length - 1)
						{
							line_out = line;
							tag_out = LineTag.GetFinalTag(lineTag);
							pos = index - num2;
							return;
						}
						if (lineTag.Next == null)
						{
							Line line2 = this.GetLine(line.line_no + 1);
							if (line2 != null)
							{
								line_out = line2;
								tag_out = LineTag.GetFinalTag(line2.tags);
								pos = 0;
								return;
							}
							line_out = line;
							tag_out = LineTag.GetFinalTag(lineTag);
							pos = line_out.text.Length;
							return;
						}
						else
						{
							lineTag = lineTag.Next;
						}
					}
				}
			}
			line_out = this.GetLine(this.lines);
			lineTag = line_out.tags;
			while (lineTag.Next != null)
			{
				lineTag = lineTag.Next;
			}
			tag_out = lineTag;
			pos = line_out.text.Length;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000D095C File Offset: 0x000CEB5C
		internal int LineTagToCharIndex(Line line, int pos)
		{
			int num = 0;
			for (int i = 1; i < line.line_no; i++)
			{
				num += this.GetLine(i).text.Length;
			}
			return num + pos;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000D099C File Offset: 0x000CEB9C
		internal int SelectionLength()
		{
			if (this.selection_start.pos == this.selection_end.pos && this.selection_start.line == this.selection_end.line)
			{
				return 0;
			}
			if (this.selection_start.line == this.selection_end.line)
			{
				return this.selection_end.pos - this.selection_start.pos;
			}
			int num = this.selection_start.line.text.Length - this.selection_start.pos + this.selection_end.pos + this.crlf_size;
			int num2 = this.selection_start.line.line_no + 1;
			int line_no = this.selection_end.line.line_no;
			if (num2 < line_no)
			{
				for (int i = num2; i < line_no; i++)
				{
					Line line = this.GetLine(i);
					num += line.text.Length + this.LineEndingLength(line.ending);
				}
			}
			return num;
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000D0AB0 File Offset: 0x000CECB0
		internal Line GetLine(int LineNo)
		{
			Line line = this.document;
			while (line != this.sentinel)
			{
				if (LineNo == line.line_no)
				{
					return line;
				}
				if (LineNo < line.line_no)
				{
					line = line.left;
				}
				else
				{
					line = line.right;
				}
			}
			return null;
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000D0B04 File Offset: 0x000CED04
		internal LineTag PreviousTag(LineTag tag)
		{
			if (tag.Previous != null)
			{
				return tag.Previous;
			}
			if (tag.Line.line_no == 1)
			{
				return null;
			}
			Line line = this.GetLine(tag.Line.line_no - 1);
			if (line != null)
			{
				LineTag lineTag = line.tags;
				while (lineTag.Next != null)
				{
					lineTag = lineTag.Next;
				}
				return lineTag;
			}
			return null;
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000D0B74 File Offset: 0x000CED74
		internal LineTag NextTag(LineTag tag)
		{
			if (tag.Next != null)
			{
				return tag.Next;
			}
			Line line = this.GetLine(tag.Line.line_no + 1);
			if (line != null)
			{
				return line.tags;
			}
			return null;
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000D0BB8 File Offset: 0x000CEDB8
		internal Line ParagraphStart(Line line)
		{
			Line line2 = line;
			while (line.line_no > 1)
			{
				line = line2;
				line2 = this.GetLine(line.line_no - 1);
				if (line2.ending != LineEnding.Wrap)
				{
					return line;
				}
			}
			return line;
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000D0BF8 File Offset: 0x000CEDF8
		internal Line ParagraphEnd(Line line)
		{
			while (line.ending == LineEnding.Wrap)
			{
				Line line2 = this.GetLine(line.line_no + 1);
				if (line2 == null || line2.ending != LineEnding.Wrap)
				{
					break;
				}
				line = line2;
			}
			return line;
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000D0C40 File Offset: 0x000CEE40
		internal Line GetLineByPixel(int offset, bool exact)
		{
			Line line = this.document;
			Line line2 = null;
			if (this.multiline)
			{
				while (line != this.sentinel)
				{
					line2 = line;
					if (offset >= line.Y && offset < line.Y + line.height)
					{
						return line;
					}
					if (offset < line.Y)
					{
						line = line.left;
					}
					else
					{
						line = line.right;
					}
				}
			}
			else
			{
				while (line != this.sentinel)
				{
					line2 = line;
					if (offset >= line.X && offset < line.X + line.Width)
					{
						return line;
					}
					if (offset < line.X)
					{
						line = line.left;
					}
					else
					{
						line = line.right;
					}
				}
			}
			if (exact)
			{
				return null;
			}
			return line2;
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000D0D18 File Offset: 0x000CEF18
		internal LineTag FindCursor(int x, int y, out int index)
		{
			x -= this.offset_x;
			y -= this.offset_y;
			Line lineByPixel = this.GetLineByPixel((!this.multiline) ? x : y, false);
			LineTag tag = lineByPixel.GetTag(x);
			if (tag.Length == 0 && tag.Start == 1)
			{
				index = 0;
			}
			else
			{
				index = tag.GetCharIndex(x - lineByPixel.align_shift);
			}
			return tag;
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000D0D8C File Offset: 0x000CEF8C
		internal void FormatText(Line start_line, int start_pos, Line end_line, int end_pos, Font font, Color color, Color back_color, FormatSpecified specified)
		{
			if (start_line != end_line)
			{
				LineTag.FormatText(start_line, start_pos, start_line.text.Length - start_pos + 1, font, color, back_color, specified);
				LineTag.FormatText(end_line, 1, end_pos, font, color, back_color, specified);
				for (int i = start_line.line_no + 1; i < end_line.line_no; i++)
				{
					Line line = this.GetLine(i);
					LineTag.FormatText(line, 1, line.text.Length, font, color, back_color, specified);
				}
			}
			else
			{
				LineTag.FormatText(start_line, start_pos, end_pos - start_pos, font, color, back_color, specified);
				if (end_pos - start_pos == 0 && this.CaretTag.Length != 0)
				{
					this.CaretTag = this.CaretTag.Next;
				}
			}
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000D0E58 File Offset: 0x000CF058
		internal void RecalculateAlignments()
		{
			for (int i = 1; i <= this.lines; i++)
			{
				Line line = this.GetLine(i);
				if (line != null)
				{
					switch (line.alignment)
					{
					case HorizontalAlignment.Left:
						line.align_shift = 0;
						break;
					case HorizontalAlignment.Right:
						line.align_shift = this.viewport_width - (int)line.widths[line.text.Length] - this.right_margin;
						break;
					case HorizontalAlignment.Center:
						line.align_shift = (this.viewport_width - (int)line.widths[line.text.Length]) / 2;
						break;
					}
				}
			}
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000D0F08 File Offset: 0x000CF108
		internal bool RecalculateDocument(Graphics g)
		{
			return this.RecalculateDocument(g, 1, this.lines, false);
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000D0F1C File Offset: 0x000CF11C
		internal bool RecalculateDocument(Graphics g, int start)
		{
			return this.RecalculateDocument(g, start, this.lines, false);
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000D0F30 File Offset: 0x000CF130
		internal bool RecalculateDocument(Graphics g, int start, int end)
		{
			return this.RecalculateDocument(g, start, end, false);
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000D0F3C File Offset: 0x000CF13C
		internal bool RecalculateDocument(Graphics g, int start, int end, bool optimize)
		{
			if (this.recalc_suspended > 0)
			{
				this.recalc_pending = true;
				this.recalc_start = Math.Min(this.recalc_start, start);
				this.recalc_end = Math.Max(this.recalc_end, end);
				this.recalc_optimize = optimize;
				return false;
			}
			start = Math.Max(start, 1);
			end = Math.Min(end, this.lines);
			int num = this.GetLine(start).offset;
			int i = start;
			int num2 = 0;
			int num3 = this.lines;
			bool flag = !optimize;
			Line line;
			while (i <= end + this.lines - num3)
			{
				line = this.GetLine(i++);
				line.offset = num;
				if (!this.calc_pass)
				{
					if (!optimize)
					{
						line.RecalculateLine(g, this);
					}
					else if (line.recalc && line.RecalculateLine(g, this))
					{
						flag = true;
						end = this.lines;
						num3 = this.lines;
					}
				}
				else if (!optimize)
				{
					line.RecalculatePasswordLine(g, this);
				}
				else if (line.recalc && line.RecalculatePasswordLine(g, this))
				{
					flag = true;
					end = this.lines;
					num3 = this.lines;
				}
				if (line.widths[line.text.Length] > (float)num2)
				{
					num2 = (int)line.widths[line.text.Length];
				}
				if (line.alignment != HorizontalAlignment.Left)
				{
					if (line.alignment == HorizontalAlignment.Center)
					{
						line.align_shift = (this.viewport_width - (int)line.widths[line.text.Length]) / 2;
					}
					else
					{
						line.align_shift = this.viewport_width - (int)line.widths[line.text.Length] - 1;
					}
				}
				if (this.multiline)
				{
					num += line.height;
				}
				else
				{
					num += (int)line.widths[line.text.Length];
				}
				if (i > this.lines)
				{
					break;
				}
			}
			if (this.document_x != num2)
			{
				this.document_x = num2;
				if (this.WidthChanged != null)
				{
					this.WidthChanged.Invoke(this, null);
				}
			}
			this.RecalculateAlignments();
			line = this.GetLine(this.lines);
			if (this.document_y != line.Y + line.height)
			{
				this.document_y = line.Y + line.height;
				if (this.HeightChanged != null)
				{
					this.HeightChanged.Invoke(this, null);
				}
			}
			if (this.EnableLinks)
			{
				this.ScanForLinks(start, end, ref flag);
			}
			this.UpdateCaret();
			return flag;
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000D11F0 File Offset: 0x000CF3F0
		internal int Size()
		{
			return this.lines;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000D11F8 File Offset: 0x000CF3F8
		private void owner_HandleCreated(object sender, EventArgs e)
		{
			this.RecalculateDocument(this.owner.CreateGraphicsInternal());
			this.AlignCaret();
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000D1214 File Offset: 0x000CF414
		private void owner_VisibleChanged(object sender, EventArgs e)
		{
			if (this.owner.Visible)
			{
				this.RecalculateDocument(this.owner.CreateGraphicsInternal());
			}
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000D1244 File Offset: 0x000CF444
		internal static bool IsWordSeparator(char ch)
		{
			switch (ch)
			{
			case '\t':
			case '\n':
			case '\r':
				break;
			default:
				if (ch != '(' && ch != ')' && ch != ' ')
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000D1290 File Offset: 0x000CF490
		internal int FindWordSeparator(Line line, int pos, bool forward)
		{
			int length = line.text.Length;
			if (forward)
			{
				for (int i = pos + 1; i < length; i++)
				{
					if (Document.IsWordSeparator(line.Text.get_Chars(i)))
					{
						return i + 1;
					}
				}
				return length;
			}
			for (int j = pos - 1; j > 0; j--)
			{
				if (Document.IsWordSeparator(line.Text.get_Chars(j - 1)))
				{
					return j;
				}
			}
			return 0;
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000D1310 File Offset: 0x000CF510
		internal bool FindChars(char[] chars, Document.Marker start, Document.Marker end, out Document.Marker result)
		{
			result = default(Document.Marker);
			Line line = start.line;
			int i = start.line.line_no;
			int j = start.pos;
			while (i <= end.line.line_no)
			{
				int length = line.text.Length;
				while (j < length)
				{
					int k = 0;
					while (k < chars.Length)
					{
						if (line.text.get_Chars(j) == chars[k])
						{
							if (line.line_no == end.line.line_no && j >= end.pos)
							{
								return false;
							}
							result.line = line;
							result.pos = j;
							return true;
						}
						else
						{
							k++;
						}
					}
					j++;
				}
				j = 0;
				i++;
				line = this.GetLine(i);
			}
			return false;
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000D13EC File Offset: 0x000CF5EC
		internal bool Find(string search, Document.Marker start, Document.Marker end, out Document.Marker result, RichTextBoxFinds options)
		{
			result = default(Document.Marker);
			bool flag = (options & RichTextBoxFinds.WholeWord) != RichTextBoxFinds.None;
			bool flag2 = (options & RichTextBoxFinds.MatchCase) == RichTextBoxFinds.None;
			bool flag3 = (options & RichTextBoxFinds.Reverse) != RichTextBoxFinds.None;
			Line line = start.line;
			int i = start.line.line_no;
			int j = start.pos;
			int num = 0;
			string text;
			if (flag2)
			{
				StringBuilder stringBuilder = new StringBuilder(search);
				for (int k = 0; k < stringBuilder.Length; k++)
				{
					stringBuilder.set_Chars(k, char.ToLower(stringBuilder.get_Chars(k)));
				}
				text = stringBuilder.ToString();
			}
			else
			{
				text = search;
			}
			bool flag4;
			if (flag)
			{
				if (i == 1)
				{
					flag4 = j == 0 || Document.IsWordSeparator(line.text.get_Chars(j - 1));
				}
				else if (j > 0)
				{
					flag4 = Document.IsWordSeparator(line.text.get_Chars(j - 1));
				}
				else
				{
					Line line2 = this.GetLine(i - 1);
					flag4 = line2.ending != LineEnding.Wrap || Document.IsWordSeparator(line2.text.get_Chars(line2.text.Length - 1));
				}
			}
			else
			{
				flag4 = false;
			}
			Document.Marker marker = default(Document.Marker);
			marker.height = -1;
			while (i <= end.line.line_no)
			{
				int num2;
				if (i != end.line.line_no)
				{
					num2 = line.text.Length;
				}
				else
				{
					num2 = end.pos;
				}
				while (j < num2)
				{
					if (flag && num == text.Length)
					{
						if (Document.IsWordSeparator(line.text.get_Chars(j)))
						{
							if (!flag3)
							{
								goto IL_036E;
							}
							marker = result;
							num = 0;
						}
						else
						{
							num = 0;
						}
					}
					char c;
					if (flag2)
					{
						c = char.ToLower(line.text.get_Chars(j));
					}
					else
					{
						c = line.text.get_Chars(j);
					}
					if (c == text.get_Chars(num))
					{
						if (num == 0)
						{
							result.line = line;
							result.pos = j;
						}
						if (!flag || (flag && (flag4 || num > 0)))
						{
							num++;
						}
						if (!flag && num == text.Length)
						{
							if (!flag3)
							{
								goto IL_036E;
							}
							marker = result;
							num = 0;
						}
					}
					else
					{
						num = 0;
					}
					j++;
					if (!flag)
					{
						continue;
					}
					flag4 = Document.IsWordSeparator(c);
					continue;
					IL_036E:
					if (!flag3)
					{
						return true;
					}
					result = marker;
					return true;
				}
				if (flag)
				{
					if (line.ending != LineEnding.Wrap || line.line_no == this.lines - 1)
					{
						flag4 = true;
					}
					if (num == text.Length)
					{
						if (flag4)
						{
							if (!flag3)
							{
								goto IL_036E;
							}
							marker = result;
							num = 0;
						}
						else
						{
							num = 0;
						}
					}
				}
				j = 0;
				i++;
				line = this.GetLine(i);
			}
			if (flag3 && marker.height != -1)
			{
				result = marker;
				return true;
			}
			return false;
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000D177C File Offset: 0x000CF97C
		internal void GetMarker(out Document.Marker mark, bool start)
		{
			mark = default(Document.Marker);
			if (start)
			{
				mark.line = this.GetLine(1);
				mark.tag = mark.line.tags;
				mark.pos = 0;
			}
			else
			{
				mark.line = this.GetLine(this.lines);
				mark.tag = mark.line.tags;
				while (mark.tag.Next != null)
				{
					mark.tag = mark.tag.Next;
				}
				mark.pos = mark.line.text.Length;
			}
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000D1820 File Offset: 0x000CFA20
		public IEnumerator GetEnumerator()
		{
			return null;
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x000D1824 File Offset: 0x000CFA24
		public override bool Equals(object obj)
		{
			return obj != null && obj is Document && (obj == this || this.ToString().Equals(((Document)obj).ToString()));
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000D1870 File Offset: 0x000CFA70
		public override int GetHashCode()
		{
			return this.document_id;
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000D1878 File Offset: 0x000CFA78
		public override string ToString()
		{
			return "document " + this.document_id;
		}

		// Token: 0x04001904 RID: 6404
		private Line document;

		// Token: 0x04001905 RID: 6405
		private int lines;

		// Token: 0x04001906 RID: 6406
		private Line sentinel;

		// Token: 0x04001907 RID: 6407
		private int document_id;

		// Token: 0x04001908 RID: 6408
		private Random random = new Random();

		// Token: 0x04001909 RID: 6409
		internal string password_char;

		// Token: 0x0400190A RID: 6410
		private StringBuilder password_cache;

		// Token: 0x0400190B RID: 6411
		private bool calc_pass;

		// Token: 0x0400190C RID: 6412
		private int char_count;

		// Token: 0x0400190D RID: 6413
		private bool enable_links;

		// Token: 0x0400190E RID: 6414
		public static readonly StringFormat string_format = new StringFormat(StringFormat.GenericTypographic);

		// Token: 0x0400190F RID: 6415
		private int recalc_suspended;

		// Token: 0x04001910 RID: 6416
		private bool recalc_pending;

		// Token: 0x04001911 RID: 6417
		private int recalc_start = 1;

		// Token: 0x04001912 RID: 6418
		private int recalc_end;

		// Token: 0x04001913 RID: 6419
		private bool recalc_optimize;

		// Token: 0x04001914 RID: 6420
		private int update_suspended;

		// Token: 0x04001915 RID: 6421
		private bool update_pending;

		// Token: 0x04001916 RID: 6422
		private int update_start = 1;

		// Token: 0x04001917 RID: 6423
		internal bool multiline;

		// Token: 0x04001918 RID: 6424
		internal HorizontalAlignment alignment;

		// Token: 0x04001919 RID: 6425
		internal bool wrap;

		// Token: 0x0400191A RID: 6426
		internal UndoManager undo;

		// Token: 0x0400191B RID: 6427
		internal Document.Marker caret;

		// Token: 0x0400191C RID: 6428
		internal Document.Marker selection_start;

		// Token: 0x0400191D RID: 6429
		internal Document.Marker selection_end;

		// Token: 0x0400191E RID: 6430
		internal bool selection_visible;

		// Token: 0x0400191F RID: 6431
		internal Document.Marker selection_anchor;

		// Token: 0x04001920 RID: 6432
		internal Document.Marker selection_prev;

		// Token: 0x04001921 RID: 6433
		internal bool selection_end_anchor;

		// Token: 0x04001922 RID: 6434
		internal int viewport_x;

		// Token: 0x04001923 RID: 6435
		internal int viewport_y;

		// Token: 0x04001924 RID: 6436
		internal int offset_x;

		// Token: 0x04001925 RID: 6437
		internal int offset_y;

		// Token: 0x04001926 RID: 6438
		internal int viewport_width;

		// Token: 0x04001927 RID: 6439
		internal int viewport_height;

		// Token: 0x04001928 RID: 6440
		internal int document_x;

		// Token: 0x04001929 RID: 6441
		internal int document_y;

		// Token: 0x0400192A RID: 6442
		internal int crlf_size;

		// Token: 0x0400192B RID: 6443
		internal TextBoxBase owner;

		// Token: 0x0400192C RID: 6444
		internal static int caret_width = 1;

		// Token: 0x0400192D RID: 6445
		internal static int caret_shift = 1;

		// Token: 0x0400192E RID: 6446
		internal int left_margin = 2;

		// Token: 0x0400192F RID: 6447
		internal int top_margin = 2;

		// Token: 0x04001930 RID: 6448
		internal int right_margin = 2;

		// Token: 0x0200031C RID: 796
		internal struct Marker
		{
			// Token: 0x0600358D RID: 13709 RVA: 0x000D1890 File Offset: 0x000CFA90
			public void Combine(Line move_to_line, int move_to_line_length)
			{
				this.line = move_to_line;
				this.pos += move_to_line_length;
				this.tag = LineTag.FindTag(this.line, this.pos);
			}

			// Token: 0x0600358E RID: 13710 RVA: 0x000D18CC File Offset: 0x000CFACC
			public void Split(Line move_to_line, int split_at)
			{
				this.line = move_to_line;
				this.pos -= split_at;
				this.tag = LineTag.FindTag(this.line, this.pos);
			}

			// Token: 0x0600358F RID: 13711 RVA: 0x000D1908 File Offset: 0x000CFB08
			public override bool Equals(object obj)
			{
				return this == (Document.Marker)obj;
			}

			// Token: 0x06003590 RID: 13712 RVA: 0x000D191C File Offset: 0x000CFB1C
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06003591 RID: 13713 RVA: 0x000D1930 File Offset: 0x000CFB30
			public override string ToString()
			{
				return string.Concat(new object[] { "Marker Line ", this.line, ", Position ", this.pos });
			}

			// Token: 0x06003592 RID: 13714 RVA: 0x000D1970 File Offset: 0x000CFB70
			public static bool operator <(Document.Marker lhs, Document.Marker rhs)
			{
				return lhs.line.line_no < rhs.line.line_no || (lhs.line.line_no == rhs.line.line_no && lhs.pos < rhs.pos);
			}

			// Token: 0x06003593 RID: 13715 RVA: 0x000D19D0 File Offset: 0x000CFBD0
			public static bool operator >(Document.Marker lhs, Document.Marker rhs)
			{
				return lhs.line.line_no > rhs.line.line_no || (lhs.line.line_no == rhs.line.line_no && lhs.pos > rhs.pos);
			}

			// Token: 0x06003594 RID: 13716 RVA: 0x000D1A30 File Offset: 0x000CFC30
			public static bool operator ==(Document.Marker lhs, Document.Marker rhs)
			{
				return lhs.line.line_no == rhs.line.line_no && lhs.pos == rhs.pos;
			}

			// Token: 0x06003595 RID: 13717 RVA: 0x000D1A68 File Offset: 0x000CFC68
			public static bool operator !=(Document.Marker lhs, Document.Marker rhs)
			{
				return lhs.line.line_no != rhs.line.line_no || lhs.pos != rhs.pos;
			}

			// Token: 0x04001937 RID: 6455
			internal Line line;

			// Token: 0x04001938 RID: 6456
			internal LineTag tag;

			// Token: 0x04001939 RID: 6457
			internal int pos;

			// Token: 0x0400193A RID: 6458
			internal int height;
		}
	}
}

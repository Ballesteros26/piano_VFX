using System;
using System.Collections;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x0200031E RID: 798
	internal class UndoManager
	{
		// Token: 0x0600359D RID: 13725 RVA: 0x000D1B2C File Offset: 0x000CFD2C
		internal UndoManager(Document document)
		{
			this.document = document;
			this.undo_actions = new Stack(50);
			this.redo_actions = new Stack(50);
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600359E RID: 13726 RVA: 0x000D1B58 File Offset: 0x000CFD58
		internal bool CanUndo
		{
			get
			{
				return this.undo_actions.Count > 0;
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x000D1B68 File Offset: 0x000CFD68
		internal bool CanRedo
		{
			get
			{
				return this.redo_actions.Count > 0;
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000D1B78 File Offset: 0x000CFD78
		internal string UndoActionName
		{
			get
			{
				foreach (object obj in this.undo_actions)
				{
					UndoManager.Action action = (UndoManager.Action)obj;
					if (action.type == UndoManager.ActionType.UserActionBegin)
					{
						return (string)action.data;
					}
					if (action.type == UndoManager.ActionType.Typing)
					{
						return Locale.GetText("Typing");
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000D1C20 File Offset: 0x000CFE20
		internal string RedoActionName
		{
			get
			{
				foreach (object obj in this.redo_actions)
				{
					UndoManager.Action action = (UndoManager.Action)obj;
					if (action.type == UndoManager.ActionType.UserActionBegin)
					{
						return (string)action.data;
					}
					if (action.type == UndoManager.ActionType.Typing)
					{
						return Locale.GetText("Typing");
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000D1CC8 File Offset: 0x000CFEC8
		internal void Clear()
		{
			this.undo_actions.Clear();
			this.redo_actions.Clear();
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000D1CE0 File Offset: 0x000CFEE0
		internal bool Undo()
		{
			bool flag = false;
			if (this.undo_actions.Count == 0)
			{
				return false;
			}
			this.locked = true;
			do
			{
				UndoManager.Action action = (UndoManager.Action)this.undo_actions.Pop();
				this.redo_actions.Push(action);
				switch (action.type)
				{
				case UndoManager.ActionType.Typing:
				{
					Line line = this.document.GetLine(action.line_no);
					this.document.SuspendUpdate();
					this.document.DeleteMultiline(line, action.pos, ((StringBuilder)action.data).Length);
					this.document.PositionCaret(line, action.pos);
					this.document.SetSelectionToCaret(true);
					this.document.ResumeUpdate(true);
					flag = true;
					break;
				}
				case UndoManager.ActionType.InsertString:
				{
					Line line = this.document.GetLine(action.line_no);
					this.document.SuspendUpdate();
					this.document.DeleteMultiline(line, action.pos, ((string)action.data).Length + 1);
					this.document.PositionCaret(line, action.pos);
					this.document.SetSelectionToCaret(true);
					this.document.ResumeUpdate(true);
					break;
				}
				case UndoManager.ActionType.DeleteString:
				{
					Line line = this.document.GetLine(action.line_no);
					this.document.SuspendUpdate();
					this.Insert(line, action.pos, (Line)action.data, true);
					this.document.ResumeUpdate(true);
					break;
				}
				case UndoManager.ActionType.UserActionBegin:
					flag = true;
					break;
				}
			}
			while (!flag && this.undo_actions.Count > 0);
			this.locked = false;
			return true;
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000D1EA0 File Offset: 0x000D00A0
		internal bool Redo()
		{
			bool flag = false;
			if (this.redo_actions.Count == 0)
			{
				return false;
			}
			this.locked = true;
			do
			{
				UndoManager.Action action = (UndoManager.Action)this.redo_actions.Pop();
				this.undo_actions.Push(action);
				switch (action.type)
				{
				case UndoManager.ActionType.Typing:
				{
					Line line = this.document.GetLine(action.line_no);
					this.document.SuspendUpdate();
					int num = this.document.LineTagToCharIndex(line, action.pos);
					this.document.InsertString(line, action.pos, ((StringBuilder)action.data).ToString());
					this.document.CharIndexToLineTag(num + ((StringBuilder)action.data).Length, out this.document.caret.line, out this.document.caret.tag, out this.document.caret.pos);
					this.document.UpdateCaret();
					this.document.SetSelectionToCaret(true);
					this.document.ResumeUpdate(true);
					flag = true;
					break;
				}
				case UndoManager.ActionType.InsertString:
				{
					Line line = this.document.GetLine(action.line_no);
					this.document.SuspendUpdate();
					int num = this.document.LineTagToCharIndex(line, action.pos);
					this.document.InsertString(line, action.pos, (string)action.data);
					this.document.CharIndexToLineTag(num + ((string)action.data).Length, out this.document.caret.line, out this.document.caret.tag, out this.document.caret.pos);
					this.document.UpdateCaret();
					this.document.SetSelectionToCaret(true);
					this.document.ResumeUpdate(true);
					break;
				}
				case UndoManager.ActionType.DeleteString:
				{
					Line line = this.document.GetLine(action.line_no);
					this.document.SuspendUpdate();
					this.document.DeleteMultiline(line, action.pos, ((Line)action.data).text.Length);
					this.document.PositionCaret(line, action.pos);
					this.document.SetSelectionToCaret(true);
					this.document.ResumeUpdate(true);
					break;
				}
				case UndoManager.ActionType.UserActionEnd:
					flag = true;
					break;
				}
			}
			while (!flag && this.redo_actions.Count > 0);
			this.locked = false;
			return true;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000D2138 File Offset: 0x000D0338
		public void BeginUserAction(string name)
		{
			if (this.locked)
			{
				return;
			}
			this.redo_actions.Clear();
			UndoManager.Action action = new UndoManager.Action();
			action.type = UndoManager.ActionType.UserActionBegin;
			action.data = name;
			this.undo_actions.Push(action);
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000D217C File Offset: 0x000D037C
		public void EndUserAction()
		{
			if (this.locked)
			{
				return;
			}
			UndoManager.Action action = new UndoManager.Action();
			action.type = UndoManager.ActionType.UserActionEnd;
			this.undo_actions.Push(action);
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000D21B0 File Offset: 0x000D03B0
		public void RecordDeleteString(Line start_line, int start_pos, Line end_line, int end_pos)
		{
			if (this.locked)
			{
				return;
			}
			this.redo_actions.Clear();
			UndoManager.Action action = new UndoManager.Action();
			action.type = UndoManager.ActionType.DeleteString;
			action.line_no = start_line.line_no;
			action.pos = start_pos;
			action.data = this.Duplicate(start_line, start_pos, end_line, end_pos);
			this.undo_actions.Push(action);
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000D2214 File Offset: 0x000D0414
		public void RecordInsertString(Line line, int pos, string str)
		{
			if (this.locked || str.Length == 0)
			{
				return;
			}
			this.redo_actions.Clear();
			UndoManager.Action action = new UndoManager.Action();
			action.type = UndoManager.ActionType.InsertString;
			action.data = str;
			action.line_no = line.line_no;
			action.pos = pos;
			this.undo_actions.Push(action);
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000D2278 File Offset: 0x000D0478
		public void RecordTyping(Line line, int pos, char ch)
		{
			if (this.locked)
			{
				return;
			}
			this.redo_actions.Clear();
			UndoManager.Action action = null;
			if (this.undo_actions.Count > 0)
			{
				action = (UndoManager.Action)this.undo_actions.Peek();
			}
			if (action == null || action.type != UndoManager.ActionType.Typing)
			{
				action = new UndoManager.Action();
				action.type = UndoManager.ActionType.Typing;
				action.data = new StringBuilder();
				action.line_no = line.line_no;
				action.pos = pos;
				this.undo_actions.Push(action);
			}
			StringBuilder stringBuilder = (StringBuilder)action.data;
			stringBuilder.Append(ch);
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000D231C File Offset: 0x000D051C
		public Line Duplicate(Line start_line, int start_pos, Line end_line, int end_pos)
		{
			Line line = new Line(start_line.document, start_line.ending);
			Line line2 = line;
			for (int i = start_line.line_no; i <= end_line.line_no; i++)
			{
				Line line3 = this.document.GetLine(i);
				int num;
				if (start_line.line_no == i)
				{
					num = start_pos;
				}
				else
				{
					num = 0;
				}
				int num2;
				if (end_line.line_no == i)
				{
					num2 = end_pos;
				}
				else
				{
					num2 = line3.text.Length;
				}
				if (end_pos != 0)
				{
					line.text = new StringBuilder(line3.text.ToString(num, num2 - num));
					LineTag lineTag = line3.FindTag(num + 1);
					while (lineTag != null && lineTag.Start <= num2)
					{
						int num3;
						if (lineTag.Start <= num && num < lineTag.Start + lineTag.Length)
						{
							num3 = num;
						}
						else
						{
							num3 = lineTag.Start;
						}
						LineTag lineTag2 = new LineTag(line, num3 - num + 1);
						lineTag2.CopyFormattingFrom(lineTag);
						lineTag = lineTag.Next;
						if (line.tags == null)
						{
							line.tags = lineTag2;
						}
						else
						{
							LineTag lineTag3 = line.tags;
							while (lineTag3.Next != null)
							{
								lineTag3 = lineTag3.Next;
							}
							lineTag3.Next = lineTag2;
							lineTag2.Previous = lineTag3;
						}
					}
					if (i + 1 <= end_line.line_no)
					{
						line.ending = line3.ending;
						line.right = new Line(start_line.document, start_line.ending);
						line.right.left = line;
						line = line.right;
					}
				}
			}
			return line2;
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000D24DC File Offset: 0x000D06DC
		internal void Insert(Line line, int pos, Line insert, bool select)
		{
			LineTag lineTag;
			int num2;
			if (insert.right != null)
			{
				Line line2 = line;
				int num = 1;
				Line line3 = insert;
				while (line3 != null)
				{
					if (line3 == insert)
					{
						this.document.Split(line.line_no, pos);
						lineTag = line.tags;
						if (lineTag != null && lineTag.Length != 0)
						{
							while (lineTag.Next != null)
							{
								lineTag = lineTag.Next;
							}
							num2 = lineTag.Start + lineTag.Length - 1;
							lineTag.Next = line3.tags;
							lineTag.Next.Previous = lineTag;
							lineTag = lineTag.Next;
						}
						else
						{
							num2 = 0;
							line.tags = line3.tags;
							line.tags.Previous = null;
							lineTag = line.tags;
						}
						line.ending = line3.ending;
					}
					else
					{
						this.document.Split(line.line_no, 0);
						num2 = 0;
						line.tags = line3.tags;
						line.tags.Previous = null;
						line.ending = line3.ending;
						lineTag = line.tags;
					}
					while (lineTag != null)
					{
						lineTag.Start += num2 - 1;
						lineTag.Line = line;
						lineTag = lineTag.Next;
					}
					line.text.Insert(num2, line3.text.ToString());
					line.Grow(line.text.Length);
					line.recalc = true;
					line = this.document.GetLine(line.line_no + 1);
					if (line3.right == null && line3.tags.Length != 0)
					{
						this.document.Combine(line.line_no - 1, line.line_no);
					}
					line3 = line3.right;
					num++;
				}
				this.document.UpdateView(line2, num, pos);
				return;
			}
			this.document.Split(line, pos);
			if (insert.tags == null)
			{
				return;
			}
			lineTag = line.tags;
			while (lineTag.Next != null)
			{
				lineTag = lineTag.Next;
			}
			num2 = lineTag.Start + lineTag.Length - 1;
			lineTag.Next = insert.tags;
			line.text.Insert(num2, insert.text.ToString());
			for (lineTag = lineTag.Next; lineTag != null; lineTag = lineTag.Next)
			{
				lineTag.Start += num2;
				lineTag.Line = line;
			}
			this.document.Combine(line.line_no, line.line_no + 1);
			if (select)
			{
				this.document.SetSelectionStart(line, pos, false);
				this.document.SetSelectionEnd(line, pos + insert.text.Length, false);
			}
			this.document.UpdateView(line, pos);
		}

		// Token: 0x0400193C RID: 6460
		private Document document;

		// Token: 0x0400193D RID: 6461
		private Stack undo_actions;

		// Token: 0x0400193E RID: 6462
		private Stack redo_actions;

		// Token: 0x0400193F RID: 6463
		private bool locked;

		// Token: 0x0200031F RID: 799
		internal enum ActionType
		{
			// Token: 0x04001941 RID: 6465
			Typing,
			// Token: 0x04001942 RID: 6466
			InsertString,
			// Token: 0x04001943 RID: 6467
			DeleteString,
			// Token: 0x04001944 RID: 6468
			UserActionBegin,
			// Token: 0x04001945 RID: 6469
			UserActionEnd
		}

		// Token: 0x02000320 RID: 800
		internal class Action
		{
			// Token: 0x04001946 RID: 6470
			internal UndoManager.ActionType type;

			// Token: 0x04001947 RID: 6471
			internal int line_no;

			// Token: 0x04001948 RID: 6472
			internal int pos;

			// Token: 0x04001949 RID: 6473
			internal object data;
		}
	}
}

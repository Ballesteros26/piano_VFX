using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	public class TextEditor
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000FB50 File Offset: 0x0000DD50
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000FB68 File Offset: 0x0000DD68
		[Obsolete("Please use 'text' instead of 'content'", false)]
		public GUIContent content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				this.m_Content = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000FB74 File Offset: 0x0000DD74
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000FB91 File Offset: 0x0000DD91
		public string text
		{
			get
			{
				return this.m_Content.text;
			}
			set
			{
				this.m_Content.text = value ?? string.Empty;
				this.EnsureValidCodePointIndex(ref this.m_CursorIndex);
				this.EnsureValidCodePointIndex(ref this.m_SelectIndex);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		public Rect position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				bool flag = this.m_Position == value;
				if (!flag)
				{
					this.scrollOffset = Vector2.zero;
					this.m_Position = GUIUtility.AlignRectToDevice(value);
					this.UpdateScrollOffset();
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		internal virtual Rect localPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000FC34 File Offset: 0x0000DE34
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		public int cursorIndex
		{
			get
			{
				return this.m_CursorIndex;
			}
			set
			{
				int cursorIndex = this.m_CursorIndex;
				this.m_CursorIndex = value;
				this.EnsureValidCodePointIndex(ref this.m_CursorIndex);
				bool flag = this.m_CursorIndex != cursorIndex;
				if (flag)
				{
					this.m_RevealCursor = true;
					this.OnCursorIndexChange();
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000FC98 File Offset: 0x0000DE98
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000FCB0 File Offset: 0x0000DEB0
		public int selectIndex
		{
			get
			{
				return this.m_SelectIndex;
			}
			set
			{
				int selectIndex = this.m_SelectIndex;
				this.m_SelectIndex = value;
				this.EnsureValidCodePointIndex(ref this.m_SelectIndex);
				bool flag = this.m_SelectIndex != selectIndex;
				if (flag)
				{
					this.OnSelectIndexChange();
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		private void ClearCursorPos()
		{
			this.hasHorizontalCursorPos = false;
			this.m_iAltCursorPos = -1;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000FD04 File Offset: 0x0000DF04
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		public TextEditor.DblClickSnapping doubleClickSnapping
		{
			get
			{
				return this.m_DblClickSnap;
			}
			set
			{
				this.m_DblClickSnap = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000FD28 File Offset: 0x0000DF28
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public int altCursorPosition
		{
			get
			{
				return this.m_iAltCursorPos;
			}
			set
			{
				this.m_iAltCursorPos = value;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		[RequiredByNativeCode]
		public TextEditor()
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public void OnFocus()
		{
			bool flag = this.multiline;
			if (flag)
			{
				this.cursorIndex = (this.selectIndex = 0);
			}
			else
			{
				this.SelectAll();
			}
			this.m_HasFocus = true;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000FE1A File Offset: 0x0000E01A
		public void OnLostFocus()
		{
			this.m_HasFocus = false;
			this.scrollOffset = Vector2.zero;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000FE30 File Offset: 0x0000E030
		private void GrabGraphicalCursorPos()
		{
			bool flag = !this.hasHorizontalCursorPos;
			if (flag)
			{
				this.graphicalCursorPos = this.style.GetCursorPixelPosition(this.localPosition, this.m_Content, this.cursorIndex);
				this.graphicalSelectCursorPos = this.style.GetCursorPixelPosition(this.localPosition, this.m_Content, this.selectIndex);
				this.hasHorizontalCursorPos = false;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000FE9C File Offset: 0x0000E09C
		public bool HandleKeyEvent(Event e)
		{
			return this.HandleKeyEvent(e, false);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		[VisibleToOtherModules]
		internal bool HandleKeyEvent(Event e, bool textIsReadOnly)
		{
			this.InitKeyActions();
			EventModifiers modifiers = e.modifiers;
			e.modifiers &= ~EventModifiers.CapsLock;
			bool flag = TextEditor.s_Keyactions.ContainsKey(e);
			bool flag2;
			if (flag)
			{
				TextEditor.TextEditOp textEditOp = TextEditor.s_Keyactions[e];
				this.PerformOperation(textEditOp, textIsReadOnly);
				e.modifiers = modifiers;
				flag2 = true;
			}
			else
			{
				e.modifiers = modifiers;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000FF24 File Offset: 0x0000E124
		public bool DeleteLineBack()
		{
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				int num = this.cursorIndex;
				int num2 = num;
				while (num2-- != 0)
				{
					bool flag2 = this.text.get_Chars(num2) == '\n';
					if (flag2)
					{
						num = num2 + 1;
						break;
					}
				}
				bool flag3 = num2 == -1;
				if (flag3)
				{
					num = 0;
				}
				bool flag4 = this.cursorIndex != num;
				if (flag4)
				{
					this.m_Content.text = this.text.Remove(num, this.cursorIndex - num);
					this.selectIndex = (this.cursorIndex = num);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public bool DeleteWordBack()
		{
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				int num = this.FindEndOfPreviousWord(this.cursorIndex);
				bool flag2 = this.cursorIndex != num;
				if (flag2)
				{
					this.m_Content.text = this.text.Remove(num, this.cursorIndex - num);
					this.selectIndex = (this.cursorIndex = num);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00010060 File Offset: 0x0000E260
		public bool DeleteWordForward()
		{
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				int num = this.FindStartOfNextWord(this.cursorIndex);
				bool flag2 = this.cursorIndex < this.text.Length;
				if (flag2)
				{
					this.m_Content.text = this.text.Remove(this.cursorIndex, num - this.cursorIndex);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000100D8 File Offset: 0x0000E2D8
		public bool Delete()
		{
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				bool flag2 = this.cursorIndex < this.text.Length;
				if (flag2)
				{
					this.m_Content.text = this.text.Remove(this.cursorIndex, this.NextCodePointIndex(this.cursorIndex) - this.cursorIndex);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00010150 File Offset: 0x0000E350
		public bool CanPaste()
		{
			return GUIUtility.systemCopyBuffer.Length != 0;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00010170 File Offset: 0x0000E370
		public bool Backspace()
		{
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				bool flag2 = this.cursorIndex > 0;
				if (flag2)
				{
					int num = this.PreviousCodePointIndex(this.cursorIndex);
					this.m_Content.text = this.text.Remove(num, this.cursorIndex - num);
					this.selectIndex = (this.cursorIndex = num);
					this.ClearCursorPos();
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000101F3 File Offset: 0x0000E3F3
		public void SelectAll()
		{
			this.cursorIndex = 0;
			this.selectIndex = this.text.Length;
			this.ClearCursorPos();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00010217 File Offset: 0x0000E417
		public void SelectNone()
		{
			this.selectIndex = this.cursorIndex;
			this.ClearCursorPos();
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00010230 File Offset: 0x0000E430
		public bool hasSelection
		{
			get
			{
				return this.cursorIndex != this.selectIndex;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00010254 File Offset: 0x0000E454
		public string SelectedText
		{
			get
			{
				bool flag = this.cursorIndex == this.selectIndex;
				string text;
				if (flag)
				{
					text = "";
				}
				else
				{
					bool flag2 = this.cursorIndex < this.selectIndex;
					if (flag2)
					{
						text = this.text.Substring(this.cursorIndex, this.selectIndex - this.cursorIndex);
					}
					else
					{
						text = this.text.Substring(this.selectIndex, this.cursorIndex - this.selectIndex);
					}
				}
				return text;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000102D4 File Offset: 0x0000E4D4
		public bool DeleteSelection()
		{
			bool flag = this.cursorIndex == this.selectIndex;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.cursorIndex < this.selectIndex;
				if (flag3)
				{
					this.m_Content.text = this.text.Substring(0, this.cursorIndex) + this.text.Substring(this.selectIndex, this.text.Length - this.selectIndex);
					this.selectIndex = this.cursorIndex;
				}
				else
				{
					this.m_Content.text = this.text.Substring(0, this.selectIndex) + this.text.Substring(this.cursorIndex, this.text.Length - this.cursorIndex);
					this.cursorIndex = this.selectIndex;
				}
				this.ClearCursorPos();
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000103C8 File Offset: 0x0000E5C8
		public void ReplaceSelection(string replace)
		{
			this.DeleteSelection();
			this.m_Content.text = this.text.Insert(this.cursorIndex, replace);
			this.selectIndex = (this.cursorIndex += replace.Length);
			this.ClearCursorPos();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00010420 File Offset: 0x0000E620
		public void Insert(char c)
		{
			this.ReplaceSelection(c.ToString());
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00010434 File Offset: 0x0000E634
		public void MoveSelectionToAltCursor()
		{
			bool flag = this.m_iAltCursorPos == -1;
			if (!flag)
			{
				int iAltCursorPos = this.m_iAltCursorPos;
				string selectedText = this.SelectedText;
				this.m_Content.text = this.text.Insert(iAltCursorPos, selectedText);
				bool flag2 = iAltCursorPos < this.cursorIndex;
				if (flag2)
				{
					this.cursorIndex += selectedText.Length;
					this.selectIndex += selectedText.Length;
				}
				this.DeleteSelection();
				this.selectIndex = (this.cursorIndex = iAltCursorPos);
				this.ClearCursorPos();
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000104D4 File Offset: 0x0000E6D4
		public void MoveRight()
		{
			this.ClearCursorPos();
			bool flag = this.selectIndex == this.cursorIndex;
			if (flag)
			{
				this.cursorIndex = this.NextCodePointIndex(this.cursorIndex);
				this.DetectFocusChange();
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				bool flag2 = this.selectIndex > this.cursorIndex;
				if (flag2)
				{
					this.cursorIndex = this.selectIndex;
				}
				else
				{
					this.selectIndex = this.cursorIndex;
				}
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00010558 File Offset: 0x0000E758
		public void MoveLeft()
		{
			bool flag = this.selectIndex == this.cursorIndex;
			if (flag)
			{
				this.cursorIndex = this.PreviousCodePointIndex(this.cursorIndex);
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				bool flag2 = this.selectIndex > this.cursorIndex;
				if (flag2)
				{
					this.selectIndex = this.cursorIndex;
				}
				else
				{
					this.cursorIndex = this.selectIndex;
				}
			}
			this.ClearCursorPos();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000105D4 File Offset: 0x0000E7D4
		public void MoveUp()
		{
			bool flag = this.selectIndex < this.cursorIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.cursorIndex = this.selectIndex;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			this.cursorIndex = (this.selectIndex = this.style.GetCursorStringIndex(this.localPosition, this.m_Content, this.graphicalCursorPos));
			bool flag2 = this.cursorIndex <= 0;
			if (flag2)
			{
				this.ClearCursorPos();
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00010670 File Offset: 0x0000E870
		public void MoveDown()
		{
			bool flag = this.selectIndex > this.cursorIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.cursorIndex = this.selectIndex;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			this.cursorIndex = (this.selectIndex = this.style.GetCursorStringIndex(this.localPosition, this.m_Content, this.graphicalCursorPos));
			bool flag2 = this.cursorIndex == this.text.Length;
			if (flag2)
			{
				this.ClearCursorPos();
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00010720 File Offset: 0x0000E920
		public void MoveLineStart()
		{
			int num = ((this.selectIndex < this.cursorIndex) ? this.selectIndex : this.cursorIndex);
			int num2 = num;
			while (num2-- != 0)
			{
				bool flag = this.text.get_Chars(num2) == '\n';
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = num2 + 1);
					return;
				}
			}
			this.selectIndex = (this.cursorIndex = 0);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001079C File Offset: 0x0000E99C
		public void MoveLineEnd()
		{
			int num = ((this.selectIndex > this.cursorIndex) ? this.selectIndex : this.cursorIndex);
			int i = num;
			int length = this.text.Length;
			while (i < length)
			{
				bool flag = this.text.get_Chars(i) == '\n';
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = i);
					return;
				}
				i++;
			}
			this.selectIndex = (this.cursorIndex = length);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00010828 File Offset: 0x0000EA28
		public void MoveGraphicalLineStart()
		{
			this.cursorIndex = (this.selectIndex = this.GetGraphicalLineStart((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex));
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001086C File Offset: 0x0000EA6C
		public void MoveGraphicalLineEnd()
		{
			this.cursorIndex = (this.selectIndex = this.GetGraphicalLineEnd((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex));
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public void MoveTextStart()
		{
			this.selectIndex = (this.cursorIndex = 0);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000108D0 File Offset: 0x0000EAD0
		public void MoveTextEnd()
		{
			this.selectIndex = (this.cursorIndex = this.text.Length);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000108FC File Offset: 0x0000EAFC
		private int IndexOfEndOfLine(int startIndex)
		{
			int num = this.text.IndexOf('\n', startIndex);
			return (num != -1) ? num : this.text.Length;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00010930 File Offset: 0x0000EB30
		public void MoveParagraphForward()
		{
			this.cursorIndex = ((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex);
			bool flag = this.cursorIndex < this.text.Length;
			if (flag)
			{
				this.selectIndex = (this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex + 1));
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001099C File Offset: 0x0000EB9C
		public void MoveParagraphBackward()
		{
			this.cursorIndex = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
			bool flag = this.cursorIndex > 1;
			if (flag)
			{
				this.selectIndex = (this.cursorIndex = this.text.LastIndexOf('\n', this.cursorIndex - 2) + 1);
			}
			else
			{
				this.selectIndex = (this.cursorIndex = 0);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00010A18 File Offset: 0x0000EC18
		public void MoveCursorToPosition(Vector2 cursorPosition)
		{
			this.MoveCursorToPosition_Internal(cursorPosition, Event.current.shift);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00010A30 File Offset: 0x0000EC30
		protected internal void MoveCursorToPosition_Internal(Vector2 cursorPosition, bool shift)
		{
			this.selectIndex = this.style.GetCursorStringIndex(this.localPosition, this.m_Content, cursorPosition + this.scrollOffset);
			bool flag = !shift;
			if (flag)
			{
				this.cursorIndex = this.selectIndex;
			}
			this.DetectFocusChange();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00010A88 File Offset: 0x0000EC88
		public void MoveAltCursorToPosition(Vector2 cursorPosition)
		{
			int cursorStringIndex = this.style.GetCursorStringIndex(this.localPosition, this.m_Content, cursorPosition + this.scrollOffset);
			this.m_iAltCursorPos = Mathf.Min(this.text.Length, cursorStringIndex);
			this.DetectFocusChange();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		public bool IsOverSelection(Vector2 cursorPosition)
		{
			int cursorStringIndex = this.style.GetCursorStringIndex(this.localPosition, this.m_Content, cursorPosition + this.scrollOffset);
			return cursorStringIndex < Mathf.Max(this.cursorIndex, this.selectIndex) && cursorStringIndex > Mathf.Min(this.cursorIndex, this.selectIndex);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00010B3C File Offset: 0x0000ED3C
		public void SelectToPosition(Vector2 cursorPosition)
		{
			bool flag = !this.m_MouseDragSelectsWholeWords;
			if (flag)
			{
				this.cursorIndex = this.style.GetCursorStringIndex(this.localPosition, this.m_Content, cursorPosition + this.scrollOffset);
			}
			else
			{
				int cursorStringIndex = this.style.GetCursorStringIndex(this.localPosition, this.m_Content, cursorPosition + this.scrollOffset);
				this.EnsureValidCodePointIndex(ref cursorStringIndex);
				this.EnsureValidCodePointIndex(ref this.m_DblClickInitPos);
				bool flag2 = this.m_DblClickSnap == TextEditor.DblClickSnapping.WORDS;
				if (flag2)
				{
					bool flag3 = cursorStringIndex < this.m_DblClickInitPos;
					if (flag3)
					{
						this.cursorIndex = this.FindEndOfClassification(cursorStringIndex, TextEditor.Direction.Backward);
						this.selectIndex = this.FindEndOfClassification(this.m_DblClickInitPos, TextEditor.Direction.Forward);
					}
					else
					{
						this.cursorIndex = this.FindEndOfClassification(cursorStringIndex, TextEditor.Direction.Forward);
						this.selectIndex = this.FindEndOfClassification(this.m_DblClickInitPos, TextEditor.Direction.Backward);
					}
				}
				else
				{
					bool flag4 = cursorStringIndex < this.m_DblClickInitPos;
					if (flag4)
					{
						bool flag5 = cursorStringIndex > 0;
						if (flag5)
						{
							this.cursorIndex = this.text.LastIndexOf('\n', Mathf.Max(0, cursorStringIndex - 2)) + 1;
						}
						else
						{
							this.cursorIndex = 0;
						}
						this.selectIndex = this.text.LastIndexOf('\n', Mathf.Min(this.text.Length - 1, this.m_DblClickInitPos));
					}
					else
					{
						bool flag6 = cursorStringIndex < this.text.Length;
						if (flag6)
						{
							this.cursorIndex = this.IndexOfEndOfLine(cursorStringIndex);
						}
						else
						{
							this.cursorIndex = this.text.Length;
						}
						this.selectIndex = this.text.LastIndexOf('\n', Mathf.Max(0, this.m_DblClickInitPos - 2)) + 1;
					}
				}
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00010D04 File Offset: 0x0000EF04
		public void SelectLeft()
		{
			bool bJustSelected = this.m_bJustSelected;
			if (bJustSelected)
			{
				bool flag = this.cursorIndex > this.selectIndex;
				if (flag)
				{
					int cursorIndex = this.cursorIndex;
					this.cursorIndex = this.selectIndex;
					this.selectIndex = cursorIndex;
				}
			}
			this.m_bJustSelected = false;
			this.cursorIndex = this.PreviousCodePointIndex(this.cursorIndex);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00010D68 File Offset: 0x0000EF68
		public void SelectRight()
		{
			bool bJustSelected = this.m_bJustSelected;
			if (bJustSelected)
			{
				bool flag = this.cursorIndex < this.selectIndex;
				if (flag)
				{
					int cursorIndex = this.cursorIndex;
					this.cursorIndex = this.selectIndex;
					this.selectIndex = cursorIndex;
				}
			}
			this.m_bJustSelected = false;
			this.cursorIndex = this.NextCodePointIndex(this.cursorIndex);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00010DCC File Offset: 0x0000EFCC
		public void SelectUp()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			this.cursorIndex = this.style.GetCursorStringIndex(this.localPosition, this.m_Content, this.graphicalCursorPos);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00010E1C File Offset: 0x0000F01C
		public void SelectDown()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			this.cursorIndex = this.style.GetCursorStringIndex(this.localPosition, this.m_Content, this.graphicalCursorPos);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00010E75 File Offset: 0x0000F075
		public void SelectTextEnd()
		{
			this.cursorIndex = this.text.Length;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00010E8A File Offset: 0x0000F08A
		public void SelectTextStart()
		{
			this.cursorIndex = 0;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00010E95 File Offset: 0x0000F095
		public void MouseDragSelectsWholeWords(bool on)
		{
			this.m_MouseDragSelectsWholeWords = on;
			this.m_DblClickInitPos = this.cursorIndex;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		public void DblClickSnap(TextEditor.DblClickSnapping snapping)
		{
			this.m_DblClickSnap = snapping;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00010EAC File Offset: 0x0000F0AC
		private int GetGraphicalLineStart(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.localPosition, this.m_Content, p);
			cursorPixelPosition.y += 1f / GUIUtility.pixelsPerPoint;
			cursorPixelPosition.x = this.localPosition.x;
			return this.style.GetCursorStringIndex(this.localPosition, this.m_Content, cursorPixelPosition);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00010F1C File Offset: 0x0000F11C
		private int GetGraphicalLineEnd(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.localPosition, this.m_Content, p);
			cursorPixelPosition.y += 1f / GUIUtility.pixelsPerPoint;
			cursorPixelPosition.x += 5000f;
			return this.style.GetCursorStringIndex(this.localPosition, this.m_Content, cursorPixelPosition);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00010F88 File Offset: 0x0000F188
		private int FindNextSeperator(int startPos)
		{
			int length = this.text.Length;
			while (startPos < length && this.ClassifyChar(startPos) > TextEditor.CharacterType.LetterLike)
			{
				startPos = this.NextCodePointIndex(startPos);
			}
			while (startPos < length && this.ClassifyChar(startPos) == TextEditor.CharacterType.LetterLike)
			{
				startPos = this.NextCodePointIndex(startPos);
			}
			return startPos;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00010FE8 File Offset: 0x0000F1E8
		private int FindPrevSeperator(int startPos)
		{
			startPos = this.PreviousCodePointIndex(startPos);
			while (startPos > 0 && this.ClassifyChar(startPos) > TextEditor.CharacterType.LetterLike)
			{
				startPos = this.PreviousCodePointIndex(startPos);
			}
			bool flag = startPos == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				while (startPos > 0 && this.ClassifyChar(startPos) == TextEditor.CharacterType.LetterLike)
				{
					startPos = this.PreviousCodePointIndex(startPos);
				}
				bool flag2 = this.ClassifyChar(startPos) == TextEditor.CharacterType.LetterLike;
				if (flag2)
				{
					num = startPos;
				}
				else
				{
					num = this.NextCodePointIndex(startPos);
				}
			}
			return num;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001106C File Offset: 0x0000F26C
		public void MoveWordRight()
		{
			this.cursorIndex = ((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex);
			this.cursorIndex = (this.selectIndex = this.FindNextSeperator(this.cursorIndex));
			this.ClearCursorPos();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public void MoveToStartOfNextWord()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex != this.selectIndex;
			if (flag)
			{
				this.MoveRight();
			}
			else
			{
				this.cursorIndex = (this.selectIndex = this.FindStartOfNextWord(this.cursorIndex));
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00011118 File Offset: 0x0000F318
		public void MoveToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex != this.selectIndex;
			if (flag)
			{
				this.MoveLeft();
			}
			else
			{
				this.cursorIndex = (this.selectIndex = this.FindEndOfPreviousWord(this.cursorIndex));
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00011169 File Offset: 0x0000F369
		public void SelectToStartOfNextWord()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.FindStartOfNextWord(this.cursorIndex);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00011186 File Offset: 0x0000F386
		public void SelectToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.FindEndOfPreviousWord(this.cursorIndex);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000111A4 File Offset: 0x0000F3A4
		private TextEditor.CharacterType ClassifyChar(int index)
		{
			bool flag = char.IsWhiteSpace(this.text, index);
			TextEditor.CharacterType characterType;
			if (flag)
			{
				characterType = TextEditor.CharacterType.WhiteSpace;
			}
			else
			{
				bool flag2 = char.IsLetterOrDigit(this.text, index) || this.text.get_Chars(index) == '\'';
				if (flag2)
				{
					characterType = TextEditor.CharacterType.LetterLike;
				}
				else
				{
					characterType = TextEditor.CharacterType.Symbol;
				}
			}
			return characterType;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000111F4 File Offset: 0x0000F3F4
		public int FindStartOfNextWord(int p)
		{
			int length = this.text.Length;
			bool flag = p == length;
			int num;
			if (flag)
			{
				num = p;
			}
			else
			{
				TextEditor.CharacterType characterType = this.ClassifyChar(p);
				bool flag2 = characterType != TextEditor.CharacterType.WhiteSpace;
				if (flag2)
				{
					p = this.NextCodePointIndex(p);
					while (p < length && this.ClassifyChar(p) == characterType)
					{
						p = this.NextCodePointIndex(p);
					}
				}
				else
				{
					bool flag3 = this.text.get_Chars(p) == '\t' || this.text.get_Chars(p) == '\n';
					if (flag3)
					{
						return this.NextCodePointIndex(p);
					}
				}
				bool flag4 = p == length;
				if (flag4)
				{
					num = p;
				}
				else
				{
					bool flag5 = this.text.get_Chars(p) == ' ';
					if (flag5)
					{
						while (p < length && this.ClassifyChar(p) == TextEditor.CharacterType.WhiteSpace)
						{
							p = this.NextCodePointIndex(p);
						}
					}
					else
					{
						bool flag6 = this.text.get_Chars(p) == '\t' || this.text.get_Chars(p) == '\n';
						if (flag6)
						{
							return p;
						}
					}
					num = p;
				}
			}
			return num;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00011314 File Offset: 0x0000F514
		private int FindEndOfPreviousWord(int p)
		{
			bool flag = p == 0;
			int num;
			if (flag)
			{
				num = p;
			}
			else
			{
				p = this.PreviousCodePointIndex(p);
				while (p > 0 && this.text.get_Chars(p) == ' ')
				{
					p = this.PreviousCodePointIndex(p);
				}
				TextEditor.CharacterType characterType = this.ClassifyChar(p);
				bool flag2 = characterType != TextEditor.CharacterType.WhiteSpace;
				if (flag2)
				{
					while (p > 0 && this.ClassifyChar(this.PreviousCodePointIndex(p)) == characterType)
					{
						p = this.PreviousCodePointIndex(p);
					}
				}
				num = p;
			}
			return num;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000113A4 File Offset: 0x0000F5A4
		public void MoveWordLeft()
		{
			this.cursorIndex = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
			this.cursorIndex = this.FindPrevSeperator(this.cursorIndex);
			this.selectIndex = this.cursorIndex;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000113F8 File Offset: 0x0000F5F8
		public void SelectWordRight()
		{
			this.ClearCursorPos();
			int selectIndex = this.selectIndex;
			bool flag = this.cursorIndex < this.selectIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordRight();
				this.selectIndex = selectIndex;
				this.cursorIndex = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
			}
			else
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordRight();
				this.selectIndex = selectIndex;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00011484 File Offset: 0x0000F684
		public void SelectWordLeft()
		{
			this.ClearCursorPos();
			int selectIndex = this.selectIndex;
			bool flag = this.cursorIndex > this.selectIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordLeft();
				this.selectIndex = selectIndex;
				this.cursorIndex = ((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex);
			}
			else
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordLeft();
				this.selectIndex = selectIndex;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00011510 File Offset: 0x0000F710
		public void ExpandSelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex < this.selectIndex;
			if (flag)
			{
				this.cursorIndex = this.GetGraphicalLineStart(this.cursorIndex);
			}
			else
			{
				int cursorIndex = this.cursorIndex;
				this.cursorIndex = this.GetGraphicalLineStart(this.selectIndex);
				this.selectIndex = cursorIndex;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00011570 File Offset: 0x0000F770
		public void ExpandSelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex > this.selectIndex;
			if (flag)
			{
				this.cursorIndex = this.GetGraphicalLineEnd(this.cursorIndex);
			}
			else
			{
				int cursorIndex = this.cursorIndex;
				this.cursorIndex = this.GetGraphicalLineEnd(this.selectIndex);
				this.selectIndex = cursorIndex;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000115D0 File Offset: 0x0000F7D0
		public void SelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.GetGraphicalLineStart(this.cursorIndex);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000115ED File Offset: 0x0000F7ED
		public void SelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.GetGraphicalLineEnd(this.cursorIndex);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001160C File Offset: 0x0000F80C
		public void SelectParagraphForward()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex < this.selectIndex;
			bool flag2 = this.cursorIndex < this.text.Length;
			if (flag2)
			{
				this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex + 1);
				bool flag3 = flag && this.cursorIndex > this.selectIndex;
				if (flag3)
				{
					this.cursorIndex = this.selectIndex;
				}
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00011684 File Offset: 0x0000F884
		public void SelectParagraphBackward()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex > this.selectIndex;
			bool flag2 = this.cursorIndex > 1;
			if (flag2)
			{
				this.cursorIndex = this.text.LastIndexOf('\n', this.cursorIndex - 2) + 1;
				bool flag3 = flag && this.cursorIndex < this.selectIndex;
				if (flag3)
				{
					this.cursorIndex = this.selectIndex;
				}
			}
			else
			{
				this.selectIndex = (this.cursorIndex = 0);
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00011710 File Offset: 0x0000F910
		public void SelectCurrentWord()
		{
			int cursorIndex = this.cursorIndex;
			bool flag = this.cursorIndex < this.selectIndex;
			if (flag)
			{
				this.cursorIndex = this.FindEndOfClassification(cursorIndex, TextEditor.Direction.Backward);
				this.selectIndex = this.FindEndOfClassification(cursorIndex, TextEditor.Direction.Forward);
			}
			else
			{
				this.cursorIndex = this.FindEndOfClassification(cursorIndex, TextEditor.Direction.Forward);
				this.selectIndex = this.FindEndOfClassification(cursorIndex, TextEditor.Direction.Backward);
			}
			this.ClearCursorPos();
			this.m_bJustSelected = true;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00011788 File Offset: 0x0000F988
		private int FindEndOfClassification(int p, TextEditor.Direction dir)
		{
			bool flag = this.text.Length == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = p == this.text.Length;
				if (flag2)
				{
					p = this.PreviousCodePointIndex(p);
				}
				TextEditor.CharacterType characterType = this.ClassifyChar(p);
				for (;;)
				{
					if (dir != TextEditor.Direction.Forward)
					{
						if (dir == TextEditor.Direction.Backward)
						{
							p = this.PreviousCodePointIndex(p);
							bool flag3 = p == 0;
							if (flag3)
							{
								break;
							}
						}
					}
					else
					{
						p = this.NextCodePointIndex(p);
						bool flag4 = p == this.text.Length;
						if (flag4)
						{
							goto Block_7;
						}
					}
					if (this.ClassifyChar(p) != characterType)
					{
						goto Block_8;
					}
				}
				return (this.ClassifyChar(0) == characterType) ? 0 : this.NextCodePointIndex(0);
				Block_7:
				return this.text.Length;
				Block_8:
				bool flag5 = dir == TextEditor.Direction.Forward;
				if (flag5)
				{
					num = p;
				}
				else
				{
					num = this.NextCodePointIndex(p);
				}
			}
			return num;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00011868 File Offset: 0x0000FA68
		public void SelectCurrentParagraph()
		{
			this.ClearCursorPos();
			int length = this.text.Length;
			bool flag = this.cursorIndex < length;
			if (flag)
			{
				this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex) + 1;
			}
			bool flag2 = this.selectIndex != 0;
			if (flag2)
			{
				this.selectIndex = this.text.LastIndexOf('\n', this.selectIndex - 1) + 1;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000118D8 File Offset: 0x0000FAD8
		public void UpdateScrollOffsetIfNeeded(Event evt)
		{
			bool flag = evt.type != EventType.Repaint && evt.type != EventType.Layout;
			if (flag)
			{
				this.UpdateScrollOffset();
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001190C File Offset: 0x0000FB0C
		[VisibleToOtherModules]
		internal void UpdateScrollOffset()
		{
			int cursorIndex = this.cursorIndex;
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.m_Content, cursorIndex);
			Rect rect = this.style.padding.Remove(this.position);
			Vector2 vector = this.graphicalCursorPos;
			vector.x -= (float)this.style.padding.left;
			vector.y -= (float)this.style.padding.top;
			Vector2 vector2 = new Vector2(this.style.CalcSize(this.m_Content).x, this.style.CalcHeight(this.m_Content, this.position.width));
			vector2.x -= (float)(this.style.padding.left + this.style.padding.right);
			vector2.y -= (float)(this.style.padding.top + this.style.padding.bottom);
			bool flag = vector2.x < rect.width;
			if (flag)
			{
				this.scrollOffset.x = 0f;
			}
			else
			{
				bool revealCursor = this.m_RevealCursor;
				if (revealCursor)
				{
					bool flag2 = vector.x + 1f > this.scrollOffset.x + rect.width;
					if (flag2)
					{
						this.scrollOffset.x = vector.x - rect.width + 1f;
					}
					bool flag3 = vector.x < this.scrollOffset.x;
					if (flag3)
					{
						this.scrollOffset.x = vector.x;
					}
				}
			}
			bool flag4 = vector2.y < rect.height;
			if (flag4)
			{
				this.scrollOffset.y = 0f;
			}
			else
			{
				bool revealCursor2 = this.m_RevealCursor;
				if (revealCursor2)
				{
					bool flag5 = vector.y + this.style.lineHeight > this.scrollOffset.y + rect.height;
					if (flag5)
					{
						this.scrollOffset.y = vector.y - rect.height + this.style.lineHeight;
					}
					bool flag6 = vector.y < this.scrollOffset.y;
					if (flag6)
					{
						this.scrollOffset.y = vector.y;
					}
				}
			}
			bool flag7 = this.scrollOffset.y > 0f && vector2.y - this.scrollOffset.y < rect.height;
			if (flag7)
			{
				this.scrollOffset.y = vector2.y - rect.height;
			}
			this.scrollOffset.y = ((this.scrollOffset.y < 0f) ? 0f : this.scrollOffset.y);
			this.m_RevealCursor = false;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00011C38 File Offset: 0x0000FE38
		public void DrawCursor(string newText)
		{
			string text = this.text;
			int num = this.cursorIndex;
			bool flag = GUIUtility.compositionString.Length > 0;
			if (flag)
			{
				this.m_Content.text = newText.Substring(0, this.cursorIndex) + GUIUtility.compositionString + newText.Substring(this.selectIndex);
				num += GUIUtility.compositionString.Length;
			}
			else
			{
				this.m_Content.text = newText;
			}
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.m_Content, num);
			Vector2 contentOffset = this.style.contentOffset;
			this.style.contentOffset -= this.scrollOffset;
			this.style.Internal_clipOffset = this.scrollOffset;
			GUIUtility.compositionCursorPos = GUIClip.UnclipToWindow(this.graphicalCursorPos + new Vector2(this.position.x, this.position.y + this.style.lineHeight) - this.scrollOffset);
			bool flag2 = GUIUtility.compositionString.Length > 0;
			if (flag2)
			{
				this.style.DrawWithTextSelection(this.position, this.m_Content, this.controlID, this.cursorIndex, this.cursorIndex + GUIUtility.compositionString.Length, true);
			}
			else
			{
				this.style.DrawWithTextSelection(this.position, this.m_Content, this.controlID, this.cursorIndex, this.selectIndex);
			}
			bool flag3 = this.m_iAltCursorPos != -1;
			if (flag3)
			{
				this.style.DrawCursor(this.position, this.m_Content, this.controlID, this.m_iAltCursorPos);
			}
			this.style.contentOffset = contentOffset;
			this.style.Internal_clipOffset = Vector2.zero;
			this.m_Content.text = text;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00011E58 File Offset: 0x00010058
		private bool PerformOperation(TextEditor.TextEditOp operation, bool textIsReadOnly)
		{
			this.m_RevealCursor = true;
			switch (operation)
			{
			case TextEditor.TextEditOp.MoveLeft:
				this.MoveLeft();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveRight:
				this.MoveRight();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveUp:
				this.MoveUp();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveDown:
				this.MoveDown();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveLineStart:
				this.MoveLineStart();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveLineEnd:
				this.MoveLineEnd();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveTextStart:
				this.MoveTextStart();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveTextEnd:
				this.MoveTextEnd();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveGraphicalLineStart:
				this.MoveGraphicalLineStart();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveGraphicalLineEnd:
				this.MoveGraphicalLineEnd();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveWordLeft:
				this.MoveWordLeft();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveWordRight:
				this.MoveWordRight();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveParagraphForward:
				this.MoveParagraphForward();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveParagraphBackward:
				this.MoveParagraphBackward();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveToStartOfNextWord:
				this.MoveToStartOfNextWord();
				goto IL_031A;
			case TextEditor.TextEditOp.MoveToEndOfPreviousWord:
				this.MoveToEndOfPreviousWord();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectLeft:
				this.SelectLeft();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectRight:
				this.SelectRight();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectUp:
				this.SelectUp();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectDown:
				this.SelectDown();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectTextStart:
				this.SelectTextStart();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectTextEnd:
				this.SelectTextEnd();
				goto IL_031A;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineStart:
				this.ExpandSelectGraphicalLineStart();
				goto IL_031A;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd:
				this.ExpandSelectGraphicalLineEnd();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectGraphicalLineStart:
				this.SelectGraphicalLineStart();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectGraphicalLineEnd:
				this.SelectGraphicalLineEnd();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectWordLeft:
				this.SelectWordLeft();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectWordRight:
				this.SelectWordRight();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectToEndOfPreviousWord:
				this.SelectToEndOfPreviousWord();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectToStartOfNextWord:
				this.SelectToStartOfNextWord();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectParagraphBackward:
				this.SelectParagraphBackward();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectParagraphForward:
				this.SelectParagraphForward();
				goto IL_031A;
			case TextEditor.TextEditOp.Delete:
				return !textIsReadOnly && this.Delete();
			case TextEditor.TextEditOp.Backspace:
				return !textIsReadOnly && this.Backspace();
			case TextEditor.TextEditOp.DeleteWordBack:
				return !textIsReadOnly && this.DeleteWordBack();
			case TextEditor.TextEditOp.DeleteWordForward:
				return !textIsReadOnly && this.DeleteWordForward();
			case TextEditor.TextEditOp.DeleteLineBack:
				return !textIsReadOnly && this.DeleteLineBack();
			case TextEditor.TextEditOp.Cut:
				return !textIsReadOnly && this.Cut();
			case TextEditor.TextEditOp.Copy:
				this.Copy();
				goto IL_031A;
			case TextEditor.TextEditOp.Paste:
				return !textIsReadOnly && this.Paste();
			case TextEditor.TextEditOp.SelectAll:
				this.SelectAll();
				goto IL_031A;
			case TextEditor.TextEditOp.SelectNone:
				this.SelectNone();
				goto IL_031A;
			}
			Debug.Log("Unimplemented: " + operation);
			IL_031A:
			return false;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00012184 File Offset: 0x00010384
		public void SaveBackup()
		{
			this.oldText = this.text;
			this.oldPos = this.cursorIndex;
			this.oldSelectPos = this.selectIndex;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000121AB File Offset: 0x000103AB
		public void Undo()
		{
			this.m_Content.text = this.oldText;
			this.cursorIndex = this.oldPos;
			this.selectIndex = this.oldSelectPos;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000121DC File Offset: 0x000103DC
		public bool Cut()
		{
			bool flag = this.isPasswordField;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.Copy();
				flag2 = this.DeleteSelection();
			}
			return flag2;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001220C File Offset: 0x0001040C
		public void Copy()
		{
			bool flag = this.selectIndex == this.cursorIndex;
			if (!flag)
			{
				bool flag2 = this.isPasswordField;
				if (!flag2)
				{
					string text = this.style.Internal_GetSelectedRenderedText(this.localPosition, this.m_Content, this.selectIndex, this.cursorIndex);
					GUIUtility.systemCopyBuffer = text;
				}
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00012268 File Offset: 0x00010468
		internal Rect[] GetHyperlinksRect()
		{
			return this.style.Internal_GetHyperlinksRect(this.localPosition, this.m_Content);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00012294 File Offset: 0x00010494
		private static string ReplaceNewlinesWithSpaces(string value)
		{
			value = value.Replace("\r\n", " ");
			value = value.Replace('\n', ' ');
			value = value.Replace('\r', ' ');
			return value;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000122D4 File Offset: 0x000104D4
		public bool Paste()
		{
			string text = GUIUtility.systemCopyBuffer;
			bool flag = text != "";
			bool flag3;
			if (flag)
			{
				bool flag2 = !this.multiline;
				if (flag2)
				{
					text = TextEditor.ReplaceNewlinesWithSpaces(text);
				}
				this.ReplaceSelection(text);
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001231D File Offset: 0x0001051D
		private static void MapKey(string key, TextEditor.TextEditOp action)
		{
			TextEditor.s_Keyactions[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00012334 File Offset: 0x00010534
		private void InitKeyActions()
		{
			bool flag = TextEditor.s_Keyactions != null;
			if (!flag)
			{
				TextEditor.s_Keyactions = new Dictionary<Event, TextEditor.TextEditOp>();
				TextEditor.MapKey("left", TextEditor.TextEditOp.MoveLeft);
				TextEditor.MapKey("right", TextEditor.TextEditOp.MoveRight);
				TextEditor.MapKey("up", TextEditor.TextEditOp.MoveUp);
				TextEditor.MapKey("down", TextEditor.TextEditOp.MoveDown);
				TextEditor.MapKey("#left", TextEditor.TextEditOp.SelectLeft);
				TextEditor.MapKey("#right", TextEditor.TextEditOp.SelectRight);
				TextEditor.MapKey("#up", TextEditor.TextEditOp.SelectUp);
				TextEditor.MapKey("#down", TextEditor.TextEditOp.SelectDown);
				TextEditor.MapKey("delete", TextEditor.TextEditOp.Delete);
				TextEditor.MapKey("backspace", TextEditor.TextEditOp.Backspace);
				TextEditor.MapKey("#backspace", TextEditor.TextEditOp.Backspace);
				bool flag2 = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX;
				if (flag2)
				{
					TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveGraphicalLineStart);
					TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
					TextEditor.MapKey("&left", TextEditor.TextEditOp.MoveWordLeft);
					TextEditor.MapKey("&right", TextEditor.TextEditOp.MoveWordRight);
					TextEditor.MapKey("&up", TextEditor.TextEditOp.MoveParagraphBackward);
					TextEditor.MapKey("&down", TextEditor.TextEditOp.MoveParagraphForward);
					TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveGraphicalLineStart);
					TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
					TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveTextStart);
					TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveTextEnd);
					TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectTextStart);
					TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectTextEnd);
					TextEditor.MapKey("#^left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
					TextEditor.MapKey("#^right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
					TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
					TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
					TextEditor.MapKey("#&left", TextEditor.TextEditOp.SelectWordLeft);
					TextEditor.MapKey("#&right", TextEditor.TextEditOp.SelectWordRight);
					TextEditor.MapKey("#&up", TextEditor.TextEditOp.SelectParagraphBackward);
					TextEditor.MapKey("#&down", TextEditor.TextEditOp.SelectParagraphForward);
					TextEditor.MapKey("#%left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
					TextEditor.MapKey("#%right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
					TextEditor.MapKey("#%up", TextEditor.TextEditOp.SelectTextStart);
					TextEditor.MapKey("#%down", TextEditor.TextEditOp.SelectTextEnd);
					TextEditor.MapKey("%a", TextEditor.TextEditOp.SelectAll);
					TextEditor.MapKey("%x", TextEditor.TextEditOp.Cut);
					TextEditor.MapKey("%c", TextEditor.TextEditOp.Copy);
					TextEditor.MapKey("%v", TextEditor.TextEditOp.Paste);
					TextEditor.MapKey("^d", TextEditor.TextEditOp.Delete);
					TextEditor.MapKey("^h", TextEditor.TextEditOp.Backspace);
					TextEditor.MapKey("^b", TextEditor.TextEditOp.MoveLeft);
					TextEditor.MapKey("^f", TextEditor.TextEditOp.MoveRight);
					TextEditor.MapKey("^a", TextEditor.TextEditOp.MoveLineStart);
					TextEditor.MapKey("^e", TextEditor.TextEditOp.MoveLineEnd);
					TextEditor.MapKey("&delete", TextEditor.TextEditOp.DeleteWordForward);
					TextEditor.MapKey("&backspace", TextEditor.TextEditOp.DeleteWordBack);
					TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
				}
				else
				{
					TextEditor.MapKey("home", TextEditor.TextEditOp.MoveGraphicalLineStart);
					TextEditor.MapKey("end", TextEditor.TextEditOp.MoveGraphicalLineEnd);
					TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveWordLeft);
					TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveWordRight);
					TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveParagraphBackward);
					TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveParagraphForward);
					TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveToEndOfPreviousWord);
					TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveToStartOfNextWord);
					TextEditor.MapKey("^up", TextEditor.TextEditOp.MoveParagraphBackward);
					TextEditor.MapKey("^down", TextEditor.TextEditOp.MoveParagraphForward);
					TextEditor.MapKey("#^left", TextEditor.TextEditOp.SelectToEndOfPreviousWord);
					TextEditor.MapKey("#^right", TextEditor.TextEditOp.SelectToStartOfNextWord);
					TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
					TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
					TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectGraphicalLineStart);
					TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectGraphicalLineEnd);
					TextEditor.MapKey("^delete", TextEditor.TextEditOp.DeleteWordForward);
					TextEditor.MapKey("^backspace", TextEditor.TextEditOp.DeleteWordBack);
					TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
					TextEditor.MapKey("^a", TextEditor.TextEditOp.SelectAll);
					TextEditor.MapKey("^x", TextEditor.TextEditOp.Cut);
					TextEditor.MapKey("^c", TextEditor.TextEditOp.Copy);
					TextEditor.MapKey("^v", TextEditor.TextEditOp.Paste);
					TextEditor.MapKey("#delete", TextEditor.TextEditOp.Cut);
					TextEditor.MapKey("^insert", TextEditor.TextEditOp.Copy);
					TextEditor.MapKey("#insert", TextEditor.TextEditOp.Paste);
				}
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001272D File Offset: 0x0001092D
		public void DetectFocusChange()
		{
			this.OnDetectFocusChange();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00012738 File Offset: 0x00010938
		internal virtual void OnDetectFocusChange()
		{
			bool flag = this.m_HasFocus && this.controlID != GUIUtility.keyboardControl;
			if (flag)
			{
				this.OnLostFocus();
			}
			bool flag2 = !this.m_HasFocus && this.controlID == GUIUtility.keyboardControl;
			if (flag2)
			{
				this.OnFocus();
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00002201 File Offset: 0x00000401
		internal virtual void OnCursorIndexChange()
		{
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00002201 File Offset: 0x00000401
		internal virtual void OnSelectIndexChange()
		{
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001278F File Offset: 0x0001098F
		private void ClampTextIndex(ref int index)
		{
			index = Mathf.Clamp(index, 0, this.text.Length);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000127A8 File Offset: 0x000109A8
		private void EnsureValidCodePointIndex(ref int index)
		{
			this.ClampTextIndex(ref index);
			bool flag = !this.IsValidCodePointIndex(index);
			if (flag)
			{
				index = this.NextCodePointIndex(index);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000127D8 File Offset: 0x000109D8
		private bool IsValidCodePointIndex(int index)
		{
			bool flag = index < 0 || index > this.text.Length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = index == 0 || index == this.text.Length;
				flag2 = flag3 || !char.IsLowSurrogate(this.text.get_Chars(index));
			}
			return flag2;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00012838 File Offset: 0x00010A38
		private int PreviousCodePointIndex(int index)
		{
			bool flag = index > 0;
			if (flag)
			{
				index--;
			}
			while (index > 0 && char.IsLowSurrogate(this.text.get_Chars(index)))
			{
				index--;
			}
			return index;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001287C File Offset: 0x00010A7C
		private int NextCodePointIndex(int index)
		{
			bool flag = index < this.text.Length;
			if (flag)
			{
				index++;
			}
			while (index < this.text.Length && char.IsLowSurrogate(this.text.get_Chars(index)))
			{
				index++;
			}
			return index;
		}

		// Token: 0x04000136 RID: 310
		public TouchScreenKeyboard keyboardOnScreen = null;

		// Token: 0x04000137 RID: 311
		public int controlID = 0;

		// Token: 0x04000138 RID: 312
		public GUIStyle style = GUIStyle.none;

		// Token: 0x04000139 RID: 313
		public bool multiline = false;

		// Token: 0x0400013A RID: 314
		public bool hasHorizontalCursorPos = false;

		// Token: 0x0400013B RID: 315
		public bool isPasswordField = false;

		// Token: 0x0400013C RID: 316
		internal bool m_HasFocus;

		// Token: 0x0400013D RID: 317
		public Vector2 scrollOffset = Vector2.zero;

		// Token: 0x0400013E RID: 318
		private GUIContent m_Content = new GUIContent();

		// Token: 0x0400013F RID: 319
		private Rect m_Position;

		// Token: 0x04000140 RID: 320
		private int m_CursorIndex = 0;

		// Token: 0x04000141 RID: 321
		private int m_SelectIndex = 0;

		// Token: 0x04000142 RID: 322
		private bool m_RevealCursor = false;

		// Token: 0x04000143 RID: 323
		public Vector2 graphicalCursorPos;

		// Token: 0x04000144 RID: 324
		public Vector2 graphicalSelectCursorPos;

		// Token: 0x04000145 RID: 325
		private bool m_MouseDragSelectsWholeWords = false;

		// Token: 0x04000146 RID: 326
		private int m_DblClickInitPos = 0;

		// Token: 0x04000147 RID: 327
		private TextEditor.DblClickSnapping m_DblClickSnap = TextEditor.DblClickSnapping.WORDS;

		// Token: 0x04000148 RID: 328
		private bool m_bJustSelected = false;

		// Token: 0x04000149 RID: 329
		private int m_iAltCursorPos = -1;

		// Token: 0x0400014A RID: 330
		private string oldText;

		// Token: 0x0400014B RID: 331
		private int oldPos;

		// Token: 0x0400014C RID: 332
		private int oldSelectPos;

		// Token: 0x0400014D RID: 333
		private static Dictionary<Event, TextEditor.TextEditOp> s_Keyactions;

		// Token: 0x0200003B RID: 59
		public enum DblClickSnapping : byte
		{
			// Token: 0x0400014F RID: 335
			WORDS,
			// Token: 0x04000150 RID: 336
			PARAGRAPHS
		}

		// Token: 0x0200003C RID: 60
		private enum CharacterType
		{
			// Token: 0x04000152 RID: 338
			LetterLike,
			// Token: 0x04000153 RID: 339
			Symbol,
			// Token: 0x04000154 RID: 340
			Symbol2,
			// Token: 0x04000155 RID: 341
			WhiteSpace
		}

		// Token: 0x0200003D RID: 61
		private enum Direction
		{
			// Token: 0x04000157 RID: 343
			Forward,
			// Token: 0x04000158 RID: 344
			Backward
		}

		// Token: 0x0200003E RID: 62
		private enum TextEditOp
		{
			// Token: 0x0400015A RID: 346
			MoveLeft,
			// Token: 0x0400015B RID: 347
			MoveRight,
			// Token: 0x0400015C RID: 348
			MoveUp,
			// Token: 0x0400015D RID: 349
			MoveDown,
			// Token: 0x0400015E RID: 350
			MoveLineStart,
			// Token: 0x0400015F RID: 351
			MoveLineEnd,
			// Token: 0x04000160 RID: 352
			MoveTextStart,
			// Token: 0x04000161 RID: 353
			MoveTextEnd,
			// Token: 0x04000162 RID: 354
			MovePageUp,
			// Token: 0x04000163 RID: 355
			MovePageDown,
			// Token: 0x04000164 RID: 356
			MoveGraphicalLineStart,
			// Token: 0x04000165 RID: 357
			MoveGraphicalLineEnd,
			// Token: 0x04000166 RID: 358
			MoveWordLeft,
			// Token: 0x04000167 RID: 359
			MoveWordRight,
			// Token: 0x04000168 RID: 360
			MoveParagraphForward,
			// Token: 0x04000169 RID: 361
			MoveParagraphBackward,
			// Token: 0x0400016A RID: 362
			MoveToStartOfNextWord,
			// Token: 0x0400016B RID: 363
			MoveToEndOfPreviousWord,
			// Token: 0x0400016C RID: 364
			SelectLeft,
			// Token: 0x0400016D RID: 365
			SelectRight,
			// Token: 0x0400016E RID: 366
			SelectUp,
			// Token: 0x0400016F RID: 367
			SelectDown,
			// Token: 0x04000170 RID: 368
			SelectTextStart,
			// Token: 0x04000171 RID: 369
			SelectTextEnd,
			// Token: 0x04000172 RID: 370
			SelectPageUp,
			// Token: 0x04000173 RID: 371
			SelectPageDown,
			// Token: 0x04000174 RID: 372
			ExpandSelectGraphicalLineStart,
			// Token: 0x04000175 RID: 373
			ExpandSelectGraphicalLineEnd,
			// Token: 0x04000176 RID: 374
			SelectGraphicalLineStart,
			// Token: 0x04000177 RID: 375
			SelectGraphicalLineEnd,
			// Token: 0x04000178 RID: 376
			SelectWordLeft,
			// Token: 0x04000179 RID: 377
			SelectWordRight,
			// Token: 0x0400017A RID: 378
			SelectToEndOfPreviousWord,
			// Token: 0x0400017B RID: 379
			SelectToStartOfNextWord,
			// Token: 0x0400017C RID: 380
			SelectParagraphBackward,
			// Token: 0x0400017D RID: 381
			SelectParagraphForward,
			// Token: 0x0400017E RID: 382
			Delete,
			// Token: 0x0400017F RID: 383
			Backspace,
			// Token: 0x04000180 RID: 384
			DeleteWordBack,
			// Token: 0x04000181 RID: 385
			DeleteWordForward,
			// Token: 0x04000182 RID: 386
			DeleteLineBack,
			// Token: 0x04000183 RID: 387
			Cut,
			// Token: 0x04000184 RID: 388
			Copy,
			// Token: 0x04000185 RID: 389
			Paste,
			// Token: 0x04000186 RID: 390
			SelectAll,
			// Token: 0x04000187 RID: 391
			SelectNone,
			// Token: 0x04000188 RID: 392
			ScrollStart,
			// Token: 0x04000189 RID: 393
			ScrollEnd,
			// Token: 0x0400018A RID: 394
			ScrollPageUp,
			// Token: 0x0400018B RID: 395
			ScrollPageDown
		}
	}
}

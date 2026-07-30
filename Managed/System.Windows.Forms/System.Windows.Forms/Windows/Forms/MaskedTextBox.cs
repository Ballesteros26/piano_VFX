using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Uses a mask to distinguish between proper and improper user input.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200023F RID: 575
	[ClassInterface(1)]
	[DefaultBindingProperty("Text")]
	[Designer("System.Windows.Forms.Design.MaskedTextBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("MaskInputRejected")]
	[DefaultProperty("Mask")]
	[ComVisible(true)]
	public class MaskedTextBox : TextBoxBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskedTextBox" /> class using defaults.</summary>
		// Token: 0x06002558 RID: 9560 RVA: 0x0008CFA4 File Offset: 0x0008B1A4
		public MaskedTextBox()
		{
			this.provider = new MaskedTextProvider("<>", CultureInfo.CurrentCulture);
			this.is_empty_mask = true;
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskedTextBox" /> class using the specified custom mask language provider.</summary>
		/// <param name="maskedTextProvider">A custom mask language provider, derived from the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="maskedTextProvider" /> is null.</exception>
		// Token: 0x06002559 RID: 9561 RVA: 0x0008CFDC File Offset: 0x0008B1DC
		public MaskedTextBox(MaskedTextProvider maskedTextProvider)
		{
			if (maskedTextProvider == null)
			{
				throw new ArgumentNullException();
			}
			this.provider = maskedTextProvider;
			this.is_empty_mask = false;
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskedTextBox" /> class using the specified input mask.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> representing the input mask. The initial value of the <see cref="P:System.Windows.Forms.MaskedTextBox.Mask" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mask" /> is null.</exception>
		// Token: 0x0600255A RID: 9562 RVA: 0x0008D010 File Offset: 0x0008B210
		public MaskedTextBox(string mask)
		{
			if (mask == null)
			{
				throw new ArgumentNullException();
			}
			this.provider = new MaskedTextProvider(mask, CultureInfo.CurrentCulture);
			this.is_empty_mask = false;
			this.Init();
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0008D050 File Offset: 0x0008B250
		// Note: this type is marked as 'beforefieldinit'.
		static MaskedTextBox()
		{
			MaskedTextBox.AcceptsTabChangedEvent = new object();
			MaskedTextBox.IsOverwriteModeChangedEvent = new object();
			MaskedTextBox.MaskChangedEvent = new object();
			MaskedTextBox.MaskInputRejectedEvent = new object();
			MaskedTextBox.MultilineChangedEvent = new object();
			MaskedTextBox.TextAlignChangedEvent = new object();
			MaskedTextBox.TypeValidationCompletedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MaskedTextBox.AcceptsTab" /> property has changed. This event is not raised by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x14000238 RID: 568
		// (add) Token: 0x0600255C RID: 9564 RVA: 0x0008D0A4 File Offset: 0x0008B2A4
		// (remove) Token: 0x0600255D RID: 9565 RVA: 0x0008D0B8 File Offset: 0x0008B2B8
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new event EventHandler AcceptsTabChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.AcceptsTabChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.AcceptsTabChangedEvent, value);
			}
		}

		/// <summary>Occurs after the insert mode has changed. </summary>
		// Token: 0x14000239 RID: 569
		// (add) Token: 0x0600255E RID: 9566 RVA: 0x0008D0CC File Offset: 0x0008B2CC
		// (remove) Token: 0x0600255F RID: 9567 RVA: 0x0008D0E0 File Offset: 0x0008B2E0
		public event EventHandler IsOverwriteModeChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.IsOverwriteModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.IsOverwriteModeChangedEvent, value);
			}
		}

		/// <summary>Occurs after the input mask is changed.</summary>
		// Token: 0x1400023A RID: 570
		// (add) Token: 0x06002560 RID: 9568 RVA: 0x0008D0F4 File Offset: 0x0008B2F4
		// (remove) Token: 0x06002561 RID: 9569 RVA: 0x0008D108 File Offset: 0x0008B308
		public event EventHandler MaskChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.MaskChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.MaskChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user's input or assigned character does not match the corresponding format element of the input mask.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400023B RID: 571
		// (add) Token: 0x06002562 RID: 9570 RVA: 0x0008D11C File Offset: 0x0008B31C
		// (remove) Token: 0x06002563 RID: 9571 RVA: 0x0008D130 File Offset: 0x0008B330
		public event MaskInputRejectedEventHandler MaskInputRejected
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.MaskInputRejectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.MaskInputRejectedEvent, value);
			}
		}

		/// <summary>Typically occurs when the value of the <see cref="P:System.Windows.Forms.MaskedTextBox.Multiline" /> property has changed; however, this event is not raised by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x1400023C RID: 572
		// (add) Token: 0x06002564 RID: 9572 RVA: 0x0008D144 File Offset: 0x0008B344
		// (remove) Token: 0x06002565 RID: 9573 RVA: 0x0008D158 File Offset: 0x0008B358
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler MultilineChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.MultilineChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.MultilineChangedEvent, value);
			}
		}

		/// <summary>Occurs when the text alignment is changed. </summary>
		// Token: 0x1400023D RID: 573
		// (add) Token: 0x06002566 RID: 9574 RVA: 0x0008D16C File Offset: 0x0008B36C
		// (remove) Token: 0x06002567 RID: 9575 RVA: 0x0008D180 File Offset: 0x0008B380
		public event EventHandler TextAlignChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.TextAlignChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.TextAlignChangedEvent, value);
			}
		}

		/// <summary>Occurs when <see cref="T:System.Windows.Forms.MaskedTextBox" /> has finished parsing the current value using the <see cref="P:System.Windows.Forms.MaskedTextBox.ValidatingType" /> property.</summary>
		// Token: 0x1400023E RID: 574
		// (add) Token: 0x06002568 RID: 9576 RVA: 0x0008D194 File Offset: 0x0008B394
		// (remove) Token: 0x06002569 RID: 9577 RVA: 0x0008D1A8 File Offset: 0x0008B3A8
		public event TypeValidationEventHandler TypeValidationCompleted
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.TypeValidationCompletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.TypeValidationCompletedEvent, value);
			}
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x0008D1BC File Offset: 0x0008B3BC
		private void Init()
		{
			this.BackColor = SystemColors.Window;
			this.cut_copy_mask_format = MaskFormat.IncludeLiterals;
			this.insert_key_overwriting = false;
			this.UpdateVisibleText();
		}

		/// <summary>Clears information about the most recent operation from the undo buffer of the text box. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x0600256B RID: 9579 RVA: 0x0008D1E0 File Offset: 0x0008B3E0
		[EditorBrowsable(1)]
		public new void ClearUndo()
		{
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x0008D1E4 File Offset: 0x0008B3E4
		[EditorBrowsable(2)]
		[PermissionSet(7, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nWindow=\"AllWindows\"/>\n</PermissionSet>\n")]
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <returns>The character at the specified location.</returns>
		/// <param name="pt">The location from which to seek the nearest character. </param>
		// Token: 0x0600256D RID: 9581 RVA: 0x0008D1EC File Offset: 0x0008B3EC
		public override char GetCharFromPosition(Point pt)
		{
			return base.GetCharFromPosition(pt);
		}

		/// <returns>The zero-based character index at the specified location.</returns>
		/// <param name="pt">The location to search. </param>
		// Token: 0x0600256E RID: 9582 RVA: 0x0008D1F8 File Offset: 0x0008B3F8
		public override int GetCharIndexFromPosition(Point pt)
		{
			return base.GetCharIndexFromPosition(pt);
		}

		/// <summary>Retrieves the index of the first character of a given line. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
		/// <returns>This method will always return 0. </returns>
		/// <param name="lineNumber">This parameter is not used.</param>
		// Token: 0x0600256F RID: 9583 RVA: 0x0008D204 File Offset: 0x0008B404
		[EditorBrowsable(1)]
		public new int GetFirstCharIndexFromLine(int lineNumber)
		{
			return 0;
		}

		/// <summary>Retrieves the index of the first character of the current line. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
		/// <returns>This method will always return 0. </returns>
		// Token: 0x06002570 RID: 9584 RVA: 0x0008D208 File Offset: 0x0008B408
		[EditorBrowsable(1)]
		public new int GetFirstCharIndexOfCurrentLine()
		{
			return 0;
		}

		/// <summary>Retrieves the line number from the specified character position within the text of the control. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
		/// <returns>This method will always return 0.</returns>
		/// <param name="index">This parameter is not used.</param>
		// Token: 0x06002571 RID: 9585 RVA: 0x0008D20C File Offset: 0x0008B40C
		[EditorBrowsable(1)]
		public override int GetLineFromCharIndex(int index)
		{
			return 0;
		}

		/// <returns>The location of the specified character within the client rectangle of the control.</returns>
		/// <param name="index">The index of the character for which to retrieve the location. </param>
		// Token: 0x06002572 RID: 9586 RVA: 0x0008D210 File Offset: 0x0008B410
		public override Point GetPositionFromCharIndex(int index)
		{
			return base.GetPositionFromCharIndex(index);
		}

		/// <param name="keyData"></param>
		// Token: 0x06002573 RID: 9587 RVA: 0x0008D21C File Offset: 0x0008B41C
		protected override bool IsInputKey(Keys keyData)
		{
			return base.IsInputKey(keyData);
		}

		/// <param name="e"></param>
		// Token: 0x06002574 RID: 9588 RVA: 0x0008D228 File Offset: 0x0008B428
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0008D234 File Offset: 0x0008B434
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MaskedTextBox.IsOverwriteModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x06002576 RID: 9590 RVA: 0x0008D240 File Offset: 0x0008B440
		[EditorBrowsable(2)]
		protected virtual void OnIsOverwriteModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MaskedTextBox.IsOverwriteModeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x06002577 RID: 9591 RVA: 0x0008D274 File Offset: 0x0008B474
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Insert && this.insert_key_mode == InsertKeyMode.Default)
			{
				this.insert_key_overwriting = !this.insert_key_overwriting;
				this.OnIsOverwriteModeChanged(EventArgs.Empty);
				e.Handled = true;
				return;
			}
			if (e.KeyCode != Keys.Delete || this.is_empty_mask)
			{
				base.OnKeyDown(e);
				return;
			}
			int num = ((this.SelectionLength != 0) ? (base.SelectionStart + this.SelectionLength - 1) : base.SelectionStart);
			int num2;
			MaskedTextResultHint maskedTextResultHint;
			bool flag = this.provider.RemoveAt(base.SelectionStart, num, ref num2, ref maskedTextResultHint);
			this.PostprocessKeyboardInput(flag, num2, num2, maskedTextResultHint);
			e.Handled = true;
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
		// Token: 0x06002578 RID: 9592 RVA: 0x0008D32C File Offset: 0x0008B52C
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (this.is_empty_mask)
			{
				base.OnKeyPress(e);
				return;
			}
			int num;
			MaskedTextResultHint maskedTextResultHint;
			bool flag;
			int num2;
			if (e.KeyChar == '\b')
			{
				if (this.SelectionLength == 0)
				{
					flag = this.provider.RemoveAt(base.SelectionStart - 1, base.SelectionStart - 1, ref num, ref maskedTextResultHint);
				}
				else
				{
					flag = this.provider.RemoveAt(base.SelectionStart, base.SelectionStart + this.SelectionLength - 1, ref num, ref maskedTextResultHint);
				}
				num2 = num;
			}
			else if (this.IsOverwriteMode || this.SelectionLength > 0)
			{
				int num3 = this.provider.FindEditPositionFrom(base.SelectionStart, true);
				int num4 = ((this.SelectionLength <= 0) ? num3 : (base.SelectionStart + this.SelectionLength - 1));
				flag = this.provider.Replace(e.KeyChar, num3, num4, ref num, ref maskedTextResultHint);
				num2 = num + 1;
			}
			else
			{
				flag = this.provider.InsertAt(e.KeyChar, base.SelectionStart, ref num, ref maskedTextResultHint);
				num2 = num + 1;
			}
			this.PostprocessKeyboardInput(flag, num2, num, maskedTextResultHint);
			e.Handled = true;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x0008D458 File Offset: 0x0008B658
		private void PostprocessKeyboardInput(bool result, int newPosition, int testPosition, MaskedTextResultHint resultHint)
		{
			if (!result)
			{
				this.OnMaskInputRejected(new MaskInputRejectedEventArgs(testPosition, resultHint));
			}
			else
			{
				if (newPosition != MaskedTextProvider.InvalidIndex)
				{
					base.SelectionStart = newPosition;
				}
				else
				{
					base.SelectionStart = this.provider.Length;
				}
				this.UpdateVisibleText();
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x0600257A RID: 9594 RVA: 0x0008D4AC File Offset: 0x0008B6AC
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MaskedTextBox.MaskChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600257B RID: 9595 RVA: 0x0008D4B8 File Offset: 0x0008B6B8
		[EditorBrowsable(2)]
		protected virtual void OnMaskChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MaskedTextBox.MaskChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x0008D4EC File Offset: 0x0008B6EC
		private void OnMaskInputRejected(MaskInputRejectedEventArgs e)
		{
			MaskInputRejectedEventHandler maskInputRejectedEventHandler = (MaskInputRejectedEventHandler)base.Events[MaskedTextBox.MaskInputRejectedEvent];
			if (maskInputRejectedEventHandler != null)
			{
				maskInputRejectedEventHandler(this, e);
			}
		}

		/// <summary>Typically raises the <see cref="E:System.Windows.Forms.MaskedTextBox.MultilineChanged" /> event, but disabled for <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600257D RID: 9597 RVA: 0x0008D520 File Offset: 0x0008B720
		[EditorBrowsable(1)]
		protected override void OnMultilineChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MaskedTextBox.MultilineChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MaskedTextBox.TextAlignChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600257E RID: 9598 RVA: 0x0008D554 File Offset: 0x0008B754
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MaskedTextBox.TextAlignChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600257F RID: 9599 RVA: 0x0008D588 File Offset: 0x0008B788
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains event data. </param>
		/// <exception cref="T:System.Exception">A critical exception occurred during the parsing of the input string.</exception>
		// Token: 0x06002580 RID: 9600 RVA: 0x0008D594 File Offset: 0x0008B794
		[EditorBrowsable(2)]
		protected override void OnValidating(CancelEventArgs e)
		{
			base.OnValidating(e);
		}

		/// <returns>true if the command key was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process. </param>
		// Token: 0x06002581 RID: 9601 RVA: 0x0008D5A0 File Offset: 0x0008B7A0
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return base.ProcessCmdKey(ref msg, keyData);
		}

		/// <summary>Overrides the base implementation of this method to handle input language changes.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> object containing a description of the key pressed.</param>
		// Token: 0x06002582 RID: 9602 RVA: 0x0008D5AC File Offset: 0x0008B7AC
		protected internal override bool ProcessKeyMessage(ref Message m)
		{
			return base.ProcessKeyMessage(ref m);
		}

		/// <summary>Scrolls the contents of the control to the current caret position. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x06002583 RID: 9603 RVA: 0x0008D5B8 File Offset: 0x0008B7B8
		[EditorBrowsable(1)]
		public new void ScrollToCaret()
		{
		}

		/// <summary>Returns a string that represents the current masked text box. This method overrides <see cref="M:System.Windows.Forms.TextBoxBase.ToString" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains information about the current <see cref="T:System.Windows.Forms.MaskedTextBox" />. The string includes the type, a simplified view of the input string, and the formatted input string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002584 RID: 9604 RVA: 0x0008D5BC File Offset: 0x0008B7BC
		public override string ToString()
		{
			return base.ToString() + ", Text: " + this.provider.ToString(false, false);
		}

		/// <summary>Undoes the last edit operation in the text box. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x06002585 RID: 9605 RVA: 0x0008D5DC File Offset: 0x0008B7DC
		[EditorBrowsable(1)]
		public new void Undo()
		{
		}

		/// <summary>Converts the user input string to an instance of the validating type.</summary>
		/// <returns>If successful, an <see cref="T:System.Object" /> of the type specified by the <see cref="P:System.Windows.Forms.MaskedTextBox.ValidatingType" /> property; otherwise, null to indicate conversion failure.</returns>
		/// <exception cref="T:System.Exception">A critical exception occurred during the parsing of the input string.</exception>
		// Token: 0x06002586 RID: 9606 RVA: 0x0008D5E0 File Offset: 0x0008B7E0
		public object ValidateText()
		{
			throw new NotImplementedException();
		}

		/// <param name="m">A Windows Message Object. </param>
		// Token: 0x06002587 RID: 9607 RVA: 0x0008D5E8 File Offset: 0x0008B7E8
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			base.WndProc(ref m);
		}

		/// <summary>Gets or sets a value determining how TAB keys are handled for multiline configurations. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x0008D604 File Offset: 0x0008B804
		// (set) Token: 0x06002589 RID: 9609 RVA: 0x0008D608 File Offset: 0x0008B808
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new bool AcceptsTab
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> can be entered as valid data by the user. </summary>
		/// <returns>true if the user can enter the prompt character into the control; otherwise, false. The default is true. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x0008D60C File Offset: 0x0008B80C
		// (set) Token: 0x0600258B RID: 9611 RVA: 0x0008D61C File Offset: 0x0008B81C
		[DefaultValue(true)]
		public bool AllowPromptAsInput
		{
			get
			{
				return this.provider.AllowPromptAsInput;
			}
			set
			{
				this.provider = new MaskedTextProvider(this.provider.Mask, this.provider.Culture, value, this.provider.PromptChar, this.provider.PasswordChar, this.provider.AsciiOnly);
				this.UpdateVisibleText();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control accepts characters outside of the ASCII character set.</summary>
		/// <returns>true if only ASCII is accepted; false if the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control can accept any arbitrary Unicode character. The default is false.</returns>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x0008D674 File Offset: 0x0008B874
		// (set) Token: 0x0600258D RID: 9613 RVA: 0x0008D684 File Offset: 0x0008B884
		[DefaultValue(false)]
		[RefreshProperties(2)]
		public bool AsciiOnly
		{
			get
			{
				return this.provider.AsciiOnly;
			}
			set
			{
				this.provider = new MaskedTextProvider(this.provider.Mask, this.provider.Culture, this.provider.AllowPromptAsInput, this.provider.PromptChar, this.provider.PasswordChar, value);
				this.UpdateVisibleText();
			}
		}

		/// <summary>Gets or sets a value indicating whether the masked text box control raises the system beep for each user key stroke that it rejects.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control should beep on invalid input; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x0008D6DC File Offset: 0x0008B8DC
		// (set) Token: 0x0600258F RID: 9615 RVA: 0x0008D6E4 File Offset: 0x0008B8E4
		[DefaultValue(false)]
		public bool BeepOnError
		{
			get
			{
				return this.beep_on_error;
			}
			set
			{
				this.beep_on_error = value;
			}
		}

		/// <summary>Gets a value indicating whether the user can undo the previous operation. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
		/// <returns>false in all cases. </returns>
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x0008D6F0 File Offset: 0x0008B8F0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new bool CanUndo
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> representing the information needed when creating a control.</returns>
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x0008D6F4 File Offset: 0x0008B8F4
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets or sets the culture information associated with the masked text box.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the culture supported by the <see cref="T:System.Windows.Forms.MaskedTextBox" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Windows.Forms.MaskedTextBox.Culture" /> was set to null.</exception>
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x0008D6FC File Offset: 0x0008B8FC
		// (set) Token: 0x06002593 RID: 9619 RVA: 0x0008D70C File Offset: 0x0008B90C
		[RefreshProperties(2)]
		public CultureInfo Culture
		{
			get
			{
				return this.provider.Culture;
			}
			set
			{
				this.provider = new MaskedTextProvider(this.provider.Mask, value, this.provider.AllowPromptAsInput, this.provider.PromptChar, this.provider.PasswordChar, this.provider.AsciiOnly);
				this.UpdateVisibleText();
			}
		}

		/// <summary>Gets or sets a value that determines whether literals and prompt characters are copied to the clipboard.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MaskFormat" /> values. The default is <see cref="F:System.Windows.Forms.MaskFormat.IncludeLiterals" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Property set with a <see cref="T:System.Windows.Forms.MaskFormat" />  value that is not valid. </exception>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x0008D764 File Offset: 0x0008B964
		// (set) Token: 0x06002595 RID: 9621 RVA: 0x0008D76C File Offset: 0x0008B96C
		[RefreshProperties(2)]
		[DefaultValue(MaskFormat.IncludeLiterals)]
		public MaskFormat CutCopyMaskFormat
		{
			get
			{
				return this.cut_copy_mask_format;
			}
			set
			{
				if (!Enum.IsDefined(typeof(MaskFormat), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MaskFormat));
				}
				this.cut_copy_mask_format = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> to use when performing type validation.</summary>
		/// <returns>An object that implements the <see cref="T:System.IFormatProvider" /> interface. </returns>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x0008D7A8 File Offset: 0x0008B9A8
		// (set) Token: 0x06002597 RID: 9623 RVA: 0x0008D7B0 File Offset: 0x0008B9B0
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public IFormatProvider FormatProvider
		{
			get
			{
				return this.format_provider;
			}
			set
			{
				this.format_provider = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the prompt characters in the input mask are hidden when the masked text box loses focus.</summary>
		/// <returns>true if <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> is hidden when <see cref="T:System.Windows.Forms.MaskedTextBox" /> does not have focus; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x0008D7BC File Offset: 0x0008B9BC
		// (set) Token: 0x06002599 RID: 9625 RVA: 0x0008D7C4 File Offset: 0x0008B9C4
		[DefaultValue(false)]
		[RefreshProperties(2)]
		public bool HidePromptOnLeave
		{
			get
			{
				return this.hide_prompt_on_leave;
			}
			set
			{
				this.hide_prompt_on_leave = value;
			}
		}

		/// <summary>Gets or sets the text insertion mode of the masked text box control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InsertKeyMode" /> value that indicates the current insertion mode. The default is <see cref="F:System.Windows.Forms.InsertKeyMode.Default" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An invalid <see cref="T:System.Windows.Forms.InsertKeyMode" /> value was supplied when setting this property.</exception>
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x0008D7D0 File Offset: 0x0008B9D0
		// (set) Token: 0x0600259B RID: 9627 RVA: 0x0008D7D8 File Offset: 0x0008B9D8
		[DefaultValue(InsertKeyMode.Default)]
		public InsertKeyMode InsertKeyMode
		{
			get
			{
				return this.insert_key_mode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(InsertKeyMode), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(InsertKeyMode));
				}
				this.insert_key_mode = value;
			}
		}

		/// <summary>Gets a value that specifies whether new user input overwrites existing input.</summary>
		/// <returns>true if <see cref="T:System.Windows.Forms.MaskedTextBox" /> will overwrite existing characters as the user enters new ones; false if <see cref="T:System.Windows.Forms.MaskedTextBox" /> will shift existing characters forward. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x0008D814 File Offset: 0x0008BA14
		[Browsable(false)]
		public bool IsOverwriteMode
		{
			get
			{
				if (this.insert_key_mode == InsertKeyMode.Default)
				{
					return this.insert_key_overwriting;
				}
				return this.insert_key_mode == InsertKeyMode.Overwrite;
			}
		}

		/// <summary>Gets or sets the lines of text in multiline configurations. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> that contains a single line. </returns>
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x0008D834 File Offset: 0x0008BA34
		// (set) Token: 0x0600259E RID: 9630 RVA: 0x0008D890 File Offset: 0x0008BA90
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new string[] Lines
		{
			get
			{
				string text = this.Text;
				if (text == null || text == string.Empty)
				{
					return new string[0];
				}
				return this.Text.Split(new string[] { "\r\n", "\r", "\n" }, 0);
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the input mask to use at run time. </summary>
		/// <returns>A <see cref="T:System.String" /> representing the current mask. The default value is the empty string which allows any input.</returns>
		/// <exception cref="T:System.ArgumentException">The string supplied to the <see cref="P:System.Windows.Forms.MaskedTextBox.Mask" /> property is not a valid mask. Invalid masks include masks containing non-printable characters.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x0008D894 File Offset: 0x0008BA94
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x0008D8B4 File Offset: 0x0008BAB4
		[Localizable(true)]
		[RefreshProperties(2)]
		[Editor("System.Windows.Forms.Design.MaskPropertyEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[MergableProperty(false)]
		public string Mask
		{
			get
			{
				if (this.is_empty_mask)
				{
					return string.Empty;
				}
				return this.provider.Mask;
			}
			set
			{
				this.is_empty_mask = value == string.Empty || value == null;
				if (this.is_empty_mask)
				{
					value = "<>";
				}
				this.provider = new MaskedTextProvider(value, this.provider.Culture, this.provider.AllowPromptAsInput, this.provider.PromptChar, this.provider.PasswordChar, this.provider.AsciiOnly);
				this.ReCalculatePasswordChar();
				this.UpdateVisibleText();
			}
		}

		/// <summary>Gets a value indicating whether all required inputs have been entered into the input mask.</summary>
		/// <returns>true if all required input has been entered into the mask; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x0008D940 File Offset: 0x0008BB40
		[Browsable(false)]
		public bool MaskCompleted
		{
			get
			{
				return this.provider.MaskCompleted;
			}
		}

		/// <summary>Gets a clone of the mask provider associated with this instance of the masked text box control.</summary>
		/// <returns>A masking language provider of type <see cref="P:System.Windows.Forms.MaskedTextBox.MaskedTextProvider" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x0008D950 File Offset: 0x0008BB50
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public MaskedTextProvider MaskedTextProvider
		{
			get
			{
				if (this.is_empty_mask)
				{
					return null;
				}
				return this.provider.Clone() as MaskedTextProvider;
			}
		}

		/// <summary>Gets a value indicating whether all required and optional inputs have been entered into the input mask. </summary>
		/// <returns>true if all required and optional inputs have been entered; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0008D970 File Offset: 0x0008BB70
		[Browsable(false)]
		public bool MaskFull
		{
			get
			{
				return this.provider.MaskFull;
			}
		}

		/// <summary>Gets or sets the maximum number of characters the user can type or paste into the text box control. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
		/// <returns>This property always returns 0. </returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x0008D980 File Offset: 0x0008BB80
		// (set) Token: 0x060025A5 RID: 9637 RVA: 0x0008D988 File Offset: 0x0008BB88
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override int MaxLength
		{
			get
			{
				return base.MaxLength;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline text box control. This property is not fully supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x0008D98C File Offset: 0x0008BB8C
		// (set) Token: 0x060025A7 RID: 9639 RVA: 0x0008D990 File Offset: 0x0008BB90
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override bool Multiline
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the character to be displayed in substitute for user input.</summary>
		/// <returns>The <see cref="T:System.Char" /> value used as the password character.</returns>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class.</exception>
		/// <exception cref="T:System.InvalidOperationException">The password character specified is the same as the current prompt character, <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" />. The two are required to be different.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x0008D994 File Offset: 0x0008BB94
		// (set) Token: 0x060025A9 RID: 9641 RVA: 0x0008D9B0 File Offset: 0x0008BBB0
		[RefreshProperties(2)]
		[DefaultValue('\0')]
		public char PasswordChar
		{
			get
			{
				if (this.use_system_password_char)
				{
					return '*';
				}
				return this.provider.PasswordChar;
			}
			set
			{
				this.provider.PasswordChar = value;
				if (value != '\0')
				{
					this.provider.IsPassword = true;
				}
				else
				{
					this.provider.IsPassword = false;
				}
				this.ReCalculatePasswordChar(true);
				base.CalculateDocument();
				this.UpdateVisibleText();
			}
		}

		/// <summary>Gets or sets the character used to represent the absence of user input in <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>The character used to prompt the user for input. The default is an underscore (_). </returns>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid prompt character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class.</exception>
		/// <exception cref="T:System.InvalidOperationException">The prompt character specified is the same as the current password character, <see cref="P:System.Windows.Forms.MaskedTextBox.PasswordChar" />. The two are required to be different.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x0008DA00 File Offset: 0x0008BC00
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x0008DA10 File Offset: 0x0008BC10
		[DefaultValue('_')]
		[Localizable(true)]
		[RefreshProperties(2)]
		public char PromptChar
		{
			get
			{
				return this.provider.PromptChar;
			}
			set
			{
				this.provider.PromptChar = value;
				this.UpdateVisibleText();
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x0008DA24 File Offset: 0x0008BC24
		// (set) Token: 0x060025AD RID: 9645 RVA: 0x0008DA2C File Offset: 0x0008BC2C
		public new bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				base.ReadOnly = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the parsing of user input should stop after the first invalid character is reached.</summary>
		/// <returns>true if processing of the input string should be terminated at the first parsing error; otherwise, false if processing should ignore all errors. The default is false.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x0008DA38 File Offset: 0x0008BC38
		// (set) Token: 0x060025AF RID: 9647 RVA: 0x0008DA40 File Offset: 0x0008BC40
		[DefaultValue(false)]
		public bool RejectInputOnFirstFailure
		{
			get
			{
				return this.reject_input_on_first_failure;
			}
			set
			{
				this.reject_input_on_first_failure = value;
			}
		}

		/// <summary>Gets or sets a value that determines how an input character that matches the prompt character should be handled.</summary>
		/// <returns>true if the prompt character entered as input causes the current editable position in the mask to be reset; otherwise, false to indicate that the prompt character is to be processed as a normal input character. The default is true.</returns>
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0008DA4C File Offset: 0x0008BC4C
		// (set) Token: 0x060025B1 RID: 9649 RVA: 0x0008DA5C File Offset: 0x0008BC5C
		[DefaultValue(true)]
		public bool ResetOnPrompt
		{
			get
			{
				return this.provider.ResetOnPrompt;
			}
			set
			{
				this.provider.ResetOnPrompt = value;
			}
		}

		/// <summary>Gets or sets a value that determines how a space input character should be handled.</summary>
		/// <returns>true if the space input character causes the current editable position in the mask to be reset; otherwise, false to indicate that it is to be processed as a normal input character. The default is true.</returns>
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x0008DA6C File Offset: 0x0008BC6C
		// (set) Token: 0x060025B3 RID: 9651 RVA: 0x0008DA7C File Offset: 0x0008BC7C
		[DefaultValue(true)]
		public bool ResetOnSpace
		{
			get
			{
				return this.provider.ResetOnSpace;
			}
			set
			{
				this.provider.ResetOnSpace = value;
			}
		}

		/// <summary>Gets or sets the current selection in the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control.</summary>
		/// <returns>The currently selected text as a <see cref="T:System.String" />. If no text is currently selected, this property resolves to an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlThread, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x0008DA8C File Offset: 0x0008BC8C
		// (set) Token: 0x060025B5 RID: 9653 RVA: 0x0008DA94 File Offset: 0x0008BC94
		public override string SelectedText
		{
			get
			{
				return base.SelectedText;
			}
			set
			{
				base.SelectedText = value;
				this.UpdateVisibleText();
			}
		}

		/// <summary>Gets or sets a value indicating whether the user is allowed to reenter literal values.</summary>
		/// <returns>true to allow literals to be reentered; otherwise, false to prevent the user from overwriting literal characters. The default is true.</returns>
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x0008DAA4 File Offset: 0x0008BCA4
		// (set) Token: 0x060025B7 RID: 9655 RVA: 0x0008DAB4 File Offset: 0x0008BCB4
		[DefaultValue(true)]
		public bool SkipLiterals
		{
			get
			{
				return this.provider.SkipLiterals;
			}
			set
			{
				this.provider.SkipLiterals = value;
			}
		}

		/// <summary>Gets or sets the text as it is currently displayed to the user. </summary>
		/// <returns>A <see cref="T:System.String" /> containing the text currently displayed by the control. The default is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x0008DAC4 File Offset: 0x0008BCC4
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x0008DB00 File Offset: 0x0008BD00
		[Editor("System.Windows.Forms.Design.MaskedTextBoxTextEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[RefreshProperties(2)]
		[Localizable(true)]
		[Bindable(true)]
		public override string Text
		{
			get
			{
				if (this.is_empty_mask || this.setting_text)
				{
					return base.Text;
				}
				if (this.provider == null)
				{
					return string.Empty;
				}
				return this.provider.ToString();
			}
			set
			{
				if (this.is_empty_mask)
				{
					this.setting_text = true;
					base.Text = value;
					this.setting_text = false;
				}
				else
				{
					this.InputText(value);
				}
				this.UpdateVisibleText();
			}
		}

		/// <summary>Gets or sets how text is aligned in a masked text box control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies how text is aligned relative to the control. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to this property is not of type <see cref="T:System.Windows.Forms.HorizontalAlignment" />.</exception>
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x0008DB40 File Offset: 0x0008BD40
		// (set) Token: 0x060025BB RID: 9659 RVA: 0x0008DB48 File Offset: 0x0008BD48
		[DefaultValue(HorizontalAlignment.Left)]
		[Localizable(true)]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.text_align;
			}
			set
			{
				if (this.text_align != value)
				{
					if (!Enum.IsDefined(typeof(HorizontalAlignment), value))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
					}
					this.text_align = value;
					this.OnTextAlignChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the length of the displayed text. </summary>
		/// <returns>An Int32 representing the number of characters in the <see cref="P:System.Windows.Forms.MaskedTextBox.Text" /> property. <see cref="P:System.Windows.Forms.MaskedTextBox.TextLength" /> respects properties such as <see cref="P:System.Windows.Forms.MaskedTextBox.HidePromptOnLeave" />, which means that the return results may be different depending on whether the control has focus.</returns>
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x0008DBA4 File Offset: 0x0008BDA4
		[Browsable(false)]
		public override int TextLength
		{
			get
			{
				return this.Text.Length;
			}
		}

		/// <summary>Gets or sets a value that determines whether literals and prompt characters are included in the formatted string.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MaskFormat" /> values. The default is <see cref="F:System.Windows.Forms.MaskFormat.IncludeLiterals" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Property set with a <see cref="T:System.Windows.Forms.MaskFormat" /> value that is not valid. </exception>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x0008DBB4 File Offset: 0x0008BDB4
		// (set) Token: 0x060025BE RID: 9662 RVA: 0x0008DC08 File Offset: 0x0008BE08
		[RefreshProperties(2)]
		[DefaultValue(MaskFormat.IncludeLiterals)]
		public MaskFormat TextMaskFormat
		{
			get
			{
				if (this.provider.IncludePrompt && this.provider.IncludeLiterals)
				{
					return MaskFormat.IncludePromptAndLiterals;
				}
				if (this.provider.IncludeLiterals)
				{
					return MaskFormat.IncludeLiterals;
				}
				if (this.provider.IncludePrompt)
				{
					return MaskFormat.IncludePrompt;
				}
				return MaskFormat.ExcludePromptAndLiterals;
			}
			set
			{
				if (!Enum.IsDefined(typeof(MaskFormat), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MaskFormat));
				}
				this.provider.IncludeLiterals = (value & MaskFormat.IncludeLiterals) == MaskFormat.IncludeLiterals;
				this.provider.IncludePrompt = (value & MaskFormat.IncludePrompt) == MaskFormat.IncludePrompt;
			}
		}

		/// <summary>Gets or sets a value indicating whether the operating system-supplied password character should be used.</summary>
		/// <returns>true if the system password should be used as the prompt character; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The password character specified is the same as the current prompt character, <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" />. The two are required to be different.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x0008DC68 File Offset: 0x0008BE68
		// (set) Token: 0x060025C0 RID: 9664 RVA: 0x0008DC70 File Offset: 0x0008BE70
		[RefreshProperties(2)]
		[DefaultValue(false)]
		public bool UseSystemPasswordChar
		{
			get
			{
				return this.use_system_password_char;
			}
			set
			{
				if (this.use_system_password_char != value)
				{
					this.use_system_password_char = value;
					if (this.use_system_password_char)
					{
						this.PasswordChar = this.PasswordChar;
					}
					else
					{
						this.PasswordChar = '\0';
					}
				}
			}
		}

		/// <summary>Gets or sets the data type used to verify the data input by the user. </summary>
		/// <returns>A <see cref="T:System.Type" /> representing the data type used in validation. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x0008DCB4 File Offset: 0x0008BEB4
		// (set) Token: 0x060025C2 RID: 9666 RVA: 0x0008DCBC File Offset: 0x0008BEBC
		[DefaultValue(null)]
		[Browsable(false)]
		public Type ValidatingType
		{
			get
			{
				return this.validating_type;
			}
			set
			{
				this.validating_type = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a multiline text box control automatically wraps words to the beginning of the next line when necessary. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />. </summary>
		/// <returns>The <see cref="P:System.Windows.Forms.MaskedTextBox.WordWrap" /> property always returns false. </returns>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x0008DCC8 File Offset: 0x0008BEC8
		// (set) Token: 0x060025C4 RID: 9668 RVA: 0x0008DCCC File Offset: 0x0008BECC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new bool WordWrap
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0008DCD0 File Offset: 0x0008BED0
		private void ReCalculatePasswordChar()
		{
			this.ReCalculatePasswordChar(this.PasswordChar != '\0');
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0008DCE4 File Offset: 0x0008BEE4
		private void ReCalculatePasswordChar(bool using_password)
		{
			if (using_password)
			{
				if (this.is_empty_mask)
				{
					this.document.PasswordChar = this.PasswordChar.ToString();
				}
				else
				{
					this.document.PasswordChar = string.Empty;
				}
			}
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0008DD30 File Offset: 0x0008BF30
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			base.OnPaintInternal(pevent);
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0008DD3C File Offset: 0x0008BF3C
		internal override Color ChangeBackColor(Color backColor)
		{
			return backColor;
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0008DD40 File Offset: 0x0008BF40
		private void UpdateVisibleText()
		{
			string text;
			if (this.is_empty_mask || this.setting_text)
			{
				text = base.Text;
			}
			else if (this.provider == null)
			{
				text = string.Empty;
			}
			else
			{
				text = this.provider.ToDisplayString();
			}
			this.setting_text = true;
			if (base.Text != text)
			{
				int selectionStart = base.SelectionStart;
				base.Text = text;
				base.SelectionStart = selectionStart;
			}
			this.setting_text = false;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0008DDC8 File Offset: 0x0008BFC8
		private void InputText(string text)
		{
			if (this.RejectInputOnFirstFailure)
			{
				int num;
				MaskedTextResultHint maskedTextResultHint;
				if (!this.provider.Set(text, ref num, ref maskedTextResultHint))
				{
					this.OnMaskInputRejected(new MaskInputRejectedEventArgs(num, maskedTextResultHint));
				}
			}
			else
			{
				this.provider.Clear();
				int num = 0;
				for (int i = 0; i < text.Length; i++)
				{
					char c = text.get_Chars(i);
					MaskedTextResultHint maskedTextResultHint;
					bool flag = this.provider.InsertAt(c, num, ref num, ref maskedTextResultHint);
					if (flag)
					{
						num++;
					}
					else
					{
						this.OnMaskInputRejected(new MaskInputRejectedEventArgs(num, maskedTextResultHint));
					}
				}
			}
		}

		// Token: 0x040012F0 RID: 4848
		private MaskedTextProvider provider;

		// Token: 0x040012F1 RID: 4849
		private bool beep_on_error;

		// Token: 0x040012F2 RID: 4850
		private IFormatProvider format_provider;

		// Token: 0x040012F3 RID: 4851
		private bool hide_prompt_on_leave;

		// Token: 0x040012F4 RID: 4852
		private InsertKeyMode insert_key_mode;

		// Token: 0x040012F5 RID: 4853
		private bool insert_key_overwriting;

		// Token: 0x040012F6 RID: 4854
		private bool reject_input_on_first_failure;

		// Token: 0x040012F7 RID: 4855
		private HorizontalAlignment text_align;

		// Token: 0x040012F8 RID: 4856
		private MaskFormat cut_copy_mask_format;

		// Token: 0x040012F9 RID: 4857
		private bool use_system_password_char;

		// Token: 0x040012FA RID: 4858
		private Type validating_type;

		// Token: 0x040012FB RID: 4859
		private bool is_empty_mask;

		// Token: 0x040012FC RID: 4860
		private bool setting_text;
	}
}

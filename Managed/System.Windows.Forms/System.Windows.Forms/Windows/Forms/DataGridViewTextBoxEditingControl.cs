using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a text box control that can be hosted in a <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013A RID: 314
	[ClassInterface(1)]
	[ComVisible(true)]
	public class DataGridViewTextBoxEditingControl : TextBox, IDataGridViewEditingControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxEditingControl" /> class.</summary>
		// Token: 0x060015F5 RID: 5621 RVA: 0x00051784 File Offset: 0x0004F984
		public DataGridViewTextBoxEditingControl()
		{
			this.repositionEditingControlOnValueChange = false;
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the text box control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridView" /> that contains the <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> that contains this control; otherwise, null if there is no associated <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00051794 File Offset: 0x0004F994
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x0005179C File Offset: 0x0004F99C
		public virtual DataGridView EditingControlDataGridView
		{
			get
			{
				return this.editingControlDataGridView;
			}
			set
			{
				this.editingControlDataGridView = value;
			}
		}

		/// <summary>Gets or sets the formatted representation of the current value of the text box control.</summary>
		/// <returns>An object representing the current value of this control.</returns>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x000517A8 File Offset: 0x0004F9A8
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x000517B0 File Offset: 0x0004F9B0
		public virtual object EditingControlFormattedValue
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = (string)value;
			}
		}

		/// <summary>Gets or sets the index of the owning cell's parent row.</summary>
		/// <returns>The index of the row that contains the owning cell; -1 if there is no owning row.</returns>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x000517C0 File Offset: 0x0004F9C0
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x000517C8 File Offset: 0x0004F9C8
		public virtual int EditingControlRowIndex
		{
			get
			{
				return this.rowIndex;
			}
			set
			{
				this.rowIndex = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the current value of the text box control has changed.</summary>
		/// <returns>true if the value of the control has changed; otherwise, false.</returns>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x000517D4 File Offset: 0x0004F9D4
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x000517DC File Offset: 0x0004F9DC
		public virtual bool EditingControlValueChanged
		{
			get
			{
				return this.editingControlValueChanged;
			}
			set
			{
				this.editingControlValueChanged = value;
			}
		}

		/// <summary>Gets the cursor used when the mouse pointer is over the <see cref="P:System.Windows.Forms.DataGridView.EditingPanel" /> but not over the editing control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the mouse pointer used for the editing panel. </returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x000517E8 File Offset: 0x0004F9E8
		public virtual Cursor EditingPanelCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		/// <summary>Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.</summary>
		/// <returns>true if the cell's <see cref="P:System.Windows.Forms.DataGridViewCellStyle.WrapMode" /> is set to true and the alignment property is not set to one of the <see cref="T:System.Windows.Forms.DataGridViewContentAlignment" /> values that aligns the content to the top; otherwise, false.</returns>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x000517F0 File Offset: 0x0004F9F0
		public virtual bool RepositionEditingControlOnValueChange
		{
			get
			{
				return this.repositionEditingControlOnValueChange;
			}
		}

		/// <summary>Changes the control's user interface (UI) to be consistent with the specified cell style.</summary>
		/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to use as the model for the UI.</param>
		// Token: 0x06001600 RID: 5632 RVA: 0x000517F8 File Offset: 0x0004F9F8
		public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			this.Font = dataGridViewCellStyle.Font;
			this.BackColor = dataGridViewCellStyle.BackColor;
			this.ForeColor = dataGridViewCellStyle.ForeColor;
		}

		/// <summary>Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView" /> should process.</summary>
		/// <returns>true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.</returns>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> that represents the key that was pressed.</param>
		/// <param name="dataGridViewWantsInputKey">true when the <see cref="T:System.Windows.Forms.DataGridView" /> wants to process the <paramref name="keyData" />; otherwise, false.</param>
		// Token: 0x06001601 RID: 5633 RVA: 0x0005182C File Offset: 0x0004FA2C
		public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			switch (keyData)
			{
			case Keys.Left:
				return base.SelectionStart != 0;
			case Keys.Up:
			case Keys.Down:
				return false;
			case Keys.Right:
				return base.SelectionStart != this.TextLength;
			default:
				return true;
			}
		}

		/// <summary>Retrieves the formatted value of the cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.</returns>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the data error context.</param>
		// Token: 0x06001602 RID: 5634 RVA: 0x0005187C File Offset: 0x0004FA7C
		public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return this.EditingControlFormattedValue;
		}

		/// <summary>Prepares the currently selected cell for editing.</summary>
		/// <param name="selectAll">true to select the cell contents; otherwise, false.</param>
		// Token: 0x06001603 RID: 5635 RVA: 0x00051884 File Offset: 0x0004FA84
		public virtual void PrepareEditingControlForEdit(bool selectAll)
		{
			base.Focus();
			if (selectAll)
			{
				base.SelectAll();
			}
			this.editingControlValueChanged = false;
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06001604 RID: 5636 RVA: 0x000518A0 File Offset: 0x0004FAA0
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000518AC File Offset: 0x0004FAAC
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.editingControlValueChanged = true;
		}

		/// <summary>Processes key events.</summary>
		/// <returns>true if the key event was handled by the editing control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> indicating the key that was pressed.</param>
		// Token: 0x06001606 RID: 5638 RVA: 0x000518BC File Offset: 0x0004FABC
		protected override bool ProcessKeyEventArgs(ref Message m)
		{
			return base.ProcessKeyEventArgs(ref m);
		}

		// Token: 0x04000C34 RID: 3124
		private DataGridView editingControlDataGridView;

		// Token: 0x04000C35 RID: 3125
		private int rowIndex;

		// Token: 0x04000C36 RID: 3126
		private bool editingControlValueChanged;

		// Token: 0x04000C37 RID: 3127
		private bool repositionEditingControlOnValueChange;
	}
}

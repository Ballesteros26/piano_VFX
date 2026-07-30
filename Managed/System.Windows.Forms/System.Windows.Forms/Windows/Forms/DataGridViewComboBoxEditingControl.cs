using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the hosted combo box control in a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200010C RID: 268
	[ClassInterface(1)]
	[ComVisible(true)]
	public class DataGridViewComboBoxEditingControl : ComboBox, IDataGridViewEditingControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxEditingControl" /> class.</summary>
		// Token: 0x060013F8 RID: 5112 RVA: 0x0004C2A0 File Offset: 0x0004A4A0
		public DataGridViewComboBoxEditingControl()
		{
			this.editingControlValueChanged = false;
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the combo box control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridView" /> that contains the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> that contains this control; otherwise, null if there is no associated <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0004C2B0 File Offset: 0x0004A4B0
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x0004C2B8 File Offset: 0x0004A4B8
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

		/// <summary>Gets or sets the formatted representation of the current value of the control.</summary>
		/// <returns>An object representing the current value of this control.</returns>
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0004C2C4 File Offset: 0x0004A4C4
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x0004C2CC File Offset: 0x0004A4CC
		public virtual object EditingControlFormattedValue
		{
			get
			{
				return this.editingControlFormattedValue;
			}
			set
			{
				this.editingControlFormattedValue = value;
			}
		}

		/// <summary>Gets or sets the index of the owning cell's parent row.</summary>
		/// <returns>The index of the row that contains the owning cell; -1 if there is no owning row.</returns>
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0004C2D8 File Offset: 0x0004A4D8
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x0004C2E0 File Offset: 0x0004A4E0
		public virtual int EditingControlRowIndex
		{
			get
			{
				return this.editingControlRowIndex;
			}
			set
			{
				this.editingControlRowIndex = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the current value of the control has changed.</summary>
		/// <returns>true if the value of the control has changed; otherwise, false.</returns>
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0004C2EC File Offset: 0x0004A4EC
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x0004C2F4 File Offset: 0x0004A4F4
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

		/// <summary>Gets the cursor used during editing.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor image used by the mouse pointer during editing.</returns>
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0004C300 File Offset: 0x0004A500
		public virtual Cursor EditingPanelCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		/// <summary>Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0004C308 File Offset: 0x0004A508
		public virtual bool RepositionEditingControlOnValueChange
		{
			get
			{
				return false;
			}
		}

		/// <summary>Changes the control's user interface (UI) to be consistent with the specified cell style.</summary>
		/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to use as a pattern for the UI.</param>
		// Token: 0x06001403 RID: 5123 RVA: 0x0004C30C File Offset: 0x0004A50C
		public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
		}

		/// <summary>Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView" /> should process.</summary>
		/// <returns>true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key that was pressed.</param>
		/// <param name="dataGridViewWantsInputKey">true to indicate that the <see cref="T:System.Windows.Forms.DataGridView" /> control can process the key; otherwise, false.</param>
		// Token: 0x06001404 RID: 5124 RVA: 0x0004C310 File Offset: 0x0004A510
		public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			return base.IsInputKey(keyData);
		}

		/// <summary>Retrieves the formatted value of the cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.</returns>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the data error context.</param>
		// Token: 0x06001405 RID: 5125 RVA: 0x0004C31C File Offset: 0x0004A51C
		public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return this.Text;
		}

		/// <summary>Prepares the currently selected cell for editing.</summary>
		/// <param name="selectAll">true to select all of the cell's content; otherwise, false.</param>
		// Token: 0x06001406 RID: 5126 RVA: 0x0004C324 File Offset: 0x0004A524
		public virtual void PrepareEditingControlForEdit(bool selectAll)
		{
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001407 RID: 5127 RVA: 0x0004C328 File Offset: 0x0004A528
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
		}

		// Token: 0x04000B86 RID: 2950
		private DataGridView editingControlDataGridView;

		// Token: 0x04000B87 RID: 2951
		private object editingControlFormattedValue;

		// Token: 0x04000B88 RID: 2952
		private int editingControlRowIndex;

		// Token: 0x04000B89 RID: 2953
		private bool editingControlValueChanged;
	}
}

using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.EditingControlShowing" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000111 RID: 273
	public class DataGridViewEditingControlShowingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs" /> class.</summary>
		/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> in which the user will edit the selected cell's contents.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> representing the style of the cell being edited.</param>
		// Token: 0x0600140D RID: 5133 RVA: 0x0004C378 File Offset: 0x0004A578
		public DataGridViewEditingControlShowingEventArgs(Control control, DataGridViewCellStyle cellStyle)
		{
			this.control = control;
			this.cellStyle = cellStyle;
		}

		/// <summary>Gets or sets the cell style of the edited cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> representing the style of the cell being edited.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0004C390 File Offset: 0x0004A590
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x0004C398 File Offset: 0x0004A598
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
			set
			{
				this.cellStyle = value;
			}
		}

		/// <summary>The control shown to the user for editing the selected cell's value.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that displays an area for the user to enter or change the selected cell's value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0004C3A4 File Offset: 0x0004A5A4
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x04000BAA RID: 2986
		private Control control;

		// Token: 0x04000BAB RID: 2987
		private DataGridViewCellStyle cellStyle;
	}
}

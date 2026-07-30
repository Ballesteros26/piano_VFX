using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnModeChanged" /> event. </summary>
	// Token: 0x020000D8 RID: 216
	public class DataGridViewAutoSizeColumnModeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs" /> class. </summary>
		/// <param name="dataGridViewColumn">The column with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property that changed.</param>
		/// <param name="previousMode">The previous <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value of the column's <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property. </param>
		// Token: 0x06001123 RID: 4387 RVA: 0x00044D7C File Offset: 0x00042F7C
		public DataGridViewAutoSizeColumnModeEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewAutoSizeColumnMode previousMode)
		{
			this.dataGridViewColumn = dataGridViewColumn;
			this.previousMode = previousMode;
		}

		/// <summary>Gets the column with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property that changed.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property that changed.</returns>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00044D94 File Offset: 0x00042F94
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		/// <summary>Gets the previous value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property of the column.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value representing the previous value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property of the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs.Column" />.</returns>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00044D9C File Offset: 0x00042F9C
		public DataGridViewAutoSizeColumnMode PreviousMode
		{
			get
			{
				return this.previousMode;
			}
		}

		// Token: 0x04000AAD RID: 2733
		private DataGridViewColumn dataGridViewColumn;

		// Token: 0x04000AAE RID: 2734
		private DataGridViewAutoSizeColumnMode previousMode;
	}
}

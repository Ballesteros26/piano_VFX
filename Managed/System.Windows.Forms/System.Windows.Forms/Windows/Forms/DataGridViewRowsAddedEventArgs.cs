using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowsAdded" /> event. </summary>
	// Token: 0x02000131 RID: 305
	public class DataGridViewRowsAddedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowsAddedEventArgs" /> class. </summary>
		/// <param name="rowIndex">The index of the first added row.</param>
		/// <param name="rowCount">The number of rows that have been added.</param>
		// Token: 0x0600158C RID: 5516 RVA: 0x00050CC8 File Offset: 0x0004EEC8
		public DataGridViewRowsAddedEventArgs(int rowIndex, int rowCount)
		{
			this.rowIndex = rowIndex;
			this.rowCount = rowCount;
		}

		/// <summary>Gets the number of rows that have been added.</summary>
		/// <returns>The number of rows that have been added.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00050CE0 File Offset: 0x0004EEE0
		public int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		/// <summary>Gets the index of the first added row.</summary>
		/// <returns>The index of the first added row.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00050CE8 File Offset: 0x0004EEE8
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000C20 RID: 3104
		private int rowIndex;

		// Token: 0x04000C21 RID: 3105
		private int rowCount;
	}
}

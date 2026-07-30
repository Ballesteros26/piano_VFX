using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.ColumnStateChanged" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000107 RID: 263
	public class DataGridViewColumnStateChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnStateChangedEventArgs" /> class. </summary>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> whose state has changed.</param>
		/// <param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		// Token: 0x0600138D RID: 5005 RVA: 0x0004B290 File Offset: 0x00049490
		public DataGridViewColumnStateChangedEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewElementStates stateChanged)
		{
			this.dataGridViewColumn = dataGridViewColumn;
			this.stateChanged = stateChanged;
		}

		/// <summary>Gets the column whose state changed.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> whose state changed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0004B2A8 File Offset: 0x000494A8
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		/// <summary>Gets the new column state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</returns>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0004B2B0 File Offset: 0x000494B0
		public DataGridViewElementStates StateChanged
		{
			get
			{
				return this.stateChanged;
			}
		}

		// Token: 0x04000B6E RID: 2926
		private DataGridViewColumn dataGridViewColumn;

		// Token: 0x04000B6F RID: 2927
		private DataGridViewElementStates stateChanged;
	}
}

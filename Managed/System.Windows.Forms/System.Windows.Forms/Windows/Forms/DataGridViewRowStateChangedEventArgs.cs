using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowStateChanged" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000130 RID: 304
	public class DataGridViewRowStateChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowStateChangedEventArgs" /> class. </summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has a changed state.</param>
		/// <param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the row.</param>
		// Token: 0x06001589 RID: 5513 RVA: 0x00050CA0 File Offset: 0x0004EEA0
		public DataGridViewRowStateChangedEventArgs(DataGridViewRow dataGridViewRow, DataGridViewElementStates stateChanged)
		{
			this.dataGridViewRow = dataGridViewRow;
			this.stateChanged = stateChanged;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has a changed state.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has a changed state.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x00050CB8 File Offset: 0x0004EEB8
		public DataGridViewRow Row
		{
			get
			{
				return this.dataGridViewRow;
			}
		}

		/// <summary>Gets the state that has changed on the row.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the row.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00050CC0 File Offset: 0x0004EEC0
		public DataGridViewElementStates StateChanged
		{
			get
			{
				return this.stateChanged;
			}
		}

		// Token: 0x04000C1E RID: 3102
		private DataGridViewRow dataGridViewRow;

		// Token: 0x04000C1F RID: 3103
		private DataGridViewElementStates stateChanged;
	}
}

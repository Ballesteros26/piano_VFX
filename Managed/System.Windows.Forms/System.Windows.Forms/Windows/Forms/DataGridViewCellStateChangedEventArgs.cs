using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellStateChanged" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F0 RID: 240
	public class DataGridViewCellStateChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStateChangedEventArgs" /> class. </summary>
		/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that has a changed state.</param>
		/// <param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the cell.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewCell" /> is null.</exception>
		// Token: 0x06001278 RID: 4728 RVA: 0x0004864C File Offset: 0x0004684C
		public DataGridViewCellStateChangedEventArgs(DataGridViewCell dataGridViewCell, DataGridViewElementStates stateChanged)
		{
			this.dataGridViewCell = dataGridViewCell;
			this.stateChanged = stateChanged;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that has a changed state.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> whose state has changed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00048664 File Offset: 0x00046864
		public DataGridViewCell Cell
		{
			get
			{
				return this.dataGridViewCell;
			}
		}

		/// <summary>Gets the state that has changed on the cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the cell.</returns>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0004866C File Offset: 0x0004686C
		public DataGridViewElementStates StateChanged
		{
			get
			{
				return this.stateChanged;
			}
		}

		// Token: 0x04000B14 RID: 2836
		private DataGridViewCell dataGridViewCell;

		// Token: 0x04000B15 RID: 2837
		private DataGridViewElementStates stateChanged;
	}
}

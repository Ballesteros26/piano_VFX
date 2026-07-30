using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnsModeChanged" /> event. </summary>
	// Token: 0x020000DA RID: 218
	public class DataGridViewAutoSizeColumnsModeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs" /> class. </summary>
		/// <param name="previousModes">An array of <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> values representing the previous <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property values of each column. </param>
		// Token: 0x06001126 RID: 4390 RVA: 0x00044DA4 File Offset: 0x00042FA4
		public DataGridViewAutoSizeColumnsModeEventArgs(DataGridViewAutoSizeColumnMode[] previousModes)
		{
			this.previousModes = previousModes;
		}

		/// <summary>Gets an array of the previous values of the column <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> properties.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> values representing the previous values of the column <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> properties.</returns>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00044DB4 File Offset: 0x00042FB4
		public DataGridViewAutoSizeColumnMode[] PreviousModes
		{
			get
			{
				return this.previousModes;
			}
		}

		// Token: 0x04000AB7 RID: 2743
		private DataGridViewAutoSizeColumnMode[] previousModes;
	}
}

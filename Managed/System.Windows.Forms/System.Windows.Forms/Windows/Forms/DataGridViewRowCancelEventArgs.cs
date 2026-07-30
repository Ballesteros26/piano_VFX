using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.UserDeletingRow" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000122 RID: 290
	public class DataGridViewRowCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowCancelEventArgs" /> class. </summary>
		/// <param name="dataGridViewRow">The row the user is deleting.</param>
		// Token: 0x060014ED RID: 5357 RVA: 0x0004EF88 File Offset: 0x0004D188
		public DataGridViewRowCancelEventArgs(DataGridViewRow dataGridViewRow)
		{
			this.dataGridViewRow = dataGridViewRow;
		}

		/// <summary>Gets the row that the user is deleting.</summary>
		/// <returns>The row that the user deleted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0004EF98 File Offset: 0x0004D198
		public DataGridViewRow Row
		{
			get
			{
				return this.dataGridViewRow;
			}
		}

		// Token: 0x04000BF1 RID: 3057
		private DataGridViewRow dataGridViewRow;
	}
}

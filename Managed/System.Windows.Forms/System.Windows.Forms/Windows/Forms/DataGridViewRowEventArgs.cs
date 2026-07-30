using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for row-related <see cref="T:System.Windows.Forms.DataGridView" /> events. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000128 RID: 296
	public class DataGridViewRowEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> class. </summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that the event occurred for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRow" /> is null.</exception>
		// Token: 0x0600153A RID: 5434 RVA: 0x0004FEA8 File Offset: 0x0004E0A8
		public DataGridViewRowEventArgs(DataGridViewRow dataGridViewRow)
		{
			this.dataGridViewRow = dataGridViewRow;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> associated with the event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> associated with the event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0004FEB8 File Offset: 0x0004E0B8
		public DataGridViewRow Row
		{
			get
			{
				return this.dataGridViewRow;
			}
		}

		// Token: 0x04000BFB RID: 3067
		private DataGridViewRow dataGridViewRow;
	}
}

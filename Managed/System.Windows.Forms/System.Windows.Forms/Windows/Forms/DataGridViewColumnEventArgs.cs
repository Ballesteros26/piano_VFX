using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for column-related events of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000102 RID: 258
	public class DataGridViewColumnEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> class. </summary>
		/// <param name="dataGridViewColumn">The column that the event occurs for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumn" /> is null.</exception>
		// Token: 0x06001370 RID: 4976 RVA: 0x0004A9B4 File Offset: 0x00048BB4
		public DataGridViewColumnEventArgs(DataGridViewColumn dataGridViewColumn)
		{
			this.dataGridViewColumn = dataGridViewColumn;
		}

		/// <summary>Gets the column that the event occurs for.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> that the event occurs for.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0004A9C4 File Offset: 0x00048BC4
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		// Token: 0x04000B63 RID: 2915
		private DataGridViewColumn dataGridViewColumn;
	}
}

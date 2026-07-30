using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.ColumnDividerDoubleClick" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
	// Token: 0x02000101 RID: 257
	public class DataGridViewColumnDividerDoubleClickEventArgs : HandledMouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventArgs" /> class. </summary>
		/// <param name="columnIndex">The index of the column next to the column divider that was double-clicked. </param>
		/// <param name="e">A new <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> containing the inherited event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1.</exception>
		// Token: 0x0600136E RID: 4974 RVA: 0x0004A974 File Offset: 0x00048B74
		public DataGridViewColumnDividerDoubleClickEventArgs(int columnIndex, HandledMouseEventArgs e)
			: base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
		{
			this.columnIndex = columnIndex;
		}

		/// <summary>The index of the column next to the column divider that was double-clicked.</summary>
		/// <returns>The index of the column next to the divider. </returns>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0004A9AC File Offset: 0x00048BAC
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x04000B62 RID: 2914
		private int columnIndex;
	}
}

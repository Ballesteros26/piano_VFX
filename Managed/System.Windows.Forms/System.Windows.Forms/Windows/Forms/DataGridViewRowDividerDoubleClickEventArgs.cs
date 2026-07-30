using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowDividerDoubleClick" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
	// Token: 0x02000126 RID: 294
	public class DataGridViewRowDividerDoubleClickEventArgs : HandledMouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowDividerDoubleClickEventArgs" /> class. </summary>
		/// <param name="rowIndex">The index of the row above the row divider that was double-clicked.</param>
		/// <param name="e">A new <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> containing the inherited event data.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x06001534 RID: 5428 RVA: 0x0004FE34 File Offset: 0x0004E034
		public DataGridViewRowDividerDoubleClickEventArgs(int rowIndex, HandledMouseEventArgs e)
			: base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
		{
			this.rowIndex = rowIndex;
		}

		/// <summary>The index of the row above the row divider that was double-clicked.</summary>
		/// <returns>The index of the row above the divider.</returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0004FE6C File Offset: 0x0004E06C
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000BF8 RID: 3064
		private int rowIndex;
	}
}

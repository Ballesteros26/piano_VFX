using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellToolTipTextNeeded" /> event. </summary>
	// Token: 0x020000F5 RID: 245
	public class DataGridViewCellToolTipTextNeededEventArgs : DataGridViewCellEventArgs
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x00048EB4 File Offset: 0x000470B4
		internal DataGridViewCellToolTipTextNeededEventArgs(string toolTipText, int rowIndex, int columnIndex)
			: base(columnIndex, rowIndex)
		{
			this.toolTipText = toolTipText;
		}

		/// <summary>Gets or sets the ToolTip text.</summary>
		/// <returns>The current ToolTip text.</returns>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00048EC8 File Offset: 0x000470C8
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x00048ED0 File Offset: 0x000470D0
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		// Token: 0x04000B30 RID: 2864
		private string toolTipText;
	}
}

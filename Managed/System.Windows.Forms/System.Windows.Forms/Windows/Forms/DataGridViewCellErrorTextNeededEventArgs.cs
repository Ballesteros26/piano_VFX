using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellErrorTextNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
	// Token: 0x020000EA RID: 234
	public class DataGridViewCellErrorTextNeededEventArgs : DataGridViewCellEventArgs
	{
		// Token: 0x06001251 RID: 4689 RVA: 0x000482AC File Offset: 0x000464AC
		internal DataGridViewCellErrorTextNeededEventArgs(string errorText, int rowIndex, int columnIndex)
			: base(columnIndex, rowIndex)
		{
			this.errorText = errorText;
		}

		/// <summary>Gets or sets the message that is displayed when the cell is selected.</summary>
		/// <returns>The error message.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x000482C0 File Offset: 0x000464C0
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x000482C8 File Offset: 0x000464C8
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
			set
			{
				this.errorText = value;
			}
		}

		// Token: 0x04000AFA RID: 2810
		private string errorText;
	}
}

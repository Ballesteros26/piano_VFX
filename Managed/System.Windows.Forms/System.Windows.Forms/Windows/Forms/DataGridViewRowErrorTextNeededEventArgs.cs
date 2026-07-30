using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
	// Token: 0x02000127 RID: 295
	public class DataGridViewRowErrorTextNeededEventArgs : EventArgs
	{
		// Token: 0x06001536 RID: 5430 RVA: 0x0004FE74 File Offset: 0x0004E074
		internal DataGridViewRowErrorTextNeededEventArgs(int rowIndex, string errorText)
		{
			this.rowIndex = rowIndex;
			this.errorText = errorText;
		}

		/// <summary>Gets or sets the error text for the row.</summary>
		/// <returns>A string that represents the error text for the row.</returns>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0004FE8C File Offset: 0x0004E08C
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x0004FE94 File Offset: 0x0004E094
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

		/// <summary>Gets the row that raised the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextNeeded" /> event.</summary>
		/// <returns>The zero based row index for the row.</returns>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0004FEA0 File Offset: 0x0004E0A0
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000BF9 RID: 3065
		private int rowIndex;

		// Token: 0x04000BFA RID: 3066
		private string errorText;
	}
}

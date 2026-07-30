using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the <see cref="T:System.Windows.Forms.DataGridView" /> entity that owns the cell style that was changed.</summary>
	// Token: 0x020000F4 RID: 244
	[Flags]
	public enum DataGridViewCellStyleScopes
	{
		/// <summary>The owning entity is unspecified.</summary>
		// Token: 0x04000B27 RID: 2855
		None = 0,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property changed.</summary>
		// Token: 0x04000B28 RID: 2856
		Cell = 1,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridViewColumn.DefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000B29 RID: 2857
		Column = 2,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridViewRow.DefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000B2A RID: 2858
		Row = 4,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.DefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000B2B RID: 2859
		DataGridView = 8,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersDefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000B2C RID: 2860
		ColumnHeaders = 16,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000B2D RID: 2861
		RowHeaders = 32,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.RowsDefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000B2E RID: 2862
		Rows = 64,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.AlternatingRowsDefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000B2F RID: 2863
		AlternatingRows = 128
	}
}

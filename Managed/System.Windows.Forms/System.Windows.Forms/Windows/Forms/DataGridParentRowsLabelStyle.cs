using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how the parent row labels of a <see cref="T:System.Windows.Forms.DataGrid" /> control are displayed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CA RID: 202
	public enum DataGridParentRowsLabelStyle
	{
		/// <summary>Display no parent row labels.</summary>
		// Token: 0x04000978 RID: 2424
		None,
		/// <summary>Displays the parent table name.</summary>
		// Token: 0x04000979 RID: 2425
		TableName,
		/// <summary>Displays the parent column name.</summary>
		// Token: 0x0400097A RID: 2426
		ColumnName,
		/// <summary>Displays both the parent table and column names.</summary>
		// Token: 0x0400097B RID: 2427
		Both
	}
}

using System;

namespace System.Windows.Forms
{
	/// <summary>Defines how a <see cref="T:System.Windows.Forms.DataGridView" /> column can be sorted by the user.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000106 RID: 262
	public enum DataGridViewColumnSortMode
	{
		/// <summary>The column can only be sorted programmatically, but it is not intended for sorting, so the column header will not include space for a sorting glyph.</summary>
		// Token: 0x04000B6B RID: 2923
		NotSortable,
		/// <summary>The user can sort the column by clicking the column header unless the column headers are used for selection. A sorting glyph will be displayed automatically.</summary>
		// Token: 0x04000B6C RID: 2924
		Automatic,
		/// <summary>The column can only be sorted programmatically, and the column header will include space for a sorting glyph. </summary>
		// Token: 0x04000B6D RID: 2925
		Programmatic
	}
}

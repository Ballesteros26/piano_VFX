using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a column contained in a <see cref="T:System.Windows.Forms.ListView" /> should be resized.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200008A RID: 138
	public enum ColumnHeaderAutoResizeStyle
	{
		/// <summary>Specifies no resizing should occur.</summary>
		// Token: 0x04000733 RID: 1843
		None,
		/// <summary>Specifies the column should be resized based on the length of the column header content.</summary>
		// Token: 0x04000734 RID: 1844
		HeaderSize,
		/// <summary>Specifies the column should be resized based on the length of the column content.</summary>
		// Token: 0x04000735 RID: 1845
		ColumnContent
	}
}

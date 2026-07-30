using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that indicate whether content is copied from a <see cref="T:System.Windows.Forms.DataGridView" /> control to the Clipboard.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FB RID: 251
	public enum DataGridViewClipboardCopyMode
	{
		/// <summary>Copying to the Clipboard is disabled.</summary>
		// Token: 0x04000B40 RID: 2880
		Disable,
		/// <summary>The text values of selected cells can be copied to the Clipboard. Row or column header text is included for rows or columns that contain selected cells only when the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property is set to <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.RowHeaderSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" /> and at least one header is selected. </summary>
		// Token: 0x04000B41 RID: 2881
		EnableWithAutoHeaderText,
		/// <summary>The text values of selected cells can be copied to the Clipboard. Header text is not included.</summary>
		// Token: 0x04000B42 RID: 2882
		EnableWithoutHeaderText,
		/// <summary>The text values of selected cells can be copied to the Clipboard. Header text is included for rows and columns that contain selected cells.  </summary>
		// Token: 0x04000B43 RID: 2883
		EnableAlwaysIncludeHeaderText
	}
}

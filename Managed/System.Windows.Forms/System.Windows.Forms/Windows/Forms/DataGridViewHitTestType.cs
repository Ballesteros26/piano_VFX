using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies a location in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000116 RID: 278
	public enum DataGridViewHitTestType
	{
		/// <summary>An empty part of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000BBF RID: 3007
		None,
		/// <summary>A cell in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000BC0 RID: 3008
		Cell,
		/// <summary>A column header in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000BC1 RID: 3009
		ColumnHeader,
		/// <summary>A row header in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000BC2 RID: 3010
		RowHeader,
		/// <summary>The top left column header in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000BC3 RID: 3011
		TopLeftHeader,
		/// <summary>The horizontal scroll bar of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000BC4 RID: 3012
		HorizontalScrollBar,
		/// <summary>The vertical scroll bar of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000BC5 RID: 3013
		VerticalScrollBar
	}
}

using System;

namespace System.Windows.Forms
{
	/// <summary>Describes how cells of a DataGridView control can be selected.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000136 RID: 310
	public enum DataGridViewSelectionMode
	{
		/// <summary>One or more individual cells can be selected.</summary>
		// Token: 0x04000C26 RID: 3110
		CellSelect,
		/// <summary>The entire row will be selected by clicking its row's header or a cell contained in that row.</summary>
		// Token: 0x04000C27 RID: 3111
		FullRowSelect,
		/// <summary>The entire column will be selected by clicking the column's header or a cell contained in that column.</summary>
		// Token: 0x04000C28 RID: 3112
		FullColumnSelect,
		/// <summary>The row will be selected by clicking in the row's header cell. An individual cell can be selected by clicking that cell.</summary>
		// Token: 0x04000C29 RID: 3113
		RowHeaderSelect,
		/// <summary>The column will be selected by clicking in the column's header cell. An individual cell can be selected by clicking that cell.</summary>
		// Token: 0x04000C2A RID: 3114
		ColumnHeaderSelect
	}
}

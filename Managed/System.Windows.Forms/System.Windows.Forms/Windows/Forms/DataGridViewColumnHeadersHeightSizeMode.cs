using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the height of the column headers is adjusted. </summary>
	// Token: 0x02000105 RID: 261
	public enum DataGridViewColumnHeadersHeightSizeMode
	{
		/// <summary>Users can adjust the column header height with the mouse.</summary>
		// Token: 0x04000B67 RID: 2919
		EnableResizing,
		/// <summary>Users cannot adjust the column header height with the mouse.</summary>
		// Token: 0x04000B68 RID: 2920
		DisableResizing,
		/// <summary>The column header height adjusts to fit the contents of all the column header cells. </summary>
		// Token: 0x04000B69 RID: 2921
		AutoSize
	}
}

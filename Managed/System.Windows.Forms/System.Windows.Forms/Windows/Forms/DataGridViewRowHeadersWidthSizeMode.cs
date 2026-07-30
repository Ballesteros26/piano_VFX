using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the row header width is adjusted. </summary>
	// Token: 0x0200012B RID: 299
	public enum DataGridViewRowHeadersWidthSizeMode
	{
		/// <summary>Users can adjust the column header width with the mouse.</summary>
		// Token: 0x04000BFE RID: 3070
		EnableResizing,
		/// <summary>Users cannot adjust the column header width with the mouse.</summary>
		// Token: 0x04000BFF RID: 3071
		DisableResizing,
		/// <summary>The row header width adjusts to fit the contents of all the row header cells. </summary>
		// Token: 0x04000C00 RID: 3072
		AutoSizeToAllHeaders,
		/// <summary>The row header width adjusts to fit the contents of all the row headers in the currently displayed rows. </summary>
		// Token: 0x04000C01 RID: 3073
		AutoSizeToDisplayedHeaders,
		/// <summary>The row header width adjusts to fit the contents of the first row header.</summary>
		// Token: 0x04000C02 RID: 3074
		AutoSizeToFirstHeader
	}
}

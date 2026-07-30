using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>References the method to call when retrieving table data from a provider.</summary>
	/// <param name="tableData">The data to retrieve from the provider.</param>
	// Token: 0x02000470 RID: 1136
	// (Invoke) Token: 0x0600340A RID: 13322
	public delegate void TableCallback(ICollection tableData);
}

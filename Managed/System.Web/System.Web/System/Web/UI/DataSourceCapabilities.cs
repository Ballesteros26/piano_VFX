using System;

namespace System.Web.UI
{
	/// <summary>Provides a way to request processing beyond record retrieval for a data retrieval operation of a data source control.</summary>
	// Token: 0x0200015C RID: 348
	[Flags]
	public enum DataSourceCapabilities
	{
		/// <summary>Represents no paging, sorting, or total row count retrieval capabilities.</summary>
		// Token: 0x04001238 RID: 4664
		None = 0,
		/// <summary>Represents the capability to sort through the rows returned by an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation.</summary>
		// Token: 0x04001239 RID: 4665
		Sort = 1,
		/// <summary>Represents the capability to page through the rows returned by an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation.</summary>
		// Token: 0x0400123A RID: 4666
		Page = 2,
		/// <summary>Represents the capability to retrieve a total row count of data, which corresponds to using the <see cref="F:System.Web.UI.DataSourceOperation.SelectCount" /> value. </summary>
		// Token: 0x0400123B RID: 4667
		RetrieveTotalRowCount = 4
	}
}

using System;
using System.Collections;

namespace System.Web.UI
{
	/// <summary>Represents an abstract data source that data-bound controls bind to.</summary>
	// Token: 0x02000171 RID: 369
	public interface IDataSource
	{
		/// <summary>Occurs when a data source control has changed in some way that affects data-bound controls. </summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000F68 RID: 3944
		// (remove) Token: 0x06000F69 RID: 3945
		event EventHandler DataSourceChanged;

		/// <summary>Gets the named data source view associated with the data source control.</summary>
		/// <returns>Returns the named <see cref="T:System.Web.UI.DataSourceView" /> associated with the <see cref="T:System.Web.UI.IDataSource" />.</returns>
		/// <param name="viewName">The name of the view to retrieve. </param>
		// Token: 0x06000F6A RID: 3946
		DataSourceView GetView(string viewName);

		/// <summary>Gets a collection of names representing the list of view objects associated with the <see cref="T:System.Web.UI.IDataSource" /> interface.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the views associated with the <see cref="T:System.Web.UI.IDataSource" />.</returns>
		// Token: 0x06000F6B RID: 3947
		ICollection GetViewNames();
	}
}

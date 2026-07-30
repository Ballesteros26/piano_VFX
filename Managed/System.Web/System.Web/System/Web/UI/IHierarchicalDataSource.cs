using System;

namespace System.Web.UI
{
	/// <summary>Represents a hierarchical data source that hierarchical data-bound controls such as <see cref="T:System.Web.UI.WebControls.TreeView" /> can bind to.</summary>
	// Token: 0x02000175 RID: 373
	public interface IHierarchicalDataSource
	{
		/// <summary>Occurs when the data storage that the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface represents has changed.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000F72 RID: 3954
		// (remove) Token: 0x06000F73 RID: 3955
		event EventHandler DataSourceChanged;

		/// <summary>Gets the view helper object for the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface for the specified path.</summary>
		/// <returns>Returns a <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> that represents a single view of the data at the hierarchical level identified by the <paramref name="viewPath" /> parameter.</returns>
		/// <param name="viewPath">The hierarchical path of the view to retrieve. </param>
		// Token: 0x06000F74 RID: 3956
		HierarchicalDataSourceView GetHierarchicalView(string viewPath);
	}
}

using System;

namespace System.Web.UI
{
	/// <summary>Represents a data view on a node or collection of nodes in a hierarchical data structure for a <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> control.</summary>
	// Token: 0x020001D4 RID: 468
	public abstract class HierarchicalDataSourceView
	{
		/// <summary>Gets a list of all the data items in the view.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection of data items.</returns>
		// Token: 0x060012FD RID: 4861
		public abstract IHierarchicalEnumerable Select();
	}
}

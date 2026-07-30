using System;

namespace System.Web.UI
{
	/// <summary>Exposes a node of a hierarchical data structure, including the node object and some properties that describe characteristics of the node. Objects that implement the <see cref="T:System.Web.UI.IHierarchyData" /> interface can be contained in <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collections, and are used by ASP.NET site navigation and data source controls.</summary>
	// Token: 0x02000177 RID: 375
	public interface IHierarchyData
	{
		/// <summary>Indicates whether the hierarchical data node that the <see cref="T:System.Web.UI.IHierarchyData" /> object represents has any child nodes.</summary>
		/// <returns>true if the current node has child nodes; otherwise, false.</returns>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000F76 RID: 3958
		bool HasChildren { get; }

		/// <summary>Gets the hierarchical path of the node.</summary>
		/// <returns>A <see cref="T:System.String" /> that identifies the hierarchical path relative to the current node.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000F77 RID: 3959
		string Path { get; }

		/// <summary>Gets the hierarchical data node that the <see cref="T:System.Web.UI.IHierarchyData" /> object represents.</summary>
		/// <returns>An <see cref="T:System.Object" /> hierarchical data node object.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000F78 RID: 3960
		object Item { get; }

		/// <summary>Gets the name of the type of <see cref="T:System.Object" /> contained in the <see cref="P:System.Web.UI.IHierarchyData.Item" /> property.</summary>
		/// <returns>The name of the type of object that the <see cref="T:System.Web.UI.IHierarchyData" /> object represents.</returns>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000F79 RID: 3961
		string Type { get; }

		/// <summary>Gets an enumeration object that represents all the child nodes of the current hierarchical node.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection of child nodes of the current hierarchical node.</returns>
		// Token: 0x06000F7A RID: 3962
		IHierarchicalEnumerable GetChildren();

		/// <summary>Gets an <see cref="T:System.Web.UI.IHierarchyData" /> object that represents the parent node of the current hierarchical node.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchyData" /> object that represents the parent node of the current hierarchical node.</returns>
		// Token: 0x06000F7B RID: 3963
		IHierarchyData GetParent();
	}
}

using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a design-time view of hierarchical data for the <see cref="T:System.Web.SiteMap" /> class.</summary>
	// Token: 0x020001AC RID: 428
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SiteMapDesignerHierarchicalDataSourceView : DesignerHierarchicalDataSourceView
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.SiteMapDesignerHierarchicalDataSourceView" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner" /> that is the designer for the associated <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" />.</param>
		/// <param name="viewPath">An XPath query that defines the block of data to use for the view.</param>
		// Token: 0x06000B9E RID: 2974 RVA: 0x00009519 File Offset: 0x00007719
		public SiteMapDesignerHierarchicalDataSourceView(SiteMapDataSourceDesigner owner, string viewPath)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a design-time view of data for the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> and <see cref="T:System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner" /> classes.</summary>
	// Token: 0x020001AB RID: 427
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SiteMapDesignerDataSourceView : DesignerDataSourceView
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControls.SiteMapDesignerDataSourceView" /> class using the provided <see cref="T:System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner" /> object and view name.</summary>
		/// <param name="owner">The parent <see cref="T:System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner" />.</param>
		/// <param name="viewName">The name of the view for which the data source provides data.</param>
		// Token: 0x06000B9D RID: 2973 RVA: 0x00009519 File Offset: 0x00007719
		public SiteMapDesignerDataSourceView(SiteMapDataSourceDesigner owner, string viewName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

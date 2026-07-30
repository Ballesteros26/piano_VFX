using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a design-time view of data for the <see cref="T:System.Web.UI.Design.WebControls.SqlDataSourceDesigner" /> class.</summary>
	// Token: 0x02000180 RID: 384
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SqlDesignerDataSourceView : DesignerDataSourceView
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControls.SqlDesignerDataSourceView" /> class using the provided data source designer and name of the view.</summary>
		/// <param name="owner">The parent <see cref="T:System.Web.UI.Design.WebControls.SqlDataSourceDesigner" />.</param>
		/// <param name="viewName">The name of the view in the data source.</param>
		// Token: 0x06000B27 RID: 2855 RVA: 0x00009519 File Offset: 0x00007719
		public SqlDesignerDataSourceView(SqlDataSourceDesigner owner, string viewName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a hierarchical design-time view of data for the <see cref="T:System.Web.UI.Design.WebControls.XmlDataSourceDesigner" /> class.</summary>
	// Token: 0x020001BD RID: 445
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDesignerHierarchicalDataSourceView : DesignerHierarchicalDataSourceView
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControls.XmlDesignerHierarchicalDataSourceView" /> class using the provided designer and XPath.</summary>
		/// <param name="owner">An <see cref="T:System.Web.UI.Design.WebControls.XmlDataSourceDesigner" />.</param>
		/// <param name="viewPath">An XPath string that identifies the data for the view.</param>
		// Token: 0x06000BCB RID: 3019 RVA: 0x00009519 File Offset: 0x00007719
		public XmlDesignerHierarchicalDataSourceView(XmlDataSourceDesigner owner, string viewPath)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

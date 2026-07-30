using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
	// Token: 0x020001BB RID: 443
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDataSourceDesigner : HierarchicalDataSourceDesigner, IDataSourceDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.XmlDataSourceDesigner" /> class.</summary>
		// Token: 0x06000BB3 RID: 2995 RVA: 0x00009519 File Offset: 0x00007719
		public XmlDataSourceDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a block of XML that the associated data source control binds to.</summary>
		/// <returns>A string of XML data.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x00009519 File Offset: 0x00007719
		public string Data
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the file name of an XML file that the associated data source control binds to.</summary>
		/// <returns>An XML file name.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x00009519 File Offset: 0x00007719
		public string DataFile
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00016804 File Offset: 0x00014A04
		bool IDataSourceDesigner.get_CanConfigure()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00016820 File Offset: 0x00014A20
		bool IDataSourceDesigner.get_CanRefreshSchema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Gets or sets a block of Extensible Stylesheet Language (XSL) that defines an XSLT transformation to perform on the XML data that is contained by the <see cref="P:System.Web.UI.Design.WebControls.XmlDataSourceDesigner.Data" /> property or by the XML file that is indicated by the <see cref="P:System.Web.UI.Design.WebControls.XmlDataSourceDesigner.DataFile" /> property.</summary>
		/// <returns>An XSL data string.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00009519 File Offset: 0x00007719
		public string Transform
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the file name of an Extensible Stylesheet Language (XSL) file (.xsl) that defines an XSLT transformation to perform on the XML data that is contained by the <see cref="P:System.Web.UI.Design.WebControls.XmlDataSourceDesigner.Data" /> property or by the XML file that is indicated by the <see cref="P:System.Web.UI.Design.WebControls.XmlDataSourceDesigner.DataFile" /> property.</summary>
		/// <returns>The XSLT file name.</returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00009519 File Offset: 0x00007719
		public string TransformFile
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets an XPath query to apply to the XML data that is contained by the <see cref="P:System.Web.UI.Design.WebControls.XmlDataSourceDesigner.Data" /> property or by the XML file that is indicated by the <see cref="P:System.Web.UI.Design.WebControls.XmlDataSourceDesigner.DataFile" /> property.</summary>
		/// <returns>An XPath query.</returns>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x00009519 File Offset: 0x00007719
		public string XPath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="E:System.Web.UI.Design.IDataSourceDesigner.DataSourceChanged" />. </summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000BC0 RID: 3008 RVA: 0x00009519 File Offset: 0x00007719
		// (remove) Token: 0x06000BC1 RID: 3009 RVA: 0x00009519 File Offset: 0x00007719
		event EventHandler IDataSourceDesigner.DataSourceChanged
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="E:System.Web.UI.Design.IDataSourceDesigner.SchemaRefreshed" />.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000BC2 RID: 3010 RVA: 0x00009519 File Offset: 0x00007719
		// (remove) Token: 0x06000BC3 RID: 3011 RVA: 0x00009519 File Offset: 0x00007719
		event EventHandler IDataSourceDesigner.SchemaRefreshed
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.Configure" />.</summary>
		// Token: 0x06000BC4 RID: 3012 RVA: 0x00009519 File Offset: 0x00007719
		void IDataSourceDesigner.Configure()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.GetView(System.String)" />. </summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> object containing information about the identified view, or null if a view with the specified name is not found.</returns>
		/// <param name="viewName">The name of a view in the underlying data source.</param>
		// Token: 0x06000BC5 RID: 3013 RVA: 0x0000970B File Offset: 0x0000790B
		DesignerDataSourceView IDataSourceDesigner.GetView(string viewName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.GetViewNames" />.</summary>
		/// <returns>An array of names of the views in the data source.</returns>
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0000970B File Offset: 0x0000790B
		string[] IDataSourceDesigner.GetViewNames()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.RefreshSchema(System.Boolean)" />.</summary>
		/// <param name="preferSilent">Specifies whether to suppress any events raised while refreshing the schema.</param>
		// Token: 0x06000BC7 RID: 3015 RVA: 0x00009519 File Offset: 0x00007719
		void IDataSourceDesigner.RefreshSchema(bool preferSilent)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.ResumeDataSourceEvents" />.</summary>
		// Token: 0x06000BC8 RID: 3016 RVA: 0x00009519 File Offset: 0x00007719
		void IDataSourceDesigner.ResumeDataSourceEvents()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.SuppressDataSourceEvents" />.</summary>
		// Token: 0x06000BC9 RID: 3017 RVA: 0x00009519 File Offset: 0x00007719
		void IDataSourceDesigner.SuppressDataSourceEvents()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

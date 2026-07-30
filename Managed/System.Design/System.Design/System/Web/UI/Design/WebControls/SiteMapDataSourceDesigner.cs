using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner" /> control.</summary>
	// Token: 0x020001AA RID: 426
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SiteMapDataSourceDesigner : HierarchicalDataSourceDesigner, IDataSourceDesigner
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner" /> class.</summary>
		// Token: 0x06000B8F RID: 2959 RVA: 0x00009519 File Offset: 0x00007719
		public SiteMapDataSourceDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000167B0 File Offset: 0x000149B0
		bool IDataSourceDesigner.get_CanConfigure()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000167CC File Offset: 0x000149CC
		bool IDataSourceDesigner.get_CanRefreshSchema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>For a description of this member, see <see cref="E:System.Web.UI.Design.IDataSourceDesigner.DataSourceChanged" />.</summary>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000B92 RID: 2962 RVA: 0x00009519 File Offset: 0x00007719
		// (remove) Token: 0x06000B93 RID: 2963 RVA: 0x00009519 File Offset: 0x00007719
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
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000B94 RID: 2964 RVA: 0x00009519 File Offset: 0x00007719
		// (remove) Token: 0x06000B95 RID: 2965 RVA: 0x00009519 File Offset: 0x00007719
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

		/// <summary>Gets an array of names representing named views in the data source.</summary>
		/// <returns>This implementation always returns an empty string array.</returns>
		// Token: 0x06000B96 RID: 2966 RVA: 0x0000970B File Offset: 0x0000790B
		public virtual string[] GetViewNames()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.Configure" />.</summary>
		// Token: 0x06000B97 RID: 2967 RVA: 0x00009519 File Offset: 0x00007719
		void IDataSourceDesigner.Configure()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.GetView(System.String)" />.</summary>
		/// <returns>A designer data-source view that contains information about the identified view, or null if a view with the specified name is not found.</returns>
		/// <param name="viewName">The name of the view to get.</param>
		// Token: 0x06000B98 RID: 2968 RVA: 0x0000970B File Offset: 0x0000790B
		DesignerDataSourceView IDataSourceDesigner.GetView(string viewName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.GetViewNames" />.</summary>
		/// <returns>The names of the views in the underlying data source.</returns>
		// Token: 0x06000B99 RID: 2969 RVA: 0x0000970B File Offset: 0x0000790B
		string[] IDataSourceDesigner.GetViewNames()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.RefreshSchema(System.Boolean)" />.</summary>
		/// <param name="preferSilent">true suppresses data source events until the refresh is finished.</param>
		// Token: 0x06000B9A RID: 2970 RVA: 0x00009519 File Offset: 0x00007719
		void IDataSourceDesigner.RefreshSchema(bool preferSilent)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.ResumeDataSourceEvents" /></summary>
		// Token: 0x06000B9B RID: 2971 RVA: 0x00009519 File Offset: 0x00007719
		void IDataSourceDesigner.ResumeDataSourceEvents()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceDesigner.SuppressDataSourceEvents" />.</summary>
		// Token: 0x06000B9C RID: 2972 RVA: 0x00009519 File Offset: 0x00007719
		void IDataSourceDesigner.SuppressDataSourceEvents()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

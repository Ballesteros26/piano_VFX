using System;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	// Token: 0x0200019E RID: 414
	public class MenuDesigner : HierarchicalDataBoundControlDesigner, IDataBindingSchemaProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.MenuDesigner" /> class.</summary>
		// Token: 0x06000B6D RID: 2925 RVA: 0x00009519 File Offset: 0x00007719
		public MenuDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that indicates whether the provider can refresh the schema.</summary>
		/// <returns>true if the schema can be refreshed; otherwise, false.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00016724 File Offset: 0x00014924
		protected bool CanRefreshSchema
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a schema that describes the data source view for the associated <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> object that describes the structure of the data source.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0000970B File Offset: 0x0000790B
		protected IDataSourceViewSchema Schema
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00016740 File Offset: 0x00014940
		bool IDataBindingSchemaProvider.get_CanRefreshSchema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0000970B File Offset: 0x0000790B
		IDataSourceViewSchema IDataBindingSchemaProvider.get_Schema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Refreshes the schema of the data source view for the associated <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <param name="preferSilent">If true, does not display error messages when exceptions occur during processing; otherwise, exception messages are displayed.</param>
		// Token: 0x06000B72 RID: 2930 RVA: 0x00009519 File Offset: 0x00007719
		protected void RefreshSchema(bool preferSilent)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Web.UI.Design.IDataBindingSchemaProvider.RefreshSchema(System.Boolean)" /> method.</summary>
		/// <param name="preferSilent">true to indicate that error messages should not be displayed when exceptions occur during processing; otherwise, false.</param>
		// Token: 0x06000B73 RID: 2931 RVA: 0x00009519 File Offset: 0x00007719
		void IDataBindingSchemaProvider.RefreshSchema(bool preferSilent)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

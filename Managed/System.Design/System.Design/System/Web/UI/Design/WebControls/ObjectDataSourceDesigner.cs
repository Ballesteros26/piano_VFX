using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> Web server control.</summary>
	// Token: 0x020001A2 RID: 418
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ObjectDataSourceDesigner : DataSourceDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.ObjectDataSourceDesigner" /> class. </summary>
		// Token: 0x06000B78 RID: 2936 RVA: 0x00009519 File Offset: 0x00007719
		public ObjectDataSourceDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the method to execute when the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> method of the associated control is called.</summary>
		/// <returns>A string containing the name of the method to execute when the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> is called.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x00009519 File Offset: 0x00007719
		public string SelectMethod
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

		/// <summary>Gets or sets the name of the type that contains the methods that are specified in the associated control.</summary>
		/// <returns>A string containing the name of the type that contains the methods that perform the Delete, Insert, Select, and Update database operations specified in the associated <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x00009519 File Offset: 0x00007719
		public string TypeName
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
	}
}

using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> Web server control.</summary>
	// Token: 0x0200017E RID: 382
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class AccessDataSourceDesigner : SqlDataSourceDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> class.</summary>
		// Token: 0x06000B10 RID: 2832 RVA: 0x00009519 File Offset: 0x00007719
		public AccessDataSourceDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements a designer property to shadow the <see cref="P:System.Web.UI.WebControls.AccessDataSource.DataFile" /> property of the associated control.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name and path of the data file associated with the <see cref="T:System.Web.UI.WebControls.AccessDataSource" />.</returns>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x00009519 File Offset: 0x00007719
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

		/// <summary>Gets the connection string that is valid at design time for the control that is associated with this designer.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the design-time connection string for the associated <see cref="T:System.Web.UI.WebControls.AccessDataSource" />.</returns>
		// Token: 0x06000B13 RID: 2835 RVA: 0x0000970B File Offset: 0x0000790B
		protected override string GetConnectionString()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}

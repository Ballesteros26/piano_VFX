using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls.WebParts
{
	/// <summary>Provides design-time support in a visual designer for a Web Parts zone.</summary>
	// Token: 0x020001C1 RID: 449
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ToolZoneDesigner : WebZoneDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.WebParts.ToolZoneDesigner" /> class. </summary>
		// Token: 0x06000BCF RID: 3023 RVA: 0x00009519 File Offset: 0x00007719
		public ToolZoneDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a value indicating whether the Web Parts zone is displayed when the user is in the browse display mode. </summary>
		/// <returns>true if the Web Parts zone is displayed when the user is in the browse display mode; otherwise, false. </returns>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0001683C File Offset: 0x00014A3C
		protected bool ViewInBrowseMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}
	}
}

using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.MultiView" /> Web server control.</summary>
	// Token: 0x020001A1 RID: 417
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MultiViewDesigner : ContainerControlDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.MultiViewDesigner" /> class. </summary>
		// Token: 0x06000B76 RID: 2934 RVA: 0x00009519 File Offset: 0x00007719
		public MultiViewDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that indicates that the nowrap HTML attribute should not be used.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0001675C File Offset: 0x0001495C
		protected override bool NoWrap
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}
	}
}

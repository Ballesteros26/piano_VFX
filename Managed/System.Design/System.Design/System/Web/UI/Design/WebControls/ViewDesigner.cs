using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.View" /> control.</summary>
	// Token: 0x020001B8 RID: 440
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ViewDesigner : ContainerControlDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.ViewDesigner" /> class.</summary>
		// Token: 0x06000BAD RID: 2989 RVA: 0x00009519 File Offset: 0x00007719
		public ViewDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that indicates that the nowrap HTML attribute should not be used.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000167E8 File Offset: 0x000149E8
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

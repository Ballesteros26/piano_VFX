using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls.WebParts
{
	/// <summary>Provides design-time visual support for <see cref="T:System.Web.UI.WebControls.WebParts.WebZone" /> controls.</summary>
	// Token: 0x020001C2 RID: 450
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class WebZoneDesigner : ControlDesigner
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x00009519 File Offset: 0x00007719
		internal WebZoneDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

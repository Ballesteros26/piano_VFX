using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for a control where the design surface must use a preview of the associated control.</summary>
	// Token: 0x020001A8 RID: 424
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class PreviewControlDesigner : ControlDesigner
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControls.PreviewControlDesigner" /> class.</summary>
		// Token: 0x06000B8D RID: 2957 RVA: 0x00009519 File Offset: 0x00007719
		public PreviewControlDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

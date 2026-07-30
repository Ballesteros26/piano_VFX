using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Used to create a placeholder object whenever the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's <see cref="Overload:System.Web.UI.WebControls.WebParts.WebPartManager.IsAuthorized" /> method returns false for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. This class cannot be inherited.</summary>
	// Token: 0x020007BE RID: 1982
	[ToolboxItem(false)]
	public sealed class UnauthorizedWebPart : ProxyWebPart
	{
		/// <summary>Initializes a new instance of an <see cref="T:System.Web.UI.WebControls.WebParts.UnauthorizedWebPart" /> control, called when a dynamic <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control (or server or user control) fails authorization. </summary>
		/// <param name="originalID">A string that contains the ID of the original server or user control that was added to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> to participate in a Web Parts application. </param>
		/// <param name="originalTypeName">A string containing the name of the original control's type. </param>
		/// <param name="originalPath">A string containing the relative virtual path to the user control, if the original control is a user control. </param>
		/// <param name="genericWebPartID">A string containing the ID of the <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control that wraps the original server or user control. </param>
		// Token: 0x06004FE8 RID: 20456 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public UnauthorizedWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of an <see cref="T:System.Web.UI.WebControls.WebParts.UnauthorizedWebPart" /> control, called when a static <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control (or server or user control) fails authorization.</summary>
		/// <param name="webPart">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that has failed to be authorized by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control. </param>
		// Token: 0x06004FE9 RID: 20457 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public UnauthorizedWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

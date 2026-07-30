using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Supports the ASP.NET page parser in building an instance of a user control.</summary>
	// Token: 0x02000242 RID: 578
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class UserControlControlBuilder : ControlBuilder
	{
		/// <summary>Determines whether the control builder needs to get the control's inner text. </summary>
		/// <returns>true if the control builder requires the control's inner text; otherwise, false. </returns>
		// Token: 0x060017DB RID: 6107 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool NeedsTagInnerText()
		{
			return false;
		}

		/// <summary>Provides the <see cref="T:System.Web.UI.UserControlControlBuilder" /> object with the inner text of the control tag.</summary>
		/// <param name="text">The text to be provided.</param>
		// Token: 0x060017DC RID: 6108 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO("Not implemented, does nothing")]
		public override void SetTagInnerText(string text)
		{
		}

		/// <summary>Builds an instance of the control identified by the <see cref="P:System.Web.UI.ControlBuilder.ControlType" /> property. </summary>
		/// <returns>An instance of a user control identified by <see cref="P:System.Web.UI.ControlBuilder.ControlType" />.</returns>
		// Token: 0x060017DD RID: 6109 RVA: 0x0003DA44 File Offset: 0x0003BC44
		public override object BuildObject()
		{
			return base.BuildObject();
		}
	}
}

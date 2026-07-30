using System;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Interacts with the page parser to build HTML server controls that do not have a body or closing tag. This class cannot be inherited.</summary>
	// Token: 0x02000259 RID: 601
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HtmlEmptyTagControlBuilder : ControlBuilder
	{
		/// <summary>Indicates that the controls built with the <see cref="T:System.Web.UI.HtmlControls.HtmlEmptyTagControlBuilder" /> control do not have closing tags.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x06001888 RID: 6280 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool HasBody()
		{
			return false;
		}
	}
}

using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= submit&gt; element on the server.</summary>
	// Token: 0x0200026A RID: 618
	[SupportsEventValidation]
	[DefaultEvent("ServerClick")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputSubmit : HtmlInputButton, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputSubmit" /> class using default values.</summary>
		// Token: 0x0600194F RID: 6479 RVA: 0x00043DF7 File Offset: 0x00041FF7
		public HtmlInputSubmit()
			: base("submit")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputSubmit" /> class using the specified type.</summary>
		/// <param name="type">The input button type. </param>
		// Token: 0x06001950 RID: 6480 RVA: 0x00043DBC File Offset: 0x00041FBC
		public HtmlInputSubmit(string type)
			: base(type)
		{
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00043E04 File Offset: 0x00042004
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			base.RaisePostBackEvent(eventArgument);
		}
	}
}

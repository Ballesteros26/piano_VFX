using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= password&gt; element on the server.</summary>
	// Token: 0x02000267 RID: 615
	[ValidationProperty("Value")]
	[SupportsEventValidation]
	[DefaultEvent("ServerChange")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputPassword : HtmlInputText, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputPassword" /> class using default values.</summary>
		// Token: 0x06001931 RID: 6449 RVA: 0x00043AEB File Offset: 0x00041CEB
		public HtmlInputPassword()
			: base("password")
		{
		}

		/// <summary>Renders the attributes of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputPassword" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x06001932 RID: 6450 RVA: 0x00043AF8 File Offset: 0x00041CF8
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.Attributes.Remove("value");
			base.RenderAttributes(writer);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00043B14 File Offset: 0x00041D14
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			if (base.Attributes["value"] != text)
			{
				base.Attributes["value"] = text;
				return true;
			}
			return false;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00043B55 File Offset: 0x00041D55
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.OnServerChange(EventArgs.Empty);
		}
	}
}

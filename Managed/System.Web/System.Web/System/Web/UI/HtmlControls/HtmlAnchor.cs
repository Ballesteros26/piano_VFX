using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;a&gt; element on the server.</summary>
	// Token: 0x02000254 RID: 596
	[SupportsEventValidation]
	[DefaultEvent("ServerClick")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlAnchor : HtmlContainerControl, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> class.</summary>
		// Token: 0x06001843 RID: 6211 RVA: 0x00041830 File Offset: 0x0003FA30
		public HtmlAnchor()
			: base("a")
		{
		}

		/// <summary>Gets or sets the URL target of the link specified in the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> server control.</summary>
		/// <returns>The URL target of the link.</returns>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00041840 File Offset: 0x0003FA40
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x00041868 File Offset: 0x0003FA68
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[DefaultValue("")]
		[UrlProperty]
		[WebCategory("Action")]
		public string HRef
		{
			get
			{
				string text = base.Attributes["href"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.Attributes.Remove("href");
					return;
				}
				base.Attributes["href"] = value;
			}
		}

		/// <summary>Gets or sets the bookmark name defined in the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> server control.</summary>
		/// <returns>The bookmark name.</returns>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00041898 File Offset: 0x0003FA98
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x000418C0 File Offset: 0x0003FAC0
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Navigation")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get
			{
				string text = base.Attributes["name"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.Attributes.Remove("name");
					return;
				}
				base.Attributes["name"] = value;
			}
		}

		/// <summary>Gets or sets the name of the browser window or frame that displays the contents of the Web page that is linked to when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control is clicked. </summary>
		/// <returns>The browser window or frame that displays the contents of the Web page linked to when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> is clicked. The default is an empty string ("").</returns>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x000418F0 File Offset: 0x0003FAF0
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x00041918 File Offset: 0x0003FB18
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Navigation")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public string Target
		{
			get
			{
				string text = base.Attributes["target"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.Attributes.Remove("target");
					return;
				}
				base.Attributes["target"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text displayed when the mouse pointer is placed over the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control.</summary>
		/// <returns>The text displayed when the mouse pointer is placed over the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" />.</returns>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00041948 File Offset: 0x0003FB48
		// (set) Token: 0x0600184B RID: 6219 RVA: 0x00041970 File Offset: 0x0003FB70
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDescription("")]
		[DefaultValue("")]
		public string Title
		{
			get
			{
				string text = base.Attributes["title"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.Attributes.Remove("title");
					return;
				}
				base.Attributes["title"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control is clicked. </summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> is clicked; otherwise, false. The default is true.</returns>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0004199F File Offset: 0x0003FB9F
		// (set) Token: 0x0600184D RID: 6221 RVA: 0x000419B2 File Offset: 0x0003FBB2
		[DefaultValue(true)]
		public virtual bool CausesValidation
		{
			get
			{
				return this.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x000419CA File Offset: 0x0003FBCA
		// (set) Token: 0x0600184F RID: 6223 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event and registers client script for generating a postback.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001850 RID: 6224 RVA: 0x000419F4 File Offset: 0x0003FBF4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlAnchor.ServerClick" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x06001851 RID: 6225 RVA: 0x00041A00 File Offset: 0x0003FC00
		protected virtual void OnServerClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlAnchor.serverClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlAnchor.HRef" /> contains a malformed URL.</exception>
		// Token: 0x06001852 RID: 6226 RVA: 0x00041A30 File Offset: 0x0003FC30
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if ((EventHandler)base.Events[HtmlAnchor.serverClickEvent] != null)
			{
				PostBackOptions postBackOptions = this.GetPostBackOptions();
				ClientScriptManager clientScript = this.Page.ClientScript;
				clientScript.RegisterForEventValidation(postBackOptions);
				base.Attributes["href"] = clientScript.GetPostBackEventReference(postBackOptions, true);
			}
			else
			{
				string href = this.HRef;
				if (href != string.Empty)
				{
					this.HRef = base.ResolveClientUrl(href);
				}
			}
			base.RenderAttributes(writer);
			base.Attributes.Remove("href");
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control when it posts back to the server. </summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001853 RID: 6227 RVA: 0x00041AC0 File Offset: 0x0003FCC0
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(EventArgs.Empty);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00041AF4 File Offset: 0x0003FCF4
		private PostBackOptions GetPostBackOptions()
		{
			Page page = this.Page;
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ValidationGroup = null;
			postBackOptions.ActionUrl = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = true;
			postBackOptions.ClientSubmit = true;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Page.RaisePostBackEvent(System.Web.UI.IPostBackEventHandler,System.String)" />.</summary>
		/// <param name="eventArgument">The event arguments.</param>
		// Token: 0x06001855 RID: 6229 RVA: 0x00041B6B File Offset: 0x0003FD6B
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control is clicked.</summary>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06001856 RID: 6230 RVA: 0x00041B74 File Offset: 0x0003FD74
		// (remove) Token: 0x06001857 RID: 6231 RVA: 0x00041B87 File Offset: 0x0003FD87
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlAnchor.serverClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlAnchor.serverClickEvent, value);
			}
		}

		// Token: 0x04001622 RID: 5666
		private static readonly object serverClickEvent = new object();
	}
}

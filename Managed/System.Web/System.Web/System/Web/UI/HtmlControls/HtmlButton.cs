using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;button&gt; tag on the server.</summary>
	// Token: 0x02000255 RID: 597
	[SupportsEventValidation]
	[DefaultEvent("ServerClick")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlButton : HtmlContainerControl, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> class.</summary>
		// Token: 0x06001859 RID: 6233 RVA: 0x00041BA6 File Offset: 0x0003FDA6
		public HtmlButton()
			: base("button")
		{
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0004199F File Offset: 0x0003FB9F
		// (set) Token: 0x0600185B RID: 6235 RVA: 0x000419B2 File Offset: 0x0003FBB2
		[WebSysDescription("")]
		[DefaultValue(true)]
		[WebCategory("Behavior")]
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

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control causes validation when it posts back to the server. The default value is an empty string ("") indicating that this property is not set.</returns>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x00041BB3 File Offset: 0x0003FDB3
		// (set) Token: 0x0600185D RID: 6237 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", "");
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The event arguments.</param>
		// Token: 0x0600185E RID: 6238 RVA: 0x00041BCA File Offset: 0x0003FDCA
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control when it posts back to the server. </summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x0600185F RID: 6239 RVA: 0x00041BD3 File Offset: 0x0003FDD3
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnServerClick(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event and registers client script for generating a postback.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06001860 RID: 6240 RVA: 0x000419F4 File Offset: 0x0003FBF4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlButton.ServerClick" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001861 RID: 6241 RVA: 0x00041C08 File Offset: 0x0003FE08
		protected virtual void OnServerClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlButton.ServerClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x06001862 RID: 6242 RVA: 0x00041C38 File Offset: 0x0003FE38
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null && base.Events[HtmlButton.ServerClickEvent] != null)
			{
				PostBackOptions postBackOptions = this.GetPostBackOptions();
				AttributeCollection attributes = base.Attributes;
				attributes["onclick"] = attributes["onclick"] + page.ClientScript.GetPostBackEventReference(postBackOptions, true);
				writer.WriteAttribute("language", "javascript");
			}
			base.RenderAttributes(writer);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00041CB0 File Offset: 0x0003FEB0
		private PostBackOptions GetPostBackOptions()
		{
			Page page = this.Page;
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ValidationGroup = null;
			postBackOptions.ActionUrl = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = true;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Occurs when the user clicks an <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control on the client Web page.</summary>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06001864 RID: 6244 RVA: 0x00041D27 File Offset: 0x0003FF27
		// (remove) Token: 0x06001865 RID: 6245 RVA: 0x00041D3A File Offset: 0x0003FF3A
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlButton.ServerClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlButton.ServerClickEvent, value);
			}
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00041D4D File Offset: 0x0003FF4D
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlButton()
		{
			HtmlButton.ServerClickEvent = new object();
		}
	}
}

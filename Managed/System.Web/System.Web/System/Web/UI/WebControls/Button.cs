using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a push button control on the Web page.</summary>
	// Token: 0x02000340 RID: 832
	[Designer("System.Web.UI.Design.WebControls.ButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[SupportsEventValidation]
	[ToolboxData("<{0}:Button runat=\"server\" Text=\"Button\"></{0}:Button>")]
	[DefaultEvent("Click")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Button : WebControl, IPostBackEventHandler, IButtonControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Button" /> class.</summary>
		// Token: 0x06001D99 RID: 7577 RVA: 0x00049F8B File Offset: 0x0004818B
		public Button()
			: base(HtmlTextWriterTag.Input)
		{
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0004199F File Offset: 0x0003FB9F
		// (set) Token: 0x06001D9B RID: 7579 RVA: 0x000419B2 File Offset: 0x0003FBB2
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
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

		/// <summary>Gets or sets an optional parameter passed to the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event along with the associated <see cref="P:System.Web.UI.WebControls.Button.CommandName" />.</summary>
		/// <returns>An optional parameter passed to the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event along with the associated <see cref="P:System.Web.UI.WebControls.Button.CommandName" />. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x00049F95 File Offset: 0x00048195
		// (set) Token: 0x06001D9D RID: 7581 RVA: 0x00049FAC File Offset: 0x000481AC
		[DefaultValue("")]
		[Bindable(true)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		public string CommandArgument
		{
			get
			{
				return this.ViewState.GetString("CommandArgument", string.Empty);
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		/// <summary>Gets or sets the command name associated with the <see cref="T:System.Web.UI.WebControls.Button" /> control that is passed to the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event.</summary>
		/// <returns>The command name of the <see cref="T:System.Web.UI.WebControls.Button" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x00049FBF File Offset: 0x000481BF
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x00049FD6 File Offset: 0x000481D6
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public string CommandName
		{
			get
			{
				return this.ViewState.GetString("CommandName", string.Empty);
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		/// <summary>Gets or sets the client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button" /> control's <see cref="E:System.Web.UI.WebControls.Button.Click" /> event is raised.</summary>
		/// <returns>The client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button" /> control's <see cref="E:System.Web.UI.WebControls.Button.Click" /> event is raised.</returns>
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x00049FE9 File Offset: 0x000481E9
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x0004A000 File Offset: 0x00048200
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual string OnClientClick
		{
			get
			{
				return this.ViewState.GetString("OnClientClick", string.Empty);
			}
			set
			{
				this.ViewState["OnClientClick"] = value;
			}
		}

		/// <summary>Gets or sets the text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button" /> control.</summary>
		/// <returns>The text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0004A013 File Offset: 0x00048213
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x0004A02A File Offset: 0x0004822A
		[Localizable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Button" /> control uses the client browser's submit mechanism or the ASP.NET postback mechanism.</summary>
		/// <returns>true if the control uses the client browser's submit mechanism; otherwise, false. The default is true.</returns>
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0004A03D File Offset: 0x0004823D
		// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x0004A050 File Offset: 0x00048250
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool UseSubmitBehavior
		{
			get
			{
				return this.ViewState.GetBool("UseSubmitBehavior", true);
			}
			set
			{
				this.ViewState["UseSubmitBehavior"] = value;
			}
		}

		/// <summary>Adds the attributes of the <see cref="T:System.Web.UI.WebControls.Button" /> control to the output stream for rendering on the client.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
		// Token: 0x06001DA6 RID: 7590 RVA: 0x0004A068 File Offset: 0x00048268
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, this.UseSubmitBehavior ? "submit" : "button", false);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Text);
			string text = this.OnClientClick;
			text = ClientScriptManager.EnsureEndsWithSemicolon(text);
			if (base.HasAttributes && base.Attributes["onclick"] != null)
			{
				text = ClientScriptManager.EnsureEndsWithSemicolon(text + base.Attributes["onclick"]);
				base.Attributes.Remove("onclick");
			}
			if (page != null)
			{
				text += this.GetClientScriptEventReference();
			}
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0004A13C File Offset: 0x0004833C
		internal virtual string GetClientScriptEventReference()
		{
			PostBackOptions postBackOptions = this.GetPostBackOptions();
			Page page = this.Page;
			if (page != null)
			{
				return page.ClientScript.GetPostBackEventReference(postBackOptions, true);
			}
			return string.Empty;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the <see cref="T:System.Web.UI.WebControls.Button" /> control's postback behavior.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the <see cref="T:System.Web.UI.WebControls.Button" /> control's postback behavior.</returns>
		// Token: 0x06001DA8 RID: 7592 RVA: 0x0004A170 File Offset: 0x00048370
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ActionUrl = ((this.PostBackUrl.Length > 0) ? this.Page.ResolveClientUrl(this.PostBackUrl) : null);
			postBackOptions.ValidationGroup = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = !this.UseSubmitBehavior;
			Page page = this.Page;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.Button" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001DA9 RID: 7593 RVA: 0x0004A210 File Offset: 0x00048410
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Button.Click" /> event of the <see cref="T:System.Web.UI.WebControls.Button" /> control.</summary>
		/// <param name="e">The event data. </param>
		// Token: 0x06001DAA RID: 7594 RVA: 0x0004A21C File Offset: 0x0004841C
		protected virtual void OnClick(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Button.ClickEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event of the <see cref="T:System.Web.UI.WebControls.Button" /> control.</summary>
		/// <param name="e">The event data. </param>
		// Token: 0x06001DAB RID: 7595 RVA: 0x0004A254 File Offset: 0x00048454
		protected virtual void OnCommand(CommandEventArgs e)
		{
			if (base.Events != null)
			{
				CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[Button.CommandEvent];
				if (commandEventHandler != null)
				{
					commandEventHandler(this, e);
				}
			}
			base.RaiseBubbleEvent(this, e);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.Button" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001DAC RID: 7596 RVA: 0x0004A294 File Offset: 0x00048494
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.Validate(this.ValidationGroup);
				}
			}
			this.OnClick(EventArgs.Empty);
			this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
		}

		/// <summary>Determines whether the button has been clicked prior to rendering on the client.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06001DAD RID: 7597 RVA: 0x000419F4 File Offset: 0x0003FBF4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the contents of the control to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001DAE RID: 7598 RVA: 0x0000393A File Offset: 0x00001B3A
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06001DAF RID: 7599 RVA: 0x0004A2EE File Offset: 0x000484EE
		// (remove) Token: 0x06001DB0 RID: 7600 RVA: 0x0004A301 File Offset: 0x00048501
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(Button.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Button.ClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06001DB1 RID: 7601 RVA: 0x0004A314 File Offset: 0x00048514
		// (remove) Token: 0x06001DB2 RID: 7602 RVA: 0x0004A327 File Offset: 0x00048527
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(Button.CommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Button.CommandEvent, value);
			}
		}

		/// <summary>Gets or sets the URL of the page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
		/// <returns>The URL of the Web page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked. The default value is an empty string (""), which causes the page to post back to itself.</returns>
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0004A33A File Offset: 0x0004853A
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x0004A351 File Offset: 0x00048551
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Themeable(false)]
		[UrlProperty("*.aspx")]
		public virtual string PostBackUrl
		{
			get
			{
				return this.ViewState.GetString("PostBackUrl", string.Empty);
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.Button" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.Button" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x000419CA File Offset: 0x0003FBCA
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[DefaultValue("")]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
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

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0004A364 File Offset: 0x00048564
		// Note: this type is marked as 'beforefieldinit'.
		static Button()
		{
			Button.ClickEvent = new object();
			Button.CommandEvent = new object();
		}
	}
}

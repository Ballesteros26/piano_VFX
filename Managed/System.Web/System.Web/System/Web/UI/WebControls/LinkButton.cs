using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a hyperlink-style button control on a Web page.</summary>
	// Token: 0x020003BD RID: 957
	[ParseChildren(false)]
	[ToolboxData("<{0}:LinkButton runat=\"server\">LinkButton</{0}:LinkButton>")]
	[SupportsEventValidation]
	[Designer("System.Web.UI.Design.WebControls.LinkButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ControlBuilder(typeof(LinkButtonControlBuilder))]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LinkButton : WebControl, IPostBackEventHandler, IButtonControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> class.</summary>
		// Token: 0x06002770 RID: 10096 RVA: 0x00064FA1 File Offset: 0x000631A1
		public LinkButton()
			: base(HtmlTextWriterTag.A)
		{
		}

		/// <summary>Adds the attributes of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control to the output stream for rendering on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x06002771 RID: 10097 RVA: 0x00066BA8 File Offset: 0x00064DA8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			base.AddAttributesToRender(writer);
			bool isEnabled = base.IsEnabled;
			string text = this.OnClientClick;
			text = ClientScriptManager.EnsureEndsWithSemicolon(text);
			if (base.HasAttributes && base.Attributes["onclick"] != null)
			{
				text = ClientScriptManager.EnsureEndsWithSemicolon(text + base.Attributes["onclick"]);
				base.Attributes.Remove("onclick");
			}
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
			}
			if (isEnabled && page != null)
			{
				PostBackOptions postBackOptions = this.GetPostBackOptions();
				string postBackEventReference = page.ClientScript.GetPostBackEventReference(postBackOptions, true);
				writer.AddAttribute(HtmlTextWriterAttribute.Href, postBackEventReference);
			}
			base.AddDisplayStyleAttribute(writer);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06002772 RID: 10098 RVA: 0x00066C64 File Offset: 0x00064E64
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

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />. </summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06002773 RID: 10099 RVA: 0x00066CBE File Offset: 0x00064EBE
		void IPostBackEventHandler.RaisePostBackEvent(string ea)
		{
			this.RaisePostBackEvent(ea);
		}

		/// <summary>Notifies the control that an element, either XML or HTML, was parsed, and adds the element to the control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
		// Token: 0x06002774 RID: 10100 RVA: 0x00066CC8 File Offset: 0x00064EC8
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl == null)
			{
				string text = this.Text;
				if (text.Length != 0)
				{
					this.Text = null;
					this.Controls.Add(new LiteralControl(text));
				}
				base.AddParsedSubObject(obj);
				return;
			}
			this.Text = literalControl.Text;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's postback behavior.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's postback behavior.</returns>
		// Token: 0x06002775 RID: 10101 RVA: 0x00066D2C File Offset: 0x00064F2C
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			Page page = this.Page;
			postBackOptions.ActionUrl = ((this.PostBackUrl.Length > 0) ? ((page != null) ? page.ResolveClientUrl(this.PostBackUrl) : this.PostBackUrl) : null);
			postBackOptions.ValidationGroup = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.ClientSubmit = true;
			postBackOptions.RequiresJavaScriptProtocol = true;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <param name="savedState">The saved state to retrieve.</param>
		// Token: 0x06002776 RID: 10102 RVA: 0x00066DCA File Offset: 0x00064FCA
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["Text"] != null)
			{
				this.Text = (string)this.ViewState["Text"];
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06002777 RID: 10103 RVA: 0x000419F4 File Offset: 0x0003FBF4
		[global::System.MonoTODO("Why override?")]
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the contents of the control to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002778 RID: 10104 RVA: 0x00066E00 File Offset: 0x00065000
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x0004199F File Offset: 0x0003FB9F
		// (set) Token: 0x0600277A RID: 10106 RVA: 0x000419B2 File Offset: 0x0003FBB2
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[WebSysDescription("")]
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

		/// <summary>Gets or sets an optional argument passed to the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event handler along with the associated <see cref="P:System.Web.UI.WebControls.LinkButton.CommandName" /> property.</summary>
		/// <returns>An optional argument passed to the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event handler along with the associated <see cref="P:System.Web.UI.WebControls.LinkButton.CommandName" /> property. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x00049F95 File Offset: 0x00048195
		// (set) Token: 0x0600277C RID: 10108 RVA: 0x00049FAC File Offset: 0x000481AC
		[Bindable(true)]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the command name associated with the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control. This value is passed to the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event handler along with the <see cref="P:System.Web.UI.WebControls.LinkButton.CommandArgument" /> property.</summary>
		/// <returns>The command name of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x00049FBF File Offset: 0x000481BF
		// (set) Token: 0x0600277E RID: 10110 RVA: 0x00049FD6 File Offset: 0x000481D6
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
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

		/// <summary>Gets or sets the client-side script that executes when a <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event is raised</summary>
		/// <returns>The client-side script that executes when a <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event is raised.</returns>
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x00049FE9 File Offset: 0x000481E9
		// (set) Token: 0x06002780 RID: 10112 RVA: 0x0004A000 File Offset: 0x00048200
		[WebSysDescription("")]
		[Themeable(false)]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the text caption displayed on the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
		/// <returns>The text caption displayed on the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x0004A013 File Offset: 0x00048213
		// (set) Token: 0x06002782 RID: 10114 RVA: 0x0006514D File Offset: 0x0006334D
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[Bindable(true)]
		public virtual string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002783 RID: 10115 RVA: 0x00066E28 File Offset: 0x00065028
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LinkButton.ClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06002784 RID: 10116 RVA: 0x00066E56 File Offset: 0x00065056
		// (remove) Token: 0x06002785 RID: 10117 RVA: 0x00066E69 File Offset: 0x00065069
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(LinkButton.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinkButton.ClickEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06002786 RID: 10118 RVA: 0x00066E7C File Offset: 0x0006507C
		protected virtual void OnCommand(CommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[LinkButton.CommandEvent];
			if (commandEventHandler != null)
			{
				commandEventHandler(this, e);
			}
			base.RaiseBubbleEvent(this, e);
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x06002787 RID: 10119 RVA: 0x00066EB2 File Offset: 0x000650B2
		// (remove) Token: 0x06002788 RID: 10120 RVA: 0x00066EC5 File Offset: 0x000650C5
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(LinkButton.CommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinkButton.CommandEvent, value);
			}
		}

		/// <summary>Gets or sets the URL of the page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
		/// <returns>The URL of the Web page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked. The default value is an empty string (""), which causes the page to post back to itself.</returns>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x0004A33A File Offset: 0x0004853A
		// (set) Token: 0x0600278A RID: 10122 RVA: 0x0004A351 File Offset: 0x00048551
		[UrlProperty("*.aspx")]
		[Themeable(false)]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x000419CA File Offset: 0x0003FBCA
		// (set) Token: 0x0600278C RID: 10124 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
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

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00066ED8 File Offset: 0x000650D8
		// Note: this type is marked as 'beforefieldinit'.
		static LinkButton()
		{
			LinkButton.ClickEvent = new object();
			LinkButton.CommandEvent = new object();
		}
	}
}

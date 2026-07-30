using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A control that displays an image and responds to mouse clicks on the image.</summary>
	// Token: 0x020003B8 RID: 952
	[DefaultEvent("Click")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ImageButton : Image, IPostBackDataHandler, IPostBackEventHandler, IButtonControl
	{
		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x0004199F File Offset: 0x0003FB9F
		// (set) Token: 0x06002707 RID: 9991 RVA: 0x000419B2 File Offset: 0x0003FBB2
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
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

		/// <summary>Gets or sets an optional argument that provides additional information about the <see cref="P:System.Web.UI.WebControls.ImageButton.CommandName" /> property.</summary>
		/// <returns>An optional argument that supplements the <see cref="P:System.Web.UI.WebControls.ImageButton.CommandName" /> property.</returns>
		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x00049F95 File Offset: 0x00048195
		// (set) Token: 0x06002709 RID: 9993 RVA: 0x00049FAC File Offset: 0x000481AC
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

		/// <summary>Gets or sets the command name associated with the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control.</summary>
		/// <returns>The command name associated with the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x00049FBF File Offset: 0x000481BF
		// (set) Token: 0x0600270B RID: 9995 RVA: 0x00049FD6 File Offset: 0x000481D6
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ImageButton" /> can be clicked to perform a post back to the server.</summary>
		/// <returns>true if the control is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x00065BAD File Offset: 0x00063DAD
		// (set) Token: 0x0600270D RID: 9997 RVA: 0x00065BB5 File Offset: 0x00063DB5
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(true)]
		[Bindable(true)]
		public new virtual bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control generates an alternate-text attribute for an empty string value. </summary>
		/// <returns>false, indicating that the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control does not generate an alternate-text attribute when the <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property is empty.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set this property.</exception>
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x0600270F RID: 9999 RVA: 0x00003A01 File Offset: 0x00001C01
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override bool GenerateEmptyAlternateText
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the client-side script that executes when an <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event is raised.</summary>
		/// <returns>The client-side script that executes when an <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event is raised.</returns>
		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x00049FE9 File Offset: 0x000481E9
		// (set) Token: 0x06002711 RID: 10001 RVA: 0x0004A000 File Offset: 0x00048200
		[DefaultValue("")]
		[Themeable(false)]
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

		/// <summary>Gets or sets the URL of the page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked.</summary>
		/// <returns>The URL of the Web page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked. The default value is an empty string (""), which causes the page to post back to itself.</returns>
		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x0004A33A File Offset: 0x0004853A
		// (set) Token: 0x06002713 RID: 10003 RVA: 0x0004A351 File Offset: 0x00048551
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty("*.aspx")]
		[DefaultValue("")]
		[Themeable(false)]
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

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x000419CA File Offset: 0x0003FBCA
		// (set) Token: 0x06002715 RID: 10005 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[WebSysDescription("")]
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

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value.</returns>
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06002716 RID: 10006 RVA: 0x00065BBE File Offset: 0x00063DBE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property.</summary>
		/// <returns>The value of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property.</returns>
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x00065BC2 File Offset: 0x00063DC2
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x00065BCA File Offset: 0x00063DCA
		protected virtual string Text
		{
			get
			{
				return this.AlternateText;
			}
			set
			{
				this.AlternateText = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds the attributes of an <see cref="T:System.Web.UI.WebControls.ImageButton" /> to the output stream for rendering on the client.</summary>
		/// <param name="writer">The output stream to render on the client. </param>
		// Token: 0x0600271A RID: 10010 RVA: 0x00065BD4 File Offset: 0x00063DD4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "image", false);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			base.AddAttributesToRender(writer);
			string text = this.OnClientClick;
			if (!string.IsNullOrEmpty(text))
			{
				text = ClientScriptManager.EnsureEndsWithSemicolon(text);
			}
			else
			{
				text = string.Empty;
			}
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
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x00065C9C File Offset: 0x00063E9C
		internal virtual string GetClientScriptEventReference()
		{
			PostBackOptions postBackOptions = this.GetPostBackOptions();
			Page page = this.Page;
			if (!postBackOptions.PerformValidation && string.IsNullOrEmpty(postBackOptions.ActionUrl))
			{
				if (page != null)
				{
					page.ClientScript.RegisterForEventValidation(postBackOptions);
				}
				return string.Empty;
			}
			if (page == null)
			{
				return string.Empty;
			}
			return page.ClientScript.GetPostBackEventReference(postBackOptions, true);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's postback behavior.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's postback behavior.</returns>
		// Token: 0x0600271C RID: 10012 RVA: 0x00065CF8 File Offset: 0x00063EF8
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			Page page = this.Page;
			postBackOptions.ActionUrl = ((this.PostBackUrl.Length > 0) ? ((page != null) ? page.ResolveClientUrl(this.PostBackUrl) : null) : null);
			postBackOptions.Argument = string.Empty;
			postBackOptions.ClientSubmit = true;
			postBackOptions.RequiresJavaScriptProtocol = true;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			else
			{
				postBackOptions.ValidationGroup = null;
			}
			return postBackOptions;
		}

		/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control.</summary>
		/// <returns>Returns false for all cases.</returns>
		/// <param name="postDataKey">The key value used to index an entry in the collection. </param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains post information.</param>
		// Token: 0x0600271D RID: 10013 RVA: 0x00065D94 File Offset: 0x00063F94
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string uniqueID = this.UniqueID;
			string text = postCollection[uniqueID + ".x"];
			string text2 = postCollection[uniqueID + ".y"];
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				this.pos_x = int.Parse(text);
				this.pos_y = int.Parse(text2);
				this.Page.RegisterRequiresRaiseEvent(this);
				return true;
			}
			text = postCollection[uniqueID];
			if (!string.IsNullOrEmpty(text))
			{
				this.pos_x = int.Parse(text);
				this.pos_y = 0;
				this.Page.RegisterRequiresRaiseEvent(this);
				return true;
			}
			return false;
		}

		/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control has changed.</summary>
		// Token: 0x0600271E RID: 10014 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x0600271F RID: 10015 RVA: 0x00065E34 File Offset: 0x00064034
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			if (this.CausesValidation)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.Validate(this.ValidationGroup);
				}
			}
			this.OnClick(new ImageClickEventArgs(this.pos_x, this.pos_y));
			this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />.</summary>
		/// <returns>true if the server control's state changes as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> collection that contains value information indexed by control identifiers. </param>
		// Token: 0x06002720 RID: 10016 RVA: 0x00065E9E File Offset: 0x0006409E
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
		// Token: 0x06002721 RID: 10017 RVA: 0x00065EA8 File Offset: 0x000640A8
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
		/// <param name="eventArgument">The argument for the event</param>
		// Token: 0x06002722 RID: 10018 RVA: 0x00065EB0 File Offset: 0x000640B0
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event and allows you to handle the <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event directly.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.ImageClickEventArgs" /> that contains the event data. </param>
		// Token: 0x06002723 RID: 10019 RVA: 0x00065EBC File Offset: 0x000640BC
		protected virtual void OnClick(ImageClickEventArgs e)
		{
			if (base.Events != null)
			{
				ImageClickEventHandler imageClickEventHandler = (ImageClickEventHandler)base.Events[ImageButton.ClickEvent];
				if (imageClickEventHandler != null)
				{
					imageClickEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ImageButton.Command" /> event and allows you to handle the <see cref="E:System.Web.UI.WebControls.ImageButton.Command" /> event directly.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06002724 RID: 10020 RVA: 0x00065EF4 File Offset: 0x000640F4
		protected virtual void OnCommand(CommandEventArgs e)
		{
			if (base.Events != null)
			{
				CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[ImageButton.CommandEvent];
				if (commandEventHandler != null)
				{
					commandEventHandler(this, e);
				}
			}
			base.RaiseBubbleEvent(this, e);
		}

		/// <summary>Determines whether the image has been clicked prior to rendering on the client.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06002725 RID: 10021 RVA: 0x00065F34 File Offset: 0x00064134
		protected internal override void OnPreRender(EventArgs e)
		{
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				page.RegisterRequiresPostBack(this);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> is clicked.</summary>
		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06002726 RID: 10022 RVA: 0x00065F5A File Offset: 0x0006415A
		// (remove) Token: 0x06002727 RID: 10023 RVA: 0x00065F6D File Offset: 0x0006416D
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event ImageClickEventHandler Click
		{
			add
			{
				base.Events.AddHandler(ImageButton.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageButton.ClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> is clicked.</summary>
		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06002728 RID: 10024 RVA: 0x00065F80 File Offset: 0x00064180
		// (remove) Token: 0x06002729 RID: 10025 RVA: 0x00065F93 File Offset: 0x00064193
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(ImageButton.CommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageButton.CommandEvent, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IButtonControl.Text" />.</summary>
		/// <returns>The text caption that is displayed for the button.</returns>
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x00065FA6 File Offset: 0x000641A6
		// (set) Token: 0x0600272B RID: 10027 RVA: 0x00065FAE File Offset: 0x000641AE
		string IButtonControl.Text
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		/// <summary>For a description of this member, see the <see cref="E:System.Web.UI.WebControls.IButtonControl.Click" /> event.</summary>
		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x0600272C RID: 10028 RVA: 0x00065F5A File Offset: 0x0006415A
		// (remove) Token: 0x0600272D RID: 10029 RVA: 0x00065F6D File Offset: 0x0006416D
		event EventHandler IButtonControl.Click
		{
			add
			{
				base.Events.AddHandler(ImageButton.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageButton.ClickEvent, value);
			}
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x00065FB7 File Offset: 0x000641B7
		// Note: this type is marked as 'beforefieldinit'.
		static ImageButton()
		{
			ImageButton.ClickEvent = new object();
			ImageButton.CommandEvent = new object();
		}

		// Token: 0x04001A57 RID: 6743
		private int pos_x;

		// Token: 0x04001A58 RID: 6744
		private int pos_y;
	}
}

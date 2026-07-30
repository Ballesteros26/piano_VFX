using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	/// <summary>Detects the user's authentication state and toggles the state of a link to log in to or log out of a Web site.</summary>
	// Token: 0x020003CB RID: 971
	[Designer("System.Web.UI.Design.WebControls.LoginStatusDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Bindable(false)]
	[DefaultEvent("LoggingOut")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LoginStatus : CompositeControl
	{
		/// <summary>Gets or sets the URL of the image used for the login link.</summary>
		/// <returns>A string containing the URL of the image used for the login link. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x0006A3DC File Offset: 0x000685DC
		// (set) Token: 0x060028B4 RID: 10420 RVA: 0x0006A409 File Offset: 0x00068609
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string LoginImageUrl
		{
			get
			{
				object obj = this.ViewState["LoginImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LoginImageUrl");
					return;
				}
				this.ViewState["LoginImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text used for the login link.</summary>
		/// <returns>A string displayed as the login link. The default is "Login".</returns>
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x0006A430 File Offset: 0x00068630
		// (set) Token: 0x060028B6 RID: 10422 RVA: 0x0006A462 File Offset: 0x00068662
		[Localizable(true)]
		public virtual string LoginText
		{
			get
			{
				object obj = this.ViewState["LoginText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Login");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LoginText");
					return;
				}
				this.ViewState["LoginText"] = value;
			}
		}

		/// <summary>Gets or sets a value that determines the action taken when a user logs out of a Web site with the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LogoutAction" /> values. The default is <see cref="F:System.Web.UI.WebControls.LogoutAction.Refresh" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to an invalid <see cref="T:System.Web.UI.WebControls.LogoutAction" /> value. </exception>
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060028B7 RID: 10423 RVA: 0x0006A48C File Offset: 0x0006868C
		// (set) Token: 0x060028B8 RID: 10424 RVA: 0x0006A4B5 File Offset: 0x000686B5
		[Themeable(false)]
		[DefaultValue(LogoutAction.Refresh)]
		public virtual LogoutAction LogoutAction
		{
			get
			{
				object obj = this.ViewState["LogoutAction"];
				if (obj != null)
				{
					return (LogoutAction)obj;
				}
				return LogoutAction.Refresh;
			}
			set
			{
				if (value < LogoutAction.Refresh || value > LogoutAction.RedirectToLoginPage)
				{
					throw new ArgumentOutOfRangeException("LogoutAction");
				}
				this.ViewState["LogoutAction"] = (int)value;
			}
		}

		/// <summary>Gets or sets the URL of the image used for the logout button.</summary>
		/// <returns>A string containing the URL of the image used for the logout link. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060028B9 RID: 10425 RVA: 0x0006A4E0 File Offset: 0x000686E0
		// (set) Token: 0x060028BA RID: 10426 RVA: 0x0006A50D File Offset: 0x0006870D
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string LogoutImageUrl
		{
			get
			{
				object obj = this.ViewState["LogoutImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LogoutImageUrl");
					return;
				}
				this.ViewState["LogoutImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the logout page.</summary>
		/// <returns>A string containing the URL of the logout page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x0006A534 File Offset: 0x00068734
		// (set) Token: 0x060028BC RID: 10428 RVA: 0x0006A561 File Offset: 0x00068761
		[Themeable(false)]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string LogoutPageUrl
		{
			get
			{
				object obj = this.ViewState["LogoutPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LogoutPageUrl");
					return;
				}
				this.ViewState["LogoutPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text used for the logout link.</summary>
		/// <returns>A string displayed as the logout link. The default is "Logout".</returns>
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x0006A588 File Offset: 0x00068788
		// (set) Token: 0x060028BE RID: 10430 RVA: 0x0006A5BA File Offset: 0x000687BA
		[Localizable(true)]
		public virtual string LogoutText
		{
			get
			{
				object obj = this.ViewState["LogoutText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Logout");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LogoutText");
					return;
				}
				this.ViewState["LogoutText"] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
		/// <returns>Always returns <see cref="F:System.Web.UI.HtmlTextWriterTag.A" />.</returns>
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x00008B66 File Offset: 0x00006D66
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		/// <summary>Creates the child controls that make up the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
		// Token: 0x060028C0 RID: 10432 RVA: 0x0006A5E4 File Offset: 0x000687E4
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this.logoutLinkButton = new LinkButton();
			this.logoutLinkButton.CausesValidation = false;
			this.logoutLinkButton.Command += this.LogoutClick;
			this.logoutImageButton = new ImageButton();
			this.logoutImageButton.CausesValidation = false;
			this.logoutImageButton.Command += this.LogoutClick;
			this.loginLinkButton = new LinkButton();
			this.loginLinkButton.CausesValidation = false;
			this.loginLinkButton.Command += this.LoginClick;
			this.loginImageButton = new ImageButton();
			this.loginImageButton.CausesValidation = false;
			this.loginImageButton.Command += this.LoginClick;
			this.Controls.Add(this.logoutLinkButton);
			this.Controls.Add(this.logoutImageButton);
			this.Controls.Add(this.loginLinkButton);
			this.Controls.Add(this.loginImageButton);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginStatus.LoggedOut" /> event after the user clicks the logout link and logout processing is complete.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060028C1 RID: 10433 RVA: 0x0006A6F8 File Offset: 0x000688F8
		protected virtual void OnLoggedOut(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginStatus.loggedOutEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginStatus.LoggingOut" /> event when a user clicks the logout link on the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> that contains event data.</param>
		// Token: 0x060028C2 RID: 10434 RVA: 0x0006A728 File Offset: 0x00068928
		protected virtual void OnLoggingOut(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[LoginStatus.loggingOutEvent];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		/// <summary>Determines whether a user is logged in, and gets the URL of the login page.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> containing the event data. </param>
		// Token: 0x060028C3 RID: 10435 RVA: 0x000419F4 File Offset: 0x0003FBF4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.LoginName" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> control.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x060028C4 RID: 10436 RVA: 0x0006A756 File Offset: 0x00068956
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (writer == null)
			{
				return;
			}
			this.RenderContents(writer);
		}

		/// <summary>Renders the contents of the control to the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x060028C5 RID: 10437 RVA: 0x0006A764 File Offset: 0x00068964
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (writer == null)
			{
				return;
			}
			this.EnsureChildControls();
			bool flag = false;
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
				flag = this.Page.Request.IsAuthenticated;
			}
			bool flag2 = this.LogoutImageUrl.Length > 0;
			this.logoutLinkButton.Visible = flag && !flag2;
			this.logoutImageButton.Visible = flag && flag2;
			bool flag3 = this.LoginImageUrl.Length > 0;
			this.loginLinkButton.Visible = !flag && !flag3;
			this.loginImageButton.Visible = !flag && flag3;
			if (this.logoutLinkButton.Visible)
			{
				this.logoutLinkButton.Text = this.LogoutText;
				this.logoutLinkButton.CssClass = this.CssClass;
				this.logoutLinkButton.Render(writer);
				return;
			}
			if (this.logoutImageButton.Visible)
			{
				this.logoutImageButton.AlternateText = this.LogoutText;
				this.logoutImageButton.CssClass = this.CssClass;
				this.logoutImageButton.ImageUrl = this.LogoutImageUrl;
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.logoutImageButton.UniqueID);
				this.logoutImageButton.Render(writer);
				return;
			}
			if (this.loginLinkButton.Visible)
			{
				this.loginLinkButton.Text = this.LoginText;
				this.loginLinkButton.CssClass = this.CssClass;
				this.loginLinkButton.Render(writer);
				return;
			}
			if (this.loginImageButton.Visible)
			{
				this.loginImageButton.AlternateText = this.LoginText;
				this.loginImageButton.CssClass = this.CssClass;
				this.loginImageButton.ImageUrl = this.LoginImageUrl;
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.loginImageButton.UniqueID);
				this.loginImageButton.Render(writer);
			}
		}

		/// <summary>Overrides the base <see cref="M:System.Web.UI.Control.SetDesignModeState(System.Collections.IDictionary)" /> method. </summary>
		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the state of the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</param>
		// Token: 0x060028C6 RID: 10438 RVA: 0x000524CC File Offset: 0x000506CC
		[global::System.MonoTODO("for design-time usage - no more details available")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			base.SetDesignModeState(data);
		}

		/// <summary>Raised after the user clicks the logout link and the logout process is complete.</summary>
		// Token: 0x140000AC RID: 172
		// (add) Token: 0x060028C7 RID: 10439 RVA: 0x0006A940 File Offset: 0x00068B40
		// (remove) Token: 0x060028C8 RID: 10440 RVA: 0x0006A953 File Offset: 0x00068B53
		public event EventHandler LoggedOut
		{
			add
			{
				base.Events.AddHandler(LoginStatus.loggedOutEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginStatus.loggedOutEvent, value);
			}
		}

		/// <summary>Raised when the user clicks the logout button.</summary>
		// Token: 0x140000AD RID: 173
		// (add) Token: 0x060028C9 RID: 10441 RVA: 0x0006A966 File Offset: 0x00068B66
		// (remove) Token: 0x060028CA RID: 10442 RVA: 0x0006A979 File Offset: 0x00068B79
		public event LoginCancelEventHandler LoggingOut
		{
			add
			{
				base.Events.AddHandler(LoginStatus.loggingOutEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginStatus.loggingOutEvent, value);
			}
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0006A98C File Offset: 0x00068B8C
		private void LogoutClick(object sender, CommandEventArgs e)
		{
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs(false);
			this.OnLoggingOut(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return;
			}
			FormsAuthentication.SignOut();
			this.OnLoggedOut(e);
			switch (this.LogoutAction)
			{
			case LogoutAction.Refresh:
				HttpContext.Current.Response.Redirect(this.Page.Request.Url.AbsoluteUri);
				return;
			case LogoutAction.Redirect:
			{
				string text = this.LogoutPageUrl;
				if (text.Length == 0)
				{
					text = this.Page.Request.Url.AbsoluteUri;
				}
				HttpContext.Current.Response.Redirect(text);
				return;
			}
			case LogoutAction.RedirectToLoginPage:
				FormsAuthentication.RedirectToLoginPage();
				return;
			default:
				return;
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0006AA36 File Offset: 0x00068C36
		private void LoginClick(object sender, CommandEventArgs e)
		{
			FormsAuthentication.RedirectToLoginPage();
		}

		// Token: 0x04001A89 RID: 6793
		private static readonly object loggedOutEvent = new object();

		// Token: 0x04001A8A RID: 6794
		private static readonly object loggingOutEvent = new object();

		// Token: 0x04001A8B RID: 6795
		private LinkButton logoutLinkButton;

		// Token: 0x04001A8C RID: 6796
		private ImageButton logoutImageButton;

		// Token: 0x04001A8D RID: 6797
		private LinkButton loginLinkButton;

		// Token: 0x04001A8E RID: 6798
		private ImageButton loginImageButton;
	}
}

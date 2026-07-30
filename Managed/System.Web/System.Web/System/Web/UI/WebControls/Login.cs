using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Security;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides user interface (UI) elements for logging in to a Web site.</summary>
	// Token: 0x020003C7 RID: 967
	[Designer("System.Web.UI.Design.WebControls.LoginDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("Authenticate")]
	[Bindable(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Login : CompositeControl, IRenderOuterTable
	{
		/// <summary>Gets or sets the amount of padding inside the borders of the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>The amount of space (in pixels) between the contents of a <see cref="T:System.Web.UI.WebControls.Login" /> control and the <see cref="T:System.Web.UI.WebControls.Login" /> control's border. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Login.BorderPadding" /> property is set to a value less than -1.</exception>
		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x00068430 File Offset: 0x00066630
		// (set) Token: 0x0600282A RID: 10282 RVA: 0x00068459 File Offset: 0x00066659
		[DefaultValue(1)]
		public virtual int BorderPadding
		{
			get
			{
				object obj = this.ViewState["BorderPadding"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("BorderPadding", "< -1");
				}
				this.ViewState["BorderPadding"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Remember Me check box.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the <see cref="T:System.Web.UI.WebControls.Login" /> control's Remember Me check box.</returns>
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x00068485 File Offset: 0x00066685
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle CheckBoxStyle
		{
			get
			{
				if (this.checkBoxStyle == null)
				{
					this.checkBoxStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.checkBoxStyle).TrackViewState();
					}
				}
				return this.checkBoxStyle;
			}
		}

		/// <summary>Gets the location of an image to display next to the link to a registration page for new users.</summary>
		/// <returns>The URL of the image to display. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x000684B4 File Offset: 0x000666B4
		// (set) Token: 0x0600282D RID: 10285 RVA: 0x000684E1 File Offset: 0x000666E1
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string CreateUserIconUrl
		{
			get
			{
				object obj = this.ViewState["CreateUserIconUrl"];
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
					this.ViewState.Remove("CreateUserIconUrl");
					return;
				}
				this.ViewState["CreateUserIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of a link to a registration page for new users.</summary>
		/// <returns>The text of the link to the new-user registration page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x00068508 File Offset: 0x00066708
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x00068535 File Offset: 0x00066735
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string CreateUserText
		{
			get
			{
				object obj = this.ViewState["CreateUserText"];
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
					this.ViewState.Remove("CreateUserText");
					return;
				}
				this.ViewState["CreateUserText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the new-user registration page.</summary>
		/// <returns>The URL of the new-user registration page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x0006855C File Offset: 0x0006675C
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x00068589 File Offset: 0x00066789
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string CreateUserUrl
		{
			get
			{
				object obj = this.ViewState["CreateUserUrl"];
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
					this.ViewState.Remove("CreateUserUrl");
					return;
				}
				this.ViewState["CreateUserUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the page displayed to the user when a login attempt is successful.</summary>
		/// <returns>The URL of the page the user is redirected to when a login attempt is successful. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x000685B0 File Offset: 0x000667B0
		// (set) Token: 0x06002833 RID: 10291 RVA: 0x000685DD File Offset: 0x000667DD
		[Themeable(false)]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string DestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["DestinationPageUrl"];
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
					this.ViewState.Remove("DestinationPageUrl");
					return;
				}
				this.ViewState["DestinationPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to display a check box to enable the user to control whether a persistent cookie is sent to their browser.</summary>
		/// <returns>true to display the check box; otherwise, false. The default is true.</returns>
		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x00068604 File Offset: 0x00066804
		// (set) Token: 0x06002835 RID: 10293 RVA: 0x0006862D File Offset: 0x0006682D
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool DisplayRememberMe
		{
			get
			{
				object obj = this.ViewState["DisplayRememberMe"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["DisplayRememberMe"] = value;
			}
		}

		/// <summary>Gets or sets the action that occurs when a login attempt fails.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LoginFailureAction" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.LoginFailureAction.Refresh" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.LoginFailureAction" /> enumeration values.</exception>
		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x00068648 File Offset: 0x00066848
		// (set) Token: 0x06002837 RID: 10295 RVA: 0x00068671 File Offset: 0x00066871
		[global::System.MonoTODO("RedirectToLoginPage not yet implemented in FormsAuthentication")]
		[Themeable(false)]
		[DefaultValue(LoginFailureAction.Refresh)]
		public virtual LoginFailureAction FailureAction
		{
			get
			{
				object obj = this.ViewState["FailureAction"];
				if (obj != null)
				{
					return (LoginFailureAction)obj;
				}
				return LoginFailureAction.Refresh;
			}
			set
			{
				if (value < LoginFailureAction.Refresh || value > LoginFailureAction.RedirectToLoginPage)
				{
					throw new ArgumentOutOfRangeException("FailureAction");
				}
				this.ViewState["FailureAction"] = (int)value;
			}
		}

		/// <summary>Gets or sets the text displayed when a login attempt fails.</summary>
		/// <returns>The text to display to the user when a login attempt fails. The default is "Your login attempt has failed. Please try again." </returns>
		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x0006869C File Offset: 0x0006689C
		// (set) Token: 0x06002839 RID: 10297 RVA: 0x000686CE File Offset: 0x000668CE
		[Localizable(true)]
		public virtual string FailureText
		{
			get
			{
				object obj = this.ViewState["FailureText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Your login attempt was not successful. Please try again.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("FailureText");
					return;
				}
				this.ViewState["FailureText"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of error text in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of error text.</returns>
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x000686F5 File Offset: 0x000668F5
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle FailureTextStyle
		{
			get
			{
				if (this.failureTextStyle == null)
				{
					this.failureTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.failureTextStyle).TrackViewState();
					}
				}
				return this.failureTextStyle;
			}
		}

		/// <summary>Gets the location of an image to display next to the link to the login Help page.</summary>
		/// <returns>The URL of the image to display. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x00068724 File Offset: 0x00066924
		// (set) Token: 0x0600283C RID: 10300 RVA: 0x00051319 File Offset: 0x0004F519
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string HelpPageIconUrl
		{
			get
			{
				object obj = this.ViewState["HelpPageIconUrl"];
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
					this.ViewState.Remove("HelpPageIconUrl");
					return;
				}
				this.ViewState["HelpPageIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of a link to the login Help page.</summary>
		/// <returns>The text of the link to the login Help page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600283D RID: 10301 RVA: 0x00068754 File Offset: 0x00066954
		// (set) Token: 0x0600283E RID: 10302 RVA: 0x0005136D File Offset: 0x0004F56D
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string HelpPageText
		{
			get
			{
				object obj = this.ViewState["HelpPageText"];
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
					this.ViewState.Remove("HelpPageText");
					return;
				}
				this.ViewState["HelpPageText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the login Help page.</summary>
		/// <returns>The URL of the login Help page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x00068784 File Offset: 0x00066984
		// (set) Token: 0x06002840 RID: 10304 RVA: 0x000513C1 File Offset: 0x0004F5C1
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string HelpPageUrl
		{
			get
			{
				object obj = this.ViewState["HelpPageUrl"];
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
					this.ViewState.Remove("HelpPageUrl");
					return;
				}
				this.ViewState["HelpPageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of hyperlinks in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of hyperlinks.</returns>
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x000687B1 File Offset: 0x000669B1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle HyperLinkStyle
		{
			get
			{
				if (this.hyperLinkStyle == null)
				{
					this.hyperLinkStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.hyperLinkStyle).TrackViewState();
					}
				}
				return this.hyperLinkStyle;
			}
		}

		/// <summary>Gets or sets login instruction text for the user.</summary>
		/// <returns>The login instruction text to display to the user. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000687E0 File Offset: 0x000669E0
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x00051445 File Offset: 0x0004F645
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string InstructionText
		{
			get
			{
				object obj = this.ViewState["InstructionText"];
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
					this.ViewState.Remove("InstructionText");
					return;
				}
				this.ViewState["InstructionText"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that defines the settings for instruction text in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style settings of the <see cref="T:System.Web.UI.WebControls.Login" /> control instruction text.</returns>
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x0006880D File Offset: 0x00066A0D
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TableItemStyle InstructionTextStyle
		{
			get
			{
				if (this.instructionTextStyle == null)
				{
					this.instructionTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.instructionTextStyle).TrackViewState();
					}
				}
				return this.instructionTextStyle;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that defines the settings for <see cref="T:System.Web.UI.WebControls.Login" /> control labels.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that defines the style settings of the <see cref="T:System.Web.UI.WebControls.Login" /> control labels.</returns>
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x0006883B File Offset: 0x00066A3B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle LabelStyle
		{
			get
			{
				if (this.labelStyle == null)
				{
					this.labelStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.labelStyle).TrackViewState();
					}
				}
				return this.labelStyle;
			}
		}

		/// <summary>Gets or sets the template used to display the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.Login" /> control. The default value is null.</returns>
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x00068869 File Offset: 0x00066A69
		// (set) Token: 0x06002847 RID: 10311 RVA: 0x00068871 File Offset: 0x00066A71
		[Browsable(false)]
		[TemplateContainer(typeof(Login))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate LayoutTemplate
		{
			get
			{
				return this.layoutTemplate;
			}
			set
			{
				this.layoutTemplate = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to use for the login button.</summary>
		/// <returns>The URL of the image used for the login button. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x0006887C File Offset: 0x00066A7C
		// (set) Token: 0x06002849 RID: 10313 RVA: 0x000688A9 File Offset: 0x00066AA9
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string LoginButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["LoginButtonImageUrl"];
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
					this.ViewState.Remove("LoginButtonImageUrl");
					return;
				}
				this.ViewState["LoginButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that allows you to set the appearance of the login button in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the login button.</returns>
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x000688D0 File Offset: 0x00066AD0
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style LoginButtonStyle
		{
			get
			{
				if (this.logonButtonStyle == null)
				{
					this.logonButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.logonButtonStyle).TrackViewState();
					}
				}
				return this.logonButtonStyle;
			}
		}

		/// <summary>Gets or sets the text for the <see cref="T:System.Web.UI.WebControls.Login" /> control's login button.</summary>
		/// <returns>The text used for the <see cref="T:System.Web.UI.WebControls.Login" /> control's login button. The default is "Login".</returns>
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x00068900 File Offset: 0x00066B00
		// (set) Token: 0x0600284C RID: 10316 RVA: 0x00068932 File Offset: 0x00066B32
		[Localizable(true)]
		public virtual string LoginButtonText
		{
			get
			{
				object obj = this.ViewState["LoginButtonText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Log In");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("LoginButtonText");
					return;
				}
				this.ViewState["LoginButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button to use when rendering the <see cref="T:System.Web.UI.WebControls.Login" /> button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Login.LoginButtonType" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration value. </exception>
		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x0006895C File Offset: 0x00066B5C
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x00068985 File Offset: 0x00066B85
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType LoginButtonType
		{
			get
			{
				object obj = this.ViewState["LoginButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Button;
			}
			set
			{
				if (value < ButtonType.Button || value > ButtonType.Link)
				{
					throw new ArgumentOutOfRangeException("LoginButtonType");
				}
				this.ViewState["LoginButtonType"] = (int)value;
			}
		}

		/// <summary>Gets or sets the name of the membership data provider used by the control.</summary>
		/// <returns>The name of the membership data provider used by the control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000689B0 File Offset: 0x00066BB0
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x000689DD File Offset: 0x00066BDD
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string MembershipProvider
		{
			get
			{
				object obj = this.ViewState["MembershipProvider"];
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
					this.ViewState.Remove("MembershipProvider");
					return;
				}
				this.ViewState["MembershipProvider"] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the position of the elements of the <see cref="T:System.Web.UI.WebControls.Login" /> control on the page.</summary>
		/// <returns>One the <see cref="T:System.Web.UI.WebControls.Orientation" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.Orientation.Vertical" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Login.Orientation" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.Orientation" /> enumeration value. </exception>
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x00068A04 File Offset: 0x00066C04
		// (set) Token: 0x06002852 RID: 10322 RVA: 0x00068A2D File Offset: 0x00066C2D
		[DefaultValue(Orientation.Vertical)]
		public virtual Orientation Orientation
		{
			get
			{
				object obj = this.ViewState["Orientation"];
				if (obj != null)
				{
					return (Orientation)obj;
				}
				return Orientation.Vertical;
			}
			set
			{
				if (value < Orientation.Horizontal || value > Orientation.Vertical)
				{
					throw new ArgumentOutOfRangeException("Orientation");
				}
				this.ViewState["Orientation"] = (int)value;
			}
		}

		/// <summary>Gets the password entered by the user.</summary>
		/// <returns>The password entered by the user. The default is null.</returns>
		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x00068A58 File Offset: 0x00066C58
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Password
		{
			get
			{
				if (this._password == null)
				{
					return string.Empty;
				}
				return this._password;
			}
		}

		/// <summary>Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box.</summary>
		/// <returns>The text of the label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box. The default is "Password:".</returns>
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x00068A70 File Offset: 0x00066C70
		// (set) Token: 0x06002855 RID: 10325 RVA: 0x000517D6 File Offset: 0x0004F9D6
		[Localizable(true)]
		public virtual string PasswordLabelText
		{
			get
			{
				object obj = this.ViewState["PasswordLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "Password:";
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("PasswordLabelText");
					return;
				}
				this.ViewState["PasswordLabelText"] = value;
			}
		}

		/// <summary>Gets the location of an image to display next to the link to the password recovery page.</summary>
		/// <returns>The URL of the image to display. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x00068AA0 File Offset: 0x00066CA0
		// (set) Token: 0x06002857 RID: 10327 RVA: 0x00068ACD File Offset: 0x00066CCD
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string PasswordRecoveryIconUrl
		{
			get
			{
				object obj = this.ViewState["PasswordRecoveryIconUrl"];
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
					this.ViewState.Remove("PasswordRecoveryIconUrl");
					return;
				}
				this.ViewState["PasswordRecoveryIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of a link to the password recovery page.</summary>
		/// <returns>The text of the link to the password recovery page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x00068AF4 File Offset: 0x00066CF4
		// (set) Token: 0x06002859 RID: 10329 RVA: 0x00068B21 File Offset: 0x00066D21
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string PasswordRecoveryText
		{
			get
			{
				object obj = this.ViewState["PasswordRecoveryText"];
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
					this.ViewState.Remove("PasswordRecoveryText");
					return;
				}
				this.ViewState["PasswordRecoveryText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the password recovery page.</summary>
		/// <returns>The URL of the password recovery page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600285A RID: 10330 RVA: 0x00068B48 File Offset: 0x00066D48
		// (set) Token: 0x0600285B RID: 10331 RVA: 0x00068B75 File Offset: 0x00066D75
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string PasswordRecoveryUrl
		{
			get
			{
				object obj = this.ViewState["PasswordRecoveryUrl"];
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
					this.ViewState.Remove("PasswordRecoveryUrl");
					return;
				}
				this.ViewState["PasswordRecoveryUrl"] = value;
			}
		}

		/// <summary>Gets or sets the error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the password field is left blank.</summary>
		/// <returns>The error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the password field is left blank. The default is "Password." </returns>
		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x00068B9C File Offset: 0x00066D9C
		// (set) Token: 0x0600285D RID: 10333 RVA: 0x000518E2 File Offset: 0x0004FAE2
		[Localizable(true)]
		public virtual string PasswordRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["PasswordRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Password is required.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("PasswordRequiredErrorMessage");
					return;
				}
				this.ViewState["PasswordRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control encloses rendered HTML in a table element in order to apply inline styles.</summary>
		/// <returns>true if the control encloses rendered HTML in a table element; otherwise, false. The default is true.</returns>
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600285E RID: 10334 RVA: 0x00068BCE File Offset: 0x00066DCE
		// (set) Token: 0x0600285F RID: 10335 RVA: 0x00068BD6 File Offset: 0x00066DD6
		[DefaultValue(true)]
		public virtual bool RenderOuterTable
		{
			get
			{
				return this.renderOuterTable;
			}
			set
			{
				this.renderOuterTable = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to send a persistent authentication cookie to the user's browser.</summary>
		/// <returns>true to send a persistent authentication cookie; otherwise, false. The default value is false.</returns>
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x00068BE0 File Offset: 0x00066DE0
		// (set) Token: 0x06002861 RID: 10337 RVA: 0x00068C09 File Offset: 0x00066E09
		[Themeable(false)]
		[DefaultValue(false)]
		public virtual bool RememberMeSet
		{
			get
			{
				object obj = this.ViewState["RememberMeSet"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["RememberMeSet"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label for the Remember Me check box.</summary>
		/// <returns>The text of the label for the Remember Me check box. The default is "Remember me next time." </returns>
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x00068C24 File Offset: 0x00066E24
		// (set) Token: 0x06002863 RID: 10339 RVA: 0x00068C56 File Offset: 0x00066E56
		[Localizable(true)]
		public virtual string RememberMeText
		{
			get
			{
				object obj = this.ViewState["RememberMeText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Remember me next time.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("RememberMeText");
					return;
				}
				this.ViewState["RememberMeText"] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to a <see cref="T:System.Web.UI.WebControls.Login" /> control. This property is used primarily by control developers.</summary>
		/// <returns>Always returns <see cref="F:System.Web.UI.HtmlTextWriterTag.Table" />.</returns>
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x0004D090 File Offset: 0x0004B290
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of text boxes in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that contains properties that define the appearance of text boxes.</returns>
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x00068C7D File Offset: 0x00066E7D
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style TextBoxStyle
		{
			get
			{
				if (this.textBoxStyle == null)
				{
					this.textBoxStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.textBoxStyle).TrackViewState();
					}
				}
				return this.textBoxStyle;
			}
		}

		/// <summary>Specifies the position of each label relative to its associated text box for the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.LoginTextLayout.TextOnLeft" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration values.</exception>
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002866 RID: 10342 RVA: 0x00068CAC File Offset: 0x00066EAC
		// (set) Token: 0x06002867 RID: 10343 RVA: 0x00068CD5 File Offset: 0x00066ED5
		[DefaultValue(LoginTextLayout.TextOnLeft)]
		public virtual LoginTextLayout TextLayout
		{
			get
			{
				object obj = this.ViewState["TextLayout"];
				if (obj != null)
				{
					return (LoginTextLayout)obj;
				}
				return LoginTextLayout.TextOnLeft;
			}
			set
			{
				if (value < LoginTextLayout.TextOnLeft || value > LoginTextLayout.TextOnTop)
				{
					throw new ArgumentOutOfRangeException("TextLayout");
				}
				this.ViewState["TextLayout"] = (int)value;
			}
		}

		/// <summary>Gets or sets the title of the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>The title of the <see cref="T:System.Web.UI.WebControls.Login" /> control. The default is "Login". </returns>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x00068D00 File Offset: 0x00066F00
		// (set) Token: 0x06002869 RID: 10345 RVA: 0x00050366 File Offset: 0x0004E566
		[Localizable(true)]
		public virtual string TitleText
		{
			get
			{
				object obj = this.ViewState["TitleText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Log In");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("TitleText");
					return;
				}
				this.ViewState["TitleText"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of the title text in the <see cref="T:System.Web.UI.WebControls.Login" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of title text.</returns>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x00068D32 File Offset: 0x00066F32
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle TitleTextStyle
		{
			get
			{
				if (this.titleTextStyle == null)
				{
					this.titleTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.titleTextStyle).TrackViewState();
					}
				}
				return this.titleTextStyle;
			}
		}

		/// <summary>Gets the user name entered by the user.</summary>
		/// <returns>The user name entered by the user. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x00068D60 File Offset: 0x00066F60
		// (set) Token: 0x0600286C RID: 10348 RVA: 0x00051B95 File Offset: 0x0004FD95
		[DefaultValue("")]
		public virtual string UserName
		{
			get
			{
				object obj = this.ViewState["UserName"];
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
					this.ViewState.Remove("UserName");
					return;
				}
				this.ViewState["UserName"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.Login.UserName" /> text box.</summary>
		/// <returns>The text of the label for the <see cref="P:System.Web.UI.WebControls.Login.UserName" /> text box. The default is "User Name:".</returns>
		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x00068D90 File Offset: 0x00066F90
		// (set) Token: 0x0600286E RID: 10350 RVA: 0x00051BEE File Offset: 0x0004FDEE
		[Localizable(true)]
		public virtual string UserNameLabelText
		{
			get
			{
				object obj = this.ViewState["UserNameLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("User Name:");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("UserNameLabelText");
					return;
				}
				this.ViewState["UserNameLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the user name field is left blank.</summary>
		/// <returns>The error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the user name field is left blank. The default is "User Name." </returns>
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x00068DC4 File Offset: 0x00066FC4
		// (set) Token: 0x06002870 RID: 10352 RVA: 0x00051C4A File Offset: 0x0004FE4A
		[Localizable(true)]
		public virtual string UserNameRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["UserNameRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("User Name is required.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("UserNameRequiredErrorMessage");
					return;
				}
				this.ViewState["UserNameRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages associated with validators used by the <see cref="T:System.Web.UI.WebControls.Login" /> control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> containing the style settings.</returns>
		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x00068DF6 File Offset: 0x00066FF6
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style ValidatorTextStyle
		{
			get
			{
				if (this.validatorTextStyle == null)
				{
					this.validatorTextStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.validatorTextStyle).TrackViewState();
					}
				}
				return this.validatorTextStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether to show the <see cref="T:System.Web.UI.WebControls.Login" /> control after the user is authenticated.</summary>
		/// <returns>false if the <see cref="T:System.Web.UI.WebControls.Login" /> control should be hidden when the user is authenticated; otherwise, true. The default is true.</returns>
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x00068E24 File Offset: 0x00067024
		// (set) Token: 0x06002873 RID: 10355 RVA: 0x00068E4D File Offset: 0x0006704D
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool VisibleWhenLoggedIn
		{
			get
			{
				object obj = this.ViewState["VisibleWhenLoggedIn"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["VisibleWhenLoggedIn"] = value;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x00068E65 File Offset: 0x00067065
		private Login.LoginContainer LoginTemplateContainer
		{
			get
			{
				if (this.container == null)
				{
					this.container = new Login.LoginContainer(this);
				}
				return this.container;
			}
		}

		/// <summary>Creates the individual controls that make up the <see cref="T:System.Web.UI.WebControls.Login" /> control and associates event handlers with their events.</summary>
		// Token: 0x06002875 RID: 10357 RVA: 0x00068E84 File Offset: 0x00067084
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			ITemplate template = this.LayoutTemplate;
			if (template == null)
			{
				template = new Login.LoginTemplate(this);
			}
			this.LoginTemplateContainer.InstantiateTemplate(template);
			this.Controls.Add(this.container);
			IEditableTextControl editableTextControl = this.container.UserNameTextBox as IEditableTextControl;
			if (editableTextControl == null)
			{
				throw new HttpException("LayoutTemplate does not contain an IEditableTextControl with ID UserName for the username.");
			}
			editableTextControl.Text = this.UserName;
			editableTextControl.TextChanged += this.UserName_TextChanged;
			editableTextControl = this.container.PasswordTextBox as IEditableTextControl;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.Password_TextChanged;
				ICheckBoxControl checkBoxControl = this.container.RememberMeCheckBox as ICheckBoxControl;
				if (checkBoxControl != null)
				{
					checkBoxControl.CheckedChanged += this.RememberMe_CheckedChanged;
				}
				return;
			}
			throw new HttpException("LayoutTemplate does not contain an IEditableTextControl with ID Password for the password.");
		}

		/// <param name="savedState">The <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> to load.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.PageStatePersister.ViewState" />.</exception>
		// Token: 0x06002876 RID: 10358 RVA: 0x00068F64 File Offset: 0x00067164
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.LoginButtonStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.LabelStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.TextBoxStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.HyperLinkStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.InstructionTextStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.TitleTextStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.CheckBoxStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.FailureTextStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.ValidatorTextStyle).LoadViewState(array[9]);
			}
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00069039 File Offset: 0x00067239
		private bool HasOnAuthenticateHandler()
		{
			return base.Events[Login.authenticateEvent] != null;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.Authenticate" /> event to authenticate the user.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /> that contains the event data. </param>
		// Token: 0x06002878 RID: 10360 RVA: 0x00069050 File Offset: 0x00067250
		protected virtual void OnAuthenticate(AuthenticateEventArgs e)
		{
			AuthenticateEventHandler authenticateEventHandler = (AuthenticateEventHandler)base.Events[Login.authenticateEvent];
			if (authenticateEventHandler != null)
			{
				authenticateEventHandler(this, e);
			}
		}

		/// <summary>Determines whether to pass an event up the page's user interface (UI) server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> containing the data for the event. </param>
		// Token: 0x06002879 RID: 10361 RVA: 0x00069080 File Offset: 0x00067280
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs != null && string.Equals(commandEventArgs.CommandName, Login.LoginButtonCommandName, StringComparison.InvariantCultureIgnoreCase))
			{
				if (!this.AuthenticateUser())
				{
					ITextControl failureTextLiteral = this.LoginTemplateContainer.FailureTextLiteral;
					if (failureTextLiteral != null)
					{
						failureTextLiteral.Text = this.FailureText;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.LoggedIn" /> event after the user logs in to the Web site and has been authenticated.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600287A RID: 10362 RVA: 0x000690D0 File Offset: 0x000672D0
		protected virtual void OnLoggedIn(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Login.loggedInEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.LoggingIn" /> event when a user submits login information but before the authentication takes place.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> containing the event data.</param>
		// Token: 0x0600287B RID: 10363 RVA: 0x00069100 File Offset: 0x00067300
		protected virtual void OnLoggingIn(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[Login.loggingInEvent];
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Login.LoginError" /> event when a login attempt fails.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600287C RID: 10364 RVA: 0x00069130 File Offset: 0x00067330
		protected virtual void OnLoginError(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Login.loginErrorEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x0600287D RID: 10365 RVA: 0x000419F4 File Offset: 0x0003FBF4
		[global::System.MonoTODO("overriden for ?")]
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the login form using the specified HTML writer.</summary>
		/// <param name="writer">The HMTL writer.</param>
		// Token: 0x0600287E RID: 10366 RVA: 0x00069160 File Offset: 0x00067360
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.VerifyInlinePropertiesNotSet();
			if (!this.VisibleWhenLoggedIn && !this.IsDefaultLoginPage() && this.IsLoggedIn())
			{
				return;
			}
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			this.EnsureChildControls();
			foreach (object obj in this.styles)
			{
				object[] array = (object[])obj;
				((WebControl)array[0]).ApplyStyle((Style)array[1]);
			}
			this.RenderContents(writer);
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x00069204 File Offset: 0x00067404
		protected override object SaveViewState()
		{
			object[] array = new object[10];
			array[0] = base.SaveViewState();
			if (this.logonButtonStyle != null)
			{
				array[1] = ((IStateManager)this.logonButtonStyle).SaveViewState();
			}
			if (this.labelStyle != null)
			{
				array[2] = ((IStateManager)this.labelStyle).SaveViewState();
			}
			if (this.textBoxStyle != null)
			{
				array[3] = ((IStateManager)this.textBoxStyle).SaveViewState();
			}
			if (this.hyperLinkStyle != null)
			{
				array[4] = ((IStateManager)this.hyperLinkStyle).SaveViewState();
			}
			if (this.instructionTextStyle != null)
			{
				array[5] = ((IStateManager)this.instructionTextStyle).SaveViewState();
			}
			if (this.titleTextStyle != null)
			{
				array[6] = ((IStateManager)this.titleTextStyle).SaveViewState();
			}
			if (this.checkBoxStyle != null)
			{
				array[7] = ((IStateManager)this.checkBoxStyle).SaveViewState();
			}
			if (this.failureTextStyle != null)
			{
				array[8] = ((IStateManager)this.failureTextStyle).SaveViewState();
			}
			if (this.validatorTextStyle != null)
			{
				array[9] = ((IStateManager)this.validatorTextStyle).SaveViewState();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[0] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the state of the <see cref="T:System.Web.UI.WebControls.Login" /> control.</param>
		// Token: 0x06002880 RID: 10368 RVA: 0x000524CC File Offset: 0x000506CC
		[global::System.MonoTODO("for design-time usage - no more details available")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			base.SetDesignModeState(data);
		}

		/// <summary>Overrides the base <see cref="M:System.Web.UI.Control.TrackViewState" /> method.</summary>
		// Token: 0x06002881 RID: 10369 RVA: 0x00069300 File Offset: 0x00067500
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.logonButtonStyle != null)
			{
				((IStateManager)this.logonButtonStyle).TrackViewState();
			}
			if (this.labelStyle != null)
			{
				((IStateManager)this.labelStyle).TrackViewState();
			}
			if (this.textBoxStyle != null)
			{
				((IStateManager)this.textBoxStyle).TrackViewState();
			}
			if (this.hyperLinkStyle != null)
			{
				((IStateManager)this.hyperLinkStyle).TrackViewState();
			}
			if (this.instructionTextStyle != null)
			{
				((IStateManager)this.instructionTextStyle).TrackViewState();
			}
			if (this.titleTextStyle != null)
			{
				((IStateManager)this.titleTextStyle).TrackViewState();
			}
			if (this.checkBoxStyle != null)
			{
				((IStateManager)this.checkBoxStyle).TrackViewState();
			}
			if (this.failureTextStyle != null)
			{
				((IStateManager)this.failureTextStyle).TrackViewState();
			}
			if (this.validatorTextStyle != null)
			{
				((IStateManager)this.validatorTextStyle).TrackViewState();
			}
		}

		/// <summary>Occurs when a user is authenticated.</summary>
		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06002882 RID: 10370 RVA: 0x000693BE File Offset: 0x000675BE
		// (remove) Token: 0x06002883 RID: 10371 RVA: 0x000693D1 File Offset: 0x000675D1
		public event AuthenticateEventHandler Authenticate
		{
			add
			{
				base.Events.AddHandler(Login.authenticateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.authenticateEvent, value);
			}
		}

		/// <summary>Occurs when the user logs in to the Web site and has been authenticated.</summary>
		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x06002884 RID: 10372 RVA: 0x000693E4 File Offset: 0x000675E4
		// (remove) Token: 0x06002885 RID: 10373 RVA: 0x000693F7 File Offset: 0x000675F7
		public event EventHandler LoggedIn
		{
			add
			{
				base.Events.AddHandler(Login.loggedInEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.loggedInEvent, value);
			}
		}

		/// <summary>Occurs when a user submits login information, before authentication takes place.</summary>
		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06002886 RID: 10374 RVA: 0x0006940A File Offset: 0x0006760A
		// (remove) Token: 0x06002887 RID: 10375 RVA: 0x0006941D File Offset: 0x0006761D
		public event LoginCancelEventHandler LoggingIn
		{
			add
			{
				base.Events.AddHandler(Login.loggingInEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.loggingInEvent, value);
			}
		}

		/// <summary>Occurs when a login error is detected.</summary>
		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06002888 RID: 10376 RVA: 0x00069430 File Offset: 0x00067630
		// (remove) Token: 0x06002889 RID: 10377 RVA: 0x00069443 File Offset: 0x00067643
		public event EventHandler LoginError
		{
			add
			{
				base.Events.AddHandler(Login.loginErrorEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Login.loginErrorEvent, value);
			}
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x00069456 File Offset: 0x00067656
		internal void RegisterApplyStyle(WebControl control, Style style)
		{
			this.styles.Add(new object[] { control, style });
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00069474 File Offset: 0x00067674
		private bool AuthenticateUser()
		{
			if (!this.Page.IsValid)
			{
				return true;
			}
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnLoggingIn(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return true;
			}
			AuthenticateEventArgs authenticateEventArgs = new AuthenticateEventArgs();
			if (!this.HasOnAuthenticateHandler())
			{
				string membershipProvider = this.MembershipProvider;
				MembershipProvider membershipProvider2 = ((membershipProvider.Length == 0) ? Membership.Provider : Membership.Providers[membershipProvider]);
				if (membershipProvider2 == null)
				{
					throw new HttpException(global::Locale.GetText("No provider named '{0}' could be found.", new object[] { membershipProvider }));
				}
				authenticateEventArgs.Authenticated = membershipProvider2.ValidateUser(this.UserName, this.Password);
			}
			this.OnAuthenticate(authenticateEventArgs);
			if (authenticateEventArgs.Authenticated)
			{
				FormsAuthentication.SetAuthCookie(this.UserName, this.RememberMeSet);
				this.OnLoggedIn(EventArgs.Empty);
				string destinationPageUrl = this.DestinationPageUrl;
				if (this.Page.Request.Path.StartsWith(FormsAuthentication.LoginUrl, StringComparison.InvariantCultureIgnoreCase))
				{
					if (!string.IsNullOrEmpty(FormsAuthentication.ReturnUrl))
					{
						this.Redirect(FormsAuthentication.ReturnUrl);
					}
					else if (!string.IsNullOrEmpty(this.DestinationPageUrl))
					{
						this.Redirect(destinationPageUrl);
					}
					else if (!string.IsNullOrEmpty(FormsAuthentication.DefaultUrl))
					{
						this.Redirect(FormsAuthentication.DefaultUrl);
					}
					else if (destinationPageUrl.Length == 0)
					{
						this.Refresh();
					}
				}
				else if (!string.IsNullOrEmpty(this.DestinationPageUrl))
				{
					this.Redirect(destinationPageUrl);
				}
				else
				{
					this.Refresh();
				}
				return true;
			}
			this.OnLoginError(EventArgs.Empty);
			if (this.FailureAction == LoginFailureAction.RedirectToLoginPage)
			{
				FormsAuthentication.RedirectToLoginPage();
			}
			return false;
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000695F4 File Offset: 0x000677F4
		[global::System.MonoTODO]
		private void LoginClick(object sender, CommandEventArgs e)
		{
			base.RaiseBubbleEvent(sender, e);
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x00069600 File Offset: 0x00067800
		private bool IsDefaultLoginPage()
		{
			if (this.Page == null || this.Page.Request == null)
			{
				return false;
			}
			string loginUrl = FormsAuthentication.LoginUrl;
			if (loginUrl == null)
			{
				return false;
			}
			string absolutePath = this.Page.Request.Url.AbsolutePath;
			return string.Compare(loginUrl, 0, absolutePath, absolutePath.Length - loginUrl.Length, loginUrl.Length, true, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x00069669 File Offset: 0x00067869
		private bool IsLoggedIn()
		{
			return this.Page != null && this.Page.Request != null && this.Page.Request.IsAuthenticated;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x00069692 File Offset: 0x00067892
		private void Redirect(string url)
		{
			if (this.Page != null && this.Page.Response != null)
			{
				this.Page.Response.Redirect(url);
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000696BA File Offset: 0x000678BA
		private void Refresh()
		{
			if (this.Page != null && this.Page.Response != null)
			{
				this.Page.Response.Redirect(this.Page.Request.RawUrl);
			}
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000696F1 File Offset: 0x000678F1
		private void UserName_TextChanged(object sender, EventArgs e)
		{
			this.UserName = ((ITextControl)sender).Text;
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x00069704 File Offset: 0x00067904
		private void Password_TextChanged(object sender, EventArgs e)
		{
			this._password = ((ITextControl)sender).Text;
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x00069717 File Offset: 0x00067917
		private void RememberMe_CheckedChanged(object sender, EventArgs e)
		{
			this.RememberMeSet = ((ICheckBoxControl)sender).Checked;
		}

		/// <summary>Represents the command name associated with the login button.</summary>
		// Token: 0x04001A71 RID: 6769
		public static readonly string LoginButtonCommandName = "Login";

		// Token: 0x04001A72 RID: 6770
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001A73 RID: 6771
		private static readonly object loggedInEvent = new object();

		// Token: 0x04001A74 RID: 6772
		private static readonly object loggingInEvent = new object();

		// Token: 0x04001A75 RID: 6773
		private static readonly object loginErrorEvent = new object();

		// Token: 0x04001A76 RID: 6774
		private TableItemStyle checkBoxStyle;

		// Token: 0x04001A77 RID: 6775
		private TableItemStyle failureTextStyle;

		// Token: 0x04001A78 RID: 6776
		private TableItemStyle hyperLinkStyle;

		// Token: 0x04001A79 RID: 6777
		private TableItemStyle instructionTextStyle;

		// Token: 0x04001A7A RID: 6778
		private TableItemStyle labelStyle;

		// Token: 0x04001A7B RID: 6779
		private Style logonButtonStyle;

		// Token: 0x04001A7C RID: 6780
		private Style textBoxStyle;

		// Token: 0x04001A7D RID: 6781
		private TableItemStyle titleTextStyle;

		// Token: 0x04001A7E RID: 6782
		private Style validatorTextStyle;

		// Token: 0x04001A7F RID: 6783
		private ArrayList styles = new ArrayList();

		// Token: 0x04001A80 RID: 6784
		private ITemplate layoutTemplate;

		// Token: 0x04001A81 RID: 6785
		private Login.LoginContainer container;

		// Token: 0x04001A82 RID: 6786
		private string _password;

		// Token: 0x04001A83 RID: 6787
		private bool renderOuterTable = true;

		// Token: 0x020003C8 RID: 968
		private sealed class LoginContainer : Control
		{
			// Token: 0x06002895 RID: 10389 RVA: 0x0006975E File Offset: 0x0006795E
			public LoginContainer(Login owner)
			{
				this._owner = owner;
				this.renderOuterTable = this._owner.RenderOuterTable;
				if (this.renderOuterTable)
				{
					this.InitTable();
				}
			}

			// Token: 0x17000CE7 RID: 3303
			// (get) Token: 0x06002896 RID: 10390 RVA: 0x0006978C File Offset: 0x0006798C
			// (set) Token: 0x06002897 RID: 10391 RVA: 0x00069799 File Offset: 0x00067999
			public override string ID
			{
				get
				{
					return this._owner.ID;
				}
				set
				{
					this._owner.ID = value;
				}
			}

			// Token: 0x17000CE8 RID: 3304
			// (get) Token: 0x06002898 RID: 10392 RVA: 0x000697A7 File Offset: 0x000679A7
			public override string ClientID
			{
				get
				{
					return this._owner.ClientID;
				}
			}

			// Token: 0x06002899 RID: 10393 RVA: 0x000697B4 File Offset: 0x000679B4
			public void InstantiateTemplate(ITemplate template)
			{
				if (!this.renderOuterTable)
				{
					template.InstantiateIn(this);
					return;
				}
				template.InstantiateIn(this._containerCell);
			}

			// Token: 0x0600289A RID: 10394 RVA: 0x000697D4 File Offset: 0x000679D4
			private void InitTable()
			{
				this._table = new Table();
				this._containerCell = new TableCell();
				TableRow tableRow = new TableRow();
				tableRow.Cells.Add(this._containerCell);
				this._table.Rows.Add(tableRow);
				this.Controls.AddAt(0, this._table);
			}

			// Token: 0x0600289B RID: 10395 RVA: 0x00069834 File Offset: 0x00067A34
			protected internal override void Render(HtmlTextWriter writer)
			{
				if (this._table != null)
				{
					this._table.CellSpacing = 0;
					this._table.CellPadding = this._owner.BorderPadding;
					this._table.ApplyStyle(this._owner.ControlStyle);
					this._table.Attributes.CopyFrom(this._owner.Attributes);
				}
				base.Render(writer);
			}

			// Token: 0x17000CE9 RID: 3305
			// (get) Token: 0x0600289C RID: 10396 RVA: 0x000698A3 File Offset: 0x00067AA3
			public Control UserNameTextBox
			{
				get
				{
					return this.FindControl("UserName");
				}
			}

			// Token: 0x17000CEA RID: 3306
			// (get) Token: 0x0600289D RID: 10397 RVA: 0x00052D11 File Offset: 0x00050F11
			public Control PasswordTextBox
			{
				get
				{
					return this.FindControl("Password");
				}
			}

			// Token: 0x17000CEB RID: 3307
			// (get) Token: 0x0600289E RID: 10398 RVA: 0x000698B0 File Offset: 0x00067AB0
			public Control RememberMeCheckBox
			{
				get
				{
					return this.FindControl("RememberMe");
				}
			}

			// Token: 0x17000CEC RID: 3308
			// (get) Token: 0x0600289F RID: 10399 RVA: 0x0004D979 File Offset: 0x0004BB79
			public ITextControl FailureTextLiteral
			{
				get
				{
					return this.FindControl("FailureText") as ITextControl;
				}
			}

			// Token: 0x04001A84 RID: 6788
			private readonly Login _owner;

			// Token: 0x04001A85 RID: 6789
			private bool renderOuterTable;

			// Token: 0x04001A86 RID: 6790
			private Table _table;

			// Token: 0x04001A87 RID: 6791
			private TableCell _containerCell;
		}

		// Token: 0x020003C9 RID: 969
		private sealed class LoginTemplate : WebControl, ITemplate
		{
			// Token: 0x060028A0 RID: 10400 RVA: 0x000698BD File Offset: 0x00067ABD
			public LoginTemplate(Login login)
			{
				this._login = login;
			}

			// Token: 0x060028A1 RID: 10401 RVA: 0x000698CC File Offset: 0x00067ACC
			void ITemplate.InstantiateIn(Control container)
			{
				LiteralControl literalControl = new LiteralControl(this._login.TitleText);
				LiteralControl literalControl2 = new LiteralControl(this._login.InstructionText);
				TextBox textBox = new TextBox();
				textBox.ID = "UserName";
				textBox.Text = this._login.UserName;
				this._login.RegisterApplyStyle(textBox, this._login.TextBoxStyle);
				Label label = new Label();
				label.ID = "UserNameLabel";
				label.AssociatedControlID = "UserName";
				label.Text = this._login.UserNameLabelText;
				RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
				requiredFieldValidator.ID = "UserNameRequired";
				requiredFieldValidator.ControlToValidate = "UserName";
				requiredFieldValidator.ErrorMessage = this._login.UserNameRequiredErrorMessage;
				requiredFieldValidator.ToolTip = this._login.UserNameRequiredErrorMessage;
				requiredFieldValidator.Text = "*";
				requiredFieldValidator.ValidationGroup = this._login.ID;
				this._login.RegisterApplyStyle(requiredFieldValidator, this._login.ValidatorTextStyle);
				TextBox textBox2 = new TextBox();
				textBox2.ID = "Password";
				textBox2.TextMode = TextBoxMode.Password;
				this._login.RegisterApplyStyle(textBox2, this._login.TextBoxStyle);
				Label label2 = new Label();
				label2.ID = "PasswordLabel";
				label2.AssociatedControlID = "PasswordLabel";
				label2.Text = this._login.PasswordLabelText;
				RequiredFieldValidator requiredFieldValidator2 = new RequiredFieldValidator();
				requiredFieldValidator2.ID = "PasswordRequired";
				requiredFieldValidator2.ControlToValidate = "Password";
				requiredFieldValidator2.ErrorMessage = this._login.PasswordRequiredErrorMessage;
				requiredFieldValidator2.ToolTip = this._login.PasswordRequiredErrorMessage;
				requiredFieldValidator2.Text = "*";
				requiredFieldValidator2.ValidationGroup = this._login.ID;
				this._login.RegisterApplyStyle(requiredFieldValidator2, this._login.ValidatorTextStyle);
				bool flag = this._login == null || this._login.DisplayRememberMe;
				CheckBox checkBox;
				if (flag)
				{
					checkBox = new CheckBox();
					checkBox.ID = "RememberMe";
					checkBox.Checked = this._login.RememberMeSet;
					checkBox.Text = this._login.RememberMeText;
					this._login.RegisterApplyStyle(checkBox, this._login.CheckBoxStyle);
				}
				else
				{
					checkBox = null;
				}
				Literal literal = new Literal();
				literal.ID = "FailureText";
				literal.EnableViewState = false;
				WebControl webControl = null;
				switch (this._login.LoginButtonType)
				{
				case ButtonType.Button:
					webControl = new Button();
					webControl.ID = "LoginButton";
					break;
				case ButtonType.Image:
					webControl = new ImageButton();
					webControl.ID = "LoginImageButton";
					break;
				case ButtonType.Link:
					webControl = new LinkButton();
					webControl.ID = "LoginLinkButton";
					break;
				}
				this._login.RegisterApplyStyle(webControl, this._login.LoginButtonStyle);
				webControl.ID = "LoginButton";
				((IButtonControl)webControl).Text = this._login.LoginButtonText;
				((IButtonControl)webControl).CommandName = Login.LoginButtonCommandName;
				((IButtonControl)webControl).Command += this._login.LoginClick;
				((IButtonControl)webControl).ValidationGroup = this._login.ID;
				Table table = new Table();
				table.CellPadding = 0;
				table.Rows.Add(this.CreateRow(this.CreateCell(literalControl, null, this._login.TitleTextStyle, HorizontalAlign.Center)));
				if (this._login.InstructionText.Length > 0)
				{
					table.Rows.Add(this.CreateRow(this.CreateCell(literalControl2, null, this._login.instructionTextStyle, HorizontalAlign.Center)));
				}
				if (this._login.Orientation == Orientation.Horizontal)
				{
					TableRow tableRow = new TableRow();
					TableRow tableRow2 = new TableRow();
					if (this._login.TextLayout == LoginTextLayout.TextOnTop)
					{
						tableRow.Cells.Add(this.CreateCell(label, null, this._login.LabelStyle));
					}
					else
					{
						tableRow2.Cells.Add(this.CreateCell(label, null, this._login.LabelStyle));
					}
					tableRow2.Cells.Add(this.CreateCell(textBox, requiredFieldValidator, null));
					if (this._login.TextLayout == LoginTextLayout.TextOnTop)
					{
						tableRow.Cells.Add(this.CreateCell(label2, null, this._login.LabelStyle));
					}
					else
					{
						tableRow2.Cells.Add(this.CreateCell(label2, null, this._login.LabelStyle));
					}
					tableRow2.Cells.Add(this.CreateCell(textBox2, requiredFieldValidator2, null));
					if (flag)
					{
						tableRow2.Cells.Add(this.CreateCell(checkBox, null, null));
					}
					tableRow2.Cells.Add(this.CreateCell(webControl, null, null));
					if (tableRow.Cells.Count > 0)
					{
						table.Rows.Add(tableRow);
					}
					table.Rows.Add(tableRow2);
				}
				else
				{
					if (this._login.TextLayout == LoginTextLayout.TextOnLeft)
					{
						table.Rows.Add(this.CreateRow(label, textBox, requiredFieldValidator, this._login.LabelStyle));
					}
					else
					{
						table.Rows.Add(this.CreateRow(label, null, null, this._login.LabelStyle));
						table.Rows.Add(this.CreateRow(null, textBox, requiredFieldValidator, null));
					}
					if (this._login.TextLayout == LoginTextLayout.TextOnLeft)
					{
						table.Rows.Add(this.CreateRow(label2, textBox2, requiredFieldValidator2, this._login.LabelStyle));
					}
					else
					{
						table.Rows.Add(this.CreateRow(label2, null, null, this._login.LabelStyle));
						table.Rows.Add(this.CreateRow(null, textBox2, requiredFieldValidator2, null));
					}
					if (flag)
					{
						table.Rows.Add(this.CreateRow(this.CreateCell(checkBox, null, null)));
					}
					table.Rows.Add(this.CreateRow(this.CreateCell(webControl, null, null, HorizontalAlign.Right)));
				}
				if (this._login.FailureTextStyle.ForeColor.IsEmpty)
				{
					this._login.FailureTextStyle.ForeColor = Color.Red;
				}
				table.Rows.Add(this.CreateRow(this.CreateCell(literal, null, this._login.FailureTextStyle)));
				TableCell tableCell = new TableCell();
				this._login.RegisterApplyStyle(tableCell, this._login.HyperLinkStyle);
				if (this.AddLink(this._login.CreateUserUrl, this._login.CreateUserText, this._login.CreateUserIconUrl, tableCell, this._login.HyperLinkStyle))
				{
					if (this._login.Orientation == Orientation.Vertical)
					{
						tableCell.Controls.Add(new LiteralControl("<br/>"));
					}
					else
					{
						tableCell.Controls.Add(new LiteralControl(" "));
					}
				}
				if (this.AddLink(this._login.PasswordRecoveryUrl, this._login.PasswordRecoveryText, this._login.PasswordRecoveryIconUrl, tableCell, this._login.HyperLinkStyle))
				{
					if (this._login.Orientation == Orientation.Vertical)
					{
						tableCell.Controls.Add(new LiteralControl("<br/>"));
					}
					else
					{
						tableCell.Controls.Add(new LiteralControl(" "));
					}
				}
				this.AddLink(this._login.HelpPageUrl, this._login.HelpPageText, this._login.HelpPageIconUrl, tableCell, this._login.HyperLinkStyle);
				table.Rows.Add(this.CreateRow(tableCell));
				this.FixTableColumnSpans(table);
				container.Controls.Add(table);
			}

			// Token: 0x060028A2 RID: 10402 RVA: 0x0006A0B7 File Offset: 0x000682B7
			private TableRow CreateRow(TableCell cell)
			{
				return new TableRow
				{
					Cells = { cell }
				};
			}

			// Token: 0x060028A3 RID: 10403 RVA: 0x0006A0CC File Offset: 0x000682CC
			private TableRow CreateRow(Control c0, Control c1, Control c2, Style s)
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				TableCell tableCell2 = new TableCell();
				if (c0 != null)
				{
					tableCell.Controls.Add(c0);
					tableRow.Controls.Add(tableCell);
				}
				if (s != null)
				{
					tableCell.ApplyStyle(s);
				}
				if (c1 != null && c2 != null)
				{
					tableCell2.Controls.Add(c1);
					tableCell2.Controls.Add(c2);
					tableCell.HorizontalAlign = HorizontalAlign.Right;
					tableRow.Controls.Add(tableCell2);
				}
				return tableRow;
			}

			// Token: 0x060028A4 RID: 10404 RVA: 0x0006A144 File Offset: 0x00068344
			private TableCell CreateCell(Control c0, Control c1, Style s, HorizontalAlign align)
			{
				TableCell tableCell = this.CreateCell(c0, c1, s);
				tableCell.HorizontalAlign = align;
				return tableCell;
			}

			// Token: 0x060028A5 RID: 10405 RVA: 0x0006A158 File Offset: 0x00068358
			private TableCell CreateCell(Control c0, Control c1, Style s)
			{
				TableCell tableCell = new TableCell();
				if (s != null)
				{
					tableCell.ApplyStyle(s);
				}
				tableCell.Controls.Add(c0);
				if (c1 != null)
				{
					tableCell.Controls.Add(c1);
				}
				return tableCell;
			}

			// Token: 0x060028A6 RID: 10406 RVA: 0x0006A194 File Offset: 0x00068394
			private bool AddLink(string pageUrl, string linkText, string linkIcon, WebControl container, Style style)
			{
				bool flag = false;
				if (linkIcon.Length > 0)
				{
					Image image = new Image();
					image.ImageUrl = linkIcon;
					container.Controls.Add(image);
					flag = true;
				}
				if (linkText.Length > 0)
				{
					HyperLink hyperLink = new HyperLink();
					hyperLink.NavigateUrl = pageUrl;
					hyperLink.Text = linkText;
					this._login.RegisterApplyStyle(hyperLink, style);
					container.Controls.Add(hyperLink);
					flag = true;
				}
				return flag;
			}

			// Token: 0x060028A7 RID: 10407 RVA: 0x0006A204 File Offset: 0x00068404
			private void FixTableColumnSpans(Table table)
			{
				int num = 0;
				for (int i = 0; i < table.Rows.Count; i++)
				{
					if (num < table.Rows[i].Cells.Count)
					{
						num = table.Rows[i].Cells.Count;
					}
				}
				for (int j = 0; j < table.Rows.Count; j++)
				{
					if (table.Rows[j].Cells.Count == 1 && num > 1)
					{
						table.Rows[j].Cells[0].ColumnSpan = num;
					}
				}
			}

			// Token: 0x04001A88 RID: 6792
			private readonly Login _login;
		}
	}
}

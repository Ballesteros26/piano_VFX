using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a user interface that enable users to change their Web site password.</summary>
	// Token: 0x02000346 RID: 838
	[Designer("System.Web.UI.Design.WebControls.ChangePasswordDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("ChangedPassword")]
	[Bindable(true)]
	public class ChangePassword : CompositeControl, INamingContainer, IRenderOuterTable
	{
		/// <summary>Occurs when the user clicks the Cancel button to cancel changing a password.</summary>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06001E49 RID: 7753 RVA: 0x0004C0C7 File Offset: 0x0004A2C7
		// (remove) Token: 0x06001E4A RID: 7754 RVA: 0x0004C0DA File Offset: 0x0004A2DA
		public event EventHandler CancelButtonClick
		{
			add
			{
				this.events.AddHandler(ChangePassword.cancelButtonClickEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ChangePassword.cancelButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the password is changed for a user account.</summary>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06001E4B RID: 7755 RVA: 0x0004C0ED File Offset: 0x0004A2ED
		// (remove) Token: 0x06001E4C RID: 7756 RVA: 0x0004C100 File Offset: 0x0004A300
		public event EventHandler ChangedPassword
		{
			add
			{
				this.events.AddHandler(ChangePassword.changedPasswordEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ChangePassword.changedPasswordEvent, value);
			}
		}

		/// <summary>Occurs when there is an error changing the password for the user account.</summary>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06001E4D RID: 7757 RVA: 0x0004C113 File Offset: 0x0004A313
		// (remove) Token: 0x06001E4E RID: 7758 RVA: 0x0004C126 File Offset: 0x0004A326
		public event EventHandler ChangePasswordError
		{
			add
			{
				this.events.AddHandler(ChangePassword.changePasswordErrorEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ChangePassword.changePasswordErrorEvent, value);
			}
		}

		/// <summary>Occurs before the password for a user account is changed by the membership provider.</summary>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06001E4F RID: 7759 RVA: 0x0004C139 File Offset: 0x0004A339
		// (remove) Token: 0x06001E50 RID: 7760 RVA: 0x0004C14C File Offset: 0x0004A34C
		public event LoginCancelEventHandler ChangingPassword
		{
			add
			{
				this.events.AddHandler(ChangePassword.changingPasswordEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ChangePassword.changingPasswordEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ContinueButtonClick" /> event when the user clicks the Continue button.</summary>
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06001E51 RID: 7761 RVA: 0x0004C15F File Offset: 0x0004A35F
		// (remove) Token: 0x06001E52 RID: 7762 RVA: 0x0004C172 File Offset: 0x0004A372
		public event EventHandler ContinueButtonClick
		{
			add
			{
				this.events.AddHandler(ChangePassword.continueButtonClickEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ChangePassword.continueButtonClickEvent, value);
			}
		}

		/// <summary>Occurs before the user is sent an e-mail confirmation that the password has been changed.</summary>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06001E53 RID: 7763 RVA: 0x0004C185 File Offset: 0x0004A385
		// (remove) Token: 0x06001E54 RID: 7764 RVA: 0x0004C198 File Offset: 0x0004A398
		public event MailMessageEventHandler SendingMail
		{
			add
			{
				this.events.AddHandler(ChangePassword.sendingMailEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ChangePassword.sendingMailEvent, value);
			}
		}

		/// <summary>Occurs when there is an SMTP error sending an e-mail message to the user.</summary>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06001E55 RID: 7765 RVA: 0x0004C1AB File Offset: 0x0004A3AB
		// (remove) Token: 0x06001E56 RID: 7766 RVA: 0x0004C1BE File Offset: 0x0004A3BE
		public event SendMailErrorEventHandler SendMailError
		{
			add
			{
				this.events.AddHandler(ChangePassword.sendMailErrorEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ChangePassword.sendMailErrorEvent, value);
			}
		}

		/// <summary>Gets or sets the amount of padding, in pixels, inside the border and the designated area for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>The number of pixels of space between the contents of a <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control and the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control's border. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Web.UI.WebControls.ChangePassword.BorderPadding" /> property is less than -1.</exception>
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0004C1D1 File Offset: 0x0004A3D1
		// (set) Token: 0x06001E58 RID: 7768 RVA: 0x0004C1E4 File Offset: 0x0004A3E4
		[DefaultValue(1)]
		public virtual int BorderPadding
		{
			get
			{
				return this.ViewState.GetInt("BorderPadding", 1);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.ViewState["BorderPadding"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to display with the Cancel button, if the Cancel button is configured by the <see cref="P:System.Web.UI.WebControls.ChangePassword.CancelButtonType" /> property to be an image button.</summary>
		/// <returns>The URL of the image to display with the Cancel button. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0004C206 File Offset: 0x0004A406
		// (set) Token: 0x06001E5A RID: 7770 RVA: 0x0004C21D File Offset: 0x0004A41D
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string CancelButtonImageUrl
		{
			get
			{
				return this.ViewState.GetString("CancelButtonImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["CancelButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the Cancel button on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of the Cancel button. The default is null.</returns>
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0004C230 File Offset: 0x0004A430
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public Style CancelButtonStyle
		{
			get
			{
				if (this._cancelButtonStyle == null)
				{
					this._cancelButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						this._cancelButtonStyle.TrackViewState();
					}
				}
				return this._cancelButtonStyle;
			}
		}

		/// <summary>Gets or sets the text displayed on the Cancel button.</summary>
		/// <returns>The text to display on the Cancel button. The default is "Cancel".</returns>
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0004C25E File Offset: 0x0004A45E
		// (set) Token: 0x06001E5D RID: 7773 RVA: 0x0004C275 File Offset: 0x0004A475
		[Localizable(true)]
		public virtual string CancelButtonText
		{
			get
			{
				return this.ViewState.GetString("CancelButtonText", "Cancel");
			}
			set
			{
				this.ViewState["CancelButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button to use for the Cancel button when rendering the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ButtonType" /> object that defines the type of button to render for the Cancel button. The property value can be one of the three <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values: Button, Image, or Link. The default is Button.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <see cref="T:System.Web.UI.WebControls.ButtonType" /> is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0004C288 File Offset: 0x0004A488
		// (set) Token: 0x06001E5F RID: 7775 RVA: 0x0004C2B3 File Offset: 0x0004A4B3
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType CancelButtonType
		{
			get
			{
				if (this.ViewState["CancelButtonType"] != null)
				{
					return (ButtonType)this.ViewState["CancelButtonType"];
				}
				return ButtonType.Button;
			}
			set
			{
				this.ViewState["CancelButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the page that the user is shown after clicking the Cancel button in the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>The URL of the page the user is redirected to after clicking the Cancel button. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0004C2CB File Offset: 0x0004A4CB
		// (set) Token: 0x06001E61 RID: 7777 RVA: 0x0004C2E2 File Offset: 0x0004A4E2
		[Themeable(false)]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string CancelDestinationPageUrl
		{
			get
			{
				return this.ViewState.GetString("CancelDestinationPageUrl", string.Empty);
			}
			set
			{
				this.ViewState["CancelDestinationPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image displayed next to the Change Password button on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control if the Change Password button is configured by the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordButtonType" /> property to be an image button.</summary>
		/// <returns>The URL of the image to display next to the Change Password button. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0004C2F5 File Offset: 0x0004A4F5
		// (set) Token: 0x06001E63 RID: 7779 RVA: 0x0004C30C File Offset: 0x0004A50C
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string ChangePasswordButtonImageUrl
		{
			get
			{
				return this.ViewState.GetString("ChangePasswordButtonImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["ChangePasswordButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the Change Password button on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of the Change Password button. The default is null.</returns>
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x0004C31F File Offset: 0x0004A51F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public Style ChangePasswordButtonStyle
		{
			get
			{
				if (this._changePasswordButtonStyle == null)
				{
					this._changePasswordButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						this._changePasswordButtonStyle.TrackViewState();
					}
				}
				return this._changePasswordButtonStyle;
			}
		}

		/// <summary>Gets or sets the text displayed on the Change Password button.</summary>
		/// <returns>The text to display on the Change Password button. The default is "Change Password".</returns>
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x0004C34D File Offset: 0x0004A54D
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x0004C364 File Offset: 0x0004A564
		[Localizable(true)]
		public virtual string ChangePasswordButtonText
		{
			get
			{
				return this.ViewState.GetString("ChangePasswordButtonText", "Change Password");
			}
			set
			{
				this.ViewState["ChangePasswordButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button to use when rendering the Change Password button of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ButtonType" /> object that defines the type of button to render for the Change Password button. The property value can be one of the three <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values: Button, Image, or Link. The default is Button.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <see cref="T:System.Web.UI.WebControls.ButtonType" /> is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x0004C377 File Offset: 0x0004A577
		// (set) Token: 0x06001E68 RID: 7784 RVA: 0x0004C3A2 File Offset: 0x0004A5A2
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType ChangePasswordButtonType
		{
			get
			{
				if (this.ViewState["ChangePasswordButtonType"] != null)
				{
					return (ButtonType)this.ViewState["ChangePasswordButtonType"];
				}
				return ButtonType.Button;
			}
			set
			{
				this.ViewState["ChangePasswordButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the message that is shown when the user's password is not changed.</summary>
		/// <returns>The error message to display when the attempt to change the user's password is not successful. The default is "Your attempt to change passwords was unsuccessful. Please try again."</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x0004C3BA File Offset: 0x0004A5BA
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x0004C3D1 File Offset: 0x0004A5D1
		[Localizable(true)]
		public virtual string ChangePasswordFailureText
		{
			get
			{
				return this.ViewState.GetString("ChangePasswordFailureText", "Your attempt to change passwords was unsuccessful. Please try again.");
			}
			set
			{
				this.ViewState["ChangePasswordFailureText"] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> object used to display the Change Password view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control in the Change Password view. The default is null.</returns>
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x0004C3E4 File Offset: 0x0004A5E4
		// (set) Token: 0x06001E6C RID: 7788 RVA: 0x0004C3EC File Offset: 0x0004A5EC
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ChangePassword))]
		public virtual ITemplate ChangePasswordTemplate
		{
			get
			{
				return this._changePasswordTemplate;
			}
			set
			{
				this._changePasswordTemplate = value;
			}
		}

		/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control uses to create an instance of the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> template. This provides programmatic access to child controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" />.</returns>
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x0004C3F5 File Offset: 0x0004A5F5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control ChangePasswordTemplateContainer
		{
			get
			{
				if (this._changePasswordTemplateContainer == null)
				{
					this._changePasswordTemplateContainer = new ChangePassword.ChangePasswordContainer(this);
				}
				return this._changePasswordTemplateContainer;
			}
		}

		/// <summary>Gets or sets the text displayed at the top of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control in Change Password view.</summary>
		/// <returns>The text to display at the top of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is "Change Your Password".</returns>
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x0004C411 File Offset: 0x0004A611
		// (set) Token: 0x06001E6F RID: 7791 RVA: 0x0004C428 File Offset: 0x0004A628
		[Localizable(true)]
		public virtual string ChangePasswordTitleText
		{
			get
			{
				return this.ViewState.GetString("ChangePasswordTitleText", "Change Your Password");
			}
			set
			{
				this.ViewState["ChangePasswordTitleText"] = value;
			}
		}

		/// <summary>Gets the duplicate password entered by the user.</summary>
		/// <returns>The duplicate new password string entered by the user.</returns>
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x0004C43B File Offset: 0x0004A63B
		[Filterable(false)]
		[Browsable(false)]
		[Themeable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string ConfirmNewPassword
		{
			get
			{
				if (this._newPasswordConfirm == null)
				{
					return string.Empty;
				}
				return this._newPasswordConfirm;
			}
		}

		/// <summary>Gets or sets the label text for the <see cref="P:System.Web.UI.WebControls.ChangePassword.ConfirmNewPassword" /> text box.</summary>
		/// <returns>The text to display with the <see cref="P:System.Web.UI.WebControls.ChangePassword.ConfirmNewPassword" /> text box. The default is "Confirm New Password:".</returns>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x0004C451 File Offset: 0x0004A651
		// (set) Token: 0x06001E72 RID: 7794 RVA: 0x0004C468 File Offset: 0x0004A668
		[Localizable(true)]
		public virtual string ConfirmNewPasswordLabelText
		{
			get
			{
				return this.ViewState.GetString("ConfirmNewPasswordLabelText", "Confirm New Password:");
			}
			set
			{
				this.ViewState["ConfirmNewPasswordLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the message that is displayed when the new password and the duplicate password entered by the user are not identical.</summary>
		/// <returns>The error message displayed when the new password and confirmed password are not identical. The default is "The confirm New Password entry must match the New Password entry."</returns>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x0004C47B File Offset: 0x0004A67B
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x0004C492 File Offset: 0x0004A692
		[Localizable(true)]
		public virtual string ConfirmPasswordCompareErrorMessage
		{
			get
			{
				return this.ViewState.GetString("ConfirmPasswordCompareErrorMessage", "The Confirm New Password must match the New Password entry.");
			}
			set
			{
				this.ViewState["ConfirmPasswordCompareErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the error message that is displayed when the Confirm New Password text box is left empty.</summary>
		/// <returns>The error message that is displayed when users attempt to change their password without entering the new password in the <see cref="P:System.Web.UI.WebControls.ChangePassword.ConfirmNewPassword" /> input box.</returns>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x0004C4A5 File Offset: 0x0004A6A5
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x0004C4BC File Offset: 0x0004A6BC
		[Localizable(true)]
		public virtual string ConfirmPasswordRequiredErrorMessage
		{
			get
			{
				return this.ViewState.GetString("ConfirmPasswordRequiredErrorMessage", "Confirm New Password is required.");
			}
			set
			{
				this.ViewState["ConfirmPasswordRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to use for the Continue button on the Success view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control if the Continue button is configured by the <see cref="P:System.Web.UI.WebControls.ChangePassword.ContinueButtonType" /> property to be an image button.</summary>
		/// <returns>The URL of the image to display with the Continue button. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x0004C4CF File Offset: 0x0004A6CF
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x0004C4E6 File Offset: 0x0004A6E6
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string ContinueButtonImageUrl
		{
			get
			{
				return this.ViewState.GetString("ContinueButtonImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["ContinueButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the Continue button on the Success view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of the Continue button. The default is null.</returns>
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x0004C4F9 File Offset: 0x0004A6F9
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style ContinueButtonStyle
		{
			get
			{
				if (this._continueButtonStyle == null)
				{
					this._continueButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						this._continueButtonStyle.TrackViewState();
					}
				}
				return this._continueButtonStyle;
			}
		}

		/// <summary>Gets or sets the text that is displayed on the Continue button on the Success view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>The text to display on the Continue button. The default is "Continue".</returns>
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x0004C527 File Offset: 0x0004A727
		// (set) Token: 0x06001E7B RID: 7803 RVA: 0x0004C53E File Offset: 0x0004A73E
		[Localizable(true)]
		public virtual string ContinueButtonText
		{
			get
			{
				return this.ViewState.GetString("ContinueButtonText", "Continue");
			}
			set
			{
				this.ViewState["ContinueButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button to use when rendering the Continue button for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ButtonType" /> object that defines the type of button to render for the Continue button. The property value can be one of the three <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values: Button, Image, or Link. The default is Button.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <see cref="T:System.Web.UI.WebControls.ButtonType" /> is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x0004C551 File Offset: 0x0004A751
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x0004C57C File Offset: 0x0004A77C
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType ContinueButtonType
		{
			get
			{
				if (this.ViewState["ContinueButtonType"] != null)
				{
					return (ButtonType)this.ViewState["ContinueButtonType"];
				}
				return ButtonType.Button;
			}
			set
			{
				this.ViewState["ContinueButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the page that the user will see after clicking the Continue button on the Success view.</summary>
		/// <returns>The URL of the page the user is redirected to after clicking the Continue button. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0004C594 File Offset: 0x0004A794
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x0004C5AB File Offset: 0x0004A7AB
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Themeable(false)]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string ContinueDestinationPageUrl
		{
			get
			{
				return this.ViewState.GetString("ContinueDestinationPageUrl", string.Empty);
			}
			set
			{
				this.ViewState["ContinueDestinationPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to display next to the link to the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site.</summary>
		/// <returns>The URL of an image to display next to the link to the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0004C5BE File Offset: 0x0004A7BE
		// (set) Token: 0x06001E81 RID: 7809 RVA: 0x0004C5D5 File Offset: 0x0004A7D5
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string CreateUserIconUrl
		{
			get
			{
				return this.ViewState.GetString("CreateUserIconUrl", string.Empty);
			}
			set
			{
				this.ViewState["CreateUserIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of the link to the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site.</summary>
		/// <returns>The text to display next to the link to the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0004C5E8 File Offset: 0x0004A7E8
		// (set) Token: 0x06001E83 RID: 7811 RVA: 0x0004C5FF File Offset: 0x0004A7FF
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string CreateUserText
		{
			get
			{
				return this.ViewState.GetString("CreateUserText", string.Empty);
			}
			set
			{
				this.ViewState["CreateUserText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site.</summary>
		/// <returns>The URL of the Web page that contains a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0004C612 File Offset: 0x0004A812
		// (set) Token: 0x06001E85 RID: 7813 RVA: 0x0004C629 File Offset: 0x0004A829
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string CreateUserUrl
		{
			get
			{
				return this.ViewState.GetString("CreateUserUrl", string.Empty);
			}
			set
			{
				this.ViewState["CreateUserUrl"] = value;
			}
		}

		/// <summary>Gets the current password for the user.</summary>
		/// <returns>The current password entered by the user.</returns>
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0004C63C File Offset: 0x0004A83C
		[Filterable(false)]
		[Themeable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string CurrentPassword
		{
			get
			{
				if (this._currentPassword == null)
				{
					return string.Empty;
				}
				return this._currentPassword;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control should display the <see cref="P:System.Web.UI.WebControls.ChangePassword.UserName" /> control and label.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control should display the <see cref="P:System.Web.UI.WebControls.ChangePassword.UserName" />; otherwise, false. The default is false.</returns>
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0004C652 File Offset: 0x0004A852
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x0004C665 File Offset: 0x0004A865
		[DefaultValue(false)]
		public virtual bool DisplayUserName
		{
			get
			{
				return this.ViewState.GetBool("DisplayUserName", false);
			}
			set
			{
				this.ViewState["DisplayUserName"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to display next to the link to the user profile editing page for the Web site.</summary>
		/// <returns>The URL of the image to display with the link to the user profile editing page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0004C67D File Offset: 0x0004A87D
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x0004C694 File Offset: 0x0004A894
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string EditProfileIconUrl
		{
			get
			{
				return this.ViewState.GetString("EditProfileIconUrl", string.Empty);
			}
			set
			{
				this.ViewState["EditProfileIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of the link to the user profile editing page for the Web site.</summary>
		/// <returns>The text to display for the link to the user profile editing page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0004C6A7 File Offset: 0x0004A8A7
		// (set) Token: 0x06001E8C RID: 7820 RVA: 0x0004C6BE File Offset: 0x0004A8BE
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string EditProfileText
		{
			get
			{
				return this.ViewState.GetString("EditProfileText", string.Empty);
			}
			set
			{
				this.ViewState["EditProfileText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the user profile editing page for the Web site.</summary>
		/// <returns>The URL of the user profile editing page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0004C6D1 File Offset: 0x0004A8D1
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x0004C6E8 File Offset: 0x0004A8E8
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string EditProfileUrl
		{
			get
			{
				return this.ViewState.GetString("EditProfileUrl", string.Empty);
			}
			set
			{
				this.ViewState["EditProfileUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is null.</returns>
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0004C6FB File Offset: 0x0004A8FB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public TableItemStyle FailureTextStyle
		{
			get
			{
				if (this._failureTextStyle == null)
				{
					this._failureTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._failureTextStyle.TrackViewState();
					}
				}
				return this._failureTextStyle;
			}
		}

		/// <summary>Gets or sets the URL of an image to display next to the Change Password help page for the Web site.</summary>
		/// <returns>The URL of the image to display next to the Change Password help page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0004C729 File Offset: 0x0004A929
		// (set) Token: 0x06001E91 RID: 7825 RVA: 0x0004C740 File Offset: 0x0004A940
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string HelpPageIconUrl
		{
			get
			{
				return this.ViewState.GetString("HelpPageIconUrl", string.Empty);
			}
			set
			{
				this.ViewState["HelpPageIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the link text to the Change Password help page for the Web site.</summary>
		/// <returns>The text to display for the link to the Change Password help page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0004C753 File Offset: 0x0004A953
		// (set) Token: 0x06001E93 RID: 7827 RVA: 0x0004C76A File Offset: 0x0004A96A
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string HelpPageText
		{
			get
			{
				return this.ViewState.GetString("HelpPageText", string.Empty);
			}
			set
			{
				this.ViewState["HelpPageText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the Change Password help page for the Web site.</summary>
		/// <returns>The URL of the Change Password help page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0004C77D File Offset: 0x0004A97D
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x0004C794 File Offset: 0x0004A994
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string HelpPageUrl
		{
			get
			{
				return this.ViewState.GetString("HelpPageUrl", string.Empty);
			}
			set
			{
				this.ViewState["HelpPageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of hyperlinks on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of hyperlinks on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is null.</returns>
		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0004C7A7 File Offset: 0x0004A9A7
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle HyperLinkStyle
		{
			get
			{
				if (this._hyperLinkStyle == null)
				{
					this._hyperLinkStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._hyperLinkStyle.TrackViewState();
					}
				}
				return this._hyperLinkStyle;
			}
		}

		/// <summary>Gets or sets informational text that appears on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control between the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTitleText" /> and the input boxes.</summary>
		/// <returns>The informational text to display on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control between the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTitleText" /> and the input boxes. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x0004C7D5 File Offset: 0x0004A9D5
		// (set) Token: 0x06001E98 RID: 7832 RVA: 0x0004C7EC File Offset: 0x0004A9EC
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string InstructionText
		{
			get
			{
				return this.ViewState.GetString("InstructionText", string.Empty);
			}
			set
			{
				this.ViewState["InstructionText"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the instructional text on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the instructional text contained in the <see cref="P:System.Web.UI.WebControls.ChangePassword.InstructionText" /> property. The default is null.</returns>
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x0004C7FF File Offset: 0x0004A9FF
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle InstructionTextStyle
		{
			get
			{
				if (this._instructionTextStyle == null)
				{
					this._instructionTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._instructionTextStyle.TrackViewState();
					}
				}
				return this._instructionTextStyle;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> objects that define the appearance of text box labels on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of text box labels on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is null.</returns>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0004C82D File Offset: 0x0004AA2D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public TableItemStyle LabelStyle
		{
			get
			{
				if (this._labelStyle == null)
				{
					this._labelStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._labelStyle.TrackViewState();
					}
				}
				return this._labelStyle;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the e-mail message that is sent to users after they have changed their password.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object that defines the e-mail message sent to a new user.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.WebControls.MailDefinition.From" /> property is not set to an e-mail address.</exception>
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0004C85B File Offset: 0x0004AA5B
		[Themeable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MailDefinition MailDefinition
		{
			get
			{
				if (this._mailDefinition == null)
				{
					this._mailDefinition = new MailDefinition();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._mailDefinition).TrackViewState();
					}
				}
				return this._mailDefinition;
			}
		}

		/// <summary>Gets or sets the membership provider that is used to manage member information.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Security.MembershipProvider" /> for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is the membership provider for the application.</returns>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0004C88C File Offset: 0x0004AA8C
		// (set) Token: 0x06001E9D RID: 7837 RVA: 0x0004C8B9 File Offset: 0x0004AAB9
		[DefaultValue("")]
		[Themeable(false)]
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
				}
				else
				{
					this.ViewState["MembershipProvider"] = value;
				}
				this._provider = null;
			}
		}

		/// <summary>Gets the new password entered by the user.</summary>
		/// <returns>The new password entered by the user.</returns>
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0004C8E8 File Offset: 0x0004AAE8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[Filterable(false)]
		public virtual string NewPassword
		{
			get
			{
				if (this._newPassword == null)
				{
					return string.Empty;
				}
				return this._newPassword;
			}
		}

		/// <summary>Gets or sets the label text for the New Password text box.</summary>
		/// <returns>The text to display next to the New Password text box. The default is "New Password:".</returns>
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0004C8FE File Offset: 0x0004AAFE
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x0004C915 File Offset: 0x0004AB15
		[Localizable(true)]
		public virtual string NewPasswordLabelText
		{
			get
			{
				return this.ViewState.GetString("NewPasswordLabelText", "New Password:");
			}
			set
			{
				this.ViewState["NewPasswordLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the regular expression that is used to validate the password provided by the user.</summary>
		/// <returns>The regular expression string used to validate the new password provided by the user. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0004C928 File Offset: 0x0004AB28
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x0004C93F File Offset: 0x0004AB3F
		public virtual string NewPasswordRegularExpression
		{
			get
			{
				return this.ViewState.GetString("NewPasswordRegularExpression", string.Empty);
			}
			set
			{
				this.ViewState["NewPasswordRegularExpression"] = value;
			}
		}

		/// <summary>Gets or sets the error message that is shown when the password entered does not pass the regular expression criteria defined in the <see cref="P:System.Web.UI.WebControls.ChangePassword.NewPasswordRegularExpression" /> property.</summary>
		/// <returns>The error message shown when the password entered does not pass the regular expression defined in the <see cref="P:System.Web.UI.WebControls.ChangePassword.NewPasswordRegularExpression" />. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x0004C952 File Offset: 0x0004AB52
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x0004C969 File Offset: 0x0004AB69
		public virtual string NewPasswordRegularExpressionErrorMessage
		{
			get
			{
				return this.ViewState.GetString("NewPasswordRegularExpressionErrorMessage", string.Empty);
			}
			set
			{
				this.ViewState["NewPasswordRegularExpressionErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the error message that is displayed when the user leaves the New Password text box empty.</summary>
		/// <returns>The error message to display if the user leaves the New Password text box empty. The default is "New Password is required."</returns>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0004C97C File Offset: 0x0004AB7C
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x0004C993 File Offset: 0x0004AB93
		[Localizable(true)]
		public virtual string NewPasswordRequiredErrorMessage
		{
			get
			{
				return this.ViewState.GetString("NewPasswordRequiredErrorMessage", "New Password is required.");
			}
			set
			{
				this.ViewState["NewPasswordRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of hint text that appears on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the text contained in the <see cref="P:System.Web.UI.WebControls.ChangePassword.PasswordHintText" /> property. The default is null.</returns>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x0004C9A6 File Offset: 0x0004ABA6
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle PasswordHintStyle
		{
			get
			{
				if (this._passwordHintStyle == null)
				{
					this._passwordHintStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._passwordHintStyle.TrackViewState();
					}
				}
				return this._passwordHintStyle;
			}
		}

		/// <summary>Gets or sets informational text about the requirements for creating a password for the Web site.</summary>
		/// <returns>The informational text to display about the criteria for the new password. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0004C9D4 File Offset: 0x0004ABD4
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0004C9EB File Offset: 0x0004ABEB
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string PasswordHintText
		{
			get
			{
				return this.ViewState.GetString("PasswordHintText", string.Empty);
			}
			set
			{
				this.ViewState["PasswordHintText"] = value;
			}
		}

		/// <summary>Gets or sets the label text for the Current Password text box.</summary>
		/// <returns>The text to display next to the Current Password text box. The default is "Password:".</returns>
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0004C9FE File Offset: 0x0004ABFE
		// (set) Token: 0x06001EAB RID: 7851 RVA: 0x0004CA15 File Offset: 0x0004AC15
		[Localizable(true)]
		public virtual string PasswordLabelText
		{
			get
			{
				return this.ViewState.GetString("PasswordLabelText", "Password:");
			}
			set
			{
				this.ViewState["PasswordLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to display next to a link to the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>The URL of the image to display next to a link to the password recovery page for the Web site. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x0004CA28 File Offset: 0x0004AC28
		// (set) Token: 0x06001EAD RID: 7853 RVA: 0x0004CA3F File Offset: 0x0004AC3F
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string PasswordRecoveryIconUrl
		{
			get
			{
				return this.ViewState.GetString("PasswordRecoveryIconUrl", string.Empty);
			}
			set
			{
				this.ViewState["PasswordRecoveryIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of the link to the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>The text to display for the link to the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x0004CA52 File Offset: 0x0004AC52
		// (set) Token: 0x06001EAF RID: 7855 RVA: 0x0004CA69 File Offset: 0x0004AC69
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string PasswordRecoveryText
		{
			get
			{
				return this.ViewState.GetString("PasswordRecoveryText", string.Empty);
			}
			set
			{
				this.ViewState["PasswordRecoveryText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>The URL for the Web page that contains the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0004CA7C File Offset: 0x0004AC7C
		// (set) Token: 0x06001EB1 RID: 7857 RVA: 0x0004CA93 File Offset: 0x0004AC93
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string PasswordRecoveryUrl
		{
			get
			{
				return this.ViewState.GetString("PasswordRecoveryUrl", string.Empty);
			}
			set
			{
				this.ViewState["PasswordRecoveryUrl"] = value;
			}
		}

		/// <summary>Gets or sets the error message that is displayed when the user leaves the Current Password text box empty.</summary>
		/// <returns>The error message to display if the user leaves the Current Password text box empty.</returns>
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x0004CAA6 File Offset: 0x0004ACA6
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x0004CABD File Offset: 0x0004ACBD
		[Localizable(true)]
		public virtual string PasswordRequiredErrorMessage
		{
			get
			{
				return this.ViewState.GetString("PasswordRequiredErrorMessage", string.Empty);
			}
			set
			{
				this.ViewState["PasswordRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control encloses rendered HTML in a table element in order to apply inline styles.</summary>
		/// <returns>true if the control encloses rendered HTML in a table element; otherwise, false. The default is true.</returns>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x0004CAD0 File Offset: 0x0004ACD0
		// (set) Token: 0x06001EB5 RID: 7861 RVA: 0x0004CAD8 File Offset: 0x0004ACD8
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

		/// <summary>Gets or sets the URL of the page that is shown to users after they have changed their password successfully.</summary>
		/// <returns>The URL of the destination page after the password is changed. The default is <see cref="F:System.String.Empty" />. </returns>
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x0004CAE1 File Offset: 0x0004ACE1
		// (set) Token: 0x06001EB7 RID: 7863 RVA: 0x0004CAF8 File Offset: 0x0004ACF8
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string SuccessPageUrl
		{
			get
			{
				return this.ViewState.GetString("SuccessPageUrl", string.Empty);
			}
			set
			{
				this.ViewState["SuccessPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> object that is used to display the Success and Change Password views of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for displaying the Success and Change Password views of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is null.</returns>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x0004CB0B File Offset: 0x0004AD0B
		// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x0004CB13 File Offset: 0x0004AD13
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ChangePassword))]
		[Browsable(false)]
		public virtual ITemplate SuccessTemplate
		{
			get
			{
				return this._successTemplate;
			}
			set
			{
				this._successTemplate = value;
			}
		}

		/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control used to create an instance of the <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessTemplate" /> template. This provides programmatic access to child controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessTemplate" />.</returns>
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0004CB1C File Offset: 0x0004AD1C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control SuccessTemplateContainer
		{
			get
			{
				if (this._successTemplateContainer == null)
				{
					this._successTemplateContainer = new ChangePassword.SuccessContainer(this);
				}
				return this._successTemplateContainer;
			}
		}

		/// <summary>Gets or sets the text that is displayed on the Success view between the <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessTitleText" /> and the Continue button.</summary>
		/// <returns>The text to display on the Success view between the <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessTitleText" /> and the Continue button. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x0004CB38 File Offset: 0x0004AD38
		// (set) Token: 0x06001EBC RID: 7868 RVA: 0x0004CB4F File Offset: 0x0004AD4F
		[Localizable(true)]
		public virtual string SuccessText
		{
			get
			{
				return this.ViewState.GetString("SuccessText", "Your password has been changed!");
			}
			set
			{
				this.ViewState["SuccessText"] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of text on the Success view.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of the text contained in the <see cref="P:System.Web.UI.WebControls.ChangePassword.SuccessText" /> property. The default is null.</returns>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x0004CB62 File Offset: 0x0004AD62
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public TableItemStyle SuccessTextStyle
		{
			get
			{
				if (this._successTextStyle == null)
				{
					this._successTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._successTextStyle.TrackViewState();
					}
				}
				return this._successTextStyle;
			}
		}

		/// <summary>Gets or sets the title of the Success view.</summary>
		/// <returns>The text to display as the title in the Success view of the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is "Change Password Complete".</returns>
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0004CB90 File Offset: 0x0004AD90
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x0004CBA7 File Offset: 0x0004ADA7
		[Localizable(true)]
		public virtual string SuccessTitleText
		{
			get
			{
				return this.ViewState.GetString("SuccessTitleText", "Change Password Complete");
			}
			set
			{
				this.ViewState["SuccessTitleText"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of text box controls on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of text box controls on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is null.</returns>
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0004CBBA File Offset: 0x0004ADBA
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style TextBoxStyle
		{
			get
			{
				if (this._textBoxStyle == null)
				{
					this._textBoxStyle = new Style();
					if (base.IsTrackingViewState)
					{
						this._textBoxStyle.TrackViewState();
					}
				}
				return this._textBoxStyle;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of titles on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages titles on the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is null.</returns>
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0004CBE8 File Offset: 0x0004ADE8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle TitleTextStyle
		{
			get
			{
				if (this._titleTextStyle == null)
				{
					this._titleTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._titleTextStyle.TrackViewState();
					}
				}
				return this._titleTextStyle;
			}
		}

		/// <summary>Gets or sets the Web site user name for which to change the password.</summary>
		/// <returns>The user name for which to change the password.</returns>
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x0004CC18 File Offset: 0x0004AE18
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x0004CC6C File Offset: 0x0004AE6C
		[DefaultValue("")]
		public virtual string UserName
		{
			get
			{
				if (this._username == null && HttpContext.Current.Request.IsAuthenticated)
				{
					this._username = HttpContext.Current.User.Identity.Name;
				}
				if (this._username == null)
				{
					return string.Empty;
				}
				return this._username;
			}
			set
			{
				this._username = value;
			}
		}

		/// <summary>Gets or sets the label for the User Name text box.</summary>
		/// <returns>The text to display next to the User Name textbox. The default string is "User Name:".</returns>
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x0004CC75 File Offset: 0x0004AE75
		// (set) Token: 0x06001EC5 RID: 7877 RVA: 0x0004CC8C File Offset: 0x0004AE8C
		[Localizable(true)]
		public virtual string UserNameLabelText
		{
			get
			{
				return this.ViewState.GetString("UserNameLabelText", "User Name:");
			}
			set
			{
				this.ViewState["UserNameLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the error message that is displayed when the user leaves the User Name text box empty.</summary>
		/// <returns>The error message to display if the user leaves the User Name text box empty. The default string is "User Name is required.".</returns>
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x0004CC9F File Offset: 0x0004AE9F
		// (set) Token: 0x06001EC7 RID: 7879 RVA: 0x0004CCB6 File Offset: 0x0004AEB6
		[Localizable(true)]
		public virtual string UserNameRequiredErrorMessage
		{
			get
			{
				return this.ViewState.GetString("UserNameRequiredErrorMessage", "User Name is required.");
			}
			set
			{
				this.ViewState["UserNameRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages that are associated with any input validation used by the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the appearance of error messages that are associated with any input validation used by the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. The default is null.</returns>
		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0004CCC9 File Offset: 0x0004AEC9
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style ValidatorTextStyle
		{
			get
			{
				if (this._validatorTextStyle == null)
				{
					this._validatorTextStyle = new Style();
					if (base.IsTrackingViewState)
					{
						this._validatorTextStyle.TrackViewState();
					}
				}
				return this._validatorTextStyle;
			}
		}

		/// <summary>Creates the individual controls that make up the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control in preparation for posting back or rendering.</summary>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.WebControls.ChangePassword.DisplayUserName" /> property is set to false, the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> contains a control that implements the <see cref="T:System.Web.UI.IEditableTextControl" /> interface, and the <see cref="P:System.Web.UI.Control.ID" /> property of the control is set to "UserName".-or-The <see cref="P:System.Web.UI.WebControls.ChangePassword.DisplayUserName" /> property is set to true, the <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> does not contain a control that implements the <see cref="T:System.Web.UI.IEditableTextControl" /> interface, and the <see cref="P:System.Web.UI.Control.ID" /> property of the control is set to "UserName".-or-The <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> does not contain a control that implements the <see cref="T:System.Web.UI.IEditableTextControl" /> interface, and the <see cref="P:System.Web.UI.Control.ID" /> property of the control is set to "CurrentPassword".-or-The <see cref="P:System.Web.UI.WebControls.ChangePassword.ChangePasswordTemplate" /> does not contain a control that implements the <see cref="T:System.Web.UI.IEditableTextControl" /> interface, and the <see cref="P:System.Web.UI.Control.ID" /> property of the control is set to "NewPassword".</exception>
		// Token: 0x06001EC9 RID: 7881 RVA: 0x0004CCF8 File Offset: 0x0004AEF8
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			ITemplate template = this.ChangePasswordTemplate;
			if (template == null)
			{
				template = new ChangePassword.ChangePasswordDeafultTemplate(this);
			}
			((ChangePassword.ChangePasswordContainer)this.ChangePasswordTemplateContainer).InstantiateTemplate(template);
			ITemplate template2 = this.SuccessTemplate;
			if (template2 == null)
			{
				template2 = new ChangePassword.SuccessDefaultTemplate(this);
			}
			((ChangePassword.SuccessContainer)this.SuccessTemplateContainer).InstantiateTemplate(template2);
			this.Controls.AddAt(0, this.ChangePasswordTemplateContainer);
			this.Controls.AddAt(1, this.SuccessTemplateContainer);
			ChangePassword.ChangePasswordContainer changePasswordContainer = (ChangePassword.ChangePasswordContainer)this.ChangePasswordTemplateContainer;
			IEditableTextControl editableTextControl;
			if (this.DisplayUserName)
			{
				editableTextControl = changePasswordContainer.UserNameTextBox;
				if (editableTextControl != null)
				{
					editableTextControl.TextChanged += this.UserName_TextChanged;
				}
			}
			editableTextControl = changePasswordContainer.CurrentPasswordTextBox;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.CurrentPassword_TextChanged;
			}
			editableTextControl = changePasswordContainer.NewPasswordTextBox;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.NewPassword_TextChanged;
			}
			editableTextControl = changePasswordContainer.ConfirmNewPasswordTextBox;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.NewPasswordConfirm_TextChanged;
			}
		}

		/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream used to write content to a Web page.</param>
		// Token: 0x06001ECA RID: 7882 RVA: 0x0004CDFC File Offset: 0x0004AFFC
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.VerifyInlinePropertiesNotSet();
			for (int i = 0; i < this.Controls.Count; i++)
			{
				if (this.Controls[i].Visible)
				{
					this.Controls[i].Render(writer);
				}
			}
		}

		/// <summary>Sets design-time data for a control.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control. </param>
		// Token: 0x06001ECB RID: 7883 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0004CE4C File Offset: 0x0004B04C
		private void InitMemberShipProvider()
		{
			string membershipProvider = this.MembershipProvider;
			this._provider = ((membershipProvider.Length == 0) ? Membership.Provider : Membership.Providers[membershipProvider]);
			if (this._provider == null)
			{
				throw new HttpException(global::Locale.GetText("No provider named '{0}' could be found.", new object[] { membershipProvider }));
			}
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0004CEA4 File Offset: 0x0004B0A4
		private void ProcessChangePasswordEvent(CommandEventArgs args)
		{
			if (!this.Page.IsValid)
			{
				return;
			}
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnChangingPassword(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return;
			}
			bool flag = false;
			try
			{
				flag = this.MembershipProviderInternal.ChangePassword(this.UserName, this.CurrentPassword, this.NewPassword);
			}
			catch
			{
			}
			if (flag)
			{
				this.OnChangedPassword(args);
				this._showContinue = true;
				if (this._mailDefinition != null)
				{
					this.SendMail(this.UserName, this.NewPassword);
					return;
				}
			}
			else
			{
				this.OnChangePasswordError(EventArgs.Empty);
				string text = string.Format("Password incorrect or New Password invalid. New Password length minimum: {0}. Non-alphanumeric characters required: {1}.", this.MembershipProviderInternal.MinRequiredPasswordLength, this.MembershipProviderInternal.MinRequiredNonAlphanumericCharacters);
				((ChangePassword.ChangePasswordContainer)this.ChangePasswordTemplateContainer).FailureTextLiteral.Text = text;
				this._showContinue = false;
			}
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x0004CF8C File Offset: 0x0004B18C
		private void ProcessCancelEvent(CommandEventArgs args)
		{
			this.OnCancelButtonClick(args);
			if (this.ContinueDestinationPageUrl.Length > 0)
			{
				this.Context.Response.Redirect(this.ContinueDestinationPageUrl);
			}
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x0004CFB9 File Offset: 0x0004B1B9
		private void ProcessContinueEvent(CommandEventArgs args)
		{
			this.OnContinueButtonClick(args);
			if (this.ContinueDestinationPageUrl.Length > 0)
			{
				this.Context.Response.Redirect(this.ContinueDestinationPageUrl);
			}
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0004CFE8 File Offset: 0x0004B1E8
		private void SendMail(string username, string password)
		{
			MembershipUser user = this.MembershipProviderInternal.GetUser(this.UserName, false);
			if (user == null)
			{
				return;
			}
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add("<%USERNAME%>", username);
			listDictionary.Add("<%PASSWORD%>", password);
			MailMessage mailMessage = this.MailDefinition.CreateMailMessage(user.Email, listDictionary, this);
			MailMessageEventArgs mailMessageEventArgs = new MailMessageEventArgs(mailMessage);
			this.OnSendingMail(mailMessageEventArgs);
			SmtpClient smtpClient = new SmtpClient();
			try
			{
				smtpClient.Send(mailMessage);
			}
			catch (Exception ex)
			{
				SendMailErrorEventArgs sendMailErrorEventArgs = new SendMailErrorEventArgs(ex);
				this.OnSendMailError(sendMailErrorEventArgs);
				if (!sendMailErrorEventArgs.Handled)
				{
					throw ex;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to a <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. This property is used primarily by control developers.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control. Always returns HtmlTextWriterTag.Table.</returns>
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x0004D090 File Offset: 0x0004B290
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0004D094 File Offset: 0x0004B294
		internal virtual MembershipProvider MembershipProviderInternal
		{
			get
			{
				if (this._provider == null)
				{
					this.InitMemberShipProvider();
				}
				return this._provider;
			}
		}

		/// <summary>Restores control state information from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.ChangePassword.SaveControlState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to restore.</param>
		// Token: 0x06001ED3 RID: 7891 RVA: 0x0004D0AC File Offset: 0x0004B2AC
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			this._showContinue = (bool)array[1];
			this._username = (string)array[2];
		}

		/// <summary>Saves any server control state changes that have occurred since the time the page was posted back to the server.</summary>
		/// <returns>The server control's current state; otherwise, null.</returns>
		// Token: 0x06001ED4 RID: 7892 RVA: 0x0004D0EC File Offset: 0x0004B2EC
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			return new object[] { obj, this._showContinue, this._username };
		}

		/// <summary>Restores view state information from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.ChangePassword.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to restore.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="savedState" /> parameter cannot be resolved to a valid <see cref="P:System.Web.UI.Control.ViewState" />.</exception>
		// Token: 0x06001ED5 RID: 7893 RVA: 0x0004D124 File Offset: 0x0004B324
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				this.CancelButtonStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.ChangePasswordButtonStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.ContinueButtonStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				this.FailureTextStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				this.HyperLinkStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				this.InstructionTextStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				this.LabelStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				this.PasswordHintStyle.LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				this.SuccessTextStyle.LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				this.TextBoxStyle.LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				this.TitleTextStyle.LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				this.ValidatorTextStyle.LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				((IStateManager)this.MailDefinition).LoadViewState(array[13]);
			}
		}

		/// <summary>Saves any server control view state changes that have occurred since the time the page was posted back to the server.</summary>
		/// <returns>The server control's current view state; otherwise, null.</returns>
		// Token: 0x06001ED6 RID: 7894 RVA: 0x0004D248 File Offset: 0x0004B448
		protected override object SaveViewState()
		{
			object[] array = new object[14];
			array[0] = base.SaveViewState();
			if (this._cancelButtonStyle != null)
			{
				array[1] = this._cancelButtonStyle.SaveViewState();
			}
			if (this._changePasswordButtonStyle != null)
			{
				array[2] = this._changePasswordButtonStyle.SaveViewState();
			}
			if (this._continueButtonStyle != null)
			{
				array[3] = this._continueButtonStyle.SaveViewState();
			}
			if (this._failureTextStyle != null)
			{
				array[4] = this._failureTextStyle.SaveViewState();
			}
			if (this._hyperLinkStyle != null)
			{
				array[5] = this._hyperLinkStyle.SaveViewState();
			}
			if (this._instructionTextStyle != null)
			{
				array[6] = this._instructionTextStyle.SaveViewState();
			}
			if (this._labelStyle != null)
			{
				array[7] = this._labelStyle.SaveViewState();
			}
			if (this._passwordHintStyle != null)
			{
				array[8] = this._passwordHintStyle.SaveViewState();
			}
			if (this._successTextStyle != null)
			{
				array[9] = this._successTextStyle.SaveViewState();
			}
			if (this._textBoxStyle != null)
			{
				array[10] = this._textBoxStyle.SaveViewState();
			}
			if (this._titleTextStyle != null)
			{
				array[11] = this._titleTextStyle.SaveViewState();
			}
			if (this._validatorTextStyle != null)
			{
				array[12] = this._validatorTextStyle.SaveViewState();
			}
			if (this._mailDefinition != null)
			{
				array[13] = ((IStateManager)this._mailDefinition).SaveViewState();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Causes tracking of view-state changes to the server control so that they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property. </summary>
		// Token: 0x06001ED7 RID: 7895 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._cancelButtonStyle != null)
			{
				this._cancelButtonStyle.TrackViewState();
			}
			if (this._changePasswordButtonStyle != null)
			{
				this._changePasswordButtonStyle.TrackViewState();
			}
			if (this._continueButtonStyle != null)
			{
				this._continueButtonStyle.TrackViewState();
			}
			if (this._failureTextStyle != null)
			{
				this._failureTextStyle.TrackViewState();
			}
			if (this._hyperLinkStyle != null)
			{
				this._hyperLinkStyle.TrackViewState();
			}
			if (this._instructionTextStyle != null)
			{
				this._instructionTextStyle.TrackViewState();
			}
			if (this._labelStyle != null)
			{
				this._labelStyle.TrackViewState();
			}
			if (this._passwordHintStyle != null)
			{
				this._passwordHintStyle.TrackViewState();
			}
			if (this._successTextStyle != null)
			{
				this._successTextStyle.TrackViewState();
			}
			if (this._textBoxStyle != null)
			{
				this._textBoxStyle.TrackViewState();
			}
			if (this._titleTextStyle != null)
			{
				this._titleTextStyle.TrackViewState();
			}
			if (this._validatorTextStyle != null)
			{
				this._validatorTextStyle.TrackViewState();
			}
			if (this._mailDefinition != null)
			{
				((IStateManager)this._mailDefinition).TrackViewState();
			}
		}

		/// <summary>Determines whether the event for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control is passed up the Web server control hierarchy for the page.</summary>
		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001ED8 RID: 7896 RVA: 0x0004D4AC File Offset: 0x0004B6AC
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (e != null)
			{
				if (commandEventArgs.CommandName == ChangePassword.ChangePasswordButtonCommandName)
				{
					this.ProcessChangePasswordEvent(commandEventArgs);
					return true;
				}
				if (commandEventArgs.CommandName == ChangePassword.CancelButtonCommandName)
				{
					this.ProcessCancelEvent(commandEventArgs);
					return true;
				}
				if (commandEventArgs.CommandName == ChangePassword.ContinueButtonCommandName)
				{
					this.ProcessContinueEvent(commandEventArgs);
					return true;
				}
			}
			return base.OnBubbleEvent(source, e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.CancelButtonClick" /> event when a user clicks the Cancel button.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001ED9 RID: 7897 RVA: 0x0004D51C File Offset: 0x0004B71C
		protected virtual void OnCancelButtonClick(EventArgs e)
		{
			EventHandler eventHandler = this.events[ChangePassword.cancelButtonClickEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ChangedPassword" /> event after the password is changed.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06001EDA RID: 7898 RVA: 0x0004D54C File Offset: 0x0004B74C
		protected virtual void OnChangedPassword(EventArgs e)
		{
			EventHandler eventHandler = this.events[ChangePassword.changedPasswordEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ChangePasswordError" /> event when the user's password is not changed.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001EDB RID: 7899 RVA: 0x0004D57C File Offset: 0x0004B77C
		protected virtual void OnChangePasswordError(EventArgs e)
		{
			EventHandler eventHandler = this.events[ChangePassword.changePasswordErrorEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ChangingPassword" /> event before the user's password is changed by the membership provider.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> object containing the event data.</param>
		// Token: 0x06001EDC RID: 7900 RVA: 0x0004D5AC File Offset: 0x0004B7AC
		protected virtual void OnChangingPassword(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = this.events[ChangePassword.changingPasswordEvent] as LoginCancelEventHandler;
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.ContinueButtonClick" /> event when a user clicks the Continue button.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001EDD RID: 7901 RVA: 0x0004D5DC File Offset: 0x0004B7DC
		protected virtual void OnContinueButtonClick(EventArgs e)
		{
			EventHandler eventHandler = this.events[ChangePassword.continueButtonClickEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control to allow the control to register itself with the page.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object containing the event data.</param>
		// Token: 0x06001EDE RID: 7902 RVA: 0x0004D60A File Offset: 0x0004B80A
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.RegisterRequiresControlState(this);
			base.OnInit(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object containing the event data.</param>
		// Token: 0x06001EDF RID: 7903 RVA: 0x0004D61F File Offset: 0x0004B81F
		protected internal override void OnPreRender(EventArgs e)
		{
			this.ChangePasswordTemplateContainer.Visible = !this._showContinue;
			this.SuccessTemplateContainer.Visible = this._showContinue;
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.SendingMail" /> event before an e-mail message is sent to the SMTP server for processing. The SMTP server then sends the e-mail message to the user.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> object containing the event data.</param>
		// Token: 0x06001EE0 RID: 7904 RVA: 0x0004D650 File Offset: 0x0004B850
		protected virtual void OnSendingMail(MailMessageEventArgs e)
		{
			MailMessageEventHandler mailMessageEventHandler = this.events[ChangePassword.sendingMailEvent] as MailMessageEventHandler;
			if (mailMessageEventHandler != null)
			{
				mailMessageEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ChangePassword.SendMailError" /> event when an e-mail message cannot be sent to the user.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> object containing the event data.</param>
		// Token: 0x06001EE1 RID: 7905 RVA: 0x0004D680 File Offset: 0x0004B880
		protected virtual void OnSendMailError(SendMailErrorEventArgs e)
		{
			SendMailErrorEventHandler sendMailErrorEventHandler = this.events[ChangePassword.sendMailErrorEvent] as SendMailErrorEventHandler;
			if (sendMailErrorEventHandler != null)
			{
				sendMailErrorEventHandler(this, e);
			}
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0004D6AE File Offset: 0x0004B8AE
		private void UserName_TextChanged(object sender, EventArgs e)
		{
			this.UserName = ((ITextControl)sender).Text;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0004D6C1 File Offset: 0x0004B8C1
		private void CurrentPassword_TextChanged(object sender, EventArgs e)
		{
			this._currentPassword = ((ITextControl)sender).Text;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0004D6D4 File Offset: 0x0004B8D4
		private void NewPassword_TextChanged(object sender, EventArgs e)
		{
			this._newPassword = ((ITextControl)sender).Text;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0004D6E7 File Offset: 0x0004B8E7
		private void NewPasswordConfirm_TextChanged(object sender, EventArgs e)
		{
			this._newPasswordConfirm = ((ITextControl)sender).Text;
		}

		// Token: 0x04001856 RID: 6230
		private static readonly object cancelButtonClickEvent = new object();

		// Token: 0x04001857 RID: 6231
		private static readonly object changedPasswordEvent = new object();

		// Token: 0x04001858 RID: 6232
		private static readonly object changePasswordErrorEvent = new object();

		// Token: 0x04001859 RID: 6233
		private static readonly object changingPasswordEvent = new object();

		// Token: 0x0400185A RID: 6234
		private static readonly object continueButtonClickEvent = new object();

		// Token: 0x0400185B RID: 6235
		private static readonly object sendingMailEvent = new object();

		// Token: 0x0400185C RID: 6236
		private static readonly object sendMailErrorEvent = new object();

		/// <summary>Represents the CommandName value of the Cancel button. This field is read-only.</summary>
		// Token: 0x0400185D RID: 6237
		public static readonly string CancelButtonCommandName = "Cancel";

		/// <summary>Represents the CommandName value of the Change Password button. This field is read-only.</summary>
		// Token: 0x0400185E RID: 6238
		public static readonly string ChangePasswordButtonCommandName = "ChangePassword";

		/// <summary>Represents CommandName value of the Continue button. This field is read-only.</summary>
		// Token: 0x0400185F RID: 6239
		public static readonly string ContinueButtonCommandName = "Continue";

		// Token: 0x04001860 RID: 6240
		private bool renderOuterTable = true;

		// Token: 0x04001861 RID: 6241
		private Style _cancelButtonStyle;

		// Token: 0x04001862 RID: 6242
		private Style _changePasswordButtonStyle;

		// Token: 0x04001863 RID: 6243
		private Style _continueButtonStyle;

		// Token: 0x04001864 RID: 6244
		private TableItemStyle _failureTextStyle;

		// Token: 0x04001865 RID: 6245
		private TableItemStyle _hyperLinkStyle;

		// Token: 0x04001866 RID: 6246
		private TableItemStyle _instructionTextStyle;

		// Token: 0x04001867 RID: 6247
		private TableItemStyle _labelStyle;

		// Token: 0x04001868 RID: 6248
		private TableItemStyle _passwordHintStyle;

		// Token: 0x04001869 RID: 6249
		private TableItemStyle _successTextStyle;

		// Token: 0x0400186A RID: 6250
		private Style _textBoxStyle;

		// Token: 0x0400186B RID: 6251
		private TableItemStyle _titleTextStyle;

		// Token: 0x0400186C RID: 6252
		private Style _validatorTextStyle;

		// Token: 0x0400186D RID: 6253
		private MailDefinition _mailDefinition;

		// Token: 0x0400186E RID: 6254
		private MembershipProvider _provider;

		// Token: 0x0400186F RID: 6255
		private ITemplate _changePasswordTemplate;

		// Token: 0x04001870 RID: 6256
		private ITemplate _successTemplate;

		// Token: 0x04001871 RID: 6257
		private Control _changePasswordTemplateContainer;

		// Token: 0x04001872 RID: 6258
		private Control _successTemplateContainer;

		// Token: 0x04001873 RID: 6259
		private string _username;

		// Token: 0x04001874 RID: 6260
		private string _currentPassword;

		// Token: 0x04001875 RID: 6261
		private string _newPassword;

		// Token: 0x04001876 RID: 6262
		private string _newPasswordConfirm;

		// Token: 0x04001877 RID: 6263
		private bool _showContinue;

		// Token: 0x04001878 RID: 6264
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x02000347 RID: 839
		private class BaseChangePasswordContainer : Control, INamingContainer, INonBindingContainer
		{
			// Token: 0x06001EE8 RID: 7912 RVA: 0x0004D785 File Offset: 0x0004B985
			public BaseChangePasswordContainer(ChangePassword owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				this._owner = owner;
				this.renderOuterTable = this._owner.RenderOuterTable;
				if (this.renderOuterTable)
				{
					this.InitTable();
				}
			}

			// Token: 0x06001EE9 RID: 7913 RVA: 0x0004D7C1 File Offset: 0x0004B9C1
			public void InstantiateTemplate(ITemplate template)
			{
				if (!this._owner.RenderOuterTable)
				{
					template.InstantiateIn(this);
					return;
				}
				template.InstantiateIn(this._containerCell);
			}

			// Token: 0x06001EEA RID: 7914 RVA: 0x0004D7E4 File Offset: 0x0004B9E4
			private void InitTable()
			{
				this._table = new Table();
				if (!string.IsNullOrEmpty(this._owner.ID))
				{
					this._table.Attributes.Add("id", this._owner.ID);
				}
				this._table.CellSpacing = 0;
				this._table.CellPadding = this._owner.BorderPadding;
				this._containerCell = new TableCell();
				TableRow tableRow = new TableRow();
				tableRow.Cells.Add(this._containerCell);
				this._table.Rows.Add(tableRow);
				this.Controls.AddAt(0, this._table);
			}

			// Token: 0x06001EEB RID: 7915 RVA: 0x0004D897 File Offset: 0x0004BA97
			protected internal override void OnPreRender(EventArgs e)
			{
				if (this._table != null)
				{
					this._table.ApplyStyle(this._owner.ControlStyle);
				}
				base.OnPreRender(e);
			}

			// Token: 0x06001EEC RID: 7916 RVA: 0x0004D8BE File Offset: 0x0004BABE
			protected override void EnsureChildControls()
			{
				base.EnsureChildControls();
				if (this._owner != null)
				{
					this._owner.EnsureChildControls();
				}
			}

			// Token: 0x04001879 RID: 6265
			protected readonly ChangePassword _owner;

			// Token: 0x0400187A RID: 6266
			private bool renderOuterTable;

			// Token: 0x0400187B RID: 6267
			private Table _table;

			// Token: 0x0400187C RID: 6268
			private TableCell _containerCell;
		}

		// Token: 0x02000348 RID: 840
		private sealed class ChangePasswordContainer : ChangePassword.BaseChangePasswordContainer
		{
			// Token: 0x06001EED RID: 7917 RVA: 0x0004D8D9 File Offset: 0x0004BAD9
			public ChangePasswordContainer(ChangePassword owner)
				: base(owner)
			{
				this.ID = "ChangePasswordContainerID";
			}

			// Token: 0x170009A2 RID: 2466
			// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0004D8ED File Offset: 0x0004BAED
			public IEditableTextControl UserNameTextBox
			{
				get
				{
					Control control = this.FindControl("UserName");
					if (control == null)
					{
						throw new HttpException("ChangePasswordTemplate does not contain an IEditableTextControl with ID UserName for the username, this is required if DisplayUserName=true.");
					}
					return control as IEditableTextControl;
				}
			}

			// Token: 0x170009A3 RID: 2467
			// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0004D90D File Offset: 0x0004BB0D
			public IEditableTextControl CurrentPasswordTextBox
			{
				get
				{
					Control control = this.FindControl("CurrentPassword");
					if (control == null)
					{
						throw new HttpException("ChangePasswordTemplate does not contain an IEditableTextControl with ID CurrentPassword for the current password.");
					}
					return control as IEditableTextControl;
				}
			}

			// Token: 0x170009A4 RID: 2468
			// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0004D92D File Offset: 0x0004BB2D
			public IEditableTextControl NewPasswordTextBox
			{
				get
				{
					Control control = this.FindControl("NewPassword");
					if (control == null)
					{
						throw new HttpException("ChangePasswordTemplate does not contain an IEditableTextControl with ID NewPassword for the new password.");
					}
					return control as IEditableTextControl;
				}
			}

			// Token: 0x170009A5 RID: 2469
			// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x0004D94D File Offset: 0x0004BB4D
			public IEditableTextControl ConfirmNewPasswordTextBox
			{
				get
				{
					return this.FindControl("ConfirmNewPassword") as IEditableTextControl;
				}
			}

			// Token: 0x170009A6 RID: 2470
			// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0004D95F File Offset: 0x0004BB5F
			public Control CancelButton
			{
				get
				{
					return this.FindControl("Cancel");
				}
			}

			// Token: 0x170009A7 RID: 2471
			// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x0004D96C File Offset: 0x0004BB6C
			public Control ChangePasswordButton
			{
				get
				{
					return this.FindControl("ChangePassword");
				}
			}

			// Token: 0x170009A8 RID: 2472
			// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0004D979 File Offset: 0x0004BB79
			public ITextControl FailureTextLiteral
			{
				get
				{
					return this.FindControl("FailureText") as ITextControl;
				}
			}
		}

		// Token: 0x02000349 RID: 841
		private sealed class ChangePasswordDeafultTemplate : ITemplate
		{
			// Token: 0x06001EF5 RID: 7925 RVA: 0x0004D98B File Offset: 0x0004BB8B
			internal ChangePasswordDeafultTemplate(ChangePassword cPassword)
			{
				this._owner = cPassword;
			}

			// Token: 0x06001EF6 RID: 7926 RVA: 0x0004D99C File Offset: 0x0004BB9C
			private TableRow CreateRow(Control c0, Control c1, Control c2, Style s0, Style s1)
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				TableCell tableCell2 = new TableCell();
				tableCell.Controls.Add(c0);
				tableRow.Controls.Add(tableCell);
				if (c1 != null && c2 != null)
				{
					tableCell2.Controls.Add(c1);
					tableCell2.Controls.Add(c2);
					tableCell.HorizontalAlign = HorizontalAlign.Right;
					if (s0 != null)
					{
						tableCell.ApplyStyle(s0);
					}
					if (s1 != null)
					{
						tableCell2.ApplyStyle(s1);
					}
					tableRow.Controls.Add(tableCell2);
				}
				else
				{
					tableCell.ColumnSpan = 2;
					tableCell.HorizontalAlign = HorizontalAlign.Center;
					if (s0 != null)
					{
						tableCell.ApplyStyle(s0);
					}
				}
				return tableRow;
			}

			// Token: 0x06001EF7 RID: 7927 RVA: 0x0004DA3C File Offset: 0x0004BC3C
			private bool AddLink(string pageUrl, string linkText, string linkIcon, WebControl container)
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
					hyperLink.ControlStyle.CopyTextStylesFrom(container.ControlStyle);
					container.Controls.Add(hyperLink);
					flag = true;
				}
				return flag;
			}

			// Token: 0x06001EF8 RID: 7928 RVA: 0x0004DAB0 File Offset: 0x0004BCB0
			public void InstantiateIn(Control container)
			{
				Table table = new Table();
				table.CellPadding = 0;
				Style controlStyle = this._owner.ControlStyle;
				Style controlStyle2 = table.ControlStyle;
				FontInfo font = controlStyle.Font;
				controlStyle2.Font.CopyFrom(font);
				font.ClearDefaults();
				Color foreColor = controlStyle.ForeColor;
				if (foreColor != Color.Empty)
				{
					controlStyle2.ForeColor = foreColor;
					controlStyle.RemoveBit(4);
				}
				table.Controls.Add(this.CreateRow(new LiteralControl(this._owner.ChangePasswordTitleText), null, null, this._owner.TitleTextStyle, null));
				if (this._owner.InstructionText.Length > 0)
				{
					table.Controls.Add(this.CreateRow(new LiteralControl(this._owner.InstructionText), null, null, this._owner.InstructionTextStyle, null));
				}
				if (this._owner.DisplayUserName)
				{
					TextBox textBox = new TextBox();
					textBox.ID = "UserName";
					textBox.Text = this._owner.UserName;
					textBox.ApplyStyle(this._owner.TextBoxStyle);
					Label label = new Label();
					label.ID = "UserNameLabel";
					label.AssociatedControlID = "UserName";
					label.Text = this._owner.UserNameLabelText;
					RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
					requiredFieldValidator.ID = "UserNameRequired";
					requiredFieldValidator.ControlToValidate = "UserName";
					requiredFieldValidator.ErrorMessage = this._owner.UserNameRequiredErrorMessage;
					requiredFieldValidator.ToolTip = this._owner.UserNameRequiredErrorMessage;
					requiredFieldValidator.Text = "*";
					requiredFieldValidator.ValidationGroup = this._owner.ID;
					requiredFieldValidator.ApplyStyle(this._owner.ValidatorTextStyle);
					table.Controls.Add(this.CreateRow(label, textBox, requiredFieldValidator, this._owner.LabelStyle, null));
				}
				TextBox textBox2 = new TextBox();
				textBox2.ID = "CurrentPassword";
				textBox2.TextMode = TextBoxMode.Password;
				textBox2.ApplyStyle(this._owner.TextBoxStyle);
				Label label2 = new Label();
				label2.ID = "CurrentPasswordLabel";
				label2.AssociatedControlID = "CurrentPasswordLabel";
				label2.Text = this._owner.PasswordLabelText;
				RequiredFieldValidator requiredFieldValidator2 = new RequiredFieldValidator();
				requiredFieldValidator2.ID = "CurrentPasswordRequired";
				requiredFieldValidator2.ControlToValidate = "CurrentPassword";
				requiredFieldValidator2.ErrorMessage = this._owner.PasswordRequiredErrorMessage;
				requiredFieldValidator2.ToolTip = this._owner.PasswordRequiredErrorMessage;
				requiredFieldValidator2.Text = "*";
				requiredFieldValidator2.ValidationGroup = this._owner.ID;
				requiredFieldValidator2.ApplyStyle(this._owner.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label2, textBox2, requiredFieldValidator2, this._owner.LabelStyle, null));
				TextBox textBox3 = new TextBox();
				textBox3.ID = "NewPassword";
				textBox3.TextMode = TextBoxMode.Password;
				textBox3.ApplyStyle(this._owner.TextBoxStyle);
				Label label3 = new Label();
				label3.ID = "NewPasswordLabel";
				label3.AssociatedControlID = "NewPassword";
				label3.Text = this._owner.NewPasswordLabelText;
				RequiredFieldValidator requiredFieldValidator3 = new RequiredFieldValidator();
				requiredFieldValidator3.ID = "NewPasswordRequired";
				requiredFieldValidator3.ControlToValidate = "NewPassword";
				requiredFieldValidator3.ErrorMessage = this._owner.PasswordRequiredErrorMessage;
				requiredFieldValidator3.ToolTip = this._owner.PasswordRequiredErrorMessage;
				requiredFieldValidator3.Text = "*";
				requiredFieldValidator3.ValidationGroup = this._owner.ID;
				requiredFieldValidator3.ApplyStyle(this._owner.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label3, textBox3, requiredFieldValidator3, this._owner.LabelStyle, null));
				if (this._owner.PasswordHintText.Length > 0)
				{
					table.Controls.Add(this.CreateRow(new LiteralControl(string.Empty), new LiteralControl(this._owner.PasswordHintText), new LiteralControl(string.Empty), null, this._owner.PasswordHintStyle));
				}
				TextBox textBox4 = new TextBox();
				textBox4.ID = "ConfirmNewPassword";
				textBox4.TextMode = TextBoxMode.Password;
				textBox4.ApplyStyle(this._owner.TextBoxStyle);
				Label label4 = new Label();
				label4.ID = "ConfirmNewPasswordLabel";
				label4.AssociatedControlID = "ConfirmNewPasswordLabel";
				label4.Text = this._owner.ConfirmNewPasswordLabelText;
				RequiredFieldValidator requiredFieldValidator4 = new RequiredFieldValidator();
				requiredFieldValidator4.ID = "ConfirmNewPasswordRequired";
				requiredFieldValidator4.ControlToValidate = "ConfirmNewPassword";
				requiredFieldValidator4.ErrorMessage = this._owner.PasswordRequiredErrorMessage;
				requiredFieldValidator4.ToolTip = this._owner.PasswordRequiredErrorMessage;
				requiredFieldValidator4.Text = "*";
				requiredFieldValidator4.ValidationGroup = this._owner.ID;
				requiredFieldValidator4.ApplyStyle(this._owner.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label4, textBox4, requiredFieldValidator4, this._owner.LabelStyle, null));
				CompareValidator compareValidator = new CompareValidator();
				compareValidator.ID = "NewPasswordCompare";
				compareValidator.ControlToCompare = "NewPassword";
				compareValidator.ControlToValidate = "ConfirmNewPassword";
				compareValidator.Display = ValidatorDisplay.Dynamic;
				compareValidator.ErrorMessage = this._owner.ConfirmPasswordCompareErrorMessage;
				compareValidator.ValidationGroup = this._owner.ID;
				table.Controls.Add(this.CreateRow(compareValidator, null, null, null, null));
				Literal literal = new Literal();
				literal.ID = "FailureText";
				literal.EnableViewState = false;
				if (this._owner.FailureTextStyle.ForeColor.IsEmpty)
				{
					this._owner.FailureTextStyle.ForeColor = Color.Red;
				}
				table.Controls.Add(this.CreateRow(literal, null, null, this._owner.FailureTextStyle, null));
				WebControl webControl = null;
				switch (this._owner.ChangePasswordButtonType)
				{
				case ButtonType.Button:
					webControl = new Button();
					break;
				case ButtonType.Image:
					webControl = new ImageButton();
					break;
				case ButtonType.Link:
					webControl = new LinkButton();
					break;
				}
				webControl.ID = "ChangePasswordPushButton";
				webControl.ApplyStyle(this._owner.ChangePasswordButtonStyle);
				((IButtonControl)webControl).CommandName = ChangePassword.ChangePasswordButtonCommandName;
				((IButtonControl)webControl).Text = this._owner.ChangePasswordButtonText;
				((IButtonControl)webControl).ValidationGroup = this._owner.ID;
				WebControl webControl2 = null;
				switch (this._owner.CancelButtonType)
				{
				case ButtonType.Button:
					webControl2 = new Button();
					break;
				case ButtonType.Image:
					webControl2 = new ImageButton();
					break;
				case ButtonType.Link:
					webControl2 = new LinkButton();
					break;
				}
				webControl2.ID = "CancelPushButton";
				webControl2.ApplyStyle(this._owner.CancelButtonStyle);
				((IButtonControl)webControl2).CommandName = ChangePassword.CancelButtonCommandName;
				((IButtonControl)webControl2).Text = this._owner.CancelButtonText;
				((IButtonControl)webControl2).CausesValidation = false;
				table.Controls.Add(this.CreateRow(webControl, webControl2, new LiteralControl(string.Empty), null, null));
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.ControlStyle.CopyFrom(this._owner.HyperLinkStyle);
				tableRow.Cells.Add(tableCell);
				if (this.AddLink(this._owner.HelpPageUrl, this._owner.HelpPageText, this._owner.HelpPageIconUrl, tableCell))
				{
					tableCell.Controls.Add(new LiteralControl("<br/>"));
				}
				if (this.AddLink(this._owner.CreateUserUrl, this._owner.CreateUserText, this._owner.CreateUserIconUrl, tableCell))
				{
					tableCell.Controls.Add(new LiteralControl("<br/>"));
				}
				if (this.AddLink(this._owner.PasswordRecoveryUrl, this._owner.PasswordRecoveryText, this._owner.PasswordRecoveryIconUrl, tableCell))
				{
					tableCell.Controls.Add(new LiteralControl("<br/>"));
				}
				this.AddLink(this._owner.EditProfileUrl, this._owner.EditProfileText, this._owner.EditProfileIconUrl, tableCell);
				table.Controls.Add(tableRow);
				container.Controls.Add(table);
			}

			// Token: 0x0400187D RID: 6269
			private readonly ChangePassword _owner;
		}

		// Token: 0x0200034A RID: 842
		private sealed class SuccessDefaultTemplate : ITemplate
		{
			// Token: 0x06001EF9 RID: 7929 RVA: 0x0004E333 File Offset: 0x0004C533
			internal SuccessDefaultTemplate(ChangePassword cPassword)
			{
				this._cPassword = cPassword;
			}

			// Token: 0x06001EFA RID: 7930 RVA: 0x0004E344 File Offset: 0x0004C544
			private TableRow CreateRow(Control c0, Style s0, HorizontalAlign align)
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				tableCell.Controls.Add(c0);
				tableCell.HorizontalAlign = align;
				if (s0 != null)
				{
					tableCell.ApplyStyle(s0);
				}
				tableRow.Controls.Add(tableCell);
				return tableRow;
			}

			// Token: 0x06001EFB RID: 7931 RVA: 0x0004E388 File Offset: 0x0004C588
			public void InstantiateIn(Control container)
			{
				Table table = new Table();
				table.ControlStyle.Width = Unit.Percentage(100.0);
				table.ControlStyle.Height = Unit.Percentage(100.0);
				table.Controls.Add(this.CreateRow(new LiteralControl(this._cPassword.SuccessTitleText), this._cPassword.TitleTextStyle, HorizontalAlign.Center));
				table.Controls.Add(this.CreateRow(new LiteralControl(this._cPassword.SuccessText), this._cPassword.SuccessTextStyle, HorizontalAlign.Center));
				WebControl webControl = null;
				switch (this._cPassword.ChangePasswordButtonType)
				{
				case ButtonType.Button:
					webControl = new Button();
					break;
				case ButtonType.Image:
					webControl = new ImageButton();
					break;
				case ButtonType.Link:
					webControl = new LinkButton();
					break;
				}
				webControl.ID = "ContinuePushButton";
				webControl.ApplyStyle(this._cPassword.ContinueButtonStyle);
				((IButtonControl)webControl).CommandName = ChangePassword.ContinueButtonCommandName;
				((IButtonControl)webControl).Text = this._cPassword.ContinueButtonText;
				((IButtonControl)webControl).CausesValidation = false;
				table.Controls.Add(this.CreateRow(webControl, null, HorizontalAlign.Right));
				container.Controls.Add(table);
			}

			// Token: 0x0400187E RID: 6270
			private readonly ChangePassword _cPassword;
		}

		// Token: 0x0200034B RID: 843
		private sealed class SuccessContainer : ChangePassword.BaseChangePasswordContainer
		{
			// Token: 0x06001EFC RID: 7932 RVA: 0x0004E4CD File Offset: 0x0004C6CD
			public SuccessContainer(ChangePassword owner)
				: base(owner)
			{
				this.ID = "SuccessContainerID";
			}

			// Token: 0x170009A9 RID: 2473
			// (get) Token: 0x06001EFD RID: 7933 RVA: 0x0004E4E1 File Offset: 0x0004C6E1
			public Control ChangePasswordButton
			{
				get
				{
					return this.FindControl("Continue");
				}
			}
		}
	}
}

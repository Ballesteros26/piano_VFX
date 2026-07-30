using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Mail;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a user interface for creating new Web site user accounts.</summary>
	// Token: 0x02000361 RID: 865
	[Designer("System.Web.UI.Design.WebControls.CreateUserWizardDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Bindable(false)]
	[ToolboxData("   ")]
	[DefaultEvent("CreatedUser")]
	public class CreateUserWizard : Wizard
	{
		/// <summary>Gets or sets the step that is currently displayed to the user.</summary>
		/// <returns>The index of the step that is currently displayed to the user.</returns>
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x00050979 File Offset: 0x0004EB79
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00050981 File Offset: 0x0004EB81
		[DefaultValue(0)]
		public override int ActiveStepIndex
		{
			get
			{
				return base.ActiveStepIndex;
			}
			set
			{
				base.ActiveStepIndex = value;
			}
		}

		/// <summary>Gets or sets the end user's answer to the password recovery confirmation question.</summary>
		/// <returns>The end user's answer to the password recovery confirmation question. The default value is an empty string ("").</returns>
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0005098C File Offset: 0x0004EB8C
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x000509B9 File Offset: 0x0004EBB9
		[Themeable(false)]
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string Answer
		{
			get
			{
				object obj = this.ViewState["Answer"];
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
					this.ViewState.Remove("Answer");
					return;
				}
				this.ViewState["Answer"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label that identifies the password confirmation answer text box.</summary>
		/// <returns>The label text that identifies the password confirmation answer text box. The default value is "Security Answer:". The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x000509E0 File Offset: 0x0004EBE0
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x00050A12 File Offset: 0x0004EC12
		[Localizable(true)]
		public virtual string AnswerLabelText
		{
			get
			{
				object obj = this.ViewState["AnswerLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Security Answer:");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("AnswerLabelText");
					return;
				}
				this.ViewState["AnswerLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the error message shown when the user does not enter an answer to the password confirmation question.</summary>
		/// <returns>The error message shown when the user does not enter an answer to the password confirmation question. The default value is "Security answer is required." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x00050A3C File Offset: 0x0004EC3C
		// (set) Token: 0x06002004 RID: 8196 RVA: 0x00050A6E File Offset: 0x0004EC6E
		[Localizable(true)]
		public virtual string AnswerRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["AnswerRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Security answer is required.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("AnswerRequiredErrorMessage");
					return;
				}
				this.ViewState["AnswerRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether or not to automatically generate a password for the new user account.</summary>
		/// <returns>true to automatically generate a password for the new user account; otherwise, false. The default value is false.This property cannot be set by themes or style sheet themes. For more information, see <see cref="T:System.Web.UI.ThemeableAttribute" /> and ASP.NET Themes and Skins.</returns>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x00050A98 File Offset: 0x0004EC98
		// (set) Token: 0x06002006 RID: 8198 RVA: 0x00050AC1 File Offset: 0x0004ECC1
		[DefaultValue(false)]
		[Themeable(false)]
		public virtual bool AutoGeneratePassword
		{
			get
			{
				object obj = this.ViewState["AutoGeneratePassword"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoGeneratePassword"] = value;
			}
		}

		/// <summary>Gets a reference to the final user account creation step.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.CompleteWizardStep" /> object that represents the final user account creation step.</returns>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x00050ADC File Offset: 0x0004ECDC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CompleteWizardStep CompleteStep
		{
			get
			{
				if (this._completeWizardStep == null)
				{
					for (int i = 0; i < this.WizardSteps.Count; i++)
					{
						if (this.WizardSteps[i] is CompleteWizardStep)
						{
							this._completeWizardStep = (CompleteWizardStep)this.WizardSteps[i];
							if (this._completeWizardStep.Wizard == null)
							{
								this._completeWizardStep.SetWizard(this);
							}
						}
					}
				}
				return this._completeWizardStep;
			}
		}

		/// <summary>Gets or sets the text displayed when a Web site user account is created successfully.</summary>
		/// <returns>The text displayed when a Web site user account is created successfully. The default is "Your account has been successfully created." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x00050B50 File Offset: 0x0004ED50
		// (set) Token: 0x06002009 RID: 8201 RVA: 0x00050B82 File Offset: 0x0004ED82
		[Localizable(true)]
		public virtual string CompleteSuccessText
		{
			get
			{
				object obj = this.ViewState["CompleteSuccessText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Your account has been successfully created.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("CompleteSuccessText");
					return;
				}
				this.ViewState["CompleteSuccessText"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of the text displayed when a Web site user account is created successfully. </summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of the text displayed when a Web site user account is created successfully.</returns>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x00050BA9 File Offset: 0x0004EDA9
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle CompleteSuccessTextStyle
		{
			get
			{
				if (this._completeSuccessTextStyle == null)
				{
					this._completeSuccessTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._completeSuccessTextStyle).TrackViewState();
					}
				}
				return this._completeSuccessTextStyle;
			}
		}

		/// <summary>Gets the second password entered by the user.</summary>
		/// <returns>The second password entered by the user. The default value is an empty string ("").</returns>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x00050BD7 File Offset: 0x0004EDD7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string ConfirmPassword
		{
			get
			{
				return this._confirmPassword;
			}
		}

		/// <summary>Gets or sets the error message shown when the user enters two different passwords in the password and confirm password text boxes.</summary>
		/// <returns>The error message shown when the user enters two different passwords in the password and confirm password text boxes. The default value is "The Password and Confirmation Password must match." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x00050BE0 File Offset: 0x0004EDE0
		// (set) Token: 0x0600200D RID: 8205 RVA: 0x00050C12 File Offset: 0x0004EE12
		[Localizable(true)]
		public virtual string ConfirmPasswordCompareErrorMessage
		{
			get
			{
				object obj = this.ViewState["ConfirmPasswordCompareErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("The Password and Confirmation Password must match.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("ConfirmPasswordCompareErrorMessage");
					return;
				}
				this.ViewState["ConfirmPasswordCompareErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets text of the label for the second password text box.</summary>
		/// <returns>The label text that identifies the confirm password text box. The default value is "Confirm Password:". The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x00050C3C File Offset: 0x0004EE3C
		// (set) Token: 0x0600200F RID: 8207 RVA: 0x00050C6E File Offset: 0x0004EE6E
		[Localizable(true)]
		public virtual string ConfirmPasswordLabelText
		{
			get
			{
				object obj = this.ViewState["ConfirmPasswordLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Confirm Password:");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("ConfirmPasswordLabelText");
					return;
				}
				this.ViewState["ConfirmPasswordLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the error message displayed when the user leaves the confirm password text box empty.</summary>
		/// <returns>The error message displayed when the user leaves the confirm password text box empty. The default value is "Confirm Password is required." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x00050C98 File Offset: 0x0004EE98
		// (set) Token: 0x06002011 RID: 8209 RVA: 0x00050CCA File Offset: 0x0004EECA
		[Localizable(true)]
		public virtual string ConfirmPasswordRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["ConfirmPasswordRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Confirm Password is required.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("ConfirmPasswordRequiredErrorMessage");
					return;
				}
				this.ViewState["ConfirmPasswordRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image used for the Continue button on the final user account creation step.</summary>
		/// <returns>The URL of an image used for the Continue button on the final user account creation step. The default value is an empty string ("").</returns>
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x0004C4CF File Offset: 0x0004A6CF
		// (set) Token: 0x06002013 RID: 8211 RVA: 0x0004C4E6 File Offset: 0x0004A6E6
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

		/// <summary>Gets a reference to a collection of properties that define the appearance of the Continue button.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.Style" /> that contains the properties that define the appearance of the Continue button.</returns>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x00050CF1 File Offset: 0x0004EEF1
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public Style ContinueButtonStyle
		{
			get
			{
				if (this._continueButtonStyle == null)
				{
					this._continueButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._continueButtonStyle).TrackViewState();
					}
				}
				return this._continueButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption displayed on the Continue button.</summary>
		/// <returns>The text caption displayed on the Continue button. The default value is "Continue". The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x0004C527 File Offset: 0x0004A727
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x0004C53E File Offset: 0x0004A73E
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

		/// <summary>Gets or sets the type of button rendered as the Continue button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values. The default value is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified button type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x00050D20 File Offset: 0x0004EF20
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x0004C57C File Offset: 0x0004A77C
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType ContinueButtonType
		{
			get
			{
				object obj = this.ViewState["ContinueButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["ContinueButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the page that the user will see after clicking the Continue button on the success page.</summary>
		/// <returns>The URL of the destination page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x00050D4C File Offset: 0x0004EF4C
		// (set) Token: 0x0600201A RID: 8218 RVA: 0x00050D79 File Offset: 0x0004EF79
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[Themeable(false)]
		public virtual string ContinueDestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["ContinueDestinationPageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("ContinueDestinationPageUrl");
					return;
				}
				this.ViewState["ContinueDestinationPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image displayed for the Create User button.</summary>
		/// <returns>The URL of the image displayed for the Create User button. The default value is an empty string ("").</returns>
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x00050DA0 File Offset: 0x0004EFA0
		// (set) Token: 0x0600201C RID: 8220 RVA: 0x00050DB7 File Offset: 0x0004EFB7
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string CreateUserButtonImageUrl
		{
			get
			{
				return this.ViewState.GetString("CreateUserButtonImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["CreateUserButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of the Create User button.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.Style" /> that contains the properties that define the appearance of the Create User button.</returns>
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x00050DCA File Offset: 0x0004EFCA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style CreateUserButtonStyle
		{
			get
			{
				if (this._createUserButtonStyle == null)
				{
					this._createUserButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._createUserButtonStyle).TrackViewState();
					}
				}
				return this._createUserButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption displayed on the Create User button.</summary>
		/// <returns>The text caption displayed on the Create User button for the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control. The default value is "Submit". The default text for the control is localized based on the server's current locale.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified button type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x00050DF8 File Offset: 0x0004EFF8
		// (set) Token: 0x0600201F RID: 8223 RVA: 0x00050E0F File Offset: 0x0004F00F
		[Localizable(true)]
		public virtual string CreateUserButtonText
		{
			get
			{
				return this.ViewState.GetString("CreateUserButtonText", "Create User");
			}
			set
			{
				this.ViewState["CreateUserButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button rendered as the Create User button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> enumeration values. The default value is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified button type is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values.</exception>
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x00050E24 File Offset: 0x0004F024
		// (set) Token: 0x06002021 RID: 8225 RVA: 0x00050E4D File Offset: 0x0004F04D
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType CreateUserButtonType
		{
			get
			{
				object obj = this.ViewState["CreateUserButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["CreateUserButtonType"] = value;
			}
		}

		/// <summary>Gets a reference to the template for the user account creation step.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.CreateUserWizardStep" /> value that represents the user account creation step.</returns>
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x00050E68 File Offset: 0x0004F068
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CreateUserWizardStep CreateUserStep
		{
			get
			{
				if (this._createUserWizardStep == null)
				{
					for (int i = 0; i < this.WizardSteps.Count; i++)
					{
						if (this.WizardSteps[i] is CreateUserWizardStep)
						{
							this._createUserWizardStep = (CreateUserWizardStep)this.WizardSteps[i];
							if (this._createUserWizardStep.Wizard == null)
							{
								this._createUserWizardStep.SetWizard(this);
							}
						}
					}
				}
				return this._createUserWizardStep;
			}
		}

		/// <summary>Gets or sets a value indicating whether the new user should be allowed to log on to the Web site.</summary>
		/// <returns>true if the new user is allowed to log on to the Web site; otherwise, false. The default is false.</returns>
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x00050EDC File Offset: 0x0004F0DC
		// (set) Token: 0x06002024 RID: 8228 RVA: 0x00050F05 File Offset: 0x0004F105
		[DefaultValue(false)]
		[Themeable(false)]
		public virtual bool DisableCreatedUser
		{
			get
			{
				object obj = this.ViewState["DisableCreatedUser"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DisableCreatedUser"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to display the sidebar area of the control.</summary>
		/// <returns>true if the sidebar area should be displayed for the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control; otherwise, false. The default value is false.</returns>
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x00050F1D File Offset: 0x0004F11D
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x00050F30 File Offset: 0x0004F130
		[DefaultValue(false)]
		public override bool DisplaySideBar
		{
			get
			{
				return this.ViewState.GetBool("DisplaySideBar", false);
			}
			set
			{
				this.ViewState["DisplaySideBar"] = value;
				base.ChildControlsCreated = false;
			}
		}

		/// <summary>Gets or sets the error message displayed when the user enters an e-mail address that is already in use in the membership provider.</summary>
		/// <returns>The error message displayed when the user enters an e-mail address that is already in use in the membership provider. The default value is "The e-mail address that you entered is already in use. Please enter a different e-mail address." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x00050F50 File Offset: 0x0004F150
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x00050F82 File Offset: 0x0004F182
		[Localizable(true)]
		public virtual string DuplicateEmailErrorMessage
		{
			get
			{
				object obj = this.ViewState["DuplicateEmailErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("The e-mail address that you entered is already in use. Please enter a different e-mail address.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("DuplicateEmailErrorMessage");
					return;
				}
				this.ViewState["DuplicateEmailErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the error message displayed when the user enters a user name that is already in use in the membership provider.</summary>
		/// <returns>The error message displayed when the user enters a user name that is already in the membership provider. The default value is "Please enter a different user name." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x00050FAC File Offset: 0x0004F1AC
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x00050FDE File Offset: 0x0004F1DE
		[Localizable(true)]
		public virtual string DuplicateUserNameErrorMessage
		{
			get
			{
				object obj = this.ViewState["DuplicateUserNameErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Please enter a different user name.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("DuplicateUserNameErrorMessage");
					return;
				}
				this.ViewState["DuplicateUserNameErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to display next to the link to the user profile editing page.</summary>
		/// <returns>The URL of an image to display next to the link to the user profile editing page. The default value is an empty string ("").</returns>
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x00051008 File Offset: 0x0004F208
		// (set) Token: 0x0600202C RID: 8236 RVA: 0x00051035 File Offset: 0x0004F235
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string EditProfileIconUrl
		{
			get
			{
				object obj = this.ViewState["EditProfileIconUrl"];
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
					this.ViewState.Remove("EditProfileIconUrl");
					return;
				}
				this.ViewState["EditProfileIconUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text caption for the link to the user profile editing page.</summary>
		/// <returns>The text caption for the link to the user profile editing page. The default value is an empty string ("").</returns>
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x0005105C File Offset: 0x0004F25C
		// (set) Token: 0x0600202E RID: 8238 RVA: 0x00051089 File Offset: 0x0004F289
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string EditProfileText
		{
			get
			{
				object obj = this.ViewState["EditProfileText"];
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
					this.ViewState.Remove("EditProfileText");
					return;
				}
				this.ViewState["EditProfileText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the user profile editing page.</summary>
		/// <returns>The URL of the user profile editing page. The default value is an empty string ("").</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x000510B0 File Offset: 0x0004F2B0
		// (set) Token: 0x06002030 RID: 8240 RVA: 0x000510DD File Offset: 0x0004F2DD
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string EditProfileUrl
		{
			get
			{
				object obj = this.ViewState["EditProfileUrl"];
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
					this.ViewState.Remove("EditProfileUrl");
					return;
				}
				this.ViewState["EditProfileUrl"] = value;
			}
		}

		/// <summary>Gets or sets the e-mail address entered by the user.</summary>
		/// <returns>The e-mail address entered by the user. The default value is an empty string ("").</returns>
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x00051104 File Offset: 0x0004F304
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x00051131 File Offset: 0x0004F331
		[DefaultValue("")]
		public virtual string Email
		{
			get
			{
				object obj = this.ViewState["Email"];
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
					this.ViewState.Remove("Email");
					return;
				}
				this.ViewState["Email"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label for the e-mail text box.</summary>
		/// <returns>The label text that identifies the e-mail text box. The default value is "E-mail:". The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x00051158 File Offset: 0x0004F358
		// (set) Token: 0x06002034 RID: 8244 RVA: 0x0005118A File Offset: 0x0004F38A
		[Localizable(true)]
		public virtual string EmailLabelText
		{
			get
			{
				object obj = this.ViewState["EmailLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("E-mail:");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("EmailLabelText");
					return;
				}
				this.ViewState["EmailLabelText"] = value;
			}
		}

		/// <summary>Gets or sets a regular expression used to validate the provided e-mail address.</summary>
		/// <returns>A string containing the regular expression used to validate an e-mail address. The default value is an empty string ("").</returns>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x000511B4 File Offset: 0x0004F3B4
		// (set) Token: 0x06002036 RID: 8246 RVA: 0x000511E1 File Offset: 0x0004F3E1
		public virtual string EmailRegularExpression
		{
			get
			{
				object obj = this.ViewState["EmailRegularExpression"];
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
					this.ViewState.Remove("EmailRegularExpression");
					return;
				}
				this.ViewState["EmailRegularExpression"] = value;
			}
		}

		/// <summary>Gets or sets the error message displayed when the entered e-mail address does not pass the site's criteria for e-mail addresses.</summary>
		/// <returns>The error message displayed when the entered e-mail address does not pass the regular expression defined in the <see cref="P:System.Web.UI.WebControls.CreateUserWizard.EmailRegularExpression" /> property. The default is "Please enter a different e-mail address." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x00051208 File Offset: 0x0004F408
		// (set) Token: 0x06002038 RID: 8248 RVA: 0x0005123A File Offset: 0x0004F43A
		public virtual string EmailRegularExpressionErrorMessage
		{
			get
			{
				object obj = this.ViewState["EmailRegularExpressionErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Please enter a different e-mail.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("EmailRegularExpressionErrorMessage");
					return;
				}
				this.ViewState["EmailRegularExpressionErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the error message shown to the user when an e-mail address is not entered in the e-mail text box.</summary>
		/// <returns>The error message shown to the user when an e-mail address is not entered in the e-mail text box. The default value is "E-mail is required." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x00051264 File Offset: 0x0004F464
		// (set) Token: 0x0600203A RID: 8250 RVA: 0x00051296 File Offset: 0x0004F496
		[Localizable(true)]
		public virtual string EmailRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["EmailRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("E-mail is required.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("EmailRequiredErrorMessage");
					return;
				}
				this.ViewState["EmailRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of style properties that define the appearance of error messages.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> containing the style properties that define the appearance of error messages on the control. The default is null.</returns>
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x000512BD File Offset: 0x0004F4BD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public TableItemStyle ErrorMessageStyle
		{
			get
			{
				if (this._errorMessageStyle == null)
				{
					this._errorMessageStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._errorMessageStyle).TrackViewState();
					}
				}
				return this._errorMessageStyle;
			}
		}

		/// <summary>Gets or sets the URL of an image to display next to the link to the Help page.</summary>
		/// <returns>The URL of an image to display next to the link to the Help page. The default value is an empty string ("").</returns>
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x000512EC File Offset: 0x0004F4EC
		// (set) Token: 0x0600203D RID: 8253 RVA: 0x00051319 File Offset: 0x0004F519
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the text caption for the link to the Help page.</summary>
		/// <returns>The text caption for the link to the Help page. The default value is an empty string ("").</returns>
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x00051340 File Offset: 0x0004F540
		// (set) Token: 0x0600203F RID: 8255 RVA: 0x0005136D File Offset: 0x0004F56D
		[Localizable(true)]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the URL of the Help page.</summary>
		/// <returns>The URL of the Help page. The default value is an empty string ("").</returns>
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x00051394 File Offset: 0x0004F594
		// (set) Token: 0x06002041 RID: 8257 RVA: 0x000513C1 File Offset: 0x0004F5C1
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		/// <summary>Gets or sets a collection of properties that define the appearance of hyperlinks.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of hyperlinks.</returns>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x000513E8 File Offset: 0x0004F5E8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TableItemStyle HyperLinkStyle
		{
			get
			{
				if (this._hyperLinkStyle == null)
				{
					this._hyperLinkStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._hyperLinkStyle).TrackViewState();
					}
				}
				return this._hyperLinkStyle;
			}
		}

		/// <summary>Gets or sets instructions for creating a new user account.</summary>
		/// <returns>The instruction text for creating a new user account. The default value is an empty string ("").</returns>
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00051418 File Offset: 0x0004F618
		// (set) Token: 0x06002044 RID: 8260 RVA: 0x00051445 File Offset: 0x0004F645
		[Localizable(true)]
		[DefaultValue("")]
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

		/// <summary>Gets a reference to a collection of properties that define the appearance of instruction text.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of instruction text.</returns>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x0005146C File Offset: 0x0004F66C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle InstructionTextStyle
		{
			get
			{
				if (this._instructionTextStyle == null)
				{
					this._instructionTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._instructionTextStyle).TrackViewState();
					}
				}
				return this._instructionTextStyle;
			}
		}

		/// <summary>Gets or sets the message displayed when the password retrieval answer is not valid.</summary>
		/// <returns>The message displayed when the password retrieval answer is not valid. The default is "Please enter a different security answer." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x0005149C File Offset: 0x0004F69C
		// (set) Token: 0x06002047 RID: 8263 RVA: 0x000514CE File Offset: 0x0004F6CE
		[Localizable(true)]
		public virtual string InvalidAnswerErrorMessage
		{
			get
			{
				object obj = this.ViewState["InvalidAnswerErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Please enter a different security answer.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("InvalidAnswerErrorMessage");
					return;
				}
				this.ViewState["InvalidAnswerErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the message displayed when the entered e-mail address is not valid.</summary>
		/// <returns>The message displayed when the e-mail address entered is not valid. The default is "Please enter a valid e-mail address." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x000514F8 File Offset: 0x0004F6F8
		// (set) Token: 0x06002049 RID: 8265 RVA: 0x0005152A File Offset: 0x0004F72A
		[Localizable(true)]
		public virtual string InvalidEmailErrorMessage
		{
			get
			{
				object obj = this.ViewState["InvalidEmailErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Please enter a valid e-mail address.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("InvalidEmailErrorMessage");
					return;
				}
				this.ViewState["InvalidEmailErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the message displayed when the password entered is not valid.</summary>
		/// <returns>The message displayed when the password entered is not valid. The default is "Please enter a valid password." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x00051554 File Offset: 0x0004F754
		// (set) Token: 0x0600204B RID: 8267 RVA: 0x00051586 File Offset: 0x0004F786
		[global::System.MonoTODO("take the values from membership provider")]
		[Localizable(true)]
		public virtual string InvalidPasswordErrorMessage
		{
			get
			{
				object obj = this.ViewState["InvalidPasswordErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Password length minimum: {0}. Non-alphanumeric characters required: {1}.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("InvalidPasswordErrorMessage");
					return;
				}
				this.ViewState["InvalidPasswordErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the message displayed when the password retrieval question entered is not valid.</summary>
		/// <returns>The message displayed when the password retrieval question is not valid. The default is "Please enter a valid answer." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x000515B0 File Offset: 0x0004F7B0
		// (set) Token: 0x0600204D RID: 8269 RVA: 0x000515E2 File Offset: 0x0004F7E2
		[Localizable(true)]
		public virtual string InvalidQuestionErrorMessage
		{
			get
			{
				object obj = this.ViewState["InvalidQuestionErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Please enter a different security question.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("InvalidQuestionErrorMessage");
					return;
				}
				this.ViewState["InvalidQuestionErrorMessage"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of labels.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of labels.</returns>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x00051609 File Offset: 0x0004F809
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle LabelStyle
		{
			get
			{
				if (this._labelStyle == null)
				{
					this._labelStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._labelStyle).TrackViewState();
					}
				}
				return this._labelStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether to log in the new user after creating the user account.</summary>
		/// <returns>true if the new user should be logged in after creating the user account; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x00051638 File Offset: 0x0004F838
		// (set) Token: 0x06002050 RID: 8272 RVA: 0x00051661 File Offset: 0x0004F861
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool LoginCreatedUser
		{
			get
			{
				object obj = this.ViewState["LoginCreatedUser"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["LoginCreatedUser"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the characteristics of the e-mail message sent to new users.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object that defines the e-mail message sent to a new user.</returns>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.UI.WebControls.MailDefinition.From" /> is not set to an e-mail address.</exception>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x00051679 File Offset: 0x0004F879
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Themeable(false)]
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

		/// <summary>Gets or sets the membership provider called to create user accounts.</summary>
		/// <returns>The <see cref="T:System.Web.Security.MembershipProvider" /> used to create user accounts. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.Web.HttpException">The specified membership provider is not defined in the Web.config file.</exception>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x000516A8 File Offset: 0x0004F8A8
		// (set) Token: 0x06002053 RID: 8275 RVA: 0x000516D5 File Offset: 0x0004F8D5
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

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x00051704 File Offset: 0x0004F904
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

		/// <summary>Gets the password entered by the user.</summary>
		/// <returns>The password entered by the user. The default value is an empty string ("").</returns>
		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x0005171A File Offset: 0x0004F91A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string Password
		{
			get
			{
				return this._password;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of the text that describes password requirements.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of the text that describes password requirements.</returns>
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x00051722 File Offset: 0x0004F922
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle PasswordHintStyle
		{
			get
			{
				if (this._passwordHintStyle == null)
				{
					this._passwordHintStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._passwordHintStyle).TrackViewState();
					}
				}
				return this._passwordHintStyle;
			}
		}

		/// <summary>Gets or sets the text that describes password requirements.</summary>
		/// <returns>The text that describes password requirements. The default value is an empty string ("").</returns>
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00051750 File Offset: 0x0004F950
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x0005177D File Offset: 0x0004F97D
		[Localizable(true)]
		public virtual string PasswordHintText
		{
			get
			{
				object obj = this.ViewState["PasswordHintText"];
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
					this.ViewState.Remove("PasswordHintText");
					return;
				}
				this.ViewState["PasswordHintText"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label for the password text box.</summary>
		/// <returns>The text of the label for the password text box. The default value is "Password:". The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x000517A4 File Offset: 0x0004F9A4
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x000517D6 File Offset: 0x0004F9D6
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
				return global::Locale.GetText("Password:");
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

		/// <summary>Gets or sets a regular expression used to validate the provided password.</summary>
		/// <returns>A string containing the regular expression used to validate the provided password. The default value is an empty string ("").</returns>
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00051800 File Offset: 0x0004FA00
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x0005182D File Offset: 0x0004FA2D
		public virtual string PasswordRegularExpression
		{
			get
			{
				object obj = this.ViewState["PasswordRegularExpression"];
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
					this.ViewState.Remove("PasswordRegularExpression");
					return;
				}
				this.ViewState["PasswordRegularExpression"] = value;
			}
		}

		/// <summary>Gets or sets the error message shown when the password entered does not conform to the site's password requirements.</summary>
		/// <returns>The error message shown when the password entered does not pass the regular expression defined in the <see cref="P:System.Web.UI.WebControls.CreateUserWizard.PasswordRegularExpression" /> property. The default is "Please enter a different password." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00051854 File Offset: 0x0004FA54
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x00051886 File Offset: 0x0004FA86
		public virtual string PasswordRegularExpressionErrorMessage
		{
			get
			{
				object obj = this.ViewState["PasswordRegularExpressionErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Please enter a different password.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("PasswordRegularExpressionErrorMessage");
					return;
				}
				this.ViewState["PasswordRegularExpressionErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the text of the error message shown when the user does not enter a password.</summary>
		/// <returns>The error message shown when the user does not enter a password. The default value is "Password is required." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x000518B0 File Offset: 0x0004FAB0
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x000518E2 File Offset: 0x0004FAE2
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

		/// <summary>Gets or sets the password recovery confirmation question entered by the user.</summary>
		/// <returns>The password recovery confirmation question entered by the user. The default value is an empty string ("").</returns>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x0005190C File Offset: 0x0004FB0C
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x00051939 File Offset: 0x0004FB39
		[Themeable(false)]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Question
		{
			get
			{
				object obj = this.ViewState["Question"];
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
					this.ViewState.Remove("Question");
					return;
				}
				this.ViewState["Question"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label for the question text box.</summary>
		/// <returns>The text of the label for the question text box. The default value is "Security Question:". The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x00051960 File Offset: 0x0004FB60
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x00051992 File Offset: 0x0004FB92
		[Localizable(true)]
		public virtual string QuestionLabelText
		{
			get
			{
				object obj = this.ViewState["QuestionLabelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Security Question:");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("QuestionLabelText");
					return;
				}
				this.ViewState["QuestionLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the error message that is displayed when the user does not enter a password confirmation question.</summary>
		/// <returns>The error message that is displayed when the user does not enter a password confirmation question. The default value is "Security question is required." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x000519BC File Offset: 0x0004FBBC
		// (set) Token: 0x06002066 RID: 8294 RVA: 0x000519EE File Offset: 0x0004FBEE
		[Localizable(true)]
		public virtual string QuestionRequiredErrorMessage
		{
			get
			{
				object obj = this.ViewState["QuestionRequiredErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Security question is required.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("QuestionRequiredErrorMessage");
					return;
				}
				this.ViewState["QuestionRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether an e-mail address is required for the Web site user.</summary>
		/// <returns>true if an e-mail address is required; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x00051A18 File Offset: 0x0004FC18
		// (set) Token: 0x06002068 RID: 8296 RVA: 0x00051A41 File Offset: 0x0004FC41
		[Themeable(false)]
		[DefaultValue(true)]
		public virtual bool RequireEmail
		{
			get
			{
				object obj = this.ViewState["RequireEmail"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["RequireEmail"] = value;
			}
		}

		/// <summary>Gets or sets a value that is used to render alternate text that notifies screen readers to skip the sidebar area's content.</summary>
		/// <returns>A string that the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> renders as alternate text with an invisible image, as a hint to screen readers. The default is an empty string ("").</returns>
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x00051A5C File Offset: 0x0004FC5C
		// (set) Token: 0x0600206A RID: 8298 RVA: 0x00051A89 File Offset: 0x0004FC89
		[global::System.MonoTODO("doesnt work")]
		[DefaultValue("")]
		public override string SkipLinkText
		{
			get
			{
				object obj = this.ViewState["SkipLinkText"];
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
					this.ViewState.Remove("SkipLinkText");
					return;
				}
				this.ViewState["SkipLinkText"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of text box controls.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that contains properties that define the appearance of text box controls.</returns>
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x00051AB0 File Offset: 0x0004FCB0
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style TextBoxStyle
		{
			get
			{
				if (this._textBoxStyle == null)
				{
					this._textBoxStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._textBoxStyle).TrackViewState();
					}
				}
				return this._textBoxStyle;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of titles.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of titles.</returns>
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x00051ADE File Offset: 0x0004FCDE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public TableItemStyle TitleTextStyle
		{
			get
			{
				if (this._titleTextStyle == null)
				{
					this._titleTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._titleTextStyle).TrackViewState();
					}
				}
				return this._titleTextStyle;
			}
		}

		/// <summary>Gets or sets the error message displayed when an error returned by the membership provider is not defined.</summary>
		/// <returns>The error message displayed when an error returned by the membership provider is not defined. The default value is "Your account was not created. Please try again." The default text of the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x00051B0C File Offset: 0x0004FD0C
		// (set) Token: 0x0600206E RID: 8302 RVA: 0x00051B3E File Offset: 0x0004FD3E
		[Localizable(true)]
		public virtual string UnknownErrorMessage
		{
			get
			{
				object obj = this.ViewState["UnknownErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Your account was not created. Please try again.");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("UnknownErrorMessage");
					return;
				}
				this.ViewState["UnknownErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the user name entered by the user.</summary>
		/// <returns>The user name entered by the user. The default value is an empty string ("").</returns>
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x00051B68 File Offset: 0x0004FD68
		// (set) Token: 0x06002070 RID: 8304 RVA: 0x00051B95 File Offset: 0x0004FD95
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

		/// <summary>Gets or sets the text of the label for the user name text box.</summary>
		/// <returns>The text of the label for the user name text box. The default value is "User Name:". The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x00051BBC File Offset: 0x0004FDBC
		// (set) Token: 0x06002072 RID: 8306 RVA: 0x00051BEE File Offset: 0x0004FDEE
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

		/// <summary>Gets or sets the error message displayed when the user name text box is left blank.</summary>
		/// <returns>The error message displayed when the user name text box is left blank. The default value is "User Name is required." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x00051C18 File Offset: 0x0004FE18
		// (set) Token: 0x06002074 RID: 8308 RVA: 0x00051C4A File Offset: 0x0004FE4A
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

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that allows you to set the appearance of the validation error messages.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> containing the style properties that define the appearance of validation error messages on the control. The default is null.</returns>
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x00051C71 File Offset: 0x0004FE71
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style ValidatorTextStyle
		{
			get
			{
				if (this._validatorTextStyle == null)
				{
					this._validatorTextStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._validatorTextStyle).TrackViewState();
					}
				}
				return this._validatorTextStyle;
			}
		}

		/// <summary>Gets a reference to a collection containing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects defined for the control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> representing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects defined for the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</returns>
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x00051C9F File Offset: 0x0004FE9F
		[Editor("System.Web.UI.Design.WebControls.CreateUserWizardStepCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override WizardStepCollection WizardSteps
		{
			get
			{
				return base.WizardSteps;
			}
		}

		/// <summary>Gets a value indicating whether the user is required to enter a password confirmation question and answer.</summary>
		/// <returns>true if the user is required to enter a password confirmation question and answer; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x00051CA7 File Offset: 0x0004FEA7
		[DefaultValue(true)]
		protected internal bool QuestionAndAnswerRequired
		{
			get
			{
				return this.MembershipProviderInternal.RequiresQuestionAndAnswer;
			}
		}

		/// <summary>Occurs when the user clicks the Continue button in the final user account creation step.</summary>
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06002078 RID: 8312 RVA: 0x00051CB4 File Offset: 0x0004FEB4
		// (remove) Token: 0x06002079 RID: 8313 RVA: 0x00051CC7 File Offset: 0x0004FEC7
		public event EventHandler ContinueButtonClick
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.ContinueButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.ContinueButtonClickEvent, value);
			}
		}

		/// <summary>Occurs after the membership provider has created the new Web site user account.</summary>
		// Token: 0x1400005E RID: 94
		// (add) Token: 0x0600207A RID: 8314 RVA: 0x00051CDA File Offset: 0x0004FEDA
		// (remove) Token: 0x0600207B RID: 8315 RVA: 0x00051CED File Offset: 0x0004FEED
		public event EventHandler CreatedUser
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.CreatedUserEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.CreatedUserEvent, value);
			}
		}

		/// <summary>Occurs when the membership provider cannot create the specified user account.</summary>
		// Token: 0x1400005F RID: 95
		// (add) Token: 0x0600207C RID: 8316 RVA: 0x00051D00 File Offset: 0x0004FF00
		// (remove) Token: 0x0600207D RID: 8317 RVA: 0x00051D13 File Offset: 0x0004FF13
		public event CreateUserErrorEventHandler CreateUserError
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.CreateUserErrorEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.CreateUserErrorEvent, value);
			}
		}

		/// <summary>Occurs before the membership provider is called to create the new Web site user account.</summary>
		// Token: 0x14000060 RID: 96
		// (add) Token: 0x0600207E RID: 8318 RVA: 0x00051D26 File Offset: 0x0004FF26
		// (remove) Token: 0x0600207F RID: 8319 RVA: 0x00051D39 File Offset: 0x0004FF39
		public event LoginCancelEventHandler CreatingUser
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.CreatingUserEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.CreatingUserEvent, value);
			}
		}

		/// <summary>Occurs before the user is sent an e-mail confirmation that an account has been created.</summary>
		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06002080 RID: 8320 RVA: 0x00051D4C File Offset: 0x0004FF4C
		// (remove) Token: 0x06002081 RID: 8321 RVA: 0x00051D5F File Offset: 0x0004FF5F
		public event MailMessageEventHandler SendingMail
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.SendingMailEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.SendingMailEvent, value);
			}
		}

		/// <summary>Occurs when there is an SMTP error sending e-mail to the new user.</summary>
		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06002082 RID: 8322 RVA: 0x00051D72 File Offset: 0x0004FF72
		// (remove) Token: 0x06002083 RID: 8323 RVA: 0x00051D85 File Offset: 0x0004FF85
		public event SendMailErrorEventHandler SendMailError
		{
			add
			{
				base.Events.AddHandler(CreateUserWizard.SendMailErrorEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CreateUserWizard.SendMailErrorEvent, value);
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00051D98 File Offset: 0x0004FF98
		internal override void InstantiateTemplateStep(TemplatedWizardStep step)
		{
			if (step is CreateUserWizardStep)
			{
				this.InstantiateCreateUserWizardStep((CreateUserWizardStep)step);
				return;
			}
			if (step is CompleteWizardStep)
			{
				this.InstantiateCompleteWizardStep((CompleteWizardStep)step);
				return;
			}
			base.InstantiateTemplateStep(step);
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00051DCC File Offset: 0x0004FFCC
		private void InstantiateCompleteWizardStep(CompleteWizardStep step)
		{
			CreateUserWizard.CompleteStepContainer completeStepContainer = new CreateUserWizard.CompleteStepContainer(this);
			if (step.ContentTemplate != null)
			{
				step.ContentTemplate.InstantiateIn(completeStepContainer.InnerCell);
			}
			else
			{
				new CreateUserWizard.CompleteStepTemplate(this).InstantiateIn(completeStepContainer.InnerCell);
				completeStepContainer.ConfirmDefaultTemplate();
			}
			step.ContentTemplateContainer = completeStepContainer;
			step.Controls.Clear();
			step.Controls.Add(completeStepContainer);
			Wizard.BaseWizardNavigationContainer baseWizardNavigationContainer = new Wizard.BaseWizardNavigationContainer();
			if (step.CustomNavigationTemplate != null)
			{
				step.CustomNavigationTemplate.InstantiateIn(baseWizardNavigationContainer);
				base.RegisterCustomNavigation(step, baseWizardNavigationContainer);
			}
			step.CustomNavigationTemplateContainer = baseWizardNavigationContainer;
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00051E5C File Offset: 0x0005005C
		private void InstantiateCreateUserWizardStep(CreateUserWizardStep step)
		{
			CreateUserWizard.CreateUserStepContainer createUserStepContainer = new CreateUserWizard.CreateUserStepContainer(this);
			if (step.ContentTemplate != null)
			{
				step.ContentTemplate.InstantiateIn(createUserStepContainer.InnerCell);
			}
			else
			{
				new CreateUserWizard.CreateUserStepTemplate(this).InstantiateIn(createUserStepContainer.InnerCell);
				createUserStepContainer.ConfirmDefaultTemplate();
				createUserStepContainer.EnsureValidatorsState();
			}
			step.ContentTemplateContainer = createUserStepContainer;
			step.Controls.Clear();
			step.Controls.Add(createUserStepContainer);
			CreateUserWizard.CreateUserNavigationContainer createUserNavigationContainer = new CreateUserWizard.CreateUserNavigationContainer(this);
			if (step.CustomNavigationTemplate != null)
			{
				step.CustomNavigationTemplate.InstantiateIn(createUserNavigationContainer);
			}
			else
			{
				new CreateUserWizard.CreateUserStepNavigationTemplate(this).InstantiateIn(createUserNavigationContainer);
				createUserNavigationContainer.ConfirmDefaultTemplate();
			}
			base.RegisterCustomNavigation(step, createUserNavigationContainer);
			step.CustomNavigationTemplateContainer = createUserNavigationContainer;
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x00051F04 File Offset: 0x00050104
		internal override ITemplate SideBarItemTemplate
		{
			get
			{
				return new CreateUserWizard.SideBarLabelTemplate(this);
			}
		}

		/// <summary>Called by the ASP.NET page framework to notify this control to create any child controls that it contains in preparation for posting back or rendering.</summary>
		// Token: 0x06002088 RID: 8328 RVA: 0x00051F0C File Offset: 0x0005010C
		protected internal override void CreateChildControls()
		{
			if (this.CreateUserStep == null)
			{
				this.WizardSteps.AddAt(0, new CreateUserWizardStep());
			}
			if (this.CompleteStep == null)
			{
				this.WizardSteps.AddAt(this.WizardSteps.Count, new CompleteWizardStep());
			}
			base.CreateChildControls();
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00051F5C File Offset: 0x0005015C
		protected override void CreateControlHierarchy()
		{
			base.CreateControlHierarchy();
			CreateUserWizard.CreateUserStepContainer createUserStepContainer = this.CreateUserStep.ContentTemplateContainer as CreateUserWizard.CreateUserStepContainer;
			if (createUserStepContainer != null)
			{
				IEditableTextControl editableTextControl = createUserStepContainer.UserNameTextBox as IEditableTextControl;
				if (editableTextControl != null)
				{
					editableTextControl.TextChanged += this.UserName_TextChanged;
				}
				if (!this.AutoGeneratePassword)
				{
					editableTextControl = createUserStepContainer.PasswordTextBox as IEditableTextControl;
					if (editableTextControl != null)
					{
						editableTextControl.TextChanged += this.Password_TextChanged;
					}
					editableTextControl = createUserStepContainer.ConfirmPasswordTextBox as IEditableTextControl;
					if (editableTextControl != null)
					{
						editableTextControl.TextChanged += this.ConfirmPassword_TextChanged;
					}
				}
				if (this.RequireEmail)
				{
					editableTextControl = createUserStepContainer.EmailTextBox as IEditableTextControl;
					if (editableTextControl != null)
					{
						editableTextControl.TextChanged += this.Email_TextChanged;
					}
				}
				if (this.QuestionAndAnswerRequired)
				{
					editableTextControl = createUserStepContainer.QuestionTextBox as IEditableTextControl;
					if (editableTextControl != null)
					{
						editableTextControl.TextChanged += this.Question_TextChanged;
					}
					editableTextControl = createUserStepContainer.AnswerTextBox as IEditableTextControl;
					if (editableTextControl != null)
					{
						editableTextControl.TextChanged += this.Answer_TextChanged;
					}
				}
				this._errorMessageLabel = createUserStepContainer.ErrorMessageLabel;
			}
		}

		/// <summary>Gets design-time data for a control.</summary>
		/// <returns>None</returns>
		// Token: 0x0600208A RID: 8330 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not Implemented")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the event for the server control is passed up the page's UI server control hierarchy.</summary>
		/// <returns>A Boolean value.</returns>
		/// <param name="source"> None</param>
		/// <param name="e"> None</param>
		// Token: 0x0600208B RID: 8331 RVA: 0x00052070 File Offset: 0x00050270
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (e != null && commandEventArgs.CommandName == CreateUserWizard.ContinueButtonCommandName)
			{
				this.ProcessContinueEvent();
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000520A9 File Offset: 0x000502A9
		private void ProcessContinueEvent()
		{
			this.OnContinueButtonClick(EventArgs.Empty);
			if (this.ContinueDestinationPageUrl.Length > 0)
			{
				this.Context.Response.Redirect(this.ContinueDestinationPageUrl);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.ContinueButtonClick" /> event when the user clicks the Continue button on the final user account creation step.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600208D RID: 8333 RVA: 0x000520DC File Offset: 0x000502DC
		protected virtual void OnContinueButtonClick(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[CreateUserWizard.ContinueButtonClickEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreatedUser" /> event after the membership provider creates the user account.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600208E RID: 8334 RVA: 0x00052114 File Offset: 0x00050314
		protected virtual void OnCreatedUser(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[CreateUserWizard.CreatedUserEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreateUserError" /> event when there is a problem creating the specified user account.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CreateUserErrorEventArgs" /> with the data for the event.</param>
		// Token: 0x0600208F RID: 8335 RVA: 0x0005214C File Offset: 0x0005034C
		protected virtual void OnCreateUserError(CreateUserErrorEventArgs e)
		{
			if (base.Events != null)
			{
				CreateUserErrorEventHandler createUserErrorEventHandler = (CreateUserErrorEventHandler)base.Events[CreateUserWizard.CreateUserErrorEvent];
				if (createUserErrorEventHandler != null)
				{
					createUserErrorEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreatingUser" /> event prior to calling the membership provider to create the new user account.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> containing the event data.</param>
		// Token: 0x06002090 RID: 8336 RVA: 0x00052184 File Offset: 0x00050384
		protected virtual void OnCreatingUser(LoginCancelEventArgs e)
		{
			if (base.Events != null)
			{
				LoginCancelEventHandler loginCancelEventHandler = (LoginCancelEventHandler)base.Events[CreateUserWizard.CreatingUserEvent];
				if (loginCancelEventHandler != null)
				{
					loginCancelEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.NextButtonClick" /> event when the user clicks the Next button in one of the Create User wizard steps.</summary>
		// Token: 0x06002091 RID: 8337 RVA: 0x000521BA File Offset: 0x000503BA
		protected override void OnNextButtonClick(WizardNavigationEventArgs e)
		{
			if (base.ActiveStep == this.CreateUserStep)
			{
				if (!this.CreateUser())
				{
					e.Cancel = true;
				}
				else if (this.LoginCreatedUser)
				{
					this.Login();
				}
			}
			base.OnNextButtonClick(e);
		}

		/// <exception cref="T:System.Web.HttpException">The membership provider for the page cannot be found. For more information, see <see cref="P:System.Web.Security.Membership.Providers" />.</exception>
		// Token: 0x06002092 RID: 8338 RVA: 0x000419F4 File Offset: 0x0003FBF4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.SendingMail" /> event before an e-mail message is sent to a new user.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> containing the event data.</param>
		// Token: 0x06002093 RID: 8339 RVA: 0x000521F0 File Offset: 0x000503F0
		protected virtual void OnSendingMail(MailMessageEventArgs e)
		{
			if (base.Events != null)
			{
				MailMessageEventHandler mailMessageEventHandler = (MailMessageEventHandler)base.Events[CreateUserWizard.SendingMailEvent];
				if (mailMessageEventHandler != null)
				{
					mailMessageEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.SendMailError" /> event when e-mail cannot be sent to the new user.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> containing the event data.</param>
		// Token: 0x06002094 RID: 8340 RVA: 0x00052228 File Offset: 0x00050428
		protected virtual void OnSendMailError(SendMailErrorEventArgs e)
		{
			if (base.Events != null)
			{
				SendMailErrorEventHandler sendMailErrorEventHandler = (SendMailErrorEventHandler)base.Events[CreateUserWizard.SendMailErrorEvent];
				if (sendMailErrorEventHandler != null)
				{
					sendMailErrorEventHandler(this, e);
				}
			}
		}

		/// <summary>Restores view-state information from a previous page request that was saved by the SaveViewState method.</summary>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.Control.ViewState" />.</exception>
		// Token: 0x06002095 RID: 8341 RVA: 0x00052260 File Offset: 0x00050460
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
				((IStateManager)this.TextBoxStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.ValidatorTextStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.CompleteSuccessTextStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.ErrorMessageStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.HyperLinkStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.InstructionTextStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.LabelStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.PasswordHintStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.TitleTextStyle).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.CreateUserButtonStyle).LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				((IStateManager)this.ContinueButtonStyle).LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				((IStateManager)this.MailDefinition).LoadViewState(array[12]);
			}
			((CreateUserWizard.CreateUserStepContainer)this.CreateUserStep.ContentTemplateContainer).EnsureValidatorsState();
		}

		/// <summary>Saves the state of the control.</summary>
		// Token: 0x06002096 RID: 8342 RVA: 0x0005238C File Offset: 0x0005058C
		protected override object SaveViewState()
		{
			object[] array = new object[13];
			array[0] = base.SaveViewState();
			if (this._textBoxStyle != null)
			{
				array[1] = ((IStateManager)this._textBoxStyle).SaveViewState();
			}
			if (this._validatorTextStyle != null)
			{
				array[2] = ((IStateManager)this._validatorTextStyle).SaveViewState();
			}
			if (this._completeSuccessTextStyle != null)
			{
				array[3] = ((IStateManager)this._completeSuccessTextStyle).SaveViewState();
			}
			if (this._errorMessageStyle != null)
			{
				array[4] = ((IStateManager)this._errorMessageStyle).SaveViewState();
			}
			if (this._hyperLinkStyle != null)
			{
				array[5] = ((IStateManager)this._hyperLinkStyle).SaveViewState();
			}
			if (this._instructionTextStyle != null)
			{
				array[6] = ((IStateManager)this._instructionTextStyle).SaveViewState();
			}
			if (this._labelStyle != null)
			{
				array[7] = ((IStateManager)this._labelStyle).SaveViewState();
			}
			if (this._passwordHintStyle != null)
			{
				array[8] = ((IStateManager)this._passwordHintStyle).SaveViewState();
			}
			if (this._titleTextStyle != null)
			{
				array[9] = ((IStateManager)this._titleTextStyle).SaveViewState();
			}
			if (this._createUserButtonStyle != null)
			{
				array[10] = ((IStateManager)this._createUserButtonStyle).SaveViewState();
			}
			if (this._continueButtonStyle != null)
			{
				array[11] = ((IStateManager)this._continueButtonStyle).SaveViewState();
			}
			if (this._mailDefinition != null)
			{
				array[12] = ((IStateManager)this._mailDefinition).SaveViewState();
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

		// Token: 0x06002097 RID: 8343 RVA: 0x000524CC File Offset: 0x000506CC
		[global::System.MonoTODO("for design-time usage - no more details available")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			base.SetDesignModeState(data);
		}

		/// <summary>Marks the starting point to begin tracking changes to the control as part of the control viewstate.</summary>
		// Token: 0x06002098 RID: 8344 RVA: 0x000524D8 File Offset: 0x000506D8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._textBoxStyle != null)
			{
				((IStateManager)this._textBoxStyle).TrackViewState();
			}
			if (this._validatorTextStyle != null)
			{
				((IStateManager)this._validatorTextStyle).TrackViewState();
			}
			if (this._completeSuccessTextStyle != null)
			{
				((IStateManager)this._completeSuccessTextStyle).TrackViewState();
			}
			if (this._errorMessageStyle != null)
			{
				((IStateManager)this._errorMessageStyle).TrackViewState();
			}
			if (this._hyperLinkStyle != null)
			{
				((IStateManager)this._hyperLinkStyle).TrackViewState();
			}
			if (this._instructionTextStyle != null)
			{
				((IStateManager)this._instructionTextStyle).TrackViewState();
			}
			if (this._labelStyle != null)
			{
				((IStateManager)this._labelStyle).TrackViewState();
			}
			if (this._passwordHintStyle != null)
			{
				((IStateManager)this._passwordHintStyle).TrackViewState();
			}
			if (this._titleTextStyle != null)
			{
				((IStateManager)this._titleTextStyle).TrackViewState();
			}
			if (this._createUserButtonStyle != null)
			{
				((IStateManager)this._createUserButtonStyle).TrackViewState();
			}
			if (this._continueButtonStyle != null)
			{
				((IStateManager)this._continueButtonStyle).TrackViewState();
			}
			if (this._mailDefinition != null)
			{
				((IStateManager)this._mailDefinition).TrackViewState();
			}
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000525CF File Offset: 0x000507CF
		private void UserName_TextChanged(object sender, EventArgs e)
		{
			this.UserName = ((ITextControl)sender).Text;
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000525E2 File Offset: 0x000507E2
		private void Password_TextChanged(object sender, EventArgs e)
		{
			this._password = ((ITextControl)sender).Text;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000525F5 File Offset: 0x000507F5
		private void ConfirmPassword_TextChanged(object sender, EventArgs e)
		{
			this._confirmPassword = ((ITextControl)sender).Text;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00052608 File Offset: 0x00050808
		private void Email_TextChanged(object sender, EventArgs e)
		{
			this.Email = ((ITextControl)sender).Text;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0005261B File Offset: 0x0005081B
		private void Question_TextChanged(object sender, EventArgs e)
		{
			this.Question = ((ITextControl)sender).Text;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0005262E File Offset: 0x0005082E
		private void Answer_TextChanged(object sender, EventArgs e)
		{
			this.Answer = ((ITextControl)sender).Text;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00052644 File Offset: 0x00050844
		private void InitMemberShipProvider()
		{
			string membershipProvider = this.MembershipProvider;
			this._provider = ((membershipProvider.Length == 0) ? (this._provider = Membership.Provider) : Membership.Providers[membershipProvider]);
			if (this._provider == null)
			{
				throw new HttpException(global::Locale.GetText("No provider named '{0}' could be found.", new object[] { membershipProvider }));
			}
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000526A4 File Offset: 0x000508A4
		private bool CreateUser()
		{
			if (!this.Page.IsValid)
			{
				return false;
			}
			if (this.AutoGeneratePassword)
			{
				this._password = this.GeneratePassword();
			}
			this.OnCreatingUser(new LoginCancelEventArgs(false));
			MembershipCreateStatus membershipCreateStatus;
			MembershipUser membershipUser = this.MembershipProviderInternal.CreateUser(this.UserName, this.Password, this.Email, this.Question, this.Answer, !this.DisableCreatedUser, null, out membershipCreateStatus);
			if (membershipUser != null && membershipCreateStatus == MembershipCreateStatus.Success)
			{
				this.OnCreatedUser(new EventArgs());
				this.SendPasswordByMail(membershipUser, this.Password);
				return true;
			}
			switch (membershipCreateStatus)
			{
			case MembershipCreateStatus.InvalidUserName:
			case MembershipCreateStatus.UserRejected:
			case MembershipCreateStatus.InvalidProviderUserKey:
			case MembershipCreateStatus.ProviderError:
				this.ShowErrorMessage(this.UnknownErrorMessage);
				break;
			case MembershipCreateStatus.InvalidPassword:
				this.ShowErrorMessage(string.Format(this.InvalidPasswordErrorMessage, this.MembershipProviderInternal.MinRequiredPasswordLength, this.MembershipProviderInternal.MinRequiredNonAlphanumericCharacters));
				break;
			case MembershipCreateStatus.InvalidQuestion:
				this.ShowErrorMessage(this.InvalidQuestionErrorMessage);
				break;
			case MembershipCreateStatus.InvalidAnswer:
				this.ShowErrorMessage(this.InvalidAnswerErrorMessage);
				break;
			case MembershipCreateStatus.InvalidEmail:
				this.ShowErrorMessage(this.InvalidEmailErrorMessage);
				break;
			case MembershipCreateStatus.DuplicateUserName:
				this.ShowErrorMessage(this.DuplicateUserNameErrorMessage);
				break;
			case MembershipCreateStatus.DuplicateEmail:
				this.ShowErrorMessage(this.DuplicateEmailErrorMessage);
				break;
			}
			this.OnCreateUserError(new CreateUserErrorEventArgs(membershipCreateStatus));
			return false;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00052804 File Offset: 0x00050A04
		private void SendPasswordByMail(MembershipUser user, string password)
		{
			if (user == null)
			{
				return;
			}
			if (this._mailDefinition == null)
			{
				return;
			}
			string text = "A new account has been created for you. Please go to the site and log in using the following information.\nUser Name: <%USERNAME%>\nPassword: <%PASSWORD%>";
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add("<%USERNAME%>", user.UserName);
			listDictionary.Add("<%PASSWORD%>", password);
			MailMessage mailMessage;
			if (this.MailDefinition.BodyFileName.Length == 0)
			{
				mailMessage = this.MailDefinition.CreateMailMessage(user.Email, listDictionary, text, this);
			}
			else
			{
				mailMessage = this.MailDefinition.CreateMailMessage(user.Email, listDictionary, this);
			}
			if (string.IsNullOrEmpty(mailMessage.Subject))
			{
				mailMessage.Subject = "Account information";
			}
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

		// Token: 0x060020A2 RID: 8354 RVA: 0x000528F0 File Offset: 0x00050AF0
		private void Login()
		{
			if (this.MembershipProviderInternal.ValidateUser(this.UserName, this.Password))
			{
				FormsAuthentication.SetAuthCookie(this.UserName, false);
			}
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00052917 File Offset: 0x00050B17
		private void ShowErrorMessage(string errorMessage)
		{
			if (this._errorMessageLabel != null)
			{
				this._errorMessageLabel.Text = errorMessage;
			}
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x0005292D File Offset: 0x00050B2D
		private string GeneratePassword()
		{
			return Membership.GeneratePassword(8, 3);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00052938 File Offset: 0x00050B38
		// Note: this type is marked as 'beforefieldinit'.
		static CreateUserWizard()
		{
			CreateUserWizard.CreatedUserEvent = new object();
			CreateUserWizard.CreateUserErrorEvent = new object();
			CreateUserWizard.CreatingUserEvent = new object();
			CreateUserWizard.ContinueButtonClickEvent = new object();
			CreateUserWizard.SendingMailEvent = new object();
			CreateUserWizard.SendMailErrorEvent = new object();
		}

		/// <summary>Represents the <see cref="P:System.Web.UI.WebControls.Button.CommandName" /> value of the Continue button on the final step for creating a user account. The <see cref="F:System.Web.UI.WebControls.CreateUserWizard.ContinueButtonCommandName" /> field is read-only. </summary>
		// Token: 0x0400188D RID: 6285
		public static readonly string ContinueButtonCommandName = "Continue";

		// Token: 0x0400188E RID: 6286
		private string _password = string.Empty;

		// Token: 0x0400188F RID: 6287
		private string _confirmPassword = string.Empty;

		// Token: 0x04001890 RID: 6288
		private MembershipProvider _provider;

		// Token: 0x04001891 RID: 6289
		private ITextControl _errorMessageLabel;

		// Token: 0x04001892 RID: 6290
		private MailDefinition _mailDefinition;

		// Token: 0x04001893 RID: 6291
		private Style _textBoxStyle;

		// Token: 0x04001894 RID: 6292
		private Style _validatorTextStyle;

		// Token: 0x04001895 RID: 6293
		private TableItemStyle _completeSuccessTextStyle;

		// Token: 0x04001896 RID: 6294
		private TableItemStyle _errorMessageStyle;

		// Token: 0x04001897 RID: 6295
		private TableItemStyle _hyperLinkStyle;

		// Token: 0x04001898 RID: 6296
		private TableItemStyle _instructionTextStyle;

		// Token: 0x04001899 RID: 6297
		private TableItemStyle _labelStyle;

		// Token: 0x0400189A RID: 6298
		private TableItemStyle _passwordHintStyle;

		// Token: 0x0400189B RID: 6299
		private TableItemStyle _titleTextStyle;

		// Token: 0x0400189C RID: 6300
		private Style _createUserButtonStyle;

		// Token: 0x0400189D RID: 6301
		private Style _continueButtonStyle;

		// Token: 0x040018A4 RID: 6308
		private CompleteWizardStep _completeWizardStep;

		// Token: 0x040018A5 RID: 6309
		private CreateUserWizardStep _createUserWizardStep;

		// Token: 0x02000362 RID: 866
		private class SideBarLabelTemplate : ITemplate
		{
			// Token: 0x060020A6 RID: 8358 RVA: 0x0005298B File Offset: 0x00050B8B
			public SideBarLabelTemplate(Wizard wizard)
			{
				this.wizard = wizard;
			}

			// Token: 0x060020A7 RID: 8359 RVA: 0x0005299C File Offset: 0x00050B9C
			public void InstantiateIn(Control control)
			{
				Label label = new Label();
				this.wizard.RegisterApplyStyle(label, this.wizard.SideBarButtonStyle);
				control.Controls.Add(label);
				control.DataBinding += this.Bound;
			}

			// Token: 0x060020A8 RID: 8360 RVA: 0x000529E4 File Offset: 0x00050BE4
			private void Bound(object s, EventArgs args)
			{
				WizardStepBase wizardStepBase = DataBinder.GetDataItem(s) as WizardStepBase;
				if (wizardStepBase != null)
				{
					Label label = (Label)((Control)s).Controls[0];
					label.ID = Wizard.SideBarButtonID;
					label.Text = wizardStepBase.Title;
				}
			}

			// Token: 0x040018A6 RID: 6310
			private Wizard wizard;
		}

		// Token: 0x02000363 RID: 867
		private sealed class CreateUserNavigationContainer : Wizard.DefaultNavigationContainer
		{
			// Token: 0x060020A9 RID: 8361 RVA: 0x00052A2C File Offset: 0x00050C2C
			public CreateUserNavigationContainer(CreateUserWizard createUserWizard)
				: base(createUserWizard)
			{
				this._createUserWizard = createUserWizard;
			}

			// Token: 0x060020AA RID: 8362 RVA: 0x00052A3C File Offset: 0x00050C3C
			protected override void UpdateState()
			{
				int num = this._createUserWizard.ActiveStepIndex - 1;
				if (num >= 0 && this._createUserWizard.AllowNavigationToStep(num))
				{
					base.UpdateNavButtonState(Wizard.StepPreviousButtonID + base.Wizard.StepPreviousButtonType, base.Wizard.StepPreviousButtonText, base.Wizard.StepPreviousButtonImageUrl, base.Wizard.StepPreviousButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[0].Visible = false;
				}
				base.UpdateNavButtonState(Wizard.StepNextButtonID + this._createUserWizard.CreateUserButtonType, this._createUserWizard.CreateUserButtonText, this._createUserWizard.CreateUserButtonImageUrl, this._createUserWizard.CreateUserButtonStyle);
				if (base.Wizard.DisplayCancelButton)
				{
					base.UpdateNavButtonState(Wizard.CancelButtonID + base.Wizard.CancelButtonType, base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
					return;
				}
				((Table)this.Controls[0]).Rows[0].Cells[2].Visible = false;
			}

			// Token: 0x040018A7 RID: 6311
			private CreateUserWizard _createUserWizard;
		}

		// Token: 0x02000364 RID: 868
		private sealed class CreateUserStepNavigationTemplate : ITemplate
		{
			// Token: 0x060020AB RID: 8363 RVA: 0x00052B96 File Offset: 0x00050D96
			public CreateUserStepNavigationTemplate(CreateUserWizard createUserWizard)
			{
				this._createUserWizard = createUserWizard;
			}

			// Token: 0x060020AC RID: 8364 RVA: 0x00052BA8 File Offset: 0x00050DA8
			public void InstantiateIn(Control container)
			{
				Table table = new Table();
				table.CellPadding = 5;
				table.CellSpacing = 5;
				table.Width = Unit.Percentage(100.0);
				table.Height = Unit.Percentage(100.0);
				TableRow tableRow = new TableRow();
				this.AddButtonCell(tableRow, this._createUserWizard.CreateButtonSet(Wizard.StepPreviousButtonID, Wizard.MovePreviousCommandName, false, this._createUserWizard.ID));
				this.AddButtonCell(tableRow, this._createUserWizard.CreateButtonSet(Wizard.StepNextButtonID, Wizard.MoveNextCommandName, true, this._createUserWizard.ID));
				this.AddButtonCell(tableRow, this._createUserWizard.CreateButtonSet(Wizard.CancelButtonID, Wizard.CancelCommandName, false, this._createUserWizard.ID));
				table.Rows.Add(tableRow);
				container.Controls.Add(table);
			}

			// Token: 0x060020AD RID: 8365 RVA: 0x00052C88 File Offset: 0x00050E88
			private void AddButtonCell(TableRow row, params Control[] controls)
			{
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				for (int i = 0; i < controls.Length; i++)
				{
					tableCell.Controls.Add(controls[i]);
				}
				row.Cells.Add(tableCell);
			}

			// Token: 0x040018A8 RID: 6312
			private readonly CreateUserWizard _createUserWizard;
		}

		// Token: 0x02000365 RID: 869
		private sealed class CreateUserStepContainer : Wizard.DefaultContentContainer
		{
			// Token: 0x060020AE RID: 8366 RVA: 0x00052CCB File Offset: 0x00050ECB
			public CreateUserStepContainer(CreateUserWizard createUserWizard)
				: base(createUserWizard)
			{
				this._createUserWizard = createUserWizard;
			}

			// Token: 0x17000A3E RID: 2622
			// (get) Token: 0x060020AF RID: 8367 RVA: 0x00052CDB File Offset: 0x00050EDB
			public Control UserNameTextBox
			{
				get
				{
					Control control = this.FindControl("UserName");
					if (control == null)
					{
						throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID UserName for the username.");
					}
					return control;
				}
			}

			// Token: 0x17000A3F RID: 2623
			// (get) Token: 0x060020B0 RID: 8368 RVA: 0x00052CF6 File Offset: 0x00050EF6
			public Control PasswordTextBox
			{
				get
				{
					Control control = this.FindControl("Password");
					if (control == null)
					{
						throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID Password for the new password, this is required if AutoGeneratePassword = true.");
					}
					return control;
				}
			}

			// Token: 0x17000A40 RID: 2624
			// (get) Token: 0x060020B1 RID: 8369 RVA: 0x00052D11 File Offset: 0x00050F11
			public Control ConfirmPasswordTextBox
			{
				get
				{
					return this.FindControl("Password");
				}
			}

			// Token: 0x17000A41 RID: 2625
			// (get) Token: 0x060020B2 RID: 8370 RVA: 0x00052D1E File Offset: 0x00050F1E
			public Control EmailTextBox
			{
				get
				{
					Control control = this.FindControl("Email");
					if (control == null)
					{
						throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID Email for the e-mail, this is required if RequireEmail = true.");
					}
					return control;
				}
			}

			// Token: 0x17000A42 RID: 2626
			// (get) Token: 0x060020B3 RID: 8371 RVA: 0x00052D39 File Offset: 0x00050F39
			public Control QuestionTextBox
			{
				get
				{
					Control control = this.FindControl("Question");
					if (control == null)
					{
						throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID Question for the security question, this is required if your membership provider requires a question and answer.");
					}
					return control;
				}
			}

			// Token: 0x17000A43 RID: 2627
			// (get) Token: 0x060020B4 RID: 8372 RVA: 0x00052D54 File Offset: 0x00050F54
			public Control AnswerTextBox
			{
				get
				{
					Control control = this.FindControl("Answer");
					if (control == null)
					{
						throw new HttpException("CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID Answer for the security answer, this is required if your membership provider requires a question and answer.");
					}
					return control;
				}
			}

			// Token: 0x17000A44 RID: 2628
			// (get) Token: 0x060020B5 RID: 8373 RVA: 0x00052D6F File Offset: 0x00050F6F
			public ITextControl ErrorMessageLabel
			{
				get
				{
					return this.FindControl("ErrorMessage") as ITextControl;
				}
			}

			// Token: 0x060020B6 RID: 8374 RVA: 0x00052D84 File Offset: 0x00050F84
			protected override void UpdateState()
			{
				if (string.IsNullOrEmpty(this._createUserWizard.CreateUserStep.Title))
				{
					((Table)base.InnerCell.Controls[0]).Rows[0].Visible = false;
				}
				else
				{
					((Table)base.InnerCell.Controls[0]).Rows[0].Cells[0].Text = this._createUserWizard.CreateUserStep.Title;
				}
				if (string.IsNullOrEmpty(this._createUserWizard.InstructionText))
				{
					((Table)base.InnerCell.Controls[0]).Rows[1].Visible = false;
				}
				else
				{
					((Table)base.InnerCell.Controls[0]).Rows[1].Cells[0].Text = this._createUserWizard.InstructionText;
				}
				((Label)((Table)base.InnerCell.Controls[0]).Rows[2].Cells[0].Controls[0]).Text = this._createUserWizard.UserNameLabelText;
				RequiredFieldValidator requiredFieldValidator = (RequiredFieldValidator)this.FindControl("UserNameRequired");
				requiredFieldValidator.ErrorMessage = this._createUserWizard.UserNameRequiredErrorMessage;
				requiredFieldValidator.ToolTip = this._createUserWizard.UserNameRequiredErrorMessage;
				if (this._createUserWizard.AutoGeneratePassword)
				{
					((Table)base.InnerCell.Controls[0]).Rows[3].Visible = false;
					((Table)base.InnerCell.Controls[0]).Rows[4].Visible = false;
					((Table)base.InnerCell.Controls[0]).Rows[5].Visible = false;
				}
				else
				{
					((Label)((Table)base.InnerCell.Controls[0]).Rows[3].Cells[0].Controls[0]).Text = this._createUserWizard.PasswordLabelText;
					RequiredFieldValidator requiredFieldValidator2 = (RequiredFieldValidator)this.FindControl("PasswordRequired");
					requiredFieldValidator2.ErrorMessage = this._createUserWizard.PasswordRequiredErrorMessage;
					requiredFieldValidator2.ToolTip = this._createUserWizard.PasswordRequiredErrorMessage;
					if (string.IsNullOrEmpty(this._createUserWizard.PasswordHintText))
					{
						((Table)base.InnerCell.Controls[0]).Rows[4].Visible = false;
					}
					else
					{
						((Table)base.InnerCell.Controls[0]).Rows[4].Cells[1].Text = this._createUserWizard.PasswordHintText;
					}
					((Label)((Table)base.InnerCell.Controls[0]).Rows[5].Cells[0].Controls[0]).Text = this._createUserWizard.ConfirmPasswordLabelText;
					RequiredFieldValidator requiredFieldValidator3 = (RequiredFieldValidator)this.FindControl("ConfirmPasswordRequired");
					requiredFieldValidator3.ErrorMessage = this._createUserWizard.ConfirmPasswordRequiredErrorMessage;
					requiredFieldValidator3.ToolTip = this._createUserWizard.ConfirmPasswordRequiredErrorMessage;
				}
				if (this._createUserWizard.RequireEmail)
				{
					((Label)((Table)base.InnerCell.Controls[0]).Rows[6].Cells[0].Controls[0]).Text = this._createUserWizard.EmailLabelText;
					RequiredFieldValidator requiredFieldValidator4 = (RequiredFieldValidator)this.FindControl("EmailRequired");
					requiredFieldValidator4.ErrorMessage = this._createUserWizard.EmailRequiredErrorMessage;
					requiredFieldValidator4.ToolTip = this._createUserWizard.EmailRequiredErrorMessage;
				}
				else
				{
					((Table)base.InnerCell.Controls[0]).Rows[6].Visible = false;
				}
				if (this._createUserWizard.QuestionAndAnswerRequired)
				{
					((Label)((Table)base.InnerCell.Controls[0]).Rows[7].Cells[0].Controls[0]).Text = this._createUserWizard.QuestionLabelText;
					RequiredFieldValidator requiredFieldValidator5 = (RequiredFieldValidator)this.FindControl("QuestionRequired");
					requiredFieldValidator5.ErrorMessage = this._createUserWizard.QuestionRequiredErrorMessage;
					requiredFieldValidator5.ToolTip = this._createUserWizard.QuestionRequiredErrorMessage;
					((Label)((Table)base.InnerCell.Controls[0]).Rows[8].Cells[0].Controls[0]).Text = this._createUserWizard.AnswerLabelText;
					RequiredFieldValidator requiredFieldValidator6 = (RequiredFieldValidator)this.FindControl("AnswerRequired");
					requiredFieldValidator6.ErrorMessage = this._createUserWizard.AnswerRequiredErrorMessage;
					requiredFieldValidator6.ToolTip = this._createUserWizard.AnswerRequiredErrorMessage;
				}
				else
				{
					((Table)base.InnerCell.Controls[0]).Rows[7].Visible = false;
					((Table)base.InnerCell.Controls[0]).Rows[8].Visible = false;
				}
				if (this._createUserWizard.AutoGeneratePassword)
				{
					((Table)base.InnerCell.Controls[0]).Rows[9].Visible = false;
				}
				else
				{
					((CompareValidator)this.FindControl("PasswordCompare")).ErrorMessage = this._createUserWizard.ConfirmPasswordCompareErrorMessage;
				}
				if (this._createUserWizard.AutoGeneratePassword || string.IsNullOrEmpty(this._createUserWizard.PasswordRegularExpression))
				{
					((Table)base.InnerCell.Controls[0]).Rows[10].Visible = false;
				}
				else
				{
					RegularExpressionValidator regularExpressionValidator = (RegularExpressionValidator)this.FindControl("PasswordRegEx");
					regularExpressionValidator.ValidationExpression = this._createUserWizard.PasswordRegularExpression;
					regularExpressionValidator.ErrorMessage = this._createUserWizard.PasswordRegularExpressionErrorMessage;
				}
				if (!this._createUserWizard.RequireEmail || string.IsNullOrEmpty(this._createUserWizard.EmailRegularExpression))
				{
					((Table)base.InnerCell.Controls[0]).Rows[11].Visible = false;
				}
				else
				{
					RegularExpressionValidator regularExpressionValidator2 = (RegularExpressionValidator)this.FindControl("EmailRegEx");
					regularExpressionValidator2.ErrorMessage = this._createUserWizard.EmailRegularExpressionErrorMessage;
					regularExpressionValidator2.ValidationExpression = this._createUserWizard.EmailRegularExpression;
				}
				if (string.IsNullOrEmpty(this.ErrorMessageLabel.Text))
				{
					((Table)base.InnerCell.Controls[0]).Rows[12].Visible = false;
				}
				Image image = (Image)((Table)base.InnerCell.Controls[0]).Rows[13].Cells[0].Controls[0];
				if (string.IsNullOrEmpty(this._createUserWizard.HelpPageIconUrl))
				{
					image.Visible = false;
				}
				else
				{
					image.ImageUrl = this._createUserWizard.HelpPageIconUrl;
					image.AlternateText = this._createUserWizard.HelpPageText;
				}
				HyperLink hyperLink = (HyperLink)((Table)base.InnerCell.Controls[0]).Rows[13].Cells[0].Controls[1];
				if (string.IsNullOrEmpty(this._createUserWizard.HelpPageText))
				{
					hyperLink.Visible = false;
				}
				else
				{
					hyperLink.Text = this._createUserWizard.HelpPageText;
					hyperLink.NavigateUrl = this._createUserWizard.HelpPageUrl;
				}
				((Table)base.InnerCell.Controls[0]).Rows[13].Visible = image.Visible || hyperLink.Visible;
			}

			// Token: 0x060020B7 RID: 8375 RVA: 0x000535C0 File Offset: 0x000517C0
			public void EnsureValidatorsState()
			{
				if (!base.IsDefaultTemplate)
				{
					return;
				}
				((RequiredFieldValidator)this.FindControl("PasswordRequired")).Enabled = !this._createUserWizard.AutoGeneratePassword;
				((RequiredFieldValidator)this.FindControl("ConfirmPasswordRequired")).Enabled = !this._createUserWizard.AutoGeneratePassword;
				((CompareValidator)this.FindControl("PasswordCompare")).Enabled = !this._createUserWizard.AutoGeneratePassword;
				RegularExpressionValidator regularExpressionValidator = (RegularExpressionValidator)this.FindControl("PasswordRegEx");
				regularExpressionValidator.Enabled = !this._createUserWizard.AutoGeneratePassword && !string.IsNullOrEmpty(this._createUserWizard.PasswordRegularExpression);
				regularExpressionValidator.ValidationExpression = this._createUserWizard.PasswordRegularExpression;
				((RequiredFieldValidator)this.FindControl("EmailRequired")).Enabled = this._createUserWizard.RequireEmail;
				RegularExpressionValidator regularExpressionValidator2 = (RegularExpressionValidator)this.FindControl("EmailRegEx");
				regularExpressionValidator2.Enabled = this._createUserWizard.RequireEmail && !string.IsNullOrEmpty(this._createUserWizard.EmailRegularExpression);
				regularExpressionValidator2.ValidationExpression = this._createUserWizard.EmailRegularExpression;
				((RequiredFieldValidator)this.FindControl("QuestionRequired")).Enabled = this._createUserWizard.QuestionAndAnswerRequired;
				((RequiredFieldValidator)this.FindControl("AnswerRequired")).Enabled = this._createUserWizard.QuestionAndAnswerRequired;
			}

			// Token: 0x040018A9 RID: 6313
			private CreateUserWizard _createUserWizard;
		}

		// Token: 0x02000366 RID: 870
		private sealed class CreateUserStepTemplate : ITemplate
		{
			// Token: 0x060020B8 RID: 8376 RVA: 0x00053731 File Offset: 0x00051931
			public CreateUserStepTemplate(CreateUserWizard createUserWizard)
			{
				this._createUserWizard = createUserWizard;
			}

			// Token: 0x060020B9 RID: 8377 RVA: 0x00053740 File Offset: 0x00051940
			private TableRow CreateRow(Control c0, Control c1, Control c2, Style s0, Style s1)
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				TableCell tableCell2 = new TableCell();
				if (c0 != null)
				{
					tableCell.Controls.Add(c0);
				}
				tableRow.Controls.Add(tableCell);
				if (c1 != null && c2 != null)
				{
					tableCell2.Controls.Add(c1);
					tableCell2.Controls.Add(c2);
					tableCell.HorizontalAlign = HorizontalAlign.Right;
					if (s0 != null)
					{
						this._createUserWizard.RegisterApplyStyle(tableCell, s0);
					}
					if (s1 != null)
					{
						this._createUserWizard.RegisterApplyStyle(tableCell2, s1);
					}
					tableRow.Controls.Add(tableCell2);
				}
				else
				{
					tableCell.ColumnSpan = 2;
					tableCell.HorizontalAlign = HorizontalAlign.Center;
					if (s0 != null)
					{
						this._createUserWizard.RegisterApplyStyle(tableCell, s0);
					}
				}
				return tableRow;
			}

			// Token: 0x060020BA RID: 8378 RVA: 0x000537F4 File Offset: 0x000519F4
			public void InstantiateIn(Control container)
			{
				Table table = new Table();
				table.ControlStyle.Width = Unit.Percentage(100.0);
				table.ControlStyle.Height = Unit.Percentage(100.0);
				table.Controls.Add(this.CreateRow(null, null, null, this._createUserWizard.TitleTextStyle, null));
				table.Controls.Add(this.CreateRow(null, null, null, this._createUserWizard.InstructionTextStyle, null));
				TextBox textBox = new TextBox();
				textBox.ID = "UserName";
				this._createUserWizard.RegisterApplyStyle(textBox, this._createUserWizard.TextBoxStyle);
				Label label = new Label();
				label.AssociatedControlID = "UserName";
				RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
				requiredFieldValidator.ID = "UserNameRequired";
				requiredFieldValidator.EnableViewState = false;
				requiredFieldValidator.ControlToValidate = "UserName";
				requiredFieldValidator.Text = "*";
				requiredFieldValidator.ValidationGroup = this._createUserWizard.ID;
				this._createUserWizard.RegisterApplyStyle(requiredFieldValidator, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label, textBox, requiredFieldValidator, this._createUserWizard.LabelStyle, null));
				TextBox textBox2 = new TextBox();
				textBox2.ID = "Password";
				textBox2.TextMode = TextBoxMode.Password;
				this._createUserWizard.RegisterApplyStyle(textBox2, this._createUserWizard.TextBoxStyle);
				Label label2 = new Label();
				label2.AssociatedControlID = "Password";
				RequiredFieldValidator requiredFieldValidator2 = new RequiredFieldValidator();
				requiredFieldValidator2.ID = "PasswordRequired";
				requiredFieldValidator2.EnableViewState = false;
				requiredFieldValidator2.ControlToValidate = "Password";
				requiredFieldValidator2.Text = "*";
				requiredFieldValidator2.ValidationGroup = this._createUserWizard.ID;
				this._createUserWizard.RegisterApplyStyle(requiredFieldValidator2, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label2, textBox2, requiredFieldValidator2, this._createUserWizard.LabelStyle, null));
				table.Controls.Add(this.CreateRow(new LiteralControl(string.Empty), new LiteralControl(string.Empty), new LiteralControl(string.Empty), null, this._createUserWizard.PasswordHintStyle));
				TextBox textBox3 = new TextBox();
				textBox3.ID = "ConfirmPassword";
				textBox3.TextMode = TextBoxMode.Password;
				this._createUserWizard.RegisterApplyStyle(textBox3, this._createUserWizard.TextBoxStyle);
				Label label3 = new Label();
				label3.AssociatedControlID = "ConfirmPassword";
				RequiredFieldValidator requiredFieldValidator3 = new RequiredFieldValidator();
				requiredFieldValidator3.ID = "ConfirmPasswordRequired";
				requiredFieldValidator3.EnableViewState = false;
				requiredFieldValidator3.ControlToValidate = "ConfirmPassword";
				requiredFieldValidator3.Text = "*";
				requiredFieldValidator3.ValidationGroup = this._createUserWizard.ID;
				this._createUserWizard.RegisterApplyStyle(requiredFieldValidator3, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label3, textBox3, requiredFieldValidator3, this._createUserWizard.LabelStyle, null));
				TextBox textBox4 = new TextBox();
				textBox4.ID = "Email";
				this._createUserWizard.RegisterApplyStyle(textBox4, this._createUserWizard.TextBoxStyle);
				Label label4 = new Label();
				label4.AssociatedControlID = "Email";
				RequiredFieldValidator requiredFieldValidator4 = new RequiredFieldValidator();
				requiredFieldValidator4.ID = "EmailRequired";
				requiredFieldValidator4.EnableViewState = false;
				requiredFieldValidator4.ControlToValidate = "Email";
				requiredFieldValidator4.Text = "*";
				requiredFieldValidator4.ValidationGroup = this._createUserWizard.ID;
				this._createUserWizard.RegisterApplyStyle(requiredFieldValidator4, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label4, textBox4, requiredFieldValidator4, this._createUserWizard.LabelStyle, null));
				TextBox textBox5 = new TextBox();
				textBox5.ID = "Question";
				this._createUserWizard.RegisterApplyStyle(textBox5, this._createUserWizard.TextBoxStyle);
				Label label5 = new Label();
				label5.AssociatedControlID = "Question";
				RequiredFieldValidator requiredFieldValidator5 = new RequiredFieldValidator();
				requiredFieldValidator5.ID = "QuestionRequired";
				requiredFieldValidator5.EnableViewState = false;
				requiredFieldValidator5.ControlToValidate = "Question";
				requiredFieldValidator5.Text = "*";
				requiredFieldValidator5.ValidationGroup = this._createUserWizard.ID;
				this._createUserWizard.RegisterApplyStyle(requiredFieldValidator5, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label5, textBox5, requiredFieldValidator5, this._createUserWizard.LabelStyle, null));
				TextBox textBox6 = new TextBox();
				textBox6.ID = "Answer";
				this._createUserWizard.RegisterApplyStyle(textBox6, this._createUserWizard.TextBoxStyle);
				Label label6 = new Label();
				label6.AssociatedControlID = "Answer";
				RequiredFieldValidator requiredFieldValidator6 = new RequiredFieldValidator();
				requiredFieldValidator6.ID = "AnswerRequired";
				requiredFieldValidator6.EnableViewState = false;
				requiredFieldValidator6.ControlToValidate = "Answer";
				requiredFieldValidator6.Text = "*";
				requiredFieldValidator6.ValidationGroup = this._createUserWizard.ID;
				this._createUserWizard.RegisterApplyStyle(requiredFieldValidator6, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label6, textBox6, requiredFieldValidator6, this._createUserWizard.LabelStyle, null));
				CompareValidator compareValidator = new CompareValidator();
				compareValidator.ID = "PasswordCompare";
				compareValidator.EnableViewState = false;
				compareValidator.ControlToCompare = "Password";
				compareValidator.ControlToValidate = "ConfirmPassword";
				compareValidator.Display = ValidatorDisplay.Static;
				compareValidator.ValidationGroup = this._createUserWizard.ID;
				compareValidator.Display = ValidatorDisplay.Dynamic;
				this._createUserWizard.RegisterApplyStyle(compareValidator, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(compareValidator, null, null, null, null));
				RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
				regularExpressionValidator.ID = "PasswordRegEx";
				regularExpressionValidator.EnableViewState = false;
				regularExpressionValidator.ControlToValidate = "Password";
				regularExpressionValidator.Display = ValidatorDisplay.Static;
				regularExpressionValidator.ValidationGroup = this._createUserWizard.ID;
				regularExpressionValidator.Display = ValidatorDisplay.Dynamic;
				this._createUserWizard.RegisterApplyStyle(regularExpressionValidator, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(regularExpressionValidator, null, null, null, null));
				RegularExpressionValidator regularExpressionValidator2 = new RegularExpressionValidator();
				regularExpressionValidator2.ID = "EmailRegEx";
				regularExpressionValidator2.EnableViewState = false;
				regularExpressionValidator2.ControlToValidate = "Email";
				regularExpressionValidator2.Display = ValidatorDisplay.Static;
				regularExpressionValidator2.ValidationGroup = this._createUserWizard.ID;
				regularExpressionValidator2.Display = ValidatorDisplay.Dynamic;
				this._createUserWizard.RegisterApplyStyle(regularExpressionValidator2, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(regularExpressionValidator2, null, null, null, null));
				Label label7 = new Label();
				label7.ID = "ErrorMessage";
				label7.EnableViewState = false;
				this._createUserWizard.RegisterApplyStyle(label7, this._createUserWizard.ValidatorTextStyle);
				table.Controls.Add(this.CreateRow(label7, null, null, null, null));
				TableRow tableRow = this.CreateRow(new Image(), null, null, null, null);
				HyperLink hyperLink = new HyperLink();
				hyperLink.ID = "HelpLink";
				this._createUserWizard.RegisterApplyStyle(hyperLink, this._createUserWizard.HyperLinkStyle);
				tableRow.Cells[0].Controls.Add(hyperLink);
				tableRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
				table.Controls.Add(tableRow);
				container.Controls.Add(table);
			}

			// Token: 0x040018AA RID: 6314
			private readonly CreateUserWizard _createUserWizard;
		}

		// Token: 0x02000367 RID: 871
		private sealed class CompleteStepContainer : Wizard.DefaultContentContainer
		{
			// Token: 0x060020BB RID: 8379 RVA: 0x00053F68 File Offset: 0x00052168
			public CompleteStepContainer(CreateUserWizard createUserWizard)
				: base(createUserWizard)
			{
				this._createUserWizard = createUserWizard;
			}

			// Token: 0x060020BC RID: 8380 RVA: 0x00053F78 File Offset: 0x00052178
			protected override void UpdateState()
			{
				if (string.IsNullOrEmpty(this._createUserWizard.CompleteStep.Title))
				{
					((Table)base.InnerCell.Controls[0]).Rows[0].Visible = false;
				}
				else
				{
					((Table)base.InnerCell.Controls[0]).Rows[0].Cells[0].Text = this._createUserWizard.CompleteStep.Title;
				}
				if (string.IsNullOrEmpty(this._createUserWizard.CompleteSuccessText))
				{
					((Table)base.InnerCell.Controls[0]).Rows[1].Visible = false;
				}
				else
				{
					((Table)base.InnerCell.Controls[0]).Rows[1].Cells[0].Text = this._createUserWizard.CompleteSuccessText;
				}
				this.UpdateNavButtonState("ContinueButton" + this._createUserWizard.ContinueButtonType, this._createUserWizard.ContinueButtonText, this._createUserWizard.ContinueButtonImageUrl, this._createUserWizard.ContinueButtonStyle);
				Image image = (Image)((Table)base.InnerCell.Controls[0]).Rows[3].Cells[0].Controls[0];
				if (string.IsNullOrEmpty(this._createUserWizard.EditProfileIconUrl))
				{
					image.Visible = false;
				}
				else
				{
					image.ImageUrl = this._createUserWizard.EditProfileIconUrl;
					image.AlternateText = this._createUserWizard.EditProfileText;
				}
				HyperLink hyperLink = (HyperLink)((Table)base.InnerCell.Controls[0]).Rows[3].Cells[0].Controls[1];
				if (string.IsNullOrEmpty(this._createUserWizard.EditProfileText))
				{
					hyperLink.Visible = false;
				}
				else
				{
					hyperLink.Text = this._createUserWizard.EditProfileText;
					hyperLink.NavigateUrl = this._createUserWizard.EditProfileUrl;
				}
				((Table)base.InnerCell.Controls[0]).Rows[3].Visible = image.Visible || hyperLink.Visible;
			}

			// Token: 0x060020BD RID: 8381 RVA: 0x000541EC File Offset: 0x000523EC
			private void UpdateNavButtonState(string id, string text, string image, Style style)
			{
				WebControl webControl = (WebControl)this.FindControl(id);
				foreach (object obj in webControl.Parent.Controls)
				{
					Control control = (Control)obj;
					control.Visible = webControl == control;
				}
				((IButtonControl)webControl).Text = text;
				ImageButton imageButton = webControl as ImageButton;
				if (imageButton != null)
				{
					imageButton.ImageUrl = image;
				}
				webControl.ApplyStyle(style);
			}

			// Token: 0x040018AB RID: 6315
			private CreateUserWizard _createUserWizard;
		}

		// Token: 0x02000368 RID: 872
		private sealed class CompleteStepTemplate : ITemplate
		{
			// Token: 0x060020BE RID: 8382 RVA: 0x00054284 File Offset: 0x00052484
			public CompleteStepTemplate(CreateUserWizard createUserWizard)
			{
				this._createUserWizard = createUserWizard;
			}

			// Token: 0x060020BF RID: 8383 RVA: 0x00054294 File Offset: 0x00052494
			public void InstantiateIn(Control container)
			{
				Table table = new Table();
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ColumnSpan = 2;
				this._createUserWizard.RegisterApplyStyle(tableCell, this._createUserWizard.TitleTextStyle);
				tableRow.Cells.Add(tableCell);
				TableRow tableRow2 = new TableRow();
				TableCell tableCell2 = new TableCell();
				tableCell2.HorizontalAlign = HorizontalAlign.Center;
				this._createUserWizard.RegisterApplyStyle(tableCell2, this._createUserWizard.CompleteSuccessTextStyle);
				tableRow2.Cells.Add(tableCell2);
				TableRow tableRow3 = new TableRow();
				TableCell tableCell3 = new TableCell();
				tableCell3.HorizontalAlign = HorizontalAlign.Right;
				tableCell3.ColumnSpan = 2;
				tableRow3.Cells.Add(tableCell3);
				Control[] array = this._createUserWizard.CreateButtonSet("ContinueButton", CreateUserWizard.ContinueButtonCommandName, false, this._createUserWizard.ID);
				for (int i = 0; i < array.Length; i++)
				{
					tableCell3.Controls.Add(array[i]);
				}
				TableRow tableRow4 = new TableRow();
				TableCell tableCell4 = new TableCell();
				tableCell4.Controls.Add(new Image());
				HyperLink hyperLink = new HyperLink();
				hyperLink.ID = "EditProfileLink";
				this._createUserWizard.RegisterApplyStyle(hyperLink, this._createUserWizard.HyperLinkStyle);
				tableCell4.Controls.Add(hyperLink);
				tableRow4.Cells.Add(tableCell4);
				table.Rows.Add(tableRow);
				table.Rows.Add(tableRow2);
				table.Rows.Add(tableRow3);
				table.Rows.Add(tableRow4);
				container.Controls.Add(table);
			}

			// Token: 0x040018AC RID: 6316
			private readonly CreateUserWizard _createUserWizard;
		}
	}
}

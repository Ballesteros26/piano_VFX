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
	/// <summary>Provides user interface (UI) elements that enable a user to recover or reset a lost password and receive it in e-mail.</summary>
	// Token: 0x020003E9 RID: 1001
	[Designer("System.Web.UI.Design.WebControls.PasswordRecoveryDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Bindable(false)]
	[DefaultEvent("SendingMail")]
	public class PasswordRecovery : CompositeControl, IRenderOuterTable
	{
		/// <summary>Occurs when the user enters an incorrect answer to the password recovery confirmation question.</summary>
		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06002C2A RID: 11306 RVA: 0x000751CA File Offset: 0x000733CA
		// (remove) Token: 0x06002C2B RID: 11307 RVA: 0x000751DD File Offset: 0x000733DD
		public event EventHandler AnswerLookupError
		{
			add
			{
				this.events.AddHandler(PasswordRecovery.answerLookupErrorEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PasswordRecovery.answerLookupErrorEvent, value);
			}
		}

		/// <summary>Occurs before the user is sent a password in e-mail.</summary>
		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06002C2C RID: 11308 RVA: 0x000751F0 File Offset: 0x000733F0
		// (remove) Token: 0x06002C2D RID: 11309 RVA: 0x00075203 File Offset: 0x00073403
		public event MailMessageEventHandler SendingMail
		{
			add
			{
				this.events.AddHandler(PasswordRecovery.sendingMailEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PasswordRecovery.sendingMailEvent, value);
			}
		}

		/// <summary>Occurs when the SMTP Mail system throws an error while attempting to send an e-mail message.</summary>
		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06002C2E RID: 11310 RVA: 0x00075216 File Offset: 0x00073416
		// (remove) Token: 0x06002C2F RID: 11311 RVA: 0x00075229 File Offset: 0x00073429
		public event SendMailErrorEventHandler SendMailError
		{
			add
			{
				this.events.AddHandler(PasswordRecovery.sendMailErrorEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PasswordRecovery.sendMailErrorEvent, value);
			}
		}

		/// <summary>Occurs when the membership provider cannot find the user name entered by the user.</summary>
		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x06002C30 RID: 11312 RVA: 0x0007523C File Offset: 0x0007343C
		// (remove) Token: 0x06002C31 RID: 11313 RVA: 0x0007524F File Offset: 0x0007344F
		public event EventHandler UserLookupError
		{
			add
			{
				this.events.AddHandler(PasswordRecovery.userLookupErrorEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PasswordRecovery.userLookupErrorEvent, value);
			}
		}

		/// <summary>Occurs when the user has submitted an answer to the password recovery confirmation question.</summary>
		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06002C32 RID: 11314 RVA: 0x00075262 File Offset: 0x00073462
		// (remove) Token: 0x06002C33 RID: 11315 RVA: 0x00075275 File Offset: 0x00073475
		public event LoginCancelEventHandler VerifyingAnswer
		{
			add
			{
				this.events.AddHandler(PasswordRecovery.verifyingAnswerEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PasswordRecovery.verifyingAnswerEvent, value);
			}
		}

		/// <summary>Occurs before the user name is validated by the membership provider.</summary>
		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06002C34 RID: 11316 RVA: 0x00075288 File Offset: 0x00073488
		// (remove) Token: 0x06002C35 RID: 11317 RVA: 0x0007529B File Offset: 0x0007349B
		public event LoginCancelEventHandler VerifyingUser
		{
			add
			{
				this.events.AddHandler(PasswordRecovery.verifyingUserEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PasswordRecovery.verifyingUserEvent, value);
			}
		}

		/// <summary>Gets the answer to the password recovery confirmation question entered by the user.</summary>
		/// <returns>The answer to the password recovery confirmation question entered by the user.</returns>
		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x000752C8 File Offset: 0x000734C8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[Browsable(false)]
		[Filterable(false)]
		public virtual string Answer
		{
			get
			{
				if (this._answer == null)
				{
					return string.Empty;
				}
				return this._answer;
			}
		}

		/// <summary>Gets or sets the label text for the password confirmation answer text box.</summary>
		/// <returns>The label for the password confirmation answer text box. The default is "Answer:" </returns>
		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000752DE File Offset: 0x000734DE
		// (set) Token: 0x06002C39 RID: 11321 RVA: 0x000752F5 File Offset: 0x000734F5
		[Localizable(true)]
		public virtual string AnswerLabelText
		{
			get
			{
				return this.ViewState.GetString("AnswerLabelText", "Answer:");
			}
			set
			{
				this.ViewState["AnswerLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the error message displayed to the user when the Answer text box is blank.</summary>
		/// <returns>The error message displayed when the Answer text box is empty. The default is "Answer." </returns>
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x00075308 File Offset: 0x00073508
		// (set) Token: 0x06002C3B RID: 11323 RVA: 0x0007531F File Offset: 0x0007351F
		[Localizable(true)]
		public virtual string AnswerRequiredErrorMessage
		{
			get
			{
				return this.ViewState.GetString("AnswerRequiredErrorMessage", "Answer is required.");
			}
			set
			{
				this.ViewState["AnswerRequiredErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the amount of padding inside the borders of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>The amount of space (in pixels) between the contents of a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control and the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control's border. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.BorderPadding" /> property is set to less than -1.</exception>
		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x0004C1D1 File Offset: 0x0004A3D1
		// (set) Token: 0x06002C3D RID: 11325 RVA: 0x0004C1E4 File Offset: 0x0004A3E4
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

		/// <summary>Gets or sets the error message to display when there is a problem with the membership provider for the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>The error message displayed when the user's password will not be sent by e-mail because of a problem with the membership provider. The default is "Your attempt to retrieve your password has failed. Please try again." </returns>
		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x00075332 File Offset: 0x00073532
		// (set) Token: 0x06002C3F RID: 11327 RVA: 0x00075349 File Offset: 0x00073549
		[Localizable(true)]
		public virtual string GeneralFailureText
		{
			get
			{
				return this.ViewState.GetString("GeneralFailureText", "Your attempt to retrieve your password was not successful. Please try again.");
			}
			set
			{
				this.ViewState["GeneralFailureText"] = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to display next to the link to the Help page.</summary>
		/// <returns>The URL of an image to display next to the link to the Help page. The default value is an empty string ("").</returns>
		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x0004C729 File Offset: 0x0004A929
		// (set) Token: 0x06002C41 RID: 11329 RVA: 0x0004C740 File Offset: 0x0004A940
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the text of the link to the password recovery Help page.</summary>
		/// <returns>The text of the link to the password recovery Help page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x0004C753 File Offset: 0x0004A953
		// (set) Token: 0x06002C43 RID: 11331 RVA: 0x0004C76A File Offset: 0x0004A96A
		[Localizable(true)]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the URL of the password recovery Help page.</summary>
		/// <returns>The URL of the password recovery Help page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x0004C77D File Offset: 0x0004A97D
		// (set) Token: 0x06002C45 RID: 11333 RVA: 0x0004C794 File Offset: 0x0004A994
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

		/// <summary>Gets a reference to a collection of properties that define the characteristics of e-mail messages used to send new or recovered passwords to users.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> that contains properties that define the characteristics of e-mail messages used to send users their passwords.</returns>
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x0007535C File Offset: 0x0007355C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Themeable(false)]
		[NotifyParentProperty(true)]
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

		/// <summary>Gets or sets the membership provider used to look up user information.</summary>
		/// <returns>The membership provider used to look up user information. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x0007538A File Offset: 0x0007358A
		// (set) Token: 0x06002C48 RID: 11336 RVA: 0x000753A1 File Offset: 0x000735A1
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string MembershipProvider
		{
			get
			{
				return this.ViewState.GetString("MembershipProvider", string.Empty);
			}
			set
			{
				this.ViewState["MembershipProvider"] = value;
			}
		}

		/// <summary>Gets the password recovery confirmation question established by the user on the Web site.</summary>
		/// <returns>The password recovery confirmation question. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x000753B4 File Offset: 0x000735B4
		// (set) Token: 0x06002C4A RID: 11338 RVA: 0x000753CB File Offset: 0x000735CB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[Filterable(false)]
		public virtual string Question
		{
			get
			{
				return this.ViewState.GetString("Question", "");
			}
			private set
			{
				this.ViewState["Question"] = value;
			}
		}

		/// <summary>Gets or sets the text to display when the user's answer to the password recovery confirmation question does not match the answer stored in the Web site data store.</summary>
		/// <returns>The text to display when the user's answer to the password recovery confirmation question does not match the answer stored in the Web site data store. The default value is "Your answer could not be verified. Please try again." </returns>
		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x000753DE File Offset: 0x000735DE
		// (set) Token: 0x06002C4C RID: 11340 RVA: 0x000753F5 File Offset: 0x000735F5
		[Localizable(true)]
		public virtual string QuestionFailureText
		{
			get
			{
				return this.ViewState.GetString("QuestionFailureText", "Your answer could not be verified. Please try again.");
			}
			set
			{
				this.ViewState["QuestionFailureText"] = value;
			}
		}

		/// <summary>Gets or sets the text to display in the Question view to instruct the user to answer the password recovery confirmation question.</summary>
		/// <returns>The instruction text to display in the Question view. The default is "Answer the following question to receive your password." </returns>
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06002C4D RID: 11341 RVA: 0x00075408 File Offset: 0x00073608
		// (set) Token: 0x06002C4E RID: 11342 RVA: 0x0007541F File Offset: 0x0007361F
		[Localizable(true)]
		public virtual string QuestionInstructionText
		{
			get
			{
				return this.ViewState.GetString("QuestionInstructionText", "Answer the following question to receive your password.");
			}
			set
			{
				this.ViewState["QuestionInstructionText"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.Question" /> text box.</summary>
		/// <returns>The label for the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.Question" /> text box. The default is "Question:" </returns>
		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x00075432 File Offset: 0x00073632
		// (set) Token: 0x06002C50 RID: 11344 RVA: 0x00075449 File Offset: 0x00073649
		[Localizable(true)]
		public virtual string QuestionLabelText
		{
			get
			{
				return this.ViewState.GetString("QuestionLabelText", "Question:");
			}
			set
			{
				this.ViewState["QuestionLabelText"] = value;
			}
		}

		/// <summary>Gets or sets the title for the Question view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>The title for the Question view. The default is "Identity Confirmation".</returns>
		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x0007545C File Offset: 0x0007365C
		// (set) Token: 0x06002C52 RID: 11346 RVA: 0x00075473 File Offset: 0x00073673
		[Localizable(true)]
		public virtual string QuestionTitleText
		{
			get
			{
				return this.ViewState.GetString("QuestionTitleText", "Identity Confirmation");
			}
			set
			{
				this.ViewState["QuestionTitleText"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control encloses rendered HTML in a table element in order to apply inline styles.</summary>
		/// <returns>true if the control encloses rendered HTML in a table element; otherwise, false. The default is true.</returns>
		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06002C53 RID: 11347 RVA: 0x00075486 File Offset: 0x00073686
		// (set) Token: 0x06002C54 RID: 11348 RVA: 0x0007548E File Offset: 0x0007368E
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

		/// <summary>Gets or sets the URL of an image to use as the Submit button.</summary>
		/// <returns>The URL of the image to use as the Submit button. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06002C55 RID: 11349 RVA: 0x00075497 File Offset: 0x00073697
		// (set) Token: 0x06002C56 RID: 11350 RVA: 0x000754AE File Offset: 0x000736AE
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string SubmitButtonImageUrl
		{
			get
			{
				return this.ViewState.GetString("SubmitButtonImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["SubmitButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the text of the button that submits the form.</summary>
		/// <returns>The text of the button. The default is "Submit".</returns>
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x000754C1 File Offset: 0x000736C1
		// (set) Token: 0x06002C58 RID: 11352 RVA: 0x000754D8 File Offset: 0x000736D8
		[Localizable(true)]
		public virtual string SubmitButtonText
		{
			get
			{
				return this.ViewState.GetString("SubmitButtonText", "Submit");
			}
			set
			{
				this.ViewState["SubmitButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of Submit button to use when rendering the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PasswordRecovery.SubmitButtonType" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.ButtonType" /> value. </exception>
		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x000754EC File Offset: 0x000736EC
		// (set) Token: 0x06002C5A RID: 11354 RVA: 0x00075515 File Offset: 0x00073715
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType SubmitButtonType
		{
			get
			{
				object obj = this.ViewState["SubmitButtonType"];
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
					throw new ArgumentOutOfRangeException("SubmitButtonType");
				}
				this.ViewState["SubmitButtonType"] = (int)value;
			}
		}

		/// <summary>Gets or sets the URL of the page to display after sending a password successfully.</summary>
		/// <returns>The URL of the password success page. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x0004CAE1 File Offset: 0x0004ACE1
		// (set) Token: 0x06002C5C RID: 11356 RVA: 0x0004CAF8 File Offset: 0x0004ACF8
		[Themeable(false)]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
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

		/// <summary>Gets or sets the text to display after sending a password successfully.</summary>
		/// <returns>The text to display when a password has been successfully sent. The default is "Your password has been sent to you." </returns>
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x00075540 File Offset: 0x00073740
		// (set) Token: 0x06002C5E RID: 11358 RVA: 0x0004CB4F File Offset: 0x0004AD4F
		[Localizable(true)]
		public virtual string SuccessText
		{
			get
			{
				return this.ViewState.GetString("SuccessText", "Your password has been sent to you.");
			}
			set
			{
				this.ViewState["SuccessText"] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether to display the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control in a horizontal or vertical layout.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.LoginTextLayout.TextOnLeft" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PasswordRecovery.TextLayout" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.LoginTextLayout" /> enumeration value. </exception>
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x00075558 File Offset: 0x00073758
		// (set) Token: 0x06002C60 RID: 11360 RVA: 0x00068CD5 File Offset: 0x00066ED5
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

		/// <summary>Gets or sets the text that appears in the User Name text box.</summary>
		/// <returns>The user name entered by the user. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x00075581 File Offset: 0x00073781
		// (set) Token: 0x06002C62 RID: 11362 RVA: 0x00075597 File Offset: 0x00073797
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string UserName
		{
			get
			{
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

		/// <summary>Gets or sets the text displayed when the user name entered by the user is not a valid user name for the Web site.</summary>
		/// <returns>The text displayed when the user name entered by the user is not a valid user name for the Web site. The default is "We were unable to access your information. Please try again." </returns>
		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000755A0 File Offset: 0x000737A0
		// (set) Token: 0x06002C64 RID: 11364 RVA: 0x000755B7 File Offset: 0x000737B7
		[Localizable(true)]
		public virtual string UserNameFailureText
		{
			get
			{
				return this.ViewState.GetString("UserNameFailureText", "We were unable to access your information. Please try again.");
			}
			set
			{
				this.ViewState["UserNameFailureText"] = value;
			}
		}

		/// <summary>Gets or sets the text to display in the UserName view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control to instruct the user to enter a user name.</summary>
		/// <returns>The instruction text to display in the UserName view. The default is "Enter your user name to receive your password." </returns>
		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x000755CA File Offset: 0x000737CA
		// (set) Token: 0x06002C66 RID: 11366 RVA: 0x000755E1 File Offset: 0x000737E1
		[Localizable(true)]
		public virtual string UserNameInstructionText
		{
			get
			{
				return this.ViewState.GetString("UserNameInstructionText", "Enter your User Name to receive your password.");
			}
			set
			{
				this.ViewState["UserNameInstructionText"] = value;
			}
		}

		/// <summary>Gets or sets the text of the label for the User Name text box.</summary>
		/// <returns>The label for the User Name text box. The default is "User Name:".</returns>
		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x0004CC75 File Offset: 0x0004AE75
		// (set) Token: 0x06002C68 RID: 11368 RVA: 0x0004CC8C File Offset: 0x0004AE8C
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

		/// <summary>Gets or sets the error message displayed when a user leaves the User Name text box empty.</summary>
		/// <returns>The error message displayed when the User Name text box is empty. The default is "User Name".</returns>
		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x0004CC9F File Offset: 0x0004AE9F
		// (set) Token: 0x06002C6A RID: 11370 RVA: 0x0004CCB6 File Offset: 0x0004AEB6
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

		/// <summary>Gets or sets the title for the UserName view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>The title for the UserName view. The default is "Forgot Your Password?" </returns>
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x000755F4 File Offset: 0x000737F4
		// (set) Token: 0x06002C6C RID: 11372 RVA: 0x0007560B File Offset: 0x0007380B
		[Localizable(true)]
		public virtual string UserNameTitleText
		{
			get
			{
				return this.ViewState.GetString("UserNameTitleText", "Forgot Your Password?");
			}
			set
			{
				this.ViewState["UserNameTitleText"] = value;
			}
		}

		/// <summary>Gets or sets the template used to display the Question view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control in Question view. The default is null.</returns>
		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x0007561E File Offset: 0x0007381E
		// (set) Token: 0x06002C6E RID: 11374 RVA: 0x00075626 File Offset: 0x00073826
		[TemplateContainer(typeof(PasswordRecovery))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate QuestionTemplate
		{
			get
			{
				return this._questionTemplate;
			}
			set
			{
				this._questionTemplate = value;
			}
		}

		/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control used to create an instance of the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.QuestionTemplate" /> template. This property provides programmatic access to child controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.PasswordRecovery.QuestionTemplate" /> template.</returns>
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x00075630 File Offset: 0x00073830
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Control QuestionTemplateContainer
		{
			get
			{
				if (this._questionTemplateContainer == null)
				{
					this._questionTemplateContainer = new PasswordRecovery.QuestionContainer(this);
					ITemplate questionTemplate = this.QuestionTemplate;
					if (questionTemplate != null)
					{
						this._questionTemplateContainer.InstantiateTemplate(questionTemplate);
					}
				}
				return this._questionTemplateContainer;
			}
		}

		/// <summary>Gets or sets the template used to display the Success view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control in Success view. The default is null.</returns>
		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x0007566D File Offset: 0x0007386D
		// (set) Token: 0x06002C71 RID: 11377 RVA: 0x00075675 File Offset: 0x00073875
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateContainer(typeof(PasswordRecovery))]
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

		/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control used to create an instance of the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.SuccessTemplate" /> template. This property provides programmatic access to child controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.PasswordRecovery.SuccessTemplate" />.</returns>
		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x00075680 File Offset: 0x00073880
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control SuccessTemplateContainer
		{
			get
			{
				if (this._successTemplateContainer == null)
				{
					this._successTemplateContainer = new PasswordRecovery.SuccessContainer(this);
					ITemplate successTemplate = this.SuccessTemplate;
					if (successTemplate != null)
					{
						this._successTemplateContainer.InstantiateTemplate(successTemplate);
					}
				}
				return this._successTemplateContainer;
			}
		}

		/// <summary>Gets or sets the template used to display the UserName view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control in UserName view. The default is null.</returns>
		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000756BD File Offset: 0x000738BD
		// (set) Token: 0x06002C74 RID: 11380 RVA: 0x000756C5 File Offset: 0x000738C5
		[TemplateContainer(typeof(PasswordRecovery))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate UserNameTemplate
		{
			get
			{
				return this._userNameTemplate;
			}
			set
			{
				this._userNameTemplate = value;
			}
		}

		/// <summary>Gets the container that a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control used to create an instance of the <see cref="P:System.Web.UI.WebControls.PasswordRecovery.UserNameTemplate" /> template. This property provides programmatic access to child controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that contains a <see cref="P:System.Web.UI.WebControls.PasswordRecovery.UserNameTemplate" />.</returns>
		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x000756D0 File Offset: 0x000738D0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Control UserNameTemplateContainer
		{
			get
			{
				if (this._userNameTemplateContainer == null)
				{
					this._userNameTemplateContainer = new PasswordRecovery.UserNameContainer(this);
					ITemplate userNameTemplate = this.UserNameTemplate;
					if (userNameTemplate != null)
					{
						this._userNameTemplateContainer.InstantiateTemplate(userNameTemplate);
					}
				}
				return this._userNameTemplateContainer;
			}
		}

		/// <summary>Gets a reference to a collection of properties that define the appearance of error text in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of error text.</returns>
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x0007570D File Offset: 0x0007390D
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		/// <summary>Gets a reference to a collection of properties that define the appearance of hyperlinks on the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the settings that define the appearance of hyperlinks.</returns>
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x0007573B File Offset: 0x0007393B
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

		/// <summary>Gets a reference to a collection of style properties that define the appearance of explanatory text in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of explanatory text.</returns>
		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x00075769 File Offset: 0x00073969
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		/// <summary>Gets a reference to a collection of style properties that define the appearance of text box labels in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of text box labels.</returns>
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x00075797 File Offset: 0x00073997
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
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

		/// <summary>Gets a reference to a collection of properties that define the appearance of Submit buttons in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that contains properties that define the appearance of the Submit buttons.</returns>
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000757C5 File Offset: 0x000739C5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style SubmitButtonStyle
		{
			get
			{
				if (this._submitButtonStyle == null)
				{
					this._submitButtonStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._submitButtonStyle.TrackViewState();
					}
				}
				return this._submitButtonStyle;
			}
		}

		/// <summary>Gets a reference to a collection of style properties that define the appearance of text displayed in the Success view of the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of text displayed in the Success view.</returns>
		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000757F3 File Offset: 0x000739F3
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		/// <summary>Gets a reference to a collection of style properties that define the appearance of text boxes in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.Style" /> that contains properties that define the appearance of text boxes in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</returns>
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x00075821 File Offset: 0x00073A21
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style TextBoxStyle
		{
			get
			{
				if (this._textBoxStyle == null)
				{
					this._textBoxStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._textBoxStyle.TrackViewState();
					}
				}
				return this._textBoxStyle;
			}
		}

		/// <summary>Gets a reference to a collection of style properties that define the appearance of title text that appears in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains properties that define the appearance of title text in the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</returns>
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x0007584F File Offset: 0x00073A4F
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
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

		/// <summary>Gets a reference to a collection of <see cref="T:System.Web.UI.WebControls.Style" /> properties that define the appearance of error messages that are associated with any input validation used by the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the appearance of error messages that are associated with any input validation used by the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. The default is null.</returns>
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x0007587D File Offset: 0x00073A7D
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public Style ValidatorTextStyle
		{
			get
			{
				if (this._validatorTextStyle == null)
				{
					this._validatorTextStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this._validatorTextStyle.TrackViewState();
					}
				}
				return this._validatorTextStyle;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to a <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. </summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. Always returns HtmlTextWriterTag.Table.</returns>
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x0004D090 File Offset: 0x0004B290
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000758AB File Offset: 0x00073AAB
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

		/// <summary>Creates the individual controls that make up the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control.</summary>
		// Token: 0x06002C81 RID: 11393 RVA: 0x000758C4 File Offset: 0x00073AC4
		protected internal override void CreateChildControls()
		{
			if (this.UserNameTemplate == null)
			{
				ITemplate template = new PasswordRecovery.UserNameDefaultTemplate(this);
				((PasswordRecovery.UserNameContainer)this.UserNameTemplateContainer).InstantiateTemplate(template);
			}
			if (this.QuestionTemplate == null)
			{
				ITemplate template2 = new PasswordRecovery.QuestionDefaultTemplate(this);
				((PasswordRecovery.QuestionContainer)this.QuestionTemplateContainer).InstantiateTemplate(template2);
			}
			if (this.SuccessTemplate == null)
			{
				ITemplate template3 = new PasswordRecovery.SuccessDefaultTemplate(this);
				((PasswordRecovery.SuccessContainer)this.SuccessTemplateContainer).InstantiateTemplate(template3);
			}
			this.Controls.AddAt(0, this.UserNameTemplateContainer);
			this.Controls.AddAt(1, this.QuestionTemplateContainer);
			this.Controls.AddAt(2, this.SuccessTemplateContainer);
			IEditableTextControl editableTextControl = ((PasswordRecovery.UserNameContainer)this.UserNameTemplateContainer).UserNameTextBox;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.UserName_TextChanged;
			}
			editableTextControl = ((PasswordRecovery.QuestionContainer)this.QuestionTemplateContainer).AnswerTextBox;
			if (editableTextControl != null)
			{
				editableTextControl.TextChanged += this.Answer_TextChanged;
			}
		}

		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
		// Token: 0x06002C82 RID: 11394 RVA: 0x000759BC File Offset: 0x00073BBC
		protected internal override void Render(HtmlTextWriter writer)
		{
			((PasswordRecovery.QuestionContainer)this.QuestionTemplateContainer).UpdateChildControls();
			for (int i = 0; i < this.Controls.Count; i++)
			{
				if (this.Controls[i].Visible)
				{
					this.Controls[i].Render(writer);
				}
			}
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.LoadControlState(System.Object)" /> method.</summary>
		/// <param name="savedState">An object that represents the control state to be restored.</param>
		// Token: 0x06002C83 RID: 11395 RVA: 0x00075A14 File Offset: 0x00073C14
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			this._currentStep = (PasswordRecovery.PasswordReciveryStep)array[1];
			this._username = (string)array[2];
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.SaveControlState" /> method.</summary>
		/// <returns>Returns the server control's current state. If there is no state associated with the control, this method returns null.</returns>
		// Token: 0x06002C84 RID: 11396 RVA: 0x00075A54 File Offset: 0x00073C54
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			return new object[] { obj, this._currentStep, this._username };
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.TrackViewState" /> method.</summary>
		// Token: 0x06002C85 RID: 11397 RVA: 0x00075A8C File Offset: 0x00073C8C
		protected override void TrackViewState()
		{
			base.TrackViewState();
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
			if (this._submitButtonStyle != null)
			{
				this._submitButtonStyle.TrackViewState();
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

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.LoadViewState(System.Object)" /> method.</summary>
		/// <param name="savedState">An object that represents the control state to restore.</param>
		/// <exception cref="T:System.ArgumentException">The view state is invalid.</exception>
		// Token: 0x06002C86 RID: 11398 RVA: 0x00075B60 File Offset: 0x00073D60
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
				this.FailureTextStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.HyperLinkStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.InstructionTextStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				this.LabelStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				this.SubmitButtonStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				this.SuccessTextStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				this.TextBoxStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				this.TitleTextStyle.LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				this.ValidatorTextStyle.LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.MailDefinition).LoadViewState(array[10]);
			}
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.SaveViewState" /> method.</summary>
		/// <returns>An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, null.</returns>
		// Token: 0x06002C87 RID: 11399 RVA: 0x00075C44 File Offset: 0x00073E44
		protected override object SaveViewState()
		{
			object[] array = new object[11];
			array[0] = base.SaveViewState();
			if (this._failureTextStyle != null)
			{
				array[1] = this._failureTextStyle.SaveViewState();
			}
			if (this._hyperLinkStyle != null)
			{
				array[2] = this._hyperLinkStyle.SaveViewState();
			}
			if (this._instructionTextStyle != null)
			{
				array[3] = this._instructionTextStyle.SaveViewState();
			}
			if (this._labelStyle != null)
			{
				array[4] = this._labelStyle.SaveViewState();
			}
			if (this._submitButtonStyle != null)
			{
				array[5] = this._submitButtonStyle.SaveViewState();
			}
			if (this._successTextStyle != null)
			{
				array[6] = this._successTextStyle.SaveViewState();
			}
			if (this._textBoxStyle != null)
			{
				array[7] = this._textBoxStyle.SaveViewState();
			}
			if (this._titleTextStyle != null)
			{
				array[8] = this._titleTextStyle.SaveViewState();
			}
			if (this._validatorTextStyle != null)
			{
				array[9] = this._validatorTextStyle.SaveViewState();
			}
			if (this._mailDefinition != null)
			{
				array[10] = ((IStateManager)this._mailDefinition).SaveViewState();
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

		// Token: 0x06002C88 RID: 11400 RVA: 0x00075D58 File Offset: 0x00073F58
		private void ProcessCommand(CommandEventArgs args)
		{
			if (!this.Page.IsValid)
			{
				return;
			}
			PasswordRecovery.PasswordReciveryStep currentStep = this._currentStep;
			if (currentStep == PasswordRecovery.PasswordReciveryStep.StepUserName)
			{
				this.ProcessUserName();
				return;
			}
			if (currentStep != PasswordRecovery.PasswordReciveryStep.StepAnswer)
			{
				return;
			}
			this.ProcessUserAnswer();
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x00075D90 File Offset: 0x00073F90
		private void ProcessUserName()
		{
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnVerifyingUser(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return;
			}
			MembershipUser user = this.MembershipProviderInternal.GetUser(this.UserName, false);
			if (user == null)
			{
				this.OnUserLookupError(EventArgs.Empty);
				((PasswordRecovery.UserNameContainer)this.UserNameTemplateContainer).FailureTextLiteral.Text = this.UserNameFailureText;
				return;
			}
			if (!this.MembershipProviderInternal.RequiresQuestionAndAnswer)
			{
				this.GenerateAndSendEmail();
				this._currentStep = PasswordRecovery.PasswordReciveryStep.StepSuccess;
				return;
			}
			this.Question = user.PasswordQuestion;
			this._currentStep = PasswordRecovery.PasswordReciveryStep.StepAnswer;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x00075E20 File Offset: 0x00074020
		private void ProcessUserAnswer()
		{
			LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs();
			this.OnVerifyingAnswer(loginCancelEventArgs);
			if (loginCancelEventArgs.Cancel)
			{
				return;
			}
			MembershipUser user = this.MembershipProviderInternal.GetUser(this.UserName, false);
			if (user == null || string.IsNullOrEmpty(user.Email))
			{
				((PasswordRecovery.QuestionContainer)this.QuestionTemplateContainer).FailureTextLiteral.Text = this.GeneralFailureText;
				return;
			}
			this.GenerateAndSendEmail();
			this._currentStep = PasswordRecovery.PasswordReciveryStep.StepSuccess;
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x00075E90 File Offset: 0x00074090
		private void GenerateAndSendEmail()
		{
			string text = "";
			try
			{
				if (this.MembershipProviderInternal.EnablePasswordRetrieval)
				{
					text = this.MembershipProviderInternal.GetPassword(this.UserName, this.Answer);
				}
				else
				{
					if (!this.MembershipProviderInternal.EnablePasswordReset)
					{
						throw new HttpException("Membership provider does not support password retrieval or reset.");
					}
					text = this.MembershipProviderInternal.ResetPassword(this.UserName, this.Answer);
				}
			}
			catch (MembershipPasswordException)
			{
				this.OnAnswerLookupError(EventArgs.Empty);
				((PasswordRecovery.QuestionContainer)this.QuestionTemplateContainer).FailureTextLiteral.Text = this.QuestionFailureText;
				return;
			}
			this.SendPasswordByMail(this.UserName, text);
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x00075F44 File Offset: 0x00074144
		private void InitMemberShipProvider()
		{
			string membershipProvider = this.MembershipProvider;
			this._provider = ((membershipProvider.Length == 0) ? (this._provider = Membership.Provider) : Membership.Providers[membershipProvider]);
			if (this._provider == null)
			{
				throw new HttpException(global::Locale.GetText("No provider named '{0}' could be found.", new object[] { membershipProvider }));
			}
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x00075FA4 File Offset: 0x000741A4
		private void SendPasswordByMail(string username, string password)
		{
			MembershipUser user = this.MembershipProviderInternal.GetUser(this.UserName, false);
			if (user == null)
			{
				return;
			}
			string text = "Please return to the site and log in using the following information.\nUser Name: <%USERNAME%>\nPassword: <%PASSWORD%>\n";
			ListDictionary listDictionary = new ListDictionary(StringComparer.OrdinalIgnoreCase);
			listDictionary.Add("<%USERNAME%>", username);
			listDictionary.Add("<% UserName %>", username);
			listDictionary.Add("<%PASSWORD%>", password);
			listDictionary.Add("<% Password %>", password);
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
				mailMessage.Subject = "Password";
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

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.AnswerLookupError" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002C8E RID: 11406 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void OnAnswerLookupError(EventArgs e)
		{
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.OnBubbleEvent(System.Object,System.EventArgs)" /> method.</summary>
		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">The event data. </param>
		// Token: 0x06002C8F RID: 11407 RVA: 0x000760B4 File Offset: 0x000742B4
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (e != null && commandEventArgs.CommandName == PasswordRecovery.SubmitButtonCommandName)
			{
				this.ProcessCommand(commandEventArgs);
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.OnInit(System.EventArgs)" /> method.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06002C90 RID: 11408 RVA: 0x0004D60A File Offset: 0x0004B80A
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.RegisterRequiresControlState(this);
			base.OnInit(e);
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06002C91 RID: 11409 RVA: 0x000760F0 File Offset: 0x000742F0
		protected internal override void OnPreRender(EventArgs e)
		{
			this.UserNameTemplateContainer.Visible = false;
			this.QuestionTemplateContainer.Visible = false;
			this.SuccessTemplateContainer.Visible = false;
			switch (this._currentStep)
			{
			case PasswordRecovery.PasswordReciveryStep.StepUserName:
				this.UserNameTemplateContainer.Visible = true;
				break;
			case PasswordRecovery.PasswordReciveryStep.StepAnswer:
				this.QuestionTemplateContainer.Visible = true;
				break;
			case PasswordRecovery.PasswordReciveryStep.StepSuccess:
				this.SuccessTemplateContainer.Visible = true;
				break;
			}
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.SendingMail" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> that contains the event data. </param>
		// Token: 0x06002C92 RID: 11410 RVA: 0x0007616C File Offset: 0x0007436C
		protected virtual void OnSendingMail(MailMessageEventArgs e)
		{
			MailMessageEventHandler mailMessageEventHandler = this.events[PasswordRecovery.sendingMailEvent] as MailMessageEventHandler;
			if (mailMessageEventHandler != null)
			{
				mailMessageEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.SendMailError" /> event when an e-mail message cannot be sent to the user.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> that contains the event data.</param>
		// Token: 0x06002C93 RID: 11411 RVA: 0x0007619C File Offset: 0x0007439C
		protected virtual void OnSendMailError(SendMailErrorEventArgs e)
		{
			SendMailErrorEventHandler sendMailErrorEventHandler = this.events[PasswordRecovery.sendingMailEvent] as SendMailErrorEventHandler;
			if (sendMailErrorEventHandler != null)
			{
				sendMailErrorEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.UserLookupError" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002C94 RID: 11412 RVA: 0x000761CC File Offset: 0x000743CC
		protected virtual void OnUserLookupError(EventArgs e)
		{
			EventHandler eventHandler = this.events[PasswordRecovery.userLookupErrorEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.VerifyingAnswer" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06002C95 RID: 11413 RVA: 0x000761FC File Offset: 0x000743FC
		protected virtual void OnVerifyingAnswer(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = this.events[PasswordRecovery.verifyingAnswerEvent] as LoginCancelEventHandler;
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.PasswordRecovery.VerifyingUser" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06002C96 RID: 11414 RVA: 0x0007622C File Offset: 0x0007442C
		protected virtual void OnVerifyingUser(LoginCancelEventArgs e)
		{
			LoginCancelEventHandler loginCancelEventHandler = this.events[PasswordRecovery.verifyingUserEvent] as LoginCancelEventHandler;
			if (loginCancelEventHandler != null)
			{
				loginCancelEventHandler(this, e);
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x0007625A File Offset: 0x0007445A
		private void UserName_TextChanged(object sender, EventArgs e)
		{
			this.UserName = ((ITextControl)sender).Text;
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x0007626D File Offset: 0x0007446D
		private void Answer_TextChanged(object sender, EventArgs e)
		{
			this._answer = ((ITextControl)sender).Text;
		}

		/// <summary>Implements the base <see cref="M:System.Web.UI.Control.System#Web#UI#IControlDesignerAccessor#SetDesignModeState(System.Collections.IDictionary)" /> method.</summary>
		/// <param name="data">The design-time data for the control.</param>
		// Token: 0x06002C99 RID: 11417 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001B39 RID: 6969
		private static readonly object answerLookupErrorEvent = new object();

		// Token: 0x04001B3A RID: 6970
		private static readonly object sendingMailEvent = new object();

		// Token: 0x04001B3B RID: 6971
		private static readonly object sendMailErrorEvent = new object();

		// Token: 0x04001B3C RID: 6972
		private static readonly object userLookupErrorEvent = new object();

		// Token: 0x04001B3D RID: 6973
		private static readonly object verifyingAnswerEvent = new object();

		// Token: 0x04001B3E RID: 6974
		private static readonly object verifyingUserEvent = new object();

		/// <summary>Represents the command to perform when the Submit button is clicked.</summary>
		// Token: 0x04001B3F RID: 6975
		public static readonly string SubmitButtonCommandName = "Submit";

		// Token: 0x04001B40 RID: 6976
		private bool renderOuterTable = true;

		// Token: 0x04001B41 RID: 6977
		private TableItemStyle _failureTextStyle;

		// Token: 0x04001B42 RID: 6978
		private TableItemStyle _hyperLinkStyle;

		// Token: 0x04001B43 RID: 6979
		private TableItemStyle _instructionTextStyle;

		// Token: 0x04001B44 RID: 6980
		private TableItemStyle _labelStyle;

		// Token: 0x04001B45 RID: 6981
		private Style _submitButtonStyle;

		// Token: 0x04001B46 RID: 6982
		private TableItemStyle _successTextStyle;

		// Token: 0x04001B47 RID: 6983
		private Style _textBoxStyle;

		// Token: 0x04001B48 RID: 6984
		private TableItemStyle _titleTextStyle;

		// Token: 0x04001B49 RID: 6985
		private Style _validatorTextStyle;

		// Token: 0x04001B4A RID: 6986
		private MailDefinition _mailDefinition;

		// Token: 0x04001B4B RID: 6987
		private MembershipProvider _provider;

		// Token: 0x04001B4C RID: 6988
		private ITemplate _questionTemplate;

		// Token: 0x04001B4D RID: 6989
		private ITemplate _successTemplate;

		// Token: 0x04001B4E RID: 6990
		private ITemplate _userNameTemplate;

		// Token: 0x04001B4F RID: 6991
		private PasswordRecovery.QuestionContainer _questionTemplateContainer;

		// Token: 0x04001B50 RID: 6992
		private PasswordRecovery.SuccessContainer _successTemplateContainer;

		// Token: 0x04001B51 RID: 6993
		private PasswordRecovery.UserNameContainer _userNameTemplateContainer;

		// Token: 0x04001B52 RID: 6994
		private PasswordRecovery.PasswordReciveryStep _currentStep;

		// Token: 0x04001B53 RID: 6995
		private string _username;

		// Token: 0x04001B54 RID: 6996
		private string _answer;

		// Token: 0x04001B55 RID: 6997
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x020003EA RID: 1002
		private abstract class BasePasswordRecoveryContainer : Control, INamingContainer
		{
			// Token: 0x06002C9B RID: 11419 RVA: 0x000762D3 File Offset: 0x000744D3
			public BasePasswordRecoveryContainer(PasswordRecovery owner)
			{
				this._owner = owner;
				this.renderOuterTable = this._owner.RenderOuterTable;
				if (this.renderOuterTable)
				{
					this.InitTable();
				}
			}

			// Token: 0x06002C9C RID: 11420 RVA: 0x00076301 File Offset: 0x00074501
			public void InstantiateTemplate(ITemplate template)
			{
				if (!this.renderOuterTable)
				{
					template.InstantiateIn(this);
					return;
				}
				template.InstantiateIn(this._containerCell);
			}

			// Token: 0x06002C9D RID: 11421 RVA: 0x00076320 File Offset: 0x00074520
			private void InitTable()
			{
				this._table = new Table();
				string id = this._owner.ID;
				if (!string.IsNullOrEmpty(id))
				{
					this._table.Attributes.Add("id", id);
				}
				this._table.CellSpacing = 0;
				this._table.CellPadding = this._owner.BorderPadding;
				this._containerCell = new TableCell();
				TableRow tableRow = new TableRow();
				tableRow.Cells.Add(this._containerCell);
				this._table.Rows.Add(tableRow);
				this.Controls.AddAt(0, this._table);
			}

			// Token: 0x06002C9E RID: 11422 RVA: 0x000763CB File Offset: 0x000745CB
			protected internal override void OnPreRender(EventArgs e)
			{
				if (this._table != null)
				{
					this._table.ApplyStyle(this._owner.ControlStyle);
				}
				base.OnPreRender(e);
			}

			// Token: 0x06002C9F RID: 11423
			public abstract void UpdateChildControls();

			// Token: 0x04001B56 RID: 6998
			protected readonly PasswordRecovery _owner;

			// Token: 0x04001B57 RID: 6999
			private bool renderOuterTable;

			// Token: 0x04001B58 RID: 7000
			private Table _table;

			// Token: 0x04001B59 RID: 7001
			private TableCell _containerCell;
		}

		// Token: 0x020003EB RID: 1003
		private sealed class QuestionContainer : PasswordRecovery.BasePasswordRecoveryContainer
		{
			// Token: 0x06002CA0 RID: 11424 RVA: 0x000763F2 File Offset: 0x000745F2
			public QuestionContainer(PasswordRecovery owner)
				: base(owner)
			{
			}

			// Token: 0x17000E3B RID: 3643
			// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x000763FB File Offset: 0x000745FB
			public IEditableTextControl AnswerTextBox
			{
				get
				{
					Control control = this.FindControl("Answer");
					if (control == null)
					{
						throw new HttpException("QuestionTemplate does not contain an IEditableTextControl with ID Answer for the username.");
					}
					return control as IEditableTextControl;
				}
			}

			// Token: 0x17000E3C RID: 3644
			// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x0007641B File Offset: 0x0007461B
			public Literal UserNameLiteral
			{
				get
				{
					return this.FindControl("UserName") as Literal;
				}
			}

			// Token: 0x17000E3D RID: 3645
			// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x0007642D File Offset: 0x0007462D
			public Literal QuestionLiteral
			{
				get
				{
					return this.FindControl("Question") as Literal;
				}
			}

			// Token: 0x17000E3E RID: 3646
			// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x0007643F File Offset: 0x0007463F
			public Literal FailureTextLiteral
			{
				get
				{
					return this.FindControl("FailureText") as Literal;
				}
			}

			// Token: 0x06002CA5 RID: 11429 RVA: 0x00076451 File Offset: 0x00074651
			public override void UpdateChildControls()
			{
				if (this.UserNameLiteral != null)
				{
					this.UserNameLiteral.Text = this._owner.UserName;
				}
				if (this.QuestionLiteral != null)
				{
					this.QuestionLiteral.Text = this._owner.Question;
				}
			}
		}

		// Token: 0x020003EC RID: 1004
		private sealed class SuccessContainer : PasswordRecovery.BasePasswordRecoveryContainer
		{
			// Token: 0x06002CA6 RID: 11430 RVA: 0x000763F2 File Offset: 0x000745F2
			public SuccessContainer(PasswordRecovery owner)
				: base(owner)
			{
			}

			// Token: 0x06002CA7 RID: 11431 RVA: 0x0000393A File Offset: 0x00001B3A
			public override void UpdateChildControls()
			{
			}
		}

		// Token: 0x020003ED RID: 1005
		private sealed class UserNameContainer : PasswordRecovery.BasePasswordRecoveryContainer
		{
			// Token: 0x06002CA8 RID: 11432 RVA: 0x000763F2 File Offset: 0x000745F2
			public UserNameContainer(PasswordRecovery owner)
				: base(owner)
			{
			}

			// Token: 0x17000E3F RID: 3647
			// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x0007648F File Offset: 0x0007468F
			public IEditableTextControl UserNameTextBox
			{
				get
				{
					Control control = this.FindControl("UserName");
					if (control == null)
					{
						throw new HttpException("UserNameTemplate does not contain an IEditableTextControl with ID UserName for the username.");
					}
					return control as IEditableTextControl;
				}
			}

			// Token: 0x17000E40 RID: 3648
			// (get) Token: 0x06002CAA RID: 11434 RVA: 0x0004D979 File Offset: 0x0004BB79
			public ITextControl FailureTextLiteral
			{
				get
				{
					return this.FindControl("FailureText") as ITextControl;
				}
			}

			// Token: 0x06002CAB RID: 11435 RVA: 0x0000393A File Offset: 0x00001B3A
			public override void UpdateChildControls()
			{
			}
		}

		// Token: 0x020003EE RID: 1006
		private class TemplateUtils
		{
			// Token: 0x06002CAC RID: 11436 RVA: 0x000764B0 File Offset: 0x000746B0
			public static TableRow CreateRow(Control c1, Control c2, Style s1, Style s2, bool twoCells)
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				tableCell.Controls.Add(c1);
				if (s1 != null)
				{
					tableCell.ApplyStyle(s1);
				}
				tableRow.Cells.Add(tableCell);
				if (c2 != null)
				{
					TableCell tableCell2 = new TableCell();
					tableCell2.Controls.Add(c2);
					if (s2 != null)
					{
						tableCell2.ApplyStyle(s2);
					}
					tableRow.Cells.Add(tableCell2);
					tableCell.HorizontalAlign = HorizontalAlign.Right;
					tableCell2.HorizontalAlign = HorizontalAlign.Left;
				}
				else
				{
					tableCell.HorizontalAlign = HorizontalAlign.Center;
					if (twoCells)
					{
						tableCell.ColumnSpan = 2;
					}
				}
				return tableRow;
			}

			// Token: 0x06002CAD RID: 11437 RVA: 0x0007653C File Offset: 0x0007473C
			public static TableRow CreateHelpRow(string pageUrl, string linkText, string linkIcon, Style linkStyle, bool twoCells)
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				if (linkIcon.Length > 0)
				{
					Image image = new Image();
					image.ImageUrl = linkIcon;
					tableCell.Controls.Add(image);
				}
				if (linkText.Length > 0)
				{
					HyperLink hyperLink = new HyperLink();
					hyperLink.NavigateUrl = pageUrl;
					hyperLink.Text = linkText;
					hyperLink.ControlStyle.CopyTextStylesFrom(linkStyle);
					tableCell.Controls.Add(hyperLink);
				}
				if (twoCells)
				{
					tableCell.ColumnSpan = 2;
				}
				tableRow.ControlStyle.CopyFrom(linkStyle);
				tableRow.Cells.Add(tableCell);
				return tableRow;
			}
		}

		// Token: 0x020003EF RID: 1007
		private sealed class UserNameDefaultTemplate : ITemplate
		{
			// Token: 0x06002CAF RID: 11439 RVA: 0x000765CF File Offset: 0x000747CF
			public UserNameDefaultTemplate(PasswordRecovery _owner)
			{
				this._owner = _owner;
			}

			// Token: 0x06002CB0 RID: 11440 RVA: 0x000765E0 File Offset: 0x000747E0
			public void InstantiateIn(Control container)
			{
				Table table = new Table();
				table.CellPadding = 0;
				bool flag = this._owner.TextLayout == LoginTextLayout.TextOnLeft;
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(new LiteralControl(this._owner.UserNameTitleText), null, this._owner.TitleTextStyle, null, flag));
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(new LiteralControl(this._owner.UserNameInstructionText), null, this._owner.InstructionTextStyle, null, flag));
				TextBox textBox = new TextBox();
				textBox.ID = "UserName";
				textBox.Text = this._owner.UserName;
				textBox.ApplyStyle(this._owner.TextBoxStyle);
				Label label = new Label();
				label.ID = "UserNameLabel";
				label.AssociatedControlID = "UserName";
				label.Text = this._owner.UserNameLabelText;
				label.ApplyStyle(this._owner.LabelStyle);
				RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
				requiredFieldValidator.ID = "UserNameRequired";
				requiredFieldValidator.ControlToValidate = "UserName";
				requiredFieldValidator.ErrorMessage = this._owner.UserNameRequiredErrorMessage;
				requiredFieldValidator.ToolTip = this._owner.UserNameRequiredErrorMessage;
				requiredFieldValidator.Text = "*";
				requiredFieldValidator.ValidationGroup = this._owner.ID;
				requiredFieldValidator.ApplyStyle(this._owner.ValidatorTextStyle);
				if (flag)
				{
					TableRow tableRow = PasswordRecovery.TemplateUtils.CreateRow(label, textBox, null, null, flag);
					tableRow.Cells[1].Controls.Add(requiredFieldValidator);
					table.Rows.Add(tableRow);
				}
				else
				{
					table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(label, null, null, null, flag));
					TableRow tableRow2 = PasswordRecovery.TemplateUtils.CreateRow(textBox, null, null, null, flag);
					tableRow2.Cells[0].Controls.Add(requiredFieldValidator);
					table.Rows.Add(tableRow2);
				}
				Literal literal = new Literal();
				literal.ID = "FailureText";
				if (this._owner.FailureTextStyle.ForeColor.IsEmpty)
				{
					this._owner.FailureTextStyle.ForeColor = Color.Red;
				}
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(literal, null, this._owner.FailureTextStyle, null, flag));
				WebControl webControl = null;
				switch (this._owner.SubmitButtonType)
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
				webControl.ID = "SubmitButton";
				webControl.ApplyStyle(this._owner.SubmitButtonStyle);
				((IButtonControl)webControl).CommandName = PasswordRecovery.SubmitButtonCommandName;
				((IButtonControl)webControl).Text = this._owner.SubmitButtonText;
				((IButtonControl)webControl).ValidationGroup = this._owner.ID;
				TableRow tableRow3 = PasswordRecovery.TemplateUtils.CreateRow(webControl, null, null, null, flag);
				tableRow3.Cells[0].HorizontalAlign = HorizontalAlign.Right;
				table.Rows.Add(tableRow3);
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateHelpRow(this._owner.HelpPageUrl, this._owner.HelpPageText, this._owner.HelpPageIconUrl, this._owner.HyperLinkStyle, flag));
				container.Controls.Add(table);
			}

			// Token: 0x04001B5A RID: 7002
			private readonly PasswordRecovery _owner;
		}

		// Token: 0x020003F0 RID: 1008
		private sealed class QuestionDefaultTemplate : ITemplate
		{
			// Token: 0x06002CB1 RID: 11441 RVA: 0x00076943 File Offset: 0x00074B43
			public QuestionDefaultTemplate(PasswordRecovery _owner)
			{
				this._owner = _owner;
			}

			// Token: 0x06002CB2 RID: 11442 RVA: 0x00076954 File Offset: 0x00074B54
			public void InstantiateIn(Control container)
			{
				Table table = new Table();
				table.CellPadding = 0;
				bool flag = this._owner.TextLayout == LoginTextLayout.TextOnLeft;
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(new LiteralControl(this._owner.QuestionTitleText), null, this._owner.TitleTextStyle, null, flag));
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(new LiteralControl(this._owner.QuestionInstructionText), null, this._owner.InstructionTextStyle, null, flag));
				Literal literal = new Literal();
				literal.ID = "UserName";
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(new LiteralControl(this._owner.UserNameLabelText), literal, this._owner.LabelStyle, this._owner.LabelStyle, flag));
				Literal literal2 = new Literal();
				literal2.ID = "Question";
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(new LiteralControl(this._owner.QuestionLabelText), literal2, this._owner.LabelStyle, this._owner.LabelStyle, flag));
				TextBox textBox = new TextBox();
				textBox.ID = "Answer";
				textBox.ApplyStyle(this._owner.TextBoxStyle);
				Label label = new Label();
				label.ID = "AnswerLabel";
				label.AssociatedControlID = "Answer";
				label.Text = this._owner.AnswerLabelText;
				label.ApplyStyle(this._owner.LabelStyle);
				RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
				requiredFieldValidator.ID = "AnswerRequired";
				requiredFieldValidator.ControlToValidate = "Answer";
				requiredFieldValidator.ErrorMessage = this._owner.AnswerRequiredErrorMessage;
				requiredFieldValidator.ToolTip = this._owner.AnswerRequiredErrorMessage;
				requiredFieldValidator.Text = "*";
				requiredFieldValidator.ValidationGroup = this._owner.ID;
				requiredFieldValidator.ApplyStyle(this._owner.ValidatorTextStyle);
				if (flag)
				{
					TableRow tableRow = PasswordRecovery.TemplateUtils.CreateRow(label, textBox, null, null, flag);
					tableRow.Cells[1].Controls.Add(requiredFieldValidator);
					table.Rows.Add(tableRow);
				}
				else
				{
					table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(label, null, null, null, flag));
					TableRow tableRow2 = PasswordRecovery.TemplateUtils.CreateRow(textBox, null, null, null, flag);
					tableRow2.Cells[0].Controls.Add(requiredFieldValidator);
					table.Rows.Add(tableRow2);
				}
				Literal literal3 = new Literal();
				literal3.ID = "FailureText";
				if (this._owner.FailureTextStyle.ForeColor.IsEmpty)
				{
					this._owner.FailureTextStyle.ForeColor = Color.Red;
				}
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(literal3, null, this._owner.FailureTextStyle, null, flag));
				WebControl webControl = null;
				switch (this._owner.SubmitButtonType)
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
				webControl.ID = "SubmitButton";
				webControl.ApplyStyle(this._owner.SubmitButtonStyle);
				((IButtonControl)webControl).CommandName = PasswordRecovery.SubmitButtonCommandName;
				((IButtonControl)webControl).Text = this._owner.SubmitButtonText;
				((IButtonControl)webControl).ValidationGroup = this._owner.ID;
				TableRow tableRow3 = PasswordRecovery.TemplateUtils.CreateRow(webControl, null, null, null, flag);
				tableRow3.Cells[0].HorizontalAlign = HorizontalAlign.Right;
				table.Rows.Add(tableRow3);
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateHelpRow(this._owner.HelpPageUrl, this._owner.HelpPageText, this._owner.HelpPageIconUrl, this._owner.HyperLinkStyle, flag));
				container.Controls.Add(table);
			}

			// Token: 0x04001B5B RID: 7003
			private readonly PasswordRecovery _owner;
		}

		// Token: 0x020003F1 RID: 1009
		private sealed class SuccessDefaultTemplate : ITemplate
		{
			// Token: 0x06002CB3 RID: 11443 RVA: 0x00076D46 File Offset: 0x00074F46
			public SuccessDefaultTemplate(PasswordRecovery _owner)
			{
				this._owner = _owner;
			}

			// Token: 0x06002CB4 RID: 11444 RVA: 0x00076D58 File Offset: 0x00074F58
			public void InstantiateIn(Control container)
			{
				Table table = new Table();
				table.CellPadding = 0;
				bool flag = this._owner.TextLayout == LoginTextLayout.TextOnLeft;
				table.Rows.Add(PasswordRecovery.TemplateUtils.CreateRow(new LiteralControl(this._owner.SuccessText), null, this._owner.SuccessTextStyle, null, flag));
				container.Controls.Add(table);
			}

			// Token: 0x04001B5C RID: 7004
			private readonly PasswordRecovery _owner;
		}

		// Token: 0x020003F2 RID: 1010
		private enum PasswordReciveryStep
		{
			// Token: 0x04001B5E RID: 7006
			StepUserName,
			// Token: 0x04001B5F RID: 7007
			StepAnswer,
			// Token: 0x04001B60 RID: 7008
			StepSuccess
		}
	}
}

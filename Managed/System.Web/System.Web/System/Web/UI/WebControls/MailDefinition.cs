using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web.Configuration;

namespace System.Web.UI.WebControls
{
	/// <summary>Allows a control to create e-mail messages from text files or strings. This class cannot be inherited.</summary>
	// Token: 0x020003CD RID: 973
	[Bindable(false)]
	[ParseChildren(true)]
	public sealed class MailDefinition : IStateManager
	{
		/// <summary>Gets or sets the name of the file that contains text for the body of the e-mail message.</summary>
		/// <returns>The name of the file that contains the message body text. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x0006AD86 File Offset: 0x00068F86
		// (set) Token: 0x060028ED RID: 10477 RVA: 0x0006AD9D File Offset: 0x00068F9D
		[Editor("System.Web.UI.Design.MailDefinitionBodyFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[UrlProperty("*.*")]
		public string BodyFileName
		{
			get
			{
				return this._bag.GetString("BodyFileName", string.Empty);
			}
			set
			{
				this._bag["BodyFileName"] = value;
			}
		}

		/// <summary>Gets or sets a comma-separated list of e-mail addresses to send a copy (CC) of the message to.</summary>
		/// <returns>A comma-separated list of e-mail addresses to send a copy (CC) of the message to. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x0006ADB0 File Offset: 0x00068FB0
		// (set) Token: 0x060028EF RID: 10479 RVA: 0x0006ADC7 File Offset: 0x00068FC7
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string CC
		{
			get
			{
				return this._bag.GetString("CC", string.Empty);
			}
			set
			{
				this._bag["CC"] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> instances, typically used to embed images in a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object before sending an e-mail to a user.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" /> instances used to embed images in a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object before sending an e-mail to a user.</returns>
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x00003A1F File Offset: 0x00001C1F
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EmbeddedMailObjectsCollection EmbeddedObjects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the e-mail address of the message sender.</summary>
		/// <returns>The e-mail address of the message sender. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x0006ADDA File Offset: 0x00068FDA
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x0006ADF1 File Offset: 0x00068FF1
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string From
		{
			get
			{
				return this._bag.GetString("From", string.Empty);
			}
			set
			{
				this._bag["From"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the body of the e-mail is HTML.</summary>
		/// <returns>true if the body of the e-mail is HTML; otherwise, false.</returns>
		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x0006AE04 File Offset: 0x00069004
		// (set) Token: 0x060028F4 RID: 10484 RVA: 0x0006AE17 File Offset: 0x00069017
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool IsBodyHtml
		{
			get
			{
				return this._bag.GetBool("IsBodyHtml", false);
			}
			set
			{
				this._bag["IsBodyHtml"] = value;
			}
		}

		/// <summary>Gets or sets the priority of the e-mail message.</summary>
		/// <returns>One of the <see cref="T:System.Net.Mail.MailPriority" /> values. The default is <see cref="F:System.Net.Mail.MailPriority.Normal" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Net.Mail.MailPriority" /> values.</exception>
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x0006AE2F File Offset: 0x0006902F
		// (set) Token: 0x060028F6 RID: 10486 RVA: 0x0006AE5A File Offset: 0x0006905A
		[DefaultValue(MailPriority.Normal)]
		[NotifyParentProperty(true)]
		public MailPriority Priority
		{
			get
			{
				if (this._bag["Priority"] != null)
				{
					return (MailPriority)this._bag["Priority"];
				}
				return MailPriority.Normal;
			}
			set
			{
				this._bag["Priority"] = value;
			}
		}

		/// <summary>Gets or sets the subject line of the e-mail message.</summary>
		/// <returns>The subject line of the e-mail message. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x0006AE72 File Offset: 0x00069072
		// (set) Token: 0x060028F8 RID: 10488 RVA: 0x0006AE89 File Offset: 0x00069089
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Subject
		{
			get
			{
				return this._bag.GetString("Subject", string.Empty);
			}
			set
			{
				this._bag["Subject"] = value;
			}
		}

		/// <summary>Creates an e-mail message from a text file to send by means of SMTP (Simple Mail Transfer Protocol).</summary>
		/// <returns>The e-mail message from a text file.</returns>
		/// <param name="recipients">A comma-separated list of message recipients.</param>
		/// <param name="replacements">An <see cref="T:System.Collections.IDictionary" /> containing a list of strings and their replacement strings.</param>
		/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> that owns this <see cref="T:System.Web.UI.WebControls.MailDefinition" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacements" /> does not contain strings.</exception>
		/// <exception cref="T:System.Web.HttpException">The From value in the SMTP section of the configuration file is null or the empty string- or -<paramref name="recipients" /> contains an incorrect e-mail address.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x060028F9 RID: 10489 RVA: 0x0006AE9C File Offset: 0x0006909C
		public MailMessage CreateMailMessage(string recipients, IDictionary replacements, Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			string text = null;
			if (this.BodyFileName.Length > 0)
			{
				string text2;
				if (Path.IsPathRooted(this.BodyFileName))
				{
					text2 = this.BodyFileName;
				}
				else
				{
					text2 = HttpContext.Current.Request.MapPath(VirtualPathUtility.Combine(owner.TemplateSourceDirectory, this.BodyFileName));
				}
				using (StreamReader streamReader = new StreamReader(text2))
				{
					text = streamReader.ReadToEnd();
					goto IL_0077;
				}
			}
			text = "";
			IL_0077:
			return this.CreateMailMessage(recipients, replacements, text, owner);
		}

		/// <summary>Creates an e-mail message with replacements from a text file to send by means of SMTP (Simple Mail Transfer Protocol).</summary>
		/// <returns>The e-mail message with replacements from a text file.</returns>
		/// <param name="recipients">The comma-separated list of recipients.</param>
		/// <param name="replacements">An <see cref="T:System.Collections.IDictionary" /> containing a list of strings and their replacement strings.</param>
		/// <param name="body">The text of the e-mail message.</param>
		/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> that owns this <see cref="T:System.Web.UI.WebControls.MailDefinition" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacements" /> does not contain strings.</exception>
		/// <exception cref="T:System.Web.HttpException">The From value in the SMTP section of the configuration file is null or an empty string ("").- or -<paramref name="recipients" /> contains an incorrect e-mail address.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x060028FA RID: 10490 RVA: 0x0006AF3C File Offset: 0x0006913C
		public MailMessage CreateMailMessage(string recipients, IDictionary replacements, string body, Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			MailMessage mailMessage = new MailMessage();
			if (this.CC.Length > 0)
			{
				mailMessage.CC.Add(this.CC);
			}
			mailMessage.IsBodyHtml = this.IsBodyHtml;
			mailMessage.Priority = this.Priority;
			mailMessage.Subject = this.Subject;
			mailMessage.Body = body;
			if (this.From.Length > 0)
			{
				mailMessage.From = new MailAddress(this.From);
			}
			else
			{
				SmtpSection smtpSection = (SmtpSection)WebConfigurationManager.GetSection("system.net/mailSettings/smtp");
				if (smtpSection != null)
				{
					if (string.IsNullOrEmpty(smtpSection.From))
					{
						throw new HttpException("A from e-mail address must be specified in the From property or the system.net/mailSettings/smtp config section");
					}
					mailMessage.From = new MailAddress(smtpSection.From);
				}
			}
			string[] array = recipients.Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				mailMessage.To.Add(array[i]);
			}
			foreach (object obj in replacements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				mailMessage.Body = mailMessage.Body.Replace((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
			return mailMessage;
		}

		/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.IStateManager.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
		// Token: 0x060028FB RID: 10491 RVA: 0x0006B0A8 File Offset: 0x000692A8
		void IStateManager.LoadViewState(object state)
		{
			this._bag.LoadViewState(state);
		}

		/// <summary>Saves any server control view-state changes that have occurred since the time the page was posted back to the server.</summary>
		/// <returns>The server control's current view state.</returns>
		// Token: 0x060028FC RID: 10492 RVA: 0x0006B0B6 File Offset: 0x000692B6
		object IStateManager.SaveViewState()
		{
			return this._bag.SaveViewState();
		}

		/// <summary>Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x060028FD RID: 10493 RVA: 0x0006B0C3 File Offset: 0x000692C3
		void IStateManager.TrackViewState()
		{
			this._bag.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether the server control is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x0006B0D0 File Offset: 0x000692D0
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._bag.IsTrackingViewState;
			}
		}

		// Token: 0x04001A96 RID: 6806
		private StateBag _bag = new StateBag();
	}
}

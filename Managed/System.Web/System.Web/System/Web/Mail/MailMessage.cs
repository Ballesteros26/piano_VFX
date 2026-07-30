using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace System.Web.Mail
{
	/// <summary>Provides properties and methods for constructing an e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	// Token: 0x020000F7 RID: 247
	[Obsolete("The recommended alternative is System.Net.Mail.MailMessage. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class MailMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Mail.MailMessage" /> class. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		// Token: 0x06000D24 RID: 3364 RVA: 0x00023854 File Offset: 0x00021A54
		public MailMessage()
		{
			this.attachments = new ArrayList(8);
			this.headers = new ListDictionary();
			this.bodyEncoding = Encoding.Default;
			this.fields = new Hashtable();
		}

		/// <summary>Specifies the collection of attachments that are transmitted with the message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> collection of <see cref="T:System.Web.Mail.MailAttachment" /> objects.</returns>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x000238AA File Offset: 0x00021AAA
		public IList Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited list of email addresses that receive a blind carbon copy (BCC) of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>A semicolon-delimited list of e-mail addresses that receive a blind carbon copy (BCC) of the e-mail message.</returns>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000238B2 File Offset: 0x00021AB2
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x000238BA File Offset: 0x00021ABA
		public string Bcc
		{
			get
			{
				return this.bcc;
			}
			set
			{
				this.bcc = value;
			}
		}

		/// <summary>Gets or sets the body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The body of the e-mail message.</returns>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x000238C3 File Offset: 0x00021AC3
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x000238CB File Offset: 0x00021ACB
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		/// <summary>Gets or sets the encoding type of the body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>One of the <see cref="T:System.Text.Encoding" /> values that indicates the encoding type of the body of the e-mail message.</returns>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000238D4 File Offset: 0x00021AD4
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x000238DC File Offset: 0x00021ADC
		public Encoding BodyEncoding
		{
			get
			{
				return this.bodyEncoding;
			}
			set
			{
				this.bodyEncoding = value;
			}
		}

		/// <summary>Gets or sets the content type of the body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>One of the <see cref="T:System.Web.Mail.MailFormat" /> values.</returns>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x000238E5 File Offset: 0x00021AE5
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x000238ED File Offset: 0x00021AED
		public MailFormat BodyFormat
		{
			get
			{
				return this.bodyFormat;
			}
			set
			{
				this.bodyFormat = value;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited list of e-mail addresses that receive a carbon copy (CC) of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>A semicolon-delimited list of e-mail addresses that receive a carbon copy (CC) of the e-mail message.</returns>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x000238F6 File Offset: 0x00021AF6
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x000238FE File Offset: 0x00021AFE
		public string Cc
		{
			get
			{
				return this.cc;
			}
			set
			{
				this.cc = value;
			}
		}

		/// <summary>Gets or sets the e-mail address of the sender. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The sender's e-mail address.</returns>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00023907 File Offset: 0x00021B07
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0002390F File Offset: 0x00021B0F
		public string From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		/// <summary>Specifies the custom headers that are transmitted with the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> collection of custom headers.</returns>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00023918 File Offset: 0x00021B18
		public IDictionary Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets or sets the priority of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>One of the <see cref="T:System.Web.Mail.MailPriority" /> values.</returns>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00023920 File Offset: 0x00021B20
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x00023928 File Offset: 0x00021B28
		public MailPriority Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		/// <summary>Gets or sets the subject line of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The subject line of the e-mail message.</returns>
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00023931 File Offset: 0x00021B31
		// (set) Token: 0x06000D36 RID: 3382 RVA: 0x00023939 File Offset: 0x00021B39
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited list of recipient e-mail addresses. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>A semicolon-delimited list of e-mail addresses.</returns>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00023942 File Offset: 0x00021B42
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0002394A File Offset: 0x00021B4A
		public string To
		{
			get
			{
				return this.to;
			}
			set
			{
				this.to = value;
			}
		}

		/// <summary>Gets or sets the Content-Base HTTP header, the URL base of all relative URLs used within the HTML-encoded body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The URL base.</returns>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00023953 File Offset: 0x00021B53
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0002395B File Offset: 0x00021B5B
		public string UrlContentBase
		{
			get
			{
				return this.urlContentBase;
			}
			set
			{
				this.urlContentBase = value;
			}
		}

		/// <summary>Gets or sets the Content-Location HTTP header for the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The content-base header.</returns>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00023964 File Offset: 0x00021B64
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x0002396C File Offset: 0x00021B6C
		public string UrlContentLocation
		{
			get
			{
				return this.urlContentLocation;
			}
			set
			{
				this.urlContentLocation = value;
			}
		}

		/// <summary>Gets a collection of objects that map to Microsoft Collaboration Data Objects (CDO) fields. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> collection of objects that map to Collaboration Data Objects (CDO) fields.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00023975 File Offset: 0x00021B75
		public IDictionary Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x04001130 RID: 4400
		private ArrayList attachments;

		// Token: 0x04001131 RID: 4401
		private string bcc;

		// Token: 0x04001132 RID: 4402
		private string body = string.Empty;

		// Token: 0x04001133 RID: 4403
		private Encoding bodyEncoding;

		// Token: 0x04001134 RID: 4404
		private MailFormat bodyFormat;

		// Token: 0x04001135 RID: 4405
		private string cc;

		// Token: 0x04001136 RID: 4406
		private string from;

		// Token: 0x04001137 RID: 4407
		private ListDictionary headers;

		// Token: 0x04001138 RID: 4408
		private MailPriority priority;

		// Token: 0x04001139 RID: 4409
		private string subject = string.Empty;

		// Token: 0x0400113A RID: 4410
		private string to;

		// Token: 0x0400113B RID: 4411
		private string urlContentBase;

		// Token: 0x0400113C RID: 4412
		private string urlContentLocation;

		// Token: 0x0400113D RID: 4413
		private Hashtable fields;
	}
}

using System;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an e-mail message that can be sent using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x02000580 RID: 1408
	public class MailMessage : IDisposable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.MailMessage" /> class.</summary>
		// Token: 0x06002BB5 RID: 11189 RVA: 0x000ACA70 File Offset: 0x000AAC70
		public MailMessage()
		{
			this.to = new MailAddressCollection();
			this.alternateViews = new AlternateViewCollection();
			this.attachments = new AttachmentCollection();
			this.bcc = new MailAddressCollection();
			this.cc = new MailAddressCollection();
			this.replyTo = new MailAddressCollection();
			this.headers = new NameValueCollection();
			this.headers.Add("MIME-Version", "1.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.Net.Mail.MailAddress" /> class objects. </summary>
		/// <param name="from">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the recipient of the e-mail message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06002BB6 RID: 11190 RVA: 0x000ACAF0 File Offset: 0x000AACF0
		public MailMessage(MailAddress from, MailAddress to)
			: this()
		{
			if (from == null || to == null)
			{
				throw new ArgumentNullException();
			}
			this.From = from;
			this.to.Add(to);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.String" /> class objects. </summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the addresses of the recipients of the e-mail message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06002BB7 RID: 11191 RVA: 0x000ACB18 File Offset: 0x000AAD18
		public MailMessage(string from, string to)
			: this()
		{
			if (from == null || from == string.Empty)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null || to == string.Empty)
			{
				throw new ArgumentNullException("to");
			}
			this.from = new MailAddress(from);
			foreach (string text in to.Split(new char[] { ',' }))
			{
				this.to.Add(new MailAddress(text.Trim()));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class. </summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the address of the recipient of the e-mail message.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject text.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06002BB8 RID: 11192 RVA: 0x000ACBA8 File Offset: 0x000AADA8
		public MailMessage(string from, string to, string subject, string body)
			: this()
		{
			if (from == null || from == string.Empty)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null || to == string.Empty)
			{
				throw new ArgumentNullException("to");
			}
			this.from = new MailAddress(from);
			foreach (string text in to.Split(new char[] { ',' }))
			{
				this.to.Add(new MailAddress(text.Trim()));
			}
			this.Body = body;
			this.Subject = subject;
		}

		/// <summary>Gets the attachment collection used to store alternate forms of the message body.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AlternateViewCollection" />.</returns>
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x000ACC46 File Offset: 0x000AAE46
		public AlternateViewCollection AlternateViews
		{
			get
			{
				return this.alternateViews;
			}
		}

		/// <summary>Gets the attachment collection used to store data attached to this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AttachmentCollection" />.</returns>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x000ACC4E File Offset: 0x000AAE4E
		public AttachmentCollection Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		/// <summary>Gets the address collection that contains the blind carbon copy (BCC) recipients for this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000ACC56 File Offset: 0x000AAE56
		public MailAddressCollection Bcc
		{
			get
			{
				return this.bcc;
			}
		}

		/// <summary>Gets or sets the message body.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the body text.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000ACC5E File Offset: 0x000AAE5E
		// (set) Token: 0x06002BBD RID: 11197 RVA: 0x000ACC66 File Offset: 0x000AAE66
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				if (value != null && this.bodyEncoding == null)
				{
					this.bodyEncoding = this.GuessEncoding(value) ?? Encoding.ASCII;
				}
				this.body = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000ACC90 File Offset: 0x000AAE90
		internal ContentType BodyContentType
		{
			get
			{
				return new ContentType(this.isHtml ? "text/html" : "text/plain")
				{
					CharSet = (this.BodyEncoding ?? Encoding.ASCII).HeaderName
				};
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000ACCC5 File Offset: 0x000AAEC5
		internal TransferEncoding ContentTransferEncoding
		{
			get
			{
				return MailMessage.GuessTransferEncoding(this.BodyEncoding);
			}
		}

		/// <summary>Gets or sets the encoding used to encode the message body.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> applied to the contents of the <see cref="P:System.Net.Mail.MailMessage.Body" />.</returns>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000ACCD2 File Offset: 0x000AAED2
		// (set) Token: 0x06002BC1 RID: 11201 RVA: 0x000ACCDA File Offset: 0x000AAEDA
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

		/// <summary>Gets or sets the transfer encoding used to encode the message body.</summary>
		/// <returns>Returns <see cref="T:System.Net.Mime.TransferEncoding" />.A <see cref="T:System.Net.Mime.TransferEncoding" /> applied to the contents of the <see cref="P:System.Net.Mail.MailMessage.Body" />.</returns>
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000ACCC5 File Offset: 0x000AAEC5
		// (set) Token: 0x06002BC3 RID: 11203 RVA: 0x00004239 File Offset: 0x00002439
		public TransferEncoding BodyTransferEncoding
		{
			get
			{
				return MailMessage.GuessTransferEncoding(this.BodyEncoding);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the address collection that contains the carbon copy (CC) recipients for this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000ACCE3 File Offset: 0x000AAEE3
		public MailAddressCollection CC
		{
			get
			{
				return this.cc;
			}
		}

		/// <summary>Gets or sets the delivery notifications for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.DeliveryNotificationOptions" /> value that contains the delivery notifications for this message.</returns>
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x000ACCEB File Offset: 0x000AAEEB
		// (set) Token: 0x06002BC6 RID: 11206 RVA: 0x000ACCF3 File Offset: 0x000AAEF3
		public DeliveryNotificationOptions DeliveryNotificationOptions
		{
			get
			{
				return this.deliveryNotificationOptions;
			}
			set
			{
				this.deliveryNotificationOptions = value;
			}
		}

		/// <summary>Gets or sets the from address for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the from address information.</returns>
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x000ACCFC File Offset: 0x000AAEFC
		// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x000ACD04 File Offset: 0x000AAF04
		public MailAddress From
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

		/// <summary>Gets the e-mail headers that are transmitted with this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the e-mail headers.</returns>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000ACD0D File Offset: 0x000AAF0D
		public NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets or sets a value indicating whether the mail message body is in Html.</summary>
		/// <returns>true if the message body is in Html; else false. The default is false.</returns>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x000ACD15 File Offset: 0x000AAF15
		// (set) Token: 0x06002BCB RID: 11211 RVA: 0x000ACD1D File Offset: 0x000AAF1D
		public bool IsBodyHtml
		{
			get
			{
				return this.isHtml;
			}
			set
			{
				this.isHtml = value;
			}
		}

		/// <summary>Gets or sets the priority of this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailPriority" /> that contains the priority of this message.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000ACD26 File Offset: 0x000AAF26
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x000ACD2E File Offset: 0x000AAF2E
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

		/// <summary>Gets or sets the encoding used for the user-defined custom headers for this e-mail message.</summary>
		/// <returns>The encoding used for user-defined custom headers for this e-mail message.</returns>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x000ACD37 File Offset: 0x000AAF37
		// (set) Token: 0x06002BCF RID: 11215 RVA: 0x000ACD3F File Offset: 0x000AAF3F
		public Encoding HeadersEncoding
		{
			get
			{
				return this.headersEncoding;
			}
			set
			{
				this.headersEncoding = value;
			}
		}

		/// <summary>Gets or sets the list of addresses to reply to for the mail message.</summary>
		/// <returns>The list of the addresses to reply to for the mail message.</returns>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x000ACD48 File Offset: 0x000AAF48
		public MailAddressCollection ReplyToList
		{
			get
			{
				return this.replyTo;
			}
		}

		/// <summary>Gets or sets the ReplyTo address for the mail message.</summary>
		/// <returns>A MailAddress that indicates the value of the <see cref="P:System.Net.Mail.MailMessage.ReplyTo" /> field.</returns>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x000ACD50 File Offset: 0x000AAF50
		// (set) Token: 0x06002BD2 RID: 11218 RVA: 0x000ACD6D File Offset: 0x000AAF6D
		[Obsolete("Use ReplyToList instead")]
		public MailAddress ReplyTo
		{
			get
			{
				if (this.replyTo.Count == 0)
				{
					return null;
				}
				return this.replyTo[0];
			}
			set
			{
				this.replyTo.Clear();
				this.replyTo.Add(value);
			}
		}

		/// <summary>Gets or sets the sender's address for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the sender's address information.</returns>
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x000ACD86 File Offset: 0x000AAF86
		// (set) Token: 0x06002BD4 RID: 11220 RVA: 0x000ACD8E File Offset: 0x000AAF8E
		public MailAddress Sender
		{
			get
			{
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		/// <summary>Gets or sets the subject line for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the subject content.</returns>
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000ACD97 File Offset: 0x000AAF97
		// (set) Token: 0x06002BD6 RID: 11222 RVA: 0x000ACD9F File Offset: 0x000AAF9F
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				if (value != null && this.subjectEncoding == null)
				{
					this.subjectEncoding = this.GuessEncoding(value);
				}
				this.subject = value;
			}
		}

		/// <summary>Gets or sets the encoding used for the subject content for this e-mail message.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that was used to encode the <see cref="P:System.Net.Mail.MailMessage.Subject" /> property.</returns>
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x000ACDC0 File Offset: 0x000AAFC0
		// (set) Token: 0x06002BD8 RID: 11224 RVA: 0x000ACDC8 File Offset: 0x000AAFC8
		public Encoding SubjectEncoding
		{
			get
			{
				return this.subjectEncoding;
			}
			set
			{
				this.subjectEncoding = value;
			}
		}

		/// <summary>Gets the address collection that contains the recipients of this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x000ACDD1 File Offset: 0x000AAFD1
		public MailAddressCollection To
		{
			get
			{
				return this.to;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.MailMessage" />. </summary>
		// Token: 0x06002BDA RID: 11226 RVA: 0x000ACDD9 File Offset: 0x000AAFD9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.MailMessage" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002BDB RID: 11227 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000ACDE8 File Offset: 0x000AAFE8
		private Encoding GuessEncoding(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					return MailMessage.UTF8Unmarked;
				}
			}
			return null;
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000ACE1C File Offset: 0x000AB01C
		internal static TransferEncoding GuessTransferEncoding(Encoding enc)
		{
			if (Encoding.ASCII.Equals(enc))
			{
				return TransferEncoding.SevenBit;
			}
			if (Encoding.UTF8.CodePage == enc.CodePage || Encoding.Unicode.CodePage == enc.CodePage || Encoding.UTF32.CodePage == enc.CodePage)
			{
				return TransferEncoding.Base64;
			}
			return TransferEncoding.QuotedPrintable;
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000ACE74 File Offset: 0x000AB074
		internal static string To2047(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				if (b < 33 || b > 126 || b == 63 || b == 61 || b == 95)
				{
					stringBuilder.Append('=');
					stringBuilder.Append(MailMessage.hex[(b >> 4) & 15]);
					stringBuilder.Append(MailMessage.hex[(int)(b & 15)]);
				}
				else
				{
					stringBuilder.Append((char)b);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000ACEF4 File Offset: 0x000AB0F4
		internal static string EncodeSubjectRFC2047(string s, Encoding enc)
		{
			if (s == null || Encoding.ASCII.Equals(enc))
			{
				return s;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					string text = MailMessage.To2047(enc.GetBytes(s));
					return string.Concat(new string[] { "=?", enc.HeaderName, "?Q?", text, "?=" });
				}
			}
			return s;
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x000ACF71 File Offset: 0x000AB171
		private static Encoding UTF8Unmarked
		{
			get
			{
				if (MailMessage.utf8unmarked == null)
				{
					MailMessage.utf8unmarked = new UTF8Encoding(false);
				}
				return MailMessage.utf8unmarked;
			}
		}

		// Token: 0x0400246C RID: 9324
		private AlternateViewCollection alternateViews;

		// Token: 0x0400246D RID: 9325
		private AttachmentCollection attachments;

		// Token: 0x0400246E RID: 9326
		private MailAddressCollection bcc;

		// Token: 0x0400246F RID: 9327
		private MailAddressCollection replyTo;

		// Token: 0x04002470 RID: 9328
		private string body;

		// Token: 0x04002471 RID: 9329
		private MailPriority priority;

		// Token: 0x04002472 RID: 9330
		private MailAddress sender;

		// Token: 0x04002473 RID: 9331
		private DeliveryNotificationOptions deliveryNotificationOptions;

		// Token: 0x04002474 RID: 9332
		private MailAddressCollection cc;

		// Token: 0x04002475 RID: 9333
		private MailAddress from;

		// Token: 0x04002476 RID: 9334
		private NameValueCollection headers;

		// Token: 0x04002477 RID: 9335
		private MailAddressCollection to;

		// Token: 0x04002478 RID: 9336
		private string subject;

		// Token: 0x04002479 RID: 9337
		private Encoding subjectEncoding;

		// Token: 0x0400247A RID: 9338
		private Encoding bodyEncoding;

		// Token: 0x0400247B RID: 9339
		private Encoding headersEncoding = Encoding.UTF8;

		// Token: 0x0400247C RID: 9340
		private bool isHtml;

		// Token: 0x0400247D RID: 9341
		private static char[] hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};

		// Token: 0x0400247E RID: 9342
		private static Encoding utf8unmarked;
	}
}

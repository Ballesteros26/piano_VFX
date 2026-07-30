using System;
using System.Collections;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000F8 RID: 248
	internal class MailMessageWrapper
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x00023980 File Offset: 0x00021B80
		public MailMessageWrapper(MailMessage message)
		{
			this.message = message;
			if (message.From != null)
			{
				this.from = MailAddress.Parse(message.From);
				this.header.From = this.from.ToString();
			}
			if (message.To != null)
			{
				this.to = MailAddressCollection.Parse(message.To);
				this.header.To = this.to.ToString();
			}
			if (message.Cc != null)
			{
				this.cc = MailAddressCollection.Parse(message.Cc);
				this.header.Cc = this.cc.ToString();
			}
			if (message.Bcc != null)
			{
				this.bcc = MailAddressCollection.Parse(message.Bcc);
				this.header.Bcc = this.bcc.ToString();
			}
			if (message.Subject != null)
			{
				if (MailUtil.NeedEncoding(message.Subject))
				{
					byte[] bytes = message.BodyEncoding.GetBytes(message.Subject);
					this.header.Subject = string.Concat(new string[]
					{
						"=?",
						message.BodyEncoding.BodyName,
						"?B?",
						Convert.ToBase64String(bytes),
						"?="
					});
				}
				else
				{
					this.header.Subject = message.Subject;
				}
			}
			if (message.Body != null)
			{
				this.body = message.Body.Replace("\n.\n", "\n..\n");
				this.body = this.body.Replace("\r\n.\r\n", "\r\n..\r\n");
			}
			if (message.UrlContentBase != null)
			{
				this.header.ContentBase = message.UrlContentBase;
			}
			if (message.UrlContentLocation != null)
			{
				this.header.ContentLocation = message.UrlContentLocation;
			}
			MailFormat bodyFormat = message.BodyFormat;
			if (bodyFormat != MailFormat.Text)
			{
				if (bodyFormat == MailFormat.Html)
				{
					this.header.ContentType = "text/html; charset=\"" + message.BodyEncoding.BodyName + "\"";
				}
				else
				{
					this.header.ContentType = "text/html; charset=\"" + message.BodyEncoding.BodyName + "\"";
				}
			}
			else
			{
				this.header.ContentType = "text/plain; charset=\"" + message.BodyEncoding.BodyName + "\"";
			}
			switch (message.Priority)
			{
			case MailPriority.Normal:
				this.header.Importance = "normal";
				break;
			case MailPriority.Low:
				this.header.Importance = "low";
				break;
			case MailPriority.High:
				this.header.Importance = "high";
				break;
			default:
				this.header.Importance = "normal";
				break;
			}
			this.header.Priority = "normal";
			this.header.MimeVersion = "1.0";
			if (message.BodyEncoding is ASCIIEncoding)
			{
				this.header.ContentTransferEncoding = "7bit";
			}
			else
			{
				this.header.ContentTransferEncoding = "8bit";
			}
			foreach (object obj in message.Headers.Keys)
			{
				string text = (string)obj;
				this.header.Data[text] = (string)this.message.Headers[text];
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00023D1C File Offset: 0x00021F1C
		public IList Attachments
		{
			get
			{
				return this.message.Attachments;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00023D29 File Offset: 0x00021F29
		public MailAddressCollection Bcc
		{
			get
			{
				return this.bcc;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00023D31 File Offset: 0x00021F31
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x00023D39 File Offset: 0x00021F39
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

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00023D42 File Offset: 0x00021F42
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x00023D4F File Offset: 0x00021F4F
		public Encoding BodyEncoding
		{
			get
			{
				return this.message.BodyEncoding;
			}
			set
			{
				this.message.BodyEncoding = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00023D5D File Offset: 0x00021F5D
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x00023D6A File Offset: 0x00021F6A
		public MailFormat BodyFormat
		{
			get
			{
				return this.message.BodyFormat;
			}
			set
			{
				this.message.BodyFormat = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00023D78 File Offset: 0x00021F78
		public MailAddressCollection Cc
		{
			get
			{
				return this.cc;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00023D80 File Offset: 0x00021F80
		public MailAddress From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00023D88 File Offset: 0x00021F88
		public MailHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00023D90 File Offset: 0x00021F90
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x00023D9D File Offset: 0x00021F9D
		public MailPriority Priority
		{
			get
			{
				return this.message.Priority;
			}
			set
			{
				this.message.Priority = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00023DAB File Offset: 0x00021FAB
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x00023DB8 File Offset: 0x00021FB8
		public string Subject
		{
			get
			{
				return this.message.Subject;
			}
			set
			{
				this.message.Subject = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00023DC6 File Offset: 0x00021FC6
		public MailAddressCollection To
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00023DCE File Offset: 0x00021FCE
		public string UrlContentBase
		{
			get
			{
				return this.message.UrlContentBase;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00023DDB File Offset: 0x00021FDB
		public string UrlContentLocation
		{
			get
			{
				return this.message.UrlContentLocation;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00023DE8 File Offset: 0x00021FE8
		public MailHeader Fields
		{
			get
			{
				MailHeader mailHeader = new MailHeader();
				foreach (object obj in this.message.Fields.Keys)
				{
					string text = (string)obj;
					mailHeader.Data[text] = this.message.Fields[text].ToString();
				}
				return mailHeader;
			}
		}

		// Token: 0x0400113E RID: 4414
		private MailAddressCollection bcc = new MailAddressCollection();

		// Token: 0x0400113F RID: 4415
		private MailAddressCollection cc = new MailAddressCollection();

		// Token: 0x04001140 RID: 4416
		private MailAddress from;

		// Token: 0x04001141 RID: 4417
		private MailAddressCollection to = new MailAddressCollection();

		// Token: 0x04001142 RID: 4418
		private MailHeader header = new MailHeader();

		// Token: 0x04001143 RID: 4419
		private MailMessage message;

		// Token: 0x04001144 RID: 4420
		private string body;
	}
}

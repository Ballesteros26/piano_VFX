using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Permissions;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000FC RID: 252
	internal class SmtpClient
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x00024004 File Offset: 0x00022204
		public SmtpClient(string server)
		{
			this.server = server;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00024024 File Offset: 0x00022224
		private void Connect()
		{
			this.tcpConnection = new TcpClient(this.server, this.port);
			NetworkStream stream = this.tcpConnection.GetStream();
			this.smtp = new SmtpStream(stream);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00024060 File Offset: 0x00022260
		private void ChangeToSSLSocket()
		{
			Assembly assembly;
			try
			{
				assembly = Assembly.Load("Mono.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756");
			}
			catch (FileNotFoundException)
			{
				throw new SmtpException("Cannot load Mono.Security.dll");
			}
			Type type = assembly.GetType("Mono.Security.Protocol.Tls.SslClientStream");
			object[] array = new object[4];
			array[0] = this.smtp.Stream;
			array[1] = this.server;
			array[2] = true;
			Type type2 = assembly.GetType("Mono.Security.Protocol.Tls.SecurityProtocolType");
			int num = (int)Enum.Parse(type2, "Ssl3");
			int num2 = (int)Enum.Parse(type2, "Tls");
			array[3] = Enum.ToObject(type2, num | num2);
			object obj = Activator.CreateInstance(type, array);
			if (obj != null)
			{
				this.smtp = new SmtpStream((Stream)obj);
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00024124 File Offset: 0x00022324
		private void ReadFields(MailMessageWrapper msg)
		{
			this.username = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendusername"];
			this.password = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendpassword"];
			string text = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"];
			if (text != null)
			{
				this.authenticate = short.Parse(text);
			}
			text = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpusessl"];
			if (text != null)
			{
				this.usessl = bool.Parse(text);
			}
			text = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpserverport"];
			if (text != null)
			{
				this.port = int.Parse(text);
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x000241D8 File Offset: 0x000223D8
		private void StartSend(MailMessageWrapper msg)
		{
			this.ReadFields(msg);
			this.Connect();
			this.smtp.ReadResponse();
			this.smtp.CheckForStatusCode(220);
			if (this.usessl || (this.username != null && this.password != null && this.authenticate != 1))
			{
				this.smtp.WriteEhlo(Dns.GetHostName());
				if (this.usessl && this.smtp.WriteStartTLS())
				{
					this.ChangeToSSLSocket();
				}
				if (this.username != null && this.password != null && this.authenticate != 1)
				{
					this.smtp.WriteAuthLogin();
					if (this.smtp.LastResponse.StatusCode == 334)
					{
						this.smtp.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(this.username)));
						this.smtp.ReadResponse();
						this.smtp.CheckForStatusCode(334);
						this.smtp.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(this.password)));
						this.smtp.ReadResponse();
						this.smtp.CheckForStatusCode(235);
						return;
					}
				}
			}
			else
			{
				this.smtp.WriteHelo(Dns.GetHostName());
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00024330 File Offset: 0x00022530
		public void Send(MailMessageWrapper msg)
		{
			if (msg.From == null)
			{
				throw new SmtpException("From property must be set.");
			}
			if (msg.To == null && msg.To.Count < 1)
			{
				throw new SmtpException("Atleast one recipient must be set.");
			}
			this.StartSend(msg);
			this.smtp.WriteRset();
			this.smtp.WriteMailFrom(msg.From.Address);
			foreach (object obj in msg.To)
			{
				MailAddress mailAddress = (MailAddress)obj;
				this.smtp.WriteRcptTo(mailAddress.Address);
			}
			foreach (object obj2 in msg.Cc)
			{
				MailAddress mailAddress2 = (MailAddress)obj2;
				this.smtp.WriteRcptTo(mailAddress2.Address);
			}
			foreach (object obj3 in msg.Bcc)
			{
				MailAddress mailAddress3 = (MailAddress)obj3;
				this.smtp.WriteRcptTo(mailAddress3.Address);
			}
			this.smtp.WriteData();
			if (msg.Attachments.Count == 0)
			{
				this.SendSinglepartMail(msg);
			}
			else
			{
				this.SendMultipartMail(msg);
			}
			this.smtp.WriteDataEndTag();
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000244C8 File Offset: 0x000226C8
		private void SendSinglepartMail(MailMessageWrapper msg)
		{
			this.smtp.WriteHeader(msg.Header);
			this.smtp.WriteBytes(msg.BodyEncoding.GetBytes(msg.Body));
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000244F8 File Offset: 0x000226F8
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private void SendMultipartMail(MailMessageWrapper msg)
		{
			string text = MailUtil.GenerateBoundary();
			string contentType = msg.Header.ContentType;
			msg.Header.ContentType = "multipart/mixed;\r\n   boundary=" + text;
			this.smtp.WriteHeader(msg.Header);
			this.smtp.WriteBoundary(text);
			MailHeader mailHeader = new MailHeader();
			mailHeader.ContentType = contentType;
			if (msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] != null)
			{
				msg.Fields.Data.Remove("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate");
			}
			if (msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendusername"] != null)
			{
				msg.Fields.Data.Remove("http://schemas.microsoft.com/cdo/configuration/sendusername");
			}
			if (msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendpassword"] != null)
			{
				msg.Fields.Data.Remove("http://schemas.microsoft.com/cdo/configuration/sendpassword");
			}
			mailHeader.Data.Add(msg.Fields.Data);
			this.smtp.WriteHeader(mailHeader);
			this.smtp.WriteBytes(msg.BodyEncoding.GetBytes(msg.Body));
			this.smtp.WriteBoundary(text);
			for (int i = 0; i < msg.Attachments.Count; i++)
			{
				MailAttachment mailAttachment = (MailAttachment)msg.Attachments[i];
				FileInfo fileInfo = new FileInfo(mailAttachment.Filename);
				MailHeader mailHeader2 = new MailHeader();
				mailHeader2.ContentType = MimeTypes.GetMimeType(fileInfo.Name) + "; name=\"" + fileInfo.Name + "\"";
				mailHeader2.ContentDisposition = "attachment; filename=\"" + fileInfo.Name + "\"";
				mailHeader2.ContentTransferEncoding = mailAttachment.Encoding.ToString();
				this.smtp.WriteHeader(mailHeader2);
				FileStream fileStream = fileInfo.OpenRead();
				IAttachmentEncoder attachmentEncoder;
				if (mailAttachment.Encoding == MailEncoding.UUEncode)
				{
					attachmentEncoder = new UUAttachmentEncoder(644, fileInfo.Name);
				}
				else
				{
					attachmentEncoder = new Base64AttachmentEncoder();
				}
				attachmentEncoder.EncodeStream(fileStream, this.smtp.Stream);
				fileStream.Close();
				this.smtp.WriteLine("");
				if (i < msg.Attachments.Count - 1)
				{
					this.smtp.WriteBoundary(text);
				}
				else
				{
					this.smtp.WriteFinalBoundary(text);
				}
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002475C File Offset: 0x0002295C
		public void Close()
		{
			this.smtp.WriteQuit();
			this.tcpConnection.Close();
		}

		// Token: 0x0400114B RID: 4427
		private string server;

		// Token: 0x0400114C RID: 4428
		private TcpClient tcpConnection;

		// Token: 0x0400114D RID: 4429
		private SmtpStream smtp;

		// Token: 0x0400114E RID: 4430
		private string username;

		// Token: 0x0400114F RID: 4431
		private string password;

		// Token: 0x04001150 RID: 4432
		private int port = 25;

		// Token: 0x04001151 RID: 4433
		private bool usessl;

		// Token: 0x04001152 RID: 4434
		private short authenticate = 1;
	}
}

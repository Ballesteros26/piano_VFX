using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;

namespace System.Web.Mail
{
	/// <summary>Provides properties and methods for sending messages using the Collaboration Data Objects for Windows 2000 (CDOSYS) message component. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	// Token: 0x020000FE RID: 254
	[Obsolete("The recommended alternative is System.Net.Mail.SmtpClient. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class SmtpMail
	{
		// Token: 0x06000D66 RID: 3430 RVA: 0x00002050 File Offset: 0x00000250
		private SmtpMail()
		{
		}

		/// <summary>Gets or sets the name of the SMTP relay mail server to use to send e-mail messages. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The name of the e-mail relay server. </returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0002477D File Offset: 0x0002297D
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x00024784 File Offset: 0x00022984
		public static string SmtpServer
		{
			get
			{
				return SmtpMail.smtpServer;
			}
			set
			{
				SmtpMail.smtpServer = value;
			}
		}

		/// <summary>Sends an e-mail message using arguments supplied in the properties of the <see cref="T:System.Web.Mail.MailMessage" /> class. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <param name="message">The <see cref="T:System.Web.Mail.MailMessage" /> to send. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The mail cannot be sent.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="M:System.Web.Mail.SmtpMail.Send(System.Web.Mail.MailMessage)" /> method requires the Microsoft Windows NT, Windows 2000, or Windows XP operating system.</exception>
		// Token: 0x06000D69 RID: 3433 RVA: 0x0002478C File Offset: 0x0002298C
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public static void Send(MailMessage message)
		{
			try
			{
				MailMessageWrapper mailMessageWrapper = new MailMessageWrapper(message);
				SmtpClient smtpClient = new SmtpClient(SmtpMail.smtpServer);
				smtpClient.Send(mailMessageWrapper);
				smtpClient.Close();
			}
			catch (SmtpException ex)
			{
				throw new HttpException(ex.Message, ex);
			}
			catch (IOException ex2)
			{
				throw new HttpException(ex2.Message, ex2);
			}
			catch (FormatException ex3)
			{
				throw new HttpException(ex3.Message, ex3);
			}
			catch (SocketException ex4)
			{
				throw new HttpException(ex4.Message, ex4);
			}
		}

		/// <summary>Sends an e-mail message using the specified destination parameters. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <param name="from">The address of the e-mail sender. </param>
		/// <param name="to">The address of the e-mail recipient. </param>
		/// <param name="subject">The subject line of the e-mail message. </param>
		/// <param name="messageText">The body of the e-mail message. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="M:System.Web.Mail.SmtpMail.Send(System.String,System.String,System.String,System.String)" /> method requires the Microsoft Windows NT, Windows 2000, or Windows XP operating system.</exception>
		// Token: 0x06000D6A RID: 3434 RVA: 0x00024828 File Offset: 0x00022A28
		public static void Send(string from, string to, string subject, string messageText)
		{
			SmtpMail.Send(new MailMessage
			{
				From = from,
				To = to,
				Subject = subject,
				Body = messageText
			});
		}

		// Token: 0x04001153 RID: 4435
		private static string smtpServer = "localhost";
	}
}

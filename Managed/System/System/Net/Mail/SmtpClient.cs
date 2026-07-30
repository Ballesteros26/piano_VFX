using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Net.Mime;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace System.Net.Mail
{
	/// <summary>Allows applications to send e-mail by using the Simple Mail Transfer Protocol (SMTP).</summary>
	// Token: 0x02000584 RID: 1412
	[Obsolete("SmtpClient and its network of types are poorly designed, we strongly recommend you use https://github.com/jstedfast/MailKit and https://github.com/jstedfast/MimeKit instead")]
	public class SmtpClient : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class by using configuration file settings. </summary>
		// Token: 0x06002BE6 RID: 11238 RVA: 0x000ACFA3 File Offset: 0x000AB1A3
		public SmtpClient()
			: this(null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends e-mail by using the specified SMTP server. </summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host computer used for SMTP transactions.</param>
		// Token: 0x06002BE7 RID: 11239 RVA: 0x000ACFAD File Offset: 0x000AB1AD
		public SmtpClient(string host)
			: this(host, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends e-mail by using the specified SMTP server and port.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host used for SMTP transactions.</param>
		/// <param name="port">An <see cref="T:System.Int32" /> greater than zero that contains the port to be used on <paramref name="host" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> cannot be less than zero.</exception>
		// Token: 0x06002BE8 RID: 11240 RVA: 0x000ACFB8 File Offset: 0x000AB1B8
		public SmtpClient(string host, int port)
		{
			SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
			if (smtpSection != null)
			{
				this.host = smtpSection.Network.Host;
				this.port = smtpSection.Network.Port;
				this.enableSsl = smtpSection.Network.EnableSsl;
				this.TargetName = smtpSection.Network.TargetName;
				if (this.TargetName == null)
				{
					this.TargetName = "SMTPSVC/" + ((host != null) ? host : "");
				}
				if (smtpSection.Network.UserName != null)
				{
					string text = string.Empty;
					if (smtpSection.Network.Password != null)
					{
						text = smtpSection.Network.Password;
					}
					this.Credentials = new CCredentialsByHost(smtpSection.Network.UserName, text);
				}
				if (!string.IsNullOrEmpty(smtpSection.From))
				{
					this.defaultFrom = new MailAddress(smtpSection.From);
				}
			}
			if (!string.IsNullOrEmpty(host))
			{
				this.host = host;
			}
			if (port != 0)
			{
				this.port = port;
				return;
			}
			if (this.port == 0)
			{
				this.port = 25;
			}
		}

		/// <summary>Specify which certificates should be used to establish the Secure Sockets Layer (SSL) connection.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />, holding one or more client certificates. The default value is derived from the mail configuration attributes in a configuration file.</returns>
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x000AD0E9 File Offset: 0x000AB2E9
		[MonoTODO("Client certificates not used")]
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.clientCertificates == null)
				{
					this.clientCertificates = new X509CertificateCollection();
				}
				return this.clientCertificates;
			}
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) to use for authentication when using extended protection.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the SPN to use for extended protection. The default value for this SPN is of the form "SMTPSVC/&lt;host&gt;" where &lt;host&gt; is the hostname of the SMTP mail server. </returns>
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000AD104 File Offset: 0x000AB304
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x000AD10C File Offset: 0x000AB30C
		public string TargetName { get; set; }

		/// <summary>Gets or sets the credentials used to authenticate the sender.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentialsByHost" /> that represents the credentials to use for authentication; or null if no credentials have been specified.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000AD115 File Offset: 0x000AB315
		// (set) Token: 0x06002BED RID: 11245 RVA: 0x000AD11D File Offset: 0x000AB31D
		public ICredentialsByHost Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.CheckState();
				this.credentials = value;
			}
		}

		/// <summary>Specifies how outgoing email messages will be handled.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpDeliveryMethod" /> that indicates how email messages are delivered.</returns>
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000AD12C File Offset: 0x000AB32C
		// (set) Token: 0x06002BEF RID: 11247 RVA: 0x000AD134 File Offset: 0x000AB334
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return this.deliveryMethod;
			}
			set
			{
				this.CheckState();
				this.deliveryMethod = value;
			}
		}

		/// <summary>Specify whether the <see cref="T:System.Net.Mail.SmtpClient" /> uses Secure Sockets Layer (SSL) to encrypt the connection.</summary>
		/// <returns>true if the <see cref="T:System.Net.Mail.SmtpClient" /> uses SSL; otherwise, false. The default is false.</returns>
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x000AD143 File Offset: 0x000AB343
		// (set) Token: 0x06002BF1 RID: 11249 RVA: 0x000AD14B File Offset: 0x000AB34B
		public bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
			set
			{
				this.CheckState();
				this.enableSsl = value;
			}
		}

		/// <summary>Gets or sets the name or IP address of the host used for SMTP transactions.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name or IP address of the computer to use for SMTP transactions.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is equal to <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000AD15A File Offset: 0x000AB35A
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x000AD162 File Offset: 0x000AB362
		public string Host
		{
			get
			{
				return this.host;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("An empty string is not allowed.", "value");
				}
				this.CheckState();
				this.host = value;
			}
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the local SMTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the pickup directory for mail messages.</returns>
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000AD197 File Offset: 0x000AB397
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x000AD19F File Offset: 0x000AB39F
		public string PickupDirectoryLocation
		{
			get
			{
				return this.pickupDirectoryLocation;
			}
			set
			{
				this.pickupDirectoryLocation = value;
			}
		}

		/// <summary>Gets or sets the port used for SMTP transactions.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the port number on the SMTP host. The default value is 25.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000AD1A8 File Offset: 0x000AB3A8
		// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x000AD1B0 File Offset: 0x000AB3B0
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckState();
				this.port = value;
			}
		}

		/// <summary>Gets or sets the delivery format used by <see cref="T:System.Net.Mail.SmtpClient" /> to send e-mail.  </summary>
		/// <returns>Returns <see cref="T:System.Net.Mail.SmtpDeliveryFormat" />.The delivery format used by <see cref="T:System.Net.Mail.SmtpClient" />.</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000AD1CE File Offset: 0x000AB3CE
		// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x000AD1D6 File Offset: 0x000AB3D6
		public SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return this.deliveryFormat;
			}
			set
			{
				this.CheckState();
				this.deliveryFormat = value;
			}
		}

		/// <summary>Gets the network connection used to transmit the e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> that connects to the <see cref="P:System.Net.Mail.SmtpClient.Host" /> property used for SMTP.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null or the empty string ("").-or-<see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero.</exception>
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public ServicePoint ServicePoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> call times out.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that specifies the time-out value in milliseconds. The default value is 100,000 (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation was less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000AD1E5 File Offset: 0x000AB3E5
		// (set) Token: 0x06002BFC RID: 11260 RVA: 0x000AD1ED File Offset: 0x000AB3ED
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckState();
				this.timeout = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an e-mail is being sent.</exception>
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x00004240 File Offset: 0x00002440
		// (set) Token: 0x06002BFE RID: 11262 RVA: 0x000AD20B File Offset: 0x000AB40B
		public bool UseDefaultCredentials
		{
			get
			{
				return false;
			}
			[MonoNotSupported("no DefaultCredential support in Mono")]
			set
			{
				if (value)
				{
					throw new NotImplementedException("Default credentials are not supported");
				}
				this.CheckState();
			}
		}

		/// <summary>Occurs when an asynchronous e-mail send operation completes.</summary>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06002BFF RID: 11263 RVA: 0x000AD224 File Offset: 0x000AB424
		// (remove) Token: 0x06002C00 RID: 11264 RVA: 0x000AD25C File Offset: 0x000AB45C
		public event SendCompletedEventHandler SendCompleted;

		/// <summary>Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, and releases all resources used by the current instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
		// Token: 0x06002C01 RID: 11265 RVA: 0x000AD291 File Offset: 0x000AB491
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, releases all resources used by the current instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x06002C02 RID: 11266 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Does nothing at the moment.")]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000AD29A File Offset: 0x000AB49A
		private void CheckState()
		{
			if (this.messageInProcess != null)
			{
				throw new InvalidOperationException("Cannot set Timeout while Sending a message");
			}
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000AD2B0 File Offset: 0x000AB4B0
		private static string EncodeAddress(MailAddress address)
		{
			if (!string.IsNullOrEmpty(address.DisplayName))
			{
				string text = MailMessage.EncodeSubjectRFC2047(address.DisplayName, Encoding.UTF8);
				return string.Concat(new string[] { "\"", text, "\" <", address.Address, ">" });
			}
			return address.ToString();
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000AD314 File Offset: 0x000AB514
		private static string EncodeAddresses(MailAddressCollection addresses)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (MailAddress mailAddress in addresses)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(SmtpClient.EncodeAddress(mailAddress));
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000AD384 File Offset: 0x000AB584
		private string EncodeSubjectRFC2047(MailMessage message)
		{
			return MailMessage.EncodeSubjectRFC2047(message.Subject, message.SubjectEncoding);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000AD398 File Offset: 0x000AB598
		private string EncodeBody(MailMessage message)
		{
			string body = message.Body;
			Encoding bodyEncoding = message.BodyEncoding;
			TransferEncoding contentTransferEncoding = message.ContentTransferEncoding;
			if (contentTransferEncoding == TransferEncoding.Base64)
			{
				return Convert.ToBase64String(bodyEncoding.GetBytes(body), Base64FormattingOptions.InsertLineBreaks);
			}
			if (contentTransferEncoding == TransferEncoding.SevenBit)
			{
				return body;
			}
			return this.ToQuotedPrintable(body, bodyEncoding);
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000AD3DC File Offset: 0x000AB5DC
		private string EncodeBody(AlternateView av)
		{
			byte[] array = new byte[av.ContentStream.Length];
			av.ContentStream.Read(array, 0, array.Length);
			TransferEncoding transferEncoding = av.TransferEncoding;
			if (transferEncoding == TransferEncoding.Base64)
			{
				return Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks);
			}
			if (transferEncoding == TransferEncoding.SevenBit)
			{
				return Encoding.ASCII.GetString(array);
			}
			return this.ToQuotedPrintable(array);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x000AD436 File Offset: 0x000AB636
		private void EndSection(string section)
		{
			this.SendData(string.Format("--{0}--", section));
			this.SendData(string.Empty);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000AD454 File Offset: 0x000AB654
		private string GenerateBoundary()
		{
			string text = SmtpClient.GenerateBoundary(this.boundaryIndex);
			this.boundaryIndex++;
			return text;
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000AD470 File Offset: 0x000AB670
		private static string GenerateBoundary(int index)
		{
			return string.Format("--boundary_{0}_{1}", index, Guid.NewGuid().ToString("D"));
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000AD49F File Offset: 0x000AB69F
		private bool IsError(SmtpClient.SmtpResponse status)
		{
			return status.StatusCode >= (SmtpStatusCode)400;
		}

		/// <summary>Raises the <see cref="E:System.Net.Mail.SmtpClient.SendCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains event data.</param>
		// Token: 0x06002C0D RID: 11277 RVA: 0x000AD4B4 File Offset: 0x000AB6B4
		protected void OnSendCompleted(AsyncCompletedEventArgs e)
		{
			try
			{
				if (this.SendCompleted != null)
				{
					this.SendCompleted(this, e);
				}
			}
			finally
			{
				this.worker = null;
				this.user_async_state = null;
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000AD4F8 File Offset: 0x000AB6F8
		private void CheckCancellation()
		{
			if (this.worker != null && this.worker.CancellationPending)
			{
				throw new SmtpClient.CancellationException();
			}
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000AD518 File Offset: 0x000AB718
		private SmtpClient.SmtpResponse Read()
		{
			byte[] array = new byte[512];
			int num = 0;
			bool flag = false;
			do
			{
				this.CheckCancellation();
				int num2 = this.stream.Read(array, num, array.Length - num);
				if (num2 <= 0)
				{
					break;
				}
				int num3 = num + num2 - 1;
				if (num3 > 4 && (array[num3] == 10 || array[num3] == 13))
				{
					int num4 = num3 - 3;
					while (num4 >= 0 && array[num4] != 10 && array[num4] != 13)
					{
						num4--;
					}
					flag = array[num4 + 4] == 32;
				}
				num += num2;
				if (num == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, 0, array2, 0, array.Length);
					array = array2;
				}
			}
			while (!flag);
			if (num > 0)
			{
				return SmtpClient.SmtpResponse.Parse(new ASCIIEncoding().GetString(array, 0, num - 1));
			}
			throw new IOException("Connection closed");
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000AD5EC File Offset: 0x000AB7EC
		private void ResetExtensions()
		{
			this.authMechs = SmtpClient.AuthMechs.None;
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000AD5F8 File Offset: 0x000AB7F8
		private void ParseExtensions(string extens)
		{
			foreach (string text in extens.Split(new char[] { '\n' }))
			{
				if (text.Length >= 4)
				{
					string text2 = text.Substring(4);
					if (text2.StartsWith("AUTH ", StringComparison.Ordinal))
					{
						string[] array2 = text2.Split(new char[] { ' ' });
						for (int j = 1; j < array2.Length; j++)
						{
							string text3 = array2[j].Trim();
							if (!(text3 == "LOGIN"))
							{
								if (text3 == "PLAIN")
								{
									this.authMechs |= SmtpClient.AuthMechs.Plain;
								}
							}
							else
							{
								this.authMechs |= SmtpClient.AuthMechs.Login;
							}
						}
					}
				}
			}
		}

		/// <summary>Sends the specified message to an SMTP server for delivery.</summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- <see cref="P:System.Net.Mail.MailMessage.From" /> is null.-or- There are no recipients specified in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" /> properties.-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The <paramref name="message" /> could not be delivered to one or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x06002C12 RID: 11282 RVA: 0x000AD6C0 File Offset: 0x000AB8C0
		public void Send(MailMessage message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (this.deliveryMethod == SmtpDeliveryMethod.Network && (this.Host == null || this.Host.Trim().Length == 0))
			{
				throw new InvalidOperationException("The SMTP host was not specified");
			}
			if (this.deliveryMethod == SmtpDeliveryMethod.PickupDirectoryFromIis)
			{
				throw new NotSupportedException("IIS delivery is not supported");
			}
			if (this.port == 0)
			{
				this.port = 25;
			}
			this.mutex.WaitOne();
			try
			{
				this.messageInProcess = message;
				if (this.deliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
				{
					this.SendToFile(message);
				}
				else
				{
					this.SendInternal(message);
				}
			}
			catch (SmtpClient.CancellationException)
			{
			}
			catch (SmtpException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new SmtpException("Message could not be sent.", ex);
			}
			finally
			{
				this.mutex.ReleaseMutex();
				this.messageInProcess = null;
			}
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000AD7B4 File Offset: 0x000AB9B4
		private void SendInternal(MailMessage message)
		{
			this.CheckCancellation();
			try
			{
				this.client = new TcpClient(this.host, this.port);
				this.stream = this.client.GetStream();
				this.writer = new StreamWriter(this.stream);
				this.reader = new StreamReader(this.stream);
				this.SendCore(message);
			}
			finally
			{
				if (this.writer != null)
				{
					this.writer.Close();
				}
				if (this.reader != null)
				{
					this.reader.Close();
				}
				if (this.stream != null)
				{
					this.stream.Close();
				}
				if (this.client != null)
				{
					this.client.Close();
				}
			}
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000AD878 File Offset: 0x000ABA78
		private void SendToFile(MailMessage message)
		{
			if (!Path.IsPathRooted(this.pickupDirectoryLocation))
			{
				throw new SmtpException("Only absolute directories are allowed for pickup directory.");
			}
			string text = Path.Combine(this.pickupDirectoryLocation, Guid.NewGuid() + ".eml");
			try
			{
				this.writer = new StreamWriter(text);
				MailAddress from = message.From;
				if (from == null)
				{
					from = this.defaultFrom;
				}
				string text2 = DateTime.Now.ToString("ddd, dd MMM yyyy HH':'mm':'ss zzz", DateTimeFormatInfo.InvariantInfo);
				text2 = text2.Remove(text2.Length - 3, 1);
				this.SendHeader("Date", text2);
				this.SendHeader("From", SmtpClient.EncodeAddress(from));
				this.SendHeader("To", SmtpClient.EncodeAddresses(message.To));
				if (message.CC.Count > 0)
				{
					this.SendHeader("Cc", SmtpClient.EncodeAddresses(message.CC));
				}
				this.SendHeader("Subject", this.EncodeSubjectRFC2047(message));
				foreach (string text3 in message.Headers.AllKeys)
				{
					this.SendHeader(text3, message.Headers[text3]);
				}
				this.AddPriorityHeader(message);
				this.boundaryIndex = 0;
				if (message.Attachments.Count > 0)
				{
					this.SendWithAttachments(message);
				}
				else
				{
					this.SendWithoutAttachments(message, null, false);
				}
			}
			finally
			{
				if (this.writer != null)
				{
					this.writer.Close();
				}
				this.writer = null;
			}
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000ADA10 File Offset: 0x000ABC10
		private void SendCore(MailMessage message)
		{
			SmtpClient.SmtpResponse smtpResponse = this.Read();
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			string text = Dns.GetHostName();
			try
			{
				text = Dns.GetHostEntry(text).HostName;
			}
			catch (SocketException)
			{
			}
			smtpResponse = this.SendCommand("EHLO " + text);
			if (this.IsError(smtpResponse))
			{
				smtpResponse = this.SendCommand("HELO " + text);
				if (this.IsError(smtpResponse))
				{
					throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
				}
			}
			else
			{
				string description = smtpResponse.Description;
				if (description != null)
				{
					this.ParseExtensions(description);
				}
			}
			if (this.enableSsl)
			{
				this.InitiateSecureConnection();
				this.ResetExtensions();
				this.writer = new StreamWriter(this.stream);
				this.reader = new StreamReader(this.stream);
				smtpResponse = this.SendCommand("EHLO " + text);
				if (this.IsError(smtpResponse))
				{
					smtpResponse = this.SendCommand("HELO " + text);
					if (this.IsError(smtpResponse))
					{
						throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
					}
				}
				else
				{
					string description2 = smtpResponse.Description;
					if (description2 != null)
					{
						this.ParseExtensions(description2);
					}
				}
			}
			if (this.authMechs != SmtpClient.AuthMechs.None)
			{
				this.Authenticate();
			}
			MailAddress mailAddress = message.Sender;
			if (mailAddress == null)
			{
				mailAddress = message.From;
			}
			if (mailAddress == null)
			{
				mailAddress = this.defaultFrom;
			}
			smtpResponse = this.SendCommand("MAIL FROM:<" + mailAddress.Address + ">");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			List<SmtpFailedRecipientException> list = new List<SmtpFailedRecipientException>();
			for (int i = 0; i < message.To.Count; i++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.To[i].Address + ">");
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.To[i].Address));
				}
			}
			for (int j = 0; j < message.CC.Count; j++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.CC[j].Address + ">");
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.CC[j].Address));
				}
			}
			for (int k = 0; k < message.Bcc.Count; k++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.Bcc[k].Address + ">");
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.Bcc[k].Address));
				}
			}
			if (list.Count > 0)
			{
				throw new SmtpFailedRecipientsException("failed recipients", list.ToArray());
			}
			smtpResponse = this.SendCommand("DATA");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			string text2 = DateTime.Now.ToString("ddd, dd MMM yyyy HH':'mm':'ss zzz", DateTimeFormatInfo.InvariantInfo);
			text2 = text2.Remove(text2.Length - 3, 1);
			this.SendHeader("Date", text2);
			MailAddress from = message.From;
			if (from == null)
			{
				from = this.defaultFrom;
			}
			this.SendHeader("From", SmtpClient.EncodeAddress(from));
			this.SendHeader("To", SmtpClient.EncodeAddresses(message.To));
			if (message.CC.Count > 0)
			{
				this.SendHeader("Cc", SmtpClient.EncodeAddresses(message.CC));
			}
			this.SendHeader("Subject", this.EncodeSubjectRFC2047(message));
			string text3 = "normal";
			switch (message.Priority)
			{
			case MailPriority.Normal:
				text3 = "normal";
				break;
			case MailPriority.Low:
				text3 = "non-urgent";
				break;
			case MailPriority.High:
				text3 = "urgent";
				break;
			}
			this.SendHeader("Priority", text3);
			if (message.Sender != null)
			{
				this.SendHeader("Sender", SmtpClient.EncodeAddress(message.Sender));
			}
			if (message.ReplyToList.Count > 0)
			{
				this.SendHeader("Reply-To", SmtpClient.EncodeAddresses(message.ReplyToList));
			}
			foreach (string text4 in message.Headers.AllKeys)
			{
				this.SendHeader(text4, MailMessage.EncodeSubjectRFC2047(message.Headers[text4], message.HeadersEncoding));
			}
			this.AddPriorityHeader(message);
			this.boundaryIndex = 0;
			if (message.Attachments.Count > 0)
			{
				this.SendWithAttachments(message);
			}
			else
			{
				this.SendWithoutAttachments(message, null, false);
			}
			this.SendDot();
			smtpResponse = this.Read();
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			try
			{
				smtpResponse = this.SendCommand("QUIT");
			}
			catch (IOException)
			{
			}
		}

		/// <summary>Sends the specified e-mail message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the addresses that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="recipients" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="recipients" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.-or- <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The message could not be delivered to one or more of the recipients in <paramref name="recipients" />. </exception>
		// Token: 0x06002C16 RID: 11286 RVA: 0x000ADF3C File Offset: 0x000AC13C
		public void Send(string from, string recipients, string subject, string body)
		{
			this.Send(new MailMessage(from, recipients, subject, body));
		}

		/// <summary>Sends the specified message to an SMTP server for delivery as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is null.</exception>
		// Token: 0x06002C17 RID: 11287 RVA: 0x000ADF50 File Offset: 0x000AC150
		public Task SendMailAsync(MailMessage message)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			SendCompletedEventHandler handler = null;
			handler = delegate(object s, AsyncCompletedEventArgs e)
			{
				SmtpClient.SendMailAsyncCompletedHandler(tcs, e, handler, this);
			};
			this.SendCompleted += handler;
			this.SendAsync(message, tcs);
			return tcs.Task;
		}

		/// <summary>Sends the specified message to an SMTP server for delivery as an asynchronous operation. . The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the addresses that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="recipients" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="recipients" /> is <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x06002C18 RID: 11288 RVA: 0x000ADFB2 File Offset: 0x000AC1B2
		public Task SendMailAsync(string from, string recipients, string subject, string body)
		{
			return this.SendMailAsync(new MailMessage(from, recipients, subject, body));
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000ADFC4 File Offset: 0x000AC1C4
		private static void SendMailAsyncCompletedHandler(TaskCompletionSource<object> source, AsyncCompletedEventArgs e, SendCompletedEventHandler handler, SmtpClient client)
		{
			if (source != e.UserState)
			{
				return;
			}
			client.SendCompleted -= handler;
			if (e.Error != null)
			{
				source.SetException(e.Error);
				return;
			}
			if (e.Cancelled)
			{
				source.SetCanceled();
				return;
			}
			source.SetResult(null);
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000AE002 File Offset: 0x000AC202
		private void SendDot()
		{
			this.writer.Write(".\r\n");
			this.writer.Flush();
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000AE020 File Offset: 0x000AC220
		private void SendData(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				this.writer.Write("\r\n");
				this.writer.Flush();
				return;
			}
			StringReader stringReader = new StringReader(data);
			bool flag = this.deliveryMethod == SmtpDeliveryMethod.Network;
			string text;
			while ((text = stringReader.ReadLine()) != null)
			{
				this.CheckCancellation();
				if (flag && text.Length > 0 && text[0] == '.')
				{
					text = "." + text;
				}
				this.writer.Write(text);
				this.writer.Write("\r\n");
			}
			this.writer.Flush();
		}

		/// <summary>Sends the specified e-mail message to an SMTP server for delivery. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes. </summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is null.-or-<see cref="P:System.Net.Mail.MailMessage.From" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- There are no recipients specified in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" /> properties.-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.-or- <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.-or-The <paramref name="message" /> could not be delivered to one or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x06002C1C RID: 11292 RVA: 0x000AE0C0 File Offset: 0x000AC2C0
		public void SendAsync(MailMessage message, object userToken)
		{
			if (this.worker != null)
			{
				throw new InvalidOperationException("Another SendAsync operation is in progress");
			}
			this.worker = new BackgroundWorker();
			this.worker.DoWork += delegate(object o, DoWorkEventArgs ea)
			{
				try
				{
					this.user_async_state = ea.Argument;
					this.Send(message);
				}
				catch (Exception ex)
				{
					ea.Result = ex;
					throw ex;
				}
			};
			this.worker.WorkerSupportsCancellation = true;
			this.worker.RunWorkerCompleted += delegate(object o, RunWorkerCompletedEventArgs ea)
			{
				this.OnSendCompleted(new AsyncCompletedEventArgs(ea.Error, ea.Cancelled, this.user_async_state));
			};
			this.worker.RunWorkerAsync(userToken);
		}

		/// <summary>Sends an e-mail message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the address that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="recipient" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="recipient" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.-or- <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.-or-The message could not be delivered to one or more of the recipients in <paramref name="recipients" />.</exception>
		// Token: 0x06002C1D RID: 11293 RVA: 0x000AE145 File Offset: 0x000AC345
		public void SendAsync(string from, string recipients, string subject, string body, object userToken)
		{
			this.SendAsync(new MailMessage(from, recipients, subject, body), userToken);
		}

		/// <summary>Cancels an asynchronous operation to send an e-mail message.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06002C1E RID: 11294 RVA: 0x000AE159 File Offset: 0x000AC359
		public void SendAsyncCancel()
		{
			if (this.worker == null)
			{
				throw new InvalidOperationException("SendAsync operation is not in progress");
			}
			this.worker.CancelAsync();
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000AE17C File Offset: 0x000AC37C
		private void AddPriorityHeader(MailMessage message)
		{
			MailPriority priority = message.Priority;
			if (priority != MailPriority.Low)
			{
				if (priority == MailPriority.High)
				{
					this.SendHeader("Priority", "Urgent");
					this.SendHeader("Importance", "high");
					this.SendHeader("X-Priority", "1");
					return;
				}
			}
			else
			{
				this.SendHeader("Priority", "Non-Urgent");
				this.SendHeader("Importance", "low");
				this.SendHeader("X-Priority", "5");
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000AE1FC File Offset: 0x000AC3FC
		private void SendSimpleBody(MailMessage message)
		{
			this.SendHeader("Content-Type", message.BodyContentType.ToString());
			if (message.ContentTransferEncoding != TransferEncoding.SevenBit)
			{
				this.SendHeader("Content-Transfer-Encoding", SmtpClient.GetTransferEncodingName(message.ContentTransferEncoding));
			}
			this.SendData(string.Empty);
			this.SendData(this.EncodeBody(message));
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000AE258 File Offset: 0x000AC458
		private void SendBodylessSingleAlternate(AlternateView av)
		{
			this.SendHeader("Content-Type", av.ContentType.ToString());
			if (av.TransferEncoding != TransferEncoding.SevenBit)
			{
				this.SendHeader("Content-Transfer-Encoding", SmtpClient.GetTransferEncodingName(av.TransferEncoding));
			}
			this.SendData(string.Empty);
			this.SendData(this.EncodeBody(av));
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000AE2B4 File Offset: 0x000AC4B4
		private void SendWithoutAttachments(MailMessage message, string boundary, bool attachmentExists)
		{
			if (message.Body == null && message.AlternateViews.Count == 1)
			{
				this.SendBodylessSingleAlternate(message.AlternateViews[0]);
				return;
			}
			if (message.AlternateViews.Count > 0)
			{
				this.SendBodyWithAlternateViews(message, boundary, attachmentExists);
				return;
			}
			this.SendSimpleBody(message);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000AE30C File Offset: 0x000AC50C
		private void SendWithAttachments(MailMessage message)
		{
			string text = this.GenerateBoundary();
			this.SendHeader("Content-Type", new ContentType
			{
				Boundary = text,
				MediaType = "multipart/mixed",
				CharSet = null
			}.ToString());
			this.SendData(string.Empty);
			Attachment attachment = null;
			if (message.AlternateViews.Count > 0)
			{
				this.SendWithoutAttachments(message, text, true);
			}
			else
			{
				attachment = Attachment.CreateAttachmentFromString(message.Body, null, message.BodyEncoding, message.IsBodyHtml ? "text/html" : "text/plain");
				message.Attachments.Insert(0, attachment);
			}
			try
			{
				this.SendAttachments(message, attachment, text);
			}
			finally
			{
				if (attachment != null)
				{
					message.Attachments.Remove(attachment);
				}
			}
			this.EndSection(text);
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000AE3E0 File Offset: 0x000AC5E0
		private void SendBodyWithAlternateViews(MailMessage message, string boundary, bool attachmentExists)
		{
			AlternateViewCollection alternateViews = message.AlternateViews;
			string text = this.GenerateBoundary();
			ContentType contentType = new ContentType();
			contentType.Boundary = text;
			contentType.MediaType = "multipart/alternative";
			if (!attachmentExists)
			{
				this.SendHeader("Content-Type", contentType.ToString());
				this.SendData(string.Empty);
			}
			AlternateView alternateView = null;
			if (message.Body != null)
			{
				alternateView = AlternateView.CreateAlternateViewFromString(message.Body, message.BodyEncoding, message.IsBodyHtml ? "text/html" : "text/plain");
				alternateViews.Insert(0, alternateView);
				this.StartSection(boundary, contentType);
			}
			try
			{
				foreach (AlternateView alternateView2 in alternateViews)
				{
					string text2 = null;
					if (alternateView2.LinkedResources.Count > 0)
					{
						text2 = this.GenerateBoundary();
						ContentType contentType2 = new ContentType("multipart/related");
						contentType2.Boundary = text2;
						contentType2.Parameters["type"] = alternateView2.ContentType.ToString();
						this.StartSection(text, contentType2);
						this.StartSection(text2, alternateView2.ContentType, alternateView2);
					}
					else
					{
						ContentType contentType2 = new ContentType(alternateView2.ContentType.ToString());
						this.StartSection(text, contentType2, alternateView2);
					}
					switch (alternateView2.TransferEncoding)
					{
					case TransferEncoding.Unknown:
					case TransferEncoding.SevenBit:
					{
						byte[] array = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array, 0, array.Length);
						this.SendData(Encoding.ASCII.GetString(array));
						break;
					}
					case TransferEncoding.QuotedPrintable:
					{
						byte[] array2 = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array2, 0, array2.Length);
						this.SendData(this.ToQuotedPrintable(array2));
						break;
					}
					case TransferEncoding.Base64:
					{
						byte[] array = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array, 0, array.Length);
						this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
						break;
					}
					}
					if (alternateView2.LinkedResources.Count > 0)
					{
						this.SendLinkedResources(message, alternateView2.LinkedResources, text2);
						this.EndSection(text2);
					}
					if (!attachmentExists)
					{
						this.SendData(string.Empty);
					}
				}
			}
			finally
			{
				if (alternateView != null)
				{
					alternateViews.Remove(alternateView);
				}
			}
			this.EndSection(text);
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000AE678 File Offset: 0x000AC878
		private void SendLinkedResources(MailMessage message, LinkedResourceCollection resources, string boundary)
		{
			foreach (LinkedResource linkedResource in resources)
			{
				this.StartSection(boundary, linkedResource.ContentType, linkedResource);
				switch (linkedResource.TransferEncoding)
				{
				case TransferEncoding.Unknown:
				case TransferEncoding.SevenBit:
				{
					byte[] array = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array, 0, array.Length);
					this.SendData(Encoding.ASCII.GetString(array));
					break;
				}
				case TransferEncoding.QuotedPrintable:
				{
					byte[] array2 = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array2, 0, array2.Length);
					this.SendData(this.ToQuotedPrintable(array2));
					break;
				}
				case TransferEncoding.Base64:
				{
					byte[] array = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array, 0, array.Length);
					this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
					break;
				}
				}
			}
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000AE790 File Offset: 0x000AC990
		private void SendAttachments(MailMessage message, Attachment body, string boundary)
		{
			foreach (Attachment attachment in message.Attachments)
			{
				ContentType contentType = new ContentType(attachment.ContentType.ToString());
				if (attachment.Name != null)
				{
					contentType.Name = attachment.Name;
					if (attachment.NameEncoding != null)
					{
						contentType.CharSet = attachment.NameEncoding.HeaderName;
					}
					attachment.ContentDisposition.FileName = attachment.Name;
				}
				this.StartSection(boundary, contentType, attachment, attachment != body);
				byte[] array = new byte[attachment.ContentStream.Length];
				attachment.ContentStream.Read(array, 0, array.Length);
				switch (attachment.TransferEncoding)
				{
				case TransferEncoding.Unknown:
				case TransferEncoding.SevenBit:
					this.SendData(Encoding.ASCII.GetString(array));
					break;
				case TransferEncoding.QuotedPrintable:
					this.SendData(this.ToQuotedPrintable(array));
					break;
				case TransferEncoding.Base64:
					this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
					break;
				}
				this.SendData(string.Empty);
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000AE8BC File Offset: 0x000ACABC
		private SmtpClient.SmtpResponse SendCommand(string command)
		{
			this.writer.Write(command);
			this.writer.Write("\r\n");
			this.writer.Flush();
			return this.Read();
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000AE8EB File Offset: 0x000ACAEB
		private void SendHeader(string name, string value)
		{
			this.SendData(string.Format("{0}: {1}", name, value));
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000AE8FF File Offset: 0x000ACAFF
		private void StartSection(string section, ContentType sectionContentType)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendData(string.Empty);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000AE930 File Offset: 0x000ACB30
		private void StartSection(string section, ContentType sectionContentType, AttachmentBase att)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendHeader("content-transfer-encoding", SmtpClient.GetTransferEncodingName(att.TransferEncoding));
			if (!string.IsNullOrEmpty(att.ContentId))
			{
				this.SendHeader("content-ID", "<" + att.ContentId + ">");
			}
			this.SendData(string.Empty);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000AE9B0 File Offset: 0x000ACBB0
		private void StartSection(string section, ContentType sectionContentType, Attachment att, bool sendDisposition)
		{
			this.SendData(string.Format("--{0}", section));
			if (!string.IsNullOrEmpty(att.ContentId))
			{
				this.SendHeader("content-ID", "<" + att.ContentId + ">");
			}
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendHeader("content-transfer-encoding", SmtpClient.GetTransferEncodingName(att.TransferEncoding));
			if (sendDisposition)
			{
				this.SendHeader("content-disposition", att.ContentDisposition.ToString());
			}
			this.SendData(string.Empty);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000AEA48 File Offset: 0x000ACC48
		private string ToQuotedPrintable(string input, Encoding enc)
		{
			byte[] bytes = enc.GetBytes(input);
			return this.ToQuotedPrintable(bytes);
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000AEA64 File Offset: 0x000ACC64
		private string ToQuotedPrintable(byte[] bytes)
		{
			StringWriter stringWriter = new StringWriter();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder("=", 3);
			byte b = 61;
			char c = '\0';
			int i = 0;
			while (i < bytes.Length)
			{
				byte b2 = bytes[i];
				int num2;
				if (b2 > 127 || b2 == b)
				{
					stringBuilder.Length = 1;
					stringBuilder.Append(Convert.ToString(b2, 16).ToUpperInvariant());
					num2 = 3;
					goto IL_007C;
				}
				c = Convert.ToChar(b2);
				if (c != '\r' && c != '\n')
				{
					num2 = 1;
					goto IL_007C;
				}
				stringWriter.Write(c);
				num = 0;
				IL_00AC:
				i++;
				continue;
				IL_007C:
				num += num2;
				if (num > 75)
				{
					stringWriter.Write("=\r\n");
					num = num2;
				}
				if (num2 == 1)
				{
					stringWriter.Write(c);
					goto IL_00AC;
				}
				stringWriter.Write(stringBuilder.ToString());
				goto IL_00AC;
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000AEB34 File Offset: 0x000ACD34
		private static string GetTransferEncodingName(TransferEncoding encoding)
		{
			switch (encoding)
			{
			case TransferEncoding.QuotedPrintable:
				return "quoted-printable";
			case TransferEncoding.Base64:
				return "base64";
			case TransferEncoding.SevenBit:
				return "7bit";
			default:
				return "unknown";
			}
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000AEB64 File Offset: 0x000ACD64
		private void InitiateSecureConnection()
		{
			SmtpClient.SmtpResponse smtpResponse = this.SendCommand("STARTTLS");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(SmtpStatusCode.GeneralFailure, "Server does not support secure connections.");
			}
			MonoTlsProvider providerInternal = Mono.Net.Security.MonoTlsProviderFactory.GetProviderInternal();
			MonoTlsSettings monoTlsSettings = MonoTlsSettings.CopyDefaultSettings();
			monoTlsSettings.UseServicePointManagerCallback = new bool?(true);
			IMonoSslStream monoSslStream = providerInternal.CreateSslStream(this.stream, false, monoTlsSettings);
			this.CheckCancellation();
			monoSslStream.AuthenticateAsClient(this.Host, this.ClientCertificates, SslProtocols.Default, false);
			this.stream = monoSslStream.AuthenticatedStream;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000AEBE4 File Offset: 0x000ACDE4
		private void Authenticate()
		{
			string text;
			string text2;
			if (this.UseDefaultCredentials)
			{
				text = CredentialCache.DefaultCredentials.GetCredential(new Uri("smtp://" + this.host), "basic").UserName;
				text2 = CredentialCache.DefaultCredentials.GetCredential(new Uri("smtp://" + this.host), "basic").Password;
			}
			else
			{
				if (this.Credentials == null)
				{
					return;
				}
				text = this.Credentials.GetCredential(this.host, this.port, "smtp").UserName;
				text2 = this.Credentials.GetCredential(this.host, this.port, "smtp").Password;
			}
			this.Authenticate(text, text2);
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000AECAA File Offset: 0x000ACEAA
		private void CheckStatus(SmtpClient.SmtpResponse status, int i)
		{
			if (status.StatusCode != (SmtpStatusCode)i)
			{
				throw new SmtpException(status.StatusCode, status.Description);
			}
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000AECC7 File Offset: 0x000ACEC7
		private void ThrowIfError(SmtpClient.SmtpResponse status)
		{
			if (this.IsError(status))
			{
				throw new SmtpException(status.StatusCode, status.Description);
			}
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000AECE4 File Offset: 0x000ACEE4
		private void Authenticate(string user, string password)
		{
			if (this.authMechs == SmtpClient.AuthMechs.None)
			{
				return;
			}
			if ((this.authMechs & SmtpClient.AuthMechs.Login) != SmtpClient.AuthMechs.None)
			{
				SmtpClient.SmtpResponse smtpResponse = this.SendCommand("AUTH LOGIN");
				this.CheckStatus(smtpResponse, 334);
				smtpResponse = this.SendCommand(Convert.ToBase64String(Encoding.UTF8.GetBytes(user)));
				this.CheckStatus(smtpResponse, 334);
				smtpResponse = this.SendCommand(Convert.ToBase64String(Encoding.UTF8.GetBytes(password)));
				this.CheckStatus(smtpResponse, 235);
				return;
			}
			if ((this.authMechs & SmtpClient.AuthMechs.Plain) != SmtpClient.AuthMechs.None)
			{
				string text = string.Format("\0{0}\0{1}", user, password);
				text = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
				SmtpClient.SmtpResponse smtpResponse = this.SendCommand("AUTH PLAIN " + text);
				this.CheckStatus(smtpResponse, 235);
				return;
			}
			throw new SmtpException("AUTH types PLAIN, LOGIN not supported by the server");
		}

		// Token: 0x04002487 RID: 9351
		private string host;

		// Token: 0x04002488 RID: 9352
		private int port;

		// Token: 0x04002489 RID: 9353
		private int timeout = 100000;

		// Token: 0x0400248A RID: 9354
		private ICredentialsByHost credentials;

		// Token: 0x0400248B RID: 9355
		private string pickupDirectoryLocation;

		// Token: 0x0400248C RID: 9356
		private SmtpDeliveryMethod deliveryMethod;

		// Token: 0x0400248D RID: 9357
		private SmtpDeliveryFormat deliveryFormat;

		// Token: 0x0400248E RID: 9358
		private bool enableSsl;

		// Token: 0x0400248F RID: 9359
		private X509CertificateCollection clientCertificates;

		// Token: 0x04002490 RID: 9360
		private TcpClient client;

		// Token: 0x04002491 RID: 9361
		private Stream stream;

		// Token: 0x04002492 RID: 9362
		private StreamWriter writer;

		// Token: 0x04002493 RID: 9363
		private StreamReader reader;

		// Token: 0x04002494 RID: 9364
		private int boundaryIndex;

		// Token: 0x04002495 RID: 9365
		private MailAddress defaultFrom;

		// Token: 0x04002496 RID: 9366
		private MailMessage messageInProcess;

		// Token: 0x04002497 RID: 9367
		private BackgroundWorker worker;

		// Token: 0x04002498 RID: 9368
		private object user_async_state;

		// Token: 0x04002499 RID: 9369
		private SmtpClient.AuthMechs authMechs;

		// Token: 0x0400249A RID: 9370
		private Mutex mutex = new Mutex();

		// Token: 0x02000585 RID: 1413
		[Flags]
		private enum AuthMechs
		{
			// Token: 0x0400249E RID: 9374
			None = 0,
			// Token: 0x0400249F RID: 9375
			Login = 1,
			// Token: 0x040024A0 RID: 9376
			Plain = 2
		}

		// Token: 0x02000586 RID: 1414
		private class CancellationException : Exception
		{
		}

		// Token: 0x02000587 RID: 1415
		private struct HeaderName
		{
			// Token: 0x040024A1 RID: 9377
			public const string ContentTransferEncoding = "Content-Transfer-Encoding";

			// Token: 0x040024A2 RID: 9378
			public const string ContentType = "Content-Type";

			// Token: 0x040024A3 RID: 9379
			public const string Bcc = "Bcc";

			// Token: 0x040024A4 RID: 9380
			public const string Cc = "Cc";

			// Token: 0x040024A5 RID: 9381
			public const string From = "From";

			// Token: 0x040024A6 RID: 9382
			public const string Subject = "Subject";

			// Token: 0x040024A7 RID: 9383
			public const string To = "To";

			// Token: 0x040024A8 RID: 9384
			public const string MimeVersion = "MIME-Version";

			// Token: 0x040024A9 RID: 9385
			public const string MessageId = "Message-ID";

			// Token: 0x040024AA RID: 9386
			public const string Priority = "Priority";

			// Token: 0x040024AB RID: 9387
			public const string Importance = "Importance";

			// Token: 0x040024AC RID: 9388
			public const string XPriority = "X-Priority";

			// Token: 0x040024AD RID: 9389
			public const string Date = "Date";
		}

		// Token: 0x02000588 RID: 1416
		private struct SmtpResponse
		{
			// Token: 0x06002C35 RID: 11317 RVA: 0x000AEDB4 File Offset: 0x000ACFB4
			public static SmtpClient.SmtpResponse Parse(string line)
			{
				SmtpClient.SmtpResponse smtpResponse = default(SmtpClient.SmtpResponse);
				if (line.Length < 4)
				{
					throw new SmtpException("Response is to short " + line.Length + ".");
				}
				if (line[3] != ' ' && line[3] != '-')
				{
					throw new SmtpException("Response format is wrong.(" + line + ")");
				}
				smtpResponse.StatusCode = (SmtpStatusCode)int.Parse(line.Substring(0, 3));
				smtpResponse.Description = line;
				return smtpResponse;
			}

			// Token: 0x040024AE RID: 9390
			public SmtpStatusCode StatusCode;

			// Token: 0x040024AF RID: 9391
			public string Description;
		}
	}
}

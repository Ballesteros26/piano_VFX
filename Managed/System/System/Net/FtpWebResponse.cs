using System;
using System.IO;
using Unity;

namespace System.Net
{
	/// <summary>Encapsulates a File Transfer Protocol (FTP) server's response to a request.</summary>
	// Token: 0x0200051B RID: 1307
	public class FtpWebResponse : WebResponse
	{
		// Token: 0x06002789 RID: 10121 RVA: 0x00098C64 File Offset: 0x00096E64
		internal FtpWebResponse(FtpWebRequest request, Uri uri, string method, bool keepAlive)
		{
			this.lastModified = DateTime.MinValue;
			this.bannerMessage = string.Empty;
			this.welcomeMessage = string.Empty;
			this.exitMessage = string.Empty;
			this.contentLength = -1L;
			base..ctor();
			this.request = request;
			this.uri = uri;
			this.method = method;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x00098CC0 File Offset: 0x00096EC0
		internal FtpWebResponse(FtpWebRequest request, Uri uri, string method, FtpStatusCode statusCode, string statusDescription)
		{
			this.lastModified = DateTime.MinValue;
			this.bannerMessage = string.Empty;
			this.welcomeMessage = string.Empty;
			this.exitMessage = string.Empty;
			this.contentLength = -1L;
			base..ctor();
			this.request = request;
			this.uri = uri;
			this.method = method;
			this.statusCode = statusCode;
			this.statusDescription = statusDescription;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00098D2C File Offset: 0x00096F2C
		internal FtpWebResponse(FtpWebRequest request, Uri uri, string method, FtpStatus status)
			: this(request, uri, method, status.StatusCode, status.StatusDescription)
		{
		}

		/// <summary>Gets the length of the data received from the FTP server.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of bytes of data received from the FTP server. </returns>
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x00098D45 File Offset: 0x00096F45
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		/// <summary>Gets an empty <see cref="T:System.Net.WebHeaderCollection" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Net.WebHeaderCollection" /> object.</returns>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x00098D4D File Offset: 0x00096F4D
		public override WebHeaderCollection Headers
		{
			get
			{
				return new WebHeaderCollection();
			}
		}

		/// <summary>Gets the URI that sent the response to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that identifies the resource associated with this response.</returns>
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x00098D54 File Offset: 0x00096F54
		public override Uri ResponseUri
		{
			get
			{
				return this.uri;
			}
		}

		/// <summary>Gets the date and time that a file on an FTP server was last modified.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the last modified date and time for a file.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600278F RID: 10127 RVA: 0x00098D5C File Offset: 0x00096F5C
		// (set) Token: 0x06002790 RID: 10128 RVA: 0x00098D64 File Offset: 0x00096F64
		public DateTime LastModified
		{
			get
			{
				return this.lastModified;
			}
			internal set
			{
				this.lastModified = value;
			}
		}

		/// <summary>Gets the message sent by the FTP server when a connection is established prior to logon.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the banner message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x00098D6D File Offset: 0x00096F6D
		// (set) Token: 0x06002792 RID: 10130 RVA: 0x00098D75 File Offset: 0x00096F75
		public string BannerMessage
		{
			get
			{
				return this.bannerMessage;
			}
			internal set
			{
				this.bannerMessage = value;
			}
		}

		/// <summary>Gets the message sent by the FTP server when authentication is complete.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the welcome message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x00098D7E File Offset: 0x00096F7E
		// (set) Token: 0x06002794 RID: 10132 RVA: 0x00098D86 File Offset: 0x00096F86
		public string WelcomeMessage
		{
			get
			{
				return this.welcomeMessage;
			}
			internal set
			{
				this.welcomeMessage = value;
			}
		}

		/// <summary>Gets the message sent by the server when the FTP session is ending.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the exit message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x00098D8F File Offset: 0x00096F8F
		// (set) Token: 0x06002796 RID: 10134 RVA: 0x00098D97 File Offset: 0x00096F97
		public string ExitMessage
		{
			get
			{
				return this.exitMessage;
			}
			internal set
			{
				this.exitMessage = value;
			}
		}

		/// <summary>Gets the most recent status code sent from the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.FtpStatusCode" /> value that indicates the most recent status code returned with this response.</returns>
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x00098DA0 File Offset: 0x00096FA0
		// (set) Token: 0x06002798 RID: 10136 RVA: 0x00098DA8 File Offset: 0x00096FA8
		public FtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			internal set
			{
				this.statusCode = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Net.FtpWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FtpWebResponse" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="P:System.Net.FtpWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FtpWebResponse" /> instance; otherwise, false.</returns>
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool SupportsHeaders
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets text that describes a status code sent from the FTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the status code and message returned with this response.</returns>
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x00098DB1 File Offset: 0x00096FB1
		// (set) Token: 0x0600279B RID: 10139 RVA: 0x00098DB9 File Offset: 0x00096FB9
		public string StatusDescription
		{
			get
			{
				return this.statusDescription;
			}
			internal set
			{
				this.statusDescription = value;
			}
		}

		/// <summary>Frees the resources held by the response.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600279C RID: 10140 RVA: 0x00098DC4 File Offset: 0x00096FC4
		public override void Close()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.stream != null)
			{
				this.stream.Close();
				if (this.stream == Stream.Null)
				{
					this.request.OperationCompleted();
				}
			}
			this.stream = null;
		}

		/// <summary>Retrieves the stream that contains response data sent from an FTP server.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> instance that contains data returned with the response; otherwise, <see cref="F:System.IO.Stream.Null" /> if no response data was returned by the server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The response did not return a data stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600279D RID: 10141 RVA: 0x00098E13 File Offset: 0x00097013
		public override Stream GetResponseStream()
		{
			if (this.stream == null)
			{
				return Stream.Null;
			}
			if (this.method != "RETR" && this.method != "NLST")
			{
				this.CheckDisposed();
			}
			return this.stream;
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x00098E5C File Offset: 0x0009705C
		// (set) Token: 0x0600279E RID: 10142 RVA: 0x00098E53 File Offset: 0x00097053
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
			set
			{
				this.stream = value;
			}
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x00098E64 File Offset: 0x00097064
		internal void UpdateStatus(FtpStatus status)
		{
			this.statusCode = status.StatusCode;
			this.statusDescription = status.StatusDescription;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00098E7E File Offset: 0x0009707E
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00098E99 File Offset: 0x00097099
		internal bool IsFinal()
		{
			return this.statusCode >= FtpStatusCode.CommandOK;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal FtpWebResponse()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002175 RID: 8565
		private Stream stream;

		// Token: 0x04002176 RID: 8566
		private Uri uri;

		// Token: 0x04002177 RID: 8567
		private FtpStatusCode statusCode;

		// Token: 0x04002178 RID: 8568
		private DateTime lastModified;

		// Token: 0x04002179 RID: 8569
		private string bannerMessage;

		// Token: 0x0400217A RID: 8570
		private string welcomeMessage;

		// Token: 0x0400217B RID: 8571
		private string exitMessage;

		// Token: 0x0400217C RID: 8572
		private string statusDescription;

		// Token: 0x0400217D RID: 8573
		private string method;

		// Token: 0x0400217E RID: 8574
		private bool disposed;

		// Token: 0x0400217F RID: 8575
		private FtpWebRequest request;

		// Token: 0x04002180 RID: 8576
		internal long contentLength;
	}
}

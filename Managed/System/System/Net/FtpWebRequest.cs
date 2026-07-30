using System;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Mono.Net.Security;
using Mono.Security.Interface;
using Unity;

namespace System.Net
{
	/// <summary>Implements a File Transfer Protocol (FTP) client.</summary>
	// Token: 0x02000519 RID: 1305
	public sealed class FtpWebRequest : WebRequest
	{
		// Token: 0x0600272D RID: 10029 RVA: 0x000970E4 File Offset: 0x000952E4
		internal FtpWebRequest(Uri uri)
		{
			this.timeout = 100000;
			this.rwTimeout = 300000;
			this.binary = true;
			this.usePassive = true;
			this.method = "RETR";
			this.locker = new object();
			this.dataEncoding = Encoding.UTF8;
			base..ctor();
			this.requestUri = uri;
			this.proxy = GlobalProxySelection.Select;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x00093A0B File Offset: 0x00091C0B
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets the certificates used for establishing an encrypted connection to the FTP server.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> object that contains the client certificates.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x0009714E File Offset: 0x0009534E
		// (set) Token: 0x06002730 RID: 10032 RVA: 0x0009714E File Offset: 0x0009534E
		[MonoTODO]
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the name of the connection group that contains the service point used to send the current request.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains a connection group name.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x0009714E File Offset: 0x0009534E
		// (set) Token: 0x06002732 RID: 10034 RVA: 0x0009714E File Offset: 0x0009534E
		[MonoTODO]
		public override string ConnectionGroupName
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Content type information is not supported for FTP.</exception>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x000074E4 File Offset: 0x000056E4
		// (set) Token: 0x06002734 RID: 10036 RVA: 0x000074E4 File Offset: 0x000056E4
		public override string ContentType
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that is ignored by the <see cref="T:System.Net.FtpWebRequest" /> class.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that should be ignored.</returns>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x00045828 File Offset: 0x00043A28
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x000027E8 File Offset: 0x000009E8
		public override long ContentLength
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a byte offset into the file being downloaded by this request.</summary>
		/// <returns>An <see cref="T:System.Int64" /> instance that specifies the file offset, in bytes. The default value is zero.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for this property is less than zero. </exception>
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x00097155 File Offset: 0x00095355
		// (set) Token: 0x06002738 RID: 10040 RVA: 0x0009715D File Offset: 0x0009535D
		public long ContentOffset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.offset = value;
			}
		}

		/// <summary>Gets or sets the credentials used to communicate with the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance; otherwise, null if the property has not been set.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">An <see cref="T:System.Net.ICredentials" /> of a type other than <see cref="T:System.Net.NetworkCredential" /> was specified for a set operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x00097177 File Offset: 0x00095377
		// (set) Token: 0x0600273A RID: 10042 RVA: 0x0009717F File Offset: 0x0009537F
		public override ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (!(value is NetworkCredential))
				{
					throw new ArgumentException();
				}
				this.credentials = value as NetworkCredential;
			}
		}

		/// <summary>Defines the default cache policy for all FTP requests.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> that defines the cache policy for FTP requests.</returns>
		/// <exception cref="T:System.ArgumentNullException">The caller tried to set this property to null.</exception>
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x0009714E File Offset: 0x0009534E
		// (set) Token: 0x0600273C RID: 10044 RVA: 0x0009714E File Offset: 0x0009534E
		[MonoTODO]
		public new static RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> that specifies that an SSL connection should be used.</summary>
		/// <returns>true if control and data transmissions are encrypted; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection to the FTP server has already been established.</exception>
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x000971AA File Offset: 0x000953AA
		// (set) Token: 0x0600273E RID: 10046 RVA: 0x000971B2 File Offset: 0x000953B2
		public bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
			set
			{
				this.CheckRequestStarted();
				this.enableSsl = value;
			}
		}

		/// <summary>Gets an empty <see cref="T:System.Net.WebHeaderCollection" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Net.WebHeaderCollection" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x0009714E File Offset: 0x0009534E
		// (set) Token: 0x06002740 RID: 10048 RVA: 0x0009714E File Offset: 0x0009534E
		[MonoTODO]
		public override WebHeaderCollection Headers
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the control connection to the FTP server is closed after the request completes.</summary>
		/// <returns>true if the connection to the server should not be destroyed; otherwise, false. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x000971C1 File Offset: 0x000953C1
		// (set) Token: 0x06002742 RID: 10050 RVA: 0x000971C9 File Offset: 0x000953C9
		[MonoTODO("We don't support KeepAlive = true")]
		public bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
			set
			{
				this.CheckRequestStarted();
			}
		}

		/// <summary>Gets or sets the command to send to the FTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the FTP command to send to the server. The default value is <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <exception cref="T:System.ArgumentException">The method is invalid.- or -The method is not supported.- or -Multiple methods were specified.</exception>
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x000971D1 File Offset: 0x000953D1
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x000971DC File Offset: 0x000953DC
		public override string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null)
				{
					throw new ArgumentNullException("Method string cannot be null");
				}
				if (value.Length == 0 || Array.BinarySearch<string>(FtpWebRequest.supportedCommands, value) < 0)
				{
					throw new ArgumentException("Method not supported", "value");
				}
				this.method = value;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Preauthentication is not supported for FTP.</exception>
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x000074E4 File Offset: 0x000056E4
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x000074E4 File Offset: 0x000056E4
		public override bool PreAuthenticate
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the proxy used to communicate with the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> instance responsible for communicating with the FTP server.</returns>
		/// <exception cref="T:System.ArgumentNullException">This property cannot be set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x0009722A File Offset: 0x0009542A
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x00097232 File Offset: 0x00095432
		public override IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.CheckRequestStarted();
				this.proxy = value;
			}
		}

		/// <summary>Gets or sets a time-out when reading from or writing to a stream.</summary>
		/// <returns>The number of milliseconds before the reading or writing times out. The default value is 300,000 milliseconds (5 minutes).</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has already been sent. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x00097241 File Offset: 0x00095441
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x00097249 File Offset: 0x00095449
		public int ReadWriteTimeout
		{
			get
			{
				return this.rwTimeout;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.rwTimeout = value;
			}
		}

		/// <summary>Gets or sets the new name of a file being renamed.</summary>
		/// <returns>The new name of the file being renamed.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is null or an empty string.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x00097262 File Offset: 0x00095462
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x0009726A File Offset: 0x0009546A
		public string RenameTo
		{
			get
			{
				return this.renameTo;
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("RenameTo value can't be null or empty", "RenameTo");
				}
				this.renameTo = value;
			}
		}

		/// <summary>Gets the URI requested by this instance.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that identifies a resource that is accessed using the File Transfer Protocol.</returns>
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x00097294 File Offset: 0x00095494
		public override Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.ServicePoint" /> object used to connect to the FTP server.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> object that can be used to customize connection behavior.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x0009729C File Offset: 0x0009549C
		public ServicePoint ServicePoint
		{
			get
			{
				return this.GetServicePoint();
			}
		}

		/// <summary>Gets or sets the behavior of a client application's data transfer process.</summary>
		/// <returns>false if the client application's data transfer process listens for a connection on the data port; otherwise, true if the client should initiate a connection on the data port. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x000972A4 File Offset: 0x000954A4
		// (set) Token: 0x06002750 RID: 10064 RVA: 0x000972AC File Offset: 0x000954AC
		public bool UsePassive
		{
			get
			{
				return this.usePassive;
			}
			set
			{
				this.CheckRequestStarted();
				this.usePassive = value;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Default credentials are not supported for FTP.</exception>
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x0009714E File Offset: 0x0009534E
		// (set) Token: 0x06002752 RID: 10066 RVA: 0x0009714E File Offset: 0x0009534E
		[MonoTODO]
		public override bool UseDefaultCredentials
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies the data type for file transfers.</summary>
		/// <returns>true to indicate to the server that the data to be transferred is binary; false to indicate that the data is text. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress.</exception>
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x000972BB File Offset: 0x000954BB
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x000972C3 File Offset: 0x000954C3
		public bool UseBinary
		{
			get
			{
				return this.binary;
			}
			set
			{
				this.CheckRequestStarted();
				this.binary = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds to wait for a request.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of milliseconds to wait before a request times out. The default value is <see cref="F:System.Threading.Timeout.Infinite" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000972D2 File Offset: 0x000954D2
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x000972DA File Offset: 0x000954DA
		public override int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.timeout = value;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000972F3 File Offset: 0x000954F3
		private string DataType
		{
			get
			{
				if (!this.binary)
				{
					return "A";
				}
				return "I";
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x00097308 File Offset: 0x00095508
		// (set) Token: 0x06002759 RID: 10073 RVA: 0x0009734C File Offset: 0x0009554C
		private FtpWebRequest.RequestState State
		{
			get
			{
				object obj = this.locker;
				FtpWebRequest.RequestState requestState;
				lock (obj)
				{
					requestState = this.requestState;
				}
				return requestState;
			}
			set
			{
				object obj = this.locker;
				lock (obj)
				{
					this.CheckIfAborted();
					this.CheckFinalState();
					this.requestState = value;
				}
			}
		}

		/// <summary>Terminates an asynchronous FTP operation.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275A RID: 10074 RVA: 0x0009739C File Offset: 0x0009559C
		public override void Abort()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.State == FtpWebRequest.RequestState.TransferInProgress)
				{
					this.SendCommand(false, "ABOR", Array.Empty<string>());
				}
				if (!this.InFinalState())
				{
					this.State = FtpWebRequest.RequestState.Aborted;
					this.ftpResponse = new FtpWebResponse(this, this.requestUri, this.method, FtpStatusCode.FileActionAborted, "Aborted by request");
				}
			}
		}

		/// <summary>Begins sending a request and receiving a response from an FTP server asynchronously.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that indicates the status of the operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.GetResponse" /> or <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> has already been called for this instance. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275B RID: 10075 RVA: 0x00097424 File Offset: 0x00095624
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			if (this.asyncResult != null && !this.asyncResult.IsCompleted)
			{
				throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress");
			}
			this.CheckIfAborted();
			this.asyncResult = new FtpAsyncResult(callback, state);
			object obj = this.locker;
			lock (obj)
			{
				if (this.InFinalState())
				{
					this.asyncResult.SetCompleted(true, this.ftpResponse);
				}
				else
				{
					if (this.State == FtpWebRequest.RequestState.Before)
					{
						this.State = FtpWebRequest.RequestState.Scheduled;
					}
					new Thread(new ThreadStart(this.ProcessRequest))
					{
						IsBackground = true
					}.Start();
				}
			}
			return this.asyncResult;
		}

		/// <summary>Ends a pending asynchronous operation started with <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> reference that contains an <see cref="T:System.Net.FtpWebResponse" /> instance. This object contains the FTP server's response to the request.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that was returned when the operation started. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		/// <exception cref="T:System.Net.WebException">An error occurred using an HTTP proxy. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275C RID: 10076 RVA: 0x000974E0 File Offset: 0x000956E0
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("AsyncResult cannot be null!");
			}
			if (!(asyncResult is FtpAsyncResult) || asyncResult != this.asyncResult)
			{
				throw new ArgumentException("AsyncResult is from another request!");
			}
			FtpAsyncResult ftpAsyncResult = (FtpAsyncResult)asyncResult;
			if (!ftpAsyncResult.WaitUntilComplete(this.timeout, false))
			{
				this.Abort();
				throw new WebException("Transfer timed out.", WebExceptionStatus.Timeout);
			}
			this.CheckIfAborted();
			asyncResult = null;
			if (ftpAsyncResult.GotException)
			{
				throw ftpAsyncResult.Exception;
			}
			return ftpAsyncResult.Response;
		}

		/// <summary>Returns the FTP server response.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> reference that contains an <see cref="T:System.Net.FtpWebResponse" /> instance. This object contains the FTP server's response to the request.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.GetResponse" /> or <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> has already been called for this instance.- or -An HTTP proxy is enabled, and you attempted to use an FTP command other than <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />, <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectory" />, or <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectoryDetails" />.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="P:System.Net.FtpWebRequest.EnableSsl" /> is set to true, but the server does not support this feature.- or -A <see cref="P:System.Net.FtpWebRequest.Timeout" /> was specified and the timeout has expired.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275D RID: 10077 RVA: 0x00097560 File Offset: 0x00095760
		public override WebResponse GetResponse()
		{
			IAsyncResult asyncResult = this.BeginGetResponse(null, null);
			return this.EndGetResponse(asyncResult);
		}

		/// <summary>Begins asynchronously opening a request's content stream for writing.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that indicates the status of the operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes. </param>
		/// <exception cref="T:System.InvalidOperationException">A previous call to this method or <see cref="M:System.Net.FtpWebRequest.GetRequestStream" /> has not yet completed. </exception>
		/// <exception cref="T:System.Net.WebException">A connection to the FTP server could not be established. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FtpWebRequest.Method" /> property is not set to <see cref="F:System.Net.WebRequestMethods.Ftp.UploadFile" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275E RID: 10078 RVA: 0x00097580 File Offset: 0x00095780
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			if (this.method != "STOR" && this.method != "STOU" && this.method != "APPE")
			{
				throw new ProtocolViolationException();
			}
			object obj = this.locker;
			lock (obj)
			{
				this.CheckIfAborted();
				if (this.State != FtpWebRequest.RequestState.Before)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress");
				}
				this.State = FtpWebRequest.RequestState.Scheduled;
			}
			this.asyncResult = new FtpAsyncResult(callback, state);
			new Thread(new ThreadStart(this.ProcessRequest))
			{
				IsBackground = true
			}.Start();
			return this.asyncResult;
		}

		/// <summary>Ends a pending asynchronous operation started with <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A writable <see cref="T:System.IO.Stream" /> instance associated with this instance.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> object that was returned when the operation started. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275F RID: 10079 RVA: 0x00097648 File Offset: 0x00095848
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!(asyncResult is FtpAsyncResult))
			{
				throw new ArgumentException("asyncResult");
			}
			if (this.State == FtpWebRequest.RequestState.Aborted)
			{
				throw new WebException("Request aborted", WebExceptionStatus.RequestCanceled);
			}
			if (asyncResult != this.asyncResult)
			{
				throw new ArgumentException("AsyncResult is from another request!");
			}
			FtpAsyncResult ftpAsyncResult = (FtpAsyncResult)asyncResult;
			if (!ftpAsyncResult.WaitUntilComplete(this.timeout, false))
			{
				this.Abort();
				throw new WebException("Request timed out");
			}
			if (ftpAsyncResult.GotException)
			{
				throw ftpAsyncResult.Exception;
			}
			return ftpAsyncResult.Stream;
		}

		/// <summary>Retrieves the stream used to upload data to an FTP server.</summary>
		/// <returns>A writable <see cref="T:System.IO.Stream" /> instance used to store data to be sent to the server by the current request.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" /> has been called and has not completed. - or -An HTTP proxy is enabled, and you attempted to use an FTP command other than <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />, <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectory" />, or <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectoryDetails" />.</exception>
		/// <exception cref="T:System.Net.WebException">A connection to the FTP server could not be established. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FtpWebRequest.Method" /> property is not set to <see cref="F:System.Net.WebRequestMethods.Ftp.UploadFile" /> or <see cref="F:System.Net.WebRequestMethods.Ftp.AppendFile" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002760 RID: 10080 RVA: 0x000976DC File Offset: 0x000958DC
		public override Stream GetRequestStream()
		{
			IAsyncResult asyncResult = this.BeginGetRequestStream(null, null);
			return this.EndGetRequestStream(asyncResult);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000976F9 File Offset: 0x000958F9
		private ServicePoint GetServicePoint()
		{
			if (this.servicePoint == null)
			{
				this.servicePoint = ServicePointManager.FindServicePoint(this.requestUri, this.proxy);
			}
			return this.servicePoint;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00097720 File Offset: 0x00095920
		private void ResolveHost()
		{
			this.CheckIfAborted();
			this.hostEntry = this.GetServicePoint().HostEntry;
			if (this.hostEntry == null)
			{
				this.ftpResponse.UpdateStatus(new FtpStatus(FtpStatusCode.ActionAbortedLocalProcessingError, "Cannot resolve server name"));
				throw new WebException("The remote server name could not be resolved: " + this.requestUri, null, WebExceptionStatus.NameResolutionFailure, this.ftpResponse);
			}
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00097784 File Offset: 0x00095984
		private void ProcessRequest()
		{
			if (this.State == FtpWebRequest.RequestState.Scheduled)
			{
				this.ftpResponse = new FtpWebResponse(this, this.requestUri, this.method, this.keepAlive);
				try
				{
					this.ProcessMethod();
					this.asyncResult.SetCompleted(false, this.ftpResponse);
					return;
				}
				catch (Exception ex)
				{
					if (!this.GetServicePoint().UsesProxy)
					{
						this.State = FtpWebRequest.RequestState.Error;
					}
					this.SetCompleteWithError(ex);
					return;
				}
			}
			if (this.InProgress())
			{
				FtpStatus responseStatus = this.GetResponseStatus();
				this.ftpResponse.UpdateStatus(responseStatus);
				if (this.ftpResponse.IsFinal())
				{
					this.State = FtpWebRequest.RequestState.Finished;
				}
			}
			this.asyncResult.SetCompleted(false, this.ftpResponse);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00097844 File Offset: 0x00095A44
		private void SetType()
		{
			if (this.binary)
			{
				FtpStatus ftpStatus = this.SendCommand("TYPE", new string[] { this.DataType });
				if (ftpStatus.StatusCode < FtpStatusCode.CommandOK || ftpStatus.StatusCode >= (FtpStatusCode)300)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00097898 File Offset: 0x00095A98
		private string GetRemoteFolderPath(Uri uri)
		{
			string text = Uri.UnescapeDataString(uri.LocalPath);
			string text2;
			if (this.initial_path == null || this.initial_path == "/")
			{
				text2 = text;
			}
			else
			{
				if (text[0] == '/')
				{
					text = text.Substring(1);
				}
				text2 = new Uri(new UriBuilder
				{
					Scheme = "ftp",
					Host = "dummy-host",
					Path = this.initial_path
				}.Uri, text).LocalPath;
			}
			int num = text2.LastIndexOf('/');
			if (num == -1)
			{
				return null;
			}
			return text2.Substring(0, num + 1);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00097934 File Offset: 0x00095B34
		private void CWDAndSetFileName(Uri uri)
		{
			string remoteFolderPath = this.GetRemoteFolderPath(uri);
			if (remoteFolderPath != null)
			{
				FtpStatus ftpStatus = this.SendCommand("CWD", new string[] { remoteFolderPath });
				if (ftpStatus.StatusCode < FtpStatusCode.CommandOK || ftpStatus.StatusCode >= (FtpStatusCode)300)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				int num = uri.LocalPath.LastIndexOf('/');
				if (num >= 0)
				{
					this.file_name = Uri.UnescapeDataString(uri.LocalPath.Substring(num + 1));
				}
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000979B0 File Offset: 0x00095BB0
		private void ProcessMethod()
		{
			if (!this.GetServicePoint().UsesProxy)
			{
				this.State = FtpWebRequest.RequestState.Connecting;
				this.ResolveHost();
				this.OpenControlConnection();
				this.CWDAndSetFileName(this.requestUri);
				this.SetType();
				string text = this.method;
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1636987420U)
				{
					if (num <= 172932033U)
					{
						if (num != 61167622U)
						{
							if (num != 111500479U)
							{
								if (num != 172932033U)
								{
									goto IL_0248;
								}
								if (!(text == "LIST"))
								{
									goto IL_0248;
								}
							}
							else
							{
								if (!(text == "STOR"))
								{
									goto IL_0248;
								}
								goto IL_0238;
							}
						}
						else
						{
							if (!(text == "STOU"))
							{
								goto IL_0248;
							}
							goto IL_0238;
						}
					}
					else if (num != 540800083U)
					{
						if (num != 1414193175U)
						{
							if (num != 1636987420U)
							{
								goto IL_0248;
							}
							if (!(text == "SIZE"))
							{
								goto IL_0248;
							}
							goto IL_0240;
						}
						else
						{
							if (!(text == "MKD"))
							{
								goto IL_0248;
							}
							goto IL_0240;
						}
					}
					else
					{
						if (!(text == "RENAME"))
						{
							goto IL_0248;
						}
						goto IL_0240;
					}
				}
				else if (num <= 2586094756U)
				{
					if (num != 2190452587U)
					{
						if (num != 2192893693U)
						{
							if (num != 2586094756U)
							{
								goto IL_0248;
							}
							if (!(text == "PWD"))
							{
								goto IL_0248;
							}
							goto IL_0240;
						}
						else
						{
							if (!(text == "DELE"))
							{
								goto IL_0248;
							}
							goto IL_0240;
						}
					}
					else
					{
						if (!(text == "APPE"))
						{
							goto IL_0248;
						}
						goto IL_0238;
					}
				}
				else if (num != 3129138359U)
				{
					if (num != 3960558266U)
					{
						if (num != 4117911256U)
						{
							goto IL_0248;
						}
						if (!(text == "NLST"))
						{
							goto IL_0248;
						}
					}
					else if (!(text == "RETR"))
					{
						goto IL_0248;
					}
				}
				else
				{
					if (!(text == "MDTM"))
					{
						goto IL_0248;
					}
					goto IL_0240;
				}
				this.DownloadData();
				goto IL_025E;
				IL_0238:
				this.UploadData();
				goto IL_025E;
				IL_0240:
				this.ProcessSimpleMethod();
				goto IL_025E;
				IL_0248:
				throw new Exception(string.Format("Support for command {0} not implemented yet", this.method));
				IL_025E:
				this.CheckIfAborted();
				return;
			}
			if (this.method != "RETR")
			{
				throw new NotSupportedException("FTP+proxy only supports RETR");
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.proxy.GetProxy(this.requestUri));
			httpWebRequest.Address = this.requestUri;
			this.requestState = FtpWebRequest.RequestState.Finished;
			WebResponse response = httpWebRequest.GetResponse();
			this.ftpResponse.Stream = new FtpDataStream(this, response.GetResponseStream(), true);
			this.ftpResponse.StatusCode = FtpStatusCode.CommandOK;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00097C21 File Offset: 0x00095E21
		private void CloseControlConnection()
		{
			if (this.controlStream != null)
			{
				this.SendCommand("QUIT", Array.Empty<string>());
				this.controlStream.Close();
				this.controlStream = null;
			}
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00097C4E File Offset: 0x00095E4E
		internal void CloseDataConnection()
		{
			if (this.origDataStream != null)
			{
				this.origDataStream.Close();
				this.origDataStream = null;
			}
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x00097C6A File Offset: 0x00095E6A
		private void CloseConnection()
		{
			this.CloseControlConnection();
			this.CloseDataConnection();
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00097C78 File Offset: 0x00095E78
		private void ProcessSimpleMethod()
		{
			this.State = FtpWebRequest.RequestState.TransferInProgress;
			if (this.method == "PWD")
			{
				this.method = "PWD";
			}
			if (this.method == "RENAME")
			{
				this.method = "RNFR";
			}
			FtpStatus ftpStatus = this.SendCommand(this.method, new string[] { this.file_name });
			this.ftpResponse.Stream = Stream.Null;
			string statusDescription = ftpStatus.StatusDescription;
			string text = this.method;
			if (!(text == "SIZE"))
			{
				if (!(text == "MDTM"))
				{
					if (!(text == "MKD"))
					{
						if (!(text == "CWD"))
						{
							if (!(text == "RNFR"))
							{
								if (text == "DELE")
								{
									if (ftpStatus.StatusCode != FtpStatusCode.FileActionOK)
									{
										throw this.CreateExceptionFromResponse(ftpStatus);
									}
								}
							}
							else
							{
								this.method = "RENAME";
								if (ftpStatus.StatusCode != FtpStatusCode.FileCommandPending)
								{
									throw this.CreateExceptionFromResponse(ftpStatus);
								}
								ftpStatus = this.SendCommand("RNTO", new string[] { (this.renameTo != null) ? this.renameTo : string.Empty });
								if (ftpStatus.StatusCode != FtpStatusCode.FileActionOK)
								{
									throw this.CreateExceptionFromResponse(ftpStatus);
								}
							}
						}
						else
						{
							this.method = "PWD";
							if (ftpStatus.StatusCode != FtpStatusCode.FileActionOK)
							{
								throw this.CreateExceptionFromResponse(ftpStatus);
							}
							ftpStatus = this.SendCommand(this.method, Array.Empty<string>());
							if (ftpStatus.StatusCode != FtpStatusCode.PathnameCreated)
							{
								throw this.CreateExceptionFromResponse(ftpStatus);
							}
						}
					}
					else if (ftpStatus.StatusCode != FtpStatusCode.PathnameCreated)
					{
						throw this.CreateExceptionFromResponse(ftpStatus);
					}
				}
				else
				{
					if (ftpStatus.StatusCode != FtpStatusCode.FileStatus)
					{
						throw this.CreateExceptionFromResponse(ftpStatus);
					}
					this.ftpResponse.LastModified = DateTime.ParseExact(statusDescription.Substring(4), "yyyyMMddHHmmss", null);
				}
			}
			else
			{
				if (ftpStatus.StatusCode != FtpStatusCode.FileStatus)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				int num = 4;
				int num2 = 0;
				while (num < statusDescription.Length && char.IsDigit(statusDescription[num]))
				{
					num++;
					num2++;
				}
				if (num2 == 0)
				{
					throw new WebException("Bad format for server response in " + this.method);
				}
				long num3;
				if (!long.TryParse(statusDescription.Substring(4, num2), out num3))
				{
					throw new WebException("Bad format for server response in " + this.method);
				}
				this.ftpResponse.contentLength = num3;
			}
			this.State = FtpWebRequest.RequestState.Finished;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x00097F04 File Offset: 0x00096104
		private void UploadData()
		{
			this.State = FtpWebRequest.RequestState.OpeningData;
			this.OpenDataConnection();
			this.State = FtpWebRequest.RequestState.TransferInProgress;
			this.requestStream = new FtpDataStream(this, this.dataStream, false);
			this.asyncResult.Stream = this.requestStream;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00097F3E File Offset: 0x0009613E
		private void DownloadData()
		{
			this.State = FtpWebRequest.RequestState.OpeningData;
			this.OpenDataConnection();
			this.State = FtpWebRequest.RequestState.TransferInProgress;
			this.ftpResponse.Stream = new FtpDataStream(this, this.dataStream, true);
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x00097F6C File Offset: 0x0009616C
		private void CheckRequestStarted()
		{
			if (this.State != FtpWebRequest.RequestState.Before)
			{
				throw new InvalidOperationException("There is a request currently in progress");
			}
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x00097F84 File Offset: 0x00096184
		private void OpenControlConnection()
		{
			Exception ex = null;
			Socket socket = null;
			foreach (IPAddress ipaddress in this.hostEntry.AddressList)
			{
				socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				this.remoteEndPoint = new IPEndPoint(ipaddress, this.requestUri.Port);
				if (!this.ServicePoint.CallEndPointDelegate(socket, this.remoteEndPoint))
				{
					socket.Close();
					socket = null;
				}
				else
				{
					try
					{
						socket.Connect(this.remoteEndPoint);
						this.localEndPoint = (IPEndPoint)socket.LocalEndPoint;
						break;
					}
					catch (SocketException ex)
					{
						socket.Close();
						socket = null;
					}
				}
			}
			if (socket == null)
			{
				throw new WebException("Unable to connect to remote server", ex, WebExceptionStatus.UnknownError, this.ftpResponse);
			}
			this.controlStream = new NetworkStream(socket);
			this.controlReader = new StreamReader(this.controlStream, Encoding.ASCII);
			this.State = FtpWebRequest.RequestState.Authenticating;
			this.Authenticate();
			FtpStatus ftpStatus = this.SendCommand("OPTS", new string[] { "utf8", "on" });
			if (ftpStatus.StatusCode < FtpStatusCode.CommandOK || ftpStatus.StatusCode > (FtpStatusCode)300)
			{
				this.dataEncoding = Encoding.Default;
			}
			else
			{
				this.dataEncoding = Encoding.UTF8;
			}
			ftpStatus = this.SendCommand("PWD", Array.Empty<string>());
			this.initial_path = FtpWebRequest.GetInitialPath(ftpStatus);
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000980F4 File Offset: 0x000962F4
		private static string GetInitialPath(FtpStatus status)
		{
			int statusCode = (int)status.StatusCode;
			if (statusCode < 200 || statusCode > 300 || status.StatusDescription.Length <= 4)
			{
				throw new WebException("Error getting current directory: " + status.StatusDescription, null, WebExceptionStatus.UnknownError, null);
			}
			string text = status.StatusDescription.Substring(4);
			if (text[0] == '"')
			{
				int num = text.IndexOf('"', 1);
				if (num == -1)
				{
					throw new WebException("Error getting current directory: PWD -> " + status.StatusDescription, null, WebExceptionStatus.UnknownError, null);
				}
				text = text.Substring(1, num - 1);
			}
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			return text;
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000981A8 File Offset: 0x000963A8
		private Socket SetupPassiveConnection(string statusDescription, bool ipv6)
		{
			if (statusDescription.Length < 4)
			{
				throw new WebException("Cannot open passive data connection");
			}
			int num = (ipv6 ? this.GetPortV6(statusDescription) : this.GetPortV4(statusDescription));
			if (num < 0 || num > 65535)
			{
				throw new WebException("Cannot open passive data connection");
			}
			IPEndPoint ipendPoint = new IPEndPoint(this.remoteEndPoint.Address, num);
			Socket socket = new Socket(ipendPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				socket.Connect(ipendPoint);
			}
			catch (SocketException)
			{
				socket.Close();
				throw new WebException("Cannot open passive data connection");
			}
			return socket;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00098244 File Offset: 0x00096444
		private int GetPortV4(string responseString)
		{
			string[] array = responseString.Split(new char[] { ' ', '(', ',', ')' });
			if (array.Length <= 7)
			{
				throw new FormatException(global::SR.GetString("The response string '{0}' has invalid format.", new object[] { responseString }));
			}
			int num = array.Length - 1;
			if (array[num] == "" || !char.IsNumber(array[num], 0))
			{
				num--;
			}
			return (int)Convert.ToByte(array[num--], NumberFormatInfo.InvariantInfo) | ((int)Convert.ToByte(array[num--], NumberFormatInfo.InvariantInfo) << 8);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000982D4 File Offset: 0x000964D4
		private int GetPortV6(string responseString)
		{
			int num = responseString.LastIndexOf("(");
			int num2 = responseString.LastIndexOf(")");
			if (num == -1 || num2 <= num)
			{
				throw new FormatException(global::SR.GetString("The response string '{0}' has invalid format.", new object[] { responseString }));
			}
			string[] array = responseString.Substring(num + 1, num2 - num - 1).Split(new char[] { '|' });
			if (array.Length < 4)
			{
				throw new FormatException(global::SR.GetString("The response string '{0}' has invalid format.", new object[] { responseString }));
			}
			return Convert.ToInt32(array[3], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00098368 File Offset: 0x00096568
		private string FormatAddress(IPAddress address, int Port)
		{
			byte[] addressBytes = address.GetAddressBytes();
			StringBuilder stringBuilder = new StringBuilder(32);
			foreach (byte b in addressBytes)
			{
				stringBuilder.Append(b);
				stringBuilder.Append(',');
			}
			stringBuilder.Append(Port / 256);
			stringBuilder.Append(',');
			stringBuilder.Append(Port % 256);
			return stringBuilder.ToString();
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000983D4 File Offset: 0x000965D4
		private string FormatAddressV6(IPAddress address, int port)
		{
			StringBuilder stringBuilder = new StringBuilder(43);
			string text = address.ToString();
			stringBuilder.Append("|2|");
			stringBuilder.Append(text);
			stringBuilder.Append('|');
			stringBuilder.Append(port.ToString(NumberFormatInfo.InvariantInfo));
			stringBuilder.Append('|');
			return stringBuilder.ToString();
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x00098430 File Offset: 0x00096630
		private Exception CreateExceptionFromResponse(FtpStatus status)
		{
			FtpWebResponse ftpWebResponse = new FtpWebResponse(this, this.requestUri, this.method, status);
			return new WebException("Server returned an error: " + status.StatusDescription, null, WebExceptionStatus.ProtocolError, ftpWebResponse);
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0009846C File Offset: 0x0009666C
		internal void SetTransferCompleted()
		{
			if (this.InFinalState())
			{
				return;
			}
			this.State = FtpWebRequest.RequestState.Finished;
			FtpStatus responseStatus = this.GetResponseStatus();
			this.ftpResponse.UpdateStatus(responseStatus);
			if (!this.keepAlive)
			{
				this.CloseConnection();
			}
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000984AA File Offset: 0x000966AA
		internal void OperationCompleted()
		{
			if (!this.keepAlive)
			{
				this.CloseConnection();
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000984BA File Offset: 0x000966BA
		private void SetCompleteWithError(Exception exc)
		{
			if (this.asyncResult != null)
			{
				this.asyncResult.SetCompleted(false, exc);
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000984D4 File Offset: 0x000966D4
		private Socket InitDataConnection()
		{
			bool flag = this.remoteEndPoint.AddressFamily == AddressFamily.InterNetworkV6;
			if (this.usePassive)
			{
				FtpStatus ftpStatus = this.SendCommand(flag ? "EPSV" : "PASV", Array.Empty<string>());
				if (ftpStatus.StatusCode != (flag ? ((FtpStatusCode)229) : FtpStatusCode.EnteringPassive))
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				return this.SetupPassiveConnection(ftpStatus.StatusDescription, flag);
			}
			else
			{
				Socket socket = new Socket(this.remoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				try
				{
					socket.Bind(new IPEndPoint(this.localEndPoint.Address, 0));
					socket.Listen(1);
				}
				catch (SocketException ex)
				{
					socket.Close();
					throw new WebException("Couldn't open listening socket on client", ex);
				}
				IPEndPoint ipendPoint = (IPEndPoint)socket.LocalEndPoint;
				string text = (flag ? this.FormatAddressV6(ipendPoint.Address, ipendPoint.Port) : this.FormatAddress(ipendPoint.Address, ipendPoint.Port));
				FtpStatus ftpStatus = this.SendCommand(flag ? "EPRT" : "PORT", new string[] { text });
				if (ftpStatus.StatusCode != FtpStatusCode.CommandOK)
				{
					socket.Close();
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				return socket;
			}
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x00098610 File Offset: 0x00096810
		private void OpenDataConnection()
		{
			Socket socket = this.InitDataConnection();
			FtpStatus ftpStatus;
			if (this.offset > 0L)
			{
				ftpStatus = this.SendCommand("REST", new string[] { this.offset.ToString() });
				if (ftpStatus.StatusCode != FtpStatusCode.FileCommandPending)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
			}
			if (this.method != "NLST" && this.method != "LIST" && this.method != "STOU")
			{
				ftpStatus = this.SendCommand(this.method, new string[] { this.file_name });
			}
			else
			{
				ftpStatus = this.SendCommand(this.method, Array.Empty<string>());
			}
			if (ftpStatus.StatusCode != FtpStatusCode.OpeningData && ftpStatus.StatusCode != FtpStatusCode.DataAlreadyOpen)
			{
				throw this.CreateExceptionFromResponse(ftpStatus);
			}
			if (this.usePassive)
			{
				this.origDataStream = new NetworkStream(socket, true);
				this.dataStream = this.origDataStream;
				if (this.EnableSsl)
				{
					this.ChangeToSSLSocket(ref this.dataStream);
				}
			}
			else
			{
				Socket socket2 = null;
				try
				{
					socket2 = socket.Accept();
				}
				catch (SocketException)
				{
					socket.Close();
					if (socket2 != null)
					{
						socket2.Close();
					}
					throw new ProtocolViolationException("Server commited a protocol violation.");
				}
				socket.Close();
				this.origDataStream = new NetworkStream(socket2, true);
				this.dataStream = this.origDataStream;
				if (this.EnableSsl)
				{
					this.ChangeToSSLSocket(ref this.dataStream);
				}
			}
			this.ftpResponse.UpdateStatus(ftpStatus);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x00098798 File Offset: 0x00096998
		private void Authenticate()
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			if (this.credentials != null)
			{
				text = this.credentials.UserName;
				text2 = this.credentials.Password;
				text3 = this.credentials.Domain;
			}
			if (text == null)
			{
				text = "anonymous";
			}
			if (text2 == null)
			{
				text2 = "@anonymous";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text = text3 + "\\" + text;
			}
			FtpStatus ftpStatus = this.GetResponseStatus();
			this.ftpResponse.BannerMessage = ftpStatus.StatusDescription;
			if (this.EnableSsl)
			{
				this.InitiateSecureConnection(ref this.controlStream);
				this.controlReader = new StreamReader(this.controlStream, Encoding.ASCII);
				ftpStatus = this.SendCommand("PBSZ", new string[] { "0" });
				int num = (int)ftpStatus.StatusCode;
				if (num < 200 || num >= 300)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				ftpStatus = this.SendCommand("PROT", new string[] { "P" });
				num = (int)ftpStatus.StatusCode;
				if (num < 200 || num >= 300)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				ftpStatus = new FtpStatus(FtpStatusCode.SendUserCommand, "");
			}
			if (ftpStatus.StatusCode != FtpStatusCode.SendUserCommand)
			{
				throw this.CreateExceptionFromResponse(ftpStatus);
			}
			ftpStatus = this.SendCommand("USER", new string[] { text });
			FtpStatusCode statusCode = ftpStatus.StatusCode;
			if (statusCode != FtpStatusCode.LoggedInProceed)
			{
				if (statusCode != FtpStatusCode.SendPasswordCommand)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				ftpStatus = this.SendCommand("PASS", new string[] { text2 });
				if (ftpStatus.StatusCode != FtpStatusCode.LoggedInProceed)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
			}
			this.ftpResponse.WelcomeMessage = ftpStatus.StatusDescription;
			this.ftpResponse.UpdateStatus(ftpStatus);
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00098960 File Offset: 0x00096B60
		private FtpStatus SendCommand(string command, params string[] parameters)
		{
			return this.SendCommand(true, command, parameters);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0009896C File Offset: 0x00096B6C
		private FtpStatus SendCommand(bool waitResponse, string command, params string[] parameters)
		{
			string text = command;
			if (parameters.Length != 0)
			{
				text = text + " " + string.Join(" ", parameters);
			}
			text += "\r\n";
			byte[] bytes = this.dataEncoding.GetBytes(text);
			try
			{
				this.controlStream.Write(bytes, 0, bytes.Length);
			}
			catch (IOException)
			{
				return new FtpStatus(FtpStatusCode.ServiceNotAvailable, "Write failed");
			}
			if (!waitResponse)
			{
				return null;
			}
			FtpStatus responseStatus = this.GetResponseStatus();
			if (this.ftpResponse != null)
			{
				this.ftpResponse.UpdateStatus(responseStatus);
			}
			return responseStatus;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00098A08 File Offset: 0x00096C08
		internal static FtpStatus ServiceNotAvailable()
		{
			return new FtpStatus(FtpStatusCode.ServiceNotAvailable, global::Locale.GetText("Invalid response from server"));
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x00098A20 File Offset: 0x00096C20
		internal FtpStatus GetResponseStatus()
		{
			string text = null;
			try
			{
				text = this.controlReader.ReadLine();
			}
			catch (IOException)
			{
			}
			if (text == null || text.Length < 3)
			{
				return FtpWebRequest.ServiceNotAvailable();
			}
			int num;
			if (!int.TryParse(text.Substring(0, 3), out num))
			{
				return FtpWebRequest.ServiceNotAvailable();
			}
			if (text.Length > 3 && text[3] == '-')
			{
				string text2 = null;
				string text3 = num.ToString() + " ";
				for (;;)
				{
					text2 = null;
					try
					{
						text2 = this.controlReader.ReadLine();
					}
					catch (IOException)
					{
					}
					if (text2 == null)
					{
						break;
					}
					text = text + Environment.NewLine + text2;
					if (text2.StartsWith(text3, StringComparison.Ordinal))
					{
						goto IL_0097;
					}
				}
				return FtpWebRequest.ServiceNotAvailable();
			}
			IL_0097:
			return new FtpStatus((FtpStatusCode)num, text);
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x00098AE8 File Offset: 0x00096CE8
		private void InitiateSecureConnection(ref Stream stream)
		{
			FtpStatus ftpStatus = this.SendCommand("AUTH", new string[] { "TLS" });
			if (ftpStatus.StatusCode != FtpStatusCode.ServerWantsSecureSession)
			{
				throw this.CreateExceptionFromResponse(ftpStatus);
			}
			this.ChangeToSSLSocket(ref stream);
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00098B2C File Offset: 0x00096D2C
		internal bool ChangeToSSLSocket(ref Stream stream)
		{
			MonoTlsProvider providerInternal = Mono.Net.Security.MonoTlsProviderFactory.GetProviderInternal();
			MonoTlsSettings monoTlsSettings = MonoTlsSettings.CopyDefaultSettings();
			monoTlsSettings.UseServicePointManagerCallback = new bool?(true);
			IMonoSslStream monoSslStream = providerInternal.CreateSslStream(stream, true, monoTlsSettings);
			monoSslStream.AuthenticateAsClient(this.requestUri.Host, null, SslProtocols.Default, false);
			stream = monoSslStream.AuthenticatedStream;
			return true;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00098B7B File Offset: 0x00096D7B
		private bool InFinalState()
		{
			return this.State == FtpWebRequest.RequestState.Aborted || this.State == FtpWebRequest.RequestState.Error || this.State == FtpWebRequest.RequestState.Finished;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00098B9A File Offset: 0x00096D9A
		private bool InProgress()
		{
			return this.State != FtpWebRequest.RequestState.Before && !this.InFinalState();
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00098BAF File Offset: 0x00096DAF
		internal void CheckIfAborted()
		{
			if (this.State == FtpWebRequest.RequestState.Aborted)
			{
				throw new WebException("Request aborted", WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x00098BC6 File Offset: 0x00096DC6
		private void CheckFinalState()
		{
			if (this.InFinalState())
			{
				throw new InvalidOperationException("Cannot change final state");
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal FtpWebRequest()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400213F RID: 8511
		private Uri requestUri;

		// Token: 0x04002140 RID: 8512
		private string file_name;

		// Token: 0x04002141 RID: 8513
		private ServicePoint servicePoint;

		// Token: 0x04002142 RID: 8514
		private Stream origDataStream;

		// Token: 0x04002143 RID: 8515
		private Stream dataStream;

		// Token: 0x04002144 RID: 8516
		private Stream controlStream;

		// Token: 0x04002145 RID: 8517
		private StreamReader controlReader;

		// Token: 0x04002146 RID: 8518
		private NetworkCredential credentials;

		// Token: 0x04002147 RID: 8519
		private IPHostEntry hostEntry;

		// Token: 0x04002148 RID: 8520
		private IPEndPoint localEndPoint;

		// Token: 0x04002149 RID: 8521
		private IPEndPoint remoteEndPoint;

		// Token: 0x0400214A RID: 8522
		private IWebProxy proxy;

		// Token: 0x0400214B RID: 8523
		private int timeout;

		// Token: 0x0400214C RID: 8524
		private int rwTimeout;

		// Token: 0x0400214D RID: 8525
		private long offset;

		// Token: 0x0400214E RID: 8526
		private bool binary;

		// Token: 0x0400214F RID: 8527
		private bool enableSsl;

		// Token: 0x04002150 RID: 8528
		private bool usePassive;

		// Token: 0x04002151 RID: 8529
		private bool keepAlive;

		// Token: 0x04002152 RID: 8530
		private string method;

		// Token: 0x04002153 RID: 8531
		private string renameTo;

		// Token: 0x04002154 RID: 8532
		private object locker;

		// Token: 0x04002155 RID: 8533
		private FtpWebRequest.RequestState requestState;

		// Token: 0x04002156 RID: 8534
		private FtpAsyncResult asyncResult;

		// Token: 0x04002157 RID: 8535
		private FtpWebResponse ftpResponse;

		// Token: 0x04002158 RID: 8536
		private Stream requestStream;

		// Token: 0x04002159 RID: 8537
		private string initial_path;

		// Token: 0x0400215A RID: 8538
		private const string ChangeDir = "CWD";

		// Token: 0x0400215B RID: 8539
		private const string UserCommand = "USER";

		// Token: 0x0400215C RID: 8540
		private const string PasswordCommand = "PASS";

		// Token: 0x0400215D RID: 8541
		private const string TypeCommand = "TYPE";

		// Token: 0x0400215E RID: 8542
		private const string PassiveCommand = "PASV";

		// Token: 0x0400215F RID: 8543
		private const string ExtendedPassiveCommand = "EPSV";

		// Token: 0x04002160 RID: 8544
		private const string PortCommand = "PORT";

		// Token: 0x04002161 RID: 8545
		private const string ExtendedPortCommand = "EPRT";

		// Token: 0x04002162 RID: 8546
		private const string AbortCommand = "ABOR";

		// Token: 0x04002163 RID: 8547
		private const string AuthCommand = "AUTH";

		// Token: 0x04002164 RID: 8548
		private const string RestCommand = "REST";

		// Token: 0x04002165 RID: 8549
		private const string RenameFromCommand = "RNFR";

		// Token: 0x04002166 RID: 8550
		private const string RenameToCommand = "RNTO";

		// Token: 0x04002167 RID: 8551
		private const string QuitCommand = "QUIT";

		// Token: 0x04002168 RID: 8552
		private const string EOL = "\r\n";

		// Token: 0x04002169 RID: 8553
		private static readonly string[] supportedCommands = new string[]
		{
			"APPE", "DELE", "LIST", "MDTM", "MKD", "NLST", "PWD", "RENAME", "RETR", "RMD",
			"SIZE", "STOR", "STOU"
		};

		// Token: 0x0400216A RID: 8554
		private Encoding dataEncoding;

		// Token: 0x0200051A RID: 1306
		private enum RequestState
		{
			// Token: 0x0400216C RID: 8556
			Before,
			// Token: 0x0400216D RID: 8557
			Scheduled,
			// Token: 0x0400216E RID: 8558
			Connecting,
			// Token: 0x0400216F RID: 8559
			Authenticating,
			// Token: 0x04002170 RID: 8560
			OpeningData,
			// Token: 0x04002171 RID: 8561
			TransferInProgress,
			// Token: 0x04002172 RID: 8562
			Finished,
			// Token: 0x04002173 RID: 8563
			Aborted,
			// Token: 0x04002174 RID: 8564
			Error
		}
	}
}

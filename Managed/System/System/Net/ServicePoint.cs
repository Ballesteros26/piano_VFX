using System;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using Unity;

namespace System.Net
{
	/// <summary>Provides connection management for HTTP connections.</summary>
	// Token: 0x02000542 RID: 1346
	public class ServicePoint
	{
		// Token: 0x060029B8 RID: 10680 RVA: 0x000A10C7 File Offset: 0x0009F2C7
		internal ServicePoint(Uri uri, int connectionLimit, int maxIdleTime)
		{
			this.sendContinue = true;
			this.hostE = new object();
			base..ctor();
			this.uri = uri;
			this.Scheduler = new ServicePointScheduler(this, connectionLimit, maxIdleTime);
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000A10F6 File Offset: 0x0009F2F6
		internal ServicePointScheduler Scheduler { get; }

		/// <summary>Gets the Uniform Resource Identifier (URI) of the server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</summary>
		/// <returns>An instance of the <see cref="T:System.Uri" /> class that contains the URI of the Internet server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.ServicePoint" /> is in host mode.</exception>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x000A10FE File Offset: 0x0009F2FE
		public Uri Address
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x00093A0B File Offset: 0x00091C0B
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Specifies the delegate to associate a local <see cref="T:System.Net.IPEndPoint" /> with a <see cref="T:System.Net.ServicePoint" />.</summary>
		/// <returns>A delegate that forces a <see cref="T:System.Net.ServicePoint" /> to use a particular local Internet Protocol (IP) address and port number. The default value is null.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x000A1106 File Offset: 0x0009F306
		// (set) Token: 0x060029BD RID: 10685 RVA: 0x000A110E File Offset: 0x0009F30E
		public BindIPEndPoint BindIPEndPointDelegate
		{
			get
			{
				return this.endPointCallback;
			}
			set
			{
				this.endPointCallback = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds after which an active <see cref="T:System.Net.ServicePoint" /> connection is closed.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the number of milliseconds that an active <see cref="T:System.Net.ServicePoint" /> connection remains open. The default is -1, which allows an active <see cref="T:System.Net.ServicePoint" /> connection to stay connected indefinitely. Set this property to 0 to force <see cref="T:System.Net.ServicePoint" /> connections to close after servicing a request.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is a negative number less than -1.</exception>
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060029BE RID: 10686 RVA: 0x000A1117 File Offset: 0x0009F317
		// (set) Token: 0x060029BF RID: 10687 RVA: 0x000A1117 File Offset: 0x0009F317
		[MonoTODO]
		public int ConnectionLeaseTimeout
		{
			get
			{
				throw ServicePoint.GetMustImplement();
			}
			set
			{
				throw ServicePoint.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The connection limit is equal to or less than 0. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x000A111E File Offset: 0x0009F31E
		// (set) Token: 0x060029C1 RID: 10689 RVA: 0x000A112B File Offset: 0x0009F32B
		public int ConnectionLimit
		{
			get
			{
				return this.Scheduler.ConnectionLimit;
			}
			set
			{
				this.Scheduler.ConnectionLimit = value;
			}
		}

		/// <summary>Gets the connection name. </summary>
		/// <returns>A <see cref="T:System.String" /> that represents the connection name. </returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x000A1139 File Offset: 0x0009F339
		public string ConnectionName
		{
			get
			{
				return this.uri.Scheme;
			}
		}

		/// <summary>Gets the number of open connections associated with this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The number of open connections associated with this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000A1146 File Offset: 0x0009F346
		public int CurrentConnections
		{
			get
			{
				return this.Scheduler.CurrentConnections;
			}
		}

		/// <summary>Gets the date and time that the <see cref="T:System.Net.ServicePoint" /> object was last connected to a host.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the date and time at which the <see cref="T:System.Net.ServicePoint" /> object was last connected.</returns>
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000A1154 File Offset: 0x0009F354
		public DateTime IdleSince
		{
			get
			{
				return this.Scheduler.IdleSince.ToLocalTime();
			}
		}

		/// <summary>Gets or sets the amount of time a connection associated with the <see cref="T:System.Net.ServicePoint" /> object can remain idle before the connection is closed.</summary>
		/// <returns>The length of time, in milliseconds, that a connection associated with the <see cref="T:System.Net.ServicePoint" /> object can remain idle before it is closed and reused for another connection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePoint.MaxIdleTime" /> is set to less than <see cref="F:System.Threading.Timeout.Infinite" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000A1174 File Offset: 0x0009F374
		// (set) Token: 0x060029C6 RID: 10694 RVA: 0x000A1181 File Offset: 0x0009F381
		public int MaxIdleTime
		{
			get
			{
				return this.Scheduler.MaxIdleTime;
			}
			set
			{
				this.Scheduler.MaxIdleTime = value;
			}
		}

		/// <summary>Gets the version of the HTTP protocol that the <see cref="T:System.Net.ServicePoint" /> object uses.</summary>
		/// <returns>A <see cref="T:System.Version" /> object that contains the HTTP protocol version that the <see cref="T:System.Net.ServicePoint" /> object uses.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000A118F File Offset: 0x0009F38F
		public virtual Version ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
		}

		/// <summary>Gets or sets the size of the receiving buffer for the socket used by this <see cref="T:System.Net.ServicePoint" />.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the size, in bytes, of the receive buffer. The default is 8192.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000A1117 File Offset: 0x0009F317
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x000A1117 File Offset: 0x0009F317
		[MonoTODO]
		public int ReceiveBufferSize
		{
			get
			{
				throw ServicePoint.GetMustImplement();
			}
			set
			{
				throw ServicePoint.GetMustImplement();
			}
		}

		/// <summary>Indicates whether the <see cref="T:System.Net.ServicePoint" /> object supports pipelined connections.</summary>
		/// <returns>true if the <see cref="T:System.Net.ServicePoint" /> object supports pipelined connections; otherwise, false.</returns>
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x000A1197 File Offset: 0x0009F397
		public bool SupportsPipelining
		{
			get
			{
				return HttpVersion.Version11.Equals(this.protocolVersion);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to expect 100-Continue responses for POST requests; otherwise, false. The default value is true.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060029CB RID: 10699 RVA: 0x000A11A9 File Offset: 0x0009F3A9
		// (set) Token: 0x060029CC RID: 10700 RVA: 0x000A11B1 File Offset: 0x0009F3B1
		public bool Expect100Continue
		{
			get
			{
				return this.SendContinue;
			}
			set
			{
				this.SendContinue = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether the Nagle algorithm is used on connections managed by this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000A11BA File Offset: 0x0009F3BA
		// (set) Token: 0x060029CE RID: 10702 RVA: 0x000A11C2 File Offset: 0x0009F3C2
		public bool UseNagleAlgorithm
		{
			get
			{
				return this.useNagle;
			}
			set
			{
				this.useNagle = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x000A11CB File Offset: 0x0009F3CB
		// (set) Token: 0x060029D0 RID: 10704 RVA: 0x000A11F7 File Offset: 0x0009F3F7
		internal bool SendContinue
		{
			get
			{
				return this.sendContinue && (this.protocolVersion == null || this.protocolVersion == HttpVersion.Version11);
			}
			set
			{
				this.sendContinue = value;
			}
		}

		/// <summary>Enables or disables the keep-alive option on a TCP connection.</summary>
		/// <param name="enabled">If set to true, then the TCP keep-alive option on a TCP connection will be enabled using the specified <paramref name="keepAliveTime " />and <paramref name="keepAliveInterval" /> values. If set to false, then the TCP keep-alive option is disabled and the remaining parameters are ignored.The default value is false.</param>
		/// <param name="keepAliveTime">Specifies the timeout, in milliseconds, with no activity until the first keep-alive packet is sent. The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <param name="keepAliveInterval">Specifies the interval, in milliseconds, between when successive keep-alive packets are sent if no acknowledgement is received.The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for <paramref name="keepAliveTime" /> or <paramref name="keepAliveInterval" /> parameter is less than or equal to 0.</exception>
		// Token: 0x060029D1 RID: 10705 RVA: 0x000A1200 File Offset: 0x0009F400
		public void SetTcpKeepAlive(bool enabled, int keepAliveTime, int keepAliveInterval)
		{
			if (enabled)
			{
				if (keepAliveTime <= 0)
				{
					throw new ArgumentOutOfRangeException("keepAliveTime", "Must be greater than 0");
				}
				if (keepAliveInterval <= 0)
				{
					throw new ArgumentOutOfRangeException("keepAliveInterval", "Must be greater than 0");
				}
			}
			this.tcp_keepalive = enabled;
			this.tcp_keepalive_time = keepAliveTime;
			this.tcp_keepalive_interval = keepAliveInterval;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000A1250 File Offset: 0x0009F450
		internal void KeepAliveSetup(Socket socket)
		{
			if (!this.tcp_keepalive)
			{
				return;
			}
			byte[] array = new byte[12];
			ServicePoint.PutBytes(array, this.tcp_keepalive ? 1U : 0U, 0);
			ServicePoint.PutBytes(array, (uint)this.tcp_keepalive_time, 4);
			ServicePoint.PutBytes(array, (uint)this.tcp_keepalive_interval, 8);
			socket.IOControl((IOControlCode)((ulong)(-1744830460)), array, null);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000A12AC File Offset: 0x0009F4AC
		private static void PutBytes(byte[] bytes, uint v, int offset)
		{
			if (BitConverter.IsLittleEndian)
			{
				bytes[offset] = (byte)(v & 255U);
				bytes[offset + 1] = (byte)((v & 65280U) >> 8);
				bytes[offset + 2] = (byte)((v & 16711680U) >> 16);
				bytes[offset + 3] = (byte)((v & 4278190080U) >> 24);
				return;
			}
			bytes[offset + 3] = (byte)(v & 255U);
			bytes[offset + 2] = (byte)((v & 65280U) >> 8);
			bytes[offset + 1] = (byte)((v & 16711680U) >> 16);
			bytes[offset] = (byte)((v & 4278190080U) >> 24);
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000A1335 File Offset: 0x0009F535
		// (set) Token: 0x060029D5 RID: 10709 RVA: 0x000A133D File Offset: 0x0009F53D
		internal bool UsesProxy
		{
			get
			{
				return this.usesProxy;
			}
			set
			{
				this.usesProxy = value;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x000A1346 File Offset: 0x0009F546
		// (set) Token: 0x060029D7 RID: 10711 RVA: 0x000A134E File Offset: 0x0009F54E
		internal bool UseConnect
		{
			get
			{
				return this.useConnect;
			}
			set
			{
				this.useConnect = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x000A1358 File Offset: 0x0009F558
		private bool HasTimedOut
		{
			get
			{
				int dnsRefreshTimeout = ServicePointManager.DnsRefreshTimeout;
				return dnsRefreshTimeout != -1 && this.lastDnsResolve + TimeSpan.FromMilliseconds((double)dnsRefreshTimeout) < DateTime.UtcNow;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x000A1390 File Offset: 0x0009F590
		internal IPHostEntry HostEntry
		{
			get
			{
				object obj = this.hostE;
				lock (obj)
				{
					string text = this.uri.Host;
					if (this.uri.HostNameType == UriHostNameType.IPv6 || this.uri.HostNameType == UriHostNameType.IPv4)
					{
						if (this.host != null)
						{
							return this.host;
						}
						if (this.uri.HostNameType == UriHostNameType.IPv6)
						{
							text = text.Substring(1, text.Length - 2);
						}
						this.host = new IPHostEntry();
						this.host.AddressList = new IPAddress[] { IPAddress.Parse(text) };
						return this.host;
					}
					else
					{
						if (!this.HasTimedOut && this.host != null)
						{
							return this.host;
						}
						this.lastDnsResolve = DateTime.UtcNow;
						try
						{
							this.host = Dns.GetHostEntry(text);
						}
						catch
						{
							return null;
						}
					}
				}
				return this.host;
			}
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000A149C File Offset: 0x0009F69C
		internal void SetVersion(Version version)
		{
			this.protocolVersion = version;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000A14A8 File Offset: 0x0009F6A8
		internal void SendRequest(WebOperation operation, string groupName)
		{
			lock (this)
			{
				this.Scheduler.SendRequest(operation, groupName);
			}
		}

		/// <summary>Removes the specified connection group from this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the connection group was closed.</returns>
		/// <param name="connectionGroupName">The name of the connection group that contains the connections to close and remove from this service point. </param>
		// Token: 0x060029DC RID: 10716 RVA: 0x000A14EC File Offset: 0x0009F6EC
		public bool CloseConnectionGroup(string connectionGroupName)
		{
			bool flag2;
			lock (this)
			{
				flag2 = this.Scheduler.CloseConnectionGroup(connectionGroupName);
			}
			return flag2;
		}

		/// <summary>Gets the certificate received for this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>An instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class that contains the security certificate received for this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000A1530 File Offset: 0x0009F730
		public X509Certificate Certificate
		{
			get
			{
				object serverCertificateOrBytes = this.m_ServerCertificateOrBytes;
				if (serverCertificateOrBytes != null && serverCertificateOrBytes.GetType() == typeof(byte[]))
				{
					return (X509Certificate)(this.m_ServerCertificateOrBytes = new X509Certificate((byte[])serverCertificateOrBytes));
				}
				return serverCertificateOrBytes as X509Certificate;
			}
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000A157E File Offset: 0x0009F77E
		internal void UpdateServerCertificate(X509Certificate certificate)
		{
			if (certificate != null)
			{
				this.m_ServerCertificateOrBytes = certificate.GetRawCertData();
				return;
			}
			this.m_ServerCertificateOrBytes = null;
		}

		/// <summary>Gets the last client certificate sent to the server.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object that contains the public values of the last client certificate sent to the server.</returns>
		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000A1598 File Offset: 0x0009F798
		public X509Certificate ClientCertificate
		{
			get
			{
				object clientCertificateOrBytes = this.m_ClientCertificateOrBytes;
				if (clientCertificateOrBytes != null && clientCertificateOrBytes.GetType() == typeof(byte[]))
				{
					return (X509Certificate)(this.m_ClientCertificateOrBytes = new X509Certificate((byte[])clientCertificateOrBytes));
				}
				return clientCertificateOrBytes as X509Certificate;
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000A15E6 File Offset: 0x0009F7E6
		internal void UpdateClientCertificate(X509Certificate certificate)
		{
			if (certificate != null)
			{
				this.m_ClientCertificateOrBytes = certificate.GetRawCertData();
				return;
			}
			this.m_ClientCertificateOrBytes = null;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000A1600 File Offset: 0x0009F800
		internal bool CallEndPointDelegate(Socket sock, IPEndPoint remote)
		{
			if (this.endPointCallback == null)
			{
				return true;
			}
			int num = 0;
			checked
			{
				for (;;)
				{
					IPEndPoint ipendPoint = null;
					try
					{
						ipendPoint = this.endPointCallback(this, remote, num);
					}
					catch
					{
						return false;
					}
					if (ipendPoint == null)
					{
						break;
					}
					try
					{
						sock.Bind(ipendPoint);
					}
					catch (SocketException)
					{
						num++;
						continue;
					}
					return true;
				}
				return true;
			}
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00004239 File Offset: 0x00002439
		internal Socket GetConnection(PooledStream PooledStream, object owner, bool async, out IPAddress address, ref Socket abortSocket, ref Socket abortSocket6)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal ServicePoint()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040022AB RID: 8875
		private readonly Uri uri;

		// Token: 0x040022AC RID: 8876
		private DateTime lastDnsResolve;

		// Token: 0x040022AD RID: 8877
		private Version protocolVersion;

		// Token: 0x040022AE RID: 8878
		private IPHostEntry host;

		// Token: 0x040022AF RID: 8879
		private bool usesProxy;

		// Token: 0x040022B0 RID: 8880
		private bool sendContinue;

		// Token: 0x040022B1 RID: 8881
		private bool useConnect;

		// Token: 0x040022B2 RID: 8882
		private object hostE;

		// Token: 0x040022B3 RID: 8883
		private bool useNagle;

		// Token: 0x040022B4 RID: 8884
		private BindIPEndPoint endPointCallback;

		// Token: 0x040022B5 RID: 8885
		private bool tcp_keepalive;

		// Token: 0x040022B6 RID: 8886
		private int tcp_keepalive_time;

		// Token: 0x040022B7 RID: 8887
		private int tcp_keepalive_interval;

		// Token: 0x040022B9 RID: 8889
		private object m_ServerCertificateOrBytes;

		// Token: 0x040022BA RID: 8890
		private object m_ClientCertificateOrBytes;
	}
}

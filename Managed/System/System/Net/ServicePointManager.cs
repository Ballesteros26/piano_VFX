using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Security;
using System.Threading;

namespace System.Net
{
	/// <summary>Manages the collection of <see cref="T:System.Net.ServicePoint" /> objects.</summary>
	// Token: 0x02000543 RID: 1347
	public class ServicePointManager
	{
		// Token: 0x060029E4 RID: 10724 RVA: 0x000A1668 File Offset: 0x0009F868
		static ServicePointManager()
		{
			ConnectionManagementSection connectionManagementSection = ConfigurationManager.GetSection("system.net/connectionManagement") as ConnectionManagementSection;
			if (connectionManagementSection != null)
			{
				ServicePointManager.manager = new ConnectionManagementData(null);
				foreach (object obj in connectionManagementSection.ConnectionManagement)
				{
					ConnectionManagementElement connectionManagementElement = (ConnectionManagementElement)obj;
					ServicePointManager.manager.Add(connectionManagementElement.Address, connectionManagementElement.MaxConnection);
				}
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
				return;
			}
			ServicePointManager.manager = (ConnectionManagementData)ConfigurationSettings.GetConfig("system.net/connectionManagement");
			if (ServicePointManager.manager != null)
			{
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
			}
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000020EB File Offset: 0x000002EB
		private ServicePointManager()
		{
		}

		/// <summary>Gets or sets policy for server certificates.</summary>
		/// <returns>An object that implements the <see cref="T:System.Net.ICertificatePolicy" /> interface.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x000A1774 File Offset: 0x0009F974
		// (set) Token: 0x060029E7 RID: 10727 RVA: 0x000A1793 File Offset: 0x0009F993
		[Obsolete("Use ServerCertificateValidationCallback instead", false)]
		public static ICertificatePolicy CertificatePolicy
		{
			get
			{
				if (ServicePointManager.policy == null)
				{
					Interlocked.CompareExchange<ICertificatePolicy>(ref ServicePointManager.policy, new DefaultCertificatePolicy(), null);
				}
				return ServicePointManager.policy;
			}
			set
			{
				ServicePointManager.policy = value;
			}
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000A179B File Offset: 0x0009F99B
		internal static ICertificatePolicy GetLegacyCertificatePolicy()
		{
			return ServicePointManager.policy;
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>true if the certificate revocation list is checked; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x000A17A2 File Offset: 0x0009F9A2
		// (set) Token: 0x060029EA RID: 10730 RVA: 0x000A17A9 File Offset: 0x0009F9A9
		[MonoTODO("CRL checks not implemented")]
		public static bool CheckCertificateRevocationList
		{
			get
			{
				return ServicePointManager._checkCRL;
			}
			set
			{
				ServicePointManager._checkCRL = false;
			}
		}

		/// <summary>Gets or sets the maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object. The default value is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> is less than or equal to 0. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x000A17B1 File Offset: 0x0009F9B1
		// (set) Token: 0x060029EC RID: 10732 RVA: 0x000A17B8 File Offset: 0x0009F9B8
		public static int DefaultConnectionLimit
		{
			get
			{
				return ServicePointManager.defaultConnectionLimit;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ServicePointManager.defaultConnectionLimit = value;
				if (ServicePointManager.manager != null)
				{
					ServicePointManager.manager.Add("*", ServicePointManager.defaultConnectionLimit);
				}
			}
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00093A0B File Offset: 0x00091C0B
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets a value that indicates how long a Domain Name Service (DNS) resolution is considered valid.</summary>
		/// <returns>The time-out value, in milliseconds. A value of -1 indicates an infinite time-out period. The default value is 120,000 milliseconds (two minutes).</returns>
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x000A17EA File Offset: 0x0009F9EA
		// (set) Token: 0x060029EF RID: 10735 RVA: 0x000A17F1 File Offset: 0x0009F9F1
		public static int DnsRefreshTimeout
		{
			get
			{
				return ServicePointManager.dnsRefreshTimeout;
			}
			set
			{
				ServicePointManager.dnsRefreshTimeout = Math.Max(-1, value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether a Domain Name Service (DNS) resolution rotates among the applicable Internet Protocol (IP) addresses.</summary>
		/// <returns>false if a DNS resolution always returns the first IP address for a particular host; otherwise true. The default is false.</returns>
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x000A17FF File Offset: 0x0009F9FF
		// (set) Token: 0x060029F1 RID: 10737 RVA: 0x000A17FF File Offset: 0x0009F9FF
		[MonoTODO]
		public static bool EnableDnsRoundRobin
		{
			get
			{
				throw ServicePointManager.GetMustImplement();
			}
			set
			{
				throw ServicePointManager.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the maximum idle time of a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum idle time, in milliseconds, of a <see cref="T:System.Net.ServicePoint" /> object. The default value is 100,000 milliseconds (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePointIdleTime" /> is less than <see cref="F:System.Threading.Timeout.Infinite" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x000A1806 File Offset: 0x0009FA06
		// (set) Token: 0x060029F3 RID: 10739 RVA: 0x000A180D File Offset: 0x0009FA0D
		public static int MaxServicePointIdleTime
		{
			get
			{
				return ServicePointManager.maxServicePointIdleTime;
			}
			set
			{
				if (value < -2 || value > 2147483647)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ServicePointManager.maxServicePointIdleTime = value;
			}
		}

		/// <summary>Gets or sets the maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain at any time.</summary>
		/// <returns>The maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain. The default value is 0, which means there is no limit to the number of <see cref="T:System.Net.ServicePoint" /> objects.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060029F4 RID: 10740 RVA: 0x000A182D File Offset: 0x0009FA2D
		// (set) Token: 0x060029F5 RID: 10741 RVA: 0x000A1834 File Offset: 0x0009FA34
		public static int MaxServicePoints
		{
			get
			{
				return ServicePointManager.maxServicePoints;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("value");
				}
				ServicePointManager.maxServicePoints = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060029F6 RID: 10742 RVA: 0x00004240 File Offset: 0x00002440
		// (set) Token: 0x060029F7 RID: 10743 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public static bool ReusePort
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the security protocol used by the <see cref="T:System.Net.ServicePoint" /> objects managed by the <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>One of the values defined in the <see cref="T:System.Net.SecurityProtocolType" /> enumeration.</returns>
		/// <exception cref="T:System.NotSupportedException">The value specified to set the property is not a valid <see cref="T:System.Net.SecurityProtocolType" /> enumeration value. </exception>
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x000A184B File Offset: 0x0009FA4B
		// (set) Token: 0x060029F9 RID: 10745 RVA: 0x000A1852 File Offset: 0x0009FA52
		public static SecurityProtocolType SecurityProtocol
		{
			get
			{
				return ServicePointManager._securityProtocol;
			}
			set
			{
				ServicePointManager._securityProtocol = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x000A185A File Offset: 0x0009FA5A
		internal static ServerCertValidationCallback ServerCertValidationCallback
		{
			get
			{
				return ServicePointManager.server_cert_cb;
			}
		}

		/// <summary>Gets or sets the callback to validate a server certificate.</summary>
		/// <returns>A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" />. The default value is null.</returns>
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x000A1861 File Offset: 0x0009FA61
		// (set) Token: 0x060029FC RID: 10748 RVA: 0x000A1876 File Offset: 0x0009FA76
		public static RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (ServicePointManager.server_cert_cb == null)
				{
					return null;
				}
				return ServicePointManager.server_cert_cb.ValidationCallback;
			}
			set
			{
				if (value == null)
				{
					ServicePointManager.server_cert_cb = null;
					return;
				}
				ServicePointManager.server_cert_cb = new ServerCertValidationCallback(value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Security.EncryptionPolicy" /> for this <see cref="T:System.Net.ServicePointManager" /> instance.</summary>
		/// <returns>The encryption policy to use for this <see cref="T:System.Net.ServicePointManager" /> instance.</returns>
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x00004240 File Offset: 0x00002440
		[MonoTODO("Always returns EncryptionPolicy.RequireEncryption.")]
		public static EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return EncryptionPolicy.RequireEncryption;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to enable 100-Continue behavior. The default value is true.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x000A188D File Offset: 0x0009FA8D
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x000A1894 File Offset: 0x0009FA94
		public static bool Expect100Continue
		{
			get
			{
				return ServicePointManager.expectContinue;
			}
			set
			{
				ServicePointManager.expectContinue = value;
			}
		}

		/// <summary>Determines whether the Nagle algorithm is used by the service points managed by this <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000A189C File Offset: 0x0009FA9C
		// (set) Token: 0x06002A01 RID: 10753 RVA: 0x000A18A3 File Offset: 0x0009FAA3
		public static bool UseNagleAlgorithm
		{
			get
			{
				return ServicePointManager.useNagle;
			}
			set
			{
				ServicePointManager.useNagle = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x00004240 File Offset: 0x00002440
		internal static bool DisableStrongCrypto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x00004240 File Offset: 0x00002440
		internal static bool DisableSendAuxRecord
		{
			get
			{
				return false;
			}
		}

		/// <summary>Enables or disables the keep-alive option on a TCP connection.</summary>
		/// <param name="enabled">If set to true, then the TCP keep-alive option on a TCP connection will be enabled using the specified <paramref name="keepAliveTime " />and <paramref name="keepAliveInterval" /> values. If set to false, then the TCP keep-alive option is disabled and the remaining parameters are ignored.The default value is false.</param>
		/// <param name="keepAliveTime">Specifies the timeout, in milliseconds, with no activity until the first keep-alive packet is sent.The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <param name="keepAliveInterval">Specifies the interval, in milliseconds, between when successive keep-alive packets are sent if no acknowledgement is received.The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for <paramref name="keepAliveTime" /> or <paramref name="keepAliveInterval" /> parameter is less than or equal to 0.</exception>
		// Token: 0x06002A04 RID: 10756 RVA: 0x000A18AB File Offset: 0x0009FAAB
		public static void SetTcpKeepAlive(bool enabled, int keepAliveTime, int keepAliveInterval)
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
			ServicePointManager.tcp_keepalive = enabled;
			ServicePointManager.tcp_keepalive_time = keepAliveTime;
			ServicePointManager.tcp_keepalive_interval = keepAliveInterval;
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="address">The <see cref="T:System.Uri" /> object of the Internet resource to contact. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002A05 RID: 10757 RVA: 0x000A18EA File Offset: 0x0009FAEA
		public static ServicePoint FindServicePoint(Uri address)
		{
			return ServicePointManager.FindServicePoint(address, null);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified Uniform Resource Identifier (URI).</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="uriString">The URI of the Internet resource to be contacted. </param>
		/// <param name="proxy">The proxy data for this request. </param>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="uriString" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002A06 RID: 10758 RVA: 0x000A18F3 File Offset: 0x0009FAF3
		public static ServicePoint FindServicePoint(string uriString, IWebProxy proxy)
		{
			return ServicePointManager.FindServicePoint(new Uri(uriString), proxy);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> object that contains the address of the Internet resource to contact. </param>
		/// <param name="proxy">The proxy data for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002A07 RID: 10759 RVA: 0x000A1904 File Offset: 0x0009FB04
		public static ServicePoint FindServicePoint(Uri address, IWebProxy proxy)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Uri uri = new Uri(address.Scheme + "://" + address.Authority);
			bool flag = false;
			bool flag2 = false;
			if (proxy != null && !proxy.IsBypassed(address))
			{
				flag = true;
				bool flag3 = address.Scheme == "https";
				address = proxy.GetProxy(address);
				if (address.Scheme != "http")
				{
					throw new NotSupportedException("Proxy scheme not supported.");
				}
				if (flag3 && address.Scheme == "http")
				{
					flag2 = true;
				}
			}
			address = new Uri(address.Scheme + "://" + address.Authority);
			ServicePoint servicePoint = null;
			ServicePointManager.SPKey spkey = new ServicePointManager.SPKey(uri, flag ? address : null, flag2);
			HybridDictionary hybridDictionary = ServicePointManager.servicePoints;
			lock (hybridDictionary)
			{
				servicePoint = ServicePointManager.servicePoints[spkey] as ServicePoint;
				if (servicePoint != null)
				{
					return servicePoint;
				}
				if (ServicePointManager.maxServicePoints > 0 && ServicePointManager.servicePoints.Count >= ServicePointManager.maxServicePoints)
				{
					throw new InvalidOperationException("maximum number of service points reached");
				}
				string text = address.ToString();
				int maxConnections = (int)ServicePointManager.manager.GetMaxConnections(text);
				servicePoint = new ServicePoint(address, maxConnections, ServicePointManager.maxServicePointIdleTime);
				servicePoint.Expect100Continue = ServicePointManager.expectContinue;
				servicePoint.UseNagleAlgorithm = ServicePointManager.useNagle;
				servicePoint.UsesProxy = flag;
				servicePoint.UseConnect = flag2;
				servicePoint.SetTcpKeepAlive(ServicePointManager.tcp_keepalive, ServicePointManager.tcp_keepalive_time, ServicePointManager.tcp_keepalive_interval);
				ServicePointManager.servicePoints.Add(spkey, servicePoint);
			}
			return servicePoint;
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000A1AAC File Offset: 0x0009FCAC
		internal static void CloseConnectionGroup(string connectionGroupName)
		{
			HybridDictionary hybridDictionary = ServicePointManager.servicePoints;
			lock (hybridDictionary)
			{
				foreach (object obj in ServicePointManager.servicePoints.Values)
				{
					((ServicePoint)obj).CloseConnectionGroup(connectionGroupName);
				}
			}
		}

		// Token: 0x040022BB RID: 8891
		private static HybridDictionary servicePoints = new HybridDictionary();

		// Token: 0x040022BC RID: 8892
		private static ICertificatePolicy policy;

		// Token: 0x040022BD RID: 8893
		private static int defaultConnectionLimit = 2;

		// Token: 0x040022BE RID: 8894
		private static int maxServicePointIdleTime = 100000;

		// Token: 0x040022BF RID: 8895
		private static int maxServicePoints = 0;

		// Token: 0x040022C0 RID: 8896
		private static int dnsRefreshTimeout = 120000;

		// Token: 0x040022C1 RID: 8897
		private static bool _checkCRL = false;

		// Token: 0x040022C2 RID: 8898
		private static SecurityProtocolType _securityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

		// Token: 0x040022C3 RID: 8899
		private static bool expectContinue = true;

		// Token: 0x040022C4 RID: 8900
		private static bool useNagle;

		// Token: 0x040022C5 RID: 8901
		private static ServerCertValidationCallback server_cert_cb;

		// Token: 0x040022C6 RID: 8902
		private static bool tcp_keepalive;

		// Token: 0x040022C7 RID: 8903
		private static int tcp_keepalive_time;

		// Token: 0x040022C8 RID: 8904
		private static int tcp_keepalive_interval;

		/// <summary>The default number of non-persistent connections (4) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.0 or later server. This field is constant but is no longer used in the .NET Framework 2.0.</summary>
		// Token: 0x040022C9 RID: 8905
		public const int DefaultNonPersistentConnectionLimit = 4;

		/// <summary>The default number of persistent connections (2) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.1 or later server. This field is constant and is used to initialize the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property if the value of the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property has not been set either directly or through configuration.</summary>
		// Token: 0x040022CA RID: 8906
		public const int DefaultPersistentConnectionLimit = 2;

		// Token: 0x040022CB RID: 8907
		private const string configKey = "system.net/connectionManagement";

		// Token: 0x040022CC RID: 8908
		private static ConnectionManagementData manager;

		// Token: 0x02000544 RID: 1348
		private class SPKey
		{
			// Token: 0x06002A09 RID: 10761 RVA: 0x000A1B30 File Offset: 0x0009FD30
			public SPKey(Uri uri, Uri proxy, bool use_connect)
			{
				this.uri = uri;
				this.proxy = proxy;
				this.use_connect = use_connect;
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000A1B4D File Offset: 0x0009FD4D
			public Uri Uri
			{
				get
				{
					return this.uri;
				}
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x06002A0B RID: 10763 RVA: 0x000A1B55 File Offset: 0x0009FD55
			public bool UseConnect
			{
				get
				{
					return this.use_connect;
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000A1B5D File Offset: 0x0009FD5D
			public bool UsesProxy
			{
				get
				{
					return this.proxy != null;
				}
			}

			// Token: 0x06002A0D RID: 10765 RVA: 0x000A1B6C File Offset: 0x0009FD6C
			public override int GetHashCode()
			{
				return ((23 * 31 + (this.use_connect ? 1 : 0)) * 31 + this.uri.GetHashCode()) * 31 + ((this.proxy != null) ? this.proxy.GetHashCode() : 0);
			}

			// Token: 0x06002A0E RID: 10766 RVA: 0x000A1BBC File Offset: 0x0009FDBC
			public override bool Equals(object obj)
			{
				ServicePointManager.SPKey spkey = obj as ServicePointManager.SPKey;
				return obj != null && this.uri.Equals(spkey.uri) && this.use_connect == spkey.use_connect && this.UsesProxy == spkey.UsesProxy && (!this.UsesProxy || this.proxy.Equals(spkey.proxy));
			}

			// Token: 0x040022CD RID: 8909
			private Uri uri;

			// Token: 0x040022CE RID: 8910
			private Uri proxy;

			// Token: 0x040022CF RID: 8911
			private bool use_connect;
		}
	}
}

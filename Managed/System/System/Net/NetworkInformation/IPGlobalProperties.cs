using System;
using System.IO;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the network connectivity of the local computer.</summary>
	// Token: 0x020005FA RID: 1530
	public abstract class IPGlobalProperties
	{
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x000BD7F9 File Offset: 0x000BB9F9
		private static bool PlatformNeedsLibCWorkaround { get; }

		/// <summary>Gets an object that provides information about the local computer's network connectivity and traffic statistics.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.IPGlobalProperties" /> object that contains information about the local computer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Net.NetworkInformation.NetworkInformationPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Access="Read" />
		/// </PermissionSet>
		// Token: 0x060030D9 RID: 12505 RVA: 0x000BD800 File Offset: 0x000BBA00
		public static IPGlobalProperties GetIPGlobalProperties()
		{
			PlatformID platform = Environment.OSVersion.Platform;
			if (platform != PlatformID.Unix)
			{
				return new Win32IPGlobalProperties();
			}
			if (IPGlobalProperties.PlatformNeedsLibCWorkaround)
			{
				return new UnixNoLibCIPGlobalProperties();
			}
			if (Directory.Exists("/proc"))
			{
				MibIPGlobalProperties mibIPGlobalProperties = new MibIPGlobalProperties("/proc");
				if (File.Exists(mibIPGlobalProperties.StatisticsFile))
				{
					return mibIPGlobalProperties;
				}
			}
			if (Directory.Exists("/usr/compat/linux/proc"))
			{
				MibIPGlobalProperties mibIPGlobalProperties = new MibIPGlobalProperties("/usr/compat/linux/proc");
				if (File.Exists(mibIPGlobalProperties.StatisticsFile))
				{
					return mibIPGlobalProperties;
				}
			}
			return new UnixIPGlobalProperties();
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000BD882 File Offset: 0x000BBA82
		internal static IPGlobalProperties InternalGetIPGlobalProperties()
		{
			return IPGlobalProperties.GetIPGlobalProperties();
		}

		/// <summary>Returns information about the Internet Protocol version 4 (IPv4) and IPv6 User Datagram Protocol (UDP) listeners on the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> array that contains objects that describe the UDP listeners, or an empty array if no UDP listeners are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetUdpTable failed. </exception>
		// Token: 0x060030DB RID: 12507
		public abstract IPEndPoint[] GetActiveUdpListeners();

		/// <summary>Returns endpoint information about the Internet Protocol version 4 (IPv4) and IPv6 Transmission Control Protocol (TCP) listeners on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.IPEndPoint" /> array that contains objects that describe the active TCP listeners, or an empty array, if no active TCP listeners are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function GetTcpTable failed. </exception>
		// Token: 0x060030DC RID: 12508
		public abstract IPEndPoint[] GetActiveTcpListeners();

		/// <summary>Returns information about the Internet Protocol version 4 (IPv4) and IPv6 Transmission Control Protocol (TCP) connections on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpConnectionInformation" /> array that contains objects that describe the active TCP connections, or an empty array if no active TCP connections are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function GetTcpTable failed. </exception>
		// Token: 0x060030DD RID: 12509
		public abstract TcpConnectionInformation[] GetActiveTcpConnections();

		/// <summary>Gets the Dynamic Host Configuration Protocol (DHCP) scope name.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's DHCP scope name.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060030DE RID: 12510
		public abstract string DhcpScopeName { get; }

		/// <summary>Gets the domain in which the local computer is registered.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's domain name. If the computer does not belong to a domain, returns <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060030DF RID: 12511
		public abstract string DomainName { get; }

		/// <summary>Gets the host name for the local computer.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's NetBIOS name.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060030E0 RID: 12512
		public abstract string HostName { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that specifies whether the local computer is acting as a Windows Internet Name Service (WINS) proxy.</summary>
		/// <returns>true if the local computer is a WINS proxy; otherwise, false.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060030E1 RID: 12513
		public abstract bool IsWinsProxy { get; }

		/// <summary>Gets the Network Basic Input/Output System (NetBIOS) node type of the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetBiosNodeType" /> value.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060030E2 RID: 12514
		public abstract NetBiosNodeType NodeType { get; }

		/// <summary>Provides Transmission Control Protocol/Internet Protocol version 4 (TCP/IPv4) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> object that provides TCP/IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetTcpStatistics failed.</exception>
		// Token: 0x060030E3 RID: 12515
		public abstract TcpStatistics GetTcpIPv4Statistics();

		/// <summary>Provides Transmission Control Protocol/Internet Protocol version 6 (TCP/IPv6) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> object that provides TCP/IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetTcpStatistics failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6. </exception>
		// Token: 0x060030E4 RID: 12516
		public abstract TcpStatistics GetTcpIPv6Statistics();

		/// <summary>Provides User Datagram Protocol/Internet Protocol version 4 (UDP/IPv4) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> object that provides UDP/IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetUdpStatistics failed.</exception>
		// Token: 0x060030E5 RID: 12517
		public abstract UdpStatistics GetUdpIPv4Statistics();

		/// <summary>Provides User Datagram Protocol/Internet Protocol version 6 (UDP/IPv6) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> object that provides UDP/IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetUdpStatistics failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6. </exception>
		// Token: 0x060030E6 RID: 12518
		public abstract UdpStatistics GetUdpIPv6Statistics();

		/// <summary>Provides Internet Control Message Protocol (ICMP) version 4 statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IcmpV4Statistics" /> object that provides ICMP version 4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function GetIcmpStatistics failed. </exception>
		// Token: 0x060030E7 RID: 12519
		public abstract IcmpV4Statistics GetIcmpV4Statistics();

		/// <summary>Provides Internet Control Message Protocol (ICMP) version 6 statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IcmpV6Statistics" /> object that provides ICMP version 6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function GetIcmpStatisticsEx failed. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer's operating system is not Windows XP or later.</exception>
		// Token: 0x060030E8 RID: 12520
		public abstract IcmpV6Statistics GetIcmpV6Statistics();

		/// <summary>Provides Internet Protocol version 4 (IPv4) statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> object that provides IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetIpStatistics failed.</exception>
		// Token: 0x060030E9 RID: 12521
		public abstract IPGlobalStatistics GetIPv4GlobalStatistics();

		/// <summary>Provides Internet Protocol version 6 (IPv6) statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> object that provides IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetIpStatistics failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6. </exception>
		// Token: 0x060030EA RID: 12522
		public abstract IPGlobalStatistics GetIPv6GlobalStatistics();

		/// <summary>Retrieves the stable unicast IP address table on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> that contains a list of stable unicast IP addresses on the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the native GetAdaptersAddresses function failed.</exception>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented on the platform. This method uses the native NotifyStableUnicastIpAddressTable function that is supported on Windows Vista and later. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have necessary <see cref="F:System.Net.NetworkInformation.NetworkInformationAccess.Read" /> permission. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The call to the native NotifyStableUnicastIpAddressTable function failed.</exception>
		// Token: 0x060030EB RID: 12523 RVA: 0x0007D720 File Offset: 0x0007B920
		public virtual UnicastIPAddressInformationCollection GetUnicastAddresses()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Begins an asynchronous request to retrieve the stable unicast IP address table on the local computer.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented on the platform. This method uses the native NotifyStableUnicastIpAddressTable function that is supported on Windows Vista and later. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The call to the native NotifyStableUnicastIpAddressTable function failed.</exception>
		// Token: 0x060030EC RID: 12524 RVA: 0x0007D720 File Offset: 0x0007B920
		public virtual IAsyncResult BeginGetUnicastAddresses(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Ends a pending asynchronous request to retrieve the stable unicast IP address table on the local computer.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</param>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the native GetAdaptersAddresses function failed.</exception>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented on the platform. This method uses the native NotifyStableUnicastIpAddressTable function that is supported on Windows Vista and later. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have necessary <see cref="F:System.Net.NetworkInformation.NetworkInformationAccess.Read" /> permission. </exception>
		// Token: 0x060030ED RID: 12525 RVA: 0x0007D720 File Offset: 0x0007B920
		public virtual UnicastIPAddressInformationCollection EndGetUnicastAddresses(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Retrieves the stable unicast IP address table on the local computer as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the native GetAdaptersAddresses function failed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have necessary <see cref="F:System.Net.NetworkInformation.NetworkInformationAccess.Read" /> permission. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The call to the native NotifyStableUnicastIpAddressTable function failed.</exception>
		// Token: 0x060030EE RID: 12526 RVA: 0x000BD889 File Offset: 0x000BBA89
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<UnicastIPAddressInformationCollection> GetUnicastAddressesAsync()
		{
			return Task<UnicastIPAddressInformationCollection>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetUnicastAddresses), new Func<IAsyncResult, UnicastIPAddressInformationCollection>(this.EndGetUnicastAddresses), null);
		}
	}
}

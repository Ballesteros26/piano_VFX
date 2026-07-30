using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4) or Internet Protocol version 6 (IPv6).</summary>
	// Token: 0x020005FC RID: 1532
	public abstract class IPInterfaceProperties
	{
		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether NetBt is configured to use DNS name resolution on this interface.</summary>
		/// <returns>true if NetBt is configured to use DNS name resolution on this interface; otherwise, false.</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003107 RID: 12551
		public abstract bool IsDnsEnabled { get; }

		/// <summary>Gets the Domain Name System (DNS) suffix associated with this interface.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the DNS suffix for this interface, or <see cref="F:System.String.Empty" /> if there is no DNS suffix for the interface.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows 2000. </exception>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003108 RID: 12552
		public abstract string DnsSuffix { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface is configured to automatically register its IP address information with the Domain Name System (DNS).</summary>
		/// <returns>true if this interface is configured to automatically register a mapping between its dynamic IP address and static domain names; otherwise, false.</returns>
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003109 RID: 12553
		public abstract bool IsDynamicDnsEnabled { get; }

		/// <summary>Gets the unicast addresses assigned to this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> that contains the unicast addresses for this interface.</returns>
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600310A RID: 12554
		public abstract UnicastIPAddressInformationCollection UnicastAddresses { get; }

		/// <summary>Gets the multicast addresses assigned to this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformationCollection" /> that contains the multicast addresses for this interface.</returns>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600310B RID: 12555
		public abstract MulticastIPAddressInformationCollection MulticastAddresses { get; }

		/// <summary>Gets the anycast IP addresses assigned to this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPAddressInformationCollection" /> that contains the anycast addresses for this interface.</returns>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600310C RID: 12556
		public abstract IPAddressInformationCollection AnycastAddresses { get; }

		/// <summary>Gets the addresses of Domain Name System (DNS) servers for this interface.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the DNS server addresses.</returns>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x0600310D RID: 12557
		public abstract IPAddressCollection DnsAddresses { get; }

		/// <summary>Gets the IPv4 network gateway addresses for this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformationCollection" /> that contains the address information for network gateways, or an empty array if no gateways are found.</returns>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600310E RID: 12558
		public abstract GatewayIPAddressInformationCollection GatewayAddresses { get; }

		/// <summary>Gets the addresses of Dynamic Host Configuration Protocol (DHCP) servers for this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the address information for DHCP servers, or an empty array if no servers are found.</returns>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600310F RID: 12559
		public abstract IPAddressCollection DhcpServerAddresses { get; }

		/// <summary>Gets the addresses of Windows Internet Name Service (WINS) servers.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the address information for WINS servers, or an empty array if no servers are found.</returns>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003110 RID: 12560
		public abstract IPAddressCollection WinsServersAddresses { get; }

		/// <summary>Provides Internet Protocol version 4 (IPv4) configuration data for this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv4InterfaceProperties" /> object that contains IPv4 configuration data, or null if no data is available for the interface.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The interface does not support the IPv4 protocol.</exception>
		// Token: 0x06003111 RID: 12561
		public abstract IPv4InterfaceProperties GetIPv4Properties();

		/// <summary>Provides Internet Protocol version 6 (IPv6) configuration data for this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv6InterfaceProperties" /> object that contains IPv6 configuration data.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The interface does not support the IPv6 protocol.</exception>
		// Token: 0x06003112 RID: 12562
		public abstract IPv6InterfaceProperties GetIPv6Properties();
	}
}

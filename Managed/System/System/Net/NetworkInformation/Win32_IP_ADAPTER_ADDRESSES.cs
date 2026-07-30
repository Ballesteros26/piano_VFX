using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200067A RID: 1658
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct Win32_IP_ADAPTER_ADDRESSES
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000C378F File Offset: 0x000C198F
		public bool DdnsEnabled
		{
			get
			{
				return (this.Flags & 1U) > 0U;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000C379C File Offset: 0x000C199C
		public bool DhcpEnabled
		{
			get
			{
				return (this.Flags & 4U) > 0U;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000C37A9 File Offset: 0x000C19A9
		public bool IsReceiveOnly
		{
			get
			{
				return (this.Flags & 8U) > 0U;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000C37B6 File Offset: 0x000C19B6
		public bool NoMulticast
		{
			get
			{
				return (this.Flags & 16U) > 0U;
			}
		}

		// Token: 0x0400299D RID: 10653
		public AlignmentUnion Alignment;

		// Token: 0x0400299E RID: 10654
		public IntPtr Next;

		// Token: 0x0400299F RID: 10655
		[MarshalAs(UnmanagedType.LPStr)]
		public string AdapterName;

		// Token: 0x040029A0 RID: 10656
		public IntPtr FirstUnicastAddress;

		// Token: 0x040029A1 RID: 10657
		public IntPtr FirstAnycastAddress;

		// Token: 0x040029A2 RID: 10658
		public IntPtr FirstMulticastAddress;

		// Token: 0x040029A3 RID: 10659
		public IntPtr FirstDnsServerAddress;

		// Token: 0x040029A4 RID: 10660
		public string DnsSuffix;

		// Token: 0x040029A5 RID: 10661
		public string Description;

		// Token: 0x040029A6 RID: 10662
		public string FriendlyName;

		// Token: 0x040029A7 RID: 10663
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysicalAddress;

		// Token: 0x040029A8 RID: 10664
		public uint PhysicalAddressLength;

		// Token: 0x040029A9 RID: 10665
		public uint Flags;

		// Token: 0x040029AA RID: 10666
		public uint Mtu;

		// Token: 0x040029AB RID: 10667
		public NetworkInterfaceType IfType;

		// Token: 0x040029AC RID: 10668
		public OperationalStatus OperStatus;

		// Token: 0x040029AD RID: 10669
		public int Ipv6IfIndex;

		// Token: 0x040029AE RID: 10670
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public uint[] ZoneIndices;

		// Token: 0x040029AF RID: 10671
		public IntPtr FirstPrefix;

		// Token: 0x040029B0 RID: 10672
		public ulong TransmitLinkSpeed;

		// Token: 0x040029B1 RID: 10673
		public ulong ReceiveLinkSpeed;

		// Token: 0x040029B2 RID: 10674
		public IntPtr FirstWinsServerAddress;

		// Token: 0x040029B3 RID: 10675
		public IntPtr FirstGatewayAddress;

		// Token: 0x040029B4 RID: 10676
		public uint Ipv4Metric;

		// Token: 0x040029B5 RID: 10677
		public uint Ipv6Metric;

		// Token: 0x040029B6 RID: 10678
		public ulong Luid;

		// Token: 0x040029B7 RID: 10679
		public Win32_SOCKET_ADDRESS Dhcpv4Server;

		// Token: 0x040029B8 RID: 10680
		public uint CompartmentId;

		// Token: 0x040029B9 RID: 10681
		public ulong NetworkGuid;

		// Token: 0x040029BA RID: 10682
		public int ConnectionType;

		// Token: 0x040029BB RID: 10683
		public int TunnelType;

		// Token: 0x040029BC RID: 10684
		public Win32_SOCKET_ADDRESS Dhcpv6Server;

		// Token: 0x040029BD RID: 10685
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 130)]
		public byte[] Dhcpv6ClientDuid;

		// Token: 0x040029BE RID: 10686
		public ulong Dhcpv6ClientDuidLength;

		// Token: 0x040029BF RID: 10687
		public ulong Dhcpv6Iaid;

		// Token: 0x040029C0 RID: 10688
		public IntPtr FirstDnsSuffix;

		// Token: 0x040029C1 RID: 10689
		public const int GAA_FLAG_INCLUDE_WINS_INFO = 64;

		// Token: 0x040029C2 RID: 10690
		public const int GAA_FLAG_INCLUDE_GATEWAYS = 128;

		// Token: 0x040029C3 RID: 10691
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x040029C4 RID: 10692
		private const int MAX_DHCPV6_DUID_LENGTH = 130;

		// Token: 0x040029C5 RID: 10693
		private const int IP_ADAPTER_DDNS_ENABLED = 1;

		// Token: 0x040029C6 RID: 10694
		private const int IP_ADAPTER_DHCP_ENABLED = 4;

		// Token: 0x040029C7 RID: 10695
		private const int IP_ADAPTER_RECEIVE_ONLY = 8;

		// Token: 0x040029C8 RID: 10696
		private const int IP_ADAPTER_NO_MULTICAST = 16;
	}
}

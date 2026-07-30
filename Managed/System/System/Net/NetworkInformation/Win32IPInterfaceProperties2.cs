using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000637 RID: 1591
	internal class Win32IPInterfaceProperties2 : IPInterfaceProperties
	{
		// Token: 0x060032BA RID: 12986 RVA: 0x000BFC10 File Offset: 0x000BDE10
		public Win32IPInterfaceProperties2(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib4, Win32_MIB_IFROW mib6)
		{
			this.addr = addr;
			this.mib4 = mib4;
			this.mib6 = mib6;
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000BFC2D File Offset: 0x000BDE2D
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			return new Win32IPv4InterfaceProperties(this.addr, this.mib4);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000BFC40 File Offset: 0x000BDE40
		public override IPv6InterfaceProperties GetIPv6Properties()
		{
			return new Win32IPv6InterfaceProperties(this.mib6);
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x000BFC4D File Offset: 0x000BDE4D
		public override IPAddressInformationCollection AnycastAddresses
		{
			get
			{
				return Win32IPInterfaceProperties2.Win32FromAnycast(this.addr.FirstAnycastAddress);
			}
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000BFC60 File Offset: 0x000BDE60
		private static IPAddressInformationCollection Win32FromAnycast(IntPtr ptr)
		{
			IPAddressInformationCollection ipaddressInformationCollection = new IPAddressInformationCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ANYCAST_ADDRESS win32_IP_ADAPTER_ANYCAST_ADDRESS = (Win32_IP_ADAPTER_ANYCAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_ANYCAST_ADDRESS));
				ipaddressInformationCollection.InternalAdd(new SystemIPAddressInformation(win32_IP_ADAPTER_ANYCAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsTransient));
				intPtr = win32_IP_ADAPTER_ANYCAST_ADDRESS.Next;
			}
			return ipaddressInformationCollection;
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x000BFCD4 File Offset: 0x000BDED4
		public override IPAddressCollection DhcpServerAddresses
		{
			get
			{
				IPAddressCollection ipaddressCollection;
				try
				{
					ipaddressCollection = Win32IPAddressCollection.FromSocketAddress(this.addr.Dhcpv4Server);
				}
				catch (IndexOutOfRangeException)
				{
					ipaddressCollection = Win32IPAddressCollection.Empty;
				}
				return ipaddressCollection;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000BFD10 File Offset: 0x000BDF10
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				return Win32IPAddressCollection.FromDnsServer(this.addr.FirstDnsServerAddress);
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060032C1 RID: 12993 RVA: 0x000BFD22 File Offset: 0x000BDF22
		public override string DnsSuffix
		{
			get
			{
				return this.addr.DnsSuffix;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000BFD30 File Offset: 0x000BDF30
		public override GatewayIPAddressInformationCollection GatewayAddresses
		{
			get
			{
				GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
				try
				{
					IntPtr intPtr = this.addr.FirstGatewayAddress;
					while (intPtr != IntPtr.Zero)
					{
						Win32_IP_ADAPTER_GATEWAY_ADDRESS win32_IP_ADAPTER_GATEWAY_ADDRESS = (Win32_IP_ADAPTER_GATEWAY_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_GATEWAY_ADDRESS));
						gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(win32_IP_ADAPTER_GATEWAY_ADDRESS.Address.GetIPAddress()));
						intPtr = win32_IP_ADAPTER_GATEWAY_ADDRESS.Next;
					}
				}
				catch (IndexOutOfRangeException)
				{
				}
				return gatewayIPAddressInformationCollection;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000BFDA8 File Offset: 0x000BDFA8
		public override bool IsDnsEnabled
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableDns > 0U;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000BFDB8 File Offset: 0x000BDFB8
		public override bool IsDynamicDnsEnabled
		{
			get
			{
				return this.addr.DdnsEnabled;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x000BFDD3 File Offset: 0x000BDFD3
		public override MulticastIPAddressInformationCollection MulticastAddresses
		{
			get
			{
				return Win32IPInterfaceProperties2.Win32FromMulticast(this.addr.FirstMulticastAddress);
			}
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000BFDE8 File Offset: 0x000BDFE8
		private static MulticastIPAddressInformationCollection Win32FromMulticast(IntPtr ptr)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_MULTICAST_ADDRESS win32_IP_ADAPTER_MULTICAST_ADDRESS = (Win32_IP_ADAPTER_MULTICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_MULTICAST_ADDRESS));
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation(new SystemIPAddressInformation(win32_IP_ADAPTER_MULTICAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsTransient)));
				intPtr = win32_IP_ADAPTER_MULTICAST_ADDRESS.Next;
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000BFE60 File Offset: 0x000BE060
		public override UnicastIPAddressInformationCollection UnicastAddresses
		{
			get
			{
				UnicastIPAddressInformationCollection unicastIPAddressInformationCollection;
				try
				{
					unicastIPAddressInformationCollection = Win32IPInterfaceProperties2.Win32FromUnicast(this.addr.FirstUnicastAddress);
				}
				catch (IndexOutOfRangeException)
				{
					unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
				}
				return unicastIPAddressInformationCollection;
			}
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000BFE9C File Offset: 0x000BE09C
		private static UnicastIPAddressInformationCollection Win32FromUnicast(IntPtr ptr)
		{
			UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_UNICAST_ADDRESS win32_IP_ADAPTER_UNICAST_ADDRESS = (Win32_IP_ADAPTER_UNICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_UNICAST_ADDRESS));
				unicastIPAddressInformationCollection.InternalAdd(new Win32UnicastIPAddressInformation(win32_IP_ADAPTER_UNICAST_ADDRESS));
				intPtr = win32_IP_ADAPTER_UNICAST_ADDRESS.Next;
			}
			return unicastIPAddressInformationCollection;
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x000BFEEC File Offset: 0x000BE0EC
		public override IPAddressCollection WinsServersAddresses
		{
			get
			{
				IPAddressCollection ipaddressCollection;
				try
				{
					ipaddressCollection = Win32IPAddressCollection.FromWinsServer(this.addr.FirstWinsServerAddress);
				}
				catch (IndexOutOfRangeException)
				{
					ipaddressCollection = Win32IPAddressCollection.Empty;
				}
				return ipaddressCollection;
			}
		}

		// Token: 0x04002894 RID: 10388
		private readonly Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04002895 RID: 10389
		private readonly Win32_MIB_IFROW mib4;

		// Token: 0x04002896 RID: 10390
		private readonly Win32_MIB_IFROW mib6;
	}
}

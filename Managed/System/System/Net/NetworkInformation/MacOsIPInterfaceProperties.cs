using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000636 RID: 1590
	internal class MacOsIPInterfaceProperties : UnixIPInterfaceProperties
	{
		// Token: 0x060032B6 RID: 12982 RVA: 0x000BFA00 File Offset: 0x000BDC00
		public MacOsIPInterfaceProperties(MacOsNetworkInterface iface, List<IPAddress> addresses)
			: base(iface, addresses)
		{
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000BFB65 File Offset: 0x000BDD65
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			if (this.ipv4iface_properties == null)
			{
				this.ipv4iface_properties = new MacOsIPv4InterfaceProperties(this.iface as MacOsNetworkInterface);
			}
			return this.ipv4iface_properties;
		}

		// Token: 0x060032B8 RID: 12984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ParseRouteInfo_internal(string iface, out string[] gw_addr_list);

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000BFB8C File Offset: 0x000BDD8C
		public override GatewayIPAddressInformationCollection GatewayAddresses
		{
			get
			{
				IPAddressCollection ipaddressCollection = new IPAddressCollection();
				string[] array;
				if (!MacOsIPInterfaceProperties.ParseRouteInfo_internal(this.iface.Name.ToString(), out array))
				{
					return new GatewayIPAddressInformationCollection();
				}
				for (int i = 0; i < array.Length; i++)
				{
					try
					{
						IPAddress ipaddress = IPAddress.Parse(array[i]);
						if (!ipaddress.Equals(IPAddress.Any) && !ipaddressCollection.Contains(ipaddress))
						{
							ipaddressCollection.InternalAdd(ipaddress);
						}
					}
					catch (ArgumentNullException)
					{
					}
				}
				return SystemGatewayIPAddressInformation.ToGatewayIpAddressInformationCollection(ipaddressCollection);
			}
		}
	}
}

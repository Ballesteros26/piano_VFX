using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000619 RID: 1561
	internal class SystemGatewayIPAddressInformation : GatewayIPAddressInformation
	{
		// Token: 0x060031D2 RID: 12754 RVA: 0x000BE2FC File Offset: 0x000BC4FC
		internal SystemGatewayIPAddressInformation(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000BE30B File Offset: 0x000BC50B
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000BE314 File Offset: 0x000BC514
		internal static GatewayIPAddressInformationCollection ToGatewayIpAddressInformationCollection(IPAddressCollection addresses)
		{
			GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
			foreach (IPAddress ipaddress in addresses)
			{
				gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(ipaddress));
			}
			return gatewayIPAddressInformationCollection;
		}

		// Token: 0x0400281B RID: 10267
		private IPAddress address;
	}
}

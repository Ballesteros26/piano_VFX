using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063A RID: 1594
	internal sealed class MacOsIPv4InterfaceProperties : UnixIPv4InterfaceProperties
	{
		// Token: 0x060032D3 RID: 13011 RVA: 0x000BFF44 File Offset: 0x000BE144
		public MacOsIPv4InterfaceProperties(MacOsNetworkInterface iface)
			: base(iface)
		{
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x00004240 File Offset: 0x00002440
		public override bool IsForwardingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x00004240 File Offset: 0x00002440
		public override int Mtu
		{
			get
			{
				return 0;
			}
		}
	}
}

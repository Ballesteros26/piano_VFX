using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000640 RID: 1600
	internal class Win32IPv6InterfaceProperties : IPv6InterfaceProperties
	{
		// Token: 0x06003308 RID: 13064 RVA: 0x000C0245 File Offset: 0x000BE445
		public Win32IPv6InterfaceProperties(Win32_MIB_IFROW mib)
		{
			this.mib = mib;
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x000C0254 File Offset: 0x000BE454
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000C0261 File Offset: 0x000BE461
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x040028A1 RID: 10401
		private Win32_MIB_IFROW mib;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063B RID: 1595
	internal sealed class Win32IPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x060032D6 RID: 13014
		[DllImport("iphlpapi.dll")]
		private static extern int GetPerAdapterInfo(int IfIndex, Win32_IP_PER_ADAPTER_INFO pPerAdapterInfo, ref int pOutBufLen);

		// Token: 0x060032D7 RID: 13015 RVA: 0x000BFFEC File Offset: 0x000BE1EC
		public Win32IPv4InterfaceProperties(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib)
		{
			this.addr = addr;
			this.mib = mib;
			int num = 0;
			Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, null, ref num);
			this.painfo = new Win32_IP_PER_ADAPTER_INFO();
			int perAdapterInfo = Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, this.painfo, ref num);
			if (perAdapterInfo != 0)
			{
				throw new NetworkInformationException(perAdapterInfo);
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000C0047 File Offset: 0x000BE247
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000C0054 File Offset: 0x000BE254
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return this.painfo.AutoconfigActive > 0U;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000C0064 File Offset: 0x000BE264
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return this.painfo.AutoconfigEnabled > 0U;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x000C0074 File Offset: 0x000BE274
		public override bool IsDhcpEnabled
		{
			get
			{
				return this.addr.DhcpEnabled;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x000C0081 File Offset: 0x000BE281
		public override bool IsForwardingEnabled
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableRouting > 0U;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x000C0090 File Offset: 0x000BE290
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x000C009D File Offset: 0x000BE29D
		public override bool UsesWins
		{
			get
			{
				return this.addr.FirstWinsServerAddress != IntPtr.Zero;
			}
		}

		// Token: 0x04002898 RID: 10392
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04002899 RID: 10393
		private Win32_IP_PER_ADAPTER_INFO painfo;

		// Token: 0x0400289A RID: 10394
		private Win32_MIB_IFROW mib;
	}
}

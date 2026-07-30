using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000666 RID: 1638
	internal class Win32NetworkInterface2 : NetworkInterface
	{
		// Token: 0x06003409 RID: 13321
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersInfo(IntPtr info, ref int size);

		// Token: 0x0600340A RID: 13322
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetIfEntry(ref Win32_MIB_IFROW row);

		// Token: 0x0600340B RID: 13323 RVA: 0x000C23D4 File Offset: 0x000C05D4
		private static Win32_IP_ADAPTER_INFO[] GetAdaptersInfo()
		{
			int num = 0;
			Win32NetworkInterface2.GetAdaptersInfo(IntPtr.Zero, ref num);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			int adaptersInfo = Win32NetworkInterface2.GetAdaptersInfo(intPtr, ref num);
			if (adaptersInfo != 0)
			{
				throw new NetworkInformationException(adaptersInfo);
			}
			List<Win32_IP_ADAPTER_INFO> list = new List<Win32_IP_ADAPTER_INFO>();
			IntPtr intPtr2 = intPtr;
			while (intPtr2 != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_INFO win32_IP_ADAPTER_INFO = Marshal.PtrToStructure<Win32_IP_ADAPTER_INFO>(intPtr2);
				list.Add(win32_IP_ADAPTER_INFO);
				intPtr2 = win32_IP_ADAPTER_INFO.Next;
			}
			return list.ToArray();
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000C2440 File Offset: 0x000C0640
		internal Win32NetworkInterface2(Win32_IP_ADAPTER_ADDRESSES addr)
		{
			this.addr = addr;
			this.mib4 = default(Win32_MIB_IFROW);
			this.mib4.Index = addr.Alignment.IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib4) != 0)
			{
				this.mib4.Index = -1;
			}
			this.mib6 = default(Win32_MIB_IFROW);
			this.mib6.Index = addr.Ipv6IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib6) != 0)
			{
				this.mib6.Index = -1;
			}
			this.ip4stats = new Win32IPv4InterfaceStatistics(this.mib4);
			this.ip_if_props = new Win32IPInterfaceProperties2(addr, this.mib4, this.mib6);
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000C24F4 File Offset: 0x000C06F4
		public override IPInterfaceProperties GetIPProperties()
		{
			return this.ip_if_props;
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000C24FC File Offset: 0x000C06FC
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			return this.ip4stats;
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000C2504 File Offset: 0x000C0704
		public override PhysicalAddress GetPhysicalAddress()
		{
			byte[] array = new byte[this.addr.PhysicalAddressLength];
			Array.Copy(this.addr.PhysicalAddress, 0, array, 0, array.Length);
			return new PhysicalAddress(array);
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000C253E File Offset: 0x000C073E
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			if (networkInterfaceComponent != NetworkInterfaceComponent.IPv4)
			{
				return networkInterfaceComponent == NetworkInterfaceComponent.IPv6 && this.mib6.Index >= 0;
			}
			return this.mib4.Index >= 0;
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000C256E File Offset: 0x000C076E
		public override string Description
		{
			get
			{
				return this.addr.Description;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000C257B File Offset: 0x000C077B
		public override string Id
		{
			get
			{
				return this.addr.AdapterName;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000C2588 File Offset: 0x000C0788
		public override bool IsReceiveOnly
		{
			get
			{
				return this.addr.IsReceiveOnly;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x000C2595 File Offset: 0x000C0795
		public override string Name
		{
			get
			{
				return this.addr.FriendlyName;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000C25A2 File Offset: 0x000C07A2
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.addr.IfType;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000C25AF File Offset: 0x000C07AF
		public override OperationalStatus OperationalStatus
		{
			get
			{
				return this.addr.OperStatus;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000C25BC File Offset: 0x000C07BC
		public override long Speed
		{
			get
			{
				return (long)((ulong)((this.mib6.Index >= 0) ? this.mib6.Speed : this.mib4.Speed));
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000C25E5 File Offset: 0x000C07E5
		public override bool SupportsMulticast
		{
			get
			{
				return !this.addr.NoMulticast;
			}
		}

		// Token: 0x0400294E RID: 10574
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x0400294F RID: 10575
		private Win32_MIB_IFROW mib4;

		// Token: 0x04002950 RID: 10576
		private Win32_MIB_IFROW mib6;

		// Token: 0x04002951 RID: 10577
		private Win32IPv4InterfaceStatistics ip4stats;

		// Token: 0x04002952 RID: 10578
		private IPInterfaceProperties ip_if_props;
	}
}

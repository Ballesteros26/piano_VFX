using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000663 RID: 1635
	internal abstract class UnixNetworkInterface : NetworkInterface
	{
		// Token: 0x060033F1 RID: 13297 RVA: 0x000C1FBF File Offset: 0x000C01BF
		internal UnixNetworkInterface(string name)
		{
			this.name = name;
			this.addresses = new List<IPAddress>();
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000C1FD9 File Offset: 0x000C01D9
		internal void AddAddress(IPAddress address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000C1FE7 File Offset: 0x000C01E7
		internal void SetLinkLayerInfo(int index, byte[] macAddress, NetworkInterfaceType type)
		{
			this.macAddress = macAddress;
			this.type = type;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000C1FF7 File Offset: 0x000C01F7
		public override PhysicalAddress GetPhysicalAddress()
		{
			if (this.macAddress != null)
			{
				return new PhysicalAddress(this.macAddress);
			}
			return PhysicalAddress.None;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000C2014 File Offset: 0x000C0214
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			bool flag = networkInterfaceComponent == NetworkInterfaceComponent.IPv4;
			bool flag2 = !flag && networkInterfaceComponent == NetworkInterfaceComponent.IPv6;
			foreach (IPAddress ipaddress in this.addresses)
			{
				if (flag && ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					return true;
				}
				if (flag2 && ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000C2098 File Offset: 0x000C0298
		public override string Description
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000C2098 File Offset: 0x000C0298
		public override string Id
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x00004240 File Offset: 0x00002440
		public override bool IsReceiveOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000C2098 File Offset: 0x000C0298
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x000C20A0 File Offset: 0x000C02A0
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060033FB RID: 13307 RVA: 0x000C20A8 File Offset: 0x000C02A8
		[MonoTODO("Parse dmesg?")]
		public override long Speed
		{
			get
			{
				return 1000000L;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x000C20B0 File Offset: 0x000C02B0
		internal int NameIndex
		{
			get
			{
				return NetworkInterfaceFactory.UnixNetworkInterfaceAPI.if_nametoindex(this.Name);
			}
		}

		// Token: 0x04002944 RID: 10564
		protected IPv4InterfaceStatistics ipv4stats;

		// Token: 0x04002945 RID: 10565
		protected IPInterfaceProperties ipproperties;

		// Token: 0x04002946 RID: 10566
		private string name;

		// Token: 0x04002947 RID: 10567
		protected List<IPAddress> addresses;

		// Token: 0x04002948 RID: 10568
		private byte[] macAddress;

		// Token: 0x04002949 RID: 10569
		private NetworkInterfaceType type;
	}
}

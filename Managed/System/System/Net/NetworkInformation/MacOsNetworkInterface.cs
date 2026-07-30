using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000665 RID: 1637
	internal sealed class MacOsNetworkInterface : UnixNetworkInterface
	{
		// Token: 0x06003404 RID: 13316 RVA: 0x000C2360 File Offset: 0x000C0560
		internal MacOsNetworkInterface(string name, uint ifa_flags)
			: base(name)
		{
			this._ifa_flags = ifa_flags;
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000C2370 File Offset: 0x000C0570
		public override IPInterfaceProperties GetIPProperties()
		{
			if (this.ipproperties == null)
			{
				this.ipproperties = new MacOsIPInterfaceProperties(this, this.addresses);
			}
			return this.ipproperties;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000C2392 File Offset: 0x000C0592
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			if (this.ipv4stats == null)
			{
				this.ipv4stats = new MacOsIPv4InterfaceStatistics(this);
			}
			return this.ipv4stats;
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x000C23AE File Offset: 0x000C05AE
		public override OperationalStatus OperationalStatus
		{
			get
			{
				if ((this._ifa_flags & 1U) == 1U)
				{
					return OperationalStatus.Up;
				}
				return OperationalStatus.Unknown;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000C23BE File Offset: 0x000C05BE
		public override bool SupportsMulticast
		{
			get
			{
				return (this._ifa_flags & 32768U) == 32768U;
			}
		}

		// Token: 0x0400294D RID: 10573
		private uint _ifa_flags;
	}
}

using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063D RID: 1597
	internal class Win32IPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x060032E0 RID: 13024 RVA: 0x000C00B4 File Offset: 0x000BE2B4
		public Win32IPv4InterfaceStatistics(Win32_MIB_IFROW info)
		{
			this.info = info;
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000C00C3 File Offset: 0x000BE2C3
		public override long BytesReceived
		{
			get
			{
				return (long)this.info.InOctets;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000C00D1 File Offset: 0x000BE2D1
		public override long BytesSent
		{
			get
			{
				return (long)this.info.OutOctets;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000C00DF File Offset: 0x000BE2DF
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return (long)this.info.InDiscards;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000C00ED File Offset: 0x000BE2ED
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return (long)this.info.InErrors;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x000C00FB File Offset: 0x000BE2FB
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return (long)this.info.InUnknownProtos;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x000C0109 File Offset: 0x000BE309
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return (long)this.info.InNUcastPkts;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x000C0117 File Offset: 0x000BE317
		public override long NonUnicastPacketsSent
		{
			get
			{
				return (long)this.info.OutNUcastPkts;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x000C0125 File Offset: 0x000BE325
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return (long)this.info.OutDiscards;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x000C0133 File Offset: 0x000BE333
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return (long)this.info.OutErrors;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x000C0141 File Offset: 0x000BE341
		public override long OutputQueueLength
		{
			get
			{
				return (long)this.info.OutQLen;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x000C014F File Offset: 0x000BE34F
		public override long UnicastPacketsReceived
		{
			get
			{
				return (long)this.info.InUcastPkts;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x000C015D File Offset: 0x000BE35D
		public override long UnicastPacketsSent
		{
			get
			{
				return (long)this.info.OutUcastPkts;
			}
		}

		// Token: 0x0400289F RID: 10399
		private Win32_MIB_IFROW info;
	}
}

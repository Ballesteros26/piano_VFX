using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063E RID: 1598
	internal class LinuxIPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x060032ED RID: 13037 RVA: 0x000C016B File Offset: 0x000BE36B
		public LinuxIPv4InterfaceStatistics(LinuxNetworkInterface parent)
		{
			this.linux = parent;
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000C017C File Offset: 0x000BE37C
		private long Read(string file)
		{
			long num;
			try
			{
				num = long.Parse(LinuxNetworkInterface.ReadLine(this.linux.IfacePath + file));
			}
			catch
			{
				num = 0L;
			}
			return num;
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x000C01C0 File Offset: 0x000BE3C0
		public override long BytesReceived
		{
			get
			{
				return this.Read("statistics/rx_bytes");
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060032F0 RID: 13040 RVA: 0x000C01CD File Offset: 0x000BE3CD
		public override long BytesSent
		{
			get
			{
				return this.Read("statistics/tx_bytes");
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x000C01DA File Offset: 0x000BE3DA
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return this.Read("statistics/rx_dropped");
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000C01E7 File Offset: 0x000BE3E7
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return this.Read("statistics/rx_errors");
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x00045828 File Offset: 0x00043A28
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000C01F4 File Offset: 0x000BE3F4
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return this.Read("statistics/multicast");
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000C01F4 File Offset: 0x000BE3F4
		public override long NonUnicastPacketsSent
		{
			get
			{
				return this.Read("statistics/multicast");
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000C0201 File Offset: 0x000BE401
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return this.Read("statistics/tx_dropped");
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000C020E File Offset: 0x000BE40E
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return this.Read("statistics/tx_errors");
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000C021B File Offset: 0x000BE41B
		public override long OutputQueueLength
		{
			get
			{
				return 1024L;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x000C0223 File Offset: 0x000BE423
		public override long UnicastPacketsReceived
		{
			get
			{
				return this.Read("statistics/rx_packets");
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000C0230 File Offset: 0x000BE430
		public override long UnicastPacketsSent
		{
			get
			{
				return this.Read("statistics/tx_packets");
			}
		}

		// Token: 0x040028A0 RID: 10400
		private LinuxNetworkInterface linux;
	}
}

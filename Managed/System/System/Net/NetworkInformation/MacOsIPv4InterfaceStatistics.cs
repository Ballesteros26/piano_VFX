using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063F RID: 1599
	internal class MacOsIPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x060032FB RID: 13051 RVA: 0x000C023D File Offset: 0x000BE43D
		public MacOsIPv4InterfaceStatistics(MacOsNetworkInterface parent)
		{
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x00045828 File Offset: 0x00043A28
		public override long BytesReceived
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x00045828 File Offset: 0x00043A28
		public override long BytesSent
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x00045828 File Offset: 0x00043A28
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x00045828 File Offset: 0x00043A28
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x00045828 File Offset: 0x00043A28
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x00045828 File Offset: 0x00043A28
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06003302 RID: 13058 RVA: 0x00045828 File Offset: 0x00043A28
		public override long NonUnicastPacketsSent
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x00045828 File Offset: 0x00043A28
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x00045828 File Offset: 0x00043A28
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x00045828 File Offset: 0x00043A28
		public override long OutputQueueLength
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x00045828 File Offset: 0x00043A28
		public override long UnicastPacketsReceived
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x00045828 File Offset: 0x00043A28
		public override long UnicastPacketsSent
		{
			get
			{
				return 0L;
			}
		}
	}
}

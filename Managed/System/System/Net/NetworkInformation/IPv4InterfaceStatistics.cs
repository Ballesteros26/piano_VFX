using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides statistical data for a network interface on the local computer.</summary>
	// Token: 0x020005FE RID: 1534
	public abstract class IPv4InterfaceStatistics
	{
		/// <summary>Gets the number of bytes that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were received on the interface.</returns>
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003121 RID: 12577
		public abstract long BytesReceived { get; }

		/// <summary>Gets the number of bytes that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were transmitted on the interface.</returns>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003122 RID: 12578
		public abstract long BytesSent { get; }

		/// <summary>Gets the number of incoming packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded incoming packets.</returns>
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06003123 RID: 12579
		public abstract long IncomingPacketsDiscarded { get; }

		/// <summary>Gets the number of incoming packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with errors.</returns>
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003124 RID: 12580
		public abstract long IncomingPacketsWithErrors { get; }

		/// <summary>Gets the number of incoming packets with an unknown protocol that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with an unknown protocol.</returns>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003125 RID: 12581
		public abstract long IncomingUnknownProtocolPackets { get; }

		/// <summary>Gets the number of non-unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were received on the interface.</returns>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003126 RID: 12582
		public abstract long NonUnicastPacketsReceived { get; }

		/// <summary>Gets the number of non-unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were sent on the interface.</returns>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06003127 RID: 12583
		public abstract long NonUnicastPacketsSent { get; }

		/// <summary>Gets the number of outgoing packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded outgoing packets.</returns>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06003128 RID: 12584
		public abstract long OutgoingPacketsDiscarded { get; }

		/// <summary>Gets the number of outgoing packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets with errors.</returns>
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06003129 RID: 12585
		public abstract long OutgoingPacketsWithErrors { get; }

		/// <summary>Gets the length of the output queue.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets in the output queue.</returns>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600312A RID: 12586
		public abstract long OutputQueueLength { get; }

		/// <summary>Gets the number of unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were received on the interface.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600312B RID: 12587
		public abstract long UnicastPacketsReceived { get; }

		/// <summary>Gets the number of unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were sent on the interface.</returns>
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600312C RID: 12588
		public abstract long UnicastPacketsSent { get; }
	}
}

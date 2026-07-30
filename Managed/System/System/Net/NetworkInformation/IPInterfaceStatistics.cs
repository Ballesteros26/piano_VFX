using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Protocol (IP) statistical data for an network interface on the local computer.</summary>
	// Token: 0x020005FD RID: 1533
	public abstract class IPInterfaceStatistics
	{
		/// <summary>Gets the number of bytes that were received on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of bytes that were received on the interface.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003114 RID: 12564
		public abstract long BytesReceived { get; }

		/// <summary>Gets the number of bytes that were sent on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of bytes that were sent on the interface.</returns>
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003115 RID: 12565
		public abstract long BytesSent { get; }

		/// <summary>Gets the number of incoming packets that were discarded.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of incoming packets that were discarded.</returns>
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003116 RID: 12566
		public abstract long IncomingPacketsDiscarded { get; }

		/// <summary>Gets the number of incoming packets with errors.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of incoming packets with errors.</returns>
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003117 RID: 12567
		public abstract long IncomingPacketsWithErrors { get; }

		/// <summary>Gets the number of incoming packets with an unknown protocol that were received on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of incoming packets with an unknown protocol that were received on the interface.</returns>
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06003118 RID: 12568
		public abstract long IncomingUnknownProtocolPackets { get; }

		/// <summary>Gets the number of non-unicast packets that were received on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of incoming non-unicast packets received on the interface.</returns>
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06003119 RID: 12569
		public abstract long NonUnicastPacketsReceived { get; }

		/// <summary>Gets the number of non-unicast packets that were sent on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of non-unicast packets that were sent on the interface.</returns>
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600311A RID: 12570
		public abstract long NonUnicastPacketsSent { get; }

		/// <summary>Gets the number of outgoing packets that were discarded.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of outgoing packets that were discarded.</returns>
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600311B RID: 12571
		public abstract long OutgoingPacketsDiscarded { get; }

		/// <summary>Gets the number of outgoing packets with errors.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of outgoing packets with errors.</returns>
		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600311C RID: 12572
		public abstract long OutgoingPacketsWithErrors { get; }

		/// <summary>Gets the length of the output queue.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of packets in the output queue.</returns>
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600311D RID: 12573
		public abstract long OutputQueueLength { get; }

		/// <summary>Gets the number of unicast packets that were received on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of unicast packets that were received on the interface.</returns>
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600311E RID: 12574
		public abstract long UnicastPacketsReceived { get; }

		/// <summary>Gets the number of unicast packets that were sent on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of unicast packets that were sent on the interface.</returns>
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600311F RID: 12575
		public abstract long UnicastPacketsSent { get; }
	}
}

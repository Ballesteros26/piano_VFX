using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Protocol (IP) statistical data.</summary>
	// Token: 0x020005FB RID: 1531
	public abstract class IPGlobalStatistics
	{
		/// <summary>Gets the default time-to-live (TTL) value for Internet Protocol (IP) packets.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the TTL.</returns>
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060030F0 RID: 12528
		public abstract int DefaultTtl { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that specifies whether Internet Protocol (IP) packet forwarding is enabled.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that specifies whether packet forwarding is enabled.</returns>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060030F1 RID: 12529
		public abstract bool ForwardingEnabled { get; }

		/// <summary>Gets the number of network interfaces.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value containing the number of network interfaces for the address family used to obtain this <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> instance.</returns>
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060030F2 RID: 12530
		public abstract int NumberOfInterfaces { get; }

		/// <summary>Gets the number of Internet Protocol (IP) addresses assigned to the local computer.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of IP addresses assigned to the address family (Internet Protocol version 4 or Internet Protocol version 6) described by this object.</returns>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060030F3 RID: 12531
		public abstract int NumberOfIPAddresses { get; }

		/// <summary>Gets the number of outbound Internet Protocol (IP) packets.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets.</returns>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060030F4 RID: 12532
		public abstract long OutputPacketRequests { get; }

		/// <summary>Gets the number of routes that have been discarded from the routing table.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of valid routes that have been discarded.</returns>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060030F5 RID: 12533
		public abstract long OutputPacketRoutingDiscards { get; }

		/// <summary>Gets the number of transmitted Internet Protocol (IP) packets that have been discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets that have been discarded.</returns>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060030F6 RID: 12534
		public abstract long OutputPacketsDiscarded { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets for which the local computer could not determine a route to the destination address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of packets that could not be sent because a route could not be found.</returns>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060030F7 RID: 12535
		public abstract long OutputPacketsWithNoRoute { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that could not be fragmented.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets that required fragmentation but had the "Don't Fragment" bit set.</returns>
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060030F8 RID: 12536
		public abstract long PacketFragmentFailures { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that required reassembly.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packet reassemblies required.</returns>
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060030F9 RID: 12537
		public abstract long PacketReassembliesRequired { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that were not successfully reassembled.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets that could not be reassembled.</returns>
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060030FA RID: 12538
		public abstract long PacketReassemblyFailures { get; }

		/// <summary>Gets the maximum amount of time within which all fragments of an Internet Protocol (IP) packet must arrive.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the maximum number of milliseconds within which all fragments of a packet must arrive to avoid being discarded.</returns>
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060030FB RID: 12539
		public abstract long PacketReassemblyTimeout { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets fragmented.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of fragmented packets.</returns>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060030FC RID: 12540
		public abstract long PacketsFragmented { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets reassembled.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of fragmented packets that have been successfully reassembled.</returns>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060030FD RID: 12541
		public abstract long PacketsReassembled { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060030FE RID: 12542
		public abstract long ReceivedPackets { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets delivered.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets delivered.</returns>
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060030FF RID: 12543
		public abstract long ReceivedPacketsDelivered { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that have been received and discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets that have been discarded.</returns>
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003100 RID: 12544
		public abstract long ReceivedPacketsDiscarded { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets forwarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of forwarded packets.</returns>
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003101 RID: 12545
		public abstract long ReceivedPacketsForwarded { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets with address errors that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received with errors in the address portion of the header.</returns>
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003102 RID: 12546
		public abstract long ReceivedPacketsWithAddressErrors { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets with header errors that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received and discarded due to errors in the header.</returns>
		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003103 RID: 12547
		public abstract long ReceivedPacketsWithHeadersErrors { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets received on the local machine with an unknown protocol in the header.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the total number of IP packets received with an unknown protocol.</returns>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06003104 RID: 12548
		public abstract long ReceivedPacketsWithUnknownProtocol { get; }

		/// <summary>Gets the number of routes in the Internet Protocol (IP) routing table.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of routes in the routing table.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003105 RID: 12549
		public abstract int NumberOfRoutes { get; }
	}
}

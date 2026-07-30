using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Control Message Protocol for IPv4 (ICMPv4) statistical data for the local computer.</summary>
	// Token: 0x02000604 RID: 1540
	public abstract class IcmpV4Statistics
	{
		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were received.</returns>
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600313A RID: 12602
		public abstract long AddressMaskRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were sent.</returns>
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600313B RID: 12603
		public abstract long AddressMaskRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were received.</returns>
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600313C RID: 12604
		public abstract long AddressMaskRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were sent.</returns>
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600313D RID: 12605
		public abstract long AddressMaskRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were received because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages that were received.</returns>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600313E RID: 12606
		public abstract long DestinationUnreachableMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages sent.</returns>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600313F RID: 12607
		public abstract long DestinationUnreachableMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were received.</returns>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06003140 RID: 12608
		public abstract long EchoRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were sent.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003141 RID: 12609
		public abstract long EchoRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were received.</returns>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06003142 RID: 12610
		public abstract long EchoRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were sent.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06003143 RID: 12611
		public abstract long EchoRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages that were received.</returns>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06003144 RID: 12612
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP error messages that were sent.</returns>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06003145 RID: 12613
		public abstract long ErrorsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were received.</returns>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06003146 RID: 12614
		public abstract long MessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were sent.</returns>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003147 RID: 12615
		public abstract long MessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were received.</returns>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003148 RID: 12616
		public abstract long ParameterProblemsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were sent.</returns>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003149 RID: 12617
		public abstract long ParameterProblemsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were received.</returns>
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x0600314A RID: 12618
		public abstract long RedirectsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were sent.</returns>
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x0600314B RID: 12619
		public abstract long RedirectsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were received.</returns>
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600314C RID: 12620
		public abstract long SourceQuenchesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were sent.</returns>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600314D RID: 12621
		public abstract long SourceQuenchesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were received.</returns>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600314E RID: 12622
		public abstract long TimeExceededMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were sent.</returns>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600314F RID: 12623
		public abstract long TimeExceededMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were received.</returns>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06003150 RID: 12624
		public abstract long TimestampRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were sent.</returns>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06003151 RID: 12625
		public abstract long TimestampRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were received.</returns>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06003152 RID: 12626
		public abstract long TimestampRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were sent.</returns>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003153 RID: 12627
		public abstract long TimestampRequestsSent { get; }
	}
}

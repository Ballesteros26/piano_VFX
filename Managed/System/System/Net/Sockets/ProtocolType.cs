using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the protocols that the <see cref="T:System.Net.Sockets.Socket" /> class supports.</summary>
	// Token: 0x020005BF RID: 1471
	public enum ProtocolType
	{
		/// <summary>Internet Protocol.</summary>
		// Token: 0x04002608 RID: 9736
		IP,
		/// <summary>IPv6 Hop by Hop Options header.</summary>
		// Token: 0x04002609 RID: 9737
		IPv6HopByHopOptions = 0,
		/// <summary>Internet Control Message Protocol.</summary>
		// Token: 0x0400260A RID: 9738
		Icmp,
		/// <summary>Internet Group Management Protocol.</summary>
		// Token: 0x0400260B RID: 9739
		Igmp,
		/// <summary>Gateway To Gateway Protocol.</summary>
		// Token: 0x0400260C RID: 9740
		Ggp,
		/// <summary>Internet Protocol version 4.</summary>
		// Token: 0x0400260D RID: 9741
		IPv4,
		/// <summary>Transmission Control Protocol.</summary>
		// Token: 0x0400260E RID: 9742
		Tcp = 6,
		/// <summary>PARC Universal Packet Protocol.</summary>
		// Token: 0x0400260F RID: 9743
		Pup = 12,
		/// <summary>User Datagram Protocol.</summary>
		// Token: 0x04002610 RID: 9744
		Udp = 17,
		/// <summary>Internet Datagram Protocol.</summary>
		// Token: 0x04002611 RID: 9745
		Idp = 22,
		/// <summary>Internet Protocol version 6 (IPv6). </summary>
		// Token: 0x04002612 RID: 9746
		IPv6 = 41,
		/// <summary>IPv6 Routing header.</summary>
		// Token: 0x04002613 RID: 9747
		IPv6RoutingHeader = 43,
		/// <summary>IPv6 Fragment header.</summary>
		// Token: 0x04002614 RID: 9748
		IPv6FragmentHeader,
		/// <summary>IPv6 Encapsulating Security Payload header.</summary>
		// Token: 0x04002615 RID: 9749
		IPSecEncapsulatingSecurityPayload = 50,
		/// <summary>IPv6 Authentication header. For details, see RFC 2292 section 2.2.1, available at http://www.ietf.org.</summary>
		// Token: 0x04002616 RID: 9750
		IPSecAuthenticationHeader,
		/// <summary>Internet Control Message Protocol for IPv6.</summary>
		// Token: 0x04002617 RID: 9751
		IcmpV6 = 58,
		/// <summary>IPv6 No next header.</summary>
		// Token: 0x04002618 RID: 9752
		IPv6NoNextHeader,
		/// <summary>IPv6 Destination Options header.</summary>
		// Token: 0x04002619 RID: 9753
		IPv6DestinationOptions,
		/// <summary>Net Disk Protocol (unofficial).</summary>
		// Token: 0x0400261A RID: 9754
		ND = 77,
		/// <summary>Raw IP packet protocol.</summary>
		// Token: 0x0400261B RID: 9755
		Raw = 255,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x0400261C RID: 9756
		Unspecified = 0,
		/// <summary>Internet Packet Exchange Protocol.</summary>
		// Token: 0x0400261D RID: 9757
		Ipx = 1000,
		/// <summary>Sequenced Packet Exchange protocol.</summary>
		// Token: 0x0400261E RID: 9758
		Spx = 1256,
		/// <summary>Sequenced Packet Exchange version 2 protocol.</summary>
		// Token: 0x0400261F RID: 9759
		SpxII,
		/// <summary>Unknown protocol.</summary>
		// Token: 0x04002620 RID: 9760
		Unknown = -1
	}
}

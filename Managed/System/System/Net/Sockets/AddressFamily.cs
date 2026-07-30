using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the addressing scheme that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020005B6 RID: 1462
	public enum AddressFamily
	{
		/// <summary>Unknown address family.</summary>
		// Token: 0x0400258F RID: 9615
		Unknown = -1,
		/// <summary>Unspecified address family.</summary>
		// Token: 0x04002590 RID: 9616
		Unspecified,
		/// <summary>Unix local to host address.</summary>
		// Token: 0x04002591 RID: 9617
		Unix,
		/// <summary>Address for IP version 4.</summary>
		// Token: 0x04002592 RID: 9618
		InterNetwork,
		/// <summary>ARPANET IMP address.</summary>
		// Token: 0x04002593 RID: 9619
		ImpLink,
		/// <summary>Address for PUP protocols.</summary>
		// Token: 0x04002594 RID: 9620
		Pup,
		/// <summary>Address for MIT CHAOS protocols.</summary>
		// Token: 0x04002595 RID: 9621
		Chaos,
		/// <summary>Address for Xerox NS protocols.</summary>
		// Token: 0x04002596 RID: 9622
		NS,
		/// <summary>IPX or SPX address.</summary>
		// Token: 0x04002597 RID: 9623
		Ipx = 6,
		/// <summary>Address for ISO protocols.</summary>
		// Token: 0x04002598 RID: 9624
		Iso,
		/// <summary>Address for OSI protocols.</summary>
		// Token: 0x04002599 RID: 9625
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) address.</summary>
		// Token: 0x0400259A RID: 9626
		Ecma,
		/// <summary>Address for Datakit protocols.</summary>
		// Token: 0x0400259B RID: 9627
		DataKit,
		/// <summary>Addresses for CCITT protocols, such as X.25.</summary>
		// Token: 0x0400259C RID: 9628
		Ccitt,
		/// <summary>IBM SNA address.</summary>
		// Token: 0x0400259D RID: 9629
		Sna,
		/// <summary>DECnet address.</summary>
		// Token: 0x0400259E RID: 9630
		DecNet,
		/// <summary>Direct data-link interface address.</summary>
		// Token: 0x0400259F RID: 9631
		DataLink,
		/// <summary>LAT address.</summary>
		// Token: 0x040025A0 RID: 9632
		Lat,
		/// <summary>NSC Hyperchannel address.</summary>
		// Token: 0x040025A1 RID: 9633
		HyperChannel,
		/// <summary>AppleTalk address.</summary>
		// Token: 0x040025A2 RID: 9634
		AppleTalk,
		/// <summary>NetBios address.</summary>
		// Token: 0x040025A3 RID: 9635
		NetBios,
		/// <summary>VoiceView address.</summary>
		// Token: 0x040025A4 RID: 9636
		VoiceView,
		/// <summary>FireFox address.</summary>
		// Token: 0x040025A5 RID: 9637
		FireFox,
		/// <summary>Banyan address.</summary>
		// Token: 0x040025A6 RID: 9638
		Banyan = 21,
		/// <summary>Native ATM services address.</summary>
		// Token: 0x040025A7 RID: 9639
		Atm,
		/// <summary>Address for IP version 6.</summary>
		// Token: 0x040025A8 RID: 9640
		InterNetworkV6,
		/// <summary>Address for Microsoft cluster products.</summary>
		// Token: 0x040025A9 RID: 9641
		Cluster,
		/// <summary>IEEE 1284.4 workgroup address.</summary>
		// Token: 0x040025AA RID: 9642
		Ieee12844,
		/// <summary>IrDA address.</summary>
		// Token: 0x040025AB RID: 9643
		Irda,
		/// <summary>Address for Network Designers OSI gateway-enabled protocols.</summary>
		// Token: 0x040025AC RID: 9644
		NetworkDesigners = 28,
		/// <summary>MAX address.</summary>
		// Token: 0x040025AD RID: 9645
		Max
	}
}

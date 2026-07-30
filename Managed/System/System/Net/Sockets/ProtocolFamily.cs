using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the type of protocol that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020005BE RID: 1470
	public enum ProtocolFamily
	{
		/// <summary>Unknown protocol.</summary>
		// Token: 0x040025E8 RID: 9704
		Unknown = -1,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x040025E9 RID: 9705
		Unspecified,
		/// <summary>Unix local to host protocol.</summary>
		// Token: 0x040025EA RID: 9706
		Unix,
		/// <summary>IP version 4 protocol.</summary>
		// Token: 0x040025EB RID: 9707
		InterNetwork,
		/// <summary>ARPANET IMP protocol.</summary>
		// Token: 0x040025EC RID: 9708
		ImpLink,
		/// <summary>PUP protocol.</summary>
		// Token: 0x040025ED RID: 9709
		Pup,
		/// <summary>MIT CHAOS protocol.</summary>
		// Token: 0x040025EE RID: 9710
		Chaos,
		/// <summary>Xerox NS protocol.</summary>
		// Token: 0x040025EF RID: 9711
		NS,
		/// <summary>IPX or SPX protocol.</summary>
		// Token: 0x040025F0 RID: 9712
		Ipx = 6,
		/// <summary>ISO protocol.</summary>
		// Token: 0x040025F1 RID: 9713
		Iso,
		/// <summary>OSI protocol.</summary>
		// Token: 0x040025F2 RID: 9714
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) protocol.</summary>
		// Token: 0x040025F3 RID: 9715
		Ecma,
		/// <summary>DataKit protocol.</summary>
		// Token: 0x040025F4 RID: 9716
		DataKit,
		/// <summary>CCITT protocol, such as X.25.</summary>
		// Token: 0x040025F5 RID: 9717
		Ccitt,
		/// <summary>IBM SNA protocol.</summary>
		// Token: 0x040025F6 RID: 9718
		Sna,
		/// <summary>DECNet protocol.</summary>
		// Token: 0x040025F7 RID: 9719
		DecNet,
		/// <summary>Direct data link protocol.</summary>
		// Token: 0x040025F8 RID: 9720
		DataLink,
		/// <summary>LAT protocol.</summary>
		// Token: 0x040025F9 RID: 9721
		Lat,
		/// <summary>NSC HyperChannel protocol.</summary>
		// Token: 0x040025FA RID: 9722
		HyperChannel,
		/// <summary>AppleTalk protocol.</summary>
		// Token: 0x040025FB RID: 9723
		AppleTalk,
		/// <summary>NetBIOS protocol.</summary>
		// Token: 0x040025FC RID: 9724
		NetBios,
		/// <summary>VoiceView protocol.</summary>
		// Token: 0x040025FD RID: 9725
		VoiceView,
		/// <summary>FireFox protocol.</summary>
		// Token: 0x040025FE RID: 9726
		FireFox,
		/// <summary>Banyan protocol.</summary>
		// Token: 0x040025FF RID: 9727
		Banyan = 21,
		/// <summary>Native ATM services protocol.</summary>
		// Token: 0x04002600 RID: 9728
		Atm,
		/// <summary>IP version 6 protocol.</summary>
		// Token: 0x04002601 RID: 9729
		InterNetworkV6,
		/// <summary>Microsoft Cluster products protocol.</summary>
		// Token: 0x04002602 RID: 9730
		Cluster,
		/// <summary>IEEE 1284.4 workgroup protocol.</summary>
		// Token: 0x04002603 RID: 9731
		Ieee12844,
		/// <summary>IrDA protocol.</summary>
		// Token: 0x04002604 RID: 9732
		Irda,
		/// <summary>Network Designers OSI gateway enabled protocol.</summary>
		// Token: 0x04002605 RID: 9733
		NetworkDesigners = 28,
		/// <summary>MAX protocol.</summary>
		// Token: 0x04002606 RID: 9734
		Max
	}
}

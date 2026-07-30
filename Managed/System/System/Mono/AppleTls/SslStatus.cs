using System;

namespace Mono.AppleTls
{
	// Token: 0x020000C0 RID: 192
	internal enum SslStatus
	{
		// Token: 0x04000AE8 RID: 2792
		Success,
		// Token: 0x04000AE9 RID: 2793
		Protocol = -9800,
		// Token: 0x04000AEA RID: 2794
		Negotiation = -9801,
		// Token: 0x04000AEB RID: 2795
		FatalAlert = -9802,
		// Token: 0x04000AEC RID: 2796
		WouldBlock = -9803,
		// Token: 0x04000AED RID: 2797
		SessionNotFound = -9804,
		// Token: 0x04000AEE RID: 2798
		ClosedGraceful = -9805,
		// Token: 0x04000AEF RID: 2799
		ClosedAbort = -9806,
		// Token: 0x04000AF0 RID: 2800
		XCertChainInvalid = -9807,
		// Token: 0x04000AF1 RID: 2801
		BadCert = -9808,
		// Token: 0x04000AF2 RID: 2802
		Crypto = -9809,
		// Token: 0x04000AF3 RID: 2803
		Internal = -9810,
		// Token: 0x04000AF4 RID: 2804
		ModuleAttach = -9811,
		// Token: 0x04000AF5 RID: 2805
		UnknownRootCert = -9812,
		// Token: 0x04000AF6 RID: 2806
		NoRootCert = -9813,
		// Token: 0x04000AF7 RID: 2807
		CertExpired = -9814,
		// Token: 0x04000AF8 RID: 2808
		CertNotYetValid = -9815,
		// Token: 0x04000AF9 RID: 2809
		ClosedNotNotified = -9816,
		// Token: 0x04000AFA RID: 2810
		BufferOverflow = -9817,
		// Token: 0x04000AFB RID: 2811
		BadCipherSuite = -9818,
		// Token: 0x04000AFC RID: 2812
		PeerUnexpectedMsg = -9819,
		// Token: 0x04000AFD RID: 2813
		PeerBadRecordMac = -9820,
		// Token: 0x04000AFE RID: 2814
		PeerDecryptionFail = -9821,
		// Token: 0x04000AFF RID: 2815
		PeerRecordOverflow = -9822,
		// Token: 0x04000B00 RID: 2816
		PeerDecompressFail = -9823,
		// Token: 0x04000B01 RID: 2817
		PeerHandshakeFail = -9824,
		// Token: 0x04000B02 RID: 2818
		PeerBadCert = -9825,
		// Token: 0x04000B03 RID: 2819
		PeerUnsupportedCert = -9826,
		// Token: 0x04000B04 RID: 2820
		PeerCertRevoked = -9827,
		// Token: 0x04000B05 RID: 2821
		PeerCertExpired = -9828,
		// Token: 0x04000B06 RID: 2822
		PeerCertUnknown = -9829,
		// Token: 0x04000B07 RID: 2823
		IllegalParam = -9830,
		// Token: 0x04000B08 RID: 2824
		PeerUnknownCA = -9831,
		// Token: 0x04000B09 RID: 2825
		PeerAccessDenied = -9832,
		// Token: 0x04000B0A RID: 2826
		PeerDecodeError = -9833,
		// Token: 0x04000B0B RID: 2827
		PeerDecryptError = -9834,
		// Token: 0x04000B0C RID: 2828
		PeerExportRestriction = -9835,
		// Token: 0x04000B0D RID: 2829
		PeerProtocolVersion = -9836,
		// Token: 0x04000B0E RID: 2830
		PeerInsufficientSecurity = -9837,
		// Token: 0x04000B0F RID: 2831
		PeerInternalError = -9838,
		// Token: 0x04000B10 RID: 2832
		PeerUserCancelled = -9839,
		// Token: 0x04000B11 RID: 2833
		PeerNoRenegotiation = -9840,
		// Token: 0x04000B12 RID: 2834
		PeerAuthCompleted = -9841,
		// Token: 0x04000B13 RID: 2835
		PeerClientCertRequested = -9842,
		// Token: 0x04000B14 RID: 2836
		HostNameMismatch = -9843,
		// Token: 0x04000B15 RID: 2837
		ConnectionRefused = -9844,
		// Token: 0x04000B16 RID: 2838
		DecryptionFail = -9845,
		// Token: 0x04000B17 RID: 2839
		BadRecordMac = -9846,
		// Token: 0x04000B18 RID: 2840
		RecordOverflow = -9847,
		// Token: 0x04000B19 RID: 2841
		BadConfiguration = -9848,
		// Token: 0x04000B1A RID: 2842
		UnexpectedRecord = -9849,
		// Token: 0x04000B1B RID: 2843
		SSLWeakPeerEphemeralDHKey = -9850,
		// Token: 0x04000B1C RID: 2844
		SSLClientHelloReceived = -9851
	}
}

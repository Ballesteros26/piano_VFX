using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000076 RID: 118
	public enum AlertDescription : byte
	{
		// Token: 0x04000220 RID: 544
		CloseNotify,
		// Token: 0x04000221 RID: 545
		UnexpectedMessage = 10,
		// Token: 0x04000222 RID: 546
		BadRecordMAC = 20,
		// Token: 0x04000223 RID: 547
		DecryptionFailed_RESERVED,
		// Token: 0x04000224 RID: 548
		RecordOverflow,
		// Token: 0x04000225 RID: 549
		DecompressionFailure = 30,
		// Token: 0x04000226 RID: 550
		HandshakeFailure = 40,
		// Token: 0x04000227 RID: 551
		NoCertificate_RESERVED,
		// Token: 0x04000228 RID: 552
		BadCertificate,
		// Token: 0x04000229 RID: 553
		UnsupportedCertificate,
		// Token: 0x0400022A RID: 554
		CertificateRevoked,
		// Token: 0x0400022B RID: 555
		CertificateExpired,
		// Token: 0x0400022C RID: 556
		CertificateUnknown,
		// Token: 0x0400022D RID: 557
		IlegalParameter,
		// Token: 0x0400022E RID: 558
		UnknownCA,
		// Token: 0x0400022F RID: 559
		AccessDenied,
		// Token: 0x04000230 RID: 560
		DecodeError,
		// Token: 0x04000231 RID: 561
		DecryptError,
		// Token: 0x04000232 RID: 562
		ExportRestriction = 60,
		// Token: 0x04000233 RID: 563
		ProtocolVersion = 70,
		// Token: 0x04000234 RID: 564
		InsuficientSecurity,
		// Token: 0x04000235 RID: 565
		InternalError = 80,
		// Token: 0x04000236 RID: 566
		UserCancelled = 90,
		// Token: 0x04000237 RID: 567
		NoRenegotiation = 100,
		// Token: 0x04000238 RID: 568
		UnsupportedExtension = 110
	}
}

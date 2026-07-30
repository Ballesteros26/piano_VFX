using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies conditions under which verification of certificates in the X509 chain should be conducted.</summary>
	// Token: 0x020003A2 RID: 930
	[Flags]
	public enum X509VerificationFlags
	{
		/// <summary>No flags pertaining to verification are included.</summary>
		// Token: 0x04001967 RID: 6503
		NoFlag = 0,
		/// <summary>Ignore certificates in the chain that are not valid either because they have expired or they are not yet in effect when determining certificate validity.</summary>
		// Token: 0x04001968 RID: 6504
		IgnoreNotTimeValid = 1,
		/// <summary>Ignore that the certificate trust list (CTL) is not valid, for reasons such as the CTL has expired, when determining certificate verification.</summary>
		// Token: 0x04001969 RID: 6505
		IgnoreCtlNotTimeValid = 2,
		/// <summary>Ignore that the CA (certificate authority) certificate and the issued certificate have validity periods that are not nested when verifying the certificate. For example, the CA cert can be valid from January 1 to December 1 and the issued certificate from January 2 to December 2, which would mean the validity periods are not nested.</summary>
		// Token: 0x0400196A RID: 6506
		IgnoreNotTimeNested = 4,
		/// <summary>Ignore that the basic constraints are not valid when determining certificate verification.</summary>
		// Token: 0x0400196B RID: 6507
		IgnoreInvalidBasicConstraints = 8,
		/// <summary>Ignore that the chain cannot be verified due to an unknown certificate authority (CA).</summary>
		// Token: 0x0400196C RID: 6508
		AllowUnknownCertificateAuthority = 16,
		/// <summary>Ignore that the certificate was not issued for the current use when determining certificate verification.</summary>
		// Token: 0x0400196D RID: 6509
		IgnoreWrongUsage = 32,
		/// <summary>Ignore that the certificate has an invalid name when determining certificate verification.</summary>
		// Token: 0x0400196E RID: 6510
		IgnoreInvalidName = 64,
		/// <summary>Ignore that the certificate has invalid policy when determining certificate verification.</summary>
		// Token: 0x0400196F RID: 6511
		IgnoreInvalidPolicy = 128,
		/// <summary>Ignore that the end certificate (the user certificate) revocation is unknown when determining certificate verification.</summary>
		// Token: 0x04001970 RID: 6512
		IgnoreEndRevocationUnknown = 256,
		/// <summary>Ignore that the certificate trust list (CTL) signer revocation is unknown when determining certificate verification.</summary>
		// Token: 0x04001971 RID: 6513
		IgnoreCtlSignerRevocationUnknown = 512,
		/// <summary>Ignore that the certificate authority revocation is unknown when determining certificate verification.</summary>
		// Token: 0x04001972 RID: 6514
		IgnoreCertificateAuthorityRevocationUnknown = 1024,
		/// <summary>Ignore that the root revocation is unknown when determining certificate verification.</summary>
		// Token: 0x04001973 RID: 6515
		IgnoreRootRevocationUnknown = 2048,
		/// <summary>All flags pertaining to verification are included.</summary>
		// Token: 0x04001974 RID: 6516
		AllFlags = 4095
	}
}

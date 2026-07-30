using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the status of an X509 chain.</summary>
	// Token: 0x0200039A RID: 922
	[Flags]
	public enum X509ChainStatusFlags
	{
		/// <summary>Specifies that the X509 chain has no errors.</summary>
		// Token: 0x04001919 RID: 6425
		NoError = 0,
		/// <summary>Specifies that the X509 chain is not valid due to an invalid time value, such as a value that indicates an expired certificate.</summary>
		// Token: 0x0400191A RID: 6426
		NotTimeValid = 1,
		/// <summary>Deprecated. Specifies that the CA (certificate authority) certificate and the issued certificate have validity periods that are not nested. For example, the CA cert can be valid from January 1 to December 1 and the issued certificate from January 2 to December 2, which would mean the validity periods are not nested.</summary>
		// Token: 0x0400191B RID: 6427
		NotTimeNested = 2,
		/// <summary>Specifies that the X509 chain is invalid due to a revoked certificate.</summary>
		// Token: 0x0400191C RID: 6428
		Revoked = 4,
		/// <summary>Specifies that the X509 chain is invalid due to an invalid certificate signature.</summary>
		// Token: 0x0400191D RID: 6429
		NotSignatureValid = 8,
		/// <summary>Specifies that the key usage is not valid.</summary>
		// Token: 0x0400191E RID: 6430
		NotValidForUsage = 16,
		/// <summary>Specifies that the X509 chain is invalid due to an untrusted root certificate.</summary>
		// Token: 0x0400191F RID: 6431
		UntrustedRoot = 32,
		/// <summary>Specifies that it is not possible to determine whether the certificate has been revoked. This can be due to the certificate revocation list (CRL) being offline or unavailable.</summary>
		// Token: 0x04001920 RID: 6432
		RevocationStatusUnknown = 64,
		/// <summary>Specifies that the X509 chain could not be built.</summary>
		// Token: 0x04001921 RID: 6433
		Cyclic = 128,
		/// <summary>Specifies that the X509 chain is invalid due to an invalid extension.</summary>
		// Token: 0x04001922 RID: 6434
		InvalidExtension = 256,
		/// <summary>Specifies that the X509 chain is invalid due to invalid policy constraints.</summary>
		// Token: 0x04001923 RID: 6435
		InvalidPolicyConstraints = 512,
		/// <summary>Specifies that the X509 chain is invalid due to invalid basic constraints.</summary>
		// Token: 0x04001924 RID: 6436
		InvalidBasicConstraints = 1024,
		/// <summary>Specifies that the X509 chain is invalid due to invalid name constraints.</summary>
		// Token: 0x04001925 RID: 6437
		InvalidNameConstraints = 2048,
		/// <summary>Specifies that the certificate does not have a supported name constraint or has a name constraint that is unsupported.</summary>
		// Token: 0x04001926 RID: 6438
		HasNotSupportedNameConstraint = 4096,
		/// <summary>Specifies that the certificate has an undefined name constraint.</summary>
		// Token: 0x04001927 RID: 6439
		HasNotDefinedNameConstraint = 8192,
		/// <summary>Specifies that the certificate has an impermissible name constraint.</summary>
		// Token: 0x04001928 RID: 6440
		HasNotPermittedNameConstraint = 16384,
		/// <summary>Specifies that the X509 chain is invalid because a certificate has excluded a name constraint.</summary>
		// Token: 0x04001929 RID: 6441
		HasExcludedNameConstraint = 32768,
		/// <summary>Specifies that the X509 chain could not be built up to the root certificate.</summary>
		// Token: 0x0400192A RID: 6442
		PartialChain = 65536,
		/// <summary>Specifies that the certificate trust list (CTL) is not valid because of an invalid time value, such as one that indicates that the CTL has expired.</summary>
		// Token: 0x0400192B RID: 6443
		CtlNotTimeValid = 131072,
		/// <summary>Specifies that the certificate trust list (CTL) contains an invalid signature.</summary>
		// Token: 0x0400192C RID: 6444
		CtlNotSignatureValid = 262144,
		/// <summary>Specifies that the certificate trust list (CTL) is not valid for this use.</summary>
		// Token: 0x0400192D RID: 6445
		CtlNotValidForUsage = 524288,
		/// <summary>Specifies that the online certificate revocation list (CRL) the X509 chain relies on is currently offline.</summary>
		// Token: 0x0400192E RID: 6446
		OfflineRevocation = 16777216,
		/// <summary>Specifies that there is no certificate policy extension in the certificate. This error would occur if a group policy has specified that all certificates must have a certificate policy.</summary>
		// Token: 0x0400192F RID: 6447
		NoIssuanceChainPolicy = 33554432,
		// Token: 0x04001930 RID: 6448
		ExplicitDistrust = 67108864,
		// Token: 0x04001931 RID: 6449
		HasNotSupportedCriticalExtension = 134217728,
		// Token: 0x04001932 RID: 6450
		HasWeakSignature = 1048576
	}
}

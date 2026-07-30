using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies most of the result codes for signature verification. </summary>
	// Token: 0x02000362 RID: 866
	public enum SignatureVerificationResult
	{
		/// <summary>The identity of the assembly specified in the /asm:assembly/asm:assemblyIdentity node of the manifest does not match the identity of the assembly in the Authenticode signature in the /asm:assembly/ds:signature/ds:KeyInfo/msrel:RelData/r:license/r:grant/as:ManifestInformation/as:assemblyIdentity node.</summary>
		// Token: 0x04000BA2 RID: 2978
		AssemblyIdentityMismatch = 1,
		/// <summary>The digital signature of the object did not verify.</summary>
		// Token: 0x04000BA3 RID: 2979
		BadDigest = -2146869232,
		/// <summary>The signature format is invalid.</summary>
		// Token: 0x04000BA4 RID: 2980
		BadSignatureFormat = -2146762749,
		/// <summary>The basic constraint extension of a certificate has not been observed.</summary>
		// Token: 0x04000BA5 RID: 2981
		BasicConstraintsNotObserved = -2146869223,
		/// <summary>The certificate has expired.</summary>
		// Token: 0x04000BA6 RID: 2982
		CertificateExpired = -2146762495,
		/// <summary>The certificate was explicitly marked as not trusted by the user.</summary>
		// Token: 0x04000BA7 RID: 2983
		CertificateExplicitlyDistrusted = -2146762479,
		/// <summary>The certificate is missing or has an empty value for an important field, such as a subject or issuer name.</summary>
		// Token: 0x04000BA8 RID: 2984
		CertificateMalformed = -2146762488,
		/// <summary>The certificate is not trusted explicitly.</summary>
		// Token: 0x04000BA9 RID: 2985
		CertificateNotExplicitlyTrusted = -2146762748,
		/// <summary>The certificate has been revoked.</summary>
		// Token: 0x04000BAA RID: 2986
		CertificateRevoked = -2146762484,
		/// <summary>The certificate cannot be used for signing and verification.</summary>
		// Token: 0x04000BAB RID: 2987
		CertificateUsageNotAllowed = -2146762490,
		/// <summary>The strong name signature does not verify in the <see cref="T:System.Security.Cryptography.X509Certificates.AuthenticodeSignatureInformation" /> object. Because the strong name signature wraps the Authenticode signature, someone could replace the Authenticode signature with a signature of their choosing. To prevent this, this error code is returned if the strong name does not verify because substituting a part of the strong name signature will invalidate it.</summary>
		// Token: 0x04000BAC RID: 2988
		ContainingSignatureInvalid = 2,
		/// <summary>The chain could not be built.</summary>
		// Token: 0x04000BAD RID: 2989
		CouldNotBuildChain = -2146762486,
		/// <summary>There is a general trust failure with the certificate.</summary>
		// Token: 0x04000BAE RID: 2990
		GenericTrustFailure,
		/// <summary>The certificate has an invalid name. The name is either not included in the permitted list or is explicitly excluded.</summary>
		// Token: 0x04000BAF RID: 2991
		InvalidCertificateName = -2146762476,
		/// <summary>The certificate has an invalid policy.</summary>
		// Token: 0x04000BB0 RID: 2992
		InvalidCertificatePolicy = -2146762477,
		/// <summary>The certificate has an invalid role.</summary>
		// Token: 0x04000BB1 RID: 2993
		InvalidCertificateRole = -2146762493,
		/// <summary>The signature of the certificate cannot be verified.</summary>
		// Token: 0x04000BB2 RID: 2994
		InvalidCertificateSignature = -2146869244,
		/// <summary>The certificate has an invalid usage.</summary>
		// Token: 0x04000BB3 RID: 2995
		InvalidCertificateUsage = -2146762480,
		/// <summary>One of the counter signatures is invalid.</summary>
		// Token: 0x04000BB4 RID: 2996
		InvalidCountersignature = -2146869245,
		/// <summary>The certificate for the signer of the message is invalid or not found.</summary>
		// Token: 0x04000BB5 RID: 2997
		InvalidSignerCertificate = -2146869246,
		/// <summary>A certificate was issued after the issuing certificate has expired.</summary>
		// Token: 0x04000BB6 RID: 2998
		InvalidTimePeriodNesting = -2146762494,
		/// <summary>The time stamp signature or certificate could not be verified or is malformed.</summary>
		// Token: 0x04000BB7 RID: 2999
		InvalidTimestamp = -2146869243,
		/// <summary>A parent of a given certificate did not issue that child certificate.</summary>
		// Token: 0x04000BB8 RID: 3000
		IssuerChainingError = -2146762489,
		/// <summary>The signature is missing.</summary>
		// Token: 0x04000BB9 RID: 3001
		MissingSignature = -2146762496,
		/// <summary>A path length constraint in the certification chain has been violated.</summary>
		// Token: 0x04000BBA RID: 3002
		PathLengthConstraintViolated = -2146762492,
		/// <summary>The public key token from the manifest identity in the /asm:assembly/asm:AssemblyIdentity node does not match the public key token of the key that is used to sign the manifest.</summary>
		// Token: 0x04000BBB RID: 3003
		PublicKeyTokenMismatch = 3,
		/// <summary>The publisher name from /asm:assembly/asmv2:publisherIdentity does not match the subject name of the signing certificate, or the issuer key hash from the same publisherIdentity node does not match the key hash of the signing certificate.</summary>
		// Token: 0x04000BBC RID: 3004
		PublisherMismatch,
		/// <summary>The revocation check failed.</summary>
		// Token: 0x04000BBD RID: 3005
		RevocationCheckFailure = -2146762482,
		/// <summary>A system-level error occurred while verifying trust.</summary>
		// Token: 0x04000BBE RID: 3006
		SystemError = -2146869247,
		/// <summary>A certificate contains an unknown extension that is marked critical.</summary>
		// Token: 0x04000BBF RID: 3007
		UnknownCriticalExtension = -2146762491,
		/// <summary>The certificate has an unknown trust provider.</summary>
		// Token: 0x04000BC0 RID: 3008
		UnknownTrustProvider = -2146762751,
		/// <summary>The certificate has an unknown verification action.</summary>
		// Token: 0x04000BC1 RID: 3009
		UnknownVerificationAction,
		/// <summary>The certification chain processed correctly, but one of the CA certificates is not trusted by the policy provider.</summary>
		// Token: 0x04000BC2 RID: 3010
		UntrustedCertificationAuthority = -2146762478,
		/// <summary>The root certificate is not trusted.</summary>
		// Token: 0x04000BC3 RID: 3011
		UntrustedRootCertificate = -2146762487,
		/// <summary>The test root certificate is not trusted.</summary>
		// Token: 0x04000BC4 RID: 3012
		UntrustedTestRootCertificate = -2146762483,
		/// <summary>The certificate verification result is valid.</summary>
		// Token: 0x04000BC5 RID: 3013
		Valid = 0
	}
}

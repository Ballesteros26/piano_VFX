using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides information about an Authenticode signature for a manifest. </summary>
	// Token: 0x02000360 RID: 864
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AuthenticodeSignatureInformation
	{
		// Token: 0x06001A31 RID: 6705 RVA: 0x0000220F File Offset: 0x0000040F
		internal AuthenticodeSignatureInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the description of the signing certificate.</summary>
		/// <returns>The description of the signing certificate.</returns>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Description
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the description URL of the signing certificate.</summary>
		/// <returns>The description URL of the signing certificate.</returns>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x000560B4 File Offset: 0x000542B4
		public Uri DescriptionUrl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the hash algorithm used to compute the signature.</summary>
		/// <returns>The hash algorithm used to compute the signature.</returns>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x000560B4 File Offset: 0x000542B4
		public string HashAlgorithm
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the HRESULT value from verifying the signature.</summary>
		/// <returns>The HRESULT value from verifying the signature.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00056148 File Offset: 0x00054348
		public int HResult
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the chain of certificates used to verify the Authenticode signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object that contains the certificate chain.</returns>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x000560B4 File Offset: 0x000542B4
		public X509Chain SignatureChain
		{
			[SecuritySafeCritical]
			[StorePermission(SecurityAction.Demand, OpenStore = true, EnumerateCertificates = true)]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the certificate that signed the manifest.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the certificate.</returns>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x000560B4 File Offset: 0x000542B4
		public X509Certificate2 SigningCertificate
		{
			[SecuritySafeCritical]
			[StorePermission(SecurityAction.Demand, OpenStore = true, EnumerateCertificates = true)]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the time stamp that was applied to the Authenticode signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.X509Certificates.TimestampInformation" /> object that contains the signature time stamp.</returns>
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x000560B4 File Offset: 0x000542B4
		public TimestampInformation Timestamp
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the trustworthiness of the Authenticode signature.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.X509Certificates.TrustStatus" /> values. </returns>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00056164 File Offset: 0x00054364
		public TrustStatus TrustStatus
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return TrustStatus.Untrusted;
			}
		}

		/// <summary>Gets the result of verifying the Authenticode signature.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.SignatureVerificationResult" /> values.</returns>
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00056180 File Offset: 0x00054380
		public SignatureVerificationResult VerificationResult
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return SignatureVerificationResult.Valid;
			}
		}
	}
}

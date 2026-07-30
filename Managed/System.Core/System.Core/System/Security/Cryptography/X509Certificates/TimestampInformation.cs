using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides details about the time stamp that was applied to an Authenticode signature for a manifest. </summary>
	// Token: 0x02000361 RID: 865
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class TimestampInformation
	{
		// Token: 0x06001A3B RID: 6715 RVA: 0x0000220F File Offset: 0x0000040F
		internal TimestampInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the hash algorithm used to compute the time stamp signature.</summary>
		/// <returns>The hash algorithm used to compute the time stamp signature.</returns>
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000560B4 File Offset: 0x000542B4
		public string HashAlgorithm
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the HRESULT value that results from verifying the signature.</summary>
		/// <returns>The HRESULT value that results from verifying the signature.</returns>
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0005619C File Offset: 0x0005439C
		public int HResult
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether the time stamp of the signature is valid.</summary>
		/// <returns>true if the time stamp is valid; otherwise, false. </returns>
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x000561B8 File Offset: 0x000543B8
		public bool IsValid
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the chain of certificates used to verify the time stamp of the signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object that represents the certificate chain.</returns>
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x000560B4 File Offset: 0x000542B4
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

		/// <summary>Gets the certificate that signed the time stamp.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the certificate.</returns>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x000560B4 File Offset: 0x000542B4
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

		/// <summary>Gets the time stamp that was applied to the signature.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the time stamp.</returns>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x000561D4 File Offset: 0x000543D4
		public DateTime Timestamp
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the result of verifying the time stamp signature.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.SignatureVerificationResult" /> values.</returns>
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x000561F0 File Offset: 0x000543F0
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

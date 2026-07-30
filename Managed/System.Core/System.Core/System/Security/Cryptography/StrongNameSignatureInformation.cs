using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Holds the strong name signature information for a manifest.</summary>
	// Token: 0x02000363 RID: 867
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class StrongNameSignatureInformation
	{
		// Token: 0x06001A43 RID: 6723 RVA: 0x0000220F File Offset: 0x0000040F
		internal StrongNameSignatureInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the hash algorithm that is used to calculate the strong name signature.</summary>
		/// <returns>The name of the hash algorithm that is used to calculate the strong name signature.</returns>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x000560B4 File Offset: 0x000542B4
		public string HashAlgorithm
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the HRESULT value of the result code.</summary>
		/// <returns>The HRESULT value of the result code.</returns>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x0005620C File Offset: 0x0005440C
		public int HResult
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether the strong name signature is valid.</summary>
		/// <returns>true if the strong name signature is valid; otherwise, false.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00056228 File Offset: 0x00054428
		public bool IsValid
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the public key that is used to verify the signature.</summary>
		/// <returns>The public key that is used to verify the signature. </returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x000560B4 File Offset: 0x000542B4
		public AsymmetricAlgorithm PublicKey
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the results of verifying the strong name signature.</summary>
		/// <returns>The result codes for signature verification.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00056244 File Offset: 0x00054444
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

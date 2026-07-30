using System;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an element of an X.509 chain.</summary>
	// Token: 0x020003B2 RID: 946
	public class X509ChainElement
	{
		// Token: 0x06001CCC RID: 7372 RVA: 0x0007217C File Offset: 0x0007037C
		internal X509ChainElement(X509Certificate2 certificate)
		{
			this.certificate = certificate;
			this.info = string.Empty;
		}

		/// <summary>Gets the X.509 certificate at a particular chain element.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</returns>
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x00072196 File Offset: 0x00070396
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		/// <summary>Gets the error status of the current X.509 certificate in a chain.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0007219E File Offset: 0x0007039E
		public X509ChainStatus[] ChainElementStatus
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Gets additional error information from an unmanaged certificate chain structure.</summary>
		/// <returns>A string representing the pwszExtendedErrorInfo member of the unmanaged CERT_CHAIN_ELEMENT structure in the Crypto API.</returns>
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x000721A6 File Offset: 0x000703A6
		public string Information
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x000721AE File Offset: 0x000703AE
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x000721B6 File Offset: 0x000703B6
		internal X509ChainStatusFlags StatusFlags
		{
			get
			{
				return this.compressed_status_flags;
			}
			set
			{
				this.compressed_status_flags = value;
			}
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000721C0 File Offset: 0x000703C0
		private int Count(X509ChainStatusFlags flags)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 1;
			while (num2++ < 32)
			{
				if ((flags & (X509ChainStatusFlags)num3) == (X509ChainStatusFlags)num3)
				{
					num++;
				}
				num3 <<= 1;
			}
			return num;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000721EF File Offset: 0x000703EF
		private void Set(X509ChainStatus[] status, ref int position, X509ChainStatusFlags flags, X509ChainStatusFlags mask)
		{
			if ((flags & mask) != X509ChainStatusFlags.NoError)
			{
				status[position].Status = mask;
				status[position].StatusInformation = X509ChainStatus.GetInformation(mask);
				position++;
			}
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00072220 File Offset: 0x00070420
		internal void UncompressFlags()
		{
			if (this.compressed_status_flags == X509ChainStatusFlags.NoError)
			{
				this.status = new X509ChainStatus[0];
				return;
			}
			int num = this.Count(this.compressed_status_flags);
			this.status = new X509ChainStatus[num];
			int num2 = 0;
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.UntrustedRoot);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeNested);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Revoked);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotSignatureValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotValidForUsage);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.RevocationStatusUnknown);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Cyclic);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidExtension);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidPolicyConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidBasicConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidNameConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotSupportedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotDefinedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotPermittedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasExcludedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.PartialChain);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotTimeValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotSignatureValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotValidForUsage);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.OfflineRevocation);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NoIssuanceChainPolicy);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal X509ChainElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040019A3 RID: 6563
		private X509Certificate2 certificate;

		// Token: 0x040019A4 RID: 6564
		private X509ChainStatus[] status;

		// Token: 0x040019A5 RID: 6565
		private string info;

		// Token: 0x040019A6 RID: 6566
		private X509ChainStatusFlags compressed_status_flags;
	}
}

using System;
using System.Text;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020006B0 RID: 1712
	internal sealed class X509CertificateImplMono : X509CertificateImpl
	{
		// Token: 0x06004937 RID: 18743 RVA: 0x00107287 File Offset: 0x00105487
		public X509CertificateImplMono(X509Certificate x509)
		{
			this.x509 = x509;
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004938 RID: 18744 RVA: 0x00107296 File Offset: 0x00105496
		public override bool IsValid
		{
			get
			{
				return this.x509 != null;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x00101C6E File Offset: 0x000FFE6E
		public override IntPtr Handle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x00101C6E File Offset: 0x000FFE6E
		public override IntPtr GetNativeAppleCertificate()
		{
			return IntPtr.Zero;
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x001072A1 File Offset: 0x001054A1
		public override X509CertificateImpl Clone()
		{
			base.ThrowIfContextInvalid();
			return new X509CertificateImplMono(this.x509);
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x001072B4 File Offset: 0x001054B4
		public override string GetIssuerName(bool legacyV1Mode)
		{
			base.ThrowIfContextInvalid();
			if (legacyV1Mode)
			{
				return this.x509.IssuerName;
			}
			return X501.ToString(this.x509.GetIssuerName(), true, ", ", true);
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x001072E2 File Offset: 0x001054E2
		public override string GetSubjectName(bool legacyV1Mode)
		{
			base.ThrowIfContextInvalid();
			if (legacyV1Mode)
			{
				return this.x509.SubjectName;
			}
			return X501.ToString(this.x509.GetSubjectName(), true, ", ", true);
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x00107310 File Offset: 0x00105510
		public override byte[] GetRawCertData()
		{
			base.ThrowIfContextInvalid();
			return this.x509.RawData;
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x00107323 File Offset: 0x00105523
		protected override byte[] GetCertHash(bool lazy)
		{
			base.ThrowIfContextInvalid();
			return SHA1.Create().ComputeHash(this.x509.RawData);
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x00107340 File Offset: 0x00105540
		public override DateTime GetValidFrom()
		{
			base.ThrowIfContextInvalid();
			return this.x509.ValidFrom;
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x00107353 File Offset: 0x00105553
		public override DateTime GetValidUntil()
		{
			base.ThrowIfContextInvalid();
			return this.x509.ValidUntil;
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x00107366 File Offset: 0x00105566
		public override bool Equals(X509CertificateImpl other, out bool result)
		{
			result = false;
			return false;
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x0010736C File Offset: 0x0010556C
		public override string GetKeyAlgorithm()
		{
			base.ThrowIfContextInvalid();
			return this.x509.KeyAlgorithm;
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x0010737F File Offset: 0x0010557F
		public override byte[] GetKeyAlgorithmParameters()
		{
			base.ThrowIfContextInvalid();
			return this.x509.KeyAlgorithmParameters;
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x00107392 File Offset: 0x00105592
		public override byte[] GetPublicKey()
		{
			base.ThrowIfContextInvalid();
			return this.x509.PublicKey;
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x001073A5 File Offset: 0x001055A5
		public override byte[] GetSerialNumber()
		{
			base.ThrowIfContextInvalid();
			return this.x509.SerialNumber;
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x001073B8 File Offset: 0x001055B8
		public override byte[] Export(X509ContentType contentType, byte[] password)
		{
			base.ThrowIfContextInvalid();
			switch (contentType)
			{
			case X509ContentType.Cert:
				return this.GetRawCertData();
			case X509ContentType.SerializedCert:
				throw new NotSupportedException();
			case X509ContentType.Pfx:
				throw new NotSupportedException();
			default:
				throw new CryptographicException(Locale.GetText("This certificate format '{0}' cannot be exported.", new object[] { contentType }));
			}
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x00107414 File Offset: 0x00105614
		public override string ToString(bool full)
		{
			base.ThrowIfContextInvalid();
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[Subject]{0}  {1}{0}{0}", newLine, this.GetSubjectName(false));
			stringBuilder.AppendFormat("[Issuer]{0}  {1}{0}{0}", newLine, this.GetIssuerName(false));
			stringBuilder.AppendFormat("[Not Before]{0}  {1}{0}{0}", newLine, this.GetValidFrom().ToLocalTime());
			stringBuilder.AppendFormat("[Not After]{0}  {1}{0}{0}", newLine, this.GetValidUntil().ToLocalTime());
			stringBuilder.AppendFormat("[Thumbprint]{0}  {1}{0}", newLine, X509Helper.ToHexString(base.GetCertHash()));
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x001074BF File Offset: 0x001056BF
		protected override void Dispose(bool disposing)
		{
			this.x509 = null;
		}

		// Token: 0x04002669 RID: 9833
		private X509Certificate x509;
	}
}

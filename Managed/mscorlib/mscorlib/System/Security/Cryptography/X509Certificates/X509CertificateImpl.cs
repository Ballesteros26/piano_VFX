using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020006AE RID: 1710
	internal abstract class X509CertificateImpl : IDisposable
	{
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004906 RID: 18694
		public abstract bool IsValid { get; }

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004907 RID: 18695
		public abstract IntPtr Handle { get; }

		// Token: 0x06004908 RID: 18696
		public abstract IntPtr GetNativeAppleCertificate();

		// Token: 0x06004909 RID: 18697 RVA: 0x00106DD5 File Offset: 0x00104FD5
		protected void ThrowIfContextInvalid()
		{
			if (!this.IsValid)
			{
				throw X509Helper.GetInvalidContextException();
			}
		}

		// Token: 0x0600490A RID: 18698
		public abstract X509CertificateImpl Clone();

		// Token: 0x0600490B RID: 18699
		public abstract string GetIssuerName(bool legacyV1Mode);

		// Token: 0x0600490C RID: 18700
		public abstract string GetSubjectName(bool legacyV1Mode);

		// Token: 0x0600490D RID: 18701
		public abstract byte[] GetRawCertData();

		// Token: 0x0600490E RID: 18702
		public abstract DateTime GetValidFrom();

		// Token: 0x0600490F RID: 18703
		public abstract DateTime GetValidUntil();

		// Token: 0x06004910 RID: 18704 RVA: 0x00106DE5 File Offset: 0x00104FE5
		public byte[] GetCertHash()
		{
			this.ThrowIfContextInvalid();
			if (this.cachedCertificateHash == null)
			{
				this.cachedCertificateHash = this.GetCertHash(false);
			}
			return this.cachedCertificateHash;
		}

		// Token: 0x06004911 RID: 18705
		protected abstract byte[] GetCertHash(bool lazy);

		// Token: 0x06004912 RID: 18706 RVA: 0x00106E08 File Offset: 0x00105008
		public override int GetHashCode()
		{
			if (!this.IsValid)
			{
				return 0;
			}
			if (this.cachedCertificateHash == null)
			{
				this.cachedCertificateHash = this.GetCertHash(true);
			}
			if (this.cachedCertificateHash != null && this.cachedCertificateHash.Length >= 4)
			{
				return ((int)this.cachedCertificateHash[0] << 24) | ((int)this.cachedCertificateHash[1] << 16) | ((int)this.cachedCertificateHash[2] << 8) | (int)this.cachedCertificateHash[3];
			}
			return 0;
		}

		// Token: 0x06004913 RID: 18707
		public abstract bool Equals(X509CertificateImpl other, out bool result);

		// Token: 0x06004914 RID: 18708
		public abstract string GetKeyAlgorithm();

		// Token: 0x06004915 RID: 18709
		public abstract byte[] GetKeyAlgorithmParameters();

		// Token: 0x06004916 RID: 18710
		public abstract byte[] GetPublicKey();

		// Token: 0x06004917 RID: 18711
		public abstract byte[] GetSerialNumber();

		// Token: 0x06004918 RID: 18712
		public abstract byte[] Export(X509ContentType contentType, byte[] password);

		// Token: 0x06004919 RID: 18713
		public abstract string ToString(bool full);

		// Token: 0x0600491A RID: 18714 RVA: 0x00106E74 File Offset: 0x00105074
		public override bool Equals(object obj)
		{
			X509CertificateImpl x509CertificateImpl = obj as X509CertificateImpl;
			if (x509CertificateImpl == null)
			{
				return false;
			}
			if (!this.IsValid || !x509CertificateImpl.IsValid)
			{
				return false;
			}
			bool flag;
			if (this.Equals(x509CertificateImpl, out flag))
			{
				return flag;
			}
			byte[] rawCertData = this.GetRawCertData();
			byte[] rawCertData2 = x509CertificateImpl.GetRawCertData();
			if (rawCertData == null)
			{
				return rawCertData2 == null;
			}
			if (rawCertData2 == null)
			{
				return false;
			}
			if (rawCertData.Length != rawCertData2.Length)
			{
				return false;
			}
			for (int i = 0; i < rawCertData.Length; i++)
			{
				if (rawCertData[i] != rawCertData2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x00106EF0 File Offset: 0x001050F0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x00106EFF File Offset: 0x001050FF
		protected virtual void Dispose(bool disposing)
		{
			this.cachedCertificateHash = null;
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x00106F08 File Offset: 0x00105108
		~X509CertificateImpl()
		{
			this.Dispose(false);
		}

		// Token: 0x04002666 RID: 9830
		private byte[] cachedCertificateHash;
	}
}

using System;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Security.X509;
using XamMac.CoreFoundation;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020006AF RID: 1711
	internal class X509CertificateImplApple : X509CertificateImpl
	{
		// Token: 0x0600491F RID: 18719 RVA: 0x00106F38 File Offset: 0x00105138
		public X509CertificateImplApple(IntPtr handle, bool owns)
		{
			this.handle = handle;
			if (!owns)
			{
				CFHelpers.CFRetain(handle);
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x00106F51 File Offset: 0x00105151
		public override bool IsValid
		{
			get
			{
				return this.handle != IntPtr.Zero;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004921 RID: 18721 RVA: 0x00106F63 File Offset: 0x00105163
		public override IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x00106F6B File Offset: 0x0010516B
		public override IntPtr GetNativeAppleCertificate()
		{
			base.ThrowIfContextInvalid();
			return this.handle;
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x00106F79 File Offset: 0x00105179
		public override X509CertificateImpl Clone()
		{
			base.ThrowIfContextInvalid();
			return new X509CertificateImplApple(this.handle, false);
		}

		// Token: 0x06004924 RID: 18724
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecCertificateCopySubjectSummary(IntPtr cert);

		// Token: 0x06004925 RID: 18725
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecCertificateCopyData(IntPtr cert);

		// Token: 0x06004926 RID: 18726 RVA: 0x00106F90 File Offset: 0x00105190
		public override byte[] GetRawCertData()
		{
			base.ThrowIfContextInvalid();
			IntPtr intPtr = X509CertificateImplApple.SecCertificateCopyData(this.handle);
			if (intPtr == IntPtr.Zero)
			{
				throw new ArgumentException("Not a valid certificate");
			}
			byte[] array;
			try
			{
				array = CFHelpers.FetchDataBuffer(intPtr);
			}
			finally
			{
				CFHelpers.CFRelease(intPtr);
			}
			return array;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x00106FEC File Offset: 0x001051EC
		public string GetSubjectSummary()
		{
			base.ThrowIfContextInvalid();
			IntPtr intPtr = X509CertificateImplApple.SecCertificateCopySubjectSummary(this.handle);
			string text = CFHelpers.FetchString(intPtr);
			CFHelpers.CFRelease(intPtr);
			return text;
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x00107017 File Offset: 0x00105217
		protected override byte[] GetCertHash(bool lazy)
		{
			base.ThrowIfContextInvalid();
			return SHA1.Create().ComputeHash(this.GetRawCertData());
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x00107030 File Offset: 0x00105230
		public override bool Equals(X509CertificateImpl other, out bool result)
		{
			X509CertificateImplApple x509CertificateImplApple = other as X509CertificateImplApple;
			if (x509CertificateImplApple != null && x509CertificateImplApple.handle == this.handle)
			{
				result = true;
				return true;
			}
			result = false;
			return false;
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x00107064 File Offset: 0x00105264
		private void MustFallback()
		{
			base.ThrowIfContextInvalid();
			if (this.fallback != null)
			{
				return;
			}
			X509Certificate x509Certificate = new X509Certificate(this.GetRawCertData());
			this.fallback = new X509CertificateImplMono(x509Certificate);
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x00107098 File Offset: 0x00105298
		public X509CertificateImpl FallbackImpl
		{
			get
			{
				this.MustFallback();
				return this.fallback;
			}
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x001070A6 File Offset: 0x001052A6
		public override string GetSubjectName(bool legacyV1Mode)
		{
			return this.FallbackImpl.GetSubjectName(legacyV1Mode);
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x001070B4 File Offset: 0x001052B4
		public override string GetIssuerName(bool legacyV1Mode)
		{
			return this.FallbackImpl.GetIssuerName(legacyV1Mode);
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x001070C2 File Offset: 0x001052C2
		public override DateTime GetValidFrom()
		{
			return this.FallbackImpl.GetValidFrom();
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x001070CF File Offset: 0x001052CF
		public override DateTime GetValidUntil()
		{
			return this.FallbackImpl.GetValidUntil();
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x001070DC File Offset: 0x001052DC
		public override string GetKeyAlgorithm()
		{
			return this.FallbackImpl.GetKeyAlgorithm();
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x001070E9 File Offset: 0x001052E9
		public override byte[] GetKeyAlgorithmParameters()
		{
			return this.FallbackImpl.GetKeyAlgorithmParameters();
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x001070F6 File Offset: 0x001052F6
		public override byte[] GetPublicKey()
		{
			return this.FallbackImpl.GetPublicKey();
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x00107103 File Offset: 0x00105303
		public override byte[] GetSerialNumber()
		{
			return this.FallbackImpl.GetSerialNumber();
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x00107110 File Offset: 0x00105310
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

		// Token: 0x06004935 RID: 18741 RVA: 0x0010716C File Offset: 0x0010536C
		public override string ToString(bool full)
		{
			base.ThrowIfContextInvalid();
			if (!full || this.fallback == null)
			{
				string subjectSummary = this.GetSubjectSummary();
				return string.Format("[X509Certificate: {0}]", subjectSummary);
			}
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

		// Token: 0x06004936 RID: 18742 RVA: 0x00107238 File Offset: 0x00105438
		protected override void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFHelpers.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
			if (this.fallback != null)
			{
				this.fallback.Dispose();
				this.fallback = null;
			}
		}

		// Token: 0x04002667 RID: 9831
		private IntPtr handle;

		// Token: 0x04002668 RID: 9832
		private X509CertificateImpl fallback;
	}
}

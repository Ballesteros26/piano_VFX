using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Mono.Security.X509;
using XamMac.CoreFoundation;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020006B1 RID: 1713
	internal static class X509Helper
	{
		// Token: 0x0600494A RID: 18762 RVA: 0x001074C8 File Offset: 0x001056C8
		public static X509CertificateImpl InitFromHandleApple(IntPtr handle)
		{
			return new X509CertificateImplApple(handle, false);
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x001074D4 File Offset: 0x001056D4
		private static X509CertificateImpl ImportApple(byte[] rawData)
		{
			IntPtr intPtr = CFHelpers.CreateCertificateFromData(rawData);
			if (intPtr != IntPtr.Zero)
			{
				return new X509CertificateImplApple(intPtr, true);
			}
			X509Certificate x509Certificate;
			try
			{
				x509Certificate = new X509Certificate(rawData);
			}
			catch (Exception ex)
			{
				try
				{
					x509Certificate = X509Helper.ImportPkcs12(rawData, null);
				}
				catch
				{
					throw new CryptographicException(Locale.GetText("Unable to decode certificate."), ex);
				}
			}
			return new X509CertificateImplMono(x509Certificate);
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x00107548 File Offset: 0x00105748
		internal static void InstallNativeHelper(INativeCertificateHelper helper)
		{
			if (X509Helper.nativeHelper == null)
			{
				Interlocked.CompareExchange<INativeCertificateHelper>(ref X509Helper.nativeHelper, helper, null);
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600494D RID: 18765 RVA: 0x00107560 File Offset: 0x00105760
		private static bool ShouldUseAppleTls
		{
			get
			{
				if (!Environment.IsMacOS)
				{
					return false;
				}
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_TLS_PROVIDER");
				return string.IsNullOrEmpty(environmentVariable) || environmentVariable == "default" || environmentVariable == "apple";
			}
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x001075A3 File Offset: 0x001057A3
		public static X509CertificateImpl InitFromHandle(IntPtr handle)
		{
			if (X509Helper.ShouldUseAppleTls)
			{
				return X509Helper.InitFromHandleApple(handle);
			}
			return X509Helper.InitFromHandleCore(handle);
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x001075B9 File Offset: 0x001057B9
		private static X509CertificateImpl Import(byte[] rawData)
		{
			if (X509Helper.ShouldUseAppleTls)
			{
				return X509Helper.ImportApple(rawData);
			}
			return X509Helper.ImportCore(rawData);
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x001075D0 File Offset: 0x001057D0
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static X509CertificateImpl InitFromHandleCore(IntPtr handle)
		{
			X509Helper.CertificateContext certificateContext = (X509Helper.CertificateContext)Marshal.PtrToStructure(handle, typeof(X509Helper.CertificateContext));
			byte[] array = new byte[certificateContext.cbCertEncoded];
			Marshal.Copy(certificateContext.pbCertEncoded, array, 0, (int)certificateContext.cbCertEncoded);
			return new X509CertificateImplMono(new X509Certificate(array));
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x0010761D File Offset: 0x0010581D
		public static X509CertificateImpl InitFromCertificate(X509Certificate cert)
		{
			if (X509Helper.nativeHelper != null)
			{
				return X509Helper.nativeHelper.Import(cert);
			}
			return X509Helper.InitFromCertificate(cert.Impl);
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x00107640 File Offset: 0x00105840
		public static X509CertificateImpl InitFromCertificate(X509CertificateImpl impl)
		{
			X509Helper.ThrowIfContextInvalid(impl);
			X509CertificateImpl x509CertificateImpl = impl.Clone();
			if (x509CertificateImpl != null)
			{
				return x509CertificateImpl;
			}
			byte[] rawCertData = impl.GetRawCertData();
			if (rawCertData == null)
			{
				return null;
			}
			return new X509CertificateImplMono(new X509Certificate(rawCertData));
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x00107676 File Offset: 0x00105876
		public static bool IsValid(X509CertificateImpl impl)
		{
			return impl != null && impl.IsValid;
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x00107683 File Offset: 0x00105883
		internal static void ThrowIfContextInvalid(X509CertificateImpl impl)
		{
			if (!X509Helper.IsValid(impl))
			{
				throw X509Helper.GetInvalidContextException();
			}
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x00107693 File Offset: 0x00105893
		internal static Exception GetInvalidContextException()
		{
			return new CryptographicException(Locale.GetText("Certificate instance is empty."));
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x001076A4 File Offset: 0x001058A4
		internal static X509Certificate ImportPkcs12(byte[] rawData, string password)
		{
			PKCS12 pkcs = ((password == null) ? new PKCS12(rawData) : new PKCS12(rawData, password));
			if (pkcs.Certificates.Count == 0)
			{
				return null;
			}
			if (pkcs.Keys.Count == 0)
			{
				return pkcs.Certificates[0];
			}
			string text = (pkcs.Keys[0] as AsymmetricAlgorithm).ToXmlString(false);
			foreach (X509Certificate x509Certificate in pkcs.Certificates)
			{
				if (x509Certificate.RSA != null && text == x509Certificate.RSA.ToXmlString(false))
				{
					return x509Certificate;
				}
				if (x509Certificate.DSA != null && text == x509Certificate.DSA.ToXmlString(false))
				{
					return x509Certificate;
				}
			}
			return pkcs.Certificates[0];
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x0010779C File Offset: 0x0010599C
		private static byte[] PEM(string type, byte[] data)
		{
			string @string = Encoding.ASCII.GetString(data);
			string text = string.Format("-----BEGIN {0}-----", type);
			string text2 = string.Format("-----END {0}-----", type);
			int num = @string.IndexOf(text) + text.Length;
			int num2 = @string.IndexOf(text2, num);
			return Convert.FromBase64String(@string.Substring(num, num2 - num));
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x001077F4 File Offset: 0x001059F4
		private static byte[] ConvertData(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return data;
			}
			if (data[0] != 48)
			{
				try
				{
					return X509Helper.PEM("CERTIFICATE", data);
				}
				catch
				{
				}
				return data;
			}
			return data;
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x00107838 File Offset: 0x00105A38
		private static X509CertificateImpl ImportCore(byte[] rawData)
		{
			X509Certificate x509Certificate;
			try
			{
				x509Certificate = new X509Certificate(rawData);
			}
			catch (Exception ex)
			{
				try
				{
					x509Certificate = X509Helper.ImportPkcs12(rawData, null);
				}
				catch
				{
					throw new CryptographicException(Locale.GetText("Unable to decode certificate."), ex);
				}
			}
			return new X509CertificateImplMono(x509Certificate);
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x00107890 File Offset: 0x00105A90
		public static X509CertificateImpl Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			if (password == null)
			{
				rawData = X509Helper.ConvertData(rawData);
				return X509Helper.Import(rawData);
			}
			X509Certificate x509Certificate;
			try
			{
				x509Certificate = X509Helper.ImportPkcs12(rawData, password);
			}
			catch
			{
				x509Certificate = new X509Certificate(rawData);
			}
			return new X509CertificateImplMono(x509Certificate);
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x001078DC File Offset: 0x00105ADC
		public static byte[] Export(X509CertificateImpl impl, X509ContentType contentType, byte[] password)
		{
			X509Helper.ThrowIfContextInvalid(impl);
			return impl.Export(contentType, password);
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x001078EC File Offset: 0x00105AEC
		public static bool Equals(X509CertificateImpl first, X509CertificateImpl second)
		{
			if (!X509Helper.IsValid(first) || !X509Helper.IsValid(second))
			{
				return false;
			}
			bool flag;
			if (first.Equals(second, out flag))
			{
				return flag;
			}
			byte[] rawCertData = first.GetRawCertData();
			byte[] rawCertData2 = second.GetRawCertData();
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

		// Token: 0x0600495D RID: 18781 RVA: 0x00107958 File Offset: 0x00105B58
		public static string ToHexString(byte[] data)
		{
			if (data != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					stringBuilder.Append(data[i].ToString("X2"));
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0400266A RID: 9834
		private static INativeCertificateHelper nativeHelper;

		// Token: 0x020006B2 RID: 1714
		internal struct CertificateContext
		{
			// Token: 0x0400266B RID: 9835
			public uint dwCertEncodingType;

			// Token: 0x0400266C RID: 9836
			public IntPtr pbCertEncoded;

			// Token: 0x0400266D RID: 9837
			public uint cbCertEncoded;

			// Token: 0x0400266E RID: 9838
			public IntPtr pCertInfo;

			// Token: 0x0400266F RID: 9839
			public IntPtr hCertStore;
		}
	}
}

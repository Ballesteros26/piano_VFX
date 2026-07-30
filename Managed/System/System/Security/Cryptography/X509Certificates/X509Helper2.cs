using System;
using System.IO;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003BD RID: 957
	internal static class X509Helper2
	{
		// Token: 0x06001D58 RID: 7512 RVA: 0x0007430C File Offset: 0x0007250C
		internal static long GetSubjectNameHash(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate)
		{
			return X509Helper2.GetSubjectNameHash(certificate.Impl);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0007431C File Offset: 0x0007251C
		internal static long GetSubjectNameHash(X509CertificateImpl impl)
		{
			long subjectNameHash;
			using (global::System.Security.Cryptography.X509Certificates.X509Certificate nativeInstance = X509Helper2.GetNativeInstance(impl))
			{
				subjectNameHash = X509Helper2.GetSubjectNameHash(nativeInstance);
			}
			return subjectNameHash;
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00074354 File Offset: 0x00072554
		internal static void ExportAsPEM(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, Stream stream, bool includeHumanReadableForm)
		{
			X509Helper2.ExportAsPEM(certificate.Impl, stream, includeHumanReadableForm);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00074364 File Offset: 0x00072564
		internal static void ExportAsPEM(X509CertificateImpl impl, Stream stream, bool includeHumanReadableForm)
		{
			using (global::System.Security.Cryptography.X509Certificates.X509Certificate nativeInstance = X509Helper2.GetNativeInstance(impl))
			{
				X509Helper2.ExportAsPEM(nativeInstance, stream, includeHumanReadableForm);
			}
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0007439C File Offset: 0x0007259C
		internal static void Initialize()
		{
			X509Helper.InstallNativeHelper(new X509Helper2.MyNativeHelper());
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x000743A8 File Offset: 0x000725A8
		internal static void ThrowIfContextInvalid(X509CertificateImpl impl)
		{
			X509Helper.ThrowIfContextInvalid(impl);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0000F3CE File Offset: 0x0000D5CE
		private static global::System.Security.Cryptography.X509Certificates.X509Certificate GetNativeInstance(X509CertificateImpl impl)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000743B0 File Offset: 0x000725B0
		internal static X509Certificate2Impl Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags, bool disableProvider = false)
		{
			X509Certificate2ImplMono x509Certificate2ImplMono = new X509Certificate2ImplMono();
			x509Certificate2ImplMono.Import(rawData, password, keyStorageFlags);
			return x509Certificate2ImplMono;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x000743C0 File Offset: 0x000725C0
		internal static X509Certificate2Impl Import(global::System.Security.Cryptography.X509Certificates.X509Certificate cert, bool disableProvider = false)
		{
			X509Certificate2Impl x509Certificate2Impl = cert.Impl as X509Certificate2Impl;
			if (x509Certificate2Impl != null)
			{
				return (X509Certificate2Impl)x509Certificate2Impl.Clone();
			}
			return X509Helper2.Import(cert.GetRawCertData(), null, X509KeyStorageFlags.DefaultKeySet, false);
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000743F8 File Offset: 0x000725F8
		[MonoTODO("Investigate replacement; see comments in source.")]
		internal static Mono.Security.X509.X509Certificate GetMonoCertificate(X509Certificate2 certificate)
		{
			X509Certificate2Impl x509Certificate2Impl = certificate.Impl;
			if (x509Certificate2Impl == null)
			{
				x509Certificate2Impl = X509Helper2.Import(certificate, true);
			}
			X509Certificate2ImplMono x509Certificate2ImplMono = x509Certificate2Impl.FallbackImpl as X509Certificate2ImplMono;
			if (x509Certificate2ImplMono == null)
			{
				throw new NotSupportedException();
			}
			return x509Certificate2ImplMono.MonoCertificate;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00074430 File Offset: 0x00072630
		internal static X509ChainImpl CreateChainImpl(bool useMachineContext)
		{
			return new X509ChainImplMono(useMachineContext);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00074438 File Offset: 0x00072638
		public static bool IsValid(X509ChainImpl impl)
		{
			return impl != null && impl.IsValid;
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x00074445 File Offset: 0x00072645
		internal static void ThrowIfContextInvalid(X509ChainImpl impl)
		{
			if (!X509Helper2.IsValid(impl))
			{
				throw X509Helper2.GetInvalidChainContextException();
			}
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x00074455 File Offset: 0x00072655
		internal static Exception GetInvalidChainContextException()
		{
			return new CryptographicException(global::Locale.GetText("Chain instance is empty."));
		}

		// Token: 0x020003BE RID: 958
		private class MyNativeHelper : INativeCertificateHelper
		{
			// Token: 0x06001D66 RID: 7526 RVA: 0x00074466 File Offset: 0x00072666
			public X509CertificateImpl Import(byte[] data, string password, X509KeyStorageFlags flags)
			{
				return X509Helper2.Import(data, password, flags, false);
			}

			// Token: 0x06001D67 RID: 7527 RVA: 0x00074471 File Offset: 0x00072671
			public X509CertificateImpl Import(global::System.Security.Cryptography.X509Certificates.X509Certificate cert)
			{
				return X509Helper2.Import(cert, false);
			}
		}
	}
}

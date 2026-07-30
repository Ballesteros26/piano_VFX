using System;
using System.IO;
using Mono.Net.Security;

namespace Mono.Security.Interface
{
	// Token: 0x0200007B RID: 123
	public static class CertificateValidationHelper
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x00017028 File Offset: 0x00015228
		static CertificateValidationHelper()
		{
			if (File.Exists("/System/Library/Frameworks/Security.framework/Security"))
			{
				CertificateValidationHelper.noX509Chain = true;
				CertificateValidationHelper.supportsTrustAnchors = true;
				return;
			}
			CertificateValidationHelper.noX509Chain = false;
			CertificateValidationHelper.supportsTrustAnchors = false;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001704F File Offset: 0x0001524F
		public static bool SupportsX509Chain
		{
			get
			{
				return !CertificateValidationHelper.noX509Chain;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00017059 File Offset: 0x00015259
		public static bool SupportsTrustAnchors
		{
			get
			{
				return CertificateValidationHelper.supportsTrustAnchors;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00017060 File Offset: 0x00015260
		internal static ICertificateValidator2 GetInternalValidator(MonoTlsSettings settings, MonoTlsProvider provider)
		{
			return (ICertificateValidator2)NoReflectionHelper.GetInternalValidator(provider, settings);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001706E File Offset: 0x0001526E
		[Obsolete("Use GetInternalValidator")]
		internal static ICertificateValidator2 GetDefaultValidator(MonoTlsSettings settings, MonoTlsProvider provider)
		{
			return CertificateValidationHelper.GetInternalValidator(settings, provider);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00017077 File Offset: 0x00015277
		public static ICertificateValidator GetValidator(MonoTlsSettings settings)
		{
			return (ICertificateValidator)NoReflectionHelper.GetDefaultValidator(settings);
		}

		// Token: 0x0400023F RID: 575
		private const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";

		// Token: 0x04000240 RID: 576
		private static readonly bool noX509Chain;

		// Token: 0x04000241 RID: 577
		private static readonly bool supportsTrustAnchors;
	}
}

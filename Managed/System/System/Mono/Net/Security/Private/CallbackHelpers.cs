using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security.Private
{
	// Token: 0x0200007F RID: 127
	internal static class CallbackHelpers
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00009494 File Offset: 0x00007694
		internal static MonoRemoteCertificateValidationCallback PublicToMono(RemoteCertificateValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, MonoSslPolicyErrors e) => callback(h, c, ch, (SslPolicyErrors)e);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000094C4 File Offset: 0x000076C4
		internal static MonoLocalCertificateSelectionCallback PublicToMono(LocalCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(null, t, lc, rc, ai);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000094F4 File Offset: 0x000076F4
		internal static MonoRemoteCertificateValidationCallback InternalToMono(RemoteCertValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, MonoSslPolicyErrors e) => callback(h, c, ch, (SslPolicyErrors)e);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00009524 File Offset: 0x00007724
		internal static RemoteCertificateValidationCallback InternalToPublic(string hostname, RemoteCertValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, X509Certificate c, X509Chain ch, SslPolicyErrors e) => callback(hostname, c, ch, e);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000955C File Offset: 0x0000775C
		internal static MonoLocalCertificateSelectionCallback InternalToMono(LocalCertSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000958C File Offset: 0x0000778C
		internal static RemoteCertificateValidationCallback MonoToPublic(MonoRemoteCertificateValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object t, X509Certificate c, X509Chain ch, SslPolicyErrors e) => callback(null, c, ch, (MonoSslPolicyErrors)e);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000095BC File Offset: 0x000077BC
		internal static LocalCertificateSelectionCallback MonoToPublic(MonoLocalCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000095EC File Offset: 0x000077EC
		internal static RemoteCertValidationCallback MonoToInternal(MonoRemoteCertificateValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, SslPolicyErrors e) => callback(h, c, ch, (MonoSslPolicyErrors)e);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000961C File Offset: 0x0000781C
		internal static LocalCertSelectionCallback MonoToInternal(MonoLocalCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}
	}
}

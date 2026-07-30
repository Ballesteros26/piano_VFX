using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security;

namespace Mono.Security.Interface
{
	// Token: 0x02000087 RID: 135
	public static class MonoTlsProviderFactory
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0001712D File Offset: 0x0001532D
		public static MonoTlsProvider GetProvider()
		{
			return (MonoTlsProvider)NoReflectionHelper.GetProvider();
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00017139 File Offset: 0x00015339
		public static bool IsInitialized
		{
			get
			{
				return NoReflectionHelper.IsInitialized;
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00017140 File Offset: 0x00015340
		public static void Initialize()
		{
			NoReflectionHelper.Initialize();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00017147 File Offset: 0x00015347
		public static void Initialize(string provider)
		{
			NoReflectionHelper.Initialize(provider);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001714F File Offset: 0x0001534F
		public static bool IsProviderSupported(string provider)
		{
			return NoReflectionHelper.IsProviderSupported(provider);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00017157 File Offset: 0x00015357
		public static MonoTlsProvider GetProvider(string provider)
		{
			return (MonoTlsProvider)NoReflectionHelper.GetProvider(provider);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00017164 File Offset: 0x00015364
		public static HttpWebRequest CreateHttpsRequest(Uri requestUri, MonoTlsProvider provider, MonoTlsSettings settings = null)
		{
			return NoReflectionHelper.CreateHttpsRequest(requestUri, provider, settings);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001716E File Offset: 0x0001536E
		public static HttpListener CreateHttpListener(X509Certificate certificate, MonoTlsProvider provider = null, MonoTlsSettings settings = null)
		{
			return (HttpListener)NoReflectionHelper.CreateHttpListener(certificate, provider, settings);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001717D File Offset: 0x0001537D
		public static IMonoSslStream GetMonoSslStream(SslStream stream)
		{
			return (IMonoSslStream)NoReflectionHelper.GetMonoSslStream(stream);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001718A File Offset: 0x0001538A
		public static IMonoSslStream GetMonoSslStream(HttpListenerContext context)
		{
			return (IMonoSslStream)NoReflectionHelper.GetMonoSslStream(context);
		}

		// Token: 0x04000371 RID: 881
		internal const int InternalVersion = 1;
	}
}

using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x0200007D RID: 125
	internal static class NoReflectionHelper
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00008BCE File Offset: 0x00006DCE
		internal static object GetInternalValidator(object provider, object settings)
		{
			return ChainValidationHelper.GetInternalValidator((MonoTlsProvider)provider, (MonoTlsSettings)settings);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008BE1 File Offset: 0x00006DE1
		internal static object GetDefaultValidator(object settings)
		{
			return ChainValidationHelper.GetDefaultValidator((MonoTlsSettings)settings);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008BEE File Offset: 0x00006DEE
		internal static object GetProvider()
		{
			return MonoTlsProviderFactory.GetProvider();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00008BF5 File Offset: 0x00006DF5
		internal static bool IsInitialized
		{
			get
			{
				return MonoTlsProviderFactory.IsInitialized;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008BFC File Offset: 0x00006DFC
		internal static void Initialize()
		{
			MonoTlsProviderFactory.Initialize();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008C03 File Offset: 0x00006E03
		internal static void Initialize(string provider)
		{
			MonoTlsProviderFactory.Initialize(provider);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00008C0B File Offset: 0x00006E0B
		internal static HttpWebRequest CreateHttpsRequest(Uri requestUri, object provider, object settings)
		{
			return new HttpWebRequest(requestUri, (MonoTlsProvider)provider, (MonoTlsSettings)settings);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00008C1F File Offset: 0x00006E1F
		internal static object CreateHttpListener(object certificate, object provider, object settings)
		{
			return new HttpListener((X509Certificate)certificate, (MonoTlsProvider)provider, (MonoTlsSettings)settings);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00008C38 File Offset: 0x00006E38
		internal static object GetMonoSslStream(SslStream stream)
		{
			return stream.Impl;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008C40 File Offset: 0x00006E40
		internal static object GetMonoSslStream(HttpListenerContext context)
		{
			SslStream sslStream = context.Connection.SslStream;
			if (sslStream == null)
			{
				return null;
			}
			return sslStream.Impl;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008C58 File Offset: 0x00006E58
		internal static bool IsProviderSupported(string name)
		{
			return MonoTlsProviderFactory.IsProviderSupported(name);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008C60 File Offset: 0x00006E60
		internal static object GetProvider(string name)
		{
			return MonoTlsProviderFactory.GetProvider(name);
		}
	}
}

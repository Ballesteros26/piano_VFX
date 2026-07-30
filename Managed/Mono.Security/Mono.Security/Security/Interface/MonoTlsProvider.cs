using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000086 RID: 134
	public abstract class MonoTlsProvider
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x00017114 File Offset: 0x00015314
		internal MonoTlsProvider()
		{
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004C6 RID: 1222
		public abstract Guid ID { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004C7 RID: 1223
		public abstract string Name { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004C8 RID: 1224
		public abstract bool SupportsSslStream { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004C9 RID: 1225
		public abstract bool SupportsConnectionInfo { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004CA RID: 1226
		public abstract bool SupportsMonoExtensions { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004CB RID: 1227
		public abstract SslProtocols SupportedProtocols { get; }

		// Token: 0x060004CC RID: 1228
		public abstract IMonoSslStream CreateSslStream(Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings = null);

		// Token: 0x060004CD RID: 1229
		internal abstract IMonoSslStream CreateSslStreamInternal(SslStream sslStream, Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings);

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001711C File Offset: 0x0001531C
		internal virtual bool HasNativeCertificates
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001711F File Offset: 0x0001531F
		internal virtual X509Certificate2Impl GetNativeCertificate(byte[] data, string password, X509KeyStorageFlags flags)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00017126 File Offset: 0x00015326
		internal virtual X509Certificate2Impl GetNativeCertificate(X509Certificate certificate)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060004D1 RID: 1233
		internal abstract bool ValidateCertificate(ICertificateValidator2 validator, string targetHost, bool serverMode, X509CertificateCollection certificates, bool wantsChain, ref X509Chain chain, ref MonoSslPolicyErrors errors, ref int status11);

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004D2 RID: 1234
		internal abstract bool SupportsCleanShutdown { get; }
	}
}

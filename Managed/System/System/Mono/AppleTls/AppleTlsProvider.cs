using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.AppleTls
{
	// Token: 0x020000A8 RID: 168
	internal class AppleTlsProvider : MonoTlsProvider
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		public override string Name
		{
			get
			{
				return "apple-tls";
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000D3FB File Offset: 0x0000B5FB
		public override Guid ID
		{
			get
			{
				return Mono.Net.Security.MonoTlsProviderFactory.AppleTlsId;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00003DF3 File Offset: 0x00001FF3
		public override IMonoSslStream CreateSslStream(Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings = null)
		{
			return SslStream.CreateMonoSslStream(innerStream, leaveInnerStreamOpen, this, settings);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000D402 File Offset: 0x0000B602
		internal override IMonoSslStream CreateSslStreamInternal(SslStream sslStream, Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings)
		{
			return new AppleTlsStream(innerStream, leaveInnerStreamOpen, sslStream, settings, this);
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool SupportsSslStream
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool SupportsMonoExtensions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool SupportsConnectionInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00004240 File Offset: 0x00002440
		internal override bool SupportsCleanShutdown
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00003DEC File Offset: 0x00001FEC
		public override SslProtocols SupportedProtocols
		{
			get
			{
				return SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000D40F File Offset: 0x0000B60F
		internal override bool ValidateCertificate(ICertificateValidator2 validator, string targetHost, bool serverMode, X509CertificateCollection certificates, bool wantsChain, ref X509Chain chain, ref MonoSslPolicyErrors errors, ref int status11)
		{
			if (wantsChain)
			{
				chain = SystemCertificateValidator.CreateX509Chain(certificates);
			}
			return AppleCertificateHelper.InvokeSystemCertificateValidator(validator, targetHost, serverMode, certificates, ref errors, ref status11);
		}
	}
}

using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security.Private;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x02000071 RID: 113
	internal class LegacyTlsProvider : MonoTlsProvider
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00006894 File Offset: 0x00004A94
		public override Guid ID
		{
			get
			{
				return MonoTlsProviderFactory.LegacyId;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000689B File Offset: 0x00004A9B
		public override string Name
		{
			get
			{
				return "legacy";
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool SupportsSslStream
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00004240 File Offset: 0x00002440
		public override bool SupportsConnectionInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00004240 File Offset: 0x00002440
		public override bool SupportsMonoExtensions
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00004240 File Offset: 0x00002440
		internal override bool SupportsCleanShutdown
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000068A2 File Offset: 0x00004AA2
		public override SslProtocols SupportedProtocols
		{
			get
			{
				return SslProtocols.Tls;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00003DF3 File Offset: 0x00001FF3
		public override IMonoSslStream CreateSslStream(Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings = null)
		{
			return SslStream.CreateMonoSslStream(innerStream, leaveInnerStreamOpen, this, settings);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000068A9 File Offset: 0x00004AA9
		internal override IMonoSslStream CreateSslStreamInternal(SslStream sslStream, Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings)
		{
			return new LegacySslStream(innerStream, leaveInnerStreamOpen, sslStream, this, settings);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000068B8 File Offset: 0x00004AB8
		internal override bool ValidateCertificate(ICertificateValidator2 validator, string targetHost, bool serverMode, X509CertificateCollection certificates, bool wantsChain, ref X509Chain chain, ref MonoSslPolicyErrors errors, ref int status11)
		{
			if (wantsChain)
			{
				chain = SystemCertificateValidator.CreateX509Chain(certificates);
			}
			SslPolicyErrors sslPolicyErrors = (SslPolicyErrors)errors;
			bool flag = SystemCertificateValidator.Evaluate(validator.Settings, targetHost, certificates, chain, ref sslPolicyErrors, ref status11);
			errors = (MonoSslPolicyErrors)sslPolicyErrors;
			return flag;
		}
	}
}

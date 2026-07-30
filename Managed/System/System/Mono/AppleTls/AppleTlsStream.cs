using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.AppleTls
{
	// Token: 0x020000A9 RID: 169
	internal class AppleTlsStream : MobileAuthenticatedStream
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000409C File Offset: 0x0000229C
		public AppleTlsStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MonoTlsProvider provider)
			: base(innerStream, leaveInnerStreamOpen, owner, settings, provider)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000D42D File Offset: 0x0000B62D
		protected override MobileTlsContext CreateContext(bool serverMode, string targetHost, SslProtocols enabledProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool askForClientCert)
		{
			return new AppleTlsContext(this, serverMode, targetHost, enabledProtocols, serverCertificate, clientCertificates, askForClientCert);
		}
	}
}

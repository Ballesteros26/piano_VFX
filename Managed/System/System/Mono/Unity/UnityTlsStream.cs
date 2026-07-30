using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Unity
{
	// Token: 0x0200004A RID: 74
	internal class UnityTlsStream : MobileAuthenticatedStream
	{
		// Token: 0x0600011B RID: 283 RVA: 0x0000409C File Offset: 0x0000229C
		public UnityTlsStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MonoTlsProvider provider)
			: base(innerStream, leaveInnerStreamOpen, owner, settings, provider)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000040AB File Offset: 0x000022AB
		protected override MobileTlsContext CreateContext(bool serverMode, string targetHost, SslProtocols enabledProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool askForClientCert)
		{
			return new UnityTlsContext(this, serverMode, targetHost, enabledProtocols, serverCertificate, clientCertificates, askForClientCert);
		}
	}
}

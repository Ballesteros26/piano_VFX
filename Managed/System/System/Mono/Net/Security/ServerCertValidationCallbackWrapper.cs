using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x0200006F RID: 111
	// (Invoke) Token: 0x060001FC RID: 508
	internal delegate bool ServerCertValidationCallbackWrapper(ServerCertValidationCallback callback, X509Certificate certificate, X509Chain chain, MonoSslPolicyErrors sslPolicyErrors);
}

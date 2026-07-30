using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x0200007A RID: 122
	internal interface ICertificateValidator2 : ICertificateValidator
	{
		// Token: 0x06000472 RID: 1138
		ValidationResult ValidateCertificate(string targetHost, bool serverMode, X509Certificate leaf, X509Chain chain);
	}
}

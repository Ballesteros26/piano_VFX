using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x02000503 RID: 1283
	internal class DefaultCertificatePolicy : ICertificatePolicy
	{
		// Token: 0x0600265B RID: 9819 RVA: 0x0009426D File Offset: 0x0009246D
		public bool CheckValidationResult(ServicePoint point, X509Certificate certificate, WebRequest request, int certificateProblem)
		{
			return ServicePointManager.ServerCertificateValidationCallback != null || (certificateProblem == -2146762495 || certificateProblem == 0);
		}
	}
}

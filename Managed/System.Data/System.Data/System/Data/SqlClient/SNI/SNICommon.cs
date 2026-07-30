using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000244 RID: 580
	internal class SNICommon
	{
		// Token: 0x060019C4 RID: 6596 RVA: 0x00082D20 File Offset: 0x00080F20
		internal static bool ValidateSslServerCertificate(string targetServerName, object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
		{
			if (policyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if ((policyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.None)
			{
				return false;
			}
			string text = cert.Subject.Substring(cert.Subject.IndexOf('=') + 1);
			if (targetServerName.Length > text.Length)
			{
				return false;
			}
			if (targetServerName.Length == text.Length)
			{
				if (!targetServerName.Equals(text, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			else
			{
				if (string.Compare(targetServerName, 0, text, 0, targetServerName.Length, StringComparison.OrdinalIgnoreCase) != 0)
				{
					return false;
				}
				if (text[targetServerName.Length] != '.')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00082DA7 File Offset: 0x00080FA7
		internal static uint ReportSNIError(SNIProviders provider, uint nativeError, uint sniError, string errorMessage)
		{
			return SNICommon.ReportSNIError(new SNIError(provider, nativeError, sniError, errorMessage));
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00082DB7 File Offset: 0x00080FB7
		internal static uint ReportSNIError(SNIProviders provider, uint sniError, Exception sniException)
		{
			return SNICommon.ReportSNIError(new SNIError(provider, sniError, sniException));
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00082DC6 File Offset: 0x00080FC6
		internal static uint ReportSNIError(SNIError error)
		{
			SNILoadHandle.SingletonInstance.LastError = error;
			return 1U;
		}

		// Token: 0x04001276 RID: 4726
		internal const int ConnTerminatedError = 2;

		// Token: 0x04001277 RID: 4727
		internal const int InvalidParameterError = 5;

		// Token: 0x04001278 RID: 4728
		internal const int ProtocolNotSupportedError = 8;

		// Token: 0x04001279 RID: 4729
		internal const int ConnTimeoutError = 11;

		// Token: 0x0400127A RID: 4730
		internal const int ConnNotUsableError = 19;

		// Token: 0x0400127B RID: 4731
		internal const int InvalidConnStringError = 25;

		// Token: 0x0400127C RID: 4732
		internal const int HandshakeFailureError = 31;

		// Token: 0x0400127D RID: 4733
		internal const int InternalExceptionError = 35;

		// Token: 0x0400127E RID: 4734
		internal const int ConnOpenFailedError = 40;

		// Token: 0x0400127F RID: 4735
		internal const int ErrorSpnLookup = 44;

		// Token: 0x04001280 RID: 4736
		internal const int LocalDBErrorCode = 50;

		// Token: 0x04001281 RID: 4737
		internal const int MultiSubnetFailoverWithMoreThan64IPs = 47;

		// Token: 0x04001282 RID: 4738
		internal const int MultiSubnetFailoverWithInstanceSpecified = 48;

		// Token: 0x04001283 RID: 4739
		internal const int MultiSubnetFailoverWithNonTcpProtocol = 49;

		// Token: 0x04001284 RID: 4740
		internal const int MaxErrorValue = 50157;

		// Token: 0x04001285 RID: 4741
		internal const int LocalDBNoInstanceName = 51;

		// Token: 0x04001286 RID: 4742
		internal const int LocalDBNoInstallation = 52;

		// Token: 0x04001287 RID: 4743
		internal const int LocalDBInvalidConfig = 53;

		// Token: 0x04001288 RID: 4744
		internal const int LocalDBNoSqlUserInstanceDllPath = 54;

		// Token: 0x04001289 RID: 4745
		internal const int LocalDBInvalidSqlUserInstanceDllPath = 55;

		// Token: 0x0400128A RID: 4746
		internal const int LocalDBFailedToLoadDll = 56;

		// Token: 0x0400128B RID: 4747
		internal const int LocalDBBadRuntime = 57;
	}
}

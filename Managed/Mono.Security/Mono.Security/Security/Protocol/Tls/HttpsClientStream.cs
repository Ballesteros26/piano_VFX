using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200003C RID: 60
	[Obsolete("This class is obsolete and will be removed shortly.")]
	internal class HttpsClientStream : SslClientStream
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		public HttpsClientStream(Stream stream, X509CertificateCollection clientCertificates, HttpWebRequest request, byte[] buffer)
			: base(stream, request.Address.Host, false, (SecurityProtocolType)ServicePointManager.SecurityProtocol, clientCertificates)
		{
			this._request = request;
			this._status = 0;
			if (buffer != null)
			{
				base.InputBuffer.Write(buffer, 0, buffer.Length);
			}
			base.CheckCertRevocationStatus = ServicePointManager.CheckCertificateRevocationList;
			base.ClientCertSelection += delegate(X509CertificateCollection clientCerts, X509Certificate serverCertificate, string targetHost, X509CertificateCollection serverRequestedCertificates)
			{
				if (clientCerts != null && clientCerts.Count != 0)
				{
					return clientCerts[0];
				}
				return null;
			};
			base.PrivateKeySelection += delegate(X509Certificate certificate, string targetHost)
			{
				X509Certificate2 x509Certificate = certificate as X509Certificate2;
				if (x509Certificate != null)
				{
					return x509Certificate.PrivateKey;
				}
				return null;
			};
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000EF90 File Offset: 0x0000D190
		public bool TrustFailure
		{
			get
			{
				int status = this._status;
				return status - -2146762487 <= 1;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		internal override bool RaiseServerCertificateValidation(X509Certificate certificate, int[] certificateErrors)
		{
			bool flag = certificateErrors.Length != 0;
			this._status = (flag ? certificateErrors[0] : 0);
			if (ServicePointManager.CertificatePolicy != null)
			{
				ServicePoint servicePoint = this._request.ServicePoint;
				if (!ServicePointManager.CertificatePolicy.CheckValidationResult(servicePoint, certificate, this._request, this._status))
				{
					return false;
				}
				flag = true;
			}
			if (this.HaveRemoteValidation2Callback)
			{
				return flag;
			}
			RemoteCertificateValidationCallback serverCertificateValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
			if (serverCertificateValidationCallback != null)
			{
				SslPolicyErrors sslPolicyErrors = SslPolicyErrors.None;
				foreach (int num in certificateErrors)
				{
					if (num == -2146762490)
					{
						sslPolicyErrors |= SslPolicyErrors.RemoteCertificateNotAvailable;
					}
					else if (num == -2146762481)
					{
						sslPolicyErrors |= SslPolicyErrors.RemoteCertificateNameMismatch;
					}
					else
					{
						sslPolicyErrors |= SslPolicyErrors.RemoteCertificateChainErrors;
					}
				}
				X509Certificate2 x509Certificate = new X509Certificate2(certificate.GetRawCertData());
				X509Chain x509Chain = new X509Chain();
				if (!x509Chain.Build(x509Certificate))
				{
					sslPolicyErrors |= SslPolicyErrors.RemoteCertificateChainErrors;
				}
				return serverCertificateValidationCallback(this._request, x509Certificate, x509Chain, sslPolicyErrors);
			}
			return flag;
		}

		// Token: 0x04000175 RID: 373
		private HttpWebRequest _request;

		// Token: 0x04000176 RID: 374
		private int _status;
	}
}

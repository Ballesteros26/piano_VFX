using System;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Mono.Security.Interface
{
	// Token: 0x02000080 RID: 128
	public interface IMonoSslStream : IDisposable
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000479 RID: 1145
		SslStream SslStream { get; }

		// Token: 0x0600047A RID: 1146
		void AuthenticateAsClient(string targetHost);

		// Token: 0x0600047B RID: 1147
		void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation);

		// Token: 0x0600047C RID: 1148
		IAsyncResult BeginAuthenticateAsClient(string targetHost, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600047D RID: 1149
		IAsyncResult BeginAuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600047E RID: 1150
		void EndAuthenticateAsClient(IAsyncResult asyncResult);

		// Token: 0x0600047F RID: 1151
		void AuthenticateAsServer(X509Certificate serverCertificate);

		// Token: 0x06000480 RID: 1152
		void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation);

		// Token: 0x06000481 RID: 1153
		IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000482 RID: 1154
		IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000483 RID: 1155
		void EndAuthenticateAsServer(IAsyncResult asyncResult);

		// Token: 0x06000484 RID: 1156
		Task AuthenticateAsClientAsync(string targetHost);

		// Token: 0x06000485 RID: 1157
		Task AuthenticateAsClientAsync(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation);

		// Token: 0x06000486 RID: 1158
		Task AuthenticateAsServerAsync(X509Certificate serverCertificate);

		// Token: 0x06000487 RID: 1159
		Task AuthenticateAsServerAsync(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation);

		// Token: 0x06000488 RID: 1160
		int Read(byte[] buffer, int offset, int count);

		// Token: 0x06000489 RID: 1161
		void Write(byte[] buffer);

		// Token: 0x0600048A RID: 1162
		void Write(byte[] buffer, int offset, int count);

		// Token: 0x0600048B RID: 1163
		IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600048C RID: 1164
		int EndRead(IAsyncResult asyncResult);

		// Token: 0x0600048D RID: 1165
		IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600048E RID: 1166
		void EndWrite(IAsyncResult asyncResult);

		// Token: 0x0600048F RID: 1167
		Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);

		// Token: 0x06000490 RID: 1168
		Task ShutdownAsync();

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000491 RID: 1169
		TransportContext TransportContext { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000492 RID: 1170
		bool IsAuthenticated { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000493 RID: 1171
		bool IsMutuallyAuthenticated { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000494 RID: 1172
		bool IsEncrypted { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000495 RID: 1173
		bool IsSigned { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000496 RID: 1174
		bool IsServer { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000497 RID: 1175
		CipherAlgorithmType CipherAlgorithm { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000498 RID: 1176
		int CipherStrength { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000499 RID: 1177
		HashAlgorithmType HashAlgorithm { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600049A RID: 1178
		int HashStrength { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600049B RID: 1179
		ExchangeAlgorithmType KeyExchangeAlgorithm { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600049C RID: 1180
		int KeyExchangeStrength { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600049D RID: 1181
		bool CanRead { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600049E RID: 1182
		bool CanTimeout { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600049F RID: 1183
		bool CanWrite { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004A0 RID: 1184
		long Length { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004A1 RID: 1185
		long Position { get; }

		// Token: 0x060004A2 RID: 1186
		void SetLength(long value);

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004A3 RID: 1187
		AuthenticatedStream AuthenticatedStream { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004A4 RID: 1188
		// (set) Token: 0x060004A5 RID: 1189
		int ReadTimeout { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004A6 RID: 1190
		// (set) Token: 0x060004A7 RID: 1191
		int WriteTimeout { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004A8 RID: 1192
		bool CheckCertRevocationStatus { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004A9 RID: 1193
		X509Certificate InternalLocalCertificate { get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004AA RID: 1194
		X509Certificate LocalCertificate { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004AB RID: 1195
		X509Certificate RemoteCertificate { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004AC RID: 1196
		SslProtocols SslProtocol { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004AD RID: 1197
		MonoTlsProvider Provider { get; }

		// Token: 0x060004AE RID: 1198
		MonoTlsConnectionInfo GetConnectionInfo();
	}
}

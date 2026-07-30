using System;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x02000057 RID: 87
	internal class TlsClientCertificateVerify : HandshakeMessage
	{
		// Token: 0x060003AE RID: 942 RVA: 0x00013858 File Offset: 0x00011A58
		public TlsClientCertificateVerify(Context context, byte[] buffer)
			: base(context, HandshakeType.CertificateVerify, buffer)
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00013864 File Offset: 0x00011A64
		protected override void ProcessAsSsl3()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			int num = (int)base.ReadInt16();
			byte[] array = base.ReadBytes(num);
			SslHandshakeHash sslHandshakeHash = new SslHandshakeHash(serverContext.MasterSecret);
			sslHandshakeHash.TransformFinalBlock(serverContext.HandshakeMessages.ToArray(), 0, (int)serverContext.HandshakeMessages.Length);
			if (!sslHandshakeHash.VerifySignature(serverContext.ClientSettings.CertificateRSA, array))
			{
				throw new TlsException(AlertDescription.HandshakeFailiure, "Handshake Failure.");
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000138D8 File Offset: 0x00011AD8
		protected override void ProcessAsTls1()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			int num = (int)base.ReadInt16();
			byte[] array = base.ReadBytes(num);
			MD5SHA1 md5SHA = new MD5SHA1();
			md5SHA.ComputeHash(serverContext.HandshakeMessages.ToArray(), 0, (int)serverContext.HandshakeMessages.Length);
			if (!md5SHA.VerifySignature(serverContext.ClientSettings.CertificateRSA, array))
			{
				throw new TlsException(AlertDescription.HandshakeFailiure, "Handshake Failure.");
			}
		}
	}
}

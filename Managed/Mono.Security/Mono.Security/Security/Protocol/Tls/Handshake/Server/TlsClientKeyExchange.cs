using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x0200005A RID: 90
	internal class TlsClientKeyExchange : HandshakeMessage
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00013C57 File Offset: 0x00011E57
		public TlsClientKeyExchange(Context context, byte[] buffer)
			: base(context, HandshakeType.ClientKeyExchange, buffer)
		{
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00013C64 File Offset: 0x00011E64
		protected override void ProcessAsSsl3()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			AsymmetricAlgorithm asymmetricAlgorithm = serverContext.SslStream.RaisePrivateKeySelection(new X509Certificate(serverContext.ServerSettings.Certificates[0].RawData), null);
			if (asymmetricAlgorithm == null)
			{
				throw new TlsException(AlertDescription.UserCancelled, "Server certificate Private Key unavailable.");
			}
			byte[] array = base.ReadBytes((int)this.Length);
			byte[] array2 = new RSAPKCS1KeyExchangeDeformatter(asymmetricAlgorithm).DecryptKeyExchange(array);
			base.Context.Negotiating.Cipher.ComputeMasterSecret(array2);
			base.Context.Negotiating.Cipher.ComputeKeys();
			base.Context.Negotiating.Cipher.InitializeCipher();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00013D10 File Offset: 0x00011F10
		protected override void ProcessAsTls1()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			AsymmetricAlgorithm asymmetricAlgorithm = serverContext.SslStream.RaisePrivateKeySelection(new X509Certificate(serverContext.ServerSettings.Certificates[0].RawData), null);
			if (asymmetricAlgorithm == null)
			{
				throw new TlsException(AlertDescription.UserCancelled, "Server certificate Private Key unavailable.");
			}
			byte[] array = base.ReadBytes((int)base.ReadInt16());
			byte[] array2 = new RSAPKCS1KeyExchangeDeformatter(asymmetricAlgorithm).DecryptKeyExchange(array);
			base.Context.Negotiating.Cipher.ComputeMasterSecret(array2);
			base.Context.Negotiating.Cipher.ComputeKeys();
			base.Context.Negotiating.Cipher.InitializeCipher();
		}
	}
}

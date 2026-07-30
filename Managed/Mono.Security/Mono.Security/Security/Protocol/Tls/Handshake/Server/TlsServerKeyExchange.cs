using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x02000060 RID: 96
	internal class TlsServerKeyExchange : HandshakeMessage
	{
		// Token: 0x060003CF RID: 975 RVA: 0x0001416D File Offset: 0x0001236D
		public TlsServerKeyExchange(Context context)
			: base(context, HandshakeType.ServerKeyExchange)
		{
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00014178 File Offset: 0x00012378
		public override void Update()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001417F File Offset: 0x0001237F
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00014188 File Offset: 0x00012388
		protected override void ProcessAsTls1()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			RSA rsa = (RSA)serverContext.SslStream.PrivateKeyCertSelectionDelegate(new X509Certificate(serverContext.ServerSettings.Certificates[0].RawData), null);
			RSAParameters rsaparameters = rsa.ExportParameters(false);
			base.WriteInt24(rsaparameters.Modulus.Length);
			this.Write(rsaparameters.Modulus, 0, rsaparameters.Modulus.Length);
			base.WriteInt24(rsaparameters.Exponent.Length);
			this.Write(rsaparameters.Exponent, 0, rsaparameters.Exponent.Length);
			byte[] array = this.createSignature(rsa, base.ToArray());
			base.WriteInt24(array.Length);
			base.Write(array);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00014240 File Offset: 0x00012440
		private byte[] createSignature(RSA rsa, byte[] buffer)
		{
			MD5SHA1 md5SHA = new MD5SHA1();
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(base.Context.RandomCS);
			tlsStream.Write(buffer, 0, buffer.Length);
			md5SHA.ComputeHash(tlsStream.ToArray());
			tlsStream.Reset();
			return md5SHA.CreateSignature(rsa);
		}
	}
}

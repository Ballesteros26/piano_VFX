using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x0200006B RID: 107
	internal class TlsServerKeyExchange : HandshakeMessage
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x000155FE File Offset: 0x000137FE
		public TlsServerKeyExchange(Context context, byte[] buffer)
			: base(context, HandshakeType.ServerKeyExchange, buffer)
		{
			this.verifySignature();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00015610 File Offset: 0x00013810
		public override void Update()
		{
			base.Update();
			base.Context.ServerSettings.ServerKeyExchange = true;
			base.Context.ServerSettings.RsaParameters = this.rsaParams;
			base.Context.ServerSettings.SignedParams = this.signedParams;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00015660 File Offset: 0x00013860
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00015668 File Offset: 0x00013868
		protected override void ProcessAsTls1()
		{
			this.rsaParams = default(RSAParameters);
			this.rsaParams.Modulus = base.ReadBytes((int)base.ReadInt16());
			this.rsaParams.Exponent = base.ReadBytes((int)base.ReadInt16());
			this.signedParams = base.ReadBytes((int)base.ReadInt16());
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000156C4 File Offset: 0x000138C4
		private void verifySignature()
		{
			MD5SHA1 md5SHA = new MD5SHA1();
			int num = this.rsaParams.Modulus.Length + this.rsaParams.Exponent.Length + 4;
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(base.Context.RandomCS);
			tlsStream.Write(base.ToArray(), 0, num);
			md5SHA.ComputeHash(tlsStream.ToArray());
			tlsStream.Reset();
			if (!md5SHA.VerifySignature(base.Context.ServerSettings.CertificateRSA, this.signedParams))
			{
				throw new TlsException(AlertDescription.DecodeError, "Data was not signed with the server certificate.");
			}
		}

		// Token: 0x040001EF RID: 495
		private RSAParameters rsaParams;

		// Token: 0x040001F0 RID: 496
		private byte[] signedParams;
	}
}

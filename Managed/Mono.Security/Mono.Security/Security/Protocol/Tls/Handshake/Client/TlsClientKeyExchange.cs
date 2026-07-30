using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x02000065 RID: 101
	internal class TlsClientKeyExchange : HandshakeMessage
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x00014A51 File Offset: 0x00012C51
		public TlsClientKeyExchange(Context context)
			: base(context, HandshakeType.ClientKeyExchange)
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00014A5C File Offset: 0x00012C5C
		protected override void ProcessAsSsl3()
		{
			this.ProcessCommon(false);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00014A65 File Offset: 0x00012C65
		protected override void ProcessAsTls1()
		{
			this.ProcessCommon(true);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00014A70 File Offset: 0x00012C70
		public void ProcessCommon(bool sendLength)
		{
			byte[] array = base.Context.Negotiating.Cipher.CreatePremasterSecret();
			RSA rsa;
			if (base.Context.ServerSettings.ServerKeyExchange)
			{
				rsa = new RSAManaged();
				rsa.ImportParameters(base.Context.ServerSettings.RsaParameters);
			}
			else
			{
				rsa = base.Context.ServerSettings.CertificateRSA;
			}
			byte[] array2 = new RSAPKCS1KeyExchangeFormatter(rsa).CreateKeyExchange(array);
			if (sendLength)
			{
				base.Write((short)array2.Length);
			}
			base.Write(array2);
			base.Context.Negotiating.Cipher.ComputeMasterSecret(array);
			base.Context.Negotiating.Cipher.ComputeKeys();
			rsa.Clear();
		}
	}
}

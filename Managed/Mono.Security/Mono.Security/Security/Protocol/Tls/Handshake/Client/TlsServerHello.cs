using System;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x02000069 RID: 105
	internal class TlsServerHello : HandshakeMessage
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0001539B File Offset: 0x0001359B
		public TlsServerHello(Context context, byte[] buffer)
			: base(context, HandshakeType.ServerHello, buffer)
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000153A8 File Offset: 0x000135A8
		public override void Update()
		{
			base.Update();
			base.Context.SessionId = this.sessionId;
			base.Context.ServerRandom = this.random;
			base.Context.Negotiating.Cipher = this.cipherSuite;
			base.Context.CompressionMethod = this.compressionMethod;
			base.Context.ProtocolNegotiated = true;
			int num = base.Context.ClientRandom.Length;
			int num2 = base.Context.ServerRandom.Length;
			int num3 = num + num2;
			byte[] array = new byte[num3];
			Buffer.BlockCopy(base.Context.ClientRandom, 0, array, 0, num);
			Buffer.BlockCopy(base.Context.ServerRandom, 0, array, num, num2);
			base.Context.RandomCS = array;
			byte[] array2 = new byte[num3];
			Buffer.BlockCopy(base.Context.ServerRandom, 0, array2, 0, num2);
			Buffer.BlockCopy(base.Context.ClientRandom, 0, array2, num2, num);
			base.Context.RandomSC = array2;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000154A4 File Offset: 0x000136A4
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000154AC File Offset: 0x000136AC
		protected override void ProcessAsTls1()
		{
			this.processProtocol(base.ReadInt16());
			this.random = base.ReadBytes(32);
			int num = (int)base.ReadByte();
			if (num > 0)
			{
				this.sessionId = base.ReadBytes(num);
				ClientSessionCache.Add(base.Context.ClientSettings.TargetHost, this.sessionId);
				base.Context.AbbreviatedHandshake = HandshakeMessage.Compare(this.sessionId, base.Context.SessionId);
			}
			else
			{
				base.Context.AbbreviatedHandshake = false;
			}
			short num2 = base.ReadInt16();
			if (base.Context.SupportedCiphers.IndexOf(num2) == -1)
			{
				throw new TlsException(AlertDescription.InsuficientSecurity, "Invalid cipher suite received from server");
			}
			this.cipherSuite = base.Context.SupportedCiphers[num2];
			this.compressionMethod = (SecurityCompressionType)base.ReadByte();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00015580 File Offset: 0x00013780
		private void processProtocol(short protocol)
		{
			SecurityProtocolType securityProtocolType = base.Context.DecodeProtocolCode(protocol, false);
			if ((securityProtocolType & base.Context.SecurityProtocolFlags) == securityProtocolType || (base.Context.SecurityProtocolFlags & SecurityProtocolType.Default) == SecurityProtocolType.Default)
			{
				base.Context.SecurityProtocol = securityProtocolType;
				base.Context.SupportedCiphers = CipherSuiteFactory.GetSupportedCiphers(false, securityProtocolType);
				return;
			}
			throw new TlsException(AlertDescription.ProtocolVersion, "Incorrect protocol version received from server");
		}

		// Token: 0x040001EB RID: 491
		private SecurityCompressionType compressionMethod;

		// Token: 0x040001EC RID: 492
		private byte[] random;

		// Token: 0x040001ED RID: 493
		private byte[] sessionId;

		// Token: 0x040001EE RID: 494
		private CipherSuite cipherSuite;
	}
}

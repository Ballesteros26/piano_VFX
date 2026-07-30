using System;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x02000059 RID: 89
	internal class TlsClientHello : HandshakeMessage
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x00013A55 File Offset: 0x00011C55
		public TlsClientHello(Context context, byte[] buffer)
			: base(context, HandshakeType.ClientHello, buffer)
		{
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00013A60 File Offset: 0x00011C60
		public override void Update()
		{
			base.Update();
			this.selectCipherSuite();
			this.selectCompressionMethod();
			base.Context.SessionId = this.sessionId;
			base.Context.ClientRandom = this.random;
			base.Context.ProtocolNegotiated = true;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00013AAD File Offset: 0x00011CAD
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00013AB8 File Offset: 0x00011CB8
		protected override void ProcessAsTls1()
		{
			this.processProtocol(base.ReadInt16());
			this.random = base.ReadBytes(32);
			this.sessionId = base.ReadBytes((int)base.ReadByte());
			this.cipherSuites = new short[(int)(base.ReadInt16() / 2)];
			for (int i = 0; i < this.cipherSuites.Length; i++)
			{
				this.cipherSuites[i] = base.ReadInt16();
			}
			this.compressionMethods = new byte[(int)base.ReadByte()];
			for (int j = 0; j < this.compressionMethods.Length; j++)
			{
				this.compressionMethods[j] = base.ReadByte();
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00013B58 File Offset: 0x00011D58
		private void processProtocol(short protocol)
		{
			SecurityProtocolType securityProtocolType = base.Context.DecodeProtocolCode(protocol, true);
			if ((securityProtocolType & base.Context.SecurityProtocolFlags) == securityProtocolType || (base.Context.SecurityProtocolFlags & SecurityProtocolType.Default) == SecurityProtocolType.Default)
			{
				base.Context.SecurityProtocol = securityProtocolType;
				base.Context.SupportedCiphers = CipherSuiteFactory.GetSupportedCiphers(true, securityProtocolType);
				return;
			}
			throw new TlsException(AlertDescription.ProtocolVersion, "Incorrect protocol version received from server");
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00013BC8 File Offset: 0x00011DC8
		private void selectCipherSuite()
		{
			for (int i = 0; i < this.cipherSuites.Length; i++)
			{
				int num;
				if ((num = base.Context.SupportedCiphers.IndexOf(this.cipherSuites[i])) != -1)
				{
					base.Context.Negotiating.Cipher = base.Context.SupportedCiphers[num];
					break;
				}
			}
			if (base.Context.Negotiating.Cipher == null)
			{
				throw new TlsException(AlertDescription.InsuficientSecurity, "Insuficient Security");
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00013C49 File Offset: 0x00011E49
		private void selectCompressionMethod()
		{
			base.Context.CompressionMethod = SecurityCompressionType.None;
		}

		// Token: 0x040001DC RID: 476
		private byte[] random;

		// Token: 0x040001DD RID: 477
		private byte[] sessionId;

		// Token: 0x040001DE RID: 478
		private short[] cipherSuites;

		// Token: 0x040001DF RID: 479
		private byte[] compressionMethods;
	}
}

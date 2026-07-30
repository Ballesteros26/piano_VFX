using System;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x0200005E RID: 94
	internal class TlsServerHello : HandshakeMessage
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x00013FCC File Offset: 0x000121CC
		public TlsServerHello(Context context)
			: base(context, HandshakeType.ServerHello)
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00013FD8 File Offset: 0x000121D8
		public override void Update()
		{
			base.Update();
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(this.unixTime);
			tlsStream.Write(this.random);
			base.Context.ServerRandom = tlsStream.ToArray();
			tlsStream.Reset();
			tlsStream.Write(base.Context.ClientRandom);
			tlsStream.Write(base.Context.ServerRandom);
			base.Context.RandomCS = tlsStream.ToArray();
			tlsStream.Reset();
			tlsStream.Write(base.Context.ServerRandom);
			tlsStream.Write(base.Context.ClientRandom);
			base.Context.RandomSC = tlsStream.ToArray();
			tlsStream.Reset();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00014092 File Offset: 0x00012292
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001409C File Offset: 0x0001229C
		protected override void ProcessAsTls1()
		{
			base.Write(base.Context.Protocol);
			this.unixTime = base.Context.GetUnixTime();
			base.Write(this.unixTime);
			this.random = base.Context.GetSecureRandomBytes(28);
			base.Write(this.random);
			if (base.Context.SessionId == null)
			{
				this.WriteByte(0);
			}
			else
			{
				this.WriteByte((byte)base.Context.SessionId.Length);
				base.Write(base.Context.SessionId);
			}
			base.Write(base.Context.Negotiating.Cipher.Code);
			this.WriteByte((byte)base.Context.CompressionMethod);
		}

		// Token: 0x040001E1 RID: 481
		private int unixTime;

		// Token: 0x040001E2 RID: 482
		private byte[] random;
	}
}

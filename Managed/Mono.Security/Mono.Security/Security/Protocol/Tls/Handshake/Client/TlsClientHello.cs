using System;
using System.Net;
using System.Text;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x02000064 RID: 100
	internal class TlsClientHello : HandshakeMessage
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x00014848 File Offset: 0x00012A48
		public TlsClientHello(Context context)
			: base(context, HandshakeType.ClientHello)
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00014852 File Offset: 0x00012A52
		public override void Update()
		{
			ClientContext clientContext = (ClientContext)base.Context;
			base.Update();
			clientContext.ClientRandom = this.random;
			clientContext.ClientHelloProtocol = base.Context.Protocol;
			this.random = null;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00014888 File Offset: 0x00012A88
		protected override void ProcessAsSsl3()
		{
			base.Write(base.Context.Protocol);
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(base.Context.GetUnixTime());
			tlsStream.Write(base.Context.GetSecureRandomBytes(28));
			this.random = tlsStream.ToArray();
			tlsStream.Reset();
			base.Write(this.random);
			base.Context.SessionId = ClientSessionCache.FromHost(base.Context.ClientSettings.TargetHost);
			if (base.Context.SessionId != null)
			{
				base.Write((byte)base.Context.SessionId.Length);
				if (base.Context.SessionId.Length != 0)
				{
					base.Write(base.Context.SessionId);
				}
			}
			else
			{
				base.Write(0);
			}
			base.Write((short)(base.Context.SupportedCiphers.Count * 2));
			for (int i = 0; i < base.Context.SupportedCiphers.Count; i++)
			{
				base.Write(base.Context.SupportedCiphers[i].Code);
			}
			base.Write(1);
			base.Write((byte)base.Context.CompressionMethod);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000149C0 File Offset: 0x00012BC0
		protected override void ProcessAsTls1()
		{
			this.ProcessAsSsl3();
			string targetHost = base.Context.ClientSettings.TargetHost;
			IPAddress ipaddress;
			if (IPAddress.TryParse(targetHost, out ipaddress))
			{
				return;
			}
			TlsStream tlsStream = new TlsStream();
			byte[] bytes = Encoding.UTF8.GetBytes(targetHost);
			tlsStream.Write(0);
			tlsStream.Write((short)(bytes.Length + 5));
			tlsStream.Write((short)(bytes.Length + 3));
			tlsStream.Write(0);
			tlsStream.Write((short)bytes.Length);
			tlsStream.Write(bytes);
			base.Write((short)tlsStream.Length);
			base.Write(tlsStream.ToArray());
		}

		// Token: 0x040001E6 RID: 486
		private byte[] random;
	}
}

using System;

namespace Mono.Security.Protocol.Tls.Handshake
{
	// Token: 0x02000054 RID: 84
	internal abstract class HandshakeMessage : TlsStream
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00013246 File Offset: 0x00011446
		public Context Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001324E File Offset: 0x0001144E
		public HandshakeType HandshakeType
		{
			get
			{
				return this.handshakeType;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00013256 File Offset: 0x00011456
		public ContentType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001325E File Offset: 0x0001145E
		public HandshakeMessage(Context context, HandshakeType handshakeType)
			: this(context, handshakeType, ContentType.Handshake)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001326A File Offset: 0x0001146A
		public HandshakeMessage(Context context, HandshakeType handshakeType, ContentType contentType)
		{
			this.context = context;
			this.handshakeType = handshakeType;
			this.contentType = contentType;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00013287 File Offset: 0x00011487
		public HandshakeMessage(Context context, HandshakeType handshakeType, byte[] data)
			: base(data)
		{
			this.context = context;
			this.handshakeType = handshakeType;
		}

		// Token: 0x060003A1 RID: 929
		protected abstract void ProcessAsTls1();

		// Token: 0x060003A2 RID: 930
		protected abstract void ProcessAsSsl3();

		// Token: 0x060003A3 RID: 931 RVA: 0x000132A0 File Offset: 0x000114A0
		public void Process()
		{
			SecurityProtocolType securityProtocol = this.Context.SecurityProtocol;
			if (securityProtocol <= SecurityProtocolType.Ssl2)
			{
				if (securityProtocol != SecurityProtocolType.Default)
				{
					if (securityProtocol != SecurityProtocolType.Ssl2)
					{
						goto IL_003B;
					}
					goto IL_003B;
				}
			}
			else
			{
				if (securityProtocol == SecurityProtocolType.Ssl3)
				{
					this.ProcessAsSsl3();
					return;
				}
				if (securityProtocol != SecurityProtocolType.Tls)
				{
					goto IL_003B;
				}
			}
			this.ProcessAsTls1();
			return;
			IL_003B:
			throw new NotSupportedException("Unsupported security protocol type");
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000132F4 File Offset: 0x000114F4
		public virtual void Update()
		{
			if (this.CanWrite)
			{
				if (this.cache == null)
				{
					this.cache = this.EncodeMessage();
				}
				this.context.HandshakeMessages.Write(this.cache);
				base.Reset();
				this.cache = null;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00013340 File Offset: 0x00011540
		public virtual byte[] EncodeMessage()
		{
			this.cache = null;
			if (this.CanWrite)
			{
				byte[] array = base.ToArray();
				int num = array.Length;
				this.cache = new byte[4 + num];
				this.cache[0] = (byte)this.HandshakeType;
				this.cache[1] = (byte)(num >> 16);
				this.cache[2] = (byte)(num >> 8);
				this.cache[3] = (byte)num;
				Buffer.BlockCopy(array, 0, this.cache, 4, num);
			}
			return this.cache;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000133BC File Offset: 0x000115BC
		public static bool Compare(byte[] buffer1, byte[] buffer2)
		{
			if (buffer1 == null || buffer2 == null)
			{
				return false;
			}
			if (buffer1.Length != buffer2.Length)
			{
				return false;
			}
			for (int i = 0; i < buffer1.Length; i++)
			{
				if (buffer1[i] != buffer2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040001CB RID: 459
		private Context context;

		// Token: 0x040001CC RID: 460
		private HandshakeType handshakeType;

		// Token: 0x040001CD RID: 461
		private ContentType contentType;

		// Token: 0x040001CE RID: 462
		private byte[] cache;
	}
}

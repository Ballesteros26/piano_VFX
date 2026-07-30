using System;
using System.Net.Sockets;

namespace Mono.Unix
{
	// Token: 0x0200000D RID: 13
	public class PeerCred
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002423 File Offset: 0x00000623
		public PeerCred(Socket sock)
		{
			if (sock.AddressFamily != AddressFamily.Unix)
			{
				throw new ArgumentException("Only Unix sockets are supported", "sock");
			}
			this.data = (PeerCredData)sock.GetSocketOption(SocketOptionLevel.Socket, (SocketOptionName)10001);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000245F File Offset: 0x0000065F
		public int ProcessID
		{
			get
			{
				return this.data.pid;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000246C File Offset: 0x0000066C
		public int UserID
		{
			get
			{
				return this.data.uid;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002479 File Offset: 0x00000679
		public int GroupID
		{
			get
			{
				return this.data.gid;
			}
		}

		// Token: 0x04000052 RID: 82
		private const int so_peercred = 10001;

		// Token: 0x04000053 RID: 83
		private PeerCredData data;
	}
}

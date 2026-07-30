using System;
using System.Net.Sockets;

namespace Mono.Posix
{
	// Token: 0x02000094 RID: 148
	[Obsolete("Use Mono.Unix.PeerCred")]
	public class PeerCred
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x00010080 File Offset: 0x0000E280
		public PeerCred(Socket sock)
		{
			if (sock.AddressFamily != AddressFamily.Unix)
			{
				throw new ArgumentException("Only Unix sockets are supported", "sock");
			}
			this.data = (PeerCredData)sock.GetSocketOption(SocketOptionLevel.Socket, (SocketOptionName)10001);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x000100BC File Offset: 0x0000E2BC
		public int ProcessID
		{
			get
			{
				return this.data.pid;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x000100C9 File Offset: 0x0000E2C9
		public int UserID
		{
			get
			{
				return this.data.uid;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x000100D6 File Offset: 0x0000E2D6
		public int GroupID
		{
			get
			{
				return this.data.gid;
			}
		}

		// Token: 0x040004DD RID: 1245
		private const int so_peercred = 10001;

		// Token: 0x040004DE RID: 1246
		private PeerCredData data;
	}
}

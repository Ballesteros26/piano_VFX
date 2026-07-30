using System;
using System.Net.Sockets;

namespace Mono.Unix
{
	// Token: 0x0200000F RID: 15
	public class UnixClient : MarshalByRefObject, IDisposable
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002CAC File Offset: 0x00000EAC
		public UnixClient()
		{
			if (this.client != null)
			{
				this.client.Close();
				this.client = null;
			}
			this.client = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002CDC File Offset: 0x00000EDC
		public UnixClient(string path)
			: this()
		{
			if (path == null)
			{
				throw new ArgumentNullException("ep");
			}
			this.Connect(path);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CF9 File Offset: 0x00000EF9
		public UnixClient(UnixEndPoint ep)
			: this()
		{
			if (ep == null)
			{
				throw new ArgumentNullException("ep");
			}
			this.Connect(ep);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D16 File Offset: 0x00000F16
		internal UnixClient(Socket sock)
		{
			this.Client = sock;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002D25 File Offset: 0x00000F25
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002D2D File Offset: 0x00000F2D
		public Socket Client
		{
			get
			{
				return this.client;
			}
			set
			{
				this.client = value;
				this.stream = null;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D3D File Offset: 0x00000F3D
		public PeerCred PeerCredential
		{
			get
			{
				this.CheckDisposed();
				return new PeerCred(this.client);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D50 File Offset: 0x00000F50
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002D72 File Offset: 0x00000F72
		public LingerOption LingerState
		{
			get
			{
				this.CheckDisposed();
				return (LingerOption)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002D90 File Offset: 0x00000F90
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002DB2 File Offset: 0x00000FB2
		public int ReceiveBufferSize
		{
			get
			{
				this.CheckDisposed();
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002DD0 File Offset: 0x00000FD0
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002DF2 File Offset: 0x00000FF2
		public int ReceiveTimeout
		{
			get
			{
				this.CheckDisposed();
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002E10 File Offset: 0x00001010
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002E32 File Offset: 0x00001032
		public int SendBufferSize
		{
			get
			{
				this.CheckDisposed();
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002E50 File Offset: 0x00001050
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002E72 File Offset: 0x00001072
		public int SendTimeout
		{
			get
			{
				this.CheckDisposed();
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E90 File Offset: 0x00001090
		public void Close()
		{
			this.CheckDisposed();
			this.Dispose();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E9E File Offset: 0x0000109E
		public void Connect(UnixEndPoint remoteEndPoint)
		{
			this.CheckDisposed();
			this.client.Connect(remoteEndPoint);
			this.stream = new NetworkStream(this.client, true);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002EC4 File Offset: 0x000010C4
		public void Connect(string path)
		{
			this.CheckDisposed();
			this.Connect(new UnixEndPoint(path));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002ED8 File Offset: 0x000010D8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002EE8 File Offset: 0x000010E8
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				NetworkStream networkStream = this.stream;
				this.stream = null;
				if (networkStream != null)
				{
					networkStream.Close();
				}
				else if (this.client != null)
				{
					this.client.Close();
				}
				this.client = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F3D File Offset: 0x0000113D
		public NetworkStream GetStream()
		{
			this.CheckDisposed();
			if (this.stream == null)
			{
				this.stream = new NetworkStream(this.client, true);
			}
			return this.stream;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002F65 File Offset: 0x00001165
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F80 File Offset: 0x00001180
		~UnixClient()
		{
			this.Dispose(false);
		}

		// Token: 0x0400005D RID: 93
		private NetworkStream stream;

		// Token: 0x0400005E RID: 94
		private Socket client;

		// Token: 0x0400005F RID: 95
		private bool disposed;
	}
}

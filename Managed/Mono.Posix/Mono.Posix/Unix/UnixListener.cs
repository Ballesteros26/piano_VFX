using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Mono.Unix
{
	// Token: 0x0200001A RID: 26
	public class UnixListener : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000136 RID: 310 RVA: 0x000056A4 File Offset: 0x000038A4
		private void Init(UnixEndPoint ep)
		{
			this.listening = false;
			string filename = ep.Filename;
			if (File.Exists(filename))
			{
				Socket socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
				try
				{
					socket.Connect(ep);
					socket.Close();
					throw new InvalidOperationException("There's already a server listening on " + filename);
				}
				catch (SocketException)
				{
				}
				File.Delete(filename);
			}
			this.server = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
			this.server.Bind(ep);
			this.savedEP = this.server.LocalEndPoint;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005734 File Offset: 0x00003934
		public UnixListener(string path)
		{
			if (!Directory.Exists(Path.GetDirectoryName(path)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
			}
			this.Init(new UnixEndPoint(path));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005761 File Offset: 0x00003961
		public UnixListener(UnixEndPoint localEndPoint)
		{
			if (localEndPoint == null)
			{
				throw new ArgumentNullException("localendPoint");
			}
			this.Init(localEndPoint);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000577E File Offset: 0x0000397E
		public EndPoint LocalEndpoint
		{
			get
			{
				return this.savedEP;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005786 File Offset: 0x00003986
		protected Socket Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000578E File Offset: 0x0000398E
		public Socket AcceptSocket()
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return this.server.Accept();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000057B4 File Offset: 0x000039B4
		public UnixClient AcceptUnixClient()
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return new UnixClient(this.AcceptSocket());
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000057DC File Offset: 0x000039DC
		~UnixListener()
		{
			this.Dispose(false);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000580C File Offset: 0x00003A0C
		public bool Pending()
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return this.server.Poll(1000, SelectMode.SelectRead);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005838 File Offset: 0x00003A38
		public void Start()
		{
			this.Start(5);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005841 File Offset: 0x00003A41
		public void Start(int backlog)
		{
			this.CheckDisposed();
			if (this.listening)
			{
				return;
			}
			this.server.Listen(backlog);
			this.listening = true;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005865 File Offset: 0x00003A65
		public void Stop()
		{
			this.CheckDisposed();
			this.Dispose(true);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005874 File Offset: 0x00003A74
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005884 File Offset: 0x00003A84
		protected void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				try
				{
					File.Delete(((UnixEndPoint)this.savedEP).Filename);
				}
				catch
				{
				}
				if (this.server != null)
				{
					this.server.Close();
				}
				this.server = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000058E8 File Offset: 0x00003AE8
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04000077 RID: 119
		private bool disposed;

		// Token: 0x04000078 RID: 120
		private bool listening;

		// Token: 0x04000079 RID: 121
		private Socket server;

		// Token: 0x0400007A RID: 122
		private EndPoint savedEP;
	}
}

using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000492 RID: 1170
	internal class PooledStream : Stream
	{
		// Token: 0x06002280 RID: 8832 RVA: 0x00086114 File Offset: 0x00084314
		internal PooledStream(object owner)
		{
			this.m_Owner = new WeakReference(owner);
			this.m_PooledCount = -1;
			this.m_Initalizing = true;
			this.m_NetworkStream = new NetworkStream();
			this.m_CreateTime = DateTime.UtcNow;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0008614C File Offset: 0x0008434C
		internal PooledStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime)
		{
			this.m_ConnectionPool = connectionPool;
			this.m_Lifetime = lifetime;
			this.m_CheckLifetime = checkLifetime;
			this.m_Initalizing = true;
			this.m_NetworkStream = new NetworkStream();
			this.m_CreateTime = DateTime.UtcNow;
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x00086186 File Offset: 0x00084386
		internal bool JustConnected
		{
			get
			{
				if (this.m_JustConnected)
				{
					this.m_JustConnected = false;
					return true;
				}
				return false;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x0008619A File Offset: 0x0008439A
		internal IPAddress ServerAddress
		{
			get
			{
				return this.m_ServerAddress;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000861A2 File Offset: 0x000843A2
		internal bool IsInitalizing
		{
			get
			{
				return this.m_Initalizing;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x000861AC File Offset: 0x000843AC
		// (set) Token: 0x06002286 RID: 8838 RVA: 0x000861F1 File Offset: 0x000843F1
		internal bool CanBePooled
		{
			get
			{
				if (this.m_Initalizing)
				{
					return true;
				}
				if (!this.m_NetworkStream.Connected)
				{
					return false;
				}
				WeakReference owner = this.m_Owner;
				return !this.m_ConnectionIsDoomed && (owner == null || !owner.IsAlive);
			}
			set
			{
				this.m_ConnectionIsDoomed |= !value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x00086204 File Offset: 0x00084404
		internal bool IsEmancipated
		{
			get
			{
				WeakReference owner = this.m_Owner;
				return 0 >= this.m_PooledCount && (owner == null || !owner.IsAlive);
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x00086234 File Offset: 0x00084434
		// (set) Token: 0x06002289 RID: 8841 RVA: 0x0008625C File Offset: 0x0008445C
		internal object Owner
		{
			get
			{
				WeakReference owner = this.m_Owner;
				if (owner != null && owner.IsAlive)
				{
					return owner.Target;
				}
				return null;
			}
			set
			{
				lock (this)
				{
					if (this.m_Owner != null)
					{
						this.m_Owner.Target = value;
					}
				}
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000862A8 File Offset: 0x000844A8
		internal ConnectionPool Pool
		{
			get
			{
				return this.m_ConnectionPool;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x000862B0 File Offset: 0x000844B0
		internal virtual ServicePoint ServicePoint
		{
			get
			{
				return this.Pool.ServicePoint;
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000862BD File Offset: 0x000844BD
		internal bool Activate(object owningObject, GeneralAsyncDelegate asyncCallback)
		{
			return this.Activate(owningObject, asyncCallback != null, asyncCallback);
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000862CC File Offset: 0x000844CC
		protected bool Activate(object owningObject, bool async, GeneralAsyncDelegate asyncCallback)
		{
			bool flag;
			try
			{
				if (this.m_Initalizing)
				{
					IPAddress ipaddress = null;
					this.m_AsyncCallback = asyncCallback;
					Socket connection = this.ServicePoint.GetConnection(this, owningObject, async, out ipaddress, ref this.m_AbortSocket, ref this.m_AbortSocket6);
					if (connection != null)
					{
						bool on = Logging.On;
						this.m_NetworkStream.InitNetworkStream(connection, FileAccess.ReadWrite);
						this.m_ServerAddress = ipaddress;
						this.m_Initalizing = false;
						this.m_JustConnected = true;
						this.m_AbortSocket = null;
						this.m_AbortSocket6 = null;
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					if (async && asyncCallback != null)
					{
						asyncCallback(owningObject, this);
					}
					flag = true;
				}
			}
			catch
			{
				this.m_Initalizing = false;
				throw;
			}
			return flag;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00086378 File Offset: 0x00084578
		internal void Deactivate()
		{
			this.m_AsyncCallback = null;
			if (!this.m_ConnectionIsDoomed && this.m_CheckLifetime)
			{
				this.CheckLifetime();
			}
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00086398 File Offset: 0x00084598
		internal virtual void ConnectionCallback(object owningObject, Exception e, Socket socket, IPAddress address)
		{
			object obj = null;
			if (e != null)
			{
				this.m_Initalizing = false;
				obj = e;
			}
			else
			{
				try
				{
					bool on = Logging.On;
					this.m_NetworkStream.InitNetworkStream(socket, FileAccess.ReadWrite);
					obj = this;
				}
				catch (Exception ex)
				{
					if (NclUtilities.IsFatal(ex))
					{
						throw;
					}
					obj = ex;
				}
				this.m_ServerAddress = address;
				this.m_Initalizing = false;
				this.m_JustConnected = true;
			}
			if (this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(owningObject, obj);
			}
			this.m_AbortSocket = null;
			this.m_AbortSocket6 = null;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x00086424 File Offset: 0x00084624
		protected void CheckLifetime()
		{
			if (!this.m_ConnectionIsDoomed)
			{
				TimeSpan timeSpan = DateTime.UtcNow.Subtract(this.m_CreateTime);
				this.m_ConnectionIsDoomed = 0 < TimeSpan.Compare(this.m_Lifetime, timeSpan);
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x00086468 File Offset: 0x00084668
		internal void UpdateLifetime()
		{
			int connectionLeaseTimeout = this.ServicePoint.ConnectionLeaseTimeout;
			TimeSpan maxValue;
			if (connectionLeaseTimeout == -1)
			{
				maxValue = TimeSpan.MaxValue;
				this.m_CheckLifetime = false;
			}
			else
			{
				maxValue = new TimeSpan(0, 0, 0, 0, connectionLeaseTimeout);
				this.m_CheckLifetime = true;
			}
			if (maxValue != this.m_Lifetime)
			{
				this.m_Lifetime = maxValue;
			}
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000864BC File Offset: 0x000846BC
		internal void PrePush(object expectedOwner)
		{
			lock (this)
			{
				if (expectedOwner == null)
				{
					if (this.m_Owner != null && this.m_Owner.Target != null)
					{
						throw new InternalException();
					}
				}
				else if (this.m_Owner == null || this.m_Owner.Target != expectedOwner)
				{
					throw new InternalException();
				}
				this.m_PooledCount++;
				if (1 != this.m_PooledCount)
				{
					throw new InternalException();
				}
				if (this.m_Owner != null)
				{
					this.m_Owner.Target = null;
				}
			}
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0008655C File Offset: 0x0008475C
		internal void PostPop(object newOwner)
		{
			lock (this)
			{
				if (this.m_Owner == null)
				{
					this.m_Owner = new WeakReference(newOwner);
				}
				else
				{
					if (this.m_Owner.Target != null)
					{
						throw new InternalException();
					}
					this.m_Owner.Target = newOwner;
				}
				this.m_PooledCount--;
				if (this.Pool != null)
				{
					if (this.m_PooledCount != 0)
					{
						throw new InternalException();
					}
				}
				else if (-1 != this.m_PooledCount)
				{
					throw new InternalException();
				}
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x00004240 File Offset: 0x00002440
		protected bool UsingSecureStream
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x000865FC File Offset: 0x000847FC
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x00086604 File Offset: 0x00084804
		internal NetworkStream NetworkStream
		{
			get
			{
				return this.m_NetworkStream;
			}
			set
			{
				this.m_Initalizing = false;
				this.m_NetworkStream = value;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x00086614 File Offset: 0x00084814
		protected Socket Socket
		{
			get
			{
				return this.m_NetworkStream.InternalSocket;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x00086621 File Offset: 0x00084821
		public override bool CanRead
		{
			get
			{
				return this.m_NetworkStream.CanRead;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x0008662E File Offset: 0x0008482E
		public override bool CanSeek
		{
			get
			{
				return this.m_NetworkStream.CanSeek;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x0008663B File Offset: 0x0008483B
		public override bool CanWrite
		{
			get
			{
				return this.m_NetworkStream.CanWrite;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x00086648 File Offset: 0x00084848
		public override bool CanTimeout
		{
			get
			{
				return this.m_NetworkStream.CanTimeout;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x00086655 File Offset: 0x00084855
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x00086662 File Offset: 0x00084862
		public override int ReadTimeout
		{
			get
			{
				return this.m_NetworkStream.ReadTimeout;
			}
			set
			{
				this.m_NetworkStream.ReadTimeout = value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x00086670 File Offset: 0x00084870
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x0008667D File Offset: 0x0008487D
		public override int WriteTimeout
		{
			get
			{
				return this.m_NetworkStream.WriteTimeout;
			}
			set
			{
				this.m_NetworkStream.WriteTimeout = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x0008668B File Offset: 0x0008488B
		public override long Length
		{
			get
			{
				return this.m_NetworkStream.Length;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x00086698 File Offset: 0x00084898
		// (set) Token: 0x060022A2 RID: 8866 RVA: 0x000866A5 File Offset: 0x000848A5
		public override long Position
		{
			get
			{
				return this.m_NetworkStream.Position;
			}
			set
			{
				this.m_NetworkStream.Position = value;
			}
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000866B3 File Offset: 0x000848B3
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_NetworkStream.Seek(offset, origin);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000866C2 File Offset: 0x000848C2
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this.m_NetworkStream.Read(buffer, offset, size);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000866D2 File Offset: 0x000848D2
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.m_NetworkStream.Write(buffer, offset, size);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000866E2 File Offset: 0x000848E2
		internal void MultipleWrite(BufferOffsetSize[] buffers)
		{
			this.m_NetworkStream.MultipleWrite(buffers);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000866F0 File Offset: 0x000848F0
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.m_Owner = null;
					this.m_ConnectionIsDoomed = true;
					this.CloseSocket();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x00086730 File Offset: 0x00084930
		internal void CloseSocket()
		{
			Socket abortSocket = this.m_AbortSocket;
			Socket abortSocket2 = this.m_AbortSocket6;
			this.m_NetworkStream.Close();
			if (abortSocket != null)
			{
				abortSocket.Close();
			}
			if (abortSocket2 != null)
			{
				abortSocket2.Close();
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00086768 File Offset: 0x00084968
		public void Close(int timeout)
		{
			Socket abortSocket = this.m_AbortSocket;
			Socket abortSocket2 = this.m_AbortSocket6;
			this.m_NetworkStream.Close(timeout);
			if (abortSocket != null)
			{
				abortSocket.Close(timeout);
			}
			if (abortSocket2 != null)
			{
				abortSocket2.Close(timeout);
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000867A3 File Offset: 0x000849A3
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000867B7 File Offset: 0x000849B7
		internal virtual IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.UnsafeBeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000867CB File Offset: 0x000849CB
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_NetworkStream.EndRead(asyncResult);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000867D9 File Offset: 0x000849D9
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000867ED File Offset: 0x000849ED
		internal virtual IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.UnsafeBeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x00086801 File Offset: 0x00084A01
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_NetworkStream.EndWrite(asyncResult);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0008680F File Offset: 0x00084A0F
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginMultipleWrite(buffers, callback, state);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0008681F File Offset: 0x00084A1F
		internal void EndMultipleWrite(IAsyncResult asyncResult)
		{
			this.m_NetworkStream.EndMultipleWrite(asyncResult);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0008682D File Offset: 0x00084A2D
		public override void Flush()
		{
			this.m_NetworkStream.Flush();
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0008683A File Offset: 0x00084A3A
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.m_NetworkStream.FlushAsync(cancellationToken);
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x00086848 File Offset: 0x00084A48
		public override void SetLength(long value)
		{
			this.m_NetworkStream.SetLength(value);
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00086856 File Offset: 0x00084A56
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			this.m_NetworkStream.SetSocketTimeoutOption(mode, timeout, silent);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00086866 File Offset: 0x00084A66
		internal bool Poll(int microSeconds, SelectMode mode)
		{
			return this.m_NetworkStream.Poll(microSeconds, mode);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00086875 File Offset: 0x00084A75
		internal bool PollRead()
		{
			return this.m_NetworkStream.PollRead();
		}

		// Token: 0x04001F0C RID: 7948
		private bool m_CheckLifetime;

		// Token: 0x04001F0D RID: 7949
		private TimeSpan m_Lifetime;

		// Token: 0x04001F0E RID: 7950
		private DateTime m_CreateTime;

		// Token: 0x04001F0F RID: 7951
		private bool m_ConnectionIsDoomed;

		// Token: 0x04001F10 RID: 7952
		private ConnectionPool m_ConnectionPool;

		// Token: 0x04001F11 RID: 7953
		private WeakReference m_Owner;

		// Token: 0x04001F12 RID: 7954
		private int m_PooledCount;

		// Token: 0x04001F13 RID: 7955
		private bool m_Initalizing;

		// Token: 0x04001F14 RID: 7956
		private IPAddress m_ServerAddress;

		// Token: 0x04001F15 RID: 7957
		private NetworkStream m_NetworkStream;

		// Token: 0x04001F16 RID: 7958
		private Socket m_AbortSocket;

		// Token: 0x04001F17 RID: 7959
		private Socket m_AbortSocket6;

		// Token: 0x04001F18 RID: 7960
		private bool m_JustConnected;

		// Token: 0x04001F19 RID: 7961
		private GeneralAsyncDelegate m_AsyncCallback;
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020005E1 RID: 1505
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class SocketAsyncResult : IOAsyncResult
	{
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000BC66F File Offset: 0x000BA86F
		public IntPtr Handle
		{
			get
			{
				if (this.socket == null)
				{
					return IntPtr.Zero;
				}
				return this.socket.Handle;
			}
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000BC68A File Offset: 0x000BA88A
		public SocketAsyncResult()
		{
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000BC694 File Offset: 0x000BA894
		public void Init(Socket socket, AsyncCallback callback, object state, SocketOperation operation)
		{
			base.Init(callback, state);
			this.socket = socket;
			this.operation = operation;
			this.DelayedException = null;
			this.EndPoint = null;
			this.Buffer = null;
			this.Offset = 0;
			this.Size = 0;
			this.SockFlags = SocketFlags.None;
			this.AcceptSocket = null;
			this.Addresses = null;
			this.Port = 0;
			this.Buffers = null;
			this.ReuseSocket = false;
			this.CurrentAddress = 0;
			this.AcceptedSocket = null;
			this.Total = 0;
			this.error = 0;
			this.EndCalled = 0;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000BC728 File Offset: 0x000BA928
		public SocketAsyncResult(Socket socket, AsyncCallback callback, object state, SocketOperation operation)
			: base(callback, state)
		{
			this.socket = socket;
			this.operation = operation;
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x000BC744 File Offset: 0x000BA944
		public SocketError ErrorCode
		{
			get
			{
				SocketException ex = this.DelayedException as SocketException;
				if (ex != null)
				{
					return ex.SocketErrorCode;
				}
				if (this.error != 0)
				{
					return (SocketError)this.error;
				}
				return SocketError.Success;
			}
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000BC777 File Offset: 0x000BA977
		public void CheckIfThrowDelayedException()
		{
			if (this.DelayedException != null)
			{
				this.socket.is_connected = false;
				throw this.DelayedException;
			}
			if (this.error != 0)
			{
				this.socket.is_connected = false;
				throw new SocketException(this.error);
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000BC7B4 File Offset: 0x000BA9B4
		internal override void CompleteDisposed()
		{
			this.Complete();
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000BC7BC File Offset: 0x000BA9BC
		public void Complete()
		{
			if (this.operation != SocketOperation.Receive && this.socket.CleanedUp)
			{
				this.DelayedException = new ObjectDisposedException(this.socket.GetType().ToString());
			}
			base.IsCompleted = true;
			Socket socket = this.socket;
			SocketOperation socketOperation = this.operation;
			if (base.AsyncCallback != null)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
				{
					((SocketAsyncResult)state).AsyncCallback((SocketAsyncResult)state);
				}, this);
			}
			switch (socketOperation)
			{
			case SocketOperation.Accept:
			case SocketOperation.Receive:
			case SocketOperation.ReceiveFrom:
			case SocketOperation.ReceiveGeneric:
				socket.ReadSem.Release();
				return;
			case SocketOperation.Connect:
			case SocketOperation.RecvJustCallback:
			case SocketOperation.SendJustCallback:
			case SocketOperation.Disconnect:
			case SocketOperation.AcceptReceive:
				break;
			case SocketOperation.Send:
			case SocketOperation.SendTo:
			case SocketOperation.SendGeneric:
				socket.WriteSem.Release();
				break;
			default:
				return;
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000BC88D File Offset: 0x000BAA8D
		public void Complete(bool synch)
		{
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000BC89C File Offset: 0x000BAA9C
		public void Complete(int total)
		{
			this.Total = total;
			this.Complete();
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000BC8AB File Offset: 0x000BAAAB
		public void Complete(Exception e, bool synch)
		{
			this.DelayedException = e;
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000BC8C1 File Offset: 0x000BAAC1
		public void Complete(Exception e)
		{
			this.DelayedException = e;
			this.Complete();
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000BC8D0 File Offset: 0x000BAAD0
		public void Complete(Socket s)
		{
			this.AcceptedSocket = s;
			this.Complete();
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000BC8DF File Offset: 0x000BAADF
		public void Complete(Socket s, int total)
		{
			this.AcceptedSocket = s;
			this.Total = total;
			this.Complete();
		}

		// Token: 0x04002741 RID: 10049
		public Socket socket;

		// Token: 0x04002742 RID: 10050
		public SocketOperation operation;

		// Token: 0x04002743 RID: 10051
		private Exception DelayedException;

		// Token: 0x04002744 RID: 10052
		public EndPoint EndPoint;

		// Token: 0x04002745 RID: 10053
		public byte[] Buffer;

		// Token: 0x04002746 RID: 10054
		public int Offset;

		// Token: 0x04002747 RID: 10055
		public int Size;

		// Token: 0x04002748 RID: 10056
		public SocketFlags SockFlags;

		// Token: 0x04002749 RID: 10057
		public Socket AcceptSocket;

		// Token: 0x0400274A RID: 10058
		public IPAddress[] Addresses;

		// Token: 0x0400274B RID: 10059
		public int Port;

		// Token: 0x0400274C RID: 10060
		public IList<ArraySegment<byte>> Buffers;

		// Token: 0x0400274D RID: 10061
		public bool ReuseSocket;

		// Token: 0x0400274E RID: 10062
		public int CurrentAddress;

		// Token: 0x0400274F RID: 10063
		public Socket AcceptedSocket;

		// Token: 0x04002750 RID: 10064
		public int Total;

		// Token: 0x04002751 RID: 10065
		internal int error;

		// Token: 0x04002752 RID: 10066
		public int EndCalled;
	}
}

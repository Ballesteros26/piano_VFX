using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity;

namespace System.Net.Sockets
{
	/// <summary>Represents an asynchronous socket operation.</summary>
	// Token: 0x020005E0 RID: 1504
	public class SocketAsyncEventArgs : EventArgs, IDisposable
	{
		/// <summary>Gets the exception in the case of a connection failure when a <see cref="T:System.Net.DnsEndPoint" /> was used.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that indicates the cause of the connection error when a <see cref="T:System.Net.DnsEndPoint" /> was specified for the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> property.</returns>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000BC2D7 File Offset: 0x000BA4D7
		// (set) Token: 0x06002F9F RID: 12191 RVA: 0x000BC2DF File Offset: 0x000BA4DF
		public Exception ConnectByNameError { get; internal set; }

		/// <summary>Gets or sets the socket to use or the socket created for accepting a connection with an asynchronous socket method.</summary>
		/// <returns>The <see cref="T:System.Net.Sockets.Socket" /> to use or the socket created for accepting a connection with an asynchronous socket method.</returns>
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000BC2E8 File Offset: 0x000BA4E8
		// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x000BC2F0 File Offset: 0x000BA4F0
		public Socket AcceptSocket { get; set; }

		/// <summary>Gets the data buffer to use with an asynchronous socket method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that represents the data buffer to use with an asynchronous socket method.</returns>
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000BC2F9 File Offset: 0x000BA4F9
		// (set) Token: 0x06002FA3 RID: 12195 RVA: 0x000BC301 File Offset: 0x000BA501
		public byte[] Buffer { get; private set; }

		/// <summary>Gets or sets an array of data buffers to use with an asynchronous socket method.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents an array of data buffers to use with an asynchronous socket method.</returns>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified on a set operation. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property has been set to a non-null value and an attempt was made to set the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property to a non-null value.</exception>
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x000BC30A File Offset: 0x000BA50A
		// (set) Token: 0x06002FA5 RID: 12197 RVA: 0x000BC312 File Offset: 0x000BA512
		public IList<ArraySegment<byte>> BufferList
		{
			get
			{
				return this.m_BufferList;
			}
			set
			{
				if (this.Buffer != null && value != null)
				{
					throw new ArgumentException("Buffer and BufferList properties cannot both be non-null.");
				}
				this.m_BufferList = value;
			}
		}

		/// <summary>Gets the number of bytes transferred in the socket operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of bytes transferred in the socket operation.</returns>
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000BC331 File Offset: 0x000BA531
		// (set) Token: 0x06002FA7 RID: 12199 RVA: 0x000BC339 File Offset: 0x000BA539
		public int BytesTransferred { get; internal set; }

		/// <summary>Gets the maximum amount of data, in bytes, to send or receive in an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the maximum amount of data, in bytes, to send or receive.</returns>
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x000BC342 File Offset: 0x000BA542
		// (set) Token: 0x06002FA9 RID: 12201 RVA: 0x000BC34A File Offset: 0x000BA54A
		public int Count { get; internal set; }

		/// <summary>Gets or sets a value that specifies if socket can be reused after a disconnect operation.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that specifies if socket can be reused after a disconnect operation.</returns>
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x000BC353 File Offset: 0x000BA553
		// (set) Token: 0x06002FAB RID: 12203 RVA: 0x000BC35B File Offset: 0x000BA55B
		public bool DisconnectReuseSocket { get; set; }

		/// <summary>Gets the type of socket operation most recently performed with this context object.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketAsyncOperation" /> instance that indicates the type of socket operation most recently performed with this context object.</returns>
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000BC364 File Offset: 0x000BA564
		// (set) Token: 0x06002FAD RID: 12205 RVA: 0x000BC36C File Offset: 0x000BA56C
		public SocketAsyncOperation LastOperation { get; private set; }

		/// <summary>Gets the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</returns>
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000BC375 File Offset: 0x000BA575
		// (set) Token: 0x06002FAF RID: 12207 RVA: 0x000BC37D File Offset: 0x000BA57D
		public int Offset { get; private set; }

		/// <summary>Gets or sets the remote IP endpoint for an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> that represents the remote IP endpoint for an asynchronous operation.</returns>
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x000BC386 File Offset: 0x000BA586
		// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x000BC38E File Offset: 0x000BA58E
		public EndPoint RemoteEndPoint
		{
			get
			{
				return this.remote_ep;
			}
			set
			{
				this.remote_ep = value;
			}
		}

		/// <summary>Gets the IP address and interface of a received packet.</summary>
		/// <returns>An <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that contains the destination IP address and interface of a received packet.</returns>
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000BC397 File Offset: 0x000BA597
		// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x000BC39F File Offset: 0x000BA59F
		public IPPacketInformation ReceiveMessageFromPacketInfo { get; private set; }

		/// <summary>Gets or sets an array of buffers to be sent for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Net.Sockets.SendPacketsElement" /> objects that represent an array of buffers to be sent.</returns>
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x000BC3A8 File Offset: 0x000BA5A8
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x000BC3B0 File Offset: 0x000BA5B0
		public SendPacketsElement[] SendPacketsElements { get; set; }

		/// <summary>Gets or sets a bitwise combination of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TransmitFileOptions" /> that contains a bitwise combination of values that are used with an asynchronous operation.</returns>
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000BC3B9 File Offset: 0x000BA5B9
		// (set) Token: 0x06002FB7 RID: 12215 RVA: 0x000BC3C1 File Offset: 0x000BA5C1
		public TransmitFileOptions SendPacketsFlags { get; set; }

		/// <summary>Gets or sets the size, in bytes, of the data block used in the send operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the data block used in the send operation.</returns>
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000BC3CA File Offset: 0x000BA5CA
		// (set) Token: 0x06002FB9 RID: 12217 RVA: 0x000BC3D2 File Offset: 0x000BA5D2
		[MonoTODO("unused property")]
		public int SendPacketsSendSize { get; set; }

		/// <summary>Gets or sets the result of the asynchronous socket operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketError" /> that represents the result of the asynchronous socket operation.</returns>
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000BC3DB File Offset: 0x000BA5DB
		// (set) Token: 0x06002FBB RID: 12219 RVA: 0x000BC3E3 File Offset: 0x000BA5E3
		public SocketError SocketError { get; set; }

		/// <summary>Gets the results of an asynchronous socket operation or sets the behavior of an asynchronous operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketFlags" /> that represents the results of an asynchronous socket operation.</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000BC3EC File Offset: 0x000BA5EC
		// (set) Token: 0x06002FBD RID: 12221 RVA: 0x000BC3F4 File Offset: 0x000BA5F4
		public SocketFlags SocketFlags { get; set; }

		/// <summary>Gets or sets a user or application object associated with this asynchronous socket operation.</summary>
		/// <returns>An object that represents the user or application object associated with this asynchronous socket operation.</returns>
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x000BC3FD File Offset: 0x000BA5FD
		// (set) Token: 0x06002FBF RID: 12223 RVA: 0x000BC405 File Offset: 0x000BA605
		public object UserToken { get; set; }

		/// <summary>The created and connected <see cref="T:System.Net.Sockets.Socket" /> object after successful completion of the <see cref="Overload:System.Net.Sockets.Socket.ConnectAsync" /> method.</summary>
		/// <returns>The connected <see cref="T:System.Net.Sockets.Socket" /> object.</returns>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000BC410 File Offset: 0x000BA610
		public Socket ConnectSocket
		{
			get
			{
				SocketError socketError = this.SocketError;
				if (socketError == SocketError.AccessDenied)
				{
					return null;
				}
				return this.current_socket;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000BC434 File Offset: 0x000BA634
		// (set) Token: 0x06002FC2 RID: 12226 RVA: 0x000BC43C File Offset: 0x000BA63C
		internal bool PolicyRestricted { get; private set; }

		/// <summary>The event used to complete an asynchronous operation.</summary>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06002FC3 RID: 12227 RVA: 0x000BC448 File Offset: 0x000BA648
		// (remove) Token: 0x06002FC4 RID: 12228 RVA: 0x000BC480 File Offset: 0x000BA680
		public event EventHandler<SocketAsyncEventArgs> Completed;

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000BC4B5 File Offset: 0x000BA6B5
		internal SocketAsyncEventArgs(bool policy)
			: this()
		{
			this.PolicyRestricted = policy;
		}

		/// <summary>Creates an empty <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The platform is not supported. </exception>
		// Token: 0x06002FC6 RID: 12230 RVA: 0x000BC4C4 File Offset: 0x000BA6C4
		public SocketAsyncEventArgs()
		{
			this.SendPacketsSendSize = -1;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000BC4E0 File Offset: 0x000BA6E0
		~SocketAsyncEventArgs()
		{
			this.Dispose(false);
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000BC510 File Offset: 0x000BA710
		private void Dispose(bool disposing)
		{
			this.disposed = true;
			if (disposing)
			{
				int num = this.in_progress;
				return;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance and optionally disposes of the managed resources.</summary>
		// Token: 0x06002FC9 RID: 12233 RVA: 0x000BC526 File Offset: 0x000BA726
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000BC535 File Offset: 0x000BA735
		internal void SetLastOperation(SocketAsyncOperation op)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("System.Net.Sockets.SocketAsyncEventArgs");
			}
			if (Interlocked.Exchange(ref this.in_progress, 1) != 0)
			{
				throw new InvalidOperationException("Operation already in progress");
			}
			this.LastOperation = op;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000BC56A File Offset: 0x000BA76A
		internal void Complete()
		{
			this.OnCompleted(this);
		}

		/// <summary>Represents a method that is called when an asynchronous operation completes.</summary>
		/// <param name="e">The event that is signaled.</param>
		// Token: 0x06002FCC RID: 12236 RVA: 0x000BC574 File Offset: 0x000BA774
		protected virtual void OnCompleted(SocketAsyncEventArgs e)
		{
			if (e == null)
			{
				return;
			}
			EventHandler<SocketAsyncEventArgs> completed = e.Completed;
			if (completed != null)
			{
				completed(e.current_socket, e);
			}
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x06002FCD RID: 12237 RVA: 0x000BC59C File Offset: 0x000BA79C
		public void SetBuffer(int offset, int count)
		{
			this.SetBuffer(this.Buffer, offset, count);
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="buffer">The data buffer to use with an asynchronous socket method.</param>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property is also not null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is also not null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x06002FCE RID: 12238 RVA: 0x000BC5AC File Offset: 0x000BA7AC
		public void SetBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer != null)
			{
				if (this.BufferList != null)
				{
					throw new ArgumentException("Buffer and BufferList properties cannot both be non-null.");
				}
				int num = buffer.Length;
				if (offset < 0 || (offset != 0 && offset >= num))
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (count < 0 || count > num - offset)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.Count = count;
				this.Offset = offset;
			}
			this.Buffer = buffer;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000BC613 File Offset: 0x000BA813
		internal void StartOperationCommon(Socket socket)
		{
			this.current_socket = socket;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000BC61C File Offset: 0x000BA81C
		internal void StartOperationWrapperConnect(MultipleConnectAsync args)
		{
			this.SetLastOperation(SocketAsyncOperation.Connect);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00004239 File Offset: 0x00002439
		internal void FinishConnectByNameSyncFailure(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x00004239 File Offset: 0x00002439
		internal void FinishOperationAsyncFailure(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000BC625 File Offset: 0x000BA825
		internal void FinishWrapperConnectSuccess(Socket connectSocket, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(SocketError.Success, bytesTransferred, flags);
			this.current_socket = connectSocket;
			this.Complete();
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000BC63D File Offset: 0x000BA83D
		internal void SetResults(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.SocketError = socketError;
			this.BytesTransferred = bytesTransferred;
			this.SocketFlags = flags;
		}

		/// <summary>Gets or sets the protocol to use to download the socket client access policy file. </summary>
		/// <returns>Returns <see cref="T:System.Net.Sockets.SocketClientAccessPolicyProtocol" />.The protocol to use to download the socket client access policy file.</returns>
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000BC654 File Offset: 0x000BA854
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public SocketClientAccessPolicyProtocol SocketClientAccessPolicyProtocol
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return SocketClientAccessPolicyProtocol.Tcp;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0400272A RID: 10026
		private bool disposed;

		// Token: 0x0400272B RID: 10027
		internal volatile int in_progress;

		// Token: 0x0400272C RID: 10028
		internal EndPoint remote_ep;

		// Token: 0x0400272D RID: 10029
		internal Socket current_socket;

		// Token: 0x0400272E RID: 10030
		internal SocketAsyncResult socket_async_result = new SocketAsyncResult();

		// Token: 0x04002732 RID: 10034
		internal IList<ArraySegment<byte>> m_BufferList;
	}
}

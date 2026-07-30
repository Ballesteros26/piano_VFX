using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	/// <summary>Provides a client for connecting to WebSocket services.</summary>
	// Token: 0x020006D3 RID: 1747
	public sealed class ClientWebSocket : WebSocket
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> class.</summary>
		// Token: 0x06003683 RID: 13955 RVA: 0x000C9248 File Offset: 0x000C7448
		public ClientWebSocket()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, ".ctor");
			}
			WebSocketHandle.CheckPlatformSupport();
			this._state = 0;
			this._options = new ClientWebSocketOptions();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(this, null, ".ctor");
			}
		}

		/// <summary>Gets the WebSocket options for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.ClientWebSocketOptions" />.The WebSocket options for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</returns>
		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06003684 RID: 13956 RVA: 0x000C9298 File Offset: 0x000C7498
		public ClientWebSocketOptions Options
		{
			get
			{
				return this._options;
			}
		}

		/// <summary>Gets the reason why the close handshake was initiated on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.The reason why the close handshake was initiated.</returns>
		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x000C92A0 File Offset: 0x000C74A0
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				if (WebSocketHandle.IsValid(this._innerWebSocket))
				{
					return this._innerWebSocket.CloseStatus;
				}
				return null;
			}
		}

		/// <summary>Gets a description of the reason why the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance was closed.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The description of the reason why the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance was closed.</returns>
		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06003686 RID: 13958 RVA: 0x000C92CF File Offset: 0x000C74CF
		public override string CloseStatusDescription
		{
			get
			{
				if (WebSocketHandle.IsValid(this._innerWebSocket))
				{
					return this._innerWebSocket.CloseStatusDescription;
				}
				return null;
			}
		}

		/// <summary>Gets the supported WebSocket sub-protocol for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The supported WebSocket sub-protocol.</returns>
		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x000C92EB File Offset: 0x000C74EB
		public override string SubProtocol
		{
			get
			{
				if (WebSocketHandle.IsValid(this._innerWebSocket))
				{
					return this._innerWebSocket.SubProtocol;
				}
				return null;
			}
		}

		/// <summary>Get the WebSocket state of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketState" />.The WebSocket state of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</returns>
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06003688 RID: 13960 RVA: 0x000C9308 File Offset: 0x000C7508
		public override WebSocketState State
		{
			get
			{
				if (WebSocketHandle.IsValid(this._innerWebSocket))
				{
					return this._innerWebSocket.State;
				}
				ClientWebSocket.InternalState state = (ClientWebSocket.InternalState)this._state;
				if (state == ClientWebSocket.InternalState.Created)
				{
					return WebSocketState.None;
				}
				if (state != ClientWebSocket.InternalState.Connecting)
				{
					return WebSocketState.Closed;
				}
				return WebSocketState.Connecting;
			}
		}

		/// <summary>Connect to a WebSocket server as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="uri">The URI of the WebSocket server to connect to.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that the  operation should be canceled.</param>
		// Token: 0x06003689 RID: 13961 RVA: 0x000C9344 File Offset: 0x000C7544
		public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new ArgumentException("This operation is not supported for a relative URI.", "uri");
			}
			if (uri.Scheme != "ws" && uri.Scheme != "wss")
			{
				throw new ArgumentException("Only Uris starting with 'ws://' or 'wss://' are supported.", "uri");
			}
			ClientWebSocket.InternalState internalState = (ClientWebSocket.InternalState)Interlocked.CompareExchange(ref this._state, 1, 0);
			if (internalState == ClientWebSocket.InternalState.Disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (internalState != ClientWebSocket.InternalState.Created)
			{
				throw new InvalidOperationException("The WebSocket has already been started.");
			}
			this._options.SetToReadOnly();
			return this.ConnectAsyncCore(uri, cancellationToken);
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000C93F8 File Offset: 0x000C75F8
		private async Task ConnectAsyncCore(Uri uri, CancellationToken cancellationToken)
		{
			this._innerWebSocket = WebSocketHandle.Create();
			try
			{
				if (Interlocked.CompareExchange(ref this._state, 2, 1) != 1)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				await this._innerWebSocket.ConnectAsyncCore(uri, cancellationToken, this._options).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "ConnectAsyncCore");
				}
				throw;
			}
		}

		/// <summary>Send data on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="buffer">The buffer containing the message to be sent.</param>
		/// <param name="messageType">Specifies whether the buffer is clear text or in a binary format.</param>
		/// <param name="endOfMessage">Specifies whether this is the final asynchronous send. Set to true if this is the final send; false otherwise.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x0600368B RID: 13963 RVA: 0x000C944D File Offset: 0x000C764D
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		/// <summary>Receive data on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="buffer">The buffer to receive the response.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x0600368C RID: 13964 RVA: 0x000C9465 File Offset: 0x000C7665
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.ReceiveAsync(buffer, cancellationToken);
		}

		/// <summary>Close the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="closeStatus">The WebSocket close status.</param>
		/// <param name="statusDescription">A description of the close status.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x0600368D RID: 13965 RVA: 0x000C947A File Offset: 0x000C767A
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
		}

		/// <summary>Close the output for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="closeStatus">The WebSocket close status.</param>
		/// <param name="statusDescription">A description of the close status.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x0600368E RID: 13966 RVA: 0x000C9490 File Offset: 0x000C7690
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
		}

		/// <summary>Aborts the connection and cancels any pending IO operations.</summary>
		// Token: 0x0600368F RID: 13967 RVA: 0x000C94A6 File Offset: 0x000C76A6
		public override void Abort()
		{
			if (this._state == 3)
			{
				return;
			}
			if (WebSocketHandle.IsValid(this._innerWebSocket))
			{
				this._innerWebSocket.Abort();
			}
			this.Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		// Token: 0x06003690 RID: 13968 RVA: 0x000C94D0 File Offset: 0x000C76D0
		public override void Dispose()
		{
			if (Interlocked.Exchange(ref this._state, 3) == 3)
			{
				return;
			}
			if (WebSocketHandle.IsValid(this._innerWebSocket))
			{
				this._innerWebSocket.Dispose();
			}
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000C94FA File Offset: 0x000C76FA
		private void ThrowIfNotConnected()
		{
			if (this._state == 3)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this._state != 2)
			{
				throw new InvalidOperationException("The WebSocket is not connected.");
			}
		}

		// Token: 0x04002B6C RID: 11116
		private readonly ClientWebSocketOptions _options;

		// Token: 0x04002B6D RID: 11117
		private WebSocketHandle _innerWebSocket;

		// Token: 0x04002B6E RID: 11118
		private int _state;

		// Token: 0x020006D4 RID: 1748
		private enum InternalState
		{
			// Token: 0x04002B70 RID: 11120
			Created,
			// Token: 0x04002B71 RID: 11121
			Connecting,
			// Token: 0x04002B72 RID: 11122
			Connected,
			// Token: 0x04002B73 RID: 11123
			Disposed
		}
	}
}

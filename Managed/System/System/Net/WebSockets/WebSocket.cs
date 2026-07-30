using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	/// <summary>The WebSocket class allows applications to send and receive data after the WebSocket upgrade has completed.</summary>
	// Token: 0x020006DE RID: 1758
	public abstract class WebSocket : IDisposable
	{
		/// <summary>Indicates the reason why the remote endpoint initiated the close handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.</returns>
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060036D1 RID: 14033
		public abstract WebSocketCloseStatus? CloseStatus { get; }

		/// <summary>Allows the remote endpoint to describe the reason why the connection was closed.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060036D2 RID: 14034
		public abstract string CloseStatusDescription { get; }

		/// <summary>The subprotocol that was negotiated during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060036D3 RID: 14035
		public abstract string SubProtocol { get; }

		/// <summary>Returns the current state of the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketState" />.</returns>
		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060036D4 RID: 14036
		public abstract WebSocketState State { get; }

		/// <summary>Aborts the WebSocket connection and cancels any pending IO operations.</summary>
		// Token: 0x060036D5 RID: 14037
		public abstract void Abort();

		/// <summary>Closes the WebSocket connection as an asynchronous operation using the close handshake defined in the WebSocket protocol specification section 7.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
		/// <param name="closeStatus">Indicates the reason for closing the WebSocket connection.</param>
		/// <param name="statusDescription">Specifies a human readable explanation as to why the connection is closed.</param>
		/// <param name="cancellationToken">The token that can be used to propagate notification that operations should be canceled.</param>
		// Token: 0x060036D6 RID: 14038
		public abstract Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		/// <summary>Initiates or completes the close handshake defined in the WebSocket protocol specification section 7.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
		/// <param name="closeStatus">Indicates the reason for closing the WebSocket connection.</param>
		/// <param name="statusDescription">Allows applications to specify a human readable explanation as to why the connection is closed.</param>
		/// <param name="cancellationToken">The token that can be used to propagate notification that operations should be canceled.</param>
		// Token: 0x060036D7 RID: 14039
		public abstract Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		/// <summary>Used to clean up unmanaged resources for ASP.NET and self-hosted implementations.</summary>
		// Token: 0x060036D8 RID: 14040
		public abstract void Dispose();

		/// <summary>Receives data from the WebSocket connection asynchronously.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the received data.</returns>
		/// <param name="buffer">References the application buffer that is the storage location for the received data.</param>
		/// <param name="cancellationToken">Propagate the notification that operations should be canceled.</param>
		// Token: 0x060036D9 RID: 14041
		public abstract Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);

		/// <summary>Sends data over the WebSocket connection asynchronously.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
		/// <param name="buffer">The buffer to be sent over the connection.</param>
		/// <param name="messageType">Indicates whether the application is sending a binary or text message.</param>
		/// <param name="endOfMessage">Indicates whether the data in “buffer” is the last part of a message.</param>
		/// <param name="cancellationToken">The token that propagates the notification that operations should be canceled.</param>
		// Token: 0x060036DA RID: 14042
		public abstract Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);

		/// <summary>Gets the default WebSocket protocol keep-alive interval in milliseconds.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The default WebSocket protocol keep-alive interval in milliseconds. The typical value for this interval is 30 seconds.</returns>
		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000CAC8A File Offset: 0x000C8E8A
		public static TimeSpan DefaultKeepAliveInterval
		{
			get
			{
				return TimeSpan.FromSeconds(30.0);
			}
		}

		/// <summary>Verifies that the connection is in an expected state.</summary>
		/// <param name="state">The current state of the WebSocket to be tested against the list of valid states.</param>
		/// <param name="validStates">List of valid connection states.</param>
		// Token: 0x060036DC RID: 14044 RVA: 0x000CAC9C File Offset: 0x000C8E9C
		protected static void ThrowOnInvalidState(WebSocketState state, params WebSocketState[] validStates)
		{
			string text = string.Empty;
			if (validStates != null && validStates.Length != 0)
			{
				foreach (WebSocketState webSocketState in validStates)
				{
					if (state == webSocketState)
					{
						return;
					}
				}
				text = string.Join<WebSocketState>(", ", validStates);
			}
			throw new WebSocketException(global::SR.Format("The WebSocket is in an invalid state ('{0}') for this operation. Valid states are: '{1}'", state, text));
		}

		/// <summary>Returns a value that indicates if the state of the WebSocket instance is closed or aborted.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="T:System.Net.WebSockets.WebSocket" /> is closed or aborted; otherwise false.</returns>
		/// <param name="state">The current state of the WebSocket.</param>
		// Token: 0x060036DD RID: 14045 RVA: 0x000CACF1 File Offset: 0x000C8EF1
		protected static bool IsStateTerminal(WebSocketState state)
		{
			return state == WebSocketState.Closed || state == WebSocketState.Aborted;
		}

		/// <summary>Create client buffers to use with this <see cref="T:System.Net.WebSockets.WebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.ArraySegment`1" />.An array with the client buffers.</returns>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the send buffer.</param>
		// Token: 0x060036DE RID: 14046 RVA: 0x000CAD00 File Offset: 0x000C8F00
		public static ArraySegment<byte> CreateClientBuffer(int receiveBufferSize, int sendBufferSize)
		{
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, global::SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, global::SR.Format("The argument must be a value greater than {0}.", 1));
			}
			return new ArraySegment<byte>(new byte[Math.Max(receiveBufferSize, sendBufferSize)]);
		}

		/// <summary>Creates a WebSocket server buffer.</summary>
		/// <returns>Returns <see cref="T:System.ArraySegment`1" />.</returns>
		/// <param name="receiveBufferSize">The size, in bytes, of the desired buffer.</param>
		// Token: 0x060036DF RID: 14047 RVA: 0x000CAD68 File Offset: 0x000C8F68
		public static ArraySegment<byte> CreateServerBuffer(int receiveBufferSize)
		{
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, global::SR.Format("The argument must be a value greater than {0}.", 1));
			}
			return new ArraySegment<byte>(new byte[receiveBufferSize]);
		}

		/// <summary>Returns a value that indicates if the WebSocket instance is targeting .NET Framework 4.5.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="T:System.Net.WebSockets.WebSocket" /> is targeting .NET Framework 4.5; otherwise false.</returns>
		// Token: 0x060036E0 RID: 14048 RVA: 0x000027E2 File Offset: 0x000009E2
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsApplicationTargeting45()
		{
			return true;
		}

		/// <summary>This API supports the .NET Framework infrastructure and is not intended to be used directly from your code. Allows callers to register prefixes for WebSocket requests (ws and wss).</summary>
		// Token: 0x060036E1 RID: 14049 RVA: 0x0000F3CE File Offset: 0x0000D5CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterPrefixes()
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.Allows callers to create a client side WebSocket class which will use the WSPC for framing purposes.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.</returns>
		/// <param name="innerStream">The connection to be used for IO operations.</param>
		/// <param name="subProtocol">The subprotocol accepted by the client.</param>
		/// <param name="receiveBufferSize">The size in bytes of the client WebSocket receive buffer.</param>
		/// <param name="sendBufferSize">The size in bytes of the client WebSocket send buffer.</param>
		/// <param name="keepAliveInterval">Determines how regularly a frame is sent over the connection as a keep-alive. Applies only when the connection is idle.</param>
		/// <param name="useZeroMaskingKey">Indicates whether a random key or a static key (just zeros) should be used for the WebSocket masking.</param>
		/// <param name="internalBuffer">Will be used as the internal buffer in the WPC. The size has to be at least 2 * ReceiveBufferSize + SendBufferSize + 256 + 20 (16 on 32-bit).</param>
		// Token: 0x060036E2 RID: 14050 RVA: 0x000CAD9C File Offset: 0x000C8F9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static WebSocket CreateClientWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval, bool useZeroMaskingKey, ArraySegment<byte> internalBuffer)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (!innerStream.CanRead || !innerStream.CanWrite)
			{
				throw new ArgumentException((!innerStream.CanRead) ? "The base stream is not readable." : "The base stream is not writeable.", "innerStream");
			}
			if (subProtocol != null)
			{
				WebSocketValidate.ValidateSubprotocol(subProtocol);
			}
			if (keepAliveInterval != Timeout.InfiniteTimeSpan && keepAliveInterval < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("keepAliveInterval", keepAliveInterval, global::SR.Format("The argument must be a value greater than {0}.", 0));
			}
			if (receiveBufferSize <= 0 || sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException((receiveBufferSize <= 0) ? "receiveBufferSize" : "sendBufferSize", (receiveBufferSize <= 0) ? receiveBufferSize : sendBufferSize, global::SR.Format("The argument must be a value greater than {0}.", 0));
			}
			return ManagedWebSocket.CreateFromConnectedStream(innerStream, false, subProtocol, keepAliveInterval, receiveBufferSize, new ArraySegment<byte>?(internalBuffer));
		}
	}
}

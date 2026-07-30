using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace System.Web.WebSockets
{
	/// <summary>Represents a real-time full-duplex connection between a web server and a client in an ASP.NET application.</summary>
	// Token: 0x020006E7 RID: 1767
	public sealed class AspNetWebSocket : WebSocket
	{
		// Token: 0x06004ACE RID: 19150 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal AspNetWebSocket()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a status code that indicates why an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object was closed.</summary>
		/// <returns>The status code.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x000CA8DC File Offset: 0x000C8ADC
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a status message that explains why an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object was closed.</summary>
		/// <returns>The status message.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06004AD0 RID: 19152 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string CloseStatusDescription
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates the open or closed state of an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object.</summary>
		/// <returns>The current state.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06004AD1 RID: 19153 RVA: 0x000CA8F8 File Offset: 0x000C8AF8
		public override WebSocketState State
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return WebSocketState.None;
			}
		}

		/// <summary>Gets the name of an application-specific protocol that a remote client and a server can use to exchange data over an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The name of the protocol.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06004AD2 RID: 19154 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string SubProtocol
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Cancels any pending I/O operations on the <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object and sets the state of the object so that it cannot be used to start additional I/O operations.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x06004AD3 RID: 19155 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Abort()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sends an asynchronous message to a client to close the connection. If the server initiates the request to close the connection, the method waits for the client to acknowledge the request before it returns.</summary>
		/// <returns>A reference to the operation.</returns>
		/// <param name="closeStatus">The status code of the close operation.</param>
		/// <param name="statusDescription">The status message of the close operation.</param>
		/// <param name="cancellationToken">The object that cancels a pending operation.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object was previously disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is in an aborted state.-or-Sending operations are unavailable.-or-Receiving operations are unavailable.</exception>
		// Token: 0x06004AD4 RID: 19156 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Sends an asynchronous message to a client to close the connection. If the server initiates the request to close the connection, the method returns without waiting for a response.</summary>
		/// <returns>A reference to the operation.</returns>
		/// <param name="closeStatus">The status code of the close operation.</param>
		/// <param name="statusDescription">The status message of the close operation.</param>
		/// <param name="cancellationToken">The object that cancels a pending operation.</param>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is in an aborted state.-or-Sending operations are unavailable.</exception>
		// Token: 0x06004AD5 RID: 19157 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Releases all resources used by an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object.</summary>
		// Token: 0x06004AD6 RID: 19158 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Receives a single message fragment from a remote client.</summary>
		/// <returns>A reference to the task of receiving a message.</returns>
		/// <param name="buffer">The array that contains the message data.</param>
		/// <param name="cancellationToken">The object that cancels a pending operation.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object was previously disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is in an aborted state.-or-Receiving operations are unavailable.</exception>
		// Token: 0x06004AD7 RID: 19159 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Sends a single message fragment to a remote client.</summary>
		/// <returns>A reference to the task of sending a message.</returns>
		/// <param name="buffer">The array that contains the message data.</param>
		/// <param name="messageType">The message type.</param>
		/// <param name="endOfMessage">true to indicate that a fragment is the end of a complete message; otherwise, false.</param>
		/// <param name="cancellationToken">The object that cancels a pending operation.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is in an aborted state.-or-Sending operations are unavailable.</exception>
		// Token: 0x06004AD8 RID: 19160 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}

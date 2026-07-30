using System;

namespace System.Net.WebSockets
{
	/// <summary>An instance of this class represents the result of performing a single ReceiveAsync operation on a WebSocket.</summary>
	// Token: 0x020006E4 RID: 1764
	public class WebSocketReceiveResult
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		// Token: 0x06003705 RID: 14085 RVA: 0x000CB064 File Offset: 0x000C9264
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage)
			: this(count, messageType, endOfMessage, null, null)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		/// <param name="closeStatus">Indicates the <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" /> of the connection.</param>
		/// <param name="closeStatusDescription">The description of <paramref name="closeStatus" />.</param>
		// Token: 0x06003706 RID: 14086 RVA: 0x000CB084 File Offset: 0x000C9284
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeStatusDescription)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Count = count;
			this.EndOfMessage = endOfMessage;
			this.MessageType = messageType;
			this.CloseStatus = closeStatus;
			this.CloseStatusDescription = closeStatusDescription;
		}

		/// <summary>Indicates the number of bytes that the WebSocket received.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x000CB0C0 File Offset: 0x000C92C0
		public int Count { get; }

		/// <summary>Indicates whether the message has been received completely.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000CB0C8 File Offset: 0x000C92C8
		public bool EndOfMessage { get; }

		/// <summary>Indicates whether the current message is a UTF-8 message or a binary message.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketMessageType" />.</returns>
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x000CB0D0 File Offset: 0x000C92D0
		public WebSocketMessageType MessageType { get; }

		/// <summary>Indicates the reason why the remote endpoint initiated the close handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.</returns>
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000CB0D8 File Offset: 0x000C92D8
		public WebSocketCloseStatus? CloseStatus { get; }

		/// <summary>Returns the optional description that describes why the close handshake has been initiated by the remote endpoint.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600370B RID: 14091 RVA: 0x000CB0E0 File Offset: 0x000C92E0
		public string CloseStatusDescription { get; }
	}
}

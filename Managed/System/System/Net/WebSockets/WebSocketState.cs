using System;

namespace System.Net.WebSockets
{
	/// <summary> Defines the different states a WebSockets instance can be in.</summary>
	// Token: 0x020006E5 RID: 1765
	public enum WebSocketState
	{
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002BE2 RID: 11234
		None,
		/// <summary>The connection is negotiating the handshake with the remote endpoint.</summary>
		// Token: 0x04002BE3 RID: 11235
		Connecting,
		/// <summary>The initial state after the HTTP handshake has been completed.</summary>
		// Token: 0x04002BE4 RID: 11236
		Open,
		/// <summary>A close message was sent to the remote endpoint.</summary>
		// Token: 0x04002BE5 RID: 11237
		CloseSent,
		/// <summary>A close message was received from the remote endpoint.</summary>
		// Token: 0x04002BE6 RID: 11238
		CloseReceived,
		/// <summary>Indicates the WebSocket close handshake completed gracefully.</summary>
		// Token: 0x04002BE7 RID: 11239
		Closed,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002BE8 RID: 11240
		Aborted
	}
}

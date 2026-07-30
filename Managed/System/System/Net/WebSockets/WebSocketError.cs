using System;

namespace System.Net.WebSockets
{
	/// <summary>Contains the list of possible WebSocket errors.</summary>
	// Token: 0x020006E1 RID: 1761
	public enum WebSocketError
	{
		/// <summary>Indicates that there was no native error information for the exception.</summary>
		// Token: 0x04002BCD RID: 11213
		Success,
		/// <summary>Indicates that a WebSocket frame with an unknown opcode was received.</summary>
		// Token: 0x04002BCE RID: 11214
		InvalidMessageType,
		/// <summary>Indicates a general error.</summary>
		// Token: 0x04002BCF RID: 11215
		Faulted,
		/// <summary>Indicates that an unknown native error occurred.</summary>
		// Token: 0x04002BD0 RID: 11216
		NativeError,
		/// <summary>Indicates that the incoming request was not a valid websocket request.</summary>
		// Token: 0x04002BD1 RID: 11217
		NotAWebSocket,
		/// <summary>Indicates that the client requested an unsupported version of the WebSocket protocol.</summary>
		// Token: 0x04002BD2 RID: 11218
		UnsupportedVersion,
		/// <summary>Indicates that the client requested an unsupported WebSocket subprotocol.</summary>
		// Token: 0x04002BD3 RID: 11219
		UnsupportedProtocol,
		/// <summary>Indicates an error occurred when parsing the HTTP headers during the opening handshake.</summary>
		// Token: 0x04002BD4 RID: 11220
		HeaderError,
		/// <summary>Indicates that the connection was terminated unexpectedly.</summary>
		// Token: 0x04002BD5 RID: 11221
		ConnectionClosedPrematurely,
		/// <summary>Indicates the WebSocket is an invalid state for the given operation (such as being closed or aborted).</summary>
		// Token: 0x04002BD6 RID: 11222
		InvalidState
	}
}

using System;

namespace System.Net.WebSockets
{
	/// <summary>Represents well known WebSocket close codes as defined in section 11.7 of the WebSocket protocol spec.</summary>
	// Token: 0x020006DF RID: 1759
	public enum WebSocketCloseStatus
	{
		/// <summary>(1000) The connection has closed after the request was fulfilled.</summary>
		// Token: 0x04002BC2 RID: 11202
		NormalClosure = 1000,
		/// <summary>(1001) Indicates an endpoint is being removed. Either the server or client will become unavailable.</summary>
		// Token: 0x04002BC3 RID: 11203
		EndpointUnavailable,
		/// <summary>(1002) The client or server is terminating the connection because of a protocol error.</summary>
		// Token: 0x04002BC4 RID: 11204
		ProtocolError,
		/// <summary>(1003) The client or server is terminating the connection because it cannot accept the data type it received.</summary>
		// Token: 0x04002BC5 RID: 11205
		InvalidMessageType,
		/// <summary>No error specified.</summary>
		// Token: 0x04002BC6 RID: 11206
		Empty = 1005,
		/// <summary>(1007) The client or server is terminating the connection because it has received data inconsistent with the message type.</summary>
		// Token: 0x04002BC7 RID: 11207
		InvalidPayloadData = 1007,
		/// <summary>(1008) The connection will be closed because an endpoint has received a message that violates its policy.</summary>
		// Token: 0x04002BC8 RID: 11208
		PolicyViolation,
		/// <summary>(1004) Reserved for future use.</summary>
		// Token: 0x04002BC9 RID: 11209
		MessageTooBig,
		/// <summary>(1010) The client is terminating the connection because it expected the server to negotiate an extension.</summary>
		// Token: 0x04002BCA RID: 11210
		MandatoryExtension,
		/// <summary>The connection will be closed by the server because of an error on the server.</summary>
		// Token: 0x04002BCB RID: 11211
		InternalServerError
	}
}

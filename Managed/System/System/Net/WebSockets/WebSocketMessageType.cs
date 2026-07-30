using System;

namespace System.Net.WebSockets
{
	/// <summary>Indicates the message type.</summary>
	// Token: 0x020006E3 RID: 1763
	public enum WebSocketMessageType
	{
		/// <summary>The message is clear text.</summary>
		// Token: 0x04002BD9 RID: 11225
		Text,
		/// <summary>The message is in binary format.</summary>
		// Token: 0x04002BDA RID: 11226
		Binary,
		/// <summary>A receive has completed because a close message was received.</summary>
		// Token: 0x04002BDB RID: 11227
		Close
	}
}

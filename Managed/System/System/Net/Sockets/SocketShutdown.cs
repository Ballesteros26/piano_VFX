using System;

namespace System.Net.Sockets
{
	/// <summary>Defines constants that are used by the <see cref="M:System.Net.Sockets.Socket.Shutdown(System.Net.Sockets.SocketShutdown)" /> method.</summary>
	// Token: 0x020005D3 RID: 1491
	public enum SocketShutdown
	{
		/// <summary>Disables a <see cref="T:System.Net.Sockets.Socket" /> for receiving. This field is constant.</summary>
		// Token: 0x040026EB RID: 9963
		Receive,
		/// <summary>Disables a <see cref="T:System.Net.Sockets.Socket" /> for sending. This field is constant.</summary>
		// Token: 0x040026EC RID: 9964
		Send,
		/// <summary>Disables a <see cref="T:System.Net.Sockets.Socket" /> for both sending and receiving. This field is constant.</summary>
		// Token: 0x040026ED RID: 9965
		Both
	}
}

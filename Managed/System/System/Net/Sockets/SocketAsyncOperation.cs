using System;

namespace System.Net.Sockets
{
	/// <summary>The type of asynchronous socket operation most recently performed with this context object.</summary>
	// Token: 0x020005CA RID: 1482
	public enum SocketAsyncOperation
	{
		/// <summary>None of the socket operations.</summary>
		// Token: 0x04002660 RID: 9824
		None,
		/// <summary>A socket Accept operation. </summary>
		// Token: 0x04002661 RID: 9825
		Accept,
		/// <summary>A socket Connect operation.</summary>
		// Token: 0x04002662 RID: 9826
		Connect,
		/// <summary>A socket Disconnect operation.</summary>
		// Token: 0x04002663 RID: 9827
		Disconnect,
		/// <summary>A socket Receive operation.</summary>
		// Token: 0x04002664 RID: 9828
		Receive,
		/// <summary>A socket ReceiveFrom operation.</summary>
		// Token: 0x04002665 RID: 9829
		ReceiveFrom,
		/// <summary>A socket ReceiveMessageFrom operation.</summary>
		// Token: 0x04002666 RID: 9830
		ReceiveMessageFrom,
		/// <summary>A socket Send operation.</summary>
		// Token: 0x04002667 RID: 9831
		Send,
		/// <summary>A socket SendPackets operation.</summary>
		// Token: 0x04002668 RID: 9832
		SendPackets,
		/// <summary>A socket SendTo operation.</summary>
		// Token: 0x04002669 RID: 9833
		SendTo
	}
}

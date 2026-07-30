using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the states of a Transmission Control Protocol (TCP) connection.</summary>
	// Token: 0x0200061E RID: 1566
	public enum TcpState
	{
		/// <summary>The TCP connection state is unknown.</summary>
		// Token: 0x04002824 RID: 10276
		Unknown,
		/// <summary>The TCP connection is closed.</summary>
		// Token: 0x04002825 RID: 10277
		Closed,
		/// <summary>The local endpoint of the TCP connection is listening for a connection request from any remote endpoint.</summary>
		// Token: 0x04002826 RID: 10278
		Listen,
		/// <summary>The local endpoint of the TCP connection has sent the remote endpoint a segment header with the synchronize (SYN) control bit set and is waiting for a matching connection request.</summary>
		// Token: 0x04002827 RID: 10279
		SynSent,
		/// <summary>The local endpoint of the TCP connection has sent and received a connection request and is waiting for an acknowledgment.</summary>
		// Token: 0x04002828 RID: 10280
		SynReceived,
		/// <summary>The TCP handshake is complete. The connection has been established and data can be sent.</summary>
		// Token: 0x04002829 RID: 10281
		Established,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint or for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x0400282A RID: 10282
		FinWait1,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint.</summary>
		// Token: 0x0400282B RID: 10283
		FinWait2,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the local user.</summary>
		// Token: 0x0400282C RID: 10284
		CloseWait,
		/// <summary>The local endpoint of the TCP connection is waiting for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x0400282D RID: 10285
		Closing,
		/// <summary>The local endpoint of the TCP connection is waiting for the final acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x0400282E RID: 10286
		LastAck,
		/// <summary>The local endpoint of the TCP connection is waiting for enough time to pass to ensure that the remote endpoint received the acknowledgement of its connection termination request.</summary>
		// Token: 0x0400282F RID: 10287
		TimeWait,
		/// <summary>The transmission control buffer (TCB) for the TCP connection is being deleted.</summary>
		// Token: 0x04002830 RID: 10288
		DeleteTcb
	}
}

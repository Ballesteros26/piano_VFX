using System;

namespace System.Net.Sockets
{
	// Token: 0x020005E5 RID: 1509
	public struct SocketReceiveMessageFromResult
	{
		// Token: 0x04002764 RID: 10084
		public int ReceivedBytes;

		// Token: 0x04002765 RID: 10085
		public SocketFlags SocketFlags;

		// Token: 0x04002766 RID: 10086
		public EndPoint RemoteEndPoint;

		// Token: 0x04002767 RID: 10087
		public IPPacketInformation PacketInformation;
	}
}

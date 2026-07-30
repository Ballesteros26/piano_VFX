using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020007A0 RID: 1952
	internal class AsyncRequest
	{
		// Token: 0x06004FC5 RID: 20421 RVA: 0x0011EEFF File Offset: 0x0011D0FF
		public AsyncRequest(IMessage msgRequest, IMessageSink replySink)
		{
			this.ReplySink = replySink;
			this.MsgRequest = msgRequest;
		}

		// Token: 0x04002A5D RID: 10845
		internal IMessageSink ReplySink;

		// Token: 0x04002A5E RID: 10846
		internal IMessage MsgRequest;
	}
}

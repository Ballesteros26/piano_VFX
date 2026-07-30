using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000799 RID: 1945
	internal class ChanelSinkStackEntry
	{
		// Token: 0x06004F9F RID: 20383 RVA: 0x0011E977 File Offset: 0x0011CB77
		public ChanelSinkStackEntry(IChannelSinkBase sink, object state, ChanelSinkStackEntry next)
		{
			this.Sink = sink;
			this.State = state;
			this.Next = next;
		}

		// Token: 0x04002A4E RID: 10830
		public IChannelSinkBase Sink;

		// Token: 0x04002A4F RID: 10831
		public object State;

		// Token: 0x04002A50 RID: 10832
		public ChanelSinkStackEntry Next;
	}
}

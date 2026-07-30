using System;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000805 RID: 2053
	[Serializable]
	internal class EnvoyTerminatorSink : IMessageSink
	{
		// Token: 0x06005230 RID: 21040 RVA: 0x00122AC5 File Offset: 0x00120CC5
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().SyncProcessMessage(msg);
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x00122AD7 File Offset: 0x00120CD7
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06005232 RID: 21042 RVA: 0x0000A42E File Offset: 0x0000862E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002B03 RID: 11011
		public static EnvoyTerminatorSink Instance = new EnvoyTerminatorSink();
	}
}

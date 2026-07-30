using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000798 RID: 1944
	internal class ExceptionFilterSink : IMessageSink
	{
		// Token: 0x06004F9B RID: 20379 RVA: 0x0011E940 File Offset: 0x0011CB40
		public ExceptionFilterSink(IMessage call, IMessageSink next)
		{
			this._call = call;
			this._next = next;
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x0011E956 File Offset: 0x0011CB56
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return this._next.SyncProcessMessage(ChannelServices.CheckReturnMessage(this._call, msg));
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x0007EA26 File Offset: 0x0007CC26
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06004F9E RID: 20382 RVA: 0x0011E96F File Offset: 0x0011CB6F
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x04002A4C RID: 10828
		private IMessageSink _next;

		// Token: 0x04002A4D RID: 10829
		private IMessage _call;
	}
}

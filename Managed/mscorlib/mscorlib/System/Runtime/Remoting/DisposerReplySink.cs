using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000762 RID: 1890
	internal class DisposerReplySink : IMessageSink
	{
		// Token: 0x06004E21 RID: 20001 RVA: 0x0011A5C1 File Offset: 0x001187C1
		public DisposerReplySink(IMessageSink next, IDisposable disposable)
		{
			this._next = next;
			this._disposable = disposable;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x0011A5D7 File Offset: 0x001187D7
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._disposable.Dispose();
			return this._next.SyncProcessMessage(msg);
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x00014B5A File Offset: 0x00012D5A
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06004E24 RID: 20004 RVA: 0x0011A5F0 File Offset: 0x001187F0
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x040029D5 RID: 10709
		private IMessageSink _next;

		// Token: 0x040029D6 RID: 10710
		private IDisposable _disposable;
	}
}

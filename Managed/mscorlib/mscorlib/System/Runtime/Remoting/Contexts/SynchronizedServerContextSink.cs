using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200078F RID: 1935
	internal class SynchronizedServerContextSink : IMessageSink
	{
		// Token: 0x06004F4B RID: 20299 RVA: 0x0011D8A8 File Offset: 0x0011BAA8
		public SynchronizedServerContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x0011D8BE File Offset: 0x0011BABE
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0011D8C6 File Offset: 0x0011BAC6
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this._att.AcquireLock();
			replySink = new SynchronizedContextReplySink(replySink, this._att, false);
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x0011D8F0 File Offset: 0x0011BAF0
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._att.AcquireLock();
			IMessage message;
			try
			{
				message = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				this._att.ReleaseLock();
			}
			return message;
		}

		// Token: 0x04002A38 RID: 10808
		private IMessageSink _next;

		// Token: 0x04002A39 RID: 10809
		private SynchronizationAttribute _att;
	}
}

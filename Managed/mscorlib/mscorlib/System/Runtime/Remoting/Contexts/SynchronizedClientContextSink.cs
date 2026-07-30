using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200078E RID: 1934
	internal class SynchronizedClientContextSink : IMessageSink
	{
		// Token: 0x06004F47 RID: 20295 RVA: 0x0011D7F2 File Offset: 0x0011B9F2
		public SynchronizedClientContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x0011D808 File Offset: 0x0011BA08
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x0011D810 File Offset: 0x0011BA10
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
				replySink = new SynchronizedContextReplySink(replySink, this._att, true);
			}
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x0011D848 File Offset: 0x0011BA48
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
			}
			IMessage message;
			try
			{
				message = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				if (this._att.IsReEntrant)
				{
					this._att.AcquireLock();
				}
			}
			return message;
		}

		// Token: 0x04002A36 RID: 10806
		private IMessageSink _next;

		// Token: 0x04002A37 RID: 10807
		private SynchronizationAttribute _att;
	}
}

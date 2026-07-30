using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000790 RID: 1936
	internal class SynchronizedContextReplySink : IMessageSink
	{
		// Token: 0x06004F4F RID: 20303 RVA: 0x0011D934 File Offset: 0x0011BB34
		public SynchronizedContextReplySink(IMessageSink next, SynchronizationAttribute att, bool newLock)
		{
			this._newLock = newLock;
			this._next = next;
			this._att = att;
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06004F50 RID: 20304 RVA: 0x0011D951 File Offset: 0x0011BB51
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x00014B5A File Offset: 0x00012D5A
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x0011D95C File Offset: 0x0011BB5C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._newLock)
			{
				this._att.AcquireLock();
			}
			else
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
				if (this._newLock)
				{
					this._att.ReleaseLock();
				}
			}
			return message;
		}

		// Token: 0x04002A3A RID: 10810
		private IMessageSink _next;

		// Token: 0x04002A3B RID: 10811
		private bool _newLock;

		// Token: 0x04002A3C RID: 10812
		private SynchronizationAttribute _att;
	}
}

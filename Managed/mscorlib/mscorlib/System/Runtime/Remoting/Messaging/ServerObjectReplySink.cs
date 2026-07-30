using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000827 RID: 2087
	internal class ServerObjectReplySink : IMessageSink
	{
		// Token: 0x06005372 RID: 21362 RVA: 0x001256A9 File Offset: 0x001238A9
		public ServerObjectReplySink(ServerIdentity identity, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._identity = identity;
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x001256BF File Offset: 0x001238BF
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._identity.NotifyServerDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x00014B5A File Offset: 0x00012D5A
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06005375 RID: 21365 RVA: 0x001256DC File Offset: 0x001238DC
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x04002B63 RID: 11107
		private IMessageSink _replySink;

		// Token: 0x04002B64 RID: 11108
		private ServerIdentity _identity;
	}
}

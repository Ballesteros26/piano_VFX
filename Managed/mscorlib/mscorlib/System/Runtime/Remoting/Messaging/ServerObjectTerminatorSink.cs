using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000826 RID: 2086
	internal class ServerObjectTerminatorSink : IMessageSink
	{
		// Token: 0x0600536E RID: 21358 RVA: 0x00125602 File Offset: 0x00123802
		public ServerObjectTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x00125614 File Offset: 0x00123814
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			serverIdentity.NotifyServerDynamicSinks(true, msg, false, false);
			IMessage message = this._nextSink.SyncProcessMessage(msg);
			serverIdentity.NotifyServerDynamicSinks(false, msg, false, false);
			return message;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x00125650 File Offset: 0x00123850
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			if (serverIdentity.HasServerDynamicSinks)
			{
				serverIdentity.NotifyServerDynamicSinks(true, msg, false, true);
				if (replySink != null)
				{
					replySink = new ServerObjectReplySink(serverIdentity, replySink);
				}
			}
			IMessageCtrl messageCtrl = this._nextSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null)
			{
				serverIdentity.NotifyServerDynamicSinks(false, msg, true, true);
			}
			return messageCtrl;
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06005371 RID: 21361 RVA: 0x001256A1 File Offset: 0x001238A1
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04002B62 RID: 11106
		private IMessageSink _nextSink;
	}
}

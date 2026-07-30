using System;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000801 RID: 2049
	internal class ClientContextReplySink : IMessageSink
	{
		// Token: 0x06005210 RID: 21008 RVA: 0x0012267F File Offset: 0x0012087F
		public ClientContextReplySink(Context ctx, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._context = ctx;
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00122695 File Offset: 0x00120895
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(false, msg, true, true);
			this._context.NotifyDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x00014B5A File Offset: 0x00012D5A
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06005213 RID: 21011 RVA: 0x001226BB File Offset: 0x001208BB
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x04002AF9 RID: 11001
		private IMessageSink _replySink;

		// Token: 0x04002AFA RID: 11002
		private Context _context;
	}
}

using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000800 RID: 2048
	internal class ClientContextTerminatorSink : IMessageSink
	{
		// Token: 0x0600520C RID: 21004 RVA: 0x0012257B File Offset: 0x0012077B
		public ClientContextTerminatorSink(Context ctx)
		{
			this._context = ctx;
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0012258C File Offset: 0x0012078C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(true, msg, true, false);
			this._context.NotifyDynamicSinks(true, msg, true, false);
			IMessage message;
			if (msg is IConstructionCallMessage)
			{
				message = ActivationServices.RemoteActivate((IConstructionCallMessage)msg);
			}
			else
			{
				message = RemotingServices.GetMessageTargetIdentity(msg).ChannelSink.SyncProcessMessage(msg);
			}
			Context.NotifyGlobalDynamicSinks(false, msg, true, false);
			this._context.NotifyDynamicSinks(false, msg, true, false);
			return message;
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x001225F4 File Offset: 0x001207F4
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks)
			{
				Context.NotifyGlobalDynamicSinks(true, msg, true, true);
				this._context.NotifyDynamicSinks(true, msg, true, true);
				if (replySink != null)
				{
					replySink = new ClientContextReplySink(this._context, replySink);
				}
			}
			IMessageCtrl messageCtrl = RemotingServices.GetMessageTargetIdentity(msg).ChannelSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null && (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks))
			{
				Context.NotifyGlobalDynamicSinks(false, msg, true, true);
				this._context.NotifyDynamicSinks(false, msg, true, true);
			}
			return messageCtrl;
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x0600520F RID: 21007 RVA: 0x0000A42E File Offset: 0x0000862E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002AF8 RID: 11000
		private Context _context;
	}
}

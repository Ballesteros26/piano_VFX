using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000780 RID: 1920
	internal class CrossContextChannel : IMessageSink
	{
		// Token: 0x06004F1A RID: 20250 RVA: 0x0011D308 File Offset: 0x0011B508
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessage message;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				message = serverIdentity.Context.GetServerContextSinkChain().SyncProcessMessage(msg);
				Context.NotifyGlobalDynamicSinks(false, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, (IMethodCallMessage)msg);
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return message;
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x0011D3B8 File Offset: 0x0011B5B8
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessageCtrl messageCtrl2;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, true);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				if (replySink != null)
				{
					replySink = new CrossContextChannel.ContextRestoreSink(replySink, context, msg);
				}
				IMessageCtrl messageCtrl = serverIdentity.AsyncObjectProcessMessage(msg, replySink);
				if (replySink == null)
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
				}
				messageCtrl2 = messageCtrl;
			}
			catch (Exception ex)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(new ReturnMessage(ex, (IMethodCallMessage)msg));
				}
				messageCtrl2 = null;
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return messageCtrl2;
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x0000A42E File Offset: 0x0000862E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x02000781 RID: 1921
		private class ContextRestoreSink : IMessageSink
		{
			// Token: 0x06004F1E RID: 20254 RVA: 0x0011D47C File Offset: 0x0011B67C
			public ContextRestoreSink(IMessageSink next, Context context, IMessage call)
			{
				this._next = next;
				this._context = context;
				this._call = call;
			}

			// Token: 0x06004F1F RID: 20255 RVA: 0x0011D49C File Offset: 0x0011B69C
			public IMessage SyncProcessMessage(IMessage msg)
			{
				IMessage message;
				try
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
					message = this._next.SyncProcessMessage(msg);
				}
				catch (Exception ex)
				{
					message = new ReturnMessage(ex, (IMethodCallMessage)this._call);
				}
				finally
				{
					if (this._context != null)
					{
						Context.SwitchToContext(this._context);
					}
				}
				return message;
			}

			// Token: 0x06004F20 RID: 20256 RVA: 0x00014B5A File Offset: 0x00012D5A
			public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000D34 RID: 3380
			// (get) Token: 0x06004F21 RID: 20257 RVA: 0x0011D514 File Offset: 0x0011B714
			public IMessageSink NextSink
			{
				get
				{
					return this._next;
				}
			}

			// Token: 0x04002A2A RID: 10794
			private IMessageSink _next;

			// Token: 0x04002A2B RID: 10795
			private Context _context;

			// Token: 0x04002A2C RID: 10796
			private IMessage _call;
		}
	}
}

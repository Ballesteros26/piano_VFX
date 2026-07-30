using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020007BE RID: 1982
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x06005029 RID: 20521 RVA: 0x0011F51F File Offset: 0x0011D71F
		public ContextLevelActivator(IActivator next)
		{
			this.m_NextActivator = next;
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x00058C99 File Offset: 0x00056E99
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Context;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x0011F52E File Offset: 0x0011D72E
		// (set) Token: 0x0600502C RID: 20524 RVA: 0x0011F536 File Offset: 0x0011D736
		public IActivator NextActivator
		{
			get
			{
				return this.m_NextActivator;
			}
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x0011F540 File Offset: 0x0011D740
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			ServerIdentity serverIdentity = RemotingServices.CreateContextBoundObjectIdentity(ctorCall.ActivationType);
			RemotingServices.SetMessageTargetIdentity(ctorCall, serverIdentity);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (constructionCall == null || !constructionCall.IsContextOk)
			{
				serverIdentity.Context = Context.CreateNewContext(ctorCall);
				Context context = Context.SwitchToContext(serverIdentity.Context);
				try
				{
					return this.m_NextActivator.Activate(ctorCall);
				}
				finally
				{
					Context.SwitchToContext(context);
				}
			}
			return this.m_NextActivator.Activate(ctorCall);
		}

		// Token: 0x04002A71 RID: 10865
		private IActivator m_NextActivator;
	}
}

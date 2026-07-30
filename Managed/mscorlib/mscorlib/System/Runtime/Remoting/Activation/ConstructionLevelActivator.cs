using System;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020007BD RID: 1981
	[Serializable]
	internal class ConstructionLevelActivator : IActivator
	{
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06005024 RID: 20516 RVA: 0x00028BDC File Offset: 0x00026DDC
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Construction;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06005026 RID: 20518 RVA: 0x00002194 File Offset: 0x00000394
		public IActivator NextActivator
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x0011F508 File Offset: 0x0011D708
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			return (IConstructionReturnMessage)Thread.CurrentContext.GetServerContextSinkChain().SyncProcessMessage(msg);
		}
	}
}

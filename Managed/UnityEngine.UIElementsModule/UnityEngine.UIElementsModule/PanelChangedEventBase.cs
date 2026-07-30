using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000170 RID: 368
	public abstract class PanelChangedEventBase<T> : EventBase<T>, IPanelChangedEvent where T : PanelChangedEventBase<T>, new()
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00026E8C File Offset: 0x0002508C
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00026E94 File Offset: 0x00025094
		public IPanel originPanel { get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00026E9D File Offset: 0x0002509D
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00026EA5 File Offset: 0x000250A5
		public IPanel destinationPanel { get; private set; }

		// Token: 0x06000A28 RID: 2600 RVA: 0x00026EAE File Offset: 0x000250AE
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00026EBF File Offset: 0x000250BF
		private void LocalInit()
		{
			this.originPanel = null;
			this.destinationPanel = null;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00026ED4 File Offset: 0x000250D4
		public static T GetPooled(IPanel originPanel, IPanel destinationPanel)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.originPanel = originPanel;
			pooled.destinationPanel = destinationPanel;
			return pooled;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00026F07 File Offset: 0x00025107
		protected PanelChangedEventBase()
		{
			this.LocalInit();
		}
	}
}

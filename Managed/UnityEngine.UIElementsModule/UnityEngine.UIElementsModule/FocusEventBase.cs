using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014B RID: 331
	public abstract class FocusEventBase<T> : EventBase<T>, IFocusEvent where T : FocusEventBase<T>, new()
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00024C8C File Offset: 0x00022E8C
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00024C94 File Offset: 0x00022E94
		public Focusable relatedTarget { get; private set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00024C9D File Offset: 0x00022E9D
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00024CA5 File Offset: 0x00022EA5
		public FocusChangeDirection direction { get; private set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00024CAE File Offset: 0x00022EAE
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00024CB6 File Offset: 0x00022EB6
		private protected FocusController focusController { protected get; private set; }

		// Token: 0x06000968 RID: 2408 RVA: 0x00024CBF File Offset: 0x00022EBF
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00024CD0 File Offset: 0x00022ED0
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
			this.relatedTarget = null;
			this.direction = FocusChangeDirection.unspecified;
			this.focusController = null;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00024CF8 File Offset: 0x00022EF8
		public static T GetPooled(IEventHandler target, Focusable relatedTarget, FocusChangeDirection direction, FocusController focusController)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.target = target;
			pooled.relatedTarget = relatedTarget;
			pooled.direction = direction;
			pooled.focusController = focusController;
			return pooled;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00024D45 File Offset: 0x00022F45
		protected FocusEventBase()
		{
			this.LocalInit();
		}
	}
}

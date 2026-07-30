using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200018A RID: 394
	public class IMGUIEvent : EventBase<IMGUIEvent>
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x0002880C File Offset: 0x00026A0C
		public static IMGUIEvent GetPooled(Event systemEvent)
		{
			IMGUIEvent pooled = EventBase<IMGUIEvent>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002882D File Offset: 0x00026A2D
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002883E File Offset: 0x00026A3E
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00028849 File Offset: 0x00026A49
		public IMGUIEvent()
		{
			this.LocalInit();
		}
	}
}

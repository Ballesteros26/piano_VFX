using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014C RID: 332
	public class FocusOutEvent : FocusEventBase<FocusOutEvent>
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x00024D56 File Offset: 0x00022F56
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00022E08 File Offset: 0x00021008
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00024D67 File Offset: 0x00022F67
		public FocusOutEvent()
		{
			this.LocalInit();
		}
	}
}

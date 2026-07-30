using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000167 RID: 359
	public class MouseLeaveEvent : MouseEventBase<MouseLeaveEvent>
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x0002655C File Offset: 0x0002475C
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00026540 File Offset: 0x00024740
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002656D File Offset: 0x0002476D
		public MouseLeaveEvent()
		{
			this.LocalInit();
		}
	}
}

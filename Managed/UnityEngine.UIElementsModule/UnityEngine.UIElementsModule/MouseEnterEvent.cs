using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000166 RID: 358
	public class MouseEnterEvent : MouseEventBase<MouseEnterEvent>
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0002652F File Offset: 0x0002472F
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00026540 File Offset: 0x00024740
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002654B File Offset: 0x0002474B
		public MouseEnterEvent()
		{
			this.LocalInit();
		}
	}
}

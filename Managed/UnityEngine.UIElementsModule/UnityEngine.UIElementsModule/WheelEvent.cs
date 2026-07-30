using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000165 RID: 357
	public class WheelEvent : MouseEventBase<WheelEvent>
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x000264AE File Offset: 0x000246AE
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x000264B6 File Offset: 0x000246B6
		public Vector3 delta { get; private set; }

		// Token: 0x06000A02 RID: 2562 RVA: 0x000264C0 File Offset: 0x000246C0
		public new static WheelEvent GetPooled(Event systemEvent)
		{
			WheelEvent pooled = MouseEventBase<WheelEvent>.GetPooled(systemEvent);
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.delta = systemEvent.delta;
			}
			return pooled;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000264FE File Offset: 0x000246FE
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002650F File Offset: 0x0002470F
		private void LocalInit()
		{
			this.delta = Vector3.zero;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002651E File Offset: 0x0002471E
		public WheelEvent()
		{
			this.LocalInit();
		}
	}
}

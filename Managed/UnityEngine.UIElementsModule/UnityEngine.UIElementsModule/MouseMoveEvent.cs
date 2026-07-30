using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000163 RID: 355
	public class MouseMoveEvent : MouseEventBase<MouseMoveEvent>
	{
		// Token: 0x060009FC RID: 2556 RVA: 0x00026460 File Offset: 0x00024660
		public new static MouseMoveEvent GetPooled(Event systemEvent)
		{
			MouseMoveEvent pooled = MouseEventBase<MouseMoveEvent>.GetPooled(systemEvent);
			pooled.button = 0;
			return pooled;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00026484 File Offset: 0x00024684
		internal static MouseMoveEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseEventBase<MouseMoveEvent>.GetPooled(pointerEvent);
		}
	}
}

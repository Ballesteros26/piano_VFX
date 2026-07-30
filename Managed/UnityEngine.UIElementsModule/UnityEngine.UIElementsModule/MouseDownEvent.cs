using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000161 RID: 353
	public class MouseDownEvent : MouseEventBase<MouseDownEvent>
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x000262E0 File Offset: 0x000244E0
		public new static MouseDownEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, systemEvent.button);
			}
			return MouseEventBase<MouseDownEvent>.GetPooled(systemEvent);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00026314 File Offset: 0x00024514
		private static MouseDownEvent MakeFromPointerEvent(IPointerEvent pointerEvent)
		{
			bool flag = pointerEvent != null && pointerEvent.button >= 0;
			if (flag)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, pointerEvent.button);
			}
			return MouseEventBase<MouseDownEvent>.GetPooled(pointerEvent);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00026358 File Offset: 0x00024558
		internal static MouseDownEvent GetPooled(PointerDownEvent pointerEvent)
		{
			return MouseDownEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00026370 File Offset: 0x00024570
		internal static MouseDownEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseDownEvent.MakeFromPointerEvent(pointerEvent);
		}
	}
}

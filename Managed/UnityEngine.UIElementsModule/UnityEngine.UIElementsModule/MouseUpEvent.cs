using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000162 RID: 354
	public class MouseUpEvent : MouseEventBase<MouseUpEvent>
	{
		// Token: 0x060009F6 RID: 2550 RVA: 0x00026394 File Offset: 0x00024594
		public new static MouseUpEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseButton(PointerId.mousePointerId, systemEvent.button);
			}
			return MouseEventBase<MouseUpEvent>.GetPooled(systemEvent);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000263C8 File Offset: 0x000245C8
		private static MouseUpEvent MakeFromPointerEvent(IPointerEvent pointerEvent)
		{
			bool flag = pointerEvent != null && pointerEvent.button >= 0;
			if (flag)
			{
				PointerDeviceState.ReleaseButton(PointerId.mousePointerId, pointerEvent.button);
			}
			return MouseEventBase<MouseUpEvent>.GetPooled(pointerEvent);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002640C File Offset: 0x0002460C
		internal static MouseUpEvent GetPooled(PointerUpEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00026424 File Offset: 0x00024624
		internal static MouseUpEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002643C File Offset: 0x0002463C
		internal static MouseUpEvent GetPooled(PointerCancelEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}
	}
}

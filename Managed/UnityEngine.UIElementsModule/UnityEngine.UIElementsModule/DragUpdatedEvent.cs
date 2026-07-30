using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000137 RID: 311
	public class DragUpdatedEvent : DragAndDropEventBase<DragUpdatedEvent>
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x00022EE8 File Offset: 0x000210E8
		public new static DragUpdatedEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, systemEvent.button);
			}
			DragUpdatedEvent pooled = MouseEventBase<DragUpdatedEvent>.GetPooled(systemEvent);
			pooled.button = 0;
			return pooled;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00022F28 File Offset: 0x00021128
		internal static DragUpdatedEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseEventBase<DragUpdatedEvent>.GetPooled(pointerEvent);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00022F40 File Offset: 0x00021140
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase == null;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}
	}
}

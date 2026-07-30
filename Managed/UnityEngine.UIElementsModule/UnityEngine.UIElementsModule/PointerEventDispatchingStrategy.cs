using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000175 RID: 373
	internal class PointerEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x000271E0 File Offset: 0x000253E0
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IPointerEvent;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000271FB File Offset: 0x000253FB
		public virtual void DispatchEvent(EventBase evt, IPanel panel)
		{
			PointerEventDispatchingStrategy.SetBestTargetForEvent(evt, panel);
			PointerEventDispatchingStrategy.SendEventToTarget(evt);
			evt.stopDispatch = true;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00027218 File Offset: 0x00025418
		private static void SendEventToTarget(EventBase evt)
		{
			bool flag = evt.target != null;
			if (flag)
			{
				EventDispatchUtilities.PropagateEvent(evt);
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002723C File Offset: 0x0002543C
		private static void SetBestTargetForEvent(EventBase evt, IPanel panel)
		{
			VisualElement visualElement;
			PointerEventDispatchingStrategy.UpdateElementUnderPointer(evt, panel, out visualElement);
			bool flag = evt.target == null && visualElement != null;
			if (flag)
			{
				evt.propagateToIMGUI = false;
				evt.target = visualElement;
			}
			else
			{
				bool flag2 = evt.target == null && visualElement == null;
				if (flag2)
				{
					evt.target = ((panel != null) ? panel.visualTree : null);
				}
				else
				{
					bool flag3 = evt.target != null;
					if (flag3)
					{
						evt.propagateToIMGUI = false;
					}
				}
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000272BC File Offset: 0x000254BC
		private static void UpdateElementUnderPointer(EventBase evt, IPanel panel, out VisualElement elementUnderPointer)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = true;
			bool flag2 = evt is IPointerEventInternal;
			if (flag2)
			{
				flag = ((IPointerEventInternal)pointerEvent).recomputeTopElementUnderPointer;
			}
			elementUnderPointer = (flag ? ((baseVisualElementPanel != null) ? baseVisualElementPanel.Pick(pointerEvent.position) : null) : ((baseVisualElementPanel != null) ? baseVisualElementPanel.GetTopElementUnderPointer(pointerEvent.pointerId) : null));
			bool flag3 = baseVisualElementPanel != null && flag;
			if (flag3)
			{
				baseVisualElementPanel.SetElementUnderPointer(elementUnderPointer, evt);
			}
		}
	}
}

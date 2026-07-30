using System;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015D RID: 349
	internal class MouseEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x000258F8 File Offset: 0x00023AF8
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IMouseEvent;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00025914 File Offset: 0x00023B14
		public void DispatchEvent(EventBase evt, IPanel iPanel)
		{
			bool flag = iPanel != null;
			if (flag)
			{
				Assert.IsTrue(iPanel is BaseVisualElementPanel);
				BaseVisualElementPanel baseVisualElementPanel = (BaseVisualElementPanel)iPanel;
				MouseEventDispatchingStrategy.SetBestTargetForEvent(evt, baseVisualElementPanel);
				MouseEventDispatchingStrategy.SendEventToTarget(evt, baseVisualElementPanel);
			}
			evt.stopDispatch = true;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002595C File Offset: 0x00023B5C
		private static bool SendEventToTarget(EventBase evt, BaseVisualElementPanel panel)
		{
			return MouseEventDispatchingStrategy.SendEventToRegularTarget(evt, panel) || MouseEventDispatchingStrategy.SendEventToIMGUIContainer(evt, panel);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00025984 File Offset: 0x00023B84
		private static bool SendEventToRegularTarget(EventBase evt, BaseVisualElementPanel panel)
		{
			bool flag = evt.target == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventDispatchUtilities.PropagateEvent(evt);
				bool flag3 = evt.target is IMGUIContainer;
				if (flag3)
				{
					evt.propagateToIMGUI = true;
					evt.skipElements.Add(evt.target);
				}
				flag2 = MouseEventDispatchingStrategy.IsDone(evt);
			}
			return flag2;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000259E4 File Offset: 0x00023BE4
		private static bool SendEventToIMGUIContainer(EventBase evt, BaseVisualElementPanel panel)
		{
			bool flag = evt.propagateToIMGUI || evt.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
			if (flag)
			{
				EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
			}
			else
			{
				IMGUIContainer rootIMGUIContainer = panel.rootIMGUIContainer;
				bool flag2 = rootIMGUIContainer != null && !evt.Skip(rootIMGUIContainer) && evt.imguiEvent != null;
				if (flag2)
				{
					rootIMGUIContainer.SendEventToIMGUI(evt, false, true);
				}
			}
			return MouseEventDispatchingStrategy.IsDone(evt);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00025A68 File Offset: 0x00023C68
		private static void SetBestTargetForEvent(EventBase evt, BaseVisualElementPanel panel)
		{
			VisualElement visualElement;
			MouseEventDispatchingStrategy.UpdateElementUnderMouse(evt, panel, out visualElement);
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

		// Token: 0x060009B6 RID: 2486 RVA: 0x00025AE8 File Offset: 0x00023CE8
		private static void UpdateElementUnderMouse(EventBase evt, BaseVisualElementPanel panel, out VisualElement elementUnderMouse)
		{
			IMouseEventInternal mouseEventInternal = evt as IMouseEventInternal;
			bool flag = mouseEventInternal == null || mouseEventInternal.recomputeTopElementUnderMouse;
			elementUnderMouse = (flag ? panel.Pick(((IMouseEvent)evt).mousePosition) : panel.GetTopElementUnderPointer(PointerId.mousePointerId));
			bool flag2 = evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId() && (evt as MouseLeaveWindowEvent).pressedButtons == 0;
			if (flag2)
			{
				panel.SetElementUnderPointer(null, evt);
			}
			else
			{
				bool flag3 = flag;
				if (flag3)
				{
					panel.SetElementUnderPointer(elementUnderMouse, evt);
				}
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00025B6C File Offset: 0x00023D6C
		private static bool IsDone(EventBase evt)
		{
			Event imguiEvent = evt.imguiEvent;
			bool flag = imguiEvent != null && imguiEvent.rawType == EventType.Used;
			if (flag)
			{
				evt.StopPropagation();
			}
			return evt.isPropagationStopped;
		}
	}
}

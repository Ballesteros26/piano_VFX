using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000131 RID: 305
	internal class DefaultDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x00022D3C File Offset: 0x00020F3C
		public bool CanDispatchEvent(EventBase evt)
		{
			return !(evt is IMGUIEvent);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00022D5C File Offset: 0x00020F5C
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = evt.target != null;
			if (flag)
			{
				evt.propagateToIMGUI = evt.target is IMGUIContainer;
				EventDispatchUtilities.PropagateEvent(evt);
			}
			else
			{
				bool flag2 = !evt.isPropagationStopped && panel != null;
				if (flag2)
				{
					bool flag3 = evt.propagateToIMGUI || evt.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
					if (flag3)
					{
						EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
					}
				}
			}
			evt.stopDispatch = true;
		}
	}
}

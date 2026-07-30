using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012B RID: 299
	internal class CommandEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x00022B40 File Offset: 0x00020D40
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is ICommandEvent;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00022B5C File Offset: 0x00020D5C
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = panel != null;
			if (flag)
			{
				Focusable leafFocusedElement = panel.focusController.GetLeafFocusedElement();
				bool flag2 = leafFocusedElement != null;
				if (flag2)
				{
					bool isIMGUIContainer = leafFocusedElement.isIMGUIContainer;
					if (isIMGUIContainer)
					{
						IMGUIContainer imguicontainer = (IMGUIContainer)leafFocusedElement;
						bool flag3 = !evt.Skip(imguicontainer) && imguicontainer.SendEventToIMGUI(evt, true, true);
						if (flag3)
						{
							evt.StopPropagation();
							evt.PreventDefault();
						}
						bool flag4 = !evt.isPropagationStopped && evt.propagateToIMGUI;
						if (flag4)
						{
							evt.skipElements.Add(imguicontainer);
							EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
						}
					}
					else
					{
						evt.target = panel.focusController.GetLeafFocusedElement();
						EventDispatchUtilities.PropagateEvent(evt);
					}
				}
				else
				{
					EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
				}
			}
			evt.propagateToIMGUI = false;
			evt.stopDispatch = true;
		}
	}
}

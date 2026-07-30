using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000153 RID: 339
	internal class IMGUIEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x00025230 File Offset: 0x00023430
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IMGUIEvent;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002524C File Offset: 0x0002344C
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = panel != null;
			if (flag)
			{
				EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
			}
			evt.propagateToIMGUI = false;
			evt.stopDispatch = true;
		}
	}
}

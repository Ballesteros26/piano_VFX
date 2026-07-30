using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000A RID: 10
	public abstract class ContextualMenuManager
	{
		// Token: 0x06000035 RID: 53
		public abstract void DisplayMenuIfEventMatches(EventBase evt, IEventHandler eventHandler);

		// Token: 0x06000036 RID: 54 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public void DisplayMenu(EventBase triggerEvent, IEventHandler target)
		{
			DropdownMenu dropdownMenu = new DropdownMenu();
			using (ContextualMenuPopulateEvent pooled = ContextualMenuPopulateEvent.GetPooled(triggerEvent, dropdownMenu, target, this))
			{
				if (target != null)
				{
					target.SendEvent(pooled);
				}
			}
		}

		// Token: 0x06000037 RID: 55
		protected internal abstract void DoDisplayMenu(DropdownMenu menu, EventBase triggerEvent);
	}
}

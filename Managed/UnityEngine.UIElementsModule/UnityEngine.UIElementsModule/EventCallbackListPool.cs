using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000145 RID: 325
	internal class EventCallbackListPool
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x00024240 File Offset: 0x00022440
		public EventCallbackList Get(EventCallbackList initializer)
		{
			bool flag = this.m_Stack.Count == 0;
			EventCallbackList eventCallbackList;
			if (flag)
			{
				bool flag2 = initializer != null;
				if (flag2)
				{
					eventCallbackList = new EventCallbackList(initializer);
				}
				else
				{
					eventCallbackList = new EventCallbackList();
				}
			}
			else
			{
				eventCallbackList = this.m_Stack.Pop();
				bool flag3 = initializer != null;
				if (flag3)
				{
					eventCallbackList.AddRange(initializer);
				}
			}
			return eventCallbackList;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000242A0 File Offset: 0x000224A0
		public void Release(EventCallbackList element)
		{
			element.Clear();
			this.m_Stack.Push(element);
		}

		// Token: 0x04000416 RID: 1046
		private readonly Stack<EventCallbackList> m_Stack = new Stack<EventCallbackList>();
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000151 RID: 337
	internal interface IEventDispatchingStrategy
	{
		// Token: 0x06000976 RID: 2422
		bool CanDispatchEvent(EventBase evt);

		// Token: 0x06000977 RID: 2423
		void DispatchEvent(EventBase evt, IPanel panel);
	}
}

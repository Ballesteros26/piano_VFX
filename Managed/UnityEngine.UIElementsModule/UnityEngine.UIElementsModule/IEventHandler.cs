using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000148 RID: 328
	public interface IEventHandler
	{
		// Token: 0x0600094F RID: 2383
		void SendEvent(EventBase e);

		// Token: 0x06000950 RID: 2384
		void HandleEvent(EventBase evt);

		// Token: 0x06000951 RID: 2385
		bool HasTrickleDownHandlers();

		// Token: 0x06000952 RID: 2386
		bool HasBubbleUpHandlers();
	}
}

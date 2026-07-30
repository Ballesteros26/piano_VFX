using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A2 RID: 162
	public interface IVisualElementScheduler
	{
		// Token: 0x060004DA RID: 1242
		IVisualElementScheduledItem Execute(Action<TimerState> timerUpdateEvent);

		// Token: 0x060004DB RID: 1243
		IVisualElementScheduledItem Execute(Action updateEvent);
	}
}

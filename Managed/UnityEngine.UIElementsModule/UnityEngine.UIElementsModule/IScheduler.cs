using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004A RID: 74
	internal interface IScheduler
	{
		// Token: 0x06000200 RID: 512
		ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs);

		// Token: 0x06000201 RID: 513
		ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition = null);

		// Token: 0x06000202 RID: 514
		ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs);

		// Token: 0x06000203 RID: 515
		void Unschedule(ScheduledItem item);

		// Token: 0x06000204 RID: 516
		void Schedule(ScheduledItem item);

		// Token: 0x06000205 RID: 517
		void UpdateScheduledEvents();
	}
}

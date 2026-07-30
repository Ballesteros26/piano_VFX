using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004B RID: 75
	internal abstract class ScheduledItem
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000078A4 File Offset: 0x00005AA4
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000078AC File Offset: 0x00005AAC
		public long startMs { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000078B5 File Offset: 0x00005AB5
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000078BD File Offset: 0x00005ABD
		public long delayMs { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000078C6 File Offset: 0x00005AC6
		// (set) Token: 0x0600020B RID: 523 RVA: 0x000078CE File Offset: 0x00005ACE
		public long intervalMs { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000078D7 File Offset: 0x00005AD7
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000078DF File Offset: 0x00005ADF
		public long endTimeMs { get; private set; }

		// Token: 0x0600020E RID: 526 RVA: 0x000078E8 File Offset: 0x00005AE8
		public ScheduledItem()
		{
			this.ResetStartTime();
			this.timerUpdateStopCondition = ScheduledItem.OnceCondition;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007904 File Offset: 0x00005B04
		protected void ResetStartTime()
		{
			this.startMs = Panel.TimeSinceStartupMs();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007913 File Offset: 0x00005B13
		public void SetDuration(long durationMs)
		{
			this.endTimeMs = this.startMs + durationMs;
		}

		// Token: 0x06000211 RID: 529
		public abstract void PerformTimerUpdate(TimerState state);

		// Token: 0x06000212 RID: 530 RVA: 0x000062F3 File Offset: 0x000044F3
		internal virtual void OnItemUnscheduled()
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007928 File Offset: 0x00005B28
		public virtual bool ShouldUnschedule()
		{
			bool flag = this.timerUpdateStopCondition != null;
			return flag && this.timerUpdateStopCondition.Invoke();
		}

		// Token: 0x040000D7 RID: 215
		public Func<bool> timerUpdateStopCondition;

		// Token: 0x040000D8 RID: 216
		public static readonly Func<bool> OnceCondition = () => true;

		// Token: 0x040000D9 RID: 217
		public static readonly Func<bool> ForeverCondition = () => false;
	}
}

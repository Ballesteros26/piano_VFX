using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004D RID: 77
	internal class TimerEventScheduler : IScheduler
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00007998 File Offset: 0x00005B98
		public void Schedule(ScheduledItem item)
		{
			bool flag = item == null;
			if (!flag)
			{
				bool flag2 = item == null;
				if (flag2)
				{
					throw new NotSupportedException("Scheduled Item type is not supported by this scheduler");
				}
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag3 = this.m_UnscheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item) || this.m_ScheduleTransactions.Contains(item);
						if (flag4)
						{
							throw new ArgumentException("Cannot schedule function " + item + " more than once");
						}
						this.m_ScheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = this.m_ScheduledItems.Contains(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot schedule function " + item + " more than once");
					}
					this.m_ScheduledItems.Add(item);
				}
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007A70 File Offset: 0x00005C70
		public ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007A9C File Offset: 0x00005C9C
		public ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = stopCondition
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = null
			};
			timerEventSchedulerItem.SetDuration(durationMs);
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007B1C File Offset: 0x00005D1C
		private bool RemovedScheduledItemAt(int index)
		{
			bool flag = index >= 0;
			bool flag2;
			if (flag)
			{
				this.m_ScheduledItems.RemoveAt(index);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007B4C File Offset: 0x00005D4C
		public void Unschedule(ScheduledItem item)
		{
			bool flag = item != null;
			if (flag)
			{
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag2 = this.m_UnscheduleTransactions.Contains(item);
					if (flag2)
					{
						throw new ArgumentException("Cannot unschedule scheduled function twice" + item);
					}
					bool flag3 = this.m_ScheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item);
						if (!flag4)
						{
							throw new ArgumentException("Cannot unschedule unknown scheduled function " + item);
						}
						this.m_UnscheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = !this.PrivateUnSchedule(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot unschedule unknown scheduled function " + item);
					}
				}
				item.OnItemUnscheduled();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007C10 File Offset: 0x00005E10
		private bool PrivateUnSchedule(ScheduledItem sItem)
		{
			return this.m_ScheduleTransactions.Remove(sItem) || this.RemovedScheduledItemAt(this.m_ScheduledItems.IndexOf(sItem));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007C48 File Offset: 0x00005E48
		public void UpdateScheduledEvents()
		{
			try
			{
				this.m_TransactionMode = true;
				long num = Panel.TimeSinceStartupMs();
				int count = this.m_ScheduledItems.Count;
				int num2 = this.m_LastUpdatedIndex + 1;
				bool flag = num2 >= count;
				if (flag)
				{
					num2 = 0;
				}
				for (int i = 0; i < count; i++)
				{
					int num3 = num2 + i;
					bool flag2 = num3 >= count;
					if (flag2)
					{
						num3 -= count;
					}
					ScheduledItem scheduledItem = this.m_ScheduledItems[num3];
					bool flag3 = false;
					bool flag4 = num - scheduledItem.delayMs >= scheduledItem.startMs;
					if (flag4)
					{
						TimerState timerState = new TimerState
						{
							start = scheduledItem.startMs,
							now = num
						};
						bool flag5 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag5)
						{
							scheduledItem.PerformTimerUpdate(timerState);
						}
						scheduledItem.startMs = num;
						scheduledItem.delayMs = scheduledItem.intervalMs;
						bool flag6 = scheduledItem.ShouldUnschedule();
						if (flag6)
						{
							flag3 = true;
						}
					}
					bool flag7 = flag3 || (scheduledItem.endTimeMs > 0L && num > scheduledItem.endTimeMs);
					if (flag7)
					{
						bool flag8 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag8)
						{
							this.Unschedule(scheduledItem);
						}
					}
					this.m_LastUpdatedIndex = num3;
				}
			}
			finally
			{
				this.m_TransactionMode = false;
				foreach (ScheduledItem scheduledItem2 in this.m_UnscheduleTransactions)
				{
					this.PrivateUnSchedule(scheduledItem2);
				}
				this.m_UnscheduleTransactions.Clear();
				foreach (ScheduledItem scheduledItem3 in this.m_ScheduleTransactions)
				{
					this.Schedule(scheduledItem3);
				}
				this.m_ScheduleTransactions.Clear();
			}
		}

		// Token: 0x040000DF RID: 223
		private readonly List<ScheduledItem> m_ScheduledItems = new List<ScheduledItem>();

		// Token: 0x040000E0 RID: 224
		private bool m_TransactionMode;

		// Token: 0x040000E1 RID: 225
		private readonly List<ScheduledItem> m_ScheduleTransactions = new List<ScheduledItem>();

		// Token: 0x040000E2 RID: 226
		private readonly HashSet<ScheduledItem> m_UnscheduleTransactions = new HashSet<ScheduledItem>();

		// Token: 0x040000E3 RID: 227
		internal bool disableThrottling = false;

		// Token: 0x040000E4 RID: 228
		private int m_LastUpdatedIndex = -1;

		// Token: 0x0200004E RID: 78
		private class TimerEventSchedulerItem : ScheduledItem
		{
			// Token: 0x06000222 RID: 546 RVA: 0x00007ED8 File Offset: 0x000060D8
			public TimerEventSchedulerItem(Action<TimerState> updateEvent)
			{
				this.m_TimerUpdateEvent = updateEvent;
			}

			// Token: 0x06000223 RID: 547 RVA: 0x00007EE9 File Offset: 0x000060E9
			public override void PerformTimerUpdate(TimerState state)
			{
				Action<TimerState> timerUpdateEvent = this.m_TimerUpdateEvent;
				if (timerUpdateEvent != null)
				{
					timerUpdateEvent.Invoke(state);
				}
			}

			// Token: 0x06000224 RID: 548 RVA: 0x00007F00 File Offset: 0x00006100
			public override string ToString()
			{
				return this.m_TimerUpdateEvent.ToString();
			}

			// Token: 0x040000E5 RID: 229
			private readonly Action<TimerState> m_TimerUpdateEvent;
		}
	}
}

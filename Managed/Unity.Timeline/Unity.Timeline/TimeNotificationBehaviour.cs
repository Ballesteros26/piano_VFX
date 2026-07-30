using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000038 RID: 56
	public class TimeNotificationBehaviour : PlayableBehaviour
	{
		// Token: 0x170000B8 RID: 184
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00008EC2 File Offset: 0x000070C2
		public Playable timeSource
		{
			set
			{
				this.m_TimeSource = value;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008ECB File Offset: 0x000070CB
		public static ScriptPlayable<TimeNotificationBehaviour> Create(PlayableGraph graph, double duration, DirectorWrapMode loopMode)
		{
			ScriptPlayable<TimeNotificationBehaviour> scriptPlayable = ScriptPlayable<TimeNotificationBehaviour>.Create(graph, 0);
			scriptPlayable.SetDuration(duration);
			scriptPlayable.SetTimeWrapMode(loopMode);
			scriptPlayable.SetPropagateSetTime(true);
			return scriptPlayable;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008EEC File Offset: 0x000070EC
		public void AddNotification(double time, INotification payload, NotificationFlags flags = NotificationFlags.Retroactive)
		{
			this.m_Notifications.Add(new TimeNotificationBehaviour.NotificationEntry
			{
				time = time,
				payload = payload,
				flags = flags
			});
			this.m_NeedSortNotifications = true;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00008F2C File Offset: 0x0000712C
		public override void OnGraphStart(Playable playable)
		{
			this.SortNotifications();
			for (int i = 0; i < this.m_Notifications.Count; i++)
			{
				TimeNotificationBehaviour.NotificationEntry notificationEntry = this.m_Notifications[i];
				notificationEntry.notificationFired = false;
				this.m_Notifications[i] = notificationEntry;
			}
			this.m_PreviousTime = playable.GetTime<Playable>();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008F84 File Offset: 0x00007184
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (playable.IsDone<Playable>())
			{
				this.SortNotifications();
				for (int i = 0; i < this.m_Notifications.Count; i++)
				{
					TimeNotificationBehaviour.NotificationEntry notificationEntry = this.m_Notifications[i];
					if (!notificationEntry.notificationFired)
					{
						double duration = playable.GetDuration<Playable>();
						if (this.m_PreviousTime <= notificationEntry.time && notificationEntry.time <= duration)
						{
							TimeNotificationBehaviour.Trigger_internal(playable, info.output, ref notificationEntry);
							this.m_Notifications[i] = notificationEntry;
						}
					}
				}
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000900C File Offset: 0x0000720C
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (info.evaluationType == FrameData.EvaluationType.Evaluate)
			{
				return;
			}
			this.SyncDurationWithExternalSource(playable);
			this.SortNotifications();
			double time = playable.GetTime<Playable>();
			if (info.timeLooped)
			{
				double duration = playable.GetDuration<Playable>();
				this.TriggerNotificationsInRange(this.m_PreviousTime, duration, info, playable, true);
				double num = playable.GetDuration<Playable>() - this.m_PreviousTime;
				int num2 = (int)(((double)(info.deltaTime * info.effectiveSpeed) - num) / playable.GetDuration<Playable>());
				for (int i = 0; i < num2; i++)
				{
					this.TriggerNotificationsInRange(0.0, duration, info, playable, false);
				}
				this.TriggerNotificationsInRange(0.0, time, info, playable, false);
			}
			else
			{
				double time2 = playable.GetTime<Playable>();
				this.TriggerNotificationsInRange(this.m_PreviousTime, time2, info, playable, true);
			}
			for (int j = 0; j < this.m_Notifications.Count; j++)
			{
				TimeNotificationBehaviour.NotificationEntry notificationEntry = this.m_Notifications[j];
				if (notificationEntry.notificationFired && TimeNotificationBehaviour.CanRestoreNotification(notificationEntry, info, time, this.m_PreviousTime))
				{
					TimeNotificationBehaviour.Restore_internal(ref notificationEntry);
					this.m_Notifications[j] = notificationEntry;
				}
			}
			this.m_PreviousTime = playable.GetTime<Playable>();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009136 File Offset: 0x00007336
		private void SortNotifications()
		{
			if (this.m_NeedSortNotifications)
			{
				this.m_Notifications.Sort((TimeNotificationBehaviour.NotificationEntry x, TimeNotificationBehaviour.NotificationEntry y) => x.time.CompareTo(y.time));
				this.m_NeedSortNotifications = false;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009171 File Offset: 0x00007371
		private static bool CanRestoreNotification(TimeNotificationBehaviour.NotificationEntry e, FrameData info, double currentTime, double previousTime)
		{
			return !e.triggerOnce && (info.timeLooped || (previousTime > currentTime && currentTime <= e.time));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000919C File Offset: 0x0000739C
		private void TriggerNotificationsInRange(double start, double end, FrameData info, Playable playable, bool checkState)
		{
			if (start <= end)
			{
				bool isPlaying = Application.isPlaying;
				for (int i = 0; i < this.m_Notifications.Count; i++)
				{
					TimeNotificationBehaviour.NotificationEntry notificationEntry = this.m_Notifications[i];
					if (!notificationEntry.notificationFired || (!checkState && !notificationEntry.triggerOnce))
					{
						double time = notificationEntry.time;
						if (notificationEntry.prewarm && time < end && (notificationEntry.triggerInEditor || isPlaying))
						{
							TimeNotificationBehaviour.Trigger_internal(playable, info.output, ref notificationEntry);
							this.m_Notifications[i] = notificationEntry;
						}
						else if (time >= start && time <= end && (notificationEntry.triggerInEditor || isPlaying))
						{
							TimeNotificationBehaviour.Trigger_internal(playable, info.output, ref notificationEntry);
							this.m_Notifications[i] = notificationEntry;
						}
					}
				}
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009262 File Offset: 0x00007462
		private void SyncDurationWithExternalSource(Playable playable)
		{
			if (this.m_TimeSource.IsValid<Playable>())
			{
				playable.SetDuration(this.m_TimeSource.GetDuration<Playable>());
				playable.SetTimeWrapMode(this.m_TimeSource.GetTimeWrapMode<Playable>());
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009293 File Offset: 0x00007493
		private static void Trigger_internal(Playable playable, PlayableOutput output, ref TimeNotificationBehaviour.NotificationEntry e)
		{
			output.PushNotification(playable, e.payload, null);
			e.notificationFired = true;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000092AA File Offset: 0x000074AA
		private static void Restore_internal(ref TimeNotificationBehaviour.NotificationEntry e)
		{
			e.notificationFired = false;
		}

		// Token: 0x040000E0 RID: 224
		private readonly List<TimeNotificationBehaviour.NotificationEntry> m_Notifications = new List<TimeNotificationBehaviour.NotificationEntry>();

		// Token: 0x040000E1 RID: 225
		private double m_PreviousTime;

		// Token: 0x040000E2 RID: 226
		private bool m_NeedSortNotifications;

		// Token: 0x040000E3 RID: 227
		private Playable m_TimeSource;

		// Token: 0x0200006F RID: 111
		private struct NotificationEntry
		{
			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000335 RID: 821 RVA: 0x0000AFD8 File Offset: 0x000091D8
			public bool triggerInEditor
			{
				get
				{
					return (this.flags & NotificationFlags.TriggerInEditMode) > (NotificationFlags)0;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000336 RID: 822 RVA: 0x0000AFE5 File Offset: 0x000091E5
			public bool prewarm
			{
				get
				{
					return (this.flags & NotificationFlags.Retroactive) > (NotificationFlags)0;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000337 RID: 823 RVA: 0x0000AFF2 File Offset: 0x000091F2
			public bool triggerOnce
			{
				get
				{
					return (this.flags & NotificationFlags.TriggerOnce) > (NotificationFlags)0;
				}
			}

			// Token: 0x04000161 RID: 353
			public double time;

			// Token: 0x04000162 RID: 354
			public INotification payload;

			// Token: 0x04000163 RID: 355
			public bool notificationFired;

			// Token: 0x04000164 RID: 356
			public NotificationFlags flags;
		}
	}
}

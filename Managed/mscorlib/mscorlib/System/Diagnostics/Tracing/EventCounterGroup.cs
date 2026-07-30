using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A94 RID: 2708
	internal class EventCounterGroup
	{
		// Token: 0x060062AE RID: 25262 RVA: 0x00141B27 File Offset: 0x0013FD27
		internal EventCounterGroup(EventSource eventSource)
		{
			this._eventSource = eventSource;
			this._eventCounters = new List<EventCounter>();
			this.RegisterCommandCallback();
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x00141B48 File Offset: 0x0013FD48
		internal void Add(EventCounter eventCounter)
		{
			lock (this)
			{
				this._eventCounters.Add(eventCounter);
			}
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x00141B8C File Offset: 0x0013FD8C
		internal void Remove(EventCounter eventCounter)
		{
			lock (this)
			{
				this._eventCounters.Remove(eventCounter);
			}
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x00141BD0 File Offset: 0x0013FDD0
		private void RegisterCommandCallback()
		{
			this._eventSource.EventCommandExecuted += this.OnEventSourceCommand;
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x00141BEC File Offset: 0x0013FDEC
		private void OnEventSourceCommand(object sender, EventCommandEventArgs e)
		{
			string text;
			float num;
			if ((e.Command == EventCommand.Enable || e.Command == EventCommand.Update) && e.Arguments.TryGetValue("EventCounterIntervalSec", out text) && float.TryParse(text, out num))
			{
				lock (this)
				{
					this.EnableTimer(num);
				}
			}
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x00141C58 File Offset: 0x0013FE58
		private static void EnsureEventSourceIndexAvailable(int eventSourceIndex)
		{
			if (EventCounterGroup.s_eventCounterGroups == null)
			{
				EventCounterGroup.s_eventCounterGroups = new WeakReference<EventCounterGroup>[eventSourceIndex + 1];
				return;
			}
			if (eventSourceIndex >= EventCounterGroup.s_eventCounterGroups.Length)
			{
				WeakReference<EventCounterGroup>[] array = new WeakReference<EventCounterGroup>[eventSourceIndex + 1];
				Array.Copy(EventCounterGroup.s_eventCounterGroups, 0, array, 0, EventCounterGroup.s_eventCounterGroups.Length);
				EventCounterGroup.s_eventCounterGroups = array;
			}
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x00141CA8 File Offset: 0x0013FEA8
		internal static EventCounterGroup GetEventCounterGroup(EventSource eventSource)
		{
			object obj = EventCounterGroup.s_eventCounterGroupsLock;
			EventCounterGroup eventCounterGroup2;
			lock (obj)
			{
				int num = EventListenerHelper.EventSourceIndex(eventSource);
				EventCounterGroup.EnsureEventSourceIndexAvailable(num);
				WeakReference<EventCounterGroup> weakReference = EventCounterGroup.s_eventCounterGroups[num];
				EventCounterGroup eventCounterGroup = null;
				if (weakReference == null || !weakReference.TryGetTarget(out eventCounterGroup))
				{
					eventCounterGroup = new EventCounterGroup(eventSource);
					EventCounterGroup.s_eventCounterGroups[num] = new WeakReference<EventCounterGroup>(eventCounterGroup);
				}
				eventCounterGroup2 = eventCounterGroup;
			}
			return eventCounterGroup2;
		}

		// Token: 0x060062B5 RID: 25269 RVA: 0x00141D24 File Offset: 0x0013FF24
		private void DisposeTimer()
		{
			if (this._pollingTimer != null)
			{
				this._pollingTimer.Dispose();
				this._pollingTimer = null;
			}
		}

		// Token: 0x060062B6 RID: 25270 RVA: 0x00141D40 File Offset: 0x0013FF40
		private void EnableTimer(float pollingIntervalInSeconds)
		{
			if (pollingIntervalInSeconds <= 0f)
			{
				this.DisposeTimer();
				this._pollingIntervalInMilliseconds = 0;
			}
			else if (this._pollingIntervalInMilliseconds == 0 || pollingIntervalInSeconds * 1000f < (float)this._pollingIntervalInMilliseconds)
			{
				this._pollingIntervalInMilliseconds = (int)(pollingIntervalInSeconds * 1000f);
				this.DisposeTimer();
				this._timeStampSinceCollectionStarted = DateTime.UtcNow;
				this._pollingTimer = new Timer(new TimerCallback(this.OnTimer), null, this._pollingIntervalInMilliseconds, this._pollingIntervalInMilliseconds);
			}
			this.OnTimer(null);
		}

		// Token: 0x060062B7 RID: 25271 RVA: 0x00141DC8 File Offset: 0x0013FFC8
		private void OnTimer(object state)
		{
			lock (this)
			{
				if (this._eventSource.IsEnabled())
				{
					DateTime utcNow = DateTime.UtcNow;
					TimeSpan timeSpan = utcNow - this._timeStampSinceCollectionStarted;
					foreach (EventCounter eventCounter in this._eventCounters)
					{
						EventCounterPayload eventCounterPayload = eventCounter.GetEventCounterPayload();
						eventCounterPayload.IntervalSec = (float)timeSpan.TotalSeconds;
						this._eventSource.Write<EventCounterGroup.PayloadType>("EventCounters", new EventSourceOptions
						{
							Level = EventLevel.LogAlways
						}, new EventCounterGroup.PayloadType(eventCounterPayload));
					}
					this._timeStampSinceCollectionStarted = utcNow;
				}
				else
				{
					this.DisposeTimer();
				}
			}
		}

		// Token: 0x04003128 RID: 12584
		private readonly EventSource _eventSource;

		// Token: 0x04003129 RID: 12585
		private readonly List<EventCounter> _eventCounters;

		// Token: 0x0400312A RID: 12586
		private static WeakReference<EventCounterGroup>[] s_eventCounterGroups;

		// Token: 0x0400312B RID: 12587
		private static readonly object s_eventCounterGroupsLock = new object();

		// Token: 0x0400312C RID: 12588
		private DateTime _timeStampSinceCollectionStarted;

		// Token: 0x0400312D RID: 12589
		private int _pollingIntervalInMilliseconds;

		// Token: 0x0400312E RID: 12590
		private Timer _pollingTimer;

		// Token: 0x02000A95 RID: 2709
		[EventData]
		private class PayloadType
		{
			// Token: 0x060062B9 RID: 25273 RVA: 0x00141EB8 File Offset: 0x001400B8
			public PayloadType(EventCounterPayload payload)
			{
				this.Payload = payload;
			}

			// Token: 0x170011BD RID: 4541
			// (get) Token: 0x060062BA RID: 25274 RVA: 0x00141EC7 File Offset: 0x001400C7
			// (set) Token: 0x060062BB RID: 25275 RVA: 0x00141ECF File Offset: 0x001400CF
			public EventCounterPayload Payload { get; set; }
		}
	}
}

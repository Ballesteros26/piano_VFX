using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000B09 RID: 2825
	internal sealed class ActivityFilter : IDisposable
	{
		// Token: 0x0600659B RID: 26011 RVA: 0x0014D648 File Offset: 0x0014B848
		public static void DisableFilter(ref ActivityFilter filterList, EventSource source)
		{
			if (filterList == null)
			{
				return;
			}
			ActivityFilter activityFilter = filterList;
			ActivityFilter activityFilter2 = activityFilter.m_next;
			while (activityFilter2 != null)
			{
				if (activityFilter2.m_providerGuid == source.Guid)
				{
					if (activityFilter2.m_eventId >= 0 && activityFilter2.m_eventId < source.m_eventData.Length)
					{
						EventSource.EventMetadata[] eventData = source.m_eventData;
						int eventId = activityFilter2.m_eventId;
						eventData[eventId].TriggersActivityTracking = eventData[eventId].TriggersActivityTracking - 1;
					}
					activityFilter.m_next = activityFilter2.m_next;
					activityFilter2.Dispose();
					activityFilter2 = activityFilter.m_next;
				}
				else
				{
					activityFilter = activityFilter2;
					activityFilter2 = activityFilter.m_next;
				}
			}
			if (filterList.m_providerGuid == source.Guid)
			{
				if (filterList.m_eventId >= 0 && filterList.m_eventId < source.m_eventData.Length)
				{
					EventSource.EventMetadata[] eventData2 = source.m_eventData;
					int eventId2 = filterList.m_eventId;
					eventData2[eventId2].TriggersActivityTracking = eventData2[eventId2].TriggersActivityTracking - 1;
				}
				ActivityFilter activityFilter3 = filterList;
				filterList = activityFilter3.m_next;
				activityFilter3.Dispose();
			}
			if (filterList != null)
			{
				ActivityFilter.EnsureActivityCleanupDelegate(filterList);
			}
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x0014D748 File Offset: 0x0014B948
		public static void UpdateFilter(ref ActivityFilter filterList, EventSource source, int perEventSourceSessionId, string startEvents)
		{
			ActivityFilter.DisableFilter(ref filterList, source);
			if (!string.IsNullOrEmpty(startEvents))
			{
				foreach (string text in startEvents.Split(new char[] { ' ' }))
				{
					int num = 1;
					int num2 = -1;
					int num3 = text.IndexOf(':');
					if (num3 < 0)
					{
						source.ReportOutOfBandMessage("ERROR: Invalid ActivitySamplingStartEvent specification: " + text, false);
					}
					else
					{
						string text2 = text.Substring(num3 + 1);
						if (!int.TryParse(text2, out num))
						{
							source.ReportOutOfBandMessage("ERROR: Invalid sampling frequency specification: " + text2, false);
						}
						else
						{
							text = text.Substring(0, num3);
							if (!int.TryParse(text, out num2))
							{
								num2 = -1;
								for (int j = 0; j < source.m_eventData.Length; j++)
								{
									EventSource.EventMetadata[] eventData = source.m_eventData;
									if (eventData[j].Name != null && eventData[j].Name.Length == text.Length && string.Compare(eventData[j].Name, text, StringComparison.OrdinalIgnoreCase) == 0)
									{
										num2 = eventData[j].Descriptor.EventId;
										break;
									}
								}
							}
							if (num2 < 0 || num2 >= source.m_eventData.Length)
							{
								source.ReportOutOfBandMessage("ERROR: Invalid eventId specification: " + text, false);
							}
							else
							{
								ActivityFilter.EnableFilter(ref filterList, source, perEventSourceSessionId, num2, num);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x0014D8B4 File Offset: 0x0014BAB4
		public static ActivityFilter GetFilter(ActivityFilter filterList, EventSource source)
		{
			for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
			{
				if (activityFilter.m_providerGuid == source.Guid && activityFilter.m_samplingFreq != -1)
				{
					return activityFilter;
				}
			}
			return null;
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x0014D8F0 File Offset: 0x0014BAF0
		[SecurityCritical]
		public unsafe static bool PassesActivityFilter(ActivityFilter filterList, Guid* childActivityID, bool triggeringEvent, EventSource source, int eventId)
		{
			bool flag = false;
			if (triggeringEvent)
			{
				ActivityFilter activityFilter = filterList;
				while (activityFilter != null)
				{
					if (eventId == activityFilter.m_eventId && source.Guid == activityFilter.m_providerGuid)
					{
						int curSampleCount;
						int num;
						do
						{
							curSampleCount = activityFilter.m_curSampleCount;
							if (curSampleCount <= 1)
							{
								num = activityFilter.m_samplingFreq;
							}
							else
							{
								num = curSampleCount - 1;
							}
						}
						while (Interlocked.CompareExchange(ref activityFilter.m_curSampleCount, num, curSampleCount) != curSampleCount);
						if (curSampleCount <= 1)
						{
							Guid internalCurrentThreadActivityId = EventSource.InternalCurrentThreadActivityId;
							Tuple<Guid, int> tuple;
							if (!activityFilter.m_rootActiveActivities.TryGetValue(internalCurrentThreadActivityId, out tuple))
							{
								flag = true;
								activityFilter.m_activeActivities[internalCurrentThreadActivityId] = Environment.TickCount;
								activityFilter.m_rootActiveActivities[internalCurrentThreadActivityId] = Tuple.Create<Guid, int>(source.Guid, eventId);
								break;
							}
							break;
						}
						else
						{
							Guid internalCurrentThreadActivityId2 = EventSource.InternalCurrentThreadActivityId;
							Tuple<Guid, int> tuple2;
							if (activityFilter.m_rootActiveActivities.TryGetValue(internalCurrentThreadActivityId2, out tuple2) && tuple2.Item1 == source.Guid && tuple2.Item2 == eventId)
							{
								int num2;
								activityFilter.m_activeActivities.TryRemove(internalCurrentThreadActivityId2, out num2);
								break;
							}
							break;
						}
					}
					else
					{
						activityFilter = activityFilter.m_next;
					}
				}
			}
			ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
			if (activeActivities != null)
			{
				if (!flag)
				{
					flag = !activeActivities.IsEmpty && activeActivities.ContainsKey(EventSource.InternalCurrentThreadActivityId);
				}
				if (flag && childActivityID != null && source.m_eventData[eventId].Descriptor.Opcode == 9)
				{
					ActivityFilter.FlowActivityIfNeeded(filterList, null, childActivityID);
				}
			}
			return flag;
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x0014DA54 File Offset: 0x0014BC54
		[SecuritySafeCritical]
		public static bool IsCurrentActivityActive(ActivityFilter filterList)
		{
			ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
			return activeActivities != null && activeActivities.ContainsKey(EventSource.InternalCurrentThreadActivityId);
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x0014DA7C File Offset: 0x0014BC7C
		[SecurityCritical]
		public unsafe static void FlowActivityIfNeeded(ActivityFilter filterList, Guid* currentActivityId, Guid* childActivityID)
		{
			ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
			if (currentActivityId != null && !activeActivities.ContainsKey(*currentActivityId))
			{
				return;
			}
			if (activeActivities.Count > 100000)
			{
				ActivityFilter.TrimActiveActivityStore(activeActivities);
				activeActivities[EventSource.InternalCurrentThreadActivityId] = Environment.TickCount;
			}
			activeActivities[*childActivityID] = Environment.TickCount;
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x0014DAD8 File Offset: 0x0014BCD8
		public static void UpdateKwdTriggers(ActivityFilter activityFilter, Guid sourceGuid, EventSource source, EventKeywords sessKeywords)
		{
			for (ActivityFilter activityFilter2 = activityFilter; activityFilter2 != null; activityFilter2 = activityFilter2.m_next)
			{
				if (sourceGuid == activityFilter2.m_providerGuid && (source.m_eventData[activityFilter2.m_eventId].TriggersActivityTracking > 0 || source.m_eventData[activityFilter2.m_eventId].Descriptor.Opcode == 9))
				{
					source.m_keywordTriggers |= source.m_eventData[activityFilter2.m_eventId].Descriptor.Keywords & (long)sessKeywords;
				}
			}
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x0014DB69 File Offset: 0x0014BD69
		public IEnumerable<Tuple<int, int>> GetFilterAsTuple(Guid sourceGuid)
		{
			ActivityFilter af;
			for (af = this; af != null; af = af.m_next)
			{
				if (af.m_providerGuid == sourceGuid)
				{
					yield return Tuple.Create<int, int>(af.m_eventId, af.m_samplingFreq);
				}
			}
			af = null;
			yield break;
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x0014DB80 File Offset: 0x0014BD80
		public void Dispose()
		{
			if (this.m_myActivityDelegate != null)
			{
				EventSource.s_activityDying = (Action<Guid>)Delegate.Remove(EventSource.s_activityDying, this.m_myActivityDelegate);
				this.m_myActivityDelegate = null;
			}
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x0014DBAC File Offset: 0x0014BDAC
		private ActivityFilter(EventSource source, int perEventSourceSessionId, int eventId, int samplingFreq, ActivityFilter existingFilter = null)
		{
			this.m_providerGuid = source.Guid;
			this.m_perEventSourceSessionId = perEventSourceSessionId;
			this.m_eventId = eventId;
			this.m_samplingFreq = samplingFreq;
			this.m_next = existingFilter;
			ConcurrentDictionary<Guid, int> activeActivities;
			if (existingFilter == null || (activeActivities = ActivityFilter.GetActiveActivities(existingFilter)) == null)
			{
				this.m_activeActivities = new ConcurrentDictionary<Guid, int>();
				this.m_rootActiveActivities = new ConcurrentDictionary<Guid, Tuple<Guid, int>>();
				this.m_myActivityDelegate = ActivityFilter.GetActivityDyingDelegate(this);
				EventSource.s_activityDying = (Action<Guid>)Delegate.Combine(EventSource.s_activityDying, this.m_myActivityDelegate);
				return;
			}
			this.m_activeActivities = activeActivities;
			this.m_rootActiveActivities = existingFilter.m_rootActiveActivities;
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x0014DC4C File Offset: 0x0014BE4C
		private static void EnsureActivityCleanupDelegate(ActivityFilter filterList)
		{
			if (filterList == null)
			{
				return;
			}
			for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
			{
				if (activityFilter.m_myActivityDelegate != null)
				{
					return;
				}
			}
			filterList.m_myActivityDelegate = ActivityFilter.GetActivityDyingDelegate(filterList);
			EventSource.s_activityDying = (Action<Guid>)Delegate.Combine(EventSource.s_activityDying, filterList.m_myActivityDelegate);
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x0014DC9A File Offset: 0x0014BE9A
		private static Action<Guid> GetActivityDyingDelegate(ActivityFilter filterList)
		{
			return delegate(Guid oldActivity)
			{
				int num;
				filterList.m_activeActivities.TryRemove(oldActivity, out num);
				Tuple<Guid, int> tuple;
				filterList.m_rootActiveActivities.TryRemove(oldActivity, out tuple);
			};
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x0014DCB3 File Offset: 0x0014BEB3
		private static bool EnableFilter(ref ActivityFilter filterList, EventSource source, int perEventSourceSessionId, int eventId, int samplingFreq)
		{
			filterList = new ActivityFilter(source, perEventSourceSessionId, eventId, samplingFreq, filterList);
			if (0 <= eventId && eventId < source.m_eventData.Length)
			{
				EventSource.EventMetadata[] eventData = source.m_eventData;
				eventData[eventId].TriggersActivityTracking = eventData[eventId].TriggersActivityTracking + 1;
			}
			return true;
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x0014DCF0 File Offset: 0x0014BEF0
		private static void TrimActiveActivityStore(ConcurrentDictionary<Guid, int> activities)
		{
			if (activities.Count > 100000)
			{
				KeyValuePair<Guid, int>[] array = activities.ToArray();
				int tickNow = Environment.TickCount;
				Array.Sort<KeyValuePair<Guid, int>>(array, (KeyValuePair<Guid, int> x, KeyValuePair<Guid, int> y) => (int.MaxValue & (tickNow - y.Value)) - (int.MaxValue & (tickNow - x.Value)));
				for (int i = 0; i < array.Length / 2; i++)
				{
					int num;
					activities.TryRemove(array[i].Key, out num);
				}
			}
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x0014DD5C File Offset: 0x0014BF5C
		private static ConcurrentDictionary<Guid, int> GetActiveActivities(ActivityFilter filterList)
		{
			for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
			{
				if (activityFilter.m_activeActivities != null)
				{
					return activityFilter.m_activeActivities;
				}
			}
			return null;
		}

		// Token: 0x04003293 RID: 12947
		private ConcurrentDictionary<Guid, int> m_activeActivities;

		// Token: 0x04003294 RID: 12948
		private ConcurrentDictionary<Guid, Tuple<Guid, int>> m_rootActiveActivities;

		// Token: 0x04003295 RID: 12949
		private Guid m_providerGuid;

		// Token: 0x04003296 RID: 12950
		private int m_eventId;

		// Token: 0x04003297 RID: 12951
		private int m_samplingFreq;

		// Token: 0x04003298 RID: 12952
		private int m_curSampleCount;

		// Token: 0x04003299 RID: 12953
		private int m_perEventSourceSessionId;

		// Token: 0x0400329A RID: 12954
		private const int MaxActivityTrackCount = 100000;

		// Token: 0x0400329B RID: 12955
		private ActivityFilter m_next;

		// Token: 0x0400329C RID: 12956
		private Action<Guid> m_myActivityDelegate;
	}
}

using System;
using System.Security;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AF2 RID: 2802
	internal class ActivityTracker
	{
		// Token: 0x060064F2 RID: 25842 RVA: 0x0014AEB0 File Offset: 0x001490B0
		public void OnStart(string providerName, string activityName, int task, ref Guid activityId, ref Guid relatedActivityId, EventActivityOptions options)
		{
			if (this.m_current == null)
			{
				if (this.m_checkedForEnable)
				{
					return;
				}
				this.m_checkedForEnable = true;
				this.Enable();
				if (this.m_current == null)
				{
					return;
				}
			}
			ActivityTracker.ActivityInfo activityInfo = this.m_current.Value;
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStartEnter", text);
				log.DebugFacilityMessage("OnStartEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo));
			}
			if (activityInfo != null)
			{
				if (activityInfo.m_level >= 100)
				{
					activityId = Guid.Empty;
					relatedActivityId = Guid.Empty;
					if (log.Debug)
					{
						log.DebugFacilityMessage("OnStartRET", "Fail");
					}
					return;
				}
				if ((options & EventActivityOptions.Recursive) == EventActivityOptions.None && this.FindActiveActivity(text, activityInfo) != null)
				{
					this.OnStop(providerName, activityName, task, ref activityId);
					activityInfo = this.m_current.Value;
				}
			}
			long num;
			if (activityInfo == null)
			{
				num = Interlocked.Increment(ref ActivityTracker.m_nextId);
			}
			else
			{
				num = Interlocked.Increment(ref activityInfo.m_lastChildID);
			}
			relatedActivityId = EventSource.CurrentThreadActivityId;
			ActivityTracker.ActivityInfo activityInfo2 = new ActivityTracker.ActivityInfo(text, num, activityInfo, relatedActivityId, options);
			this.m_current.Value = activityInfo2;
			activityId = activityInfo2.ActivityId;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStartRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo2));
				log.DebugFacilityMessage1("OnStartRet", activityId.ToString(), relatedActivityId.ToString());
			}
		}

		// Token: 0x060064F3 RID: 25843 RVA: 0x0014B020 File Offset: 0x00149220
		public void OnStop(string providerName, string activityName, int task, ref Guid activityId)
		{
			if (this.m_current == null)
			{
				return;
			}
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopEnter", text);
				log.DebugFacilityMessage("OnStopEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(this.m_current.Value));
			}
			ActivityTracker.ActivityInfo activityInfo;
			for (;;)
			{
				ActivityTracker.ActivityInfo value = this.m_current.Value;
				activityInfo = null;
				ActivityTracker.ActivityInfo activityInfo2 = this.FindActiveActivity(text, value);
				if (activityInfo2 == null)
				{
					break;
				}
				activityId = activityInfo2.ActivityId;
				ActivityTracker.ActivityInfo activityInfo3 = value;
				while (activityInfo3 != activityInfo2 && activityInfo3 != null)
				{
					if (activityInfo3.m_stopped != 0)
					{
						activityInfo3 = activityInfo3.m_creator;
					}
					else
					{
						if (activityInfo3.CanBeOrphan())
						{
							if (activityInfo == null)
							{
								activityInfo = activityInfo3;
							}
						}
						else
						{
							activityInfo3.m_stopped = 1;
						}
						activityInfo3 = activityInfo3.m_creator;
					}
				}
				if (Interlocked.CompareExchange(ref activityInfo2.m_stopped, 1, 0) == 0)
				{
					goto Block_9;
				}
			}
			activityId = Guid.Empty;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopRET", "Fail");
			}
			return;
			Block_9:
			if (activityInfo == null)
			{
				ActivityTracker.ActivityInfo activityInfo2;
				activityInfo = activityInfo2.m_creator;
			}
			this.m_current.Value = activityInfo;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo));
				log.DebugFacilityMessage("OnStopRet", activityId.ToString());
			}
		}

		// Token: 0x060064F4 RID: 25844 RVA: 0x0014B164 File Offset: 0x00149364
		[SecuritySafeCritical]
		public void Enable()
		{
			if (this.m_current == null)
			{
				this.m_current = new AsyncLocal<ActivityTracker.ActivityInfo>(new Action<AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo>>(this.ActivityChanging));
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x060064F5 RID: 25845 RVA: 0x0014B185 File Offset: 0x00149385
		public static ActivityTracker Instance
		{
			get
			{
				return ActivityTracker.s_activityTrackerInstance;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x060064F6 RID: 25846 RVA: 0x0014B18C File Offset: 0x0014938C
		private Guid CurrentActivityId
		{
			get
			{
				return this.m_current.Value.ActivityId;
			}
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x0014B1A0 File Offset: 0x001493A0
		private ActivityTracker.ActivityInfo FindActiveActivity(string name, ActivityTracker.ActivityInfo startLocation)
		{
			for (ActivityTracker.ActivityInfo activityInfo = startLocation; activityInfo != null; activityInfo = activityInfo.m_creator)
			{
				if (name == activityInfo.m_name && activityInfo.m_stopped == 0)
				{
					return activityInfo;
				}
			}
			return null;
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x0014B1D4 File Offset: 0x001493D4
		private string NormalizeActivityName(string providerName, string activityName, int task)
		{
			if (activityName.EndsWith("Start"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Start".Length);
			}
			else if (activityName.EndsWith("Stop"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Stop".Length);
			}
			else if (task != 0)
			{
				activityName = "task" + task.ToString();
			}
			return providerName + activityName;
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x0014B250 File Offset: 0x00149450
		private void ActivityChanging(AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo> args)
		{
			ActivityTracker.ActivityInfo activityInfo = args.CurrentValue;
			ActivityTracker.ActivityInfo previousValue = args.PreviousValue;
			if (previousValue != null && previousValue.m_creator == activityInfo && (activityInfo == null || previousValue.m_activityIdToRestore != activityInfo.ActivityId))
			{
				EventSource.SetCurrentThreadActivityId(previousValue.m_activityIdToRestore);
				return;
			}
			while (activityInfo != null)
			{
				if (activityInfo.m_stopped == 0)
				{
					EventSource.SetCurrentThreadActivityId(activityInfo.ActivityId);
					return;
				}
				activityInfo = activityInfo.m_creator;
			}
		}

		// Token: 0x04003214 RID: 12820
		private AsyncLocal<ActivityTracker.ActivityInfo> m_current;

		// Token: 0x04003215 RID: 12821
		private bool m_checkedForEnable;

		// Token: 0x04003216 RID: 12822
		private static ActivityTracker s_activityTrackerInstance = new ActivityTracker();

		// Token: 0x04003217 RID: 12823
		private static long m_nextId = 0L;

		// Token: 0x04003218 RID: 12824
		private const ushort MAX_ACTIVITY_DEPTH = 100;

		// Token: 0x02000AF3 RID: 2803
		private class ActivityInfo
		{
			// Token: 0x060064FC RID: 25852 RVA: 0x0014B2CC File Offset: 0x001494CC
			public ActivityInfo(string name, long uniqueId, ActivityTracker.ActivityInfo creator, Guid activityIDToRestore, EventActivityOptions options)
			{
				this.m_name = name;
				this.m_eventOptions = options;
				this.m_creator = creator;
				this.m_uniqueId = uniqueId;
				this.m_level = ((creator != null) ? (creator.m_level + 1) : 0);
				this.m_activityIdToRestore = activityIDToRestore;
				this.CreateActivityPathGuid(out this.m_guid, out this.m_activityPathGuidOffset);
			}

			// Token: 0x170011F6 RID: 4598
			// (get) Token: 0x060064FD RID: 25853 RVA: 0x0014B32A File Offset: 0x0014952A
			public Guid ActivityId
			{
				get
				{
					return this.m_guid;
				}
			}

			// Token: 0x060064FE RID: 25854 RVA: 0x0014B332 File Offset: 0x00149532
			public static string Path(ActivityTracker.ActivityInfo activityInfo)
			{
				if (activityInfo == null)
				{
					return "";
				}
				return ActivityTracker.ActivityInfo.Path(activityInfo.m_creator) + "/" + activityInfo.m_uniqueId;
			}

			// Token: 0x060064FF RID: 25855 RVA: 0x0014B360 File Offset: 0x00149560
			public override string ToString()
			{
				string text = "";
				if (this.m_stopped != 0)
				{
					text = ",DEAD";
				}
				return string.Concat(new string[]
				{
					this.m_name,
					"(",
					ActivityTracker.ActivityInfo.Path(this),
					text,
					")"
				});
			}

			// Token: 0x06006500 RID: 25856 RVA: 0x0014B3B2 File Offset: 0x001495B2
			public static string LiveActivities(ActivityTracker.ActivityInfo list)
			{
				if (list == null)
				{
					return "";
				}
				return list.ToString() + ";" + ActivityTracker.ActivityInfo.LiveActivities(list.m_creator);
			}

			// Token: 0x06006501 RID: 25857 RVA: 0x0014B3D8 File Offset: 0x001495D8
			public bool CanBeOrphan()
			{
				return (this.m_eventOptions & EventActivityOptions.Detachable) != EventActivityOptions.None;
			}

			// Token: 0x06006502 RID: 25858 RVA: 0x0014B3E8 File Offset: 0x001495E8
			[SecuritySafeCritical]
			private unsafe void CreateActivityPathGuid(out Guid idRet, out int activityPathGuidOffset)
			{
				fixed (Guid* ptr = &idRet)
				{
					Guid* ptr2 = ptr;
					int num = 0;
					if (this.m_creator != null)
					{
						num = this.m_creator.m_activityPathGuidOffset;
						idRet = this.m_creator.m_guid;
					}
					else
					{
						int domainID = Thread.GetDomainID();
						num = ActivityTracker.ActivityInfo.AddIdToGuid(ptr2, num, (uint)domainID, false);
					}
					activityPathGuidOffset = ActivityTracker.ActivityInfo.AddIdToGuid(ptr2, num, (uint)this.m_uniqueId, false);
					if (12 < activityPathGuidOffset)
					{
						this.CreateOverflowGuid(ptr2);
					}
				}
			}

			// Token: 0x06006503 RID: 25859 RVA: 0x0014B458 File Offset: 0x00149658
			[SecurityCritical]
			private unsafe void CreateOverflowGuid(Guid* outPtr)
			{
				for (ActivityTracker.ActivityInfo activityInfo = this.m_creator; activityInfo != null; activityInfo = activityInfo.m_creator)
				{
					if (activityInfo.m_activityPathGuidOffset <= 10)
					{
						uint num = (uint)Interlocked.Increment(ref activityInfo.m_lastChildID);
						*outPtr = activityInfo.m_guid;
						if (ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, activityInfo.m_activityPathGuidOffset, num, true) <= 12)
						{
							break;
						}
					}
				}
			}

			// Token: 0x06006504 RID: 25860 RVA: 0x0014B4B0 File Offset: 0x001496B0
			[SecurityCritical]
			private unsafe static int AddIdToGuid(Guid* outPtr, int whereToAddId, uint id, bool overflow = false)
			{
				byte* ptr = (byte*)outPtr;
				byte* ptr2 = ptr + 12;
				ptr += whereToAddId;
				if (ptr2 == ptr)
				{
					return 13;
				}
				if (0U < id && id <= 10U && !overflow)
				{
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, id);
				}
				else
				{
					uint num = 4U;
					if (id <= 255U)
					{
						num = 1U;
					}
					else if (id <= 65535U)
					{
						num = 2U;
					}
					else if (id <= 16777215U)
					{
						num = 3U;
					}
					if (overflow)
					{
						if (ptr2 == ptr + 2)
						{
							return 13;
						}
						ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 11U);
					}
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 12U + (num - 1U));
					if (ptr < ptr2 && *ptr != 0)
					{
						if (id < 4096U)
						{
							*ptr = (byte)(192U + (id >> 8));
							id &= 255U;
						}
						ptr++;
					}
					while (0U < num)
					{
						if (ptr2 == ptr)
						{
							ptr++;
							break;
						}
						*(ptr++) = (byte)id;
						id >>= 8;
						num -= 1U;
					}
				}
				*(int*)(outPtr + (IntPtr)3 * 4 / (IntPtr)sizeof(Guid)) = (int)(*(uint*)outPtr + *(uint*)(outPtr + 4 / sizeof(Guid)) + *(uint*)(outPtr + (IntPtr)2 * 4 / (IntPtr)sizeof(Guid)) + 1503500717U);
				return (int)((long)((byte*)ptr - (byte*)outPtr));
			}

			// Token: 0x06006505 RID: 25861 RVA: 0x0014B5A0 File Offset: 0x001497A0
			[SecurityCritical]
			private unsafe static void WriteNibble(ref byte* ptr, byte* endPtr, uint value)
			{
				if (*ptr != 0)
				{
					byte* ptr2 = ptr;
					ptr = ptr2 + 1;
					byte* ptr3 = ptr2;
					*ptr3 |= (byte)value;
					return;
				}
				*ptr = (byte)(value << 4);
			}

			// Token: 0x04003219 RID: 12825
			internal readonly string m_name;

			// Token: 0x0400321A RID: 12826
			private readonly long m_uniqueId;

			// Token: 0x0400321B RID: 12827
			internal readonly Guid m_guid;

			// Token: 0x0400321C RID: 12828
			internal readonly int m_activityPathGuidOffset;

			// Token: 0x0400321D RID: 12829
			internal readonly int m_level;

			// Token: 0x0400321E RID: 12830
			internal readonly EventActivityOptions m_eventOptions;

			// Token: 0x0400321F RID: 12831
			internal long m_lastChildID;

			// Token: 0x04003220 RID: 12832
			internal int m_stopped;

			// Token: 0x04003221 RID: 12833
			internal readonly ActivityTracker.ActivityInfo m_creator;

			// Token: 0x04003222 RID: 12834
			internal readonly Guid m_activityIdToRestore;

			// Token: 0x02000AF4 RID: 2804
			private enum NumberListCodes : byte
			{
				// Token: 0x04003224 RID: 12836
				End,
				// Token: 0x04003225 RID: 12837
				LastImmediateValue = 10,
				// Token: 0x04003226 RID: 12838
				PrefixCode,
				// Token: 0x04003227 RID: 12839
				MultiByte1
			}
		}
	}
}

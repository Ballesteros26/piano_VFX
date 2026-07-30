using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000146 RID: 326
	internal class EventCallbackList
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000242CB File Offset: 0x000224CB
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x000242D3 File Offset: 0x000224D3
		public int trickleDownCallbackCount { get; private set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x000242DC File Offset: 0x000224DC
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x000242E4 File Offset: 0x000224E4
		public int bubbleUpCallbackCount { get; private set; }

		// Token: 0x06000934 RID: 2356 RVA: 0x000242ED File Offset: 0x000224ED
		public EventCallbackList()
		{
			this.m_List = new List<EventCallbackFunctorBase>();
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00024312 File Offset: 0x00022512
		public EventCallbackList(EventCallbackList source)
		{
			this.m_List = new List<EventCallbackFunctorBase>(source.m_List);
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00024340 File Offset: 0x00022540
		public bool Contains(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.Find(eventTypeId, callback, phase) != null;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00024360 File Offset: 0x00022560
		public EventCallbackFunctorBase Find(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			for (int i = 0; i < this.m_List.Count; i++)
			{
				bool flag = this.m_List[i].IsEquivalentTo(eventTypeId, callback, phase);
				if (flag)
				{
					return this.m_List[i];
				}
			}
			return null;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000243B8 File Offset: 0x000225B8
		public bool Remove(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			for (int i = 0; i < this.m_List.Count; i++)
			{
				bool flag = this.m_List[i].IsEquivalentTo(eventTypeId, callback, phase);
				if (flag)
				{
					this.m_List.RemoveAt(i);
					bool flag2 = phase == CallbackPhase.TrickleDownAndTarget;
					if (flag2)
					{
						int num = this.trickleDownCallbackCount;
						this.trickleDownCallbackCount = num - 1;
					}
					else
					{
						bool flag3 = phase == CallbackPhase.TargetAndBubbleUp;
						if (flag3)
						{
							int num = this.bubbleUpCallbackCount;
							this.bubbleUpCallbackCount = num - 1;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00024450 File Offset: 0x00022650
		public void Add(EventCallbackFunctorBase item)
		{
			this.m_List.Add(item);
			bool flag = item.phase == CallbackPhase.TrickleDownAndTarget;
			if (flag)
			{
				int num = this.trickleDownCallbackCount;
				this.trickleDownCallbackCount = num + 1;
			}
			else
			{
				bool flag2 = item.phase == CallbackPhase.TargetAndBubbleUp;
				if (flag2)
				{
					int num = this.bubbleUpCallbackCount;
					this.bubbleUpCallbackCount = num + 1;
				}
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000244B0 File Offset: 0x000226B0
		public void AddRange(EventCallbackList list)
		{
			this.m_List.AddRange(list.m_List);
			foreach (EventCallbackFunctorBase eventCallbackFunctorBase in list.m_List)
			{
				bool flag = eventCallbackFunctorBase.phase == CallbackPhase.TrickleDownAndTarget;
				if (flag)
				{
					int num = this.trickleDownCallbackCount;
					this.trickleDownCallbackCount = num + 1;
				}
				else
				{
					bool flag2 = eventCallbackFunctorBase.phase == CallbackPhase.TargetAndBubbleUp;
					if (flag2)
					{
						int num = this.bubbleUpCallbackCount;
						this.bubbleUpCallbackCount = num + 1;
					}
				}
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00024558 File Offset: 0x00022758
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x1700022B RID: 555
		public EventCallbackFunctorBase this[int i]
		{
			get
			{
				return this.m_List[i];
			}
			set
			{
				this.m_List[i] = value;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000245A7 File Offset: 0x000227A7
		public void Clear()
		{
			this.m_List.Clear();
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		// Token: 0x04000417 RID: 1047
		private List<EventCallbackFunctorBase> m_List;
	}
}

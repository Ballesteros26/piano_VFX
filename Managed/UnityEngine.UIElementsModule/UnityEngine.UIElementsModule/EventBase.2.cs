using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200013D RID: 317
	public abstract class EventBase<T> : EventBase where T : EventBase<T>, new()
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x00023E41 File Offset: 0x00022041
		protected EventBase()
		{
			this.m_RefCount = 0;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00023E54 File Offset: 0x00022054
		public static long TypeId()
		{
			return EventBase<T>.s_TypeId;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00023E6C File Offset: 0x0002206C
		protected override void Init()
		{
			base.Init();
			bool flag = this.m_RefCount != 0;
			if (flag)
			{
				Debug.Log("Event improperly released.");
				this.m_RefCount = 0;
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00023EA4 File Offset: 0x000220A4
		public static T GetPooled()
		{
			T t = EventBase<T>.s_Pool.Get();
			t.Init();
			t.pooled = true;
			t.Acquire();
			return t;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00023EE8 File Offset: 0x000220E8
		internal static T GetPooled(EventBase e)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = e != null;
			if (flag)
			{
				pooled.SetTriggerEventId(e.eventId);
			}
			return pooled;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00023F20 File Offset: 0x00022120
		private static void ReleasePooled(T evt)
		{
			bool pooled = evt.pooled;
			if (pooled)
			{
				evt.Init();
				EventBase<T>.s_Pool.Release(evt);
				evt.pooled = false;
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00023F64 File Offset: 0x00022164
		internal override void Acquire()
		{
			this.m_RefCount++;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00023F78 File Offset: 0x00022178
		public sealed override void Dispose()
		{
			int num = this.m_RefCount - 1;
			this.m_RefCount = num;
			bool flag = num == 0;
			if (flag)
			{
				EventBase<T>.ReleasePooled((T)((object)this));
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00023FAC File Offset: 0x000221AC
		public override long eventTypeId
		{
			get
			{
				return EventBase<T>.s_TypeId;
			}
		}

		// Token: 0x04000407 RID: 1031
		private static readonly long s_TypeId = EventBase.RegisterEventType();

		// Token: 0x04000408 RID: 1032
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(100);

		// Token: 0x04000409 RID: 1033
		private int m_RefCount;
	}
}

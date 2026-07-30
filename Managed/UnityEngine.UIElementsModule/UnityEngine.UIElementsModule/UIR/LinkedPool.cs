using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000223 RID: 547
	internal class LinkedPool<T> where T : LinkedPoolItem<T>
	{
		// Token: 0x06001082 RID: 4226 RVA: 0x0003DB6F File Offset: 0x0003BD6F
		public LinkedPool(Func<T> createFunc, Action<T> resetAction, int limit = 10000)
		{
			Debug.Assert(createFunc != null);
			this.m_CreateFunc = createFunc;
			Debug.Assert(resetAction != null);
			this.m_ResetAction = resetAction;
			Debug.Assert(limit > 0);
			this.m_Limit = limit;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0003DBAC File Offset: 0x0003BDAC
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x0003DBB4 File Offset: 0x0003BDB4
		public int Count { get; private set; }

		// Token: 0x06001085 RID: 4229 RVA: 0x0003DBBD File Offset: 0x0003BDBD
		public void Clear()
		{
			this.m_PoolFirst = default(T);
			this.Count = 0;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0003DBD4 File Offset: 0x0003BDD4
		public T Get()
		{
			T t = this.m_PoolFirst;
			bool flag = this.m_PoolFirst != null;
			if (flag)
			{
				int num = this.Count - 1;
				this.Count = num;
				this.m_PoolFirst = t.poolNext;
				this.m_ResetAction.Invoke(t);
			}
			else
			{
				t = this.m_CreateFunc.Invoke();
			}
			return t;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0003DC40 File Offset: 0x0003BE40
		public void Return(T item)
		{
			bool flag = this.Count < this.m_Limit;
			if (flag)
			{
				item.poolNext = this.m_PoolFirst;
				this.m_PoolFirst = item;
				int num = this.Count + 1;
				this.Count = num;
			}
		}

		// Token: 0x0400074C RID: 1868
		private readonly Func<T> m_CreateFunc;

		// Token: 0x0400074D RID: 1869
		private readonly Action<T> m_ResetAction;

		// Token: 0x0400074E RID: 1870
		private readonly int m_Limit;

		// Token: 0x0400074F RID: 1871
		private T m_PoolFirst;
	}
}

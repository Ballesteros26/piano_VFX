using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000244 RID: 580
	internal class Pool<T> where T : PoolItem, new()
	{
		// Token: 0x06001138 RID: 4408 RVA: 0x00047A94 File Offset: 0x00045C94
		public T Get()
		{
			bool flag = this.m_Pool == null;
			T t;
			if (flag)
			{
				t = new T();
			}
			else
			{
				Debug.Assert(this.m_Pool != null);
				T t2 = (T)((object)this.m_Pool);
				this.m_Pool = this.m_Pool.poolNext;
				t2.poolNext = null;
				t = t2;
			}
			return t;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00047AF4 File Offset: 0x00045CF4
		public void Return(T obj)
		{
			obj.poolNext = this.m_Pool;
			this.m_Pool = obj;
		}

		// Token: 0x04000815 RID: 2069
		private PoolItem m_Pool;
	}
}

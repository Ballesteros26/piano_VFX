using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.Rendering
{
	// Token: 0x02000025 RID: 37
	public class ObjectPool<T> where T : new()
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005302 File Offset: 0x00003502
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000530A File Offset: 0x0000350A
		public int countAll { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005313 File Offset: 0x00003513
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005322 File Offset: 0x00003522
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000532F File Offset: 0x0000352F
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease, bool collectionCheck = true)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_CollectionCheck = collectionCheck;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005360 File Offset: 0x00003560
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = new T();
				int countAll = this.countAll;
				this.countAll = countAll + 1;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(t);
			}
			return t;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000053B4 File Offset: 0x000035B4
		public ObjectPool<T>.PooledObject Get(out T v)
		{
			return new ObjectPool<T>.PooledObject(v = this.Get(), this);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000053D6 File Offset: 0x000035D6
		public void Release(T element)
		{
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x040000B4 RID: 180
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040000B5 RID: 181
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x040000B6 RID: 182
		private readonly UnityAction<T> m_ActionOnRelease;

		// Token: 0x040000B7 RID: 183
		private readonly bool m_CollectionCheck = true;

		// Token: 0x020000BA RID: 186
		public struct PooledObject : IDisposable
		{
			// Token: 0x0600049E RID: 1182 RVA: 0x000113FA File Offset: 0x0000F5FA
			internal PooledObject(T value, ObjectPool<T> pool)
			{
				this.m_ToReturn = value;
				this.m_Pool = pool;
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x0001140A File Offset: 0x0000F60A
			void IDisposable.Dispose()
			{
				this.m_Pool.Release(this.m_ToReturn);
			}

			// Token: 0x04000268 RID: 616
			private readonly T m_ToReturn;

			// Token: 0x04000269 RID: 617
			private readonly ObjectPool<T> m_Pool;
		}
	}
}

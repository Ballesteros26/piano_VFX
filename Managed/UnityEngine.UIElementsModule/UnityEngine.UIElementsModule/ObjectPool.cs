using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000034 RID: 52
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005E7C File Offset: 0x0000407C
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00005E94 File Offset: 0x00004094
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = Math.Max(0, value);
				while (this.Size() > this.m_MaxSize)
				{
					this.Get();
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005ECC File Offset: 0x000040CC
		public ObjectPool(int maxSize = 100)
		{
			this.maxSize = maxSize;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005EEC File Offset: 0x000040EC
		public int Size()
		{
			return this.m_Stack.Count;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005F09 File Offset: 0x00004109
		public void Clear()
		{
			this.m_Stack.Clear();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005F18 File Offset: 0x00004118
		public T Get()
		{
			return (this.m_Stack.Count == 0) ? new T() : this.m_Stack.Pop();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005F4C File Offset: 0x0000414C
		public void Release(T element)
		{
			bool flag = this.m_Stack.Count > 0 && this.m_Stack.Peek() == element;
			if (flag)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			bool flag2 = this.m_Stack.Count < this.maxSize;
			if (flag2)
			{
				this.m_Stack.Push(element);
			}
		}

		// Token: 0x04000082 RID: 130
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x04000083 RID: 131
		private int m_MaxSize;
	}
}

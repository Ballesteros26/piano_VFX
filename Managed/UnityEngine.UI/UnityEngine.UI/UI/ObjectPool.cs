using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	// Token: 0x0200003D RID: 61
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00014EFD File Offset: 0x000130FD
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00014F05 File Offset: 0x00013105
		public int countAll { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00014F0E File Offset: 0x0001310E
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00014F1D File Offset: 0x0001311D
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00014F2A File Offset: 0x0001312A
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00014F4C File Offset: 0x0001314C
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

		// Token: 0x06000474 RID: 1140 RVA: 0x00014FA0 File Offset: 0x000131A0
		public void Release(T element)
		{
			if (this.m_Stack.Count > 0 && this.m_Stack.Peek() == element)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x04000170 RID: 368
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x04000171 RID: 369
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x04000172 RID: 370
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x02000032 RID: 50
	internal class TMP_ObjectPool<T> where T : new()
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000F401 File Offset: 0x0000D601
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000F409 File Offset: 0x0000D609
		public int countAll { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000F412 File Offset: 0x0000D612
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000F421 File Offset: 0x0000D621
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000F42E File Offset: 0x0000D62E
		public TMP_ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000F450 File Offset: 0x0000D650
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

		// Token: 0x0600025C RID: 604 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
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

		// Token: 0x0400018E RID: 398
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x0400018F RID: 399
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x04000190 RID: 400
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000259 RID: 601
	internal class InvokableCallList
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0002962C File Offset: 0x0002782C
		public int Count
		{
			get
			{
				return this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count;
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00029655 File Offset: 0x00027855
		public void AddPersistentInvokableCall(BaseInvokableCall call)
		{
			this.m_PersistentCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0002966C File Offset: 0x0002786C
		public void AddListener(BaseInvokableCall call)
		{
			this.m_RuntimeCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00029684 File Offset: 0x00027884
		public void RemoveListener(object targetObj, MethodInfo method)
		{
			List<BaseInvokableCall> list = new List<BaseInvokableCall>();
			for (int i = 0; i < this.m_RuntimeCalls.Count; i++)
			{
				bool flag = this.m_RuntimeCalls[i].Find(targetObj, method);
				if (flag)
				{
					list.Add(this.m_RuntimeCalls[i]);
				}
			}
			this.m_RuntimeCalls.RemoveAll(new Predicate<BaseInvokableCall>(list.Contains));
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000296FE File Offset: 0x000278FE
		public void Clear()
		{
			this.m_RuntimeCalls.Clear();
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00029714 File Offset: 0x00027914
		public void ClearPersistent()
		{
			this.m_PersistentCalls.Clear();
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0002972C File Offset: 0x0002792C
		public List<BaseInvokableCall> PrepareInvoke()
		{
			bool needsUpdate = this.m_NeedsUpdate;
			if (needsUpdate)
			{
				this.m_ExecutingCalls.Clear();
				this.m_ExecutingCalls.AddRange(this.m_PersistentCalls);
				this.m_ExecutingCalls.AddRange(this.m_RuntimeCalls);
				this.m_NeedsUpdate = false;
			}
			return this.m_ExecutingCalls;
		}

		// Token: 0x040007E2 RID: 2018
		private readonly List<BaseInvokableCall> m_PersistentCalls = new List<BaseInvokableCall>();

		// Token: 0x040007E3 RID: 2019
		private readonly List<BaseInvokableCall> m_RuntimeCalls = new List<BaseInvokableCall>();

		// Token: 0x040007E4 RID: 2020
		private readonly List<BaseInvokableCall> m_ExecutingCalls = new List<BaseInvokableCall>();

		// Token: 0x040007E5 RID: 2021
		private bool m_NeedsUpdate = true;
	}
}

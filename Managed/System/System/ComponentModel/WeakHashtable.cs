using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000300 RID: 768
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x060018B6 RID: 6326 RVA: 0x000690FC File Offset: 0x000672FC
		internal WeakHashtable()
			: base(WeakHashtable._comparer)
		{
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00069109 File Offset: 0x00067309
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00069111 File Offset: 0x00067311
		public override void Remove(object key)
		{
			base.Remove(key);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0006911A File Offset: 0x0006731A
		public void SetWeak(object key, object value)
		{
			this.ScavengeKeys();
			this[new WeakHashtable.EqualityWeakReference(key)] = value;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00069130 File Offset: 0x00067330
		private void ScavengeKeys()
		{
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			if (this._lastHashCount == 0)
			{
				this._lastHashCount = count;
				return;
			}
			long totalMemory = GC.GetTotalMemory(false);
			if (this._lastGlobalMem == 0L)
			{
				this._lastGlobalMem = totalMemory;
				return;
			}
			float num = (float)(totalMemory - this._lastGlobalMem) / (float)this._lastGlobalMem;
			float num2 = (float)(count - this._lastHashCount) / (float)this._lastHashCount;
			if (num < 0f && num2 >= 0f)
			{
				ArrayList arrayList = null;
				foreach (object obj in this.Keys)
				{
					WeakReference weakReference = obj as WeakReference;
					if (weakReference != null && !weakReference.IsAlive)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(weakReference);
					}
				}
				if (arrayList != null)
				{
					foreach (object obj2 in arrayList)
					{
						this.Remove(obj2);
					}
				}
			}
			this._lastGlobalMem = totalMemory;
			this._lastHashCount = count;
		}

		// Token: 0x04001449 RID: 5193
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x0400144A RID: 5194
		private long _lastGlobalMem;

		// Token: 0x0400144B RID: 5195
		private int _lastHashCount;

		// Token: 0x02000301 RID: 769
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x060018BC RID: 6332 RVA: 0x0006927C File Offset: 0x0006747C
			bool IEqualityComparer.Equals(object x, object y)
			{
				if (x == null)
				{
					return y == null;
				}
				if (y != null && x.GetHashCode() == y.GetHashCode())
				{
					WeakReference weakReference = x as WeakReference;
					WeakReference weakReference2 = y as WeakReference;
					if (weakReference != null)
					{
						if (!weakReference.IsAlive)
						{
							return false;
						}
						x = weakReference.Target;
					}
					if (weakReference2 != null)
					{
						if (!weakReference2.IsAlive)
						{
							return false;
						}
						y = weakReference2.Target;
					}
					return x == y;
				}
				return false;
			}

			// Token: 0x060018BD RID: 6333 RVA: 0x000692E0 File Offset: 0x000674E0
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x02000302 RID: 770
		private sealed class EqualityWeakReference : WeakReference
		{
			// Token: 0x060018BF RID: 6335 RVA: 0x000692E8 File Offset: 0x000674E8
			internal EqualityWeakReference(object o)
				: base(o)
			{
				this._hashCode = o.GetHashCode();
			}

			// Token: 0x060018C0 RID: 6336 RVA: 0x000692FD File Offset: 0x000674FD
			public override bool Equals(object o)
			{
				return o != null && o.GetHashCode() == this._hashCode && (o == this || (this.IsAlive && o == this.Target));
			}

			// Token: 0x060018C1 RID: 6337 RVA: 0x0006932C File Offset: 0x0006752C
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x0400144C RID: 5196
			private int _hashCode;
		}
	}
}

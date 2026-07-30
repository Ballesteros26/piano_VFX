using System;

namespace System.Threading
{
	// Token: 0x02000455 RID: 1109
	internal class SparselyPopulatedArrayFragment<T> where T : class
	{
		// Token: 0x0600350A RID: 13578 RVA: 0x000C426E File Offset: 0x000C246E
		internal SparselyPopulatedArrayFragment(int size)
			: this(size, null)
		{
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000C4278 File Offset: 0x000C2478
		internal SparselyPopulatedArrayFragment(int size, SparselyPopulatedArrayFragment<T> prev)
		{
			this.m_elements = new T[size];
			this.m_freeCount = size;
			this.m_prev = prev;
		}

		// Token: 0x170008DF RID: 2271
		internal T this[int index]
		{
			get
			{
				return Volatile.Read<T>(ref this.m_elements[index]);
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000C42B1 File Offset: 0x000C24B1
		internal int Length
		{
			get
			{
				return this.m_elements.Length;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x000C42BB File Offset: 0x000C24BB
		internal SparselyPopulatedArrayFragment<T> Prev
		{
			get
			{
				return this.m_prev;
			}
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000C42C8 File Offset: 0x000C24C8
		internal T SafeAtomicRemove(int index, T expectedElement)
		{
			T t = Interlocked.CompareExchange<T>(ref this.m_elements[index], default(T), expectedElement);
			if (t != null)
			{
				this.m_freeCount++;
			}
			return t;
		}

		// Token: 0x04001C48 RID: 7240
		internal readonly T[] m_elements;

		// Token: 0x04001C49 RID: 7241
		internal volatile int m_freeCount;

		// Token: 0x04001C4A RID: 7242
		internal volatile SparselyPopulatedArrayFragment<T> m_next;

		// Token: 0x04001C4B RID: 7243
		internal volatile SparselyPopulatedArrayFragment<T> m_prev;
	}
}

using System;

namespace System.Threading
{
	// Token: 0x02000453 RID: 1107
	internal class SparselyPopulatedArray<T> where T : class
	{
		// Token: 0x06003504 RID: 13572 RVA: 0x000C40BD File Offset: 0x000C22BD
		internal SparselyPopulatedArray(int initialSize)
		{
			this.m_tail = new SparselyPopulatedArrayFragment<T>(initialSize);
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x000C40D3 File Offset: 0x000C22D3
		internal SparselyPopulatedArrayFragment<T> Tail
		{
			get
			{
				return this.m_tail;
			}
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000C40E0 File Offset: 0x000C22E0
		internal SparselyPopulatedArrayAddInfo<T> Add(T element)
		{
			SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment2;
			int num2;
			for (;;)
			{
				SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment = this.m_tail;
				while (sparselyPopulatedArrayFragment.m_next != null)
				{
					sparselyPopulatedArrayFragment = (this.m_tail = sparselyPopulatedArrayFragment.m_next);
				}
				for (sparselyPopulatedArrayFragment2 = sparselyPopulatedArrayFragment; sparselyPopulatedArrayFragment2 != null; sparselyPopulatedArrayFragment2 = sparselyPopulatedArrayFragment2.m_prev)
				{
					if (sparselyPopulatedArrayFragment2.m_freeCount < 1)
					{
						sparselyPopulatedArrayFragment2.m_freeCount--;
					}
					if (sparselyPopulatedArrayFragment2.m_freeCount > 0 || sparselyPopulatedArrayFragment2.m_freeCount < -10)
					{
						int length = sparselyPopulatedArrayFragment2.Length;
						int num = (length - sparselyPopulatedArrayFragment2.m_freeCount) % length;
						if (num < 0)
						{
							num = 0;
							sparselyPopulatedArrayFragment2.m_freeCount--;
						}
						for (int i = 0; i < length; i++)
						{
							num2 = (num + i) % length;
							if (sparselyPopulatedArrayFragment2.m_elements[num2] == null && Interlocked.CompareExchange<T>(ref sparselyPopulatedArrayFragment2.m_elements[num2], element, default(T)) == null)
							{
								goto Block_5;
							}
						}
					}
				}
				SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment3 = new SparselyPopulatedArrayFragment<T>((sparselyPopulatedArrayFragment.m_elements.Length == 4096) ? 4096 : (sparselyPopulatedArrayFragment.m_elements.Length * 2), sparselyPopulatedArrayFragment);
				if (Interlocked.CompareExchange<SparselyPopulatedArrayFragment<T>>(ref sparselyPopulatedArrayFragment.m_next, sparselyPopulatedArrayFragment3, null) == null)
				{
					this.m_tail = sparselyPopulatedArrayFragment3;
				}
			}
			Block_5:
			int num3 = sparselyPopulatedArrayFragment2.m_freeCount - 1;
			sparselyPopulatedArrayFragment2.m_freeCount = ((num3 > 0) ? num3 : 0);
			return new SparselyPopulatedArrayAddInfo<T>(sparselyPopulatedArrayFragment2, num2);
		}

		// Token: 0x04001C45 RID: 7237
		private volatile SparselyPopulatedArrayFragment<T> m_tail;
	}
}

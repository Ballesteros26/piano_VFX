using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000204 RID: 516
	internal class FixedMaxHeap<TElement>
	{
		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002AD69 File Offset: 0x00028F69
		internal FixedMaxHeap(int maximumSize)
			: this(maximumSize, Util.GetDefaultComparer<TElement>())
		{
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002AD77 File Offset: 0x00028F77
		internal FixedMaxHeap(int maximumSize, IComparer<TElement> comparer)
		{
			this._elements = new TElement[maximumSize];
			this._comparer = comparer;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0002AD92 File Offset: 0x00028F92
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0002AD9A File Offset: 0x00028F9A
		internal int Size
		{
			get
			{
				return this._elements.Length;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002ADA4 File Offset: 0x00028FA4
		internal TElement MaxValue
		{
			get
			{
				if (this._count == 0)
				{
					throw new InvalidOperationException("Sequence contains no elements");
				}
				return this._elements[0];
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002ADC5 File Offset: 0x00028FC5
		internal void Clear()
		{
			this._count = 0;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002ADD0 File Offset: 0x00028FD0
		internal bool Insert(TElement e)
		{
			if (this._count < this._elements.Length)
			{
				this._elements[this._count] = e;
				this._count++;
				this.HeapifyLastLeaf();
				return true;
			}
			if (this._comparer.Compare(e, this._elements[0]) < 0)
			{
				this._elements[0] = e;
				this.HeapifyRoot();
				return true;
			}
			return false;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002AE46 File Offset: 0x00029046
		internal void ReplaceMax(TElement newValue)
		{
			this._elements[0] = newValue;
			this.HeapifyRoot();
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002AE5B File Offset: 0x0002905B
		internal void RemoveMax()
		{
			this._count--;
			if (this._count > 0)
			{
				this._elements[0] = this._elements[this._count];
				this.HeapifyRoot();
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002AE98 File Offset: 0x00029098
		private void Swap(int i, int j)
		{
			TElement telement = this._elements[i];
			this._elements[i] = this._elements[j];
			this._elements[j] = telement;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002AED8 File Offset: 0x000290D8
		private void HeapifyRoot()
		{
			int i = 0;
			int count = this._count;
			while (i < count)
			{
				int num = (i + 1) * 2 - 1;
				int num2 = num + 1;
				if (num < count && this._comparer.Compare(this._elements[i], this._elements[num]) < 0)
				{
					if (num2 < count && this._comparer.Compare(this._elements[num], this._elements[num2]) < 0)
					{
						this.Swap(i, num2);
						i = num2;
					}
					else
					{
						this.Swap(i, num);
						i = num;
					}
				}
				else
				{
					if (num2 >= count || this._comparer.Compare(this._elements[i], this._elements[num2]) >= 0)
					{
						break;
					}
					this.Swap(i, num2);
					i = num2;
				}
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002AFA8 File Offset: 0x000291A8
		private void HeapifyLastLeaf()
		{
			int num;
			for (int i = this._count - 1; i > 0; i = num)
			{
				num = (i + 1) / 2 - 1;
				if (this._comparer.Compare(this._elements[i], this._elements[num]) <= 0)
				{
					break;
				}
				this.Swap(i, num);
			}
		}

		// Token: 0x04000800 RID: 2048
		private TElement[] _elements;

		// Token: 0x04000801 RID: 2049
		private int _count;

		// Token: 0x04000802 RID: 2050
		private IComparer<TElement> _comparer;
	}
}

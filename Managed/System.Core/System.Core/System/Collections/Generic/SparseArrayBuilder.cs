using System;

namespace System.Collections.Generic
{
	// Token: 0x0200034E RID: 846
	internal struct SparseArrayBuilder<T>
	{
		// Token: 0x060019B1 RID: 6577 RVA: 0x00054573 File Offset: 0x00052773
		public SparseArrayBuilder(bool initialize)
		{
			this = default(SparseArrayBuilder<T>);
			this._builder = new LargeArrayBuilder<T>(true);
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00054588 File Offset: 0x00052788
		public int Count
		{
			get
			{
				return checked(this._builder.Count + this._reservedCount);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0005459C File Offset: 0x0005279C
		public ArrayBuilder<Marker> Markers
		{
			get
			{
				return this._markers;
			}
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000545A4 File Offset: 0x000527A4
		public void Add(T item)
		{
			this._builder.Add(item);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000545B2 File Offset: 0x000527B2
		public void AddRange(IEnumerable<T> items)
		{
			this._builder.AddRange(items);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x000545C0 File Offset: 0x000527C0
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			CopyPosition copyPosition = CopyPosition.Start;
			for (int i = 0; i < this._markers.Count; i++)
			{
				Marker marker = this._markers[i];
				int num2 = Math.Min(marker.Index - num, count);
				if (num2 > 0)
				{
					copyPosition = this._builder.CopyTo(copyPosition, array, arrayIndex, num2);
					arrayIndex += num2;
					num += num2;
					count -= num2;
				}
				if (count == 0)
				{
					return;
				}
				int num3 = Math.Min(marker.Count, count);
				arrayIndex += num3;
				num += num3;
				count -= num3;
			}
			if (count > 0)
			{
				this._builder.CopyTo(copyPosition, array, arrayIndex, count);
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00054668 File Offset: 0x00052868
		public void Reserve(int count)
		{
			this._markers.Add(new Marker(count, this.Count));
			checked
			{
				this._reservedCount += count;
			}
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00054690 File Offset: 0x00052890
		public bool ReserveOrAdd(IEnumerable<T> items)
		{
			int num;
			if (EnumerableHelpers.TryGetCount<T>(items, out num))
			{
				if (num > 0)
				{
					this.Reserve(num);
					return true;
				}
			}
			else
			{
				this.AddRange(items);
			}
			return false;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000546BC File Offset: 0x000528BC
		public T[] ToArray()
		{
			if (this._markers.Count == 0)
			{
				return this._builder.ToArray();
			}
			T[] array = new T[this.Count];
			this.CopyTo(array, 0, array.Length);
			return array;
		}

		// Token: 0x04000B6A RID: 2922
		private LargeArrayBuilder<T> _builder;

		// Token: 0x04000B6B RID: 2923
		private ArrayBuilder<Marker> _markers;

		// Token: 0x04000B6C RID: 2924
		private int _reservedCount;
	}
}

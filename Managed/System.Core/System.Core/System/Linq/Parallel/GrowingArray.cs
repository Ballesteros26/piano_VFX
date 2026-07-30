using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000205 RID: 517
	internal class GrowingArray<T>
	{
		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002AFFC File Offset: 0x000291FC
		internal GrowingArray()
		{
			this._array = new T[1024];
			this._count = 0;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002B01B File Offset: 0x0002921B
		internal T[] InternalArray
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0002B023 File Offset: 0x00029223
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002B02C File Offset: 0x0002922C
		internal void Add(T element)
		{
			if (this._count >= this._array.Length)
			{
				this.GrowArray(2 * this._array.Length);
			}
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = element;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002B078 File Offset: 0x00029278
		private void GrowArray(int newSize)
		{
			T[] array = new T[newSize];
			this._array.CopyTo(array, 0);
			this._array = array;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002B0A0 File Offset: 0x000292A0
		internal void CopyFrom(T[] otherArray, int otherCount)
		{
			if (this._count + otherCount > this._array.Length)
			{
				this.GrowArray(this._count + otherCount);
			}
			Array.Copy(otherArray, 0, this._array, this._count, otherCount);
			this._count += otherCount;
		}

		// Token: 0x04000803 RID: 2051
		private T[] _array;

		// Token: 0x04000804 RID: 2052
		private int _count;

		// Token: 0x04000805 RID: 2053
		private const int DEFAULT_ARRAY_SIZE = 1024;
	}
}

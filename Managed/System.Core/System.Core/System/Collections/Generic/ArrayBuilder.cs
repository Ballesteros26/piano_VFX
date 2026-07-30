using System;

namespace System.Collections.Generic
{
	// Token: 0x0200034A RID: 842
	internal struct ArrayBuilder<T>
	{
		// Token: 0x06001999 RID: 6553 RVA: 0x00054177 File Offset: 0x00052377
		public ArrayBuilder(int capacity)
		{
			this = default(ArrayBuilder<T>);
			if (capacity > 0)
			{
				this._array = new T[capacity];
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x00054190 File Offset: 0x00052390
		public int Capacity
		{
			get
			{
				T[] array = this._array;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x000541A0 File Offset: 0x000523A0
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000488 RID: 1160
		public T this[int index]
		{
			get
			{
				return this._array[index];
			}
			set
			{
				this._array[index] = value;
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x000541C5 File Offset: 0x000523C5
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000541EA File Offset: 0x000523EA
		public T First()
		{
			return this._array[0];
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000541F8 File Offset: 0x000523F8
		public T Last()
		{
			return this._array[this._count - 1];
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00054210 File Offset: 0x00052410
		public T[] ToArray()
		{
			if (this._count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = this._array;
			if (this._count < array.Length)
			{
				array = new T[this._count];
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			return array;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00054260 File Offset: 0x00052460
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0005428C File Offset: 0x0005248C
		private void EnsureCapacity(int minimum)
		{
			int capacity = this.Capacity;
			int num = ((capacity == 0) ? 4 : (2 * capacity));
			if (num > 2146435071)
			{
				num = Math.Max(capacity + 1, 2146435071);
			}
			num = Math.Max(num, minimum);
			T[] array = new T[num];
			if (this._count > 0)
			{
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			this._array = array;
		}

		// Token: 0x04000B63 RID: 2915
		private const int DefaultCapacity = 4;

		// Token: 0x04000B64 RID: 2916
		private const int MaxCoreClrArrayLength = 2146435071;

		// Token: 0x04000B65 RID: 2917
		private T[] _array;

		// Token: 0x04000B66 RID: 2918
		private int _count;
	}
}

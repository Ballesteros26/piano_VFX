using System;

namespace System.Collections.Generic
{
	// Token: 0x02000713 RID: 1811
	internal struct ArrayBuilder<T>
	{
		// Token: 0x0600391C RID: 14620 RVA: 0x000D0D4F File Offset: 0x000CEF4F
		public ArrayBuilder(int capacity)
		{
			this = default(ArrayBuilder<T>);
			if (capacity > 0)
			{
				this._array = new T[capacity];
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000D0D68 File Offset: 0x000CEF68
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

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x0600391E RID: 14622 RVA: 0x000D0D78 File Offset: 0x000CEF78
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000DD4 RID: 3540
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

		// Token: 0x06003921 RID: 14625 RVA: 0x000D0D9D File Offset: 0x000CEF9D
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000D0DC2 File Offset: 0x000CEFC2
		public T First()
		{
			return this._array[0];
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000D0DD0 File Offset: 0x000CEFD0
		public T Last()
		{
			return this._array[this._count - 1];
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000D0DE8 File Offset: 0x000CEFE8
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

		// Token: 0x06003925 RID: 14629 RVA: 0x000D0E38 File Offset: 0x000CF038
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000D0E64 File Offset: 0x000CF064
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

		// Token: 0x04002C8A RID: 11402
		private const int DefaultCapacity = 4;

		// Token: 0x04002C8B RID: 11403
		private const int MaxCoreClrArrayLength = 2146435071;

		// Token: 0x04002C8C RID: 11404
		private T[] _array;

		// Token: 0x04002C8D RID: 11405
		private int _count;
	}
}

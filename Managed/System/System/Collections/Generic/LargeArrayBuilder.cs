using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000716 RID: 1814
	internal struct LargeArrayBuilder<T>
	{
		// Token: 0x06003931 RID: 14641 RVA: 0x000D10F8 File Offset: 0x000CF2F8
		public LargeArrayBuilder(bool initialize)
		{
			this = new LargeArrayBuilder<T>(int.MaxValue);
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000D1108 File Offset: 0x000CF308
		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(LargeArrayBuilder<T>);
			this._first = (this._current = Array.Empty<T>());
			this._maxCapacity = maxCapacity;
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003933 RID: 14643 RVA: 0x000D1137 File Offset: 0x000CF337
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000D1140 File Offset: 0x000CF340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(T item)
		{
			if (this._index == this._current.Length)
			{
				this.AllocateBuffer();
			}
			T[] current = this._current;
			int index = this._index;
			this._index = index + 1;
			current[index] = item;
			this._count++;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000D1190 File Offset: 0x000CF390
		public void AddRange(IEnumerable<T> items)
		{
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				T[] array = this._current;
				int num = this._index;
				while (enumerator.MoveNext())
				{
					if (num == array.Length)
					{
						this._count += num - this._index;
						this._index = num;
						this.AllocateBuffer();
						array = this._current;
						num = this._index;
					}
					array[num++] = enumerator.Current;
				}
				this._count += num - this._index;
				this._index = num;
			}
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000D123C File Offset: 0x000CF43C
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			while (count > 0)
			{
				T[] buffer = this.GetBuffer(num);
				int num2 = Math.Min(count, buffer.Length);
				Array.Copy(buffer, 0, array, arrayIndex, num2);
				count -= num2;
				arrayIndex += num2;
				num++;
			}
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000D127C File Offset: 0x000CF47C
		public CopyPosition CopyTo(CopyPosition position, T[] array, int arrayIndex, int count)
		{
			LargeArrayBuilder<T>.<>c__DisplayClass15_0 CS$<>8__locals1;
			CS$<>8__locals1.count = count;
			CS$<>8__locals1.array = array;
			CS$<>8__locals1.arrayIndex = arrayIndex;
			int num = position.Row;
			int column = position.Column;
			T[] array2 = this.GetBuffer(num);
			int num2 = LargeArrayBuilder<T>.<CopyTo>g__CopyToCore|15_0(array2, column, ref CS$<>8__locals1);
			if (CS$<>8__locals1.count == 0)
			{
				return new CopyPosition(num, column + num2).Normalize(array2.Length);
			}
			do
			{
				array2 = this.GetBuffer(++num);
				num2 = LargeArrayBuilder<T>.<CopyTo>g__CopyToCore|15_0(array2, column, ref CS$<>8__locals1);
			}
			while (CS$<>8__locals1.count > 0);
			return new CopyPosition(num, num2).Normalize(array2.Length);
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000D1318 File Offset: 0x000CF518
		public T[] GetBuffer(int index)
		{
			if (index == 0)
			{
				return this._first;
			}
			if (index > this._buffers.Count)
			{
				return this._current;
			}
			return this._buffers[index - 1];
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000D1347 File Offset: 0x000CF547
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SlowAdd(T item)
		{
			this.Add(item);
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000D1350 File Offset: 0x000CF550
		public T[] ToArray()
		{
			T[] array;
			if (this.TryMove(out array))
			{
				return array;
			}
			array = new T[this._count];
			this.CopyTo(array, 0, this._count);
			return array;
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000D1384 File Offset: 0x000CF584
		public bool TryMove(out T[] array)
		{
			array = this._first;
			return this._count == this._first.Length;
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000D13A0 File Offset: 0x000CF5A0
		private void AllocateBuffer()
		{
			if (this._count < 8)
			{
				int num = Math.Min((this._count == 0) ? 4 : (this._count * 2), this._maxCapacity);
				this._current = new T[num];
				Array.Copy(this._first, 0, this._current, 0, this._count);
				this._first = this._current;
				return;
			}
			int num2;
			if (this._count == 8)
			{
				num2 = 8;
			}
			else
			{
				this._buffers.Add(this._current);
				num2 = Math.Min(this._count, this._maxCapacity - this._count);
			}
			this._current = new T[num2];
			this._index = 0;
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000D1454 File Offset: 0x000CF654
		[CompilerGenerated]
		internal static int <CopyTo>g__CopyToCore|15_0(T[] sourceBuffer, int sourceIndex, ref LargeArrayBuilder<T>.<>c__DisplayClass15_0 A_2)
		{
			int num = Math.Min(sourceBuffer.Length - sourceIndex, A_2.count);
			Array.Copy(sourceBuffer, sourceIndex, A_2.array, A_2.arrayIndex, num);
			A_2.arrayIndex += num;
			A_2.count -= num;
			return num;
		}

		// Token: 0x04002C90 RID: 11408
		private const int StartingCapacity = 4;

		// Token: 0x04002C91 RID: 11409
		private const int ResizeLimit = 8;

		// Token: 0x04002C92 RID: 11410
		private readonly int _maxCapacity;

		// Token: 0x04002C93 RID: 11411
		private T[] _first;

		// Token: 0x04002C94 RID: 11412
		private ArrayBuilder<T[]> _buffers;

		// Token: 0x04002C95 RID: 11413
		private T[] _current;

		// Token: 0x04002C96 RID: 11414
		private int _index;

		// Token: 0x04002C97 RID: 11415
		private int _count;
	}
}

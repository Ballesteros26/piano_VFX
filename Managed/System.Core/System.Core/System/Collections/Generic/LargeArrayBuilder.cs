using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000348 RID: 840
	internal struct LargeArrayBuilder<T>
	{
		// Token: 0x0600198C RID: 6540 RVA: 0x00053DCC File Offset: 0x00051FCC
		public LargeArrayBuilder(bool initialize)
		{
			this = new LargeArrayBuilder<T>(int.MaxValue);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00053DDC File Offset: 0x00051FDC
		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(LargeArrayBuilder<T>);
			this._first = (this._current = Array.Empty<T>());
			this._maxCapacity = maxCapacity;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x00053E0B File Offset: 0x0005200B
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00053E14 File Offset: 0x00052014
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

		// Token: 0x06001990 RID: 6544 RVA: 0x00053E64 File Offset: 0x00052064
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

		// Token: 0x06001991 RID: 6545 RVA: 0x00053F10 File Offset: 0x00052110
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

		// Token: 0x06001992 RID: 6546 RVA: 0x00053F50 File Offset: 0x00052150
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
				num2 = LargeArrayBuilder<T>.<CopyTo>g__CopyToCore|15_0(array2, 0, ref CS$<>8__locals1);
			}
			while (CS$<>8__locals1.count > 0);
			return new CopyPosition(num, num2).Normalize(array2.Length);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00053FEC File Offset: 0x000521EC
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

		// Token: 0x06001994 RID: 6548 RVA: 0x0005401B File Offset: 0x0005221B
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SlowAdd(T item)
		{
			this.Add(item);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00054024 File Offset: 0x00052224
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

		// Token: 0x06001996 RID: 6550 RVA: 0x00054058 File Offset: 0x00052258
		public bool TryMove(out T[] array)
		{
			array = this._first;
			return this._count == this._first.Length;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00054074 File Offset: 0x00052274
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

		// Token: 0x06001998 RID: 6552 RVA: 0x00054128 File Offset: 0x00052328
		[CompilerGenerated]
		internal static int <CopyTo>g__CopyToCore|15_0(T[] sourceBuffer, int sourceIndex, ref LargeArrayBuilder<T>.<>c__DisplayClass15_0 A_2)
		{
			int num = Math.Min(sourceBuffer.Length - sourceIndex, A_2.count);
			Array.Copy(sourceBuffer, sourceIndex, A_2.array, A_2.arrayIndex, num);
			A_2.arrayIndex += num;
			A_2.count -= num;
			return num;
		}

		// Token: 0x04000B58 RID: 2904
		private const int StartingCapacity = 4;

		// Token: 0x04000B59 RID: 2905
		private const int ResizeLimit = 8;

		// Token: 0x04000B5A RID: 2906
		private readonly int _maxCapacity;

		// Token: 0x04000B5B RID: 2907
		private T[] _first;

		// Token: 0x04000B5C RID: 2908
		private ArrayBuilder<T[]> _buffers;

		// Token: 0x04000B5D RID: 2909
		private T[] _current;

		// Token: 0x04000B5E RID: 2910
		private int _index;

		// Token: 0x04000B5F RID: 2911
		private int _count;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001BE RID: 446
	internal sealed class CircularBuffer<T>
	{
		// Token: 0x06000AFA RID: 2810 RVA: 0x00024130 File Offset: 0x00022330
		public CircularBuffer(int capacity)
		{
			this._buffer = new T[capacity];
			this._capacity = capacity;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00024152 File Offset: 0x00022352
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0002415A File Offset: 0x0002235A
		public bool IsFull { get; private set; }

		// Token: 0x06000AFD RID: 2813 RVA: 0x00024164 File Offset: 0x00022364
		public void Add(T value)
		{
			if (this._position >= this.GetItemsCount())
			{
				this._position = Math.Min(this._position + 1, this._capacity);
			}
			if (this.IsFull || this._index == this._capacity - 1)
			{
				this._start = (this._start + 1) % this._capacity;
				this.IsFull = true;
			}
			this._index = (this._index + 1) % this._capacity;
			this._buffer[this._index] = value;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000241F4 File Offset: 0x000223F4
		public T[] MovePositionForward(int offset)
		{
			T[] array = this.GetItems().Skip(this._position).Take(offset)
				.ToArray<T>();
			this._position += array.Length;
			return array;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002422F File Offset: 0x0002242F
		public void MovePositionBack(int offset)
		{
			if (offset > this._position)
			{
				throw new InvalidOperationException("Failed to move position back beyond the start of the buffer.");
			}
			this._position -= offset;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00024253 File Offset: 0x00022453
		private int GetItemsCount()
		{
			if (!this.IsFull)
			{
				return this._index + 1;
			}
			return this._capacity;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002426C File Offset: 0x0002246C
		private IEnumerable<T> GetItems()
		{
			IEnumerable<T> enumerable = Enumerable.Empty<T>();
			if (this.IsFull)
			{
				if (this._start == 0)
				{
					return this._buffer;
				}
				enumerable = this.GetItems(this._start, this._capacity - 1);
			}
			return enumerable.Concat(this.GetItems(0, this._index));
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000242BE File Offset: 0x000224BE
		private IEnumerable<T> GetItems(int start, int end)
		{
			int num;
			for (int i = start; i <= end; i = num + 1)
			{
				yield return this._buffer[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x040009A4 RID: 2468
		private readonly int _capacity;

		// Token: 0x040009A5 RID: 2469
		private readonly T[] _buffer;

		// Token: 0x040009A6 RID: 2470
		private int _start;

		// Token: 0x040009A7 RID: 2471
		private int _index = -1;

		// Token: 0x040009A8 RID: 2472
		private int _position;
	}
}

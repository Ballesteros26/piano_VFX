using System;
using System.Collections;
using System.Collections.Generic;

namespace System
{
	/// <summary>Delimits a section of a one-dimensional array.</summary>
	/// <typeparam name="T">The type of the elements in the array segment.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012A RID: 298
	[Serializable]
	public struct ArraySegment<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArraySegment`1" /> structure that delimits all the elements in the specified array.</summary>
		/// <param name="array">The array to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x06000A61 RID: 2657 RVA: 0x00032BB0 File Offset: 0x00030DB0
		public ArraySegment(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this._array = array;
			this._offset = 0;
			this._count = array.Length;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArraySegment`1" /> structure that delimits the specified range of the elements in the specified array.</summary>
		/// <param name="array">The array containing the range of elements to delimit.</param>
		/// <param name="offset">The zero-based index of the first element in the range.</param>
		/// <param name="count">The number of elements in the range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> do not specify a valid range in <paramref name="array" />.</exception>
		// Token: 0x06000A62 RID: 2658 RVA: 0x00032BD8 File Offset: 0x00030DD8
		public ArraySegment(T[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			this._array = array;
			this._offset = offset;
			this._count = count;
		}

		/// <summary>Gets the original array containing the range of elements that the array segment delimits.</summary>
		/// <returns>The original array that was passed to the constructor, and that contains the range delimited by the <see cref="T:System.ArraySegment`1" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00032C52 File Offset: 0x00030E52
		public T[] Array
		{
			get
			{
				return this._array;
			}
		}

		/// <summary>Gets the position of the first element in the range delimited by the array segment, relative to the start of the original array.</summary>
		/// <returns>The position of the first element in the range delimited by the <see cref="T:System.ArraySegment`1" />, relative to the start of the original array.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00032C5A File Offset: 0x00030E5A
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		/// <summary>Gets the number of elements in the range delimited by the array segment.</summary>
		/// <returns>The number of elements in the range delimited by the <see cref="T:System.ArraySegment`1" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00032C62 File Offset: 0x00030E62
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A66 RID: 2662 RVA: 0x00032C6A File Offset: 0x00030E6A
		public override int GetHashCode()
		{
			if (this._array != null)
			{
				return this._array.GetHashCode() ^ this._offset ^ this._count;
			}
			return 0;
		}

		/// <summary>Determines whether the specified object is equal to the current instance.</summary>
		/// <returns>true if the specified object is a <see cref="T:System.ArraySegment`1" /> structure and is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to be compared with the current instance.</param>
		// Token: 0x06000A67 RID: 2663 RVA: 0x00032C8F File Offset: 0x00030E8F
		public override bool Equals(object obj)
		{
			return obj is ArraySegment<T> && this.Equals((ArraySegment<T>)obj);
		}

		/// <summary>Determines whether the specified <see cref="T:System.ArraySegment`1" /> structure is equal to the current instance.</summary>
		/// <returns>true if the specified <see cref="T:System.ArraySegment`1" /> structure is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ArraySegment`1" /> structure to be compared with the current instance.</param>
		// Token: 0x06000A68 RID: 2664 RVA: 0x00032CA7 File Offset: 0x00030EA7
		public bool Equals(ArraySegment<T> obj)
		{
			return obj._array == this._array && obj._offset == this._offset && obj._count == this._count;
		}

		/// <summary>Indicates whether two <see cref="T:System.ArraySegment`1" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.ArraySegment`1" /> structure on the left side of the equality operator.</param>
		/// <param name="b">The <see cref="T:System.ArraySegment`1" /> structure on the right side of the equality operator.</param>
		// Token: 0x06000A69 RID: 2665 RVA: 0x00032CD5 File Offset: 0x00030ED5
		public static bool operator ==(ArraySegment<T> a, ArraySegment<T> b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.ArraySegment`1" /> structures are unequal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.ArraySegment`1" /> structure on the left side of the inequality operator.</param>
		/// <param name="b">The <see cref="T:System.ArraySegment`1" /> structure on the right side of the inequality operator.</param>
		// Token: 0x06000A6A RID: 2666 RVA: 0x00032CDF File Offset: 0x00030EDF
		public static bool operator !=(ArraySegment<T> a, ArraySegment<T> b)
		{
			return !(a == b);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.ArraySegment`1" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the array segment is read-only.</exception>
		// Token: 0x170001AD RID: 429
		T IList<T>.this[int index]
		{
			get
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("The underlying array is null."));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._array[this._offset + index];
			}
			set
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("The underlying array is null."));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._array[this._offset + index] = value;
			}
		}

		/// <summary>Determines the index of a specific item in the array segment.</summary>
		/// <returns>The index of <paramref name="item" /> if found in the list; otherwise, -1.</returns>
		/// <param name="item">The object to locate in the array segment.</param>
		// Token: 0x06000A6D RID: 2669 RVA: 0x00032D90 File Offset: 0x00030F90
		int IList<T>.IndexOf(T item)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The underlying array is null."));
			}
			int num = global::System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			if (num < 0)
			{
				return -1;
			}
			return num - this._offset;
		}

		/// <summary>Inserts an item into the array segment at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert into the array segment.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the array segment.</exception>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only.</exception>
		// Token: 0x06000A6E RID: 2670 RVA: 0x00014B5A File Offset: 0x00012D5A
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the array segment item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the array segment.</exception>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only.</exception>
		// Token: 0x06000A6F RID: 2671 RVA: 0x00014B5A File Offset: 0x00012D5A
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the element at the specified index of the array segment.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.ArraySegment`1" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
		// Token: 0x170001AE RID: 430
		T IReadOnlyList<T>.this[int index]
		{
			get
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("The underlying array is null."));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._array[this._offset + index];
			}
		}

		/// <summary>Gets a value that indicates whether the array segment  is read-only.</summary>
		/// <returns>true if the array segment is read-only; otherwise, false.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00003B29 File Offset: 0x00001D29
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Adds an item to the array segment.</summary>
		/// <param name="item">The object to add to the array segment.</param>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only.</exception>
		// Token: 0x06000A72 RID: 2674 RVA: 0x00014B5A File Offset: 0x00012D5A
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes all items from the array segment.</summary>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only. </exception>
		// Token: 0x06000A73 RID: 2675 RVA: 0x00014B5A File Offset: 0x00012D5A
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines whether the array segment contains a specific value.</summary>
		/// <returns>true if <paramref name="item" /> is found in the array segment; otherwise, false.</returns>
		/// <param name="item">The object to locate in the array segment.</param>
		// Token: 0x06000A74 RID: 2676 RVA: 0x00032E2C File Offset: 0x0003102C
		bool ICollection<T>.Contains(T item)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The underlying array is null."));
			}
			return global::System.Array.IndexOf<T>(this._array, item, this._offset, this._count) >= 0;
		}

		/// <summary>Copies the elements of the array segment to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the array segment. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source array segment is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06000A75 RID: 2677 RVA: 0x00032E64 File Offset: 0x00031064
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The underlying array is null."));
			}
			global::System.Array.Copy(this._array, this._offset, array, arrayIndex, this._count);
		}

		/// <summary>Removes the first occurrence of a specific object from the array segment.</summary>
		/// <returns>true if <paramref name="item" /> was successfully removed from the array segment; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the array segment.</returns>
		/// <param name="item">The object to remove from the array segment.</param>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only.</exception>
		// Token: 0x06000A76 RID: 2678 RVA: 0x00014B5A File Offset: 0x00012D5A
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an enumerator that iterates through the array segment.</summary>
		/// <returns>An enumerator that can be used to iterate through the array segment.</returns>
		// Token: 0x06000A77 RID: 2679 RVA: 0x00032E97 File Offset: 0x00031097
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The underlying array is null."));
			}
			return new ArraySegment<T>.ArraySegmentEnumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through an array segment.</summary>
		/// <returns>An enumerator that can be used to iterate through the array segment.</returns>
		// Token: 0x06000A78 RID: 2680 RVA: 0x00032E97 File Offset: 0x00031097
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The underlying array is null."));
			}
			return new ArraySegment<T>.ArraySegmentEnumerator(this);
		}

		// Token: 0x0400079F RID: 1951
		private T[] _array;

		// Token: 0x040007A0 RID: 1952
		private int _offset;

		// Token: 0x040007A1 RID: 1953
		private int _count;

		// Token: 0x0200012B RID: 299
		[Serializable]
		private sealed class ArraySegmentEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000A79 RID: 2681 RVA: 0x00032EBC File Offset: 0x000310BC
			internal ArraySegmentEnumerator(ArraySegment<T> arraySegment)
			{
				this._array = arraySegment._array;
				this._start = arraySegment._offset;
				this._end = this._start + arraySegment._count;
				this._current = this._start - 1;
			}

			// Token: 0x06000A7A RID: 2682 RVA: 0x00032F08 File Offset: 0x00031108
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return this._current < this._end;
				}
				return false;
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00032F38 File Offset: 0x00031138
			public T Current
			{
				get
				{
					if (this._current < this._start)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._current >= this._end)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					return this._array[this._current];
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00032F92 File Offset: 0x00031192
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000A7D RID: 2685 RVA: 0x00032F9F File Offset: 0x0003119F
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x06000A7E RID: 2686 RVA: 0x00002194 File Offset: 0x00000394
			public void Dispose()
			{
			}

			// Token: 0x040007A2 RID: 1954
			private T[] _array;

			// Token: 0x040007A3 RID: 1955
			private int _start;

			// Token: 0x040007A4 RID: 1956
			private int _end;

			// Token: 0x040007A5 RID: 1957
			private int _current;
		}
	}
}

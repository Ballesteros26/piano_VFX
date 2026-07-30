using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a first-in, first-out collection of objects.</summary>
	/// <typeparam name="T">Specifies the type of elements in the queue.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000720 RID: 1824
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(QueueDebugView<>))]
	[Serializable]
	public class Queue<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Queue`1" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06003982 RID: 14722 RVA: 0x000D20A5 File Offset: 0x000D02A5
		public Queue()
		{
			this._array = Array.Empty<T>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Queue`1" /> class that is empty and has the specified initial capacity.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Queue`1" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06003983 RID: 14723 RVA: 0x000D20B8 File Offset: 0x000D02B8
		public Queue(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this._array = new T[capacity];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Queue`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Generic.Queue`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x06003984 RID: 14724 RVA: 0x000D20E8 File Offset: 0x000D02E8
		public Queue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._array = EnumerableHelpers.ToArray<T>(collection, out this._size);
			if (this._size != this._array.Length)
			{
				this._tail = this._size;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000D2137 File Offset: 0x000D0337
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Queue`1" />, this property always returns false.</returns>
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Queue`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000D213F File Offset: 0x000D033F
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003988 RID: 14728 RVA: 0x000D2164 File Offset: 0x000D0364
		public void Clear()
		{
			if (this._size != 0)
			{
				if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
				{
					if (this._head < this._tail)
					{
						Array.Clear(this._array, this._head, this._size);
					}
					else
					{
						Array.Clear(this._array, this._head, this._array.Length - this._head);
						Array.Clear(this._array, 0, this._tail);
					}
				}
				this._size = 0;
			}
			this._head = 0;
			this._tail = 0;
			this._version++;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Queue`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Queue`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Queue`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06003989 RID: 14729 RVA: 0x000D21FC File Offset: 0x000D03FC
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			int num2 = Math.Min(this._array.Length - this._head, num);
			Array.Copy(this._array, this._head, array, arrayIndex, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, array, arrayIndex + this._array.Length - this._head, num);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600398A RID: 14730 RVA: 0x000D22AC File Offset: 0x000D04AC
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			int length = array.Length;
			if (index < 0 || index > length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (length - index < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			try
			{
				int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
				Array.Copy(this._array, this._head, array, index, num2);
				num -= num2;
				if (num > 0)
				{
					Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.Queue`1" />. The value can be null for reference types.</param>
		// Token: 0x0600398B RID: 14731 RVA: 0x000D23C4 File Offset: 0x000D05C4
		public void Enqueue(T item)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * 200L / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = item;
			this.MoveNext(ref this._tail);
			this._size++;
			this._version++;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.Queue`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		// Token: 0x0600398C RID: 14732 RVA: 0x000D2450 File Offset: 0x000D0650
		public Queue<T>.Enumerator GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x0600398D RID: 14733 RVA: 0x000D2458 File Offset: 0x000D0658
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x0600398E RID: 14734 RVA: 0x000D2458 File Offset: 0x000D0658
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		/// <summary>Removes and returns the object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>The object that is removed from the beginning of the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Queue`1" /> is empty.</exception>
		// Token: 0x0600398F RID: 14735 RVA: 0x000D2468 File Offset: 0x000D0668
		public T Dequeue()
		{
			if (this._size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			T t = this._array[this._head];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				this._array[this._head] = default(T);
			}
			this.MoveNext(ref this._head);
			this._size--;
			this._version++;
			return t;
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000D24E0 File Offset: 0x000D06E0
		public bool TryDequeue(out T result)
		{
			if (this._size == 0)
			{
				result = default(T);
				return false;
			}
			result = this._array[this._head];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				this._array[this._head] = default(T);
			}
			this.MoveNext(ref this._head);
			this._size--;
			this._version++;
			return true;
		}

		/// <summary>Returns the object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1" /> without removing it.</summary>
		/// <returns>The object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Queue`1" /> is empty.</exception>
		// Token: 0x06003991 RID: 14737 RVA: 0x000D255F File Offset: 0x000D075F
		public T Peek()
		{
			if (this._size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			return this._array[this._head];
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000D2580 File Offset: 0x000D0780
		public bool TryPeek(out T result)
		{
			if (this._size == 0)
			{
				result = default(T);
				return false;
			}
			result = this._array[this._head];
			return true;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.Queue`1" />; otherwise, false.</returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.Queue`1" />. The value can be null for reference types.</param>
		// Token: 0x06003993 RID: 14739 RVA: 0x000D25AC File Offset: 0x000D07AC
		public bool Contains(T item)
		{
			if (this._size == 0)
			{
				return false;
			}
			if (this._head < this._tail)
			{
				return Array.IndexOf<T>(this._array, item, this._head, this._size) >= 0;
			}
			return Array.IndexOf<T>(this._array, item, this._head, this._array.Length - this._head) >= 0 || Array.IndexOf<T>(this._array, item, 0, this._tail) >= 0;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Queue`1" /> elements to a new array.</summary>
		/// <returns>A new array containing elements copied from the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		// Token: 0x06003994 RID: 14740 RVA: 0x000D2630 File Offset: 0x000D0830
		public T[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._size];
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000D26C8 File Offset: 0x000D08C8
		private void SetCapacity(int capacity)
		{
			T[] array = new T[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000D2788 File Offset: 0x000D0988
		private void MoveNext(ref int index)
		{
			int num = index + 1;
			index = ((num == this._array.Length) ? 0 : num);
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000D27AB File Offset: 0x000D09AB
		private void ThrowForEmptyQueue()
		{
			throw new InvalidOperationException("Queue empty.");
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.Queue`1" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x06003998 RID: 14744 RVA: 0x000D27B8 File Offset: 0x000D09B8
		public void TrimExcess()
		{
			int num = (int)((double)this._array.Length * 0.9);
			if (this._size < num)
			{
				this.SetCapacity(this._size);
			}
		}

		// Token: 0x04002CBA RID: 11450
		private T[] _array;

		// Token: 0x04002CBB RID: 11451
		private int _head;

		// Token: 0x04002CBC RID: 11452
		private int _tail;

		// Token: 0x04002CBD RID: 11453
		private int _size;

		// Token: 0x04002CBE RID: 11454
		private int _version;

		// Token: 0x04002CBF RID: 11455
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002CC0 RID: 11456
		private const int MinimumGrow = 4;

		// Token: 0x04002CC1 RID: 11457
		private const int GrowFactor = 200;

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		// Token: 0x02000721 RID: 1825
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06003999 RID: 14745 RVA: 0x000D27EF File Offset: 0x000D09EF
			internal Enumerator(Queue<T> q)
			{
				this._q = q;
				this._version = q._version;
				this._index = -1;
				this._currentElement = default(T);
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Queue`1.Enumerator" />.</summary>
			// Token: 0x0600399A RID: 14746 RVA: 0x000D2817 File Offset: 0x000D0A17
			public void Dispose()
			{
				this._index = -2;
				this._currentElement = default(T);
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600399B RID: 14747 RVA: 0x000D2830 File Offset: 0x000D0A30
			public bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index == -2)
				{
					return false;
				}
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -2;
					this._currentElement = default(T);
					return false;
				}
				T[] array = this._q._array;
				int num = array.Length;
				int num2 = this._q._head + this._index;
				if (num2 >= num)
				{
					num2 -= num;
				}
				this._currentElement = array[num2];
				return true;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Queue`1" /> at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000DED RID: 3565
			// (get) Token: 0x0600399C RID: 14748 RVA: 0x000D28D7 File Offset: 0x000D0AD7
			public T Current
			{
				get
				{
					if (this._index < 0)
					{
						this.ThrowEnumerationNotStartedOrEnded();
					}
					return this._currentElement;
				}
			}

			// Token: 0x0600399D RID: 14749 RVA: 0x000D28EE File Offset: 0x000D0AEE
			private void ThrowEnumerationNotStartedOrEnded()
			{
				throw new InvalidOperationException((this._index == -1) ? "Enumeration has not started. Call MoveNext." : "Enumeration already finished.");
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000DEE RID: 3566
			// (get) Token: 0x0600399E RID: 14750 RVA: 0x000D290A File Offset: 0x000D0B0A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600399F RID: 14751 RVA: 0x000D2917 File Offset: 0x000D0B17
			void IEnumerator.Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -1;
				this._currentElement = default(T);
			}

			// Token: 0x04002CC2 RID: 11458
			private readonly Queue<T> _q;

			// Token: 0x04002CC3 RID: 11459
			private readonly int _version;

			// Token: 0x04002CC4 RID: 11460
			private int _index;

			// Token: 0x04002CC5 RID: 11461
			private T _currentElement;
		}
	}
}

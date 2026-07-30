using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a variable size last-in-first-out (LIFO) collection of instances of the same arbitrary type.</summary>
	/// <typeparam name="T">Specifies the type of elements in the stack.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000748 RID: 1864
	[DebuggerTypeProxy(typeof(StackDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Stack<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06003B26 RID: 15142 RVA: 0x000D7514 File Offset: 0x000D5714
		public Stack()
		{
			this._array = Array.Empty<T>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Stack`1" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06003B27 RID: 15143 RVA: 0x000D7527 File Offset: 0x000D5727
		public Stack(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this._array = new T[capacity];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection to copy elements from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x06003B28 RID: 15144 RVA: 0x000D7555 File Offset: 0x000D5755
		public Stack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._array = EnumerableHelpers.ToArray<T>(collection, out this._size);
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x000D757D File Offset: 0x000D577D
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Stack`1" />, this property always returns false.</returns>
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Stack`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003B2B RID: 15147 RVA: 0x000D7585 File Offset: 0x000D5785
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

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003B2C RID: 15148 RVA: 0x000D75A7 File Offset: 0x000D57A7
		public void Clear()
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				Array.Clear(this._array, 0, this._size);
			}
			this._size = 0;
			this._version++;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.Stack`1" />; otherwise, false.</returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.Stack`1" />. The value can be null for reference types.</param>
		// Token: 0x06003B2D RID: 15149 RVA: 0x000D75D7 File Offset: 0x000D57D7
		public bool Contains(T item)
		{
			return this._size != 0 && Array.LastIndexOf<T>(this._array, item, this._size - 1) != -1;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Stack`1" /> to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Stack`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Stack`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06003B2E RID: 15150 RVA: 0x000D7600 File Offset: 0x000D5800
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
			int i = 0;
			int num = arrayIndex + this._size;
			while (i < this._size)
			{
				array[--num] = this._array[i++];
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003B2F RID: 15151 RVA: 0x000D7684 File Offset: 0x000D5884
		void ICollection.CopyTo(Array array, int arrayIndex)
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
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			try
			{
				Array.Copy(this._array, 0, array, arrayIndex, this._size);
				Array.Reverse(array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Returns an enumerator for the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.Stack`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		// Token: 0x06003B30 RID: 15152 RVA: 0x000D7754 File Offset: 0x000D5954
		public Stack<T>.Enumerator GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003B31 RID: 15153 RVA: 0x000D775C File Offset: 0x000D595C
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003B32 RID: 15154 RVA: 0x000D775C File Offset: 0x000D595C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.Stack`1" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x06003B33 RID: 15155 RVA: 0x000D776C File Offset: 0x000D596C
		public void TrimExcess()
		{
			int num = (int)((double)this._array.Length * 0.9);
			if (this._size < num)
			{
				Array.Resize<T>(ref this._array, this._size);
				this._version++;
			}
		}

		/// <summary>Returns the object at the top of the <see cref="T:System.Collections.Generic.Stack`1" /> without removing it.</summary>
		/// <returns>The object at the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Stack`1" /> is empty.</exception>
		// Token: 0x06003B34 RID: 15156 RVA: 0x000D77B6 File Offset: 0x000D59B6
		public T Peek()
		{
			if (this._size == 0)
			{
				this.ThrowForEmptyStack();
			}
			return this._array[this._size - 1];
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000D77D9 File Offset: 0x000D59D9
		public bool TryPeek(out T result)
		{
			if (this._size == 0)
			{
				result = default(T);
				return false;
			}
			result = this._array[this._size - 1];
			return true;
		}

		/// <summary>Removes and returns the object at the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>The object removed from the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Stack`1" /> is empty.</exception>
		// Token: 0x06003B36 RID: 15158 RVA: 0x000D7808 File Offset: 0x000D5A08
		public T Pop()
		{
			if (this._size == 0)
			{
				this.ThrowForEmptyStack();
			}
			this._version++;
			T[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			T t = array[num];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				this._array[this._size] = default(T);
			}
			return t;
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x000D7870 File Offset: 0x000D5A70
		public bool TryPop(out T result)
		{
			if (this._size == 0)
			{
				result = default(T);
				return false;
			}
			this._version++;
			T[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			result = array[num];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				this._array[this._size] = default(T);
			}
			return true;
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <param name="item">The object to push onto the <see cref="T:System.Collections.Generic.Stack`1" />. The value can be null for reference types.</param>
		// Token: 0x06003B38 RID: 15160 RVA: 0x000D78E0 File Offset: 0x000D5AE0
		public void Push(T item)
		{
			if (this._size == this._array.Length)
			{
				Array.Resize<T>(ref this._array, (this._array.Length == 0) ? 4 : (2 * this._array.Length));
			}
			T[] array = this._array;
			int size = this._size;
			this._size = size + 1;
			array[size] = item;
			this._version++;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Stack`1" /> to a new array.</summary>
		/// <returns>A new array containing copies of the elements of the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		// Token: 0x06003B39 RID: 15161 RVA: 0x000D794C File Offset: 0x000D5B4C
		public T[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000D79A1 File Offset: 0x000D5BA1
		private void ThrowForEmptyStack()
		{
			throw new InvalidOperationException("Stack empty.");
		}

		// Token: 0x04002D3A RID: 11578
		private T[] _array;

		// Token: 0x04002D3B RID: 11579
		private int _size;

		// Token: 0x04002D3C RID: 11580
		private int _version;

		// Token: 0x04002D3D RID: 11581
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002D3E RID: 11582
		private const int DefaultCapacity = 4;

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		// Token: 0x02000749 RID: 1865
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06003B3B RID: 15163 RVA: 0x000D79AD File Offset: 0x000D5BAD
			internal Enumerator(Stack<T> stack)
			{
				this._stack = stack;
				this._version = stack._version;
				this._index = -2;
				this._currentElement = default(T);
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Stack`1.Enumerator" />.</summary>
			// Token: 0x06003B3C RID: 15164 RVA: 0x000D79D6 File Offset: 0x000D5BD6
			public void Dispose()
			{
				this._index = -1;
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06003B3D RID: 15165 RVA: 0x000D79E0 File Offset: 0x000D5BE0
			public bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					bool flag = this._index >= 0;
					if (flag)
					{
						this._currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				bool flag2 = num >= 0;
				if (flag2)
				{
					this._currentElement = this._stack._array[this._index];
					return flag2;
				}
				this._currentElement = default(T);
				return flag2;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Stack`1" /> at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000E53 RID: 3667
			// (get) Token: 0x06003B3E RID: 15166 RVA: 0x000D7AA2 File Offset: 0x000D5CA2
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

			// Token: 0x06003B3F RID: 15167 RVA: 0x000D7AB9 File Offset: 0x000D5CB9
			private void ThrowEnumerationNotStartedOrEnded()
			{
				throw new InvalidOperationException((this._index == -2) ? "Enumeration has not started. Call MoveNext." : "Enumeration already finished.");
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000E54 RID: 3668
			// (get) Token: 0x06003B40 RID: 15168 RVA: 0x000D7AD6 File Offset: 0x000D5CD6
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection. This class cannot be inherited.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06003B41 RID: 15169 RVA: 0x000D7AE3 File Offset: 0x000D5CE3
			void IEnumerator.Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x04002D3F RID: 11583
			private readonly Stack<T> _stack;

			// Token: 0x04002D40 RID: 11584
			private readonly int _version;

			// Token: 0x04002D41 RID: 11585
			private int _index;

			// Token: 0x04002D42 RID: 11586
			private T _currentElement;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace System.Runtime.CompilerServices
{
	/// <summary>The builder for read only collection.</summary>
	/// <typeparam name="T">The type of the collection element.</typeparam>
	// Token: 0x020002FC RID: 764
	[Serializable]
	public sealed class ReadOnlyCollectionBuilder<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		/// <summary>Constructs a ReadOnlyCollectionBuilder.</summary>
		// Token: 0x06001736 RID: 5942 RVA: 0x0004C361 File Offset: 0x0004A561
		public ReadOnlyCollectionBuilder()
		{
			this._items = Array.Empty<T>();
		}

		/// <summary>Constructs a ReadOnlyCollectionBuilder with a given initial capacity. The contents are empty but builder will have reserved room for the given number of elements before any reallocations are required.</summary>
		/// <param name="capacity">Initial capacity.</param>
		// Token: 0x06001737 RID: 5943 RVA: 0x0004C374 File Offset: 0x0004A574
		public ReadOnlyCollectionBuilder(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this._items = new T[capacity];
		}

		/// <summary>Constructs a ReadOnlyCollectionBuilder, copying contents of the given collection.</summary>
		/// <param name="collection">Collection to copy elements from.</param>
		// Token: 0x06001738 RID: 5944 RVA: 0x0004C398 File Offset: 0x0004A598
		public ReadOnlyCollectionBuilder(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				this._items = new T[count];
				collection2.CopyTo(this._items, 0);
				this._size = count;
				return;
			}
			this._size = 0;
			this._items = new T[4];
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		/// <summary>Gets and sets the capacity of this ReadOnlyCollectionBuilder.</summary>
		/// <returns>The capacity of this ReadOnlyCollectionBuilder.</returns>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0004C438 File Offset: 0x0004A638
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x0004C444 File Offset: 0x0004A644
		public int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = Array.Empty<T>();
				}
			}
		}

		/// <summary>Returns number of elements in the ReadOnlyCollectionBuilder.</summary>
		/// <returns>The number of elements in the ReadOnlyCollectionBuilder.</returns>
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0004C4AB File Offset: 0x0004A6AB
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Returns the index of the first occurrence of a given value in the builder.</summary>
		/// <returns>The index of the first occurrence of an item.</returns>
		/// <param name="item">An item to search for.</param>
		// Token: 0x0600173C RID: 5948 RVA: 0x0004C4B3 File Offset: 0x0004A6B3
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which item should be inserted.</param>
		/// <param name="item">The object to insert into the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x0600173D RID: 5949 RVA: 0x0004C4C8 File Offset: 0x0004A6C8
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		/// <summary>Removes the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x0600173E RID: 5950 RVA: 0x0004C558 File Offset: 0x0004A758
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000414 RID: 1044
		public T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._items[index];
			}
			set
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._items[index] = value;
				this._version++;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x06001741 RID: 5953 RVA: 0x0004C62C File Offset: 0x0004A82C
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		/// <summary>Removes all items from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		// Token: 0x06001742 RID: 5954 RVA: 0x0004C682 File Offset: 0x0004A882
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		/// <summary>Determines whether the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> contains a specific value</summary>
		/// <returns>true if item is found in the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />; otherwise, false.</returns>
		/// <param name="item">the object to locate in the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x06001743 RID: 5955 RVA: 0x0004C6B4 File Offset: 0x0004A8B4
		public bool Contains(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int j = 0; j < this._size; j++)
			{
				if (@default.Equals(this._items[j], item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> to an <see cref="T:System.Array" />, starting at particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
		// Token: 0x06001744 RID: 5956 RVA: 0x0004C720 File Offset: 0x0004A920
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00002285 File Offset: 0x00000485
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		/// <returns>true if item was successfully removed from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</returns>
		/// <param name="item">The object to remove from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x06001746 RID: 5958 RVA: 0x0004C738 File Offset: 0x0004A938
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001747 RID: 5959 RVA: 0x0004C75B File Offset: 0x0004A95B
		public IEnumerator<T> GetEnumerator()
		{
			return new ReadOnlyCollectionBuilder<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001748 RID: 5960 RVA: 0x0004C763 File Offset: 0x0004A963
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00002285 File Offset: 0x00000485
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600174A RID: 5962 RVA: 0x0004C76C File Offset: 0x0004A96C
		int IList.Add(object value)
		{
			ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
			try
			{
				this.Add((T)((object)value));
			}
			catch (InvalidCastException)
			{
				throw Error.InvalidTypeException(value, typeof(T), "value");
			}
			return this.Count - 1;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600174B RID: 5963 RVA: 0x0004C7C4 File Offset: 0x0004A9C4
		bool IList.Contains(object value)
		{
			return ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="item" /> if found in the list; otherwise, –1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600174C RID: 5964 RVA: 0x0004C7DC File Offset: 0x0004A9DC
		int IList.IndexOf(object value)
		{
			if (ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="value">The object to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600174D RID: 5965 RVA: 0x0004C7F4 File Offset: 0x0004A9F4
		void IList.Insert(int index, object value)
		{
			ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
			try
			{
				this.Insert(index, (T)((object)value));
			}
			catch (InvalidCastException)
			{
				throw Error.InvalidTypeException(value, typeof(T), "value");
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00002285 File Offset: 0x00000485
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600174F RID: 5967 RVA: 0x0004C844 File Offset: 0x0004AA44
		void IList.Remove(object value)
		{
			if (ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000418 RID: 1048
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					throw Error.InvalidTypeException(value, typeof(T), "value");
				}
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06001752 RID: 5970 RVA: 0x0004C8BC File Offset: 0x0004AABC
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("array");
			}
			Array.Copy(this._items, 0, array, index, this._size);
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00002285 File Offset: 0x00000485
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x000021A0 File Offset: 0x000003A0
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Reverses the order of the elements in the entire <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		// Token: 0x06001755 RID: 5973 RVA: 0x0004C8F4 File Offset: 0x0004AAF4
		public void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		/// <summary>Reverses the order of the elements in the specified range.</summary>
		/// <param name="index">The zero-based starting index of the range to reverse.</param>
		/// <param name="count">The number of elements in the range to reverse.</param>
		// Token: 0x06001756 RID: 5974 RVA: 0x0004C903 File Offset: 0x0004AB03
		public void Reverse(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			Array.Reverse<T>(this._items, index, count);
			this._version++;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> to a new array.</summary>
		/// <returns>An array containing copies of the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</returns>
		// Token: 0x06001757 RID: 5975 RVA: 0x0004C940 File Offset: 0x0004AB40
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Creates a <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> containing all of the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />, avoiding copying the elements to the new array if possible. Resets the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> after the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> has been created.</summary>
		/// <returns>A new instance of <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</returns>
		// Token: 0x06001758 RID: 5976 RVA: 0x0004C970 File Offset: 0x0004AB70
		public ReadOnlyCollection<T> ToReadOnlyCollection()
		{
			T[] array;
			if (this._size == this._items.Length)
			{
				array = this._items;
			}
			else
			{
				array = this.ToArray();
			}
			this._items = Array.Empty<T>();
			this._size = 0;
			this._version++;
			return new TrueReadOnlyCollection<T>(array);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0004C9C4 File Offset: 0x0004ABC4
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = 4;
				if (this._items.Length != 0)
				{
					num = this._items.Length * 2;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0004CA00 File Offset: 0x0004AC00
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0004CA30 File Offset: 0x0004AC30
		private static void ValidateNullValue(object value, string argument)
		{
			if (value == null && default(T) != null)
			{
				throw Error.InvalidNullValue(typeof(T), argument);
			}
		}

		// Token: 0x04000AC3 RID: 2755
		private const int DefaultCapacity = 4;

		// Token: 0x04000AC4 RID: 2756
		private T[] _items;

		// Token: 0x04000AC5 RID: 2757
		private int _size;

		// Token: 0x04000AC6 RID: 2758
		private int _version;

		// Token: 0x020002FD RID: 765
		[Serializable]
		private class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600175C RID: 5980 RVA: 0x0004CA61 File Offset: 0x0004AC61
			internal Enumerator(ReadOnlyCollectionBuilder<T> builder)
			{
				this._builder = builder;
				this._version = builder._version;
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x1700041B RID: 1051
			// (get) Token: 0x0600175D RID: 5981 RVA: 0x0004CA8F File Offset: 0x0004AC8F
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x0600175E RID: 5982 RVA: 0x00003C4C File Offset: 0x00001E4C
			public void Dispose()
			{
			}

			// Token: 0x1700041C RID: 1052
			// (get) Token: 0x0600175F RID: 5983 RVA: 0x0004CA97 File Offset: 0x0004AC97
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index > this._builder._size)
					{
						throw Error.EnumerationIsDone();
					}
					return this._current;
				}
			}

			// Token: 0x06001760 RID: 5984 RVA: 0x0004CAC8 File Offset: 0x0004ACC8
			public bool MoveNext()
			{
				if (this._version != this._builder._version)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				if (this._index < this._builder._size)
				{
					T[] items = this._builder._items;
					int index = this._index;
					this._index = index + 1;
					this._current = items[index];
					return true;
				}
				this._index = this._builder._size + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x06001761 RID: 5985 RVA: 0x0004CB4A File Offset: 0x0004AD4A
			void IEnumerator.Reset()
			{
				if (this._version != this._builder._version)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04000AC7 RID: 2759
			private readonly ReadOnlyCollectionBuilder<T> _builder;

			// Token: 0x04000AC8 RID: 2760
			private readonly int _version;

			// Token: 0x04000AC9 RID: 2761
			private int _index;

			// Token: 0x04000ACA RID: 2762
			private T _current;
		}
	}
}

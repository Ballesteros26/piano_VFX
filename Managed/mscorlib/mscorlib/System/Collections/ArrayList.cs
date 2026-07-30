using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	/// <summary>Implements the <see cref="T:System.Collections.IList" /> interface using an array whose size is dynamically increased as required.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020009AC RID: 2476
	[ComVisible(true)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ArrayList.ArrayListDebugView))]
	[Serializable]
	public class ArrayList : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06005AAB RID: 23211 RVA: 0x00002111 File Offset: 0x00000311
		internal ArrayList(bool trash)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06005AAC RID: 23212 RVA: 0x0012C958 File Offset: 0x0012AB58
		public ArrayList()
		{
			this._items = ArrayList.emptyArray;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the specified initial capacity.</summary>
		/// <param name="capacity">The number of elements that the new list can initially store. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06005AAD RID: 23213 RVA: 0x0012C96C File Offset: 0x0012AB6C
		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("'{0}' must be non-negative.", new object[] { "capacity" }));
			}
			if (capacity == 0)
			{
				this._items = ArrayList.emptyArray;
				return;
			}
			this._items = new object[capacity];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that contains elements copied from the specified collection and that has the same initial capacity as the number of elements copied.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements are copied to the new list. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		// Token: 0x06005AAE RID: 23214 RVA: 0x0012C9C4 File Offset: 0x0012ABC4
		public ArrayList(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", Environment.GetResourceString("Collection cannot be null."));
			}
			int count = c.Count;
			if (count == 0)
			{
				this._items = ArrayList.emptyArray;
				return;
			}
			this._items = new object[count];
			this.AddRange(c);
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.ArrayList.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06005AAF RID: 23215 RVA: 0x0012CA18 File Offset: 0x0012AC18
		// (set) Token: 0x06005AB0 RID: 23216 RVA: 0x0012CA24 File Offset: 0x0012AC24
		public virtual int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("capacity was less than the current size."));
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = new object[4];
				}
			}
		}

		/// <summary>Gets the number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06005AB1 RID: 23217 RVA: 0x0012CA96 File Offset: 0x0012AC96
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.ArrayList" /> has a fixed size; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06005AB2 RID: 23218 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.ArrayList" /> is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06005AB4 RID: 23220 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x0012CA9E File Offset: 0x0012AC9E
		public virtual object SyncRoot
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

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FCB RID: 4043
		public virtual object this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				return this._items[index];
			}
			set
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				this._items[index] = value;
				this._version++;
			}
		}

		/// <summary>Creates an <see cref="T:System.Collections.ArrayList" /> wrapper for a specific <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> wrapper around the <see cref="T:System.Collections.IList" />.</returns>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AB8 RID: 23224 RVA: 0x0012CB27 File Offset: 0x0012AD27
		public static ArrayList Adapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.IListWrapper(list);
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005AB9 RID: 23225 RVA: 0x0012CB40 File Offset: 0x0012AD40
		public virtual int Add(object value)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			this._items[this._size] = value;
			this._version++;
			int size = this._size;
			this._size = size + 1;
			return size;
		}

		/// <summary>Adds the elements of an <see cref="T:System.Collections.ICollection" /> to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be added to the end of the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be null, but it can contain elements that are null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ABA RID: 23226 RVA: 0x0012CB98 File Offset: 0x0012AD98
		public virtual void AddRange(ICollection c)
		{
			this.InsertRange(this._size, c);
		}

		/// <summary>Searches a range of elements in the sorted <see cref="T:System.Collections.ArrayList" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <param name="index">The zero-based starting index of the range to search. </param>
		/// <param name="count">The length of the range to search. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be null. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- null to use the default comparer that is the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="comparer" /> is null and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ABB RID: 23227 RVA: 0x0012CBA8 File Offset: 0x0012ADA8
		public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			return Array.BinarySearch(this._items, index, count, value, comparer);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.ArrayList" /> for an element using the default comparer and returns the zero-based index of the element.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be null. </param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ABC RID: 23228 RVA: 0x0012CC12 File Offset: 0x0012AE12
		public virtual int BinarySearch(object value)
		{
			return this.BinarySearch(0, this.Count, value, null);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.ArrayList" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be null. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- null to use the default comparer that is the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparer" /> is null and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ABD RID: 23229 RVA: 0x0012CC23 File Offset: 0x0012AE23
		public virtual int BinarySearch(object value, IComparer comparer)
		{
			return this.BinarySearch(0, this.Count, value, comparer);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ABE RID: 23230 RVA: 0x0012CC34 File Offset: 0x0012AE34
		public virtual void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005ABF RID: 23231 RVA: 0x0012CC68 File Offset: 0x0012AE68
		public virtual object Clone()
		{
			ArrayList arrayList = new ArrayList(this._size);
			arrayList._size = this._size;
			arrayList._version = this._version;
			Array.Copy(this._items, 0, arrayList._items, 0, this._size);
			return arrayList;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.ArrayList" />; otherwise, false.</returns>
		/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005AC0 RID: 23232 RVA: 0x0012CCB4 File Offset: 0x0012AEB4
		public virtual bool Contains(object item)
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
			for (int j = 0; j < this._size; j++)
			{
				if (this._items[j] != null && this._items[j].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the beginning of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the number of elements that the destination <paramref name="array" /> can contain. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AC1 RID: 23233 RVA: 0x0012CD11 File Offset: 0x0012AF11
		public virtual void CopyTo(Array array)
		{
			this.CopyTo(array, 0);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AC2 RID: 23234 RVA: 0x0012CD1B File Offset: 0x0012AF1B
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
			}
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		/// <summary>Copies a range of elements from the <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="index">The zero-based index in the source <see cref="T:System.Collections.ArrayList" /> at which copying begins. </param>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <param name="count">The number of elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="arrayIndex" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.ArrayList.Count" /> of the source <see cref="T:System.Collections.ArrayList" />.-or- The number of elements from <paramref name="index" /> to the end of the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AC3 RID: 23235 RVA: 0x0012CD50 File Offset: 0x0012AF50
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x0012CDA8 File Offset: 0x0012AFA8
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = ((this._items.Length == 0) ? 4 : (this._items.Length * 2));
				if (num > 2146435071)
				{
					num = 2146435071;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> wrapper with a fixed size.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> wrapper with a fixed size.</returns>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AC5 RID: 23237 RVA: 0x0012CDF2 File Offset: 0x0012AFF2
		public static IList FixedSize(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeList(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> wrapper with a fixed size.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> wrapper with a fixed size.</returns>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AC6 RID: 23238 RVA: 0x0012CE08 File Offset: 0x0012B008
		public static ArrayList FixedSize(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeArrayList(list);
		}

		/// <summary>Returns an enumerator for the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AC7 RID: 23239 RVA: 0x0012CE1E File Offset: 0x0012B01E
		public virtual IEnumerator GetEnumerator()
		{
			return new ArrayList.ArrayListEnumeratorSimple(this);
		}

		/// <summary>Returns an enumerator for a range of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the specified range of elements in the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <param name="index">The zero-based starting index of the <see cref="T:System.Collections.ArrayList" /> section that the enumerator should refer to. </param>
		/// <param name="count">The number of elements in the <see cref="T:System.Collections.ArrayList" /> section that the enumerator should refer to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AC8 RID: 23240 RVA: 0x0012CE28 File Offset: 0x0012B028
		public virtual IEnumerator GetEnumerator(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			return new ArrayList.ArrayListEnumerator(this, index, count);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.ArrayList" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005AC9 RID: 23241 RVA: 0x0012CE8A File Offset: 0x0012B08A
		public virtual int IndexOf(object value)
		{
			return Array.IndexOf(this._items, value, 0, this._size);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the specified index to the last element.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from <paramref name="startIndex" /> to the last element, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ACA RID: 23242 RVA: 0x0012CE9F File Offset: 0x0012B09F
		public virtual int IndexOf(object value, int startIndex)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			return Array.IndexOf(this._items, value, startIndex, this._size - startIndex);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that starts at <paramref name="startIndex" /> and contains <paramref name="count" /> number of elements, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ACB RID: 23243 RVA: 0x0012CED4 File Offset: 0x0012B0D4
		public virtual int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0 || startIndex > this._size - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
			}
			return Array.IndexOf(this._items, value, startIndex, count);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ACC RID: 23244 RVA: 0x0012CF34 File Offset: 0x0012B134
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Insertion index was out of range. Must be non-negative and less than or equal to size."));
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = value;
			this._size++;
			this._version++;
		}

		/// <summary>Inserts the elements of a collection into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the new elements should be inserted. </param>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be inserted into the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be null, but it can contain elements that are null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005ACD RID: 23245 RVA: 0x0012CFCC File Offset: 0x0012B1CC
		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", Environment.GetResourceString("Collection cannot be null."));
			}
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			int count = c.Count;
			if (count > 0)
			{
				this.EnsureCapacity(this._size + count);
				if (index < this._size)
				{
					Array.Copy(this._items, index, this._items, index + count, this._size - index);
				}
				object[] array = new object[count];
				c.CopyTo(array, 0);
				array.CopyTo(this._items, index);
				this._size += count;
				this._version++;
			}
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire the <see cref="T:System.Collections.ArrayList" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005ACE RID: 23246 RVA: 0x0012D08A File Offset: 0x0012B28A
		public virtual int LastIndexOf(object value)
		{
			return this.LastIndexOf(value, this._size - 1, this._size);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the first element to the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the first element to <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005ACF RID: 23247 RVA: 0x0012D0A1 File Offset: 0x0012B2A1
		public virtual int LastIndexOf(object value, int startIndex)
		{
			if (startIndex >= this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that contains <paramref name="count" /> number of elements and ends at <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AD0 RID: 23248 RVA: 0x0012D0CC File Offset: 0x0012B2CC
		public virtual int LastIndexOf(object value, int startIndex, int count)
		{
			if (this.Count != 0 && (startIndex < 0 || count < 0))
			{
				throw new ArgumentOutOfRangeException((startIndex < 0) ? "startIndex" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (startIndex >= this._size || count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException((startIndex >= this._size) ? "startIndex" : "count", Environment.GetResourceString("Larger than collection size."));
			}
			return Array.LastIndexOf(this._items, value, startIndex, count);
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.IList" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.IList" /> wrapper around <paramref name="list" />.</returns>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AD1 RID: 23249 RVA: 0x0012D155 File Offset: 0x0012B355
		public static IList ReadOnly(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyList(list);
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.ArrayList" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.ArrayList" /> wrapper around <paramref name="list" />.</returns>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AD2 RID: 23250 RVA: 0x0012D16B File Offset: 0x0012B36B
		public static ArrayList ReadOnly(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyArrayList(list);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005AD3 RID: 23251 RVA: 0x0012D184 File Offset: 0x0012B384
		public virtual void Remove(object obj)
		{
			int num = this.IndexOf(obj);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based index of the element to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005AD4 RID: 23252 RVA: 0x0012D1A4 File Offset: 0x0012B3A4
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = null;
			this._version++;
		}

		/// <summary>Removes a range of elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based starting index of the range of elements to remove. </param>
		/// <param name="count">The number of elements to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AD5 RID: 23253 RVA: 0x0012D224 File Offset: 0x0012B424
		public virtual void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (count > 0)
			{
				int i = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				while (i > this._size)
				{
					this._items[--i] = null;
				}
				this._version++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> whose elements are copies of the specified value.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> with <paramref name="count" /> number of elements, all of which are copies of <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to copy multiple times in the new <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="count">The number of times <paramref name="value" /> should be copied. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AD6 RID: 23254 RVA: 0x0012D2E4 File Offset: 0x0012B4E4
		public static ArrayList Repeat(object value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			ArrayList arrayList = new ArrayList((count > 4) ? count : 4);
			for (int i = 0; i < count; i++)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		/// <summary>Reverses the order of the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AD7 RID: 23255 RVA: 0x0012D32D File Offset: 0x0012B52D
		public virtual void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		/// <summary>Reverses the order of the elements in the specified range.</summary>
		/// <param name="index">The zero-based starting index of the range to reverse. </param>
		/// <param name="count">The number of elements in the range to reverse. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AD8 RID: 23256 RVA: 0x0012D33C File Offset: 0x0012B53C
		public virtual void Reverse(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			Array.Reverse<object>(this._items, index, count);
			this._version++;
		}

		/// <summary>Copies the elements of a collection over a range of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based <see cref="T:System.Collections.ArrayList" /> index at which to start copying the elements of <paramref name="c" />. </param>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements to copy to the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be null, but it can contain elements that are null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> plus the number of elements in <paramref name="c" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AD9 RID: 23257 RVA: 0x0012D3B4 File Offset: 0x0012B5B4
		public virtual void SetRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", Environment.GetResourceString("Collection cannot be null."));
			}
			int count = c.Count;
			if (index < 0 || index > this._size - count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count > 0)
			{
				c.CopyTo(this._items, index);
				this._version++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> which represents a subset of the elements in the source <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> which represents a subset of the elements in the source <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <param name="index">The zero-based <see cref="T:System.Collections.ArrayList" /> index at which the range starts. </param>
		/// <param name="count">The number of elements in the range. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005ADA RID: 23258 RVA: 0x0012D424 File Offset: 0x0012B624
		public virtual ArrayList GetRange(int index, int count)
		{
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			return new ArrayList.Range(this, index, count);
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ADB RID: 23259 RVA: 0x0012D47C File Offset: 0x0012B67C
		public virtual void Sort()
		{
			this.Sort(0, this.Count, Comparer.Default);
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- A null reference (Nothing in Visual Basic) to use the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ADC RID: 23260 RVA: 0x0012D490 File Offset: 0x0012B690
		public virtual void Sort(IComparer comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="index">The zero-based starting index of the range to sort. </param>
		/// <param name="count">The length of the range to sort. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- A null reference (Nothing in Visual Basic) to use the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005ADD RID: 23261 RVA: 0x0012D4A0 File Offset: 0x0012B6A0
		public virtual void Sort(int index, int count, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (this._size - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			Array.Sort(this._items, index, count, comparer);
			this._version++;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> wrapper that is synchronized (thread safe).</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> wrapper that is synchronized (thread safe).</returns>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005ADE RID: 23262 RVA: 0x0012D516 File Offset: 0x0012B716
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static IList Synchronized(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncIList(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</returns>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005ADF RID: 23263 RVA: 0x0012D52C File Offset: 0x0012B72C
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncArrayList(list);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new <see cref="T:System.Object" /> array.</summary>
		/// <returns>An <see cref="T:System.Object" /> array containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005AE0 RID: 23264 RVA: 0x0012D544 File Offset: 0x0012B744
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new array of the specified element type.</summary>
		/// <returns>An array of the specified element type containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <param name="type">The element <see cref="T:System.Type" /> of the destination array to create and copy elements to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the specified type. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005AE1 RID: 23265 RVA: 0x0012D574 File Offset: 0x0012B774
		[SecuritySafeCritical]
		public virtual Array ToArray(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Array array = Array.UnsafeCreateInstance(type, new int[] { this._size });
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005AE2 RID: 23266 RVA: 0x0012D5C0 File Offset: 0x0012B7C0
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x04002EFF RID: 12031
		private object[] _items;

		// Token: 0x04002F00 RID: 12032
		private int _size;

		// Token: 0x04002F01 RID: 12033
		private int _version;

		// Token: 0x04002F02 RID: 12034
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002F03 RID: 12035
		private const int _defaultCapacity = 4;

		// Token: 0x04002F04 RID: 12036
		private static readonly object[] emptyArray = EmptyArray<object>.Value;

		// Token: 0x020009AD RID: 2477
		[Serializable]
		private class IListWrapper : ArrayList
		{
			// Token: 0x06005AE4 RID: 23268 RVA: 0x0012D5DA File Offset: 0x0012B7DA
			internal IListWrapper(IList list)
			{
				this._list = list;
				this._version = 0;
			}

			// Token: 0x17000FCC RID: 4044
			// (get) Token: 0x06005AE5 RID: 23269 RVA: 0x0012D5F0 File Offset: 0x0012B7F0
			// (set) Token: 0x06005AE6 RID: 23270 RVA: 0x0012D5FD File Offset: 0x0012B7FD
			public override int Capacity
			{
				get
				{
					return this._list.Count;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("capacity was less than the current size."));
					}
				}
			}

			// Token: 0x17000FCD RID: 4045
			// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x0012D5F0 File Offset: 0x0012B7F0
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000FCE RID: 4046
			// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x0012D61D File Offset: 0x0012B81D
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000FCF RID: 4047
			// (get) Token: 0x06005AE9 RID: 23273 RVA: 0x0012D62A File Offset: 0x0012B82A
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17000FD0 RID: 4048
			// (get) Token: 0x06005AEA RID: 23274 RVA: 0x0012D637 File Offset: 0x0012B837
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000FD1 RID: 4049
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version++;
				}
			}

			// Token: 0x17000FD2 RID: 4050
			// (get) Token: 0x06005AED RID: 23277 RVA: 0x0012D66F File Offset: 0x0012B86F
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005AEE RID: 23278 RVA: 0x0012D67C File Offset: 0x0012B87C
			public override int Add(object obj)
			{
				int num = this._list.Add(obj);
				this._version++;
				return num;
			}

			// Token: 0x06005AEF RID: 23279 RVA: 0x0012D698 File Offset: 0x0012B898
			public override void AddRange(ICollection c)
			{
				this.InsertRange(this.Count, c);
			}

			// Token: 0x06005AF0 RID: 23280 RVA: 0x0012D6A8 File Offset: 0x0012B8A8
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				int i = index;
				int num = index + count - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					int num3 = comparer.Compare(value, this._list[num2]);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 < 0)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				return ~i;
			}

			// Token: 0x06005AF1 RID: 23281 RVA: 0x0012D741 File Offset: 0x0012B941
			public override void Clear()
			{
				if (this._list.IsFixedSize)
				{
					throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
				}
				this._list.Clear();
				this._version++;
			}

			// Token: 0x06005AF2 RID: 23282 RVA: 0x0012D779 File Offset: 0x0012B979
			public override object Clone()
			{
				return new ArrayList.IListWrapper(this._list);
			}

			// Token: 0x06005AF3 RID: 23283 RVA: 0x0012D786 File Offset: 0x0012B986
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005AF4 RID: 23284 RVA: 0x0012D794 File Offset: 0x0012B994
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005AF5 RID: 23285 RVA: 0x0012D7A4 File Offset: 0x0012B9A4
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "arrayIndex", Environment.GetResourceString("Non-negative number required."));
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				for (int i = index; i < index + count; i++)
				{
					array.SetValue(this._list[i], arrayIndex++);
				}
			}

			// Token: 0x06005AF6 RID: 23286 RVA: 0x0012D87E File Offset: 0x0012BA7E
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005AF7 RID: 23287 RVA: 0x0012D88C File Offset: 0x0012BA8C
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				return new ArrayList.IListWrapper.IListWrapperEnumWrapper(this, index, count);
			}

			// Token: 0x06005AF8 RID: 23288 RVA: 0x0012D8E9 File Offset: 0x0012BAE9
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005AF9 RID: 23289 RVA: 0x0012D8F7 File Offset: 0x0012BAF7
			public override int IndexOf(object value, int startIndex)
			{
				return this.IndexOf(value, startIndex, this._list.Count - startIndex);
			}

			// Token: 0x06005AFA RID: 23290 RVA: 0x0012D910 File Offset: 0x0012BB10
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (count < 0 || startIndex > this.Count - count)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
				}
				int num = startIndex + count;
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j < num; j++)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06005AFB RID: 23291 RVA: 0x0012D9B9 File Offset: 0x0012BBB9
			public override void Insert(int index, object obj)
			{
				this._list.Insert(index, obj);
				this._version++;
			}

			// Token: 0x06005AFC RID: 23292 RVA: 0x0012D9D8 File Offset: 0x0012BBD8
			public override void InsertRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", Environment.GetResourceString("Collection cannot be null."));
				}
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (c.Count > 0)
				{
					ArrayList arrayList = this._list as ArrayList;
					if (arrayList != null)
					{
						arrayList.InsertRange(index, c);
					}
					else
					{
						foreach (object obj in c)
						{
							this._list.Insert(index++, obj);
						}
					}
					this._version++;
				}
			}

			// Token: 0x06005AFD RID: 23293 RVA: 0x0012DA77 File Offset: 0x0012BC77
			public override int LastIndexOf(object value)
			{
				return this.LastIndexOf(value, this._list.Count - 1, this._list.Count);
			}

			// Token: 0x06005AFE RID: 23294 RVA: 0x0012DA98 File Offset: 0x0012BC98
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06005AFF RID: 23295 RVA: 0x0012DAA8 File Offset: 0x0012BCA8
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (this._list.Count == 0)
				{
					return -1;
				}
				if (startIndex < 0 || startIndex >= this._list.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (count < 0 || count > startIndex + 1)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
				}
				int num = startIndex - count + 1;
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j >= num; j--)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06005B00 RID: 23296 RVA: 0x0012DB64 File Offset: 0x0012BD64
			public override void Remove(object value)
			{
				int num = this.IndexOf(value);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06005B01 RID: 23297 RVA: 0x0012DB84 File Offset: 0x0012BD84
			public override void RemoveAt(int index)
			{
				this._list.RemoveAt(index);
				this._version++;
			}

			// Token: 0x06005B02 RID: 23298 RVA: 0x0012DBA0 File Offset: 0x0012BDA0
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				if (count > 0)
				{
					this._version++;
				}
				while (count > 0)
				{
					this._list.RemoveAt(index);
					count--;
				}
			}

			// Token: 0x06005B03 RID: 23299 RVA: 0x0012DC20 File Offset: 0x0012BE20
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					object obj = this._list[i];
					this._list[i++] = this._list[num];
					this._list[num--] = obj;
				}
				this._version++;
			}

			// Token: 0x06005B04 RID: 23300 RVA: 0x0012DCCC File Offset: 0x0012BECC
			public override void SetRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", Environment.GetResourceString("Collection cannot be null."));
				}
				if (index < 0 || index > this._list.Count - c.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (c.Count > 0)
				{
					foreach (object obj in c)
					{
						this._list[index++] = obj;
					}
					this._version++;
				}
			}

			// Token: 0x06005B05 RID: 23301 RVA: 0x0012DD60 File Offset: 0x0012BF60
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06005B06 RID: 23302 RVA: 0x0012DDC0 File Offset: 0x0012BFC0
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				object[] array = new object[count];
				this.CopyTo(index, array, 0, count);
				Array.Sort(array, 0, count, comparer);
				for (int i = 0; i < count; i++)
				{
					this._list[i + index] = array[i];
				}
				this._version++;
			}

			// Token: 0x06005B07 RID: 23303 RVA: 0x0012DE5C File Offset: 0x0012C05C
			public override object[] ToArray()
			{
				object[] array = new object[this.Count];
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06005B08 RID: 23304 RVA: 0x0012DE84 File Offset: 0x0012C084
			[SecuritySafeCritical]
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				Array array = Array.UnsafeCreateInstance(type, new int[] { this._list.Count });
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06005B09 RID: 23305 RVA: 0x00002194 File Offset: 0x00000394
			public override void TrimToSize()
			{
			}

			// Token: 0x04002F05 RID: 12037
			private IList _list;

			// Token: 0x020009AE RID: 2478
			[Serializable]
			private sealed class IListWrapperEnumWrapper : IEnumerator, ICloneable
			{
				// Token: 0x06005B0A RID: 23306 RVA: 0x00002111 File Offset: 0x00000311
				private IListWrapperEnumWrapper()
				{
				}

				// Token: 0x06005B0B RID: 23307 RVA: 0x0012DED0 File Offset: 0x0012C0D0
				internal IListWrapperEnumWrapper(ArrayList.IListWrapper listWrapper, int startIndex, int count)
				{
					this._en = listWrapper.GetEnumerator();
					this._initialStartIndex = startIndex;
					this._initialCount = count;
					while (startIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = count;
					this._firstCall = true;
				}

				// Token: 0x06005B0C RID: 23308 RVA: 0x0012DF24 File Offset: 0x0012C124
				public object Clone()
				{
					return new ArrayList.IListWrapper.IListWrapperEnumWrapper
					{
						_en = (IEnumerator)((ICloneable)this._en).Clone(),
						_initialStartIndex = this._initialStartIndex,
						_initialCount = this._initialCount,
						_remaining = this._remaining,
						_firstCall = this._firstCall
					};
				}

				// Token: 0x06005B0D RID: 23309 RVA: 0x0012DF84 File Offset: 0x0012C184
				public bool MoveNext()
				{
					if (this._firstCall)
					{
						this._firstCall = false;
						int num = this._remaining;
						this._remaining = num - 1;
						return num > 0 && this._en.MoveNext();
					}
					if (this._remaining < 0)
					{
						return false;
					}
					if (this._en.MoveNext())
					{
						int num = this._remaining;
						this._remaining = num - 1;
						return num > 0;
					}
					return false;
				}

				// Token: 0x17000FD3 RID: 4051
				// (get) Token: 0x06005B0E RID: 23310 RVA: 0x0012DFF0 File Offset: 0x0012C1F0
				public object Current
				{
					get
					{
						if (this._firstCall)
						{
							throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
						}
						if (this._remaining < 0)
						{
							throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
						}
						return this._en.Current;
					}
				}

				// Token: 0x06005B0F RID: 23311 RVA: 0x0012E030 File Offset: 0x0012C230
				public void Reset()
				{
					this._en.Reset();
					int initialStartIndex = this._initialStartIndex;
					while (initialStartIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = this._initialCount;
					this._firstCall = true;
				}

				// Token: 0x04002F06 RID: 12038
				private IEnumerator _en;

				// Token: 0x04002F07 RID: 12039
				private int _remaining;

				// Token: 0x04002F08 RID: 12040
				private int _initialStartIndex;

				// Token: 0x04002F09 RID: 12041
				private int _initialCount;

				// Token: 0x04002F0A RID: 12042
				private bool _firstCall;
			}
		}

		// Token: 0x020009AF RID: 2479
		[Serializable]
		private class SyncArrayList : ArrayList
		{
			// Token: 0x06005B10 RID: 23312 RVA: 0x0012E077 File Offset: 0x0012C277
			internal SyncArrayList(ArrayList list)
				: base(false)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17000FD4 RID: 4052
			// (get) Token: 0x06005B11 RID: 23313 RVA: 0x0012E094 File Offset: 0x0012C294
			// (set) Token: 0x06005B12 RID: 23314 RVA: 0x0012E0DC File Offset: 0x0012C2DC
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list.Capacity = value;
					}
				}
			}

			// Token: 0x17000FD5 RID: 4053
			// (get) Token: 0x06005B13 RID: 23315 RVA: 0x0012E124 File Offset: 0x0012C324
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x17000FD6 RID: 4054
			// (get) Token: 0x06005B14 RID: 23316 RVA: 0x0012E16C File Offset: 0x0012C36C
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000FD7 RID: 4055
			// (get) Token: 0x06005B15 RID: 23317 RVA: 0x0012E179 File Offset: 0x0012C379
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17000FD8 RID: 4056
			// (get) Token: 0x06005B16 RID: 23318 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FD9 RID: 4057
			public override object this[int index]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[index];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x17000FDA RID: 4058
			// (get) Token: 0x06005B19 RID: 23321 RVA: 0x0012E218 File Offset: 0x0012C418
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06005B1A RID: 23322 RVA: 0x0012E220 File Offset: 0x0012C420
			public override int Add(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.Add(value);
				}
				return num;
			}

			// Token: 0x06005B1B RID: 23323 RVA: 0x0012E268 File Offset: 0x0012C468
			public override void AddRange(ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.AddRange(c);
				}
			}

			// Token: 0x06005B1C RID: 23324 RVA: 0x0012E2B0 File Offset: 0x0012C4B0
			public override int BinarySearch(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(value);
				}
				return num;
			}

			// Token: 0x06005B1D RID: 23325 RVA: 0x0012E2F8 File Offset: 0x0012C4F8
			public override int BinarySearch(object value, IComparer comparer)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(value, comparer);
				}
				return num;
			}

			// Token: 0x06005B1E RID: 23326 RVA: 0x0012E344 File Offset: 0x0012C544
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(index, count, value, comparer);
				}
				return num;
			}

			// Token: 0x06005B1F RID: 23327 RVA: 0x0012E390 File Offset: 0x0012C590
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06005B20 RID: 23328 RVA: 0x0012E3D8 File Offset: 0x0012C5D8
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new ArrayList.SyncArrayList((ArrayList)this._list.Clone());
				}
				return obj;
			}

			// Token: 0x06005B21 RID: 23329 RVA: 0x0012E42C File Offset: 0x0012C62C
			public override bool Contains(object item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(item);
				}
				return flag2;
			}

			// Token: 0x06005B22 RID: 23330 RVA: 0x0012E474 File Offset: 0x0012C674
			public override void CopyTo(Array array)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array);
				}
			}

			// Token: 0x06005B23 RID: 23331 RVA: 0x0012E4BC File Offset: 0x0012C6BC
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06005B24 RID: 23332 RVA: 0x0012E504 File Offset: 0x0012C704
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(index, array, arrayIndex, count);
				}
			}

			// Token: 0x06005B25 RID: 23333 RVA: 0x0012E550 File Offset: 0x0012C750
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005B26 RID: 23334 RVA: 0x0012E598 File Offset: 0x0012C798
			public override IEnumerator GetEnumerator(int index, int count)
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator(index, count);
				}
				return enumerator;
			}

			// Token: 0x06005B27 RID: 23335 RVA: 0x0012E5E4 File Offset: 0x0012C7E4
			public override int IndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value);
				}
				return num;
			}

			// Token: 0x06005B28 RID: 23336 RVA: 0x0012E62C File Offset: 0x0012C82C
			public override int IndexOf(object value, int startIndex)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value, startIndex);
				}
				return num;
			}

			// Token: 0x06005B29 RID: 23337 RVA: 0x0012E678 File Offset: 0x0012C878
			public override int IndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value, startIndex, count);
				}
				return num;
			}

			// Token: 0x06005B2A RID: 23338 RVA: 0x0012E6C4 File Offset: 0x0012C8C4
			public override void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06005B2B RID: 23339 RVA: 0x0012E70C File Offset: 0x0012C90C
			public override void InsertRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.InsertRange(index, c);
				}
			}

			// Token: 0x06005B2C RID: 23340 RVA: 0x0012E754 File Offset: 0x0012C954
			public override int LastIndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.LastIndexOf(value);
				}
				return num;
			}

			// Token: 0x06005B2D RID: 23341 RVA: 0x0012E79C File Offset: 0x0012C99C
			public override int LastIndexOf(object value, int startIndex)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.LastIndexOf(value, startIndex);
				}
				return num;
			}

			// Token: 0x06005B2E RID: 23342 RVA: 0x0012E7E8 File Offset: 0x0012C9E8
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.LastIndexOf(value, startIndex, count);
				}
				return num;
			}

			// Token: 0x06005B2F RID: 23343 RVA: 0x0012E834 File Offset: 0x0012CA34
			public override void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06005B30 RID: 23344 RVA: 0x0012E87C File Offset: 0x0012CA7C
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06005B31 RID: 23345 RVA: 0x0012E8C4 File Offset: 0x0012CAC4
			public override void RemoveRange(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveRange(index, count);
				}
			}

			// Token: 0x06005B32 RID: 23346 RVA: 0x0012E90C File Offset: 0x0012CB0C
			public override void Reverse(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Reverse(index, count);
				}
			}

			// Token: 0x06005B33 RID: 23347 RVA: 0x0012E954 File Offset: 0x0012CB54
			public override void SetRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetRange(index, c);
				}
			}

			// Token: 0x06005B34 RID: 23348 RVA: 0x0012E99C File Offset: 0x0012CB9C
			public override ArrayList GetRange(int index, int count)
			{
				object root = this._root;
				ArrayList range;
				lock (root)
				{
					range = this._list.GetRange(index, count);
				}
				return range;
			}

			// Token: 0x06005B35 RID: 23349 RVA: 0x0012E9E8 File Offset: 0x0012CBE8
			public override void Sort()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort();
				}
			}

			// Token: 0x06005B36 RID: 23350 RVA: 0x0012EA30 File Offset: 0x0012CC30
			public override void Sort(IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(comparer);
				}
			}

			// Token: 0x06005B37 RID: 23351 RVA: 0x0012EA78 File Offset: 0x0012CC78
			public override void Sort(int index, int count, IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(index, count, comparer);
				}
			}

			// Token: 0x06005B38 RID: 23352 RVA: 0x0012EAC0 File Offset: 0x0012CCC0
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._list.ToArray();
				}
				return array;
			}

			// Token: 0x06005B39 RID: 23353 RVA: 0x0012EB08 File Offset: 0x0012CD08
			public override Array ToArray(Type type)
			{
				object root = this._root;
				Array array;
				lock (root)
				{
					array = this._list.ToArray(type);
				}
				return array;
			}

			// Token: 0x06005B3A RID: 23354 RVA: 0x0012EB50 File Offset: 0x0012CD50
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x04002F0B RID: 12043
			private ArrayList _list;

			// Token: 0x04002F0C RID: 12044
			private object _root;
		}

		// Token: 0x020009B0 RID: 2480
		[Serializable]
		private class SyncIList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005B3B RID: 23355 RVA: 0x0012EB98 File Offset: 0x0012CD98
			internal SyncIList(IList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17000FDB RID: 4059
			// (get) Token: 0x06005B3C RID: 23356 RVA: 0x0012EBB4 File Offset: 0x0012CDB4
			public virtual int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x17000FDC RID: 4060
			// (get) Token: 0x06005B3D RID: 23357 RVA: 0x0012EBFC File Offset: 0x0012CDFC
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000FDD RID: 4061
			// (get) Token: 0x06005B3E RID: 23358 RVA: 0x0012EC09 File Offset: 0x0012CE09
			public virtual bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17000FDE RID: 4062
			// (get) Token: 0x06005B3F RID: 23359 RVA: 0x00003B29 File Offset: 0x00001D29
			public virtual bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FDF RID: 4063
			public virtual object this[int index]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[index];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x17000FE0 RID: 4064
			// (get) Token: 0x06005B42 RID: 23362 RVA: 0x0012ECA8 File Offset: 0x0012CEA8
			public virtual object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06005B43 RID: 23363 RVA: 0x0012ECB0 File Offset: 0x0012CEB0
			public virtual int Add(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.Add(value);
				}
				return num;
			}

			// Token: 0x06005B44 RID: 23364 RVA: 0x0012ECF8 File Offset: 0x0012CEF8
			public virtual void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06005B45 RID: 23365 RVA: 0x0012ED40 File Offset: 0x0012CF40
			public virtual bool Contains(object item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(item);
				}
				return flag2;
			}

			// Token: 0x06005B46 RID: 23366 RVA: 0x0012ED88 File Offset: 0x0012CF88
			public virtual void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06005B47 RID: 23367 RVA: 0x0012EDD0 File Offset: 0x0012CFD0
			public virtual IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005B48 RID: 23368 RVA: 0x0012EE18 File Offset: 0x0012D018
			public virtual int IndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value);
				}
				return num;
			}

			// Token: 0x06005B49 RID: 23369 RVA: 0x0012EE60 File Offset: 0x0012D060
			public virtual void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06005B4A RID: 23370 RVA: 0x0012EEA8 File Offset: 0x0012D0A8
			public virtual void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06005B4B RID: 23371 RVA: 0x0012EEF0 File Offset: 0x0012D0F0
			public virtual void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x04002F0D RID: 12045
			private IList _list;

			// Token: 0x04002F0E RID: 12046
			private object _root;
		}

		// Token: 0x020009B1 RID: 2481
		[Serializable]
		private class FixedSizeList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005B4C RID: 23372 RVA: 0x0012EF38 File Offset: 0x0012D138
			internal FixedSizeList(IList l)
			{
				this._list = l;
			}

			// Token: 0x17000FE1 RID: 4065
			// (get) Token: 0x06005B4D RID: 23373 RVA: 0x0012EF47 File Offset: 0x0012D147
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000FE2 RID: 4066
			// (get) Token: 0x06005B4E RID: 23374 RVA: 0x0012EF54 File Offset: 0x0012D154
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000FE3 RID: 4067
			// (get) Token: 0x06005B4F RID: 23375 RVA: 0x00003B29 File Offset: 0x00001D29
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FE4 RID: 4068
			// (get) Token: 0x06005B50 RID: 23376 RVA: 0x0012EF61 File Offset: 0x0012D161
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000FE5 RID: 4069
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
				}
			}

			// Token: 0x17000FE6 RID: 4070
			// (get) Token: 0x06005B53 RID: 23379 RVA: 0x0012EF8B File Offset: 0x0012D18B
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005B54 RID: 23380 RVA: 0x0012EF98 File Offset: 0x0012D198
			public virtual int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B55 RID: 23381 RVA: 0x0012EF98 File Offset: 0x0012D198
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B56 RID: 23382 RVA: 0x0012EFA9 File Offset: 0x0012D1A9
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005B57 RID: 23383 RVA: 0x0012EFB7 File Offset: 0x0012D1B7
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005B58 RID: 23384 RVA: 0x0012EFC6 File Offset: 0x0012D1C6
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005B59 RID: 23385 RVA: 0x0012EFD3 File Offset: 0x0012D1D3
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005B5A RID: 23386 RVA: 0x0012EF98 File Offset: 0x0012D198
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B5B RID: 23387 RVA: 0x0012EF98 File Offset: 0x0012D198
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B5C RID: 23388 RVA: 0x0012EF98 File Offset: 0x0012D198
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x04002F0F RID: 12047
			private IList _list;
		}

		// Token: 0x020009B2 RID: 2482
		[Serializable]
		private class FixedSizeArrayList : ArrayList
		{
			// Token: 0x06005B5D RID: 23389 RVA: 0x0012EFE1 File Offset: 0x0012D1E1
			internal FixedSizeArrayList(ArrayList l)
			{
				this._list = l;
				this._version = this._list._version;
			}

			// Token: 0x17000FE7 RID: 4071
			// (get) Token: 0x06005B5E RID: 23390 RVA: 0x0012F001 File Offset: 0x0012D201
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000FE8 RID: 4072
			// (get) Token: 0x06005B5F RID: 23391 RVA: 0x0012F00E File Offset: 0x0012D20E
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000FE9 RID: 4073
			// (get) Token: 0x06005B60 RID: 23392 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FEA RID: 4074
			// (get) Token: 0x06005B61 RID: 23393 RVA: 0x0012F01B File Offset: 0x0012D21B
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000FEB RID: 4075
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version = this._list._version;
				}
			}

			// Token: 0x17000FEC RID: 4076
			// (get) Token: 0x06005B64 RID: 23396 RVA: 0x0012F056 File Offset: 0x0012D256
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005B65 RID: 23397 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B66 RID: 23398 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B67 RID: 23399 RVA: 0x0012F063 File Offset: 0x0012D263
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x17000FED RID: 4077
			// (get) Token: 0x06005B68 RID: 23400 RVA: 0x0012F075 File Offset: 0x0012D275
			// (set) Token: 0x06005B69 RID: 23401 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
				}
			}

			// Token: 0x06005B6A RID: 23402 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B6B RID: 23403 RVA: 0x0012F082 File Offset: 0x0012D282
			public override object Clone()
			{
				return new ArrayList.FixedSizeArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06005B6C RID: 23404 RVA: 0x0012F0A5 File Offset: 0x0012D2A5
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005B6D RID: 23405 RVA: 0x0012F0B3 File Offset: 0x0012D2B3
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005B6E RID: 23406 RVA: 0x0012F0C2 File Offset: 0x0012D2C2
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06005B6F RID: 23407 RVA: 0x0012F0D4 File Offset: 0x0012D2D4
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005B70 RID: 23408 RVA: 0x0012F0E1 File Offset: 0x0012D2E1
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06005B71 RID: 23409 RVA: 0x0012F0F0 File Offset: 0x0012D2F0
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005B72 RID: 23410 RVA: 0x0012F0FE File Offset: 0x0012D2FE
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06005B73 RID: 23411 RVA: 0x0012F10D File Offset: 0x0012D30D
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06005B74 RID: 23412 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B75 RID: 23413 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B76 RID: 23414 RVA: 0x0012F11D File Offset: 0x0012D31D
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06005B77 RID: 23415 RVA: 0x0012F12B File Offset: 0x0012D32B
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06005B78 RID: 23416 RVA: 0x0012F13A File Offset: 0x0012D33A
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06005B79 RID: 23417 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B7A RID: 23418 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B7B RID: 23419 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x06005B7C RID: 23420 RVA: 0x0012F14A File Offset: 0x0012D34A
			public override void SetRange(int index, ICollection c)
			{
				this._list.SetRange(index, c);
				this._version = this._list._version;
			}

			// Token: 0x06005B7D RID: 23421 RVA: 0x0012F16C File Offset: 0x0012D36C
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06005B7E RID: 23422 RVA: 0x0012F1C4 File Offset: 0x0012D3C4
			public override void Reverse(int index, int count)
			{
				this._list.Reverse(index, count);
				this._version = this._list._version;
			}

			// Token: 0x06005B7F RID: 23423 RVA: 0x0012F1E4 File Offset: 0x0012D3E4
			public override void Sort(int index, int count, IComparer comparer)
			{
				this._list.Sort(index, count, comparer);
				this._version = this._list._version;
			}

			// Token: 0x06005B80 RID: 23424 RVA: 0x0012F205 File Offset: 0x0012D405
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06005B81 RID: 23425 RVA: 0x0012F212 File Offset: 0x0012D412
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06005B82 RID: 23426 RVA: 0x0012EF98 File Offset: 0x0012D198
			public override void TrimToSize()
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection was of a fixed size."));
			}

			// Token: 0x04002F10 RID: 12048
			private ArrayList _list;
		}

		// Token: 0x020009B3 RID: 2483
		[Serializable]
		private class ReadOnlyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005B83 RID: 23427 RVA: 0x0012F220 File Offset: 0x0012D420
			internal ReadOnlyList(IList l)
			{
				this._list = l;
			}

			// Token: 0x17000FEE RID: 4078
			// (get) Token: 0x06005B84 RID: 23428 RVA: 0x0012F22F File Offset: 0x0012D42F
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000FEF RID: 4079
			// (get) Token: 0x06005B85 RID: 23429 RVA: 0x00003B29 File Offset: 0x00001D29
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FF0 RID: 4080
			// (get) Token: 0x06005B86 RID: 23430 RVA: 0x00003B29 File Offset: 0x00001D29
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FF1 RID: 4081
			// (get) Token: 0x06005B87 RID: 23431 RVA: 0x0012F23C File Offset: 0x0012D43C
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000FF2 RID: 4082
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
				}
			}

			// Token: 0x17000FF3 RID: 4083
			// (get) Token: 0x06005B8A RID: 23434 RVA: 0x0012F268 File Offset: 0x0012D468
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005B8B RID: 23435 RVA: 0x0012F257 File Offset: 0x0012D457
			public virtual int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005B8C RID: 23436 RVA: 0x0012F257 File Offset: 0x0012D457
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005B8D RID: 23437 RVA: 0x0012F275 File Offset: 0x0012D475
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005B8E RID: 23438 RVA: 0x0012F283 File Offset: 0x0012D483
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005B8F RID: 23439 RVA: 0x0012F292 File Offset: 0x0012D492
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005B90 RID: 23440 RVA: 0x0012F29F File Offset: 0x0012D49F
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005B91 RID: 23441 RVA: 0x0012F257 File Offset: 0x0012D457
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005B92 RID: 23442 RVA: 0x0012F257 File Offset: 0x0012D457
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005B93 RID: 23443 RVA: 0x0012F257 File Offset: 0x0012D457
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x04002F11 RID: 12049
			private IList _list;
		}

		// Token: 0x020009B4 RID: 2484
		[Serializable]
		private class ReadOnlyArrayList : ArrayList
		{
			// Token: 0x06005B94 RID: 23444 RVA: 0x0012F2AD File Offset: 0x0012D4AD
			internal ReadOnlyArrayList(ArrayList l)
			{
				this._list = l;
			}

			// Token: 0x17000FF4 RID: 4084
			// (get) Token: 0x06005B95 RID: 23445 RVA: 0x0012F2BC File Offset: 0x0012D4BC
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17000FF5 RID: 4085
			// (get) Token: 0x06005B96 RID: 23446 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FF6 RID: 4086
			// (get) Token: 0x06005B97 RID: 23447 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FF7 RID: 4087
			// (get) Token: 0x06005B98 RID: 23448 RVA: 0x0012F2C9 File Offset: 0x0012D4C9
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000FF8 RID: 4088
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
				}
			}

			// Token: 0x17000FF9 RID: 4089
			// (get) Token: 0x06005B9B RID: 23451 RVA: 0x0012F2E4 File Offset: 0x0012D4E4
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06005B9C RID: 23452 RVA: 0x0012F257 File Offset: 0x0012D457
			public override int Add(object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005B9D RID: 23453 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005B9E RID: 23454 RVA: 0x0012F2F1 File Offset: 0x0012D4F1
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x17000FFA RID: 4090
			// (get) Token: 0x06005B9F RID: 23455 RVA: 0x0012F303 File Offset: 0x0012D503
			// (set) Token: 0x06005BA0 RID: 23456 RVA: 0x0012F257 File Offset: 0x0012D457
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
				}
			}

			// Token: 0x06005BA1 RID: 23457 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005BA2 RID: 23458 RVA: 0x0012F310 File Offset: 0x0012D510
			public override object Clone()
			{
				return new ArrayList.ReadOnlyArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06005BA3 RID: 23459 RVA: 0x0012F333 File Offset: 0x0012D533
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06005BA4 RID: 23460 RVA: 0x0012F341 File Offset: 0x0012D541
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06005BA5 RID: 23461 RVA: 0x0012F350 File Offset: 0x0012D550
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06005BA6 RID: 23462 RVA: 0x0012F362 File Offset: 0x0012D562
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06005BA7 RID: 23463 RVA: 0x0012F36F File Offset: 0x0012D56F
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06005BA8 RID: 23464 RVA: 0x0012F37E File Offset: 0x0012D57E
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06005BA9 RID: 23465 RVA: 0x0012F38C File Offset: 0x0012D58C
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06005BAA RID: 23466 RVA: 0x0012F39B File Offset: 0x0012D59B
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06005BAB RID: 23467 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005BAC RID: 23468 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005BAD RID: 23469 RVA: 0x0012F3AB File Offset: 0x0012D5AB
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x06005BAE RID: 23470 RVA: 0x0012F3B9 File Offset: 0x0012D5B9
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x06005BAF RID: 23471 RVA: 0x0012F3C8 File Offset: 0x0012D5C8
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x06005BB0 RID: 23472 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005BB1 RID: 23473 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005BB2 RID: 23474 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005BB3 RID: 23475 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void SetRange(int index, ICollection c)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005BB4 RID: 23476 RVA: 0x0012F3D8 File Offset: 0x0012D5D8
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06005BB5 RID: 23477 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void Reverse(int index, int count)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005BB6 RID: 23478 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void Sort(int index, int count, IComparer comparer)
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x06005BB7 RID: 23479 RVA: 0x0012F430 File Offset: 0x0012D630
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06005BB8 RID: 23480 RVA: 0x0012F43D File Offset: 0x0012D63D
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06005BB9 RID: 23481 RVA: 0x0012F257 File Offset: 0x0012D457
			public override void TrimToSize()
			{
				throw new NotSupportedException(Environment.GetResourceString("Collection is read-only."));
			}

			// Token: 0x04002F12 RID: 12050
			private ArrayList _list;
		}

		// Token: 0x020009B5 RID: 2485
		[Serializable]
		private sealed class ArrayListEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06005BBA RID: 23482 RVA: 0x0012F44B File Offset: 0x0012D64B
			internal ArrayListEnumerator(ArrayList list, int index, int count)
			{
				this.list = list;
				this.startIndex = index;
				this.index = index - 1;
				this.endIndex = this.index + count;
				this.version = list._version;
				this.currentElement = null;
			}

			// Token: 0x06005BBB RID: 23483 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005BBC RID: 23484 RVA: 0x0012F48C File Offset: 0x0012D68C
			public bool MoveNext()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				if (this.index < this.endIndex)
				{
					ArrayList arrayList = this.list;
					int num = this.index + 1;
					this.index = num;
					this.currentElement = arrayList[num];
					return true;
				}
				this.index = this.endIndex + 1;
				return false;
			}

			// Token: 0x17000FFB RID: 4091
			// (get) Token: 0x06005BBD RID: 23485 RVA: 0x0012F500 File Offset: 0x0012D700
			public object Current
			{
				get
				{
					if (this.index < this.startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this.index > this.endIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06005BBE RID: 23486 RVA: 0x0012F54F File Offset: 0x0012D74F
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				this.index = this.startIndex - 1;
			}

			// Token: 0x04002F13 RID: 12051
			private ArrayList list;

			// Token: 0x04002F14 RID: 12052
			private int index;

			// Token: 0x04002F15 RID: 12053
			private int endIndex;

			// Token: 0x04002F16 RID: 12054
			private int version;

			// Token: 0x04002F17 RID: 12055
			private object currentElement;

			// Token: 0x04002F18 RID: 12056
			private int startIndex;
		}

		// Token: 0x020009B6 RID: 2486
		[Serializable]
		private class Range : ArrayList
		{
			// Token: 0x06005BBF RID: 23487 RVA: 0x0012F582 File Offset: 0x0012D782
			internal Range(ArrayList list, int index, int count)
				: base(false)
			{
				this._baseList = list;
				this._baseIndex = index;
				this._baseSize = count;
				this._baseVersion = list._version;
				this._version = list._version;
			}

			// Token: 0x06005BC0 RID: 23488 RVA: 0x0012F5B8 File Offset: 0x0012D7B8
			private void InternalUpdateRange()
			{
				if (this._baseVersion != this._baseList._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("This range in the underlying list is invalid. A possible cause is that elements were removed."));
				}
			}

			// Token: 0x06005BC1 RID: 23489 RVA: 0x0012F5DD File Offset: 0x0012D7DD
			private void InternalUpdateVersion()
			{
				this._baseVersion++;
				this._version++;
			}

			// Token: 0x06005BC2 RID: 23490 RVA: 0x0012F5FC File Offset: 0x0012D7FC
			public override int Add(object value)
			{
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + this._baseSize, value);
				this.InternalUpdateVersion();
				int baseSize = this._baseSize;
				this._baseSize = baseSize + 1;
				return baseSize;
			}

			// Token: 0x06005BC3 RID: 23491 RVA: 0x0012F640 File Offset: 0x0012D840
			public override void AddRange(ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + this._baseSize, c);
					this.InternalUpdateVersion();
					this._baseSize += count;
				}
			}

			// Token: 0x06005BC4 RID: 23492 RVA: 0x0012F69C File Offset: 0x0012D89C
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				this.InternalUpdateRange();
				int num = this._baseList.BinarySearch(this._baseIndex + index, count, value, comparer);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return num + this._baseIndex;
			}

			// Token: 0x17000FFC RID: 4092
			// (get) Token: 0x06005BC5 RID: 23493 RVA: 0x0012F71F File Offset: 0x0012D91F
			// (set) Token: 0x06005BC6 RID: 23494 RVA: 0x0012D5FD File Offset: 0x0012B7FD
			public override int Capacity
			{
				get
				{
					return this._baseList.Capacity;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("capacity was less than the current size."));
					}
				}
			}

			// Token: 0x06005BC7 RID: 23495 RVA: 0x0012F72C File Offset: 0x0012D92C
			public override void Clear()
			{
				this.InternalUpdateRange();
				if (this._baseSize != 0)
				{
					this._baseList.RemoveRange(this._baseIndex, this._baseSize);
					this.InternalUpdateVersion();
					this._baseSize = 0;
				}
			}

			// Token: 0x06005BC8 RID: 23496 RVA: 0x0012F760 File Offset: 0x0012D960
			public override object Clone()
			{
				this.InternalUpdateRange();
				return new ArrayList.Range(this._baseList, this._baseIndex, this._baseSize)
				{
					_baseList = (ArrayList)this._baseList.Clone()
				};
			}

			// Token: 0x06005BC9 RID: 23497 RVA: 0x0012F798 File Offset: 0x0012D998
			public override bool Contains(object item)
			{
				this.InternalUpdateRange();
				if (item == null)
				{
					for (int i = 0; i < this._baseSize; i++)
					{
						if (this._baseList[this._baseIndex + i] == null)
						{
							return true;
						}
					}
					return false;
				}
				for (int j = 0; j < this._baseSize; j++)
				{
					if (this._baseList[this._baseIndex + j] != null && this._baseList[this._baseIndex + j].Equals(item))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06005BCA RID: 23498 RVA: 0x0012F81C File Offset: 0x0012DA1C
			public override void CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
				}
				if (array.Length - index < this._baseSize)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex, array, index, this._baseSize);
			}

			// Token: 0x06005BCB RID: 23499 RVA: 0x0012F8A8 File Offset: 0x0012DAA8
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex + index, array, arrayIndex, count);
			}

			// Token: 0x17000FFD RID: 4093
			// (get) Token: 0x06005BCC RID: 23500 RVA: 0x0012F95A File Offset: 0x0012DB5A
			public override int Count
			{
				get
				{
					this.InternalUpdateRange();
					return this._baseSize;
				}
			}

			// Token: 0x17000FFE RID: 4094
			// (get) Token: 0x06005BCD RID: 23501 RVA: 0x0012F968 File Offset: 0x0012DB68
			public override bool IsReadOnly
			{
				get
				{
					return this._baseList.IsReadOnly;
				}
			}

			// Token: 0x17000FFF RID: 4095
			// (get) Token: 0x06005BCE RID: 23502 RVA: 0x0012F975 File Offset: 0x0012DB75
			public override bool IsFixedSize
			{
				get
				{
					return this._baseList.IsFixedSize;
				}
			}

			// Token: 0x17001000 RID: 4096
			// (get) Token: 0x06005BCF RID: 23503 RVA: 0x0012F982 File Offset: 0x0012DB82
			public override bool IsSynchronized
			{
				get
				{
					return this._baseList.IsSynchronized;
				}
			}

			// Token: 0x06005BD0 RID: 23504 RVA: 0x0012F98F File Offset: 0x0012DB8F
			public override IEnumerator GetEnumerator()
			{
				return this.GetEnumerator(0, this._baseSize);
			}

			// Token: 0x06005BD1 RID: 23505 RVA: 0x0012F9A0 File Offset: 0x0012DBA0
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				this.InternalUpdateRange();
				return this._baseList.GetEnumerator(this._baseIndex + index, count);
			}

			// Token: 0x06005BD2 RID: 23506 RVA: 0x0012FA0C File Offset: 0x0012DC0C
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				this.InternalUpdateRange();
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x17001001 RID: 4097
			// (get) Token: 0x06005BD3 RID: 23507 RVA: 0x0012FA6A File Offset: 0x0012DC6A
			public override object SyncRoot
			{
				get
				{
					return this._baseList.SyncRoot;
				}
			}

			// Token: 0x06005BD4 RID: 23508 RVA: 0x0012FA78 File Offset: 0x0012DC78
			public override int IndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005BD5 RID: 23509 RVA: 0x0012FAB4 File Offset: 0x0012DCB4
			public override int IndexOf(object value, int startIndex)
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Non-negative number required."));
				}
				if (startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, this._baseSize - startIndex);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005BD6 RID: 23510 RVA: 0x0012FB2C File Offset: 0x0012DD2C
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (count < 0 || startIndex > this._baseSize - count)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005BD7 RID: 23511 RVA: 0x0012FBAC File Offset: 0x0012DDAC
			public override void Insert(int index, object value)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + index, value);
				this.InternalUpdateVersion();
				this._baseSize++;
			}

			// Token: 0x06005BD8 RID: 23512 RVA: 0x0012FC0C File Offset: 0x0012DE0C
			public override void InsertRange(int index, ICollection c)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + index, c);
					this._baseSize += count;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06005BD9 RID: 23513 RVA: 0x0012FC84 File Offset: 0x0012DE84
			public override int LastIndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.LastIndexOf(value, this._baseIndex + this._baseSize - 1, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005BDA RID: 23514 RVA: 0x0012DA98 File Offset: 0x0012BC98
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06005BDB RID: 23515 RVA: 0x0012FCC8 File Offset: 0x0012DEC8
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				this.InternalUpdateRange();
				if (this._baseSize == 0)
				{
					return -1;
				}
				if (startIndex >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Non-negative number required."));
				}
				int num = this._baseList.LastIndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06005BDC RID: 23516 RVA: 0x0012FD40 File Offset: 0x0012DF40
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				this.InternalUpdateRange();
				this._baseList.RemoveAt(this._baseIndex + index);
				this.InternalUpdateVersion();
				this._baseSize--;
			}

			// Token: 0x06005BDD RID: 23517 RVA: 0x0012FD9C File Offset: 0x0012DF9C
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				this.InternalUpdateRange();
				if (count > 0)
				{
					this._baseList.RemoveRange(this._baseIndex + index, count);
					this.InternalUpdateVersion();
					this._baseSize -= count;
				}
			}

			// Token: 0x06005BDE RID: 23518 RVA: 0x0012FE20 File Offset: 0x0012E020
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				this.InternalUpdateRange();
				this._baseList.Reverse(this._baseIndex + index, count);
				this.InternalUpdateVersion();
			}

			// Token: 0x06005BDF RID: 23519 RVA: 0x0012FE90 File Offset: 0x0012E090
			public override void SetRange(int index, ICollection c)
			{
				this.InternalUpdateRange();
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				this._baseList.SetRange(this._baseIndex + index, c);
				if (c.Count > 0)
				{
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06005BE0 RID: 23520 RVA: 0x0012FEE8 File Offset: 0x0012E0E8
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				this.InternalUpdateRange();
				this._baseList.Sort(this._baseIndex + index, count, comparer);
				this.InternalUpdateVersion();
			}

			// Token: 0x17001002 RID: 4098
			public override object this[int index]
			{
				get
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
					}
					return this._baseList[this._baseIndex + index];
				}
				set
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
					}
					this._baseList[this._baseIndex + index] = value;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06005BE3 RID: 23523 RVA: 0x0012FFE8 File Offset: 0x0012E1E8
			public override object[] ToArray()
			{
				this.InternalUpdateRange();
				object[] array = new object[this._baseSize];
				Array.Copy(this._baseList._items, this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06005BE4 RID: 23524 RVA: 0x00130028 File Offset: 0x0012E228
			[SecuritySafeCritical]
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				this.InternalUpdateRange();
				Array array = Array.UnsafeCreateInstance(type, new int[] { this._baseSize });
				this._baseList.CopyTo(this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06005BE5 RID: 23525 RVA: 0x0013007F File Offset: 0x0012E27F
			public override void TrimToSize()
			{
				throw new NotSupportedException(Environment.GetResourceString("The specified operation is not supported on Ranges."));
			}

			// Token: 0x04002F19 RID: 12057
			private ArrayList _baseList;

			// Token: 0x04002F1A RID: 12058
			private int _baseIndex;

			// Token: 0x04002F1B RID: 12059
			private int _baseSize;

			// Token: 0x04002F1C RID: 12060
			private int _baseVersion;
		}

		// Token: 0x020009B7 RID: 2487
		[Serializable]
		private sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06005BE6 RID: 23526 RVA: 0x00130090 File Offset: 0x0012E290
			internal ArrayListEnumeratorSimple(ArrayList list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
				this.isArrayList = list.GetType() == typeof(ArrayList);
				this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
			}

			// Token: 0x06005BE7 RID: 23527 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005BE8 RID: 23528 RVA: 0x001300E4 File Offset: 0x0012E2E4
			public bool MoveNext()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				if (this.isArrayList)
				{
					if (this.index < this.list._size - 1)
					{
						object[] items = this.list._items;
						int num = this.index + 1;
						this.index = num;
						this.currentElement = items[num];
						return true;
					}
					this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
					this.index = this.list._size;
					return false;
				}
				else
				{
					if (this.index < this.list.Count - 1)
					{
						ArrayList arrayList = this.list;
						int num = this.index + 1;
						this.index = num;
						this.currentElement = arrayList[num];
						return true;
					}
					this.index = this.list.Count;
					this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
					return false;
				}
			}

			// Token: 0x17001003 RID: 4099
			// (get) Token: 0x06005BE9 RID: 23529 RVA: 0x001301CC File Offset: 0x0012E3CC
			public object Current
			{
				get
				{
					object obj = this.currentElement;
					if (ArrayList.ArrayListEnumeratorSimple.dummyObject != obj)
					{
						return obj;
					}
					if (this.index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
				}
			}

			// Token: 0x06005BEA RID: 23530 RVA: 0x00130212 File Offset: 0x0012E412
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
				this.index = -1;
			}

			// Token: 0x04002F1D RID: 12061
			private ArrayList list;

			// Token: 0x04002F1E RID: 12062
			private int index;

			// Token: 0x04002F1F RID: 12063
			private int version;

			// Token: 0x04002F20 RID: 12064
			private object currentElement;

			// Token: 0x04002F21 RID: 12065
			[NonSerialized]
			private bool isArrayList;

			// Token: 0x04002F22 RID: 12066
			private static object dummyObject = new object();
		}

		// Token: 0x020009B8 RID: 2488
		internal class ArrayListDebugView
		{
			// Token: 0x06005BEC RID: 23532 RVA: 0x00130255 File Offset: 0x0012E455
			public ArrayListDebugView(ArrayList arrayList)
			{
				if (arrayList == null)
				{
					throw new ArgumentNullException("arrayList");
				}
				this.arrayList = arrayList;
			}

			// Token: 0x17001004 RID: 4100
			// (get) Token: 0x06005BED RID: 23533 RVA: 0x00130272 File Offset: 0x0012E472
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.arrayList.ToArray();
				}
			}

			// Token: 0x04002F23 RID: 12067
			private ArrayList arrayList;
		}
	}
}

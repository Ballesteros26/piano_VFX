using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Collections.Generic
{
	/// <summary>Represents a set of values.</summary>
	/// <typeparam name="T">The type of elements in the hash set.</typeparam>
	// Token: 0x02000350 RID: 848
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[Serializable]
	public class HashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that is empty and uses the default equality comparer for the set type.</summary>
		// Token: 0x060019BF RID: 6591 RVA: 0x000547E6 File Offset: 0x000529E6
		public HashSet()
			: this(EqualityComparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that is empty and uses the specified equality comparer for the set type.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing values in the set, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> implementation for the set type.</param>
		// Token: 0x060019C0 RID: 6592 RVA: 0x000547F3 File Offset: 0x000529F3
		public HashSet(IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			this._comparer = comparer;
			this._lastIndex = 0;
			this._count = 0;
			this._freeList = -1;
			this._version = 0;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00054828 File Offset: 0x00052A28
		public HashSet(int capacity)
			: this(capacity, EqualityComparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that uses the default equality comparer for the set type, contains elements copied from the specified collection, and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x060019C2 RID: 6594 RVA: 0x00054836 File Offset: 0x00052A36
		public HashSet(IEnumerable<T> collection)
			: this(collection, EqualityComparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that uses the specified equality comparer for the set type, contains elements copied from the specified collection, and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new set.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing values in the set, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> implementation for the set type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x060019C3 RID: 6595 RVA: 0x00054844 File Offset: 0x00052A44
		public HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			HashSet<T> hashSet = collection as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				this.CopyFrom(hashSet);
				return;
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			int num = ((collection2 == null) ? 0 : collection2.Count);
			this.Initialize(num);
			this.UnionWith(collection);
			if (this._count > 0 && this._slots.Length / this._count > 3)
			{
				this.TrimExcess();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class with serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		// Token: 0x060019C4 RID: 6596 RVA: 0x000548C4 File Offset: 0x00052AC4
		protected HashSet(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000548D4 File Offset: 0x00052AD4
		private void CopyFrom(HashSet<T> source)
		{
			int count = source._count;
			if (count == 0)
			{
				return;
			}
			int num = source._buckets.Length;
			if (HashHelpers.ExpandPrime(count + 1) >= num)
			{
				this._buckets = (int[])source._buckets.Clone();
				this._slots = (HashSet<T>.Slot[])source._slots.Clone();
				this._lastIndex = source._lastIndex;
				this._freeList = source._freeList;
			}
			else
			{
				int lastIndex = source._lastIndex;
				HashSet<T>.Slot[] slots = source._slots;
				this.Initialize(count);
				int num2 = 0;
				for (int i = 0; i < lastIndex; i++)
				{
					int hashCode = slots[i].hashCode;
					if (hashCode >= 0)
					{
						this.AddValue(num2, hashCode, slots[i].value);
						num2++;
					}
				}
				this._lastIndex = num2;
			}
			this._count = count;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000549AE File Offset: 0x00052BAE
		public HashSet(int capacity, IEqualityComparer<T> comparer)
			: this(comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
		}

		/// <summary>Adds an item to an <see cref="T:System.Collections.Generic.ICollection`1" /> object.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
		// Token: 0x060019C7 RID: 6599 RVA: 0x000549D1 File Offset: 0x00052BD1
		void ICollection<T>.Add(T item)
		{
			this.AddIfNotPresent(item);
		}

		/// <summary>Removes all elements from a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		// Token: 0x060019C8 RID: 6600 RVA: 0x000549DC File Offset: 0x00052BDC
		public void Clear()
		{
			if (this._lastIndex > 0)
			{
				Array.Clear(this._slots, 0, this._lastIndex);
				Array.Clear(this._buckets, 0, this._buckets.Length);
				this._lastIndex = 0;
				this._count = 0;
				this._freeList = -1;
			}
			this._version++;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object contains the specified element.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.HashSet`1" /> object contains the specified element; otherwise, false.</returns>
		/// <param name="item">The element to locate in the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		// Token: 0x060019C9 RID: 6601 RVA: 0x00054A3C File Offset: 0x00052C3C
		public bool Contains(T item)
		{
			if (this._buckets != null)
			{
				int num = this.InternalGetHashCode(item);
				for (int i = this._buckets[num % this._buckets.Length] - 1; i >= 0; i = this._slots[i].next)
				{
					if (this._slots[i].hashCode == num && this._comparer.Equals(this._slots[i].value, item))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Copies the elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.HashSet`1" /> object. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="arrayIndex" /> is greater than the length of the destination <paramref name="array" />.</exception>
		// Token: 0x060019CA RID: 6602 RVA: 0x00054ABB File Offset: 0x00052CBB
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex, this._count);
		}

		/// <summary>Removes the specified element from a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <returns>true if the element is successfully found and removed; otherwise, false.  This method returns false if <paramref name="item" /> is not found in the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</returns>
		/// <param name="item">The element to remove.</param>
		// Token: 0x060019CB RID: 6603 RVA: 0x00054ACC File Offset: 0x00052CCC
		public bool Remove(T item)
		{
			if (this._buckets != null)
			{
				int num = this.InternalGetHashCode(item);
				int num2 = num % this._buckets.Length;
				int num3 = -1;
				for (int i = this._buckets[num2] - 1; i >= 0; i = this._slots[i].next)
				{
					if (this._slots[i].hashCode == num && this._comparer.Equals(this._slots[i].value, item))
					{
						if (num3 < 0)
						{
							this._buckets[num2] = this._slots[i].next + 1;
						}
						else
						{
							this._slots[num3].next = this._slots[i].next;
						}
						this._slots[i].hashCode = -1;
						if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
						{
							this._slots[i].value = default(T);
						}
						this._slots[i].next = this._freeList;
						this._count--;
						this._version++;
						if (this._count == 0)
						{
							this._lastIndex = 0;
							this._freeList = -1;
						}
						else
						{
							this._freeList = i;
						}
						return true;
					}
					num3 = i;
				}
			}
			return false;
		}

		/// <summary>Gets the number of elements that are contained in a set.</summary>
		/// <returns>The number of elements that are contained in the set.</returns>
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00054C25 File Offset: 0x00052E25
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Gets a value indicating whether a collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00002285 File Offset: 0x00000485
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns an enumerator that iterates through a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.HashSet`1.Enumerator" /> object for the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</returns>
		// Token: 0x060019CE RID: 6606 RVA: 0x00054C2D File Offset: 0x00052E2D
		public HashSet<T>.Enumerator GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060019CF RID: 6607 RVA: 0x00054C35 File Offset: 0x00052E35
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060019D0 RID: 6608 RVA: 0x00054C35 File Offset: 0x00052E35
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060019D1 RID: 6609 RVA: 0x00054C44 File Offset: 0x00052E44
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this._version);
			info.AddValue("Comparer", this._comparer, typeof(IComparer<T>));
			info.AddValue("Capacity", (this._buckets == null) ? 0 : this._buckets.Length);
			if (this._buckets != null)
			{
				T[] array = new T[this._count];
				this.CopyTo(array);
				info.AddValue("Elements", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.HashSet`1" /> object is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060019D2 RID: 6610 RVA: 0x00054CDC File Offset: 0x00052EDC
		public virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				return;
			}
			int @int = this._siInfo.GetInt32("Capacity");
			this._comparer = (IEqualityComparer<T>)this._siInfo.GetValue("Comparer", typeof(IEqualityComparer<T>));
			this._freeList = -1;
			if (@int != 0)
			{
				this._buckets = new int[@int];
				this._slots = new HashSet<T>.Slot[@int];
				T[] array = (T[])this._siInfo.GetValue("Elements", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The Keys for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.AddIfNotPresent(array[i]);
				}
			}
			else
			{
				this._buckets = null;
			}
			this._version = this._siInfo.GetInt32("Version");
			this._siInfo = null;
		}

		/// <summary>Adds the specified element to a set.</summary>
		/// <returns>true if the element is added to the <see cref="T:System.Collections.Generic.HashSet`1" /> object; false if the element is already present.</returns>
		/// <param name="item">The element to add to the set.</param>
		// Token: 0x060019D3 RID: 6611 RVA: 0x00054DBA File Offset: 0x00052FBA
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00054DC4 File Offset: 0x00052FC4
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			if (this._buckets != null)
			{
				int num = this.InternalIndexOf(equalValue);
				if (num >= 0)
				{
					actualValue = this._slots[num].value;
					return true;
				}
			}
			actualValue = default(T);
			return false;
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.HashSet`1" /> object to contain all elements that are present in itself, the specified collection, or both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019D5 RID: 6613 RVA: 0x00054E08 File Offset: 0x00053008
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T t in other)
			{
				this.AddIfNotPresent(t);
			}
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.HashSet`1" /> object to contain only elements that are present in that object and in the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019D6 RID: 6614 RVA: 0x00054E60 File Offset: 0x00053060
		public void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return;
			}
			if (other == this)
			{
				return;
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					this.Clear();
					return;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					this.IntersectWithHashSetWithSameEC(hashSet);
					return;
				}
			}
			this.IntersectWithEnumerable(other);
		}

		/// <summary>Removes all elements in the specified collection from the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <param name="other">The collection of items to remove from the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019D7 RID: 6615 RVA: 0x00054EC4 File Offset: 0x000530C4
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			foreach (T t in other)
			{
				this.Remove(t);
			}
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.HashSet`1" /> object to contain only elements that are present either in that object or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019D8 RID: 6616 RVA: 0x00054F30 File Offset: 0x00053130
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				this.SymmetricExceptWithUniqueHashSet(hashSet);
				return;
			}
			this.SymmetricExceptWithEnumerable(other);
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object is a subset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is a subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019D9 RID: 6617 RVA: 0x00054F88 File Offset: 0x00053188
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return true;
			}
			if (other == this)
			{
				return true;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				return this._count <= hashSet.Count && this.IsSubsetOfHashSetWithSameEC(hashSet);
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this._count && elementCount.unfoundCount >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object is a proper subset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is a proper subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019DA RID: 6618 RVA: 0x00055004 File Offset: 0x00053204
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other == this)
			{
				return false;
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return false;
				}
				if (this._count == 0)
				{
					return collection.Count > 0;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					return this._count < hashSet.Count && this.IsSubsetOfHashSetWithSameEC(hashSet);
				}
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this._count && elementCount.unfoundCount > 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object is a superset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is a superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019DB RID: 6619 RVA: 0x00055098 File Offset: 0x00053298
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other == this)
			{
				return true;
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return true;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet) && hashSet.Count > this._count)
				{
					return false;
				}
			}
			return this.ContainsAllElements(other);
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object is a proper superset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is a proper superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019DC RID: 6620 RVA: 0x000550F8 File Offset: 0x000532F8
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return false;
			}
			if (other == this)
			{
				return false;
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return true;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					return hashSet.Count < this._count && this.ContainsAllElements(hashSet);
				}
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.uniqueCount < this._count && elementCount.unfoundCount == 0;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.HashSet`1" /> object and a specified collection share common elements.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.HashSet`1" /> object and <paramref name="other" /> share at least one common element; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019DD RID: 6621 RVA: 0x00055184 File Offset: 0x00053384
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object and the specified collection contain the same elements.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060019DE RID: 6622 RVA: 0x000551F4 File Offset: 0x000533F4
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other == this)
			{
				return true;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				return this._count == hashSet.Count && this.ContainsAllElements(hashSet);
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null && this._count == 0 && collection.Count > 0)
			{
				return false;
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.uniqueCount == this._count && elementCount.unfoundCount == 0;
		}

		/// <summary>Copies the elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to an array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.HashSet`1" /> object. The array must have zero-based indexing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x060019DF RID: 6623 RVA: 0x0005527F File Offset: 0x0005347F
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this._count);
		}

		/// <summary>Copies the specified number of elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.HashSet`1" /> object. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy to <paramref name="array" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="arrayIndex" /> is greater than the length of the destination <paramref name="array" />.-or-<paramref name="count" /> is greater than the available space from the <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x060019E0 RID: 6624 RVA: 0x00055290 File Offset: 0x00053490
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Non negative number is required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Non negative number is required.");
			}
			if (arrayIndex > array.Length || count > array.Length - arrayIndex)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			int num = 0;
			int num2 = 0;
			while (num2 < this._lastIndex && num < count)
			{
				if (this._slots[num2].hashCode >= 0)
				{
					array[arrayIndex + num] = this._slots[num2].value;
					num++;
				}
				num2++;
			}
		}

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="T:System.Collections.Generic.HashSet`1" /> collection.</summary>
		/// <returns>The number of elements that were removed from the <see cref="T:System.Collections.Generic.HashSet`1" /> collection.</returns>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the elements to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x060019E1 RID: 6625 RVA: 0x00055344 File Offset: 0x00053544
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			for (int i = 0; i < this._lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0)
				{
					T value = this._slots[i].value;
					if (match(value) && this.Remove(value))
					{
						num++;
					}
				}
			}
			return num;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> object that is used to determine equality for the values in the set.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> object that is used to determine equality for the values in the set.</returns>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x000553AF File Offset: 0x000535AF
		public IEqualityComparer<T> Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		/// <summary>Sets the capacity of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to the actual number of elements it contains, rounded up to a nearby, implementation-specific value.</summary>
		// Token: 0x060019E3 RID: 6627 RVA: 0x000553B8 File Offset: 0x000535B8
		public void TrimExcess()
		{
			if (this._count == 0)
			{
				this._buckets = null;
				this._slots = null;
				this._version++;
				return;
			}
			int prime = HashHelpers.GetPrime(this._count);
			HashSet<T>.Slot[] array = new HashSet<T>.Slot[prime];
			int[] array2 = new int[prime];
			int num = 0;
			for (int i = 0; i < this._lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0)
				{
					array[num] = this._slots[i];
					int num2 = array[num].hashCode % prime;
					array[num].next = array2[num2] - 1;
					array2[num2] = num + 1;
					num++;
				}
			}
			this._lastIndex = num;
			this._slots = array;
			this._buckets = array2;
			this._freeList = -1;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object that can be used for equality testing of a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEqualityComparer" /> object that can be used for deep equality testing of the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</returns>
		// Token: 0x060019E4 RID: 6628 RVA: 0x0005548D File Offset: 0x0005368D
		public static IEqualityComparer<HashSet<T>> CreateSetComparer()
		{
			return new HashSetEqualityComparer<T>();
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00055494 File Offset: 0x00053694
		private void Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this._buckets = new int[prime];
			this._slots = new HashSet<T>.Slot[prime];
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000554C0 File Offset: 0x000536C0
		private void IncreaseCapacity()
		{
			int num = HashHelpers.ExpandPrime(this._count);
			if (num <= this._count)
			{
				throw new ArgumentException("HashSet capacity is too big.");
			}
			this.SetCapacity(num);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000554F4 File Offset: 0x000536F4
		private void SetCapacity(int newSize)
		{
			HashSet<T>.Slot[] array = new HashSet<T>.Slot[newSize];
			if (this._slots != null)
			{
				Array.Copy(this._slots, 0, array, 0, this._lastIndex);
			}
			int[] array2 = new int[newSize];
			for (int i = 0; i < this._lastIndex; i++)
			{
				int num = array[i].hashCode % newSize;
				array[i].next = array2[num] - 1;
				array2[num] = i + 1;
			}
			this._slots = array;
			this._buckets = array2;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00055570 File Offset: 0x00053770
		private bool AddIfNotPresent(T value)
		{
			if (this._buckets == null)
			{
				this.Initialize(0);
			}
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			for (int i = this._buckets[num2] - 1; i >= 0; i = this._slots[i].next)
			{
				if (this._slots[i].hashCode == num && this._comparer.Equals(this._slots[i].value, value))
				{
					return false;
				}
			}
			int num3;
			if (this._freeList >= 0)
			{
				num3 = this._freeList;
				this._freeList = this._slots[num3].next;
			}
			else
			{
				if (this._lastIndex == this._slots.Length)
				{
					this.IncreaseCapacity();
					num2 = num % this._buckets.Length;
				}
				num3 = this._lastIndex;
				this._lastIndex++;
			}
			this._slots[num3].hashCode = num;
			this._slots[num3].value = value;
			this._slots[num3].next = this._buckets[num2] - 1;
			this._buckets[num2] = num3 + 1;
			this._count++;
			this._version++;
			return true;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000556C0 File Offset: 0x000538C0
		private void AddValue(int index, int hashCode, T value)
		{
			int num = hashCode % this._buckets.Length;
			this._slots[index].hashCode = hashCode;
			this._slots[index].value = value;
			this._slots[index].next = this._buckets[num] - 1;
			this._buckets[num] = index + 1;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00055724 File Offset: 0x00053924
		private bool ContainsAllElements(IEnumerable<T> other)
		{
			foreach (T t in other)
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00055778 File Offset: 0x00053978
		private bool IsSubsetOfHashSetWithSameEC(HashSet<T> other)
		{
			foreach (T t in this)
			{
				if (!other.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000557D0 File Offset: 0x000539D0
		private void IntersectWithHashSetWithSameEC(HashSet<T> other)
		{
			for (int i = 0; i < this._lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0)
				{
					T value = this._slots[i].value;
					if (!other.Contains(value))
					{
						this.Remove(value);
					}
				}
			}
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00055828 File Offset: 0x00053A28
		private unsafe void IntersectWithEnumerable(IEnumerable<T> other)
		{
			int lastIndex = this._lastIndex;
			int num = BitHelper.ToIntArrayLength(lastIndex);
			BitHelper bitHelper;
			checked
			{
				if (num <= 100)
				{
					bitHelper = new BitHelper(stackalloc int[unchecked((UIntPtr)num) * 4], num);
				}
				else
				{
					bitHelper = new BitHelper(new int[num], num);
				}
				foreach (T t in other)
				{
					int num2 = this.InternalIndexOf(t);
					if (num2 >= 0)
					{
						bitHelper.MarkBit(num2);
					}
				}
			}
			for (int i = 0; i < lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0 && !bitHelper.IsMarked(i))
				{
					this.Remove(this._slots[i].value);
				}
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000558F8 File Offset: 0x00053AF8
		private int InternalIndexOf(T item)
		{
			int num = this.InternalGetHashCode(item);
			for (int i = this._buckets[num % this._buckets.Length] - 1; i >= 0; i = this._slots[i].next)
			{
				if (this._slots[i].hashCode == num && this._comparer.Equals(this._slots[i].value, item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00055970 File Offset: 0x00053B70
		private void SymmetricExceptWithUniqueHashSet(HashSet<T> other)
		{
			foreach (T t in other)
			{
				if (!this.Remove(t))
				{
					this.AddIfNotPresent(t);
				}
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000559C8 File Offset: 0x00053BC8
		private unsafe void SymmetricExceptWithEnumerable(IEnumerable<T> other)
		{
			int lastIndex = this._lastIndex;
			int num = BitHelper.ToIntArrayLength(lastIndex);
			BitHelper bitHelper;
			checked
			{
				BitHelper bitHelper2;
				if (num <= 50)
				{
					bitHelper = new BitHelper(stackalloc int[unchecked((UIntPtr)num) * 4], num);
					bitHelper2 = new BitHelper(stackalloc int[unchecked((UIntPtr)num) * 4], num);
				}
				else
				{
					bitHelper = new BitHelper(new int[num], num);
					bitHelper2 = new BitHelper(new int[num], num);
				}
				foreach (T t in other)
				{
					int num2 = 0;
					if (this.AddOrGetLocation(t, out num2))
					{
						bitHelper2.MarkBit(num2);
					}
					else if (num2 < lastIndex && !bitHelper2.IsMarked(num2))
					{
						bitHelper.MarkBit(num2);
					}
				}
			}
			for (int i = 0; i < lastIndex; i++)
			{
				if (bitHelper.IsMarked(i))
				{
					this.Remove(this._slots[i].value);
				}
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00055ABC File Offset: 0x00053CBC
		private bool AddOrGetLocation(T value, out int location)
		{
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			for (int i = this._buckets[num2] - 1; i >= 0; i = this._slots[i].next)
			{
				if (this._slots[i].hashCode == num && this._comparer.Equals(this._slots[i].value, value))
				{
					location = i;
					return false;
				}
			}
			int num3;
			if (this._freeList >= 0)
			{
				num3 = this._freeList;
				this._freeList = this._slots[num3].next;
			}
			else
			{
				if (this._lastIndex == this._slots.Length)
				{
					this.IncreaseCapacity();
					num2 = num % this._buckets.Length;
				}
				num3 = this._lastIndex;
				this._lastIndex++;
			}
			this._slots[num3].hashCode = num;
			this._slots[num3].value = value;
			this._slots[num3].next = this._buckets[num2] - 1;
			this._buckets[num2] = num3 + 1;
			this._count++;
			this._version++;
			location = num3;
			return true;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00055C00 File Offset: 0x00053E00
		private unsafe HashSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			HashSet<T>.ElementCount elementCount;
			if (this._count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						num++;
					}
				}
				elementCount.uniqueCount = 0;
				elementCount.unfoundCount = num;
				return elementCount;
			}
			int num2 = BitHelper.ToIntArrayLength(this._lastIndex);
			BitHelper bitHelper;
			int num3;
			int num4;
			checked
			{
				if (num2 <= 100)
				{
					bitHelper = new BitHelper(stackalloc int[unchecked((UIntPtr)num2) * 4], num2);
				}
				else
				{
					bitHelper = new BitHelper(new int[num2], num2);
				}
				num3 = 0;
				num4 = 0;
			}
			foreach (T t2 in other)
			{
				int num5 = this.InternalIndexOf(t2);
				if (num5 >= 0)
				{
					if (!bitHelper.IsMarked(num5))
					{
						bitHelper.MarkBit(num5);
						num4++;
					}
				}
				else
				{
					num3++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			elementCount.uniqueCount = num4;
			elementCount.unfoundCount = num3;
			return elementCount;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00055D18 File Offset: 0x00053F18
		internal static bool HashSetEquals(HashSet<T> set1, HashSet<T> set2, IEqualityComparer<T> comparer)
		{
			if (set1 == null)
			{
				return set2 == null;
			}
			if (set2 == null)
			{
				return false;
			}
			if (!HashSet<T>.AreEqualityComparersEqual(set1, set2))
			{
				foreach (T t in set2)
				{
					bool flag = false;
					foreach (T t2 in set1)
					{
						if (comparer.Equals(t, t2))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				return true;
			}
			if (set1.Count != set2.Count)
			{
				return false;
			}
			foreach (T t3 in set2)
			{
				if (!set1.Contains(t3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00055E24 File Offset: 0x00054024
		private static bool AreEqualityComparersEqual(HashSet<T> set1, HashSet<T> set2)
		{
			return set1.Comparer.Equals(set2.Comparer);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00055E37 File Offset: 0x00054037
		private int InternalGetHashCode(T item)
		{
			if (item == null)
			{
				return 0;
			}
			return this._comparer.GetHashCode(item) & int.MaxValue;
		}

		// Token: 0x04000B73 RID: 2931
		private const int Lower31BitMask = 2147483647;

		// Token: 0x04000B74 RID: 2932
		private const int StackAllocThreshold = 100;

		// Token: 0x04000B75 RID: 2933
		private const int ShrinkThreshold = 3;

		// Token: 0x04000B76 RID: 2934
		private const string CapacityName = "Capacity";

		// Token: 0x04000B77 RID: 2935
		private const string ElementsName = "Elements";

		// Token: 0x04000B78 RID: 2936
		private const string ComparerName = "Comparer";

		// Token: 0x04000B79 RID: 2937
		private const string VersionName = "Version";

		// Token: 0x04000B7A RID: 2938
		private int[] _buckets;

		// Token: 0x04000B7B RID: 2939
		private HashSet<T>.Slot[] _slots;

		// Token: 0x04000B7C RID: 2940
		private int _count;

		// Token: 0x04000B7D RID: 2941
		private int _lastIndex;

		// Token: 0x04000B7E RID: 2942
		private int _freeList;

		// Token: 0x04000B7F RID: 2943
		private IEqualityComparer<T> _comparer;

		// Token: 0x04000B80 RID: 2944
		private int _version;

		// Token: 0x04000B81 RID: 2945
		private SerializationInfo _siInfo;

		// Token: 0x02000351 RID: 849
		internal struct ElementCount
		{
			// Token: 0x04000B82 RID: 2946
			internal int uniqueCount;

			// Token: 0x04000B83 RID: 2947
			internal int unfoundCount;
		}

		// Token: 0x02000352 RID: 850
		internal struct Slot
		{
			// Token: 0x04000B84 RID: 2948
			internal int hashCode;

			// Token: 0x04000B85 RID: 2949
			internal int next;

			// Token: 0x04000B86 RID: 2950
			internal T value;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x02000353 RID: 851
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060019F6 RID: 6646 RVA: 0x00055E55 File Offset: 0x00054055
			internal Enumerator(HashSet<T> set)
			{
				this._set = set;
				this._index = 0;
				this._version = set._version;
				this._current = default(T);
			}

			/// <summary>Releases all resources used by a <see cref="T:System.Collections.Generic.HashSet`1.Enumerator" /> object.</summary>
			// Token: 0x060019F7 RID: 6647 RVA: 0x00003C4C File Offset: 0x00001E4C
			public void Dispose()
			{
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.HashSet`1" /> collection.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060019F8 RID: 6648 RVA: 0x00055E80 File Offset: 0x00054080
			public bool MoveNext()
			{
				if (this._version != this._set._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				while (this._index < this._set._lastIndex)
				{
					if (this._set._slots[this._index].hashCode >= 0)
					{
						this._current = this._set._slots[this._index].value;
						this._index++;
						return true;
					}
					this._index++;
				}
				this._index = this._set._lastIndex + 1;
				this._current = default(T);
				return false;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.HashSet`1" /> collection at the current position of the enumerator.</returns>
			// Token: 0x17000491 RID: 1169
			// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00055F3B File Offset: 0x0005413B
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator, as an <see cref="T:System.Object" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000492 RID: 1170
			// (get) Token: 0x060019FA RID: 6650 RVA: 0x00055F43 File Offset: 0x00054143
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._set._lastIndex + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060019FB RID: 6651 RVA: 0x00055F78 File Offset: 0x00054178
			void IEnumerator.Reset()
			{
				if (this._version != this._set._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04000B87 RID: 2951
			private HashSet<T> _set;

			// Token: 0x04000B88 RID: 2952
			private int _index;

			// Token: 0x04000B89 RID: 2953
			private int _version;

			// Token: 0x04000B8A RID: 2954
			private T _current;
		}
	}
}

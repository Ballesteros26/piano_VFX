using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</summary>
	// Token: 0x020002C2 RID: 706
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class PropertyDescriptorCollection : ICollection, IEnumerable, IList, IDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection. </param>
		// Token: 0x0600162D RID: 5677 RVA: 0x00057125 File Offset: 0x00055325
		public PropertyDescriptorCollection(PropertyDescriptor[] properties)
		{
			this.properties = properties;
			if (properties == null)
			{
				this.properties = new PropertyDescriptor[0];
				this.propCount = 0;
			}
			else
			{
				this.propCount = properties.Length;
			}
			this.propsOwned = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class, which is optionally read-only.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection.</param>
		/// <param name="readOnly">If true, specifies that the collection cannot be modified.</param>
		// Token: 0x0600162E RID: 5678 RVA: 0x00057163 File Offset: 0x00055363
		public PropertyDescriptorCollection(PropertyDescriptor[] properties, bool readOnly)
			: this(properties)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00057174 File Offset: 0x00055374
		private PropertyDescriptorCollection(PropertyDescriptor[] properties, int propCount, string[] namedSort, IComparer comparer)
		{
			this.propsOwned = false;
			if (namedSort != null)
			{
				this.namedSort = (string[])namedSort.Clone();
			}
			this.comparer = comparer;
			this.properties = properties;
			this.propCount = propCount;
			this.needSort = true;
		}

		/// <summary>Gets the number of property descriptors in the collection.</summary>
		/// <returns>The number of property descriptors in the collection.</returns>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x000571C6 File Offset: 0x000553C6
		public int Count
		{
			get
			{
				return this.propCount;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index number.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified index number.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get or set. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is not a valid index for <see cref="P:System.ComponentModel.PropertyDescriptorCollection.Item(System.Int32)" />. </exception>
		// Token: 0x170004AB RID: 1195
		public virtual PropertyDescriptor this[int index]
		{
			get
			{
				if (index >= this.propCount)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsurePropsOwned();
				return this.properties[index];
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or null if the property does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get from the collection. </param>
		// Token: 0x170004AC RID: 1196
		public virtual PropertyDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection.</summary>
		/// <returns>The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added to the collection.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001633 RID: 5683 RVA: 0x000571F8 File Offset: 0x000553F8
		public int Add(PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.propCount + 1);
			PropertyDescriptor[] array = this.properties;
			int num = this.propCount;
			this.propCount = num + 1;
			array[num] = value;
			return this.propCount - 1;
		}

		/// <summary>Removes all <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001634 RID: 5684 RVA: 0x00057242 File Offset: 0x00055442
		public void Clear()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.propCount = 0;
			this.cachedFoundProperties = null;
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>true if the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to find in the collection. </param>
		// Token: 0x06001635 RID: 5685 RVA: 0x00057260 File Offset: 0x00055460
		public bool Contains(PropertyDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		/// <summary>Copies the entire collection to an array, starting at the specified index number.</summary>
		/// <param name="array">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to copy elements of the collection to. </param>
		/// <param name="index">The index of the <paramref name="array" /> parameter at which copying begins. </param>
		// Token: 0x06001636 RID: 5686 RVA: 0x0005726F File Offset: 0x0005546F
		public void CopyTo(Array array, int index)
		{
			this.EnsurePropsOwned();
			Array.Copy(this.properties, 0, array, index, this.Count);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0005728C File Offset: 0x0005548C
		private void EnsurePropsOwned()
		{
			if (!this.propsOwned)
			{
				this.propsOwned = true;
				if (this.properties != null)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[this.Count];
					Array.Copy(this.properties, 0, array, 0, this.Count);
					this.properties = array;
				}
			}
			if (this.needSort)
			{
				this.needSort = false;
				this.InternalSort(this.namedSort);
			}
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000572F4 File Offset: 0x000554F4
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this.properties.Length)
			{
				return;
			}
			if (this.properties == null || this.properties.Length == 0)
			{
				this.propCount = 0;
				this.properties = new PropertyDescriptor[sizeNeeded];
				return;
			}
			this.EnsurePropsOwned();
			PropertyDescriptor[] array = new PropertyDescriptor[Math.Max(sizeNeeded, this.properties.Length * 2)];
			Array.Copy(this.properties, 0, array, 0, this.propCount);
			this.properties = array;
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, using a Boolean to indicate whether to ignore case.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or null if the property does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return from the collection. </param>
		/// <param name="ignoreCase">true if you want to ignore the case of the property name; otherwise, false. </param>
		// Token: 0x06001639 RID: 5689 RVA: 0x0005736C File Offset: 0x0005556C
		public virtual PropertyDescriptor Find(string name, bool ignoreCase)
		{
			PropertyDescriptor propertyDescriptor2;
			lock (this)
			{
				PropertyDescriptor propertyDescriptor = null;
				if (this.cachedFoundProperties == null || this.cachedIgnoreCase != ignoreCase)
				{
					this.cachedIgnoreCase = ignoreCase;
					this.cachedFoundProperties = new HybridDictionary(ignoreCase);
				}
				object obj = this.cachedFoundProperties[name];
				if (obj != null)
				{
					propertyDescriptor2 = (PropertyDescriptor)obj;
				}
				else
				{
					for (int i = 0; i < this.propCount; i++)
					{
						if (ignoreCase)
						{
							if (string.Equals(this.properties[i].Name, name, StringComparison.OrdinalIgnoreCase))
							{
								this.cachedFoundProperties[name] = this.properties[i];
								propertyDescriptor = this.properties[i];
								break;
							}
						}
						else if (this.properties[i].Name.Equals(name))
						{
							this.cachedFoundProperties[name] = this.properties[i];
							propertyDescriptor = this.properties[i];
							break;
						}
					}
					propertyDescriptor2 = propertyDescriptor;
				}
			}
			return propertyDescriptor2;
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return the index of. </param>
		// Token: 0x0600163A RID: 5690 RVA: 0x00057474 File Offset: 0x00055674
		public int IndexOf(PropertyDescriptor value)
		{
			return Array.IndexOf<PropertyDescriptor>(this.properties, value, 0, this.propCount);
		}

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection at the specified index number.</summary>
		/// <param name="index">The index at which to add the <paramref name="value" /> parameter to the collection. </param>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600163B RID: 5691 RVA: 0x0005748C File Offset: 0x0005568C
		public void Insert(int index, PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.propCount + 1);
			if (index < this.propCount)
			{
				Array.Copy(this.properties, index, this.properties, index + 1, this.propCount - index);
			}
			this.properties[index] = value;
			this.propCount++;
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600163C RID: 5692 RVA: 0x000574F4 File Offset: 0x000556F4
		public void Remove(PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			int num = this.IndexOf(value);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600163D RID: 5693 RVA: 0x00057524 File Offset: 0x00055724
		public void RemoveAt(int index)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.propCount - 1)
			{
				Array.Copy(this.properties, index + 1, this.properties, index, this.propCount - index - 1);
			}
			this.properties[this.propCount - 1] = null;
			this.propCount--;
		}

		/// <summary>Sorts the members of this collection, using the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x0600163E RID: 5694 RVA: 0x00057587 File Offset: 0x00055787
		public virtual PropertyDescriptorCollection Sort()
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, this.namedSort, this.comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x0600163F RID: 5695 RVA: 0x000575A6 File Offset: 0x000557A6
		public virtual PropertyDescriptorCollection Sort(string[] names)
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, names, this.comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the sort using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		/// <param name="comparer">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06001640 RID: 5696 RVA: 0x000575C0 File Offset: 0x000557C0
		public virtual PropertyDescriptorCollection Sort(string[] names, IComparer comparer)
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, names, comparer);
		}

		/// <summary>Sorts the members of this collection, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		/// <param name="comparer">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06001641 RID: 5697 RVA: 0x000575D5 File Offset: 0x000557D5
		public virtual PropertyDescriptorCollection Sort(IComparer comparer)
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, this.namedSort, comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06001642 RID: 5698 RVA: 0x000575F0 File Offset: 0x000557F0
		protected void InternalSort(string[] names)
		{
			if (this.properties == null || this.properties.Length == 0)
			{
				return;
			}
			this.InternalSort(this.comparer);
			if (names != null && names.Length != 0)
			{
				ArrayList arrayList = new ArrayList(this.properties);
				int num = 0;
				int num2 = this.properties.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)arrayList[j];
						if (propertyDescriptor != null && propertyDescriptor.Name.Equals(names[i]))
						{
							this.properties[num++] = propertyDescriptor;
							arrayList[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (arrayList[k] != null)
					{
						this.properties[num++] = (PropertyDescriptor)arrayList[k];
					}
				}
			}
		}

		/// <summary>Sorts the members of this collection, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06001643 RID: 5699 RVA: 0x000576CD File Offset: 0x000558CD
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this.properties, sorter);
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06001644 RID: 5700 RVA: 0x000576E8 File Offset: 0x000558E8
		public virtual IEnumerator GetEnumerator()
		{
			this.EnsurePropsOwned();
			if (this.properties.Length != this.propCount)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[this.propCount];
				Array.Copy(this.properties, 0, array, 0, this.propCount);
				return array.GetEnumerator();
			}
			return this.properties.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0005773D File Offset: 0x0005593D
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x00009E57 File Offset: 0x00008057
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001648 RID: 5704 RVA: 0x00057748 File Offset: 0x00055948
		void IDictionary.Add(object key, object value)
		{
			PropertyDescriptor propertyDescriptor = value as PropertyDescriptor;
			if (propertyDescriptor == null)
			{
				throw new ArgumentException("value");
			}
			this.Add(propertyDescriptor);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" />. </summary>
		// Token: 0x06001649 RID: 5705 RVA: 0x00057772 File Offset: 0x00055972
		void IDictionary.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		// Token: 0x0600164A RID: 5706 RVA: 0x0005777A File Offset: 0x0005597A
		bool IDictionary.Contains(object key)
		{
			return key is string && this[(string)key] != null;
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x0600164B RID: 5707 RVA: 0x00057795 File Offset: 0x00055995
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new PropertyDescriptorCollection.PropertyDescriptorEnumerator(this);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0005779D File Offset: 0x0005599D
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.</returns>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0005779D File Offset: 0x0005599D
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets or sets the element with the specified key. </summary>
		/// <returns>The element with the specified key.</returns>
		/// <param name="key">The key of the element to get or set. </param>
		// Token: 0x170004B2 RID: 1202
		object IDictionary.this[object key]
		{
			get
			{
				if (key is string)
				{
					return this[(string)key];
				}
				return null;
			}
			set
			{
				if (this.readOnly)
				{
					throw new NotSupportedException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				int num = -1;
				if (key is int)
				{
					num = (int)key;
					if (num < 0 || num >= this.propCount)
					{
						throw new IndexOutOfRangeException();
					}
				}
				else
				{
					if (!(key is string))
					{
						throw new ArgumentException("key");
					}
					for (int i = 0; i < this.propCount; i++)
					{
						if (this.properties[i].Name.Equals((string)key))
						{
							num = i;
							break;
						}
					}
				}
				if (num == -1)
				{
					this.Add((PropertyDescriptor)value);
					return;
				}
				this.EnsurePropsOwned();
				this.properties[num] = (PropertyDescriptor)value;
				if (this.cachedFoundProperties != null && key is string)
				{
					this.cachedFoundProperties[key] = value;
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0005789C File Offset: 0x00055A9C
		ICollection IDictionary.Keys
		{
			get
			{
				string[] array = new string[this.propCount];
				for (int i = 0; i < this.propCount; i++)
				{
					array[i] = this.properties[i].Name;
				}
				return array;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000578D8 File Offset: 0x00055AD8
		ICollection IDictionary.Values
		{
			get
			{
				if (this.properties.Length != this.propCount)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[this.propCount];
					Array.Copy(this.properties, 0, array, 0, this.propCount);
					return array;
				}
				return (ICollection)this.properties.Clone();
			}
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />. </summary>
		/// <param name="key">The key of the element to remove.</param>
		// Token: 0x06001652 RID: 5714 RVA: 0x00057928 File Offset: 0x00055B28
		void IDictionary.Remove(object key)
		{
			if (key is string)
			{
				PropertyDescriptor propertyDescriptor = this[(string)key];
				if (propertyDescriptor != null)
				{
					((IList)this).Remove(propertyDescriptor);
				}
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06001653 RID: 5715 RVA: 0x00057954 File Offset: 0x00055B54
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The item to add to the collection.</param>
		// Token: 0x06001654 RID: 5716 RVA: 0x0005795C File Offset: 0x00055B5C
		int IList.Add(object value)
		{
			return this.Add((PropertyDescriptor)value);
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001655 RID: 5717 RVA: 0x00057772 File Offset: 0x00055972
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <returns>true if the item is found in the collection; otherwise, false.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06001656 RID: 5718 RVA: 0x0005796A File Offset: 0x00055B6A
		bool IList.Contains(object value)
		{
			return this.Contains((PropertyDescriptor)value);
		}

		/// <summary>Determines the index of a specified item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list, otherwise -1.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06001657 RID: 5719 RVA: 0x00057978 File Offset: 0x00055B78
		int IList.IndexOf(object value)
		{
			return this.IndexOf((PropertyDescriptor)value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001658 RID: 5720 RVA: 0x00057986 File Offset: 0x00055B86
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (PropertyDescriptor)value);
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0005779D File Offset: 0x0005599D
		bool IList.IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0005779D File Offset: 0x0005599D
		bool IList.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Removes the first occurrence of a specified value from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600165B RID: 5723 RVA: 0x00057995 File Offset: 0x00055B95
		void IList.Remove(object value)
		{
			this.Remove((PropertyDescriptor)value);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600165C RID: 5724 RVA: 0x000579A3 File Offset: 0x00055BA3
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets or sets an item from the collection at a specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the item to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.ComponentModel.PropertyDescriptor" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0. -or-<paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x170004B7 RID: 1207
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (this.readOnly)
				{
					throw new NotSupportedException();
				}
				if (index >= this.propCount)
				{
					throw new IndexOutOfRangeException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				this.EnsurePropsOwned();
				this.properties[index] = (PropertyDescriptor)value;
			}
		}

		/// <summary>Specifies an empty collection that you can use instead of creating a new one with no items. This static field is read-only.</summary>
		// Token: 0x0400138D RID: 5005
		public static readonly PropertyDescriptorCollection Empty = new PropertyDescriptorCollection(null, true);

		// Token: 0x0400138E RID: 5006
		private IDictionary cachedFoundProperties;

		// Token: 0x0400138F RID: 5007
		private bool cachedIgnoreCase;

		// Token: 0x04001390 RID: 5008
		private PropertyDescriptor[] properties;

		// Token: 0x04001391 RID: 5009
		private int propCount;

		// Token: 0x04001392 RID: 5010
		private string[] namedSort;

		// Token: 0x04001393 RID: 5011
		private IComparer comparer;

		// Token: 0x04001394 RID: 5012
		private bool propsOwned = true;

		// Token: 0x04001395 RID: 5013
		private bool needSort;

		// Token: 0x04001396 RID: 5014
		private bool readOnly;

		// Token: 0x020002C3 RID: 707
		private class PropertyDescriptorEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06001660 RID: 5728 RVA: 0x00057A1A File Offset: 0x00055C1A
			public PropertyDescriptorEnumerator(PropertyDescriptorCollection owner)
			{
				this.owner = owner;
			}

			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x06001661 RID: 5729 RVA: 0x00057A30 File Offset: 0x00055C30
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x170004B9 RID: 1209
			// (get) Token: 0x06001662 RID: 5730 RVA: 0x00057A40 File Offset: 0x00055C40
			public DictionaryEntry Entry
			{
				get
				{
					PropertyDescriptor propertyDescriptor = this.owner[this.index];
					return new DictionaryEntry(propertyDescriptor.Name, propertyDescriptor);
				}
			}

			// Token: 0x170004BA RID: 1210
			// (get) Token: 0x06001663 RID: 5731 RVA: 0x00057A6B File Offset: 0x00055C6B
			public object Key
			{
				get
				{
					return this.owner[this.index].Name;
				}
			}

			// Token: 0x170004BB RID: 1211
			// (get) Token: 0x06001664 RID: 5732 RVA: 0x00057A6B File Offset: 0x00055C6B
			public object Value
			{
				get
				{
					return this.owner[this.index].Name;
				}
			}

			// Token: 0x06001665 RID: 5733 RVA: 0x00057A83 File Offset: 0x00055C83
			public bool MoveNext()
			{
				if (this.index < this.owner.Count - 1)
				{
					this.index++;
					return true;
				}
				return false;
			}

			// Token: 0x06001666 RID: 5734 RVA: 0x00057AAB File Offset: 0x00055CAB
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04001397 RID: 5015
			private PropertyDescriptorCollection owner;

			// Token: 0x04001398 RID: 5016
			private int index = -1;
		}
	}
}

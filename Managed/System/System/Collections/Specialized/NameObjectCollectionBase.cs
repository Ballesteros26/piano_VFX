using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using Unity;

namespace System.Collections.Specialized
{
	/// <summary>Provides the abstract base class for a collection of associated <see cref="T:System.String" /> keys and <see cref="T:System.Object" /> values that can be accessed either with the key or with the index.</summary>
	// Token: 0x020006FE RID: 1790
	[Serializable]
	public abstract class NameObjectCollectionBase : ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty.</summary>
		// Token: 0x06003818 RID: 14360 RVA: 0x000CE497 File Offset: 0x000CC697
		protected NameObjectCollectionBase()
			: this(NameObjectCollectionBase.defaultComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		// Token: 0x06003819 RID: 14361 RVA: 0x000CE4A4 File Offset: 0x000CC6A4
		protected NameObjectCollectionBase(IEqualityComparer equalityComparer)
		{
			IEqualityComparer equalityComparer2;
			if (equalityComparer != null)
			{
				equalityComparer2 = equalityComparer;
			}
			else
			{
				IEqualityComparer equalityComparer3 = NameObjectCollectionBase.defaultComparer;
				equalityComparer2 = equalityComparer3;
			}
			this._keyComparer = equalityComparer2;
			this.Reset();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object can initially contain.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600381A RID: 14362 RVA: 0x000CE4D0 File Offset: 0x000CC6D0
		protected NameObjectCollectionBase(int capacity, IEqualityComparer equalityComparer)
			: this(equalityComparer)
		{
			this.Reset(capacity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the default initial capacity, and uses the specified hash code provider and the specified comparer.</summary>
		/// <param name="hashProvider">The <see cref="T:System.Collections.IHashCodeProvider" /> that will supply the hash codes for all keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.</param>
		// Token: 0x0600381B RID: 14363 RVA: 0x000CE4E0 File Offset: 0x000CC6E0
		[Obsolete("Please use NameObjectCollectionBase(IEqualityComparer) instead.")]
		protected NameObjectCollectionBase(IHashCodeProvider hashProvider, IComparer comparer)
		{
			this._keyComparer = new CompatibleComparer(comparer, hashProvider);
			this.Reset();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the specified initial capacity and uses the specified hash code provider and the specified comparer.</summary>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance can initially contain.</param>
		/// <param name="hashProvider">The <see cref="T:System.Collections.IHashCodeProvider" /> that will supply the hash codes for all keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600381C RID: 14364 RVA: 0x000CE4FB File Offset: 0x000CC6FB
		[Obsolete("Please use NameObjectCollectionBase(Int32, IEqualityComparer) instead.")]
		protected NameObjectCollectionBase(int capacity, IHashCodeProvider hashProvider, IComparer comparer)
		{
			this._keyComparer = new CompatibleComparer(comparer, hashProvider);
			this.Reset(capacity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the specified initial capacity, and uses the default hash code provider and the default comparer.</summary>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance can initially contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x0600381D RID: 14365 RVA: 0x000CE517 File Offset: 0x000CC717
		protected NameObjectCollectionBase(int capacity)
		{
			this._keyComparer = StringComparer.InvariantCultureIgnoreCase;
			this.Reset(capacity);
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000020EB File Offset: 0x000002EB
		internal NameObjectCollectionBase(DBNull dummy)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is serializable and uses the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		// Token: 0x0600381F RID: 14367 RVA: 0x000CE531 File Offset: 0x000CC731
		protected NameObjectCollectionBase(SerializationInfo info, StreamingContext context)
		{
			this._serializationInfo = info;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06003820 RID: 14368 RVA: 0x000CE540 File Offset: 0x000CC740
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ReadOnly", this._readOnly);
			if (this._keyComparer == NameObjectCollectionBase.defaultComparer)
			{
				info.AddValue("HashProvider", CompatibleComparer.DefaultHashCodeProvider, typeof(IHashCodeProvider));
				info.AddValue("Comparer", CompatibleComparer.DefaultComparer, typeof(IComparer));
			}
			else if (this._keyComparer == null)
			{
				info.AddValue("HashProvider", null, typeof(IHashCodeProvider));
				info.AddValue("Comparer", null, typeof(IComparer));
			}
			else if (this._keyComparer is CompatibleComparer)
			{
				CompatibleComparer compatibleComparer = (CompatibleComparer)this._keyComparer;
				info.AddValue("HashProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
			}
			else
			{
				info.AddValue("KeyComparer", this._keyComparer, typeof(IEqualityComparer));
			}
			int count = this._entriesArray.Count;
			info.AddValue("Count", count);
			string[] array = new string[count];
			object[] array2 = new object[count];
			for (int i = 0; i < count; i++)
			{
				NameObjectCollectionBase.NameObjectEntry nameObjectEntry = (NameObjectCollectionBase.NameObjectEntry)this._entriesArray[i];
				array[i] = nameObjectEntry.Key;
				array2[i] = nameObjectEntry.Value;
			}
			info.AddValue("Keys", array, typeof(string[]));
			info.AddValue("Values", array2, typeof(object[]));
			info.AddValue("Version", this._version);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is invalid.</exception>
		// Token: 0x06003821 RID: 14369 RVA: 0x000CE6F4 File Offset: 0x000CC8F4
		public virtual void OnDeserialization(object sender)
		{
			if (this._keyComparer != null)
			{
				return;
			}
			if (this._serializationInfo == null)
			{
				throw new SerializationException();
			}
			SerializationInfo serializationInfo = this._serializationInfo;
			this._serializationInfo = null;
			bool flag = false;
			int num = 0;
			string[] array = null;
			object[] array2 = null;
			IHashCodeProvider hashCodeProvider = null;
			IComparer comparer = null;
			bool flag2 = false;
			int num2 = 0;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num3 = global::<PrivateImplementationDetails>.ComputeStringHash(name);
				if (num3 <= 1573770551U)
				{
					if (num3 <= 1202781175U)
					{
						if (num3 != 891156946U)
						{
							if (num3 == 1202781175U)
							{
								if (name == "ReadOnly")
								{
									flag = serializationInfo.GetBoolean("ReadOnly");
								}
							}
						}
						else if (name == "Comparer")
						{
							comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
						}
					}
					else if (num3 != 1228509323U)
					{
						if (num3 == 1573770551U)
						{
							if (name == "Version")
							{
								flag2 = true;
								num2 = serializationInfo.GetInt32("Version");
							}
						}
					}
					else if (name == "KeyComparer")
					{
						this._keyComparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
					}
				}
				else if (num3 <= 1944240600U)
				{
					if (num3 != 1613443821U)
					{
						if (num3 == 1944240600U)
						{
							if (name == "HashProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Keys")
					{
						array = (string[])serializationInfo.GetValue("Keys", typeof(string[]));
					}
				}
				else if (num3 != 2370642523U)
				{
					if (num3 == 3790059668U)
					{
						if (name == "Count")
						{
							num = serializationInfo.GetInt32("Count");
						}
					}
				}
				else if (name == "Values")
				{
					array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
				}
			}
			if (this._keyComparer == null)
			{
				if (comparer == null || hashCodeProvider == null)
				{
					throw new SerializationException();
				}
				this._keyComparer = new CompatibleComparer(comparer, hashCodeProvider);
			}
			if (array == null || array2 == null)
			{
				throw new SerializationException();
			}
			this.Reset(num);
			for (int i = 0; i < num; i++)
			{
				this.BaseAdd(array[i], array2[i]);
			}
			this._readOnly = flag;
			if (flag2)
			{
				this._version = num2;
			}
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000CE9D2 File Offset: 0x000CCBD2
		private void Reset()
		{
			this._entriesArray = new ArrayList();
			this._entriesTable = new Hashtable(this._keyComparer);
			this._nullKeyEntry = null;
			this._version++;
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000CEA09 File Offset: 0x000CCC09
		private void Reset(int capacity)
		{
			this._entriesArray = new ArrayList(capacity);
			this._entriesTable = new Hashtable(capacity, this._keyComparer);
			this._nullKeyEntry = null;
			this._version++;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000CEA42 File Offset: 0x000CCC42
		private NameObjectCollectionBase.NameObjectEntry FindEntry(string key)
		{
			if (key != null)
			{
				return (NameObjectCollectionBase.NameObjectEntry)this._entriesTable[key];
			}
			return this._nullKeyEntry;
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06003825 RID: 14373 RVA: 0x000CEA63 File Offset: 0x000CCC63
		// (set) Token: 0x06003826 RID: 14374 RVA: 0x000CEA6B File Offset: 0x000CCC6B
		internal IEqualityComparer Comparer
		{
			get
			{
				return this._keyComparer;
			}
			set
			{
				this._keyComparer = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is read-only; otherwise, false.</returns>
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x000CEA74 File Offset: 0x000CCC74
		// (set) Token: 0x06003828 RID: 14376 RVA: 0x000CEA7C File Offset: 0x000CCC7C
		protected bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				this._readOnly = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance contains entries whose keys are not null.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance contains entries whose keys are not null; otherwise, false.</returns>
		// Token: 0x06003829 RID: 14377 RVA: 0x000CEA85 File Offset: 0x000CCC85
		protected bool BaseHasKeys()
		{
			return this._entriesTable.Count > 0;
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to add. The key can be null.</param>
		/// <param name="value">The <see cref="T:System.Object" /> value of the entry to add. The value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only. </exception>
		// Token: 0x0600382A RID: 14378 RVA: 0x000CEA98 File Offset: 0x000CCC98
		protected void BaseAdd(string name, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("Collection is read-only."));
			}
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = new NameObjectCollectionBase.NameObjectEntry(name, value);
			if (name != null)
			{
				if (this._entriesTable[name] == null)
				{
					this._entriesTable.Add(name, nameObjectEntry);
				}
			}
			else if (this._nullKeyEntry == null)
			{
				this._nullKeyEntry = nameObjectEntry;
			}
			this._entriesArray.Add(nameObjectEntry);
			this._version++;
		}

		/// <summary>Removes the entries with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entries to remove. The key can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only. </exception>
		// Token: 0x0600382B RID: 14379 RVA: 0x000CEB18 File Offset: 0x000CCD18
		protected void BaseRemove(string name)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("Collection is read-only."));
			}
			if (name != null)
			{
				this._entriesTable.Remove(name);
				for (int i = this._entriesArray.Count - 1; i >= 0; i--)
				{
					if (this._keyComparer.Equals(name, this.BaseGetKey(i)))
					{
						this._entriesArray.RemoveAt(i);
					}
				}
			}
			else
			{
				this._nullKeyEntry = null;
				for (int j = this._entriesArray.Count - 1; j >= 0; j--)
				{
					if (this.BaseGetKey(j) == null)
					{
						this._entriesArray.RemoveAt(j);
					}
				}
			}
			this._version++;
		}

		/// <summary>Removes the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index of the entry to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600382C RID: 14380 RVA: 0x000CEBD0 File Offset: 0x000CCDD0
		protected void BaseRemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("Collection is read-only."));
			}
			string text = this.BaseGetKey(index);
			if (text != null)
			{
				this._entriesTable.Remove(text);
			}
			else
			{
				this._nullKeyEntry = null;
			}
			this._entriesArray.RemoveAt(index);
			this._version++;
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600382D RID: 14381 RVA: 0x000CEC33 File Offset: 0x000CCE33
		protected void BaseClear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("Collection is read-only."));
			}
			this.Reset();
		}

		/// <summary>Gets the value of the first entry with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the value of the first entry with the specified key, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to get. The key can be null.</param>
		// Token: 0x0600382E RID: 14382 RVA: 0x000CEC54 File Offset: 0x000CCE54
		protected object BaseGet(string name)
		{
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = this.FindEntry(name);
			if (nameObjectEntry == null)
			{
				return null;
			}
			return nameObjectEntry.Value;
		}

		/// <summary>Sets the value of the first entry with the specified key in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance, if found; otherwise, adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to set. The key can be null.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the new value of the entry to set. The value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only. </exception>
		// Token: 0x0600382F RID: 14383 RVA: 0x000CEC74 File Offset: 0x000CCE74
		protected void BaseSet(string name, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("Collection is read-only."));
			}
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = this.FindEntry(name);
			if (nameObjectEntry != null)
			{
				nameObjectEntry.Value = value;
				this._version++;
				return;
			}
			this.BaseAdd(name, value);
		}

		/// <summary>Gets the value of the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the value of the entry at the specified index.</returns>
		/// <param name="index">The zero-based index of the value to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x06003830 RID: 14384 RVA: 0x000CECC2 File Offset: 0x000CCEC2
		protected object BaseGet(int index)
		{
			return ((NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index]).Value;
		}

		/// <summary>Gets the key of the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the key of the entry at the specified index.</returns>
		/// <param name="index">The zero-based index of the key to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x06003831 RID: 14385 RVA: 0x000CECDA File Offset: 0x000CCEDA
		protected string BaseGetKey(int index)
		{
			return ((NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index]).Key;
		}

		/// <summary>Sets the value of the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index of the entry to set.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the new value of the entry to set. The value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x06003832 RID: 14386 RVA: 0x000CECF2 File Offset: 0x000CCEF2
		protected void BaseSet(int index, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("Collection is read-only."));
			}
			((NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index]).Value = value;
			this._version++;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x06003833 RID: 14387 RVA: 0x000CED31 File Offset: 0x000CCF31
		public virtual IEnumerator GetEnumerator()
		{
			return new NameObjectCollectionBase.NameObjectKeysEnumerator(this);
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000CED39 File Offset: 0x000CCF39
		public virtual int Count
		{
			get
			{
				return this._entriesArray.Count;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003835 RID: 14389 RVA: 0x000CED48 File Offset: 0x000CCF48
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(global::SR.GetString("Multi dimension array is not supported on this operation."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", global::SR.GetString("Index {0} is out of range.", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
			}
			if (array.Length - index < this._entriesArray.Count)
			{
				throw new ArgumentException(global::SR.GetString("Insufficient space in the target location to copy the information."));
			}
			foreach (object obj in this)
			{
				array.SetValue(obj, index++);
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object.</returns>
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x000CEDF2 File Offset: 0x000CCFF2
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> array that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x06003838 RID: 14392 RVA: 0x000CEE14 File Offset: 0x000CD014
		protected string[] BaseGetAllKeys()
		{
			int count = this._entriesArray.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGetKey(i);
			}
			return array;
		}

		/// <summary>Returns an <see cref="T:System.Object" /> array that contains all the values in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Object" /> array that contains all the values in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x06003839 RID: 14393 RVA: 0x000CEE4C File Offset: 0x000CD04C
		protected object[] BaseGetAllValues()
		{
			int count = this._entriesArray.Count;
			object[] array = new object[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGet(i);
			}
			return array;
		}

		/// <summary>Returns an array of the specified type that contains all the values in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>An array of the specified type that contains all the values in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of array to return.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Type" />. </exception>
		// Token: 0x0600383A RID: 14394 RVA: 0x000CEE84 File Offset: 0x000CD084
		protected object[] BaseGetAllValues(Type type)
		{
			int count = this._entriesArray.Count;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object[] array = (object[])SecurityUtils.ArrayCreateInstance(type, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGet(i);
			}
			return array;
		}

		/// <summary>Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000CEED5 File Offset: 0x000CD0D5
		public virtual NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new NameObjectCollectionBase.KeysCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x04002C3B RID: 11323
		private const string ReadOnlyName = "ReadOnly";

		// Token: 0x04002C3C RID: 11324
		private const string CountName = "Count";

		// Token: 0x04002C3D RID: 11325
		private const string ComparerName = "Comparer";

		// Token: 0x04002C3E RID: 11326
		private const string HashCodeProviderName = "HashProvider";

		// Token: 0x04002C3F RID: 11327
		private const string KeysName = "Keys";

		// Token: 0x04002C40 RID: 11328
		private const string ValuesName = "Values";

		// Token: 0x04002C41 RID: 11329
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04002C42 RID: 11330
		private const string VersionName = "Version";

		// Token: 0x04002C43 RID: 11331
		private bool _readOnly;

		// Token: 0x04002C44 RID: 11332
		private ArrayList _entriesArray;

		// Token: 0x04002C45 RID: 11333
		private IEqualityComparer _keyComparer;

		// Token: 0x04002C46 RID: 11334
		private volatile Hashtable _entriesTable;

		// Token: 0x04002C47 RID: 11335
		private volatile NameObjectCollectionBase.NameObjectEntry _nullKeyEntry;

		// Token: 0x04002C48 RID: 11336
		private NameObjectCollectionBase.KeysCollection _keys;

		// Token: 0x04002C49 RID: 11337
		private SerializationInfo _serializationInfo;

		// Token: 0x04002C4A RID: 11338
		private int _version;

		// Token: 0x04002C4B RID: 11339
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002C4C RID: 11340
		private static StringComparer defaultComparer = StringComparer.InvariantCultureIgnoreCase;

		// Token: 0x020006FF RID: 1791
		internal class NameObjectEntry
		{
			// Token: 0x0600383D RID: 14397 RVA: 0x000CEEFD File Offset: 0x000CD0FD
			internal NameObjectEntry(string name, object value)
			{
				this.Key = name;
				this.Value = value;
			}

			// Token: 0x04002C4D RID: 11341
			internal string Key;

			// Token: 0x04002C4E RID: 11342
			internal object Value;
		}

		// Token: 0x02000700 RID: 1792
		[Serializable]
		internal class NameObjectKeysEnumerator : IEnumerator
		{
			// Token: 0x0600383E RID: 14398 RVA: 0x000CEF13 File Offset: 0x000CD113
			internal NameObjectKeysEnumerator(NameObjectCollectionBase coll)
			{
				this._coll = coll;
				this._version = this._coll._version;
				this._pos = -1;
			}

			// Token: 0x0600383F RID: 14399 RVA: 0x000CEF3C File Offset: 0x000CD13C
			public bool MoveNext()
			{
				if (this._version != this._coll._version)
				{
					throw new InvalidOperationException(global::SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				if (this._pos < this._coll.Count - 1)
				{
					this._pos++;
					return true;
				}
				this._pos = this._coll.Count;
				return false;
			}

			// Token: 0x06003840 RID: 14400 RVA: 0x000CEFA3 File Offset: 0x000CD1A3
			public void Reset()
			{
				if (this._version != this._coll._version)
				{
					throw new InvalidOperationException(global::SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				this._pos = -1;
			}

			// Token: 0x17000D94 RID: 3476
			// (get) Token: 0x06003841 RID: 14401 RVA: 0x000CEFCF File Offset: 0x000CD1CF
			public object Current
			{
				get
				{
					if (this._pos >= 0 && this._pos < this._coll.Count)
					{
						return this._coll.BaseGetKey(this._pos);
					}
					throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x04002C4F RID: 11343
			private int _pos;

			// Token: 0x04002C50 RID: 11344
			private NameObjectCollectionBase _coll;

			// Token: 0x04002C51 RID: 11345
			private int _version;
		}

		/// <summary>Represents a collection of the <see cref="T:System.String" /> keys of a collection.</summary>
		// Token: 0x02000701 RID: 1793
		[Serializable]
		public class KeysCollection : ICollection, IEnumerable
		{
			// Token: 0x06003842 RID: 14402 RVA: 0x000CF00E File Offset: 0x000CD20E
			internal KeysCollection(NameObjectCollectionBase coll)
			{
				this._coll = coll;
			}

			/// <summary>Gets the key at the specified index of the collection.</summary>
			/// <returns>A <see cref="T:System.String" /> that contains the key at the specified index of the collection.</returns>
			/// <param name="index">The zero-based index of the key to get from the collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
			// Token: 0x06003843 RID: 14403 RVA: 0x000CF01D File Offset: 0x000CD21D
			public virtual string Get(int index)
			{
				return this._coll.BaseGetKey(index);
			}

			/// <summary>Gets the entry at the specified index of the collection.</summary>
			/// <returns>The <see cref="T:System.String" /> key of the entry at the specified index of the collection.</returns>
			/// <param name="index">The zero-based index of the entry to locate in the collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
			// Token: 0x17000D95 RID: 3477
			public string this[int index]
			{
				get
				{
					return this.Get(index);
				}
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</returns>
			// Token: 0x06003845 RID: 14405 RVA: 0x000CF034 File Offset: 0x000CD234
			public IEnumerator GetEnumerator()
			{
				return new NameObjectCollectionBase.NameObjectKeysEnumerator(this._coll);
			}

			/// <summary>Gets the number of keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</summary>
			/// <returns>The number of keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</returns>
			// Token: 0x17000D96 RID: 3478
			// (get) Token: 0x06003846 RID: 14406 RVA: 0x000CF041 File Offset: 0x000CD241
			public int Count
			{
				get
				{
					return this._coll.Count;
				}
			}

			/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero. </exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
			/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
			// Token: 0x06003847 RID: 14407 RVA: 0x000CF050 File Offset: 0x000CD250
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(global::SR.GetString("Multi dimension array is not supported on this operation."));
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", global::SR.GetString("Index {0} is out of range.", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
				}
				if (array.Length - index < this._coll.Count)
				{
					throw new ArgumentException(global::SR.GetString("Insufficient space in the target location to copy the information."));
				}
				foreach (object obj in this)
				{
					array.SetValue(obj, index++);
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</returns>
			// Token: 0x17000D97 RID: 3479
			// (get) Token: 0x06003848 RID: 14408 RVA: 0x000CF0FA File Offset: 0x000CD2FA
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._coll).SyncRoot;
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
			// Token: 0x17000D98 RID: 3480
			// (get) Token: 0x06003849 RID: 14409 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600384A RID: 14410 RVA: 0x0000F0CE File Offset: 0x0000D2CE
			internal KeysCollection()
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x04002C52 RID: 11346
			private NameObjectCollectionBase _coll;
		}
	}
}

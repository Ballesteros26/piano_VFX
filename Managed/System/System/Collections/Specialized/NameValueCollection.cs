using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of associated <see cref="T:System.String" /> keys and <see cref="T:System.String" /> values that can be accessed either with the key or with the index.</summary>
	// Token: 0x02000703 RID: 1795
	[Serializable]
	public class NameValueCollection : NameObjectCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the default initial capacity and uses the default case-insensitive hash code provider and the default case-insensitive comparer.</summary>
		// Token: 0x06003852 RID: 14418 RVA: 0x000CF211 File Offset: 0x000CD411
		public NameValueCollection()
		{
		}

		/// <summary>Copies the entries from the specified <see cref="T:System.Collections.Specialized.NameValueCollection" /> to a new <see cref="T:System.Collections.Specialized.NameValueCollection" /> with the same initial capacity as the number of entries copied and using the same hash code provider and the same comparer as the source collection.</summary>
		/// <param name="col">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to copy to the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is null.</exception>
		// Token: 0x06003853 RID: 14419 RVA: 0x000CF219 File Offset: 0x000CD419
		public NameValueCollection(NameValueCollection col)
			: base((col != null) ? col.Comparer : null)
		{
			this.Add(col);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the default initial capacity and uses the specified hash code provider and the specified comparer.</summary>
		/// <param name="hashProvider">The <see cref="T:System.Collections.IHashCodeProvider" /> that will supply the hash codes for all keys in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.</param>
		// Token: 0x06003854 RID: 14420 RVA: 0x000CF234 File Offset: 0x000CD434
		[Obsolete("Please use NameValueCollection(IEqualityComparer) instead.")]
		public NameValueCollection(IHashCodeProvider hashProvider, IComparer comparer)
			: base(hashProvider, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the specified initial capacity and uses the default case-insensitive hash code provider and the default case-insensitive comparer.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06003855 RID: 14421 RVA: 0x000CF23E File Offset: 0x000CD43E
		public NameValueCollection(int capacity)
			: base(capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		// Token: 0x06003856 RID: 14422 RVA: 0x000CF247 File Offset: 0x000CD447
		public NameValueCollection(IEqualityComparer equalityComparer)
			: base(equalityComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> object can contain.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06003857 RID: 14423 RVA: 0x000CF250 File Offset: 0x000CD450
		public NameValueCollection(int capacity, IEqualityComparer equalityComparer)
			: base(capacity, equalityComparer)
		{
		}

		/// <summary>Copies the entries from the specified <see cref="T:System.Collections.Specialized.NameValueCollection" /> to a new <see cref="T:System.Collections.Specialized.NameValueCollection" /> with the specified initial capacity or the same initial capacity as the number of entries copied, whichever is greater, and using the default case-insensitive hash code provider and the default case-insensitive comparer.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> can contain.</param>
		/// <param name="col">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to copy to the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is null.</exception>
		// Token: 0x06003858 RID: 14424 RVA: 0x000CF25A File Offset: 0x000CD45A
		public NameValueCollection(int capacity, NameValueCollection col)
			: base(capacity, (col != null) ? col.Comparer : null)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			base.Comparer = col.Comparer;
			this.Add(col);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the specified initial capacity and uses the specified hash code provider and the specified comparer.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> can contain.</param>
		/// <param name="hashProvider">The <see cref="T:System.Collections.IHashCodeProvider" /> that will supply the hash codes for all keys in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06003859 RID: 14425 RVA: 0x000CF290 File Offset: 0x000CD490
		[Obsolete("Please use NameValueCollection(Int32, IEqualityComparer) instead.")]
		public NameValueCollection(int capacity, IHashCodeProvider hashProvider, IComparer comparer)
			: base(capacity, hashProvider, comparer)
		{
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000CF29B File Offset: 0x000CD49B
		internal NameValueCollection(DBNull dummy)
			: base(dummy)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is serializable and uses the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		// Token: 0x0600385B RID: 14427 RVA: 0x000CF2A4 File Offset: 0x000CD4A4
		protected NameValueCollection(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Resets the cached arrays of the collection to null.</summary>
		// Token: 0x0600385C RID: 14428 RVA: 0x000CF2AE File Offset: 0x000CD4AE
		protected void InvalidateCachedArrays()
		{
			this._all = null;
			this._allKeys = null;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000CF2C0 File Offset: 0x000CD4C0
		private static string GetAsOneString(ArrayList list)
		{
			int num = ((list != null) ? list.Count : 0);
			if (num == 1)
			{
				return (string)list[0];
			}
			if (num > 1)
			{
				StringBuilder stringBuilder = new StringBuilder((string)list[0]);
				for (int i = 1; i < num; i++)
				{
					stringBuilder.Append(',');
					stringBuilder.Append((string)list[i]);
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000CF334 File Offset: 0x000CD534
		private static string[] GetAsStringArray(ArrayList list)
		{
			int num = ((list != null) ? list.Count : 0);
			if (num == 0)
			{
				return null;
			}
			string[] array = new string[num];
			list.CopyTo(0, array, 0, num);
			return array;
		}

		/// <summary>Copies the entries in the specified <see cref="T:System.Collections.Specialized.NameValueCollection" /> to the current <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="c">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to copy to the current <see cref="T:System.Collections.Specialized.NameValueCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null.</exception>
		// Token: 0x0600385F RID: 14431 RVA: 0x000CF368 File Offset: 0x000CD568
		public void Add(NameValueCollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			this.InvalidateCachedArrays();
			int count = c.Count;
			for (int i = 0; i < count; i++)
			{
				string key = c.GetKey(i);
				string[] values = c.GetValues(i);
				if (values != null)
				{
					for (int j = 0; j < values.Length; j++)
					{
						this.Add(key, values[j]);
					}
				}
				else
				{
					this.Add(key, null);
				}
			}
		}

		/// <summary>Invalidates the cached arrays and removes all entries from the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003860 RID: 14432 RVA: 0x000CF3D6 File Offset: 0x000CD5D6
		public virtual void Clear()
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException(global::SR.GetString("Collection is read-only."));
			}
			this.InvalidateCachedArrays();
			base.BaseClear();
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.NameValueCollection" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.NameValueCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="dest" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dest" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dest" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.NameValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="dest" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.NameValueCollection" /> cannot be cast automatically to the type of the destination <paramref name="dest" />.</exception>
		// Token: 0x06003861 RID: 14433 RVA: 0x000CF3FC File Offset: 0x000CD5FC
		public void CopyTo(Array dest, int index)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (dest.Rank != 1)
			{
				throw new ArgumentException(global::SR.GetString("Multi dimension array is not supported on this operation."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", global::SR.GetString("Index {0} is out of range.", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
			}
			if (dest.Length - index < this.Count)
			{
				throw new ArgumentException(global::SR.GetString("Insufficient space in the target location to copy the information."));
			}
			int count = this.Count;
			if (this._all == null)
			{
				string[] array = new string[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this.Get(i);
					dest.SetValue(array[i], i + index);
				}
				this._all = array;
				return;
			}
			for (int j = 0; j < count; j++)
			{
				dest.SetValue(this._all[j], j + index);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.NameValueCollection" /> contains keys that are not null.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.NameValueCollection" /> contains keys that are not null; otherwise, false.</returns>
		// Token: 0x06003862 RID: 14434 RVA: 0x000CF4DB File Offset: 0x000CD6DB
		public bool HasKeys()
		{
			return this.InternalHasKeys();
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000CF4E3 File Offset: 0x000CD6E3
		internal virtual bool InternalHasKeys()
		{
			return base.BaseHasKeys();
		}

		/// <summary>Adds an entry with the specified name and value to the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to add. The key can be null.</param>
		/// <param name="value">The <see cref="T:System.String" /> value of the entry to add. The value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only. </exception>
		// Token: 0x06003864 RID: 14436 RVA: 0x000CF4EC File Offset: 0x000CD6EC
		public virtual void Add(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException(global::SR.GetString("Collection is read-only."));
			}
			this.InvalidateCachedArrays();
			ArrayList arrayList = (ArrayList)base.BaseGet(name);
			if (arrayList == null)
			{
				arrayList = new ArrayList(1);
				if (value != null)
				{
					arrayList.Add(value);
				}
				base.BaseAdd(name, arrayList);
				return;
			}
			if (value != null)
			{
				arrayList.Add(value);
			}
		}

		/// <summary>Gets the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" /> combined into one comma-separated list.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains a comma-separated list of the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry that contains the values to get. The key can be null.</param>
		// Token: 0x06003865 RID: 14437 RVA: 0x000CF54D File Offset: 0x000CD74D
		public virtual string Get(string name)
		{
			return NameValueCollection.GetAsOneString((ArrayList)base.BaseGet(name));
		}

		/// <summary>Gets the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry that contains the values to get. The key can be null.</param>
		// Token: 0x06003866 RID: 14438 RVA: 0x000CF560 File Offset: 0x000CD760
		public virtual string[] GetValues(string name)
		{
			return NameValueCollection.GetAsStringArray((ArrayList)base.BaseGet(name));
		}

		/// <summary>Sets the value of an entry in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to add the new value to. The key can be null.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the new value to add to the specified entry. The value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003867 RID: 14439 RVA: 0x000CF574 File Offset: 0x000CD774
		public virtual void Set(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException(global::SR.GetString("Collection is read-only."));
			}
			this.InvalidateCachedArrays();
			base.BaseSet(name, new ArrayList(1) { value });
		}

		/// <summary>Removes the entries with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to remove. The key can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003868 RID: 14440 RVA: 0x000CF5B6 File Offset: 0x000CD7B6
		public virtual void Remove(string name)
		{
			this.InvalidateCachedArrays();
			base.BaseRemove(name);
		}

		/// <summary>Gets or sets the entry with the specified key in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the comma-separated list of values associated with the specified key, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to locate. The key can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only and the operation attempts to modify the collection. </exception>
		// Token: 0x17000D9D RID: 3485
		public string this[string name]
		{
			get
			{
				return this.Get(name);
			}
			set
			{
				this.Set(name, value);
			}
		}

		/// <summary>Gets the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> combined into one comma-separated list.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains a comma-separated list of the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="index">The zero-based index of the entry that contains the values to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x0600386B RID: 14443 RVA: 0x000CF5D8 File Offset: 0x000CD7D8
		public virtual string Get(int index)
		{
			return NameValueCollection.GetAsOneString((ArrayList)base.BaseGet(index));
		}

		/// <summary>Gets the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="index">The zero-based index of the entry that contains the values to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x0600386C RID: 14444 RVA: 0x000CF5EB File Offset: 0x000CD7EB
		public virtual string[] GetValues(int index)
		{
			return NameValueCollection.GetAsStringArray((ArrayList)base.BaseGet(index));
		}

		/// <summary>Gets the key at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the key at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x0600386D RID: 14445 RVA: 0x000CF5FE File Offset: 0x000CD7FE
		public virtual string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		/// <summary>Gets the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the comma-separated list of values at the specified index of the collection.</returns>
		/// <param name="index">The zero-based index of the entry to locate in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000D9E RID: 3486
		public string this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		/// <summary>Gets all the keys in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains all the keys of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000CF610 File Offset: 0x000CD810
		public virtual string[] AllKeys
		{
			get
			{
				if (this._allKeys == null)
				{
					this._allKeys = base.BaseGetAllKeys();
				}
				return this._allKeys;
			}
		}

		// Token: 0x04002C57 RID: 11351
		private string[] _all;

		// Token: 0x04002C58 RID: 11352
		private string[] _allKeys;
	}
}

using System;
using System.Collections;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.PropertyCollection" /> class contains the properties of a <see cref="T:System.DirectoryServices.DirectoryEntry" />.</summary>
	// Token: 0x02000026 RID: 38
	public class PropertyCollection : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x0600012A RID: 298 RVA: 0x0000426D File Offset: 0x0000246D
		internal PropertyCollection()
			: this(null)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004276 File Offset: 0x00002476
		internal PropertyCollection(DirectoryEntry parent)
		{
			this._parent = parent;
		}

		/// <summary>Gets the number of properties in this collection.</summary>
		/// <returns>The number of properties in this collection.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		/// <exception cref="T:System.NotSupportedException">The directory cannot report the number of properties.</exception>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000429B File Offset: 0x0000249B
		public int Count
		{
			get
			{
				return this.m_oValues.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000042A8 File Offset: 0x000024A8
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.m_oValues.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000042B5 File Offset: 0x000024B5
		object ICollection.SyncRoot
		{
			get
			{
				return this.m_oValues.SyncRoot;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000042C2 File Offset: 0x000024C2
		private void ICopyTo(Array oArray, int iArrayIndex)
		{
			this.m_oValues.CopyTo(oArray, iArrayIndex);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06000130 RID: 304 RVA: 0x000042D1 File Offset: 0x000024D1
		void ICollection.CopyTo(Array oArray, int iArrayIndex)
		{
			this.ICopyTo(oArray, iArrayIndex);
		}

		/// <summary>Copies the all objects in this collection to an array, starting at the specified index in the target array.</summary>
		/// <param name="array">The array of <see cref="T:System.DirectoryServices.PropertyValueCollection" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		// Token: 0x06000131 RID: 305 RVA: 0x000042D1 File Offset: 0x000024D1
		public void CopyTo(PropertyValueCollection[] array, int index)
		{
			this.ICopyTo(array, index);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000042DB File Offset: 0x000024DB
		private void Add(object oKey, object oValue)
		{
			this.m_oKeys.Add(oKey);
			this.m_oValues.Add(oKey, oValue);
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" /> object. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> is read-only.-or- The <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
		// Token: 0x06000133 RID: 307 RVA: 0x000042F7 File Offset: 0x000024F7
		void IDictionary.Add(object oKey, object oValue)
		{
			this.Add(oKey, oValue);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, false.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004301 File Offset: 0x00002501
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.m_oKeys.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000430E File Offset: 0x0000250E
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.m_oKeys.IsReadOnly;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000431B File Offset: 0x0000251B
		ICollection IDictionary.Keys
		{
			get
			{
				return this.m_oValues.Keys;
			}
		}

		/// <summary>Gets the names of the properties in this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object that contains the names of the properties in this collection.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000431B File Offset: 0x0000251B
		public ICollection PropertyNames
		{
			get
			{
				return this.m_oValues.Keys;
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only. </exception>
		// Token: 0x06000138 RID: 312 RVA: 0x00004328 File Offset: 0x00002528
		void IDictionary.Clear()
		{
			this.m_oValues.Clear();
			this.m_oKeys.Clear();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004340 File Offset: 0x00002540
		private bool IContains(object oKey)
		{
			return this.m_oValues.Contains(oKey);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> object contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x0600013A RID: 314 RVA: 0x0000434E File Offset: 0x0000254E
		bool IDictionary.Contains(object oKey)
		{
			return this.IContains(oKey);
		}

		/// <summary>Determines whether the specified property is in this collection.</summary>
		/// <returns>The return value is true if the specified property belongs to this collection; otherwise, false.</returns>
		/// <param name="propertyName">The name of the property to find.</param>
		// Token: 0x0600013B RID: 315 RVA: 0x00004357 File Offset: 0x00002557
		public bool Contains(string propertyName)
		{
			return this.IContains(propertyName.ToLower());
		}

		/// <summary>Returns an enumerator that you can use to iterate through this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> that you can use to iterate through this collection.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
		// Token: 0x0600013C RID: 316 RVA: 0x00004365 File Offset: 0x00002565
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.m_oValues.GetEnumerator();
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only.-or- The <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
		// Token: 0x0600013D RID: 317 RVA: 0x00004372 File Offset: 0x00002572
		void IDictionary.Remove(object oKey)
		{
			this.m_oValues.Remove(oKey);
			this.m_oKeys.Remove(oKey);
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key.</returns>
		/// <param name="key">The key of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IDictionary" /> object is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
		// Token: 0x17000056 RID: 86
		object IDictionary.this[object oKey]
		{
			get
			{
				return this.m_oValues[oKey];
			}
			set
			{
				this.m_oValues[oKey] = value;
			}
		}

		/// <summary>Gets the values of the properties in this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the values of the properties in this collection.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000043A9 File Offset: 0x000025A9
		public ICollection Values
		{
			get
			{
				return this.m_oValues.Values;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerable" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" />.object.</returns>
		// Token: 0x06000141 RID: 321 RVA: 0x00004365 File Offset: 0x00002565
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_oValues.GetEnumerator();
		}

		/// <summary>Gets the specified property.</summary>
		/// <returns>The value of the specified property.</returns>
		/// <param name="propertyName">The name of the property to retrieve.</param>
		// Token: 0x17000058 RID: 88
		public PropertyValueCollection this[string propertyName]
		{
			get
			{
				if (this.Contains(propertyName))
				{
					return (PropertyValueCollection)this.m_oValues[propertyName.ToLower()];
				}
				PropertyValueCollection propertyValueCollection = new PropertyValueCollection(this._parent);
				this.Add(propertyName.ToLower(), propertyValueCollection);
				return propertyValueCollection;
			}
		}

		// Token: 0x04000098 RID: 152
		private ArrayList m_oKeys = new ArrayList();

		// Token: 0x04000099 RID: 153
		private Hashtable m_oValues = new Hashtable();

		// Token: 0x0400009A RID: 154
		private DirectoryEntry _parent;
	}
}

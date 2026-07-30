using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Contains a set of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects.</summary>
	// Token: 0x02000014 RID: 20
	public sealed class CryptographicAttributeObjectCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> class.</summary>
		// Token: 0x0600003E RID: 62 RVA: 0x00002E39 File Offset: 0x00001039
		public CryptographicAttributeObjectCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> class, adding a specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> to the collection.</summary>
		/// <param name="attribute">A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that is added to the collection.</param>
		// Token: 0x0600003F RID: 63 RVA: 0x00002E4C File Offset: 0x0000104C
		public CryptographicAttributeObjectCollection(CryptographicAttributeObject attribute)
			: this()
		{
			this._list.Add(attribute);
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002E61 File Offset: 0x00001061
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized, or thread safe.</summary>
		/// <returns>true if access to the collection is thread safe; otherwise false.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002E6E File Offset: 0x0000106E
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object at the specified index.</returns>
		/// <param name="index">An <see cref="T:System.Int32" /> value that represents the zero-based index of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to retrieve.</param>
		// Token: 0x1700000C RID: 12
		public CryptographicAttributeObject this[int index]
		{
			get
			{
				return (CryptographicAttributeObject)this._list[index];
			}
		}

		/// <summary>Gets an <see cref="T:System.Object" /> object used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the collection.</summary>
		/// <returns>true if the method returns the zero-based index of the added item; otherwise, false.</returns>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000044 RID: 68 RVA: 0x00002E90 File Offset: 0x00001090
		public int Add(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			AsnEncodedDataCollection asnEncodedDataCollection = new AsnEncodedDataCollection(asnEncodedData);
			return this.Add(new CryptographicAttributeObject(asnEncodedData.Oid, asnEncodedDataCollection));
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to the collection.</summary>
		/// <returns>true if the method returns the zero-based index of the added item; otherwise, false.</returns>
		/// <param name="attribute">The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified item already exists in the collection.</exception>
		// Token: 0x06000045 RID: 69 RVA: 0x00002EC4 File Offset: 0x000010C4
		public int Add(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			int num = -1;
			string value = attribute.Oid.Value;
			for (int i = 0; i < this._list.Count; i++)
			{
				if ((this._list[i] as CryptographicAttributeObject).Oid.Value == value)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				CryptographicAttributeObject cryptographicAttributeObject = this[num];
				foreach (AsnEncodedData asnEncodedData in attribute.Values)
				{
					cryptographicAttributeObject.Values.Add(asnEncodedData);
				}
				return num;
			}
			return this._list.Add(attribute);
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection to an array of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects that the collection is copied to.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> to which the collection is to be copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">null was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		// Token: 0x06000046 RID: 70 RVA: 0x00002F73 File Offset: 0x00001173
		public void CopyTo(CryptographicAttributeObject[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of this <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection to an <see cref="T:System.Array" /> array, starting at a particular index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> array that is the destination of the elements copied from this <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" />. The <see cref="T:System.Array" /> array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000047 RID: 71 RVA: 0x00002F73 File Offset: 0x00001173
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectEnumerator" /> object for the collection.</summary>
		/// <returns>true if the method returns a <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectEnumerator" /> object that can be used to enumerate the collection; otherwise, false.</returns>
		// Token: 0x06000048 RID: 72 RVA: 0x00002F82 File Offset: 0x00001182
		public CryptographicAttributeObjectEnumerator GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this._list);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x06000049 RID: 73 RVA: 0x00002F82 File Offset: 0x00001182
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this._list);
		}

		/// <summary>Removes the specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attribute" /> is null.</exception>
		// Token: 0x0600004A RID: 74 RVA: 0x00002F8F File Offset: 0x0000118F
		public void Remove(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			this._list.Remove(attribute);
		}

		// Token: 0x0400009A RID: 154
		private ArrayList _list;
	}
}

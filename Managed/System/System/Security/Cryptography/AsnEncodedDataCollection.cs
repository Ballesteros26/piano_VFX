using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.AsnEncodedData" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000394 RID: 916
	public sealed class AsnEncodedDataCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> class.</summary>
		// Token: 0x06001BBE RID: 7102 RVA: 0x0006EC92 File Offset: 0x0006CE92
		public AsnEncodedDataCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> class and adds an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the collection.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		// Token: 0x06001BBF RID: 7103 RVA: 0x0006ECA5 File Offset: 0x0006CEA5
		public AsnEncodedDataCollection(AsnEncodedData asnEncodedData)
		{
			this._list = new ArrayList();
			this._list.Add(asnEncodedData);
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.AsnEncodedData" /> objects in a collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.AsnEncodedData" /> objects.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0006ECC5 File Offset: 0x0006CEC5
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object is thread safe.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0006ECD2 File Offset: 0x0006CED2
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		/// <param name="index">The location in the collection.</param>
		// Token: 0x170005A1 RID: 1441
		public AsnEncodedData this[int index]
		{
			get
			{
				return (AsnEncodedData)this._list[index];
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An object used to synchronize access to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</returns>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0006ECF2 File Offset: 0x0006CEF2
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The index of the added <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Neither of the OIDs are null and the OIDs do not match.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">One of the OIDs is null and the OIDs do not match.</exception>
		// Token: 0x06001BC4 RID: 7108 RVA: 0x0006ECFF File Offset: 0x0006CEFF
		public int Add(AsnEncodedData asnEncodedData)
		{
			return this._list.Add(asnEncodedData);
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object into an array.</summary>
		/// <param name="array">The array that the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object is to be copied into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		// Token: 0x06001BC5 RID: 7109 RVA: 0x0006ED0D File Offset: 0x0006CF0D
		public void CopyTo(AsnEncodedData[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object into an array.</summary>
		/// <param name="array">The array that the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object is to be copied into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is a multidimensional array, which is not supported by this method.</exception>
		/// <exception cref="T:System.ArgumentException">The length for <paramref name="index" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length for <paramref name="index" /> is out of range.</exception>
		// Token: 0x06001BC6 RID: 7110 RVA: 0x0006ED0D File Offset: 0x0006CF0D
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object.</returns>
		// Token: 0x06001BC7 RID: 7111 RVA: 0x0006ED1C File Offset: 0x0006CF1C
		public AsnEncodedDataEnumerator GetEnumerator()
		{
			return new AsnEncodedDataEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object that can be used to navigate the collection.</returns>
		// Token: 0x06001BC8 RID: 7112 RVA: 0x0006ED1C File Offset: 0x0006CF1C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AsnEncodedDataEnumerator(this);
		}

		/// <summary>Removes an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		// Token: 0x06001BC9 RID: 7113 RVA: 0x0006ED24 File Offset: 0x0006CF24
		public void Remove(AsnEncodedData asnEncodedData)
		{
			this._list.Remove(asnEncodedData);
		}

		// Token: 0x040018F8 RID: 6392
		private ArrayList _list;
	}
}

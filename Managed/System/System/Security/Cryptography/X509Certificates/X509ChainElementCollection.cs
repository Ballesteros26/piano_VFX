using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020003B3 RID: 947
	public sealed class X509ChainElementCollection : ICollection, IEnumerable
	{
		// Token: 0x06001CD6 RID: 7382 RVA: 0x0007246A File Offset: 0x0007066A
		internal X509ChainElementCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>An integer representing the number of elements in the collection.</returns>
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x0007247D File Offset: 0x0007067D
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection of chain elements is synchronized.</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0007248A File Offset: 0x0007068A
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> object at the specified index.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> object.</returns>
		/// <param name="index">An integer value. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the length of the collection. </exception>
		// Token: 0x170005ED RID: 1517
		public X509ChainElement this[int index]
		{
			get
			{
				return (X509ChainElement)this._list[index];
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</summary>
		/// <returns>A pointer reference to the current object.</returns>
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x000724AA File Offset: 0x000706AA
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		/// <summary>Copies an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object into an array, starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects. </param>
		/// <param name="index">An integer representing the index value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is less than zero, or greater than or equal to the length of the array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus the current count is greater than the length of the array. </exception>
		// Token: 0x06001CDB RID: 7387 RVA: 0x000724B7 File Offset: 0x000706B7
		public void CopyTo(X509ChainElement[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object into an array, starting at the specified index.</summary>
		/// <param name="array">An array to copy the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object to.</param>
		/// <param name="index">The index of <paramref name="array" /> at which to start copying.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is less than zero, or greater than or equal to the length of the array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus the current count is greater than the length of the array. </exception>
		// Token: 0x06001CDC RID: 7388 RVA: 0x000724B7 File Offset: 0x000706B7
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementEnumerator" /> object that can be used to navigate through a collection of chain elements.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementEnumerator" /> object.</returns>
		// Token: 0x06001CDD RID: 7389 RVA: 0x000724C6 File Offset: 0x000706C6
		public X509ChainElementEnumerator GetEnumerator()
		{
			return new X509ChainElementEnumerator(this._list);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object that can be used to navigate a collection of chain elements.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		// Token: 0x06001CDE RID: 7390 RVA: 0x000724C6 File Offset: 0x000706C6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ChainElementEnumerator(this._list);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000724D3 File Offset: 0x000706D3
		internal void Add(X509Certificate2 certificate)
		{
			this._list.Add(new X509ChainElement(certificate));
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000724E7 File Offset: 0x000706E7
		internal void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000724F4 File Offset: 0x000706F4
		internal bool Contains(X509Certificate2 certificate)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				if (certificate.Equals((this._list[i] as X509ChainElement).Certificate))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040019A7 RID: 6567
		private ArrayList _list;
	}
}

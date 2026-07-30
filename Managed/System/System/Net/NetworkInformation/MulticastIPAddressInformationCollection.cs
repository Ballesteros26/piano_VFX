using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types.</summary>
	// Token: 0x02000607 RID: 1543
	public class MulticastIPAddressInformationCollection : ICollection<MulticastIPAddressInformation>, IEnumerable<MulticastIPAddressInformation>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformationCollection" /> class.</summary>
		// Token: 0x0600317D RID: 12669 RVA: 0x000BD8B8 File Offset: 0x000BBAB8
		protected internal MulticastIPAddressInformationCollection()
		{
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> is greater than the available space from <paramref name="count" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x0600317E RID: 12670 RVA: 0x000BD8CB File Offset: 0x000BBACB
		public virtual void CopyTo(MulticastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000BD8DA File Offset: 0x000BBADA
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000027E2 File Offset: 0x000009E2
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be added to the collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06003181 RID: 12673 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public virtual void Add(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(global::SR.GetString("The collection is read-only."));
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000BD8E7 File Offset: 0x000BBAE7
		internal void InternalAdd(MulticastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object exists in the collection; otherwise, false.</returns>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object to be searched in the collection.</param>
		// Token: 0x06003183 RID: 12675 RVA: 0x000BD8F5 File Offset: 0x000BBAF5
		public virtual bool Contains(MulticastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06003184 RID: 12676 RVA: 0x000BD903 File Offset: 0x000BBB03
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06003185 RID: 12677 RVA: 0x000BD90B File Offset: 0x000BBB0B
		public virtual IEnumerator<MulticastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> at the specific index of the collection.</summary>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> at the specific index in the collection.</returns>
		/// <param name="index">The index of interest.</param>
		// Token: 0x17000ADF RID: 2783
		public virtual MulticastIPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x06003187 RID: 12679 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public virtual bool Remove(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(global::SR.GetString("The collection is read-only."));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		// Token: 0x06003188 RID: 12680 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public virtual void Clear()
		{
			throw new NotSupportedException(global::SR.GetString("The collection is read-only."));
		}

		// Token: 0x040027DF RID: 10207
		private Collection<MulticastIPAddressInformation> addresses = new Collection<MulticastIPAddressInformation>();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types.</summary>
	// Token: 0x02000622 RID: 1570
	public class UnicastIPAddressInformationCollection : ICollection<UnicastIPAddressInformation>, IEnumerable<UnicastIPAddressInformation>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> class.</summary>
		// Token: 0x0600320B RID: 12811 RVA: 0x000BE475 File Offset: 0x000BC675
		protected internal UnicastIPAddressInformationCollection()
		{
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> is greater than the available space from <paramref name="offset" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x0600320C RID: 12812 RVA: 0x000BE488 File Offset: 0x000BC688
		public virtual void CopyTo(UnicastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000BE497 File Offset: 0x000BC697
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000027E2 File Offset: 0x000009E2
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x0600320F RID: 12815 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public virtual void Add(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(global::SR.GetString("The collection is read-only."));
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000BE4A4 File Offset: 0x000BC6A4
		internal void InternalAdd(UnicastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> object exists in the collection; otherwise, false.</returns>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> object to be searched in the collection.</param>
		// Token: 0x06003211 RID: 12817 RVA: 0x000BE4B2 File Offset: 0x000BC6B2
		public virtual bool Contains(UnicastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06003212 RID: 12818 RVA: 0x000BE4C0 File Offset: 0x000BC6C0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06003213 RID: 12819 RVA: 0x000BE4C8 File Offset: 0x000BC6C8
		public virtual IEnumerator<UnicastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> instance at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000B25 RID: 2853
		public virtual UnicastIPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x06003215 RID: 12821 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public virtual bool Remove(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(global::SR.GetString("The collection is read-only."));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x06003216 RID: 12822 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public virtual void Clear()
		{
			throw new NotSupportedException(global::SR.GetString("The collection is read-only."));
		}

		// Token: 0x04002831 RID: 10289
		private Collection<UnicastIPAddressInformation> addresses = new Collection<UnicastIPAddressInformation>();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types.</summary>
	// Token: 0x020005F9 RID: 1529
	public class IPAddressInformationCollection : ICollection<IPAddressInformation>, IEnumerable<IPAddressInformation>, IEnumerable
	{
		// Token: 0x060030CC RID: 12492 RVA: 0x000BD78B File Offset: 0x000BB98B
		internal IPAddressInformationCollection()
		{
		}

		/// <summary>Copies the collection to the specified array.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> is greater than the available space from <paramref name="offset" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x060030CD RID: 12493 RVA: 0x000BD79E File Offset: 0x000BB99E
		public virtual void CopyTo(IPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</returns>
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000BD7AD File Offset: 0x000BB9AD
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060030CF RID: 12495 RVA: 0x000027E2 File Offset: 0x000009E2
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x060030D0 RID: 12496 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public virtual void Add(IPAddressInformation address)
		{
			throw new NotSupportedException(global::SR.GetString("The collection is read-only."));
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000BD7BA File Offset: 0x000BB9BA
		internal void InternalAdd(IPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> object exists in the collection; otherwise. false.</returns>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> object to be searched in the collection.</param>
		// Token: 0x060030D2 RID: 12498 RVA: 0x000BD7C8 File Offset: 0x000BB9C8
		public virtual bool Contains(IPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</returns>
		// Token: 0x060030D3 RID: 12499 RVA: 0x000BD7D6 File Offset: 0x000BB9D6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</returns>
		// Token: 0x060030D4 RID: 12500 RVA: 0x000BD7DE File Offset: 0x000BB9DE
		public virtual IEnumerator<IPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> at the specified index in the collection. </summary>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000A55 RID: 2645
		public virtual IPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x060030D6 RID: 12502 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public virtual bool Remove(IPAddressInformation address)
		{
			throw new NotSupportedException(global::SR.GetString("The collection is read-only."));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x060030D7 RID: 12503 RVA: 0x000BD6DB File Offset: 0x000BB8DB
		public virtual void Clear()
		{
			throw new NotSupportedException(global::SR.GetString("The collection is read-only."));
		}

		// Token: 0x040027A7 RID: 10151
		private Collection<IPAddressInformation> addresses = new Collection<IPAddressInformation>();
	}
}

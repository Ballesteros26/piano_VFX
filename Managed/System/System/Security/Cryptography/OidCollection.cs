using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.Oid" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200038F RID: 911
	public sealed class OidCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.OidCollection" /> class.</summary>
		// Token: 0x06001B98 RID: 7064 RVA: 0x0006D92D File Offset: 0x0006BB2D
		public OidCollection()
		{
			this.m_list = new ArrayList();
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.Oid" /> object to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The index of the added <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oid">The <see cref="T:System.Security.Cryptography.Oid" /> object to add to the collection.</param>
		// Token: 0x06001B99 RID: 7065 RVA: 0x0006D940 File Offset: 0x0006BB40
		public int Add(Oid oid)
		{
			return this.m_list.Add(oid);
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.Oid" /> object from the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="index">The location of the <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</param>
		// Token: 0x17000596 RID: 1430
		public Oid this[int index]
		{
			get
			{
				return this.m_list[index] as Oid;
			}
		}

		/// <summary>Gets the first <see cref="T:System.Security.Cryptography.Oid" /> object that contains a value of the <see cref="P:System.Security.Cryptography.Oid.Value" /> property or a value of the <see cref="P:System.Security.Cryptography.Oid.FriendlyName" /> property that matches the specified string value from the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oid">A string that represents a <see cref="P:System.Security.Cryptography.Oid.Value" /> property or a <see cref="P:System.Security.Cryptography.Oid.FriendlyName" /> property.</param>
		// Token: 0x17000597 RID: 1431
		public Oid this[string oid]
		{
			get
			{
				string text = X509Utils.FindOidInfoWithFallback(2U, oid, OidGroup.All);
				if (text == null)
				{
					text = oid;
				}
				foreach (object obj in this.m_list)
				{
					Oid oid2 = (Oid)obj;
					if (oid2.Value == text)
					{
						return oid2;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection. </summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection.</returns>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x0006D9DC File Offset: 0x0006BBDC
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object.</returns>
		// Token: 0x06001B9D RID: 7069 RVA: 0x0006D9E9 File Offset: 0x0006BBE9
		public OidEnumerator GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the collection.</returns>
		// Token: 0x06001B9E RID: 7070 RVA: 0x0006D9E9 File Offset: 0x0006BBE9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.OidCollection" /> object into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Security.Cryptography.OidCollection" /> object to.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> cannot be a multidimensional array.-or-The length of <paramref name="array" /> is an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is out range.</exception>
		// Token: 0x06001B9F RID: 7071 RVA: 0x0006D9F4 File Offset: 0x0006BBF4
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(global::SR.GetString("Only single dimensional arrays are supported for the requested action."));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", global::SR.GetString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(global::SR.GetString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.OidCollection" /> object into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Security.Cryptography.OidCollection" /> object into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		// Token: 0x06001BA0 RID: 7072 RVA: 0x00041B7A File Offset: 0x0003FD7A
		public void CopyTo(Oid[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object is thread safe.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00002068 File Offset: 0x00000268
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040018E8 RID: 6376
		private ArrayList m_list;
	}
}

using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;ReferenceList&gt; element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000073 RID: 115
	public sealed class ReferenceList : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> class. </summary>
		// Token: 0x060002E3 RID: 739 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public ReferenceList()
		{
			this._references = new ArrayList();
		}

		/// <summary>Returns an enumerator that iterates through a <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through a <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</returns>
		// Token: 0x060002E4 RID: 740 RVA: 0x0000ACCF File Offset: 0x00008ECF
		public IEnumerator GetEnumerator()
		{
			return this._references.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000ACDC File Offset: 0x00008EDC
		public int Count
		{
			get
			{
				return this._references.Count;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <returns>The position at which the new element was inserted.</returns>
		/// <param name="value">A <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.DataReference" />  object.-or-The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.KeyReference" />  object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x060002E6 RID: 742 RVA: 0x0000ACE9 File Offset: 0x00008EE9
		public int Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is DataReference) && !(value is KeyReference))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return this._references.Add(value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		// Token: 0x060002E7 RID: 743 RVA: 0x0000AD25 File Offset: 0x00008F25
		public void Clear()
		{
			this._references.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection contains a specific <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object is found in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection. </param>
		// Token: 0x060002E8 RID: 744 RVA: 0x0000AD32 File Offset: 0x00008F32
		public bool Contains(object value)
		{
			return this._references.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		// Token: 0x060002E9 RID: 745 RVA: 0x0000AD40 File Offset: 0x00008F40
		public int IndexOf(object value)
		{
			return this._references.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object into the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to insert into the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.DataReference" />  object.-or-The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.KeyReference" />  object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x060002EA RID: 746 RVA: 0x0000AD4E File Offset: 0x00008F4E
		public void Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is DataReference) && !(value is KeyReference))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			this._references.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to remove from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		// Token: 0x060002EB RID: 747 RVA: 0x0000AD8B File Offset: 0x00008F8B
		public void Remove(object value)
		{
			this._references.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to remove.</param>
		// Token: 0x060002EC RID: 748 RVA: 0x0000AD99 File Offset: 0x00008F99
		public void RemoveAt(int index)
		{
			this._references.RemoveAt(index);
		}

		/// <summary>Returns the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to return.</param>
		// Token: 0x060002ED RID: 749 RVA: 0x0000ADA7 File Offset: 0x00008FA7
		public EncryptedReference Item(int index)
		{
			return (EncryptedReference)this._references[index];
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to return.</param>
		// Token: 0x170000B1 RID: 177
		[IndexerName("ItemOf")]
		public EncryptedReference this[int index]
		{
			get
			{
				return this.Item(index);
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x170000B2 RID: 178
		object IList.this[int index]
		{
			get
			{
				return this._references[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!(value is DataReference) && !(value is KeyReference))
				{
					throw new ArgumentException("Type of input object is invalid.", "value");
				}
				this._references[index] = value;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object to an array, starting at a specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060002F2 RID: 754 RVA: 0x0000AE0E File Offset: 0x0000900E
		public void CopyTo(Array array, int index)
		{
			this._references.CopyTo(array, index);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000AE1D File Offset: 0x0000901D
		bool IList.IsFixedSize
		{
			get
			{
				return this._references.IsFixedSize;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000AE2A File Offset: 0x0000902A
		bool IList.IsReadOnly
		{
			get
			{
				return this._references.IsReadOnly;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000AE37 File Offset: 0x00009037
		public object SyncRoot
		{
			get
			{
				return this._references.SyncRoot;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object is synchronized (thread safe); otherwise, false. </returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000AE44 File Offset: 0x00009044
		public bool IsSynchronized
		{
			get
			{
				return this._references.IsSynchronized;
			}
		}

		// Token: 0x04000187 RID: 391
		private ArrayList _references;
	}
}

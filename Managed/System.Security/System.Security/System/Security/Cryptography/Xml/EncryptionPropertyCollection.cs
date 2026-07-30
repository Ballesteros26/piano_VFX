using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> classes used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x0200005D RID: 93
	public sealed class EncryptionPropertyCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> class.</summary>
		// Token: 0x06000250 RID: 592 RVA: 0x0000934C File Offset: 0x0000754C
		public EncryptionPropertyCollection()
		{
			this._props = new ArrayList();
		}

		/// <summary>Returns an enumerator that iterates through an <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through an <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x06000251 RID: 593 RVA: 0x0000935F File Offset: 0x0000755F
		public IEnumerator GetEnumerator()
		{
			return this._props.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000936C File Offset: 0x0000756C
		public int Count
		{
			get
			{
				return this._props.Count;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x06000253 RID: 595 RVA: 0x00009379 File Offset: 0x00007579
		int IList.Add(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return this._props.Add(value);
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>The position at which the new element is inserted.</returns>
		/// <param name="value">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x06000254 RID: 596 RVA: 0x0000939F File Offset: 0x0000759F
		public int Add(EncryptionProperty value)
		{
			return this._props.Add(value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		// Token: 0x06000255 RID: 597 RVA: 0x000093AD File Offset: 0x000075AD
		public void Clear()
		{
			this._props.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x06000256 RID: 598 RVA: 0x000093BA File Offset: 0x000075BA
		bool IList.Contains(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return this._props.Contains(value);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object contains a specific <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object is found in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. </param>
		// Token: 0x06000257 RID: 599 RVA: 0x000093E0 File Offset: 0x000075E0
		public bool Contains(EncryptionProperty value)
		{
			return this._props.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x06000258 RID: 600 RVA: 0x000093EE File Offset: 0x000075EE
		int IList.IndexOf(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return this._props.IndexOf(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x06000259 RID: 601 RVA: 0x00009414 File Offset: 0x00007614
		public int IndexOf(EncryptionProperty value)
		{
			return this._props.IndexOf(value);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x0600025A RID: 602 RVA: 0x00009422 File Offset: 0x00007622
		void IList.Insert(int index, object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			this._props.Insert(index, value);
		}

		/// <summary>Inserts an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object into the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to insert into the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x0600025B RID: 603 RVA: 0x00009449 File Offset: 0x00007649
		public void Insert(int index, EncryptionProperty value)
		{
			this._props.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		// Token: 0x0600025C RID: 604 RVA: 0x00009458 File Offset: 0x00007658
		void IList.Remove(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			this._props.Remove(value);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to remove from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		// Token: 0x0600025D RID: 605 RVA: 0x0000947E File Offset: 0x0000767E
		public void Remove(EncryptionProperty value)
		{
			this._props.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to remove.</param>
		// Token: 0x0600025E RID: 606 RVA: 0x0000948C File Offset: 0x0000768C
		public void RemoveAt(int index)
		{
			this._props.RemoveAt(index);
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object has a fixed size; otherwise, false.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000949A File Offset: 0x0000769A
		public bool IsFixedSize
		{
			get
			{
				return this._props.IsFixedSize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is read-only; otherwise, false. </returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000094A7 File Offset: 0x000076A7
		public bool IsReadOnly
		{
			get
			{
				return this._props.IsReadOnly;
			}
		}

		/// <summary>Returns the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to return.</param>
		// Token: 0x06000261 RID: 609 RVA: 0x000094B4 File Offset: 0x000076B4
		public EncryptionProperty Item(int index)
		{
			return (EncryptionProperty)this._props[index];
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to return.</param>
		// Token: 0x1700009A RID: 154
		[IndexerName("ItemOf")]
		public EncryptionProperty this[int index]
		{
			get
			{
				return (EncryptionProperty)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x1700009B RID: 155
		object IList.this[int index]
		{
			get
			{
				return this._props[index];
			}
			set
			{
				if (!(value is EncryptionProperty))
				{
					throw new ArgumentException("Type of input object is invalid.", "value");
				}
				this._props[index] = value;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x06000266 RID: 614 RVA: 0x00009514 File Offset: 0x00007714
		public void CopyTo(Array array, int index)
		{
			this._props.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object to an array of <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> objects, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional array of  <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> objects that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000267 RID: 615 RVA: 0x00009514 File Offset: 0x00007714
		public void CopyTo(EncryptionProperty[] array, int index)
		{
			this._props.CopyTo(array, index);
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00009523 File Offset: 0x00007723
		public object SyncRoot
		{
			get
			{
				return this._props.SyncRoot;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00009530 File Offset: 0x00007730
		public bool IsSynchronized
		{
			get
			{
				return this._props.IsSynchronized;
			}
		}

		// Token: 0x04000168 RID: 360
		private ArrayList _props;
	}
}

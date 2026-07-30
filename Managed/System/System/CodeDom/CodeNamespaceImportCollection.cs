using System;
using System.Collections;
using System.Collections.Generic;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeNamespaceImport" /> objects.</summary>
	// Token: 0x02000780 RID: 1920
	[Serializable]
	public class CodeNamespaceImportCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000EC3 RID: 3779
		public CodeNamespaceImport this[int index]
		{
			get
			{
				return (CodeNamespaceImport)this._data[index];
			}
			set
			{
				this._data[index] = value;
				this.SyncKeys();
			}
		}

		/// <summary>Gets the number of namespaces in the collection.</summary>
		/// <returns>The number of namespaces in the collection.</returns>
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x000D9B82 File Offset: 0x000D7D82
		public int Count
		{
			get
			{
				return this._data.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.  This property always returns false.</returns>
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x00004240 File Offset: 0x00002440
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.  This property always returns false.</returns>
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06003CDF RID: 15583 RVA: 0x00004240 File Offset: 0x00002440
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to add to the collection. </param>
		// Token: 0x06003CE0 RID: 15584 RVA: 0x000D9B8F File Offset: 0x000D7D8F
		public void Add(CodeNamespaceImport value)
		{
			if (!this._keys.ContainsKey(value.Namespace))
			{
				this._keys[value.Namespace] = value;
				this._data.Add(value);
			}
		}

		/// <summary>Adds a set of <see cref="T:System.CodeDom.CodeNamespaceImport" /> objects to the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeNamespaceImport" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003CE1 RID: 15585 RVA: 0x000D9BC4 File Offset: 0x000D7DC4
		public void AddRange(CodeNamespaceImport[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (CodeNamespaceImport codeNamespaceImport in value)
			{
				this.Add(codeNamespaceImport);
			}
		}

		/// <summary>Clears the collection of members.</summary>
		// Token: 0x06003CE2 RID: 15586 RVA: 0x000D9BFA File Offset: 0x000D7DFA
		public void Clear()
		{
			this._data.Clear();
			this._keys.Clear();
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000D9C14 File Offset: 0x000D7E14
		private void SyncKeys()
		{
			this._keys.Clear();
			foreach (object obj in this._data)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				this._keys[codeNamespaceImport.Namespace] = codeNamespaceImport;
			}
		}

		/// <summary>Gets an enumerator that enumerates the collection members.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that indicates the collection members.</returns>
		// Token: 0x06003CE4 RID: 15588 RVA: 0x000D9C84 File Offset: 0x000D7E84
		public IEnumerator GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000EC7 RID: 3783
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (CodeNamespaceImport)value;
				this.SyncKeys();
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003CE7 RID: 15591 RVA: 0x000D9CAF File Offset: 0x000D7EAF
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false. This property always returns false. </returns>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  This property always returns null.</returns>
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06003CE9 RID: 15593 RVA: 0x00009E57 File Offset: 0x00008057
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06003CEA RID: 15594 RVA: 0x000D9CB7 File Offset: 0x000D7EB7
		void ICollection.CopyTo(Array array, int index)
		{
			this._data.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003CEB RID: 15595 RVA: 0x000D9CC6 File Offset: 0x000D7EC6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003CEC RID: 15596 RVA: 0x000D9CCE File Offset: 0x000D7ECE
		int IList.Add(object value)
		{
			return this._data.Add((CodeNamespaceImport)value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Collections.IList" />.</summary>
		// Token: 0x06003CED RID: 15597 RVA: 0x000D9CE1 File Offset: 0x000D7EE1
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the value is in the list; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003CEE RID: 15598 RVA: 0x000D9CE9 File Offset: 0x000D7EE9
		bool IList.Contains(object value)
		{
			return this._data.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />. </summary>
		/// <returns>The index of <paramref name="value" /> if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003CEF RID: 15599 RVA: 0x000D9CF7 File Offset: 0x000D7EF7
		int IList.IndexOf(object value)
		{
			return this._data.IndexOf((CodeNamespaceImport)value);
		}

		/// <summary>Inserts an item in the <see cref="T:System.Collections.IList" /> at the specified position. </summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003CF0 RID: 15600 RVA: 0x000D9D0A File Offset: 0x000D7F0A
		void IList.Insert(int index, object value)
		{
			this._data.Insert(index, (CodeNamespaceImport)value);
			this.SyncKeys();
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />. </summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003CF1 RID: 15601 RVA: 0x000D9D24 File Offset: 0x000D7F24
		void IList.Remove(object value)
		{
			this._data.Remove((CodeNamespaceImport)value);
			this.SyncKeys();
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.IList" />. </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003CF2 RID: 15602 RVA: 0x000D9D3D File Offset: 0x000D7F3D
		void IList.RemoveAt(int index)
		{
			this._data.RemoveAt(index);
			this.SyncKeys();
		}

		// Token: 0x04002DCC RID: 11724
		private readonly ArrayList _data = new ArrayList();

		// Token: 0x04002DCD RID: 11725
		private readonly Dictionary<string, CodeNamespaceImport> _keys = new Dictionary<string, CodeNamespaceImport>(StringComparer.OrdinalIgnoreCase);
	}
}

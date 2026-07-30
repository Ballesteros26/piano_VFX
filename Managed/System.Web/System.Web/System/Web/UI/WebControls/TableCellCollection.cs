using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates a collection of <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> and <see cref="T:System.Web.UI.WebControls.TableCell" /> objects that make up a row in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200041B RID: 1051
	[Editor("System.Web.UI.Design.WebControls.TableCellsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class TableCellCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06002F77 RID: 12151 RVA: 0x0007D559 File Offset: 0x0007B759
		internal TableCellCollection(TableRow tr)
		{
			this.cc = tr.Controls;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. The default is 0.</returns>
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x0007D56D File Offset: 0x0007B76D
		public int Count
		{
			get
			{
				return this.cc.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> is read-only.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents an element in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</returns>
		/// <param name="index">An ordinal index value that specifies the <see cref="T:System.Web.UI.WebControls.TableCell" /> to return. </param>
		// Token: 0x17000F13 RID: 3859
		public TableCell this[int index]
		{
			get
			{
				return (TableCell)this.cc[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> to the end of the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>The index number of the <see cref="T:System.Web.UI.WebControls.TableCell" />.</returns>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to add to the collection. </param>
		// Token: 0x06002F7D RID: 12157 RVA: 0x0007D590 File Offset: 0x0007B790
		public int Add(TableCell cell)
		{
			int num = this.cc.IndexOf(cell);
			if (num < 0)
			{
				this.cc.Add(cell);
				num = this.cc.Count;
			}
			return num;
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at the specified index location.</summary>
		/// <param name="index">The location in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at which to add the <see cref="T:System.Web.UI.WebControls.TableCell" />. </param>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to add to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		// Token: 0x06002F7E RID: 12158 RVA: 0x0007D5C7 File Offset: 0x0007B7C7
		public void AddAt(int index, TableCell cell)
		{
			if (this.cc.IndexOf(cell) < 0)
			{
				this.cc.AddAt(index, cell);
			}
		}

		/// <summary>Appends the <see cref="T:System.Web.UI.WebControls.TableCell" /> objects from the specified array to the end of the collection.</summary>
		/// <param name="cells">The array containing the <see cref="T:System.Web.UI.WebControls.TableCell" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="cells" /> parameter is null. </exception>
		// Token: 0x06002F7F RID: 12159 RVA: 0x0007D5E8 File Offset: 0x0007B7E8
		public void AddRange(TableCell[] cells)
		{
			foreach (TableCell tableCell in cells)
			{
				if (this.cc.IndexOf(tableCell) < 0)
				{
					this.cc.Add(tableCell);
				}
			}
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.TableCell" /> objects from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		// Token: 0x06002F80 RID: 12160 RVA: 0x0007D624 File Offset: 0x0007B824
		public void Clear()
		{
			this.cc.Clear();
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> to the specified <see cref="T:System.Array" />, starting with the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the items. </param>
		// Token: 0x06002F81 RID: 12161 RVA: 0x0007D631 File Offset: 0x0007B831
		public void CopyTo(Array array, int index)
		{
			this.cc.CopyTo(array, index);
		}

		/// <summary>Returns a value that represents the index of the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> within the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. The default is -1, which indicates that a match has not been found.</returns>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to get the index of in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		// Token: 0x06002F82 RID: 12162 RVA: 0x0007D640 File Offset: 0x0007B840
		public int GetCellIndex(TableCell cell)
		{
			return this.cc.IndexOf(cell);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableCell" /> objects in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableCell" /> objects within the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</returns>
		// Token: 0x06002F83 RID: 12163 RVA: 0x0007D64E File Offset: 0x0007B84E
		public IEnumerator GetEnumerator()
		{
			return this.cc.GetEnumerator();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to remove from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		// Token: 0x06002F84 RID: 12164 RVA: 0x0007D65B File Offset: 0x0007B85B
		public void Remove(TableCell cell)
		{
			this.cc.Remove(cell);
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.TableCell" /> to remove from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
		// Token: 0x06002F85 RID: 12165 RVA: 0x0007D669 File Offset: 0x0007B869
		public void RemoveAt(int index)
		{
			this.cc.RemoveAt(index);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the list has a fixed size; otherwise, false</returns>
		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06002F86 RID: 12166 RVA: 0x00008A69 File Offset: 0x00006C69
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		// Token: 0x17000F16 RID: 3862
		object IList.this[int index]
		{
			get
			{
				return this.cc[index];
			}
			set
			{
				this.cc.AddAt(index, (TableRow)value);
				this.cc.RemoveAt(index + 1);
			}
		}

		/// <summary>Adds an object to the collection.</summary>
		/// <returns>The index at which the object was added to the collection.</returns>
		/// <param name="o">The object to add to the collection.</param>
		// Token: 0x06002F89 RID: 12169 RVA: 0x0007D6A7 File Offset: 0x0007B8A7
		int IList.Add(object value)
		{
			this.cc.Add((TableRow)value);
			return this.cc.IndexOf((TableRow)value);
		}

		/// <summary>Determines whether the specified object is contained within the collection.</summary>
		/// <returns>true if the object is in the collection; otherwise, false.</returns>
		/// <param name="o">The object to locate within the collection.</param>
		// Token: 0x06002F8A RID: 12170 RVA: 0x0007D6CB File Offset: 0x0007B8CB
		bool IList.Contains(object value)
		{
			return this.cc.Contains((TableRow)value);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of the object within the collection; otherwise, -1 if the object is not in the collection.</returns>
		/// <param name="o">The object to locate within the collection.</param>
		// Token: 0x06002F8B RID: 12171 RVA: 0x0007D6DE File Offset: 0x0007B8DE
		int IList.IndexOf(object value)
		{
			return this.cc.IndexOf((TableRow)value);
		}

		/// <summary>Inserts an object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index within the collection at which to insert the object.</param>
		/// <param name="o">The object to insert into the collection.</param>
		// Token: 0x06002F8C RID: 12172 RVA: 0x0007D6F1 File Offset: 0x0007B8F1
		void IList.Insert(int index, object value)
		{
			this.cc.AddAt(index, (TableRow)value);
		}

		/// <summary>Removes an object from the collection.</summary>
		/// <param name="o">The object to remove from the collection.</param>
		// Token: 0x06002F8D RID: 12173 RVA: 0x0007D705 File Offset: 0x0007B905
		void IList.Remove(object value)
		{
			this.cc.Remove((TableRow)value);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal TableCellCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001BEA RID: 7146
		private ControlCollection cc;
	}
}

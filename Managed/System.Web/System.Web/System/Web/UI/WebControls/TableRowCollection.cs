using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates a collection of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects that represent a single row in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000422 RID: 1058
	[Editor("System.Web.UI.Design.WebControls.TableRowsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class TableRowCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06002FB9 RID: 12217 RVA: 0x0007DDCA File Offset: 0x0007BFCA
		internal TableRowCollection(Table table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			this.cc = table.Controls;
			this.owner = table;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. The default is 0.</returns>
		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x0007DDF3 File Offset: 0x0007BFF3
		public int Count
		{
			get
			{
				return this.cc.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> is read-only.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableRow" /> that represents an element in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</returns>
		/// <param name="index">An ordinal index value that specifies which <see cref="T:System.Web.UI.WebControls.TableRow" /> object to return. This index is zero-based.</param>
		// Token: 0x17000F28 RID: 3880
		public TableRow this[int index]
		{
			get
			{
				return (TableRow)this.cc[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.TableRow" />.</returns>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to add to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002FBF RID: 12223 RVA: 0x0007DE14 File Offset: 0x0007C014
		public int Add(TableRow row)
		{
			if (row == null)
			{
				throw new NullReferenceException();
			}
			if (row.TableRowSectionSet)
			{
				this.owner.GenerateTableSections = true;
			}
			row.Container = this;
			int num = this.cc.IndexOf(row);
			if (num < 0)
			{
				this.cc.Add(row);
				num = this.cc.Count;
			}
			return num;
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> object to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at the specified index location.</summary>
		/// <param name="index">The location in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at which to add the <see cref="T:System.Web.UI.WebControls.TableRow" />. </param>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to add to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002FC0 RID: 12224 RVA: 0x0007DE70 File Offset: 0x0007C070
		public void AddAt(int index, TableRow row)
		{
			if (row == null)
			{
				throw new NullReferenceException();
			}
			if (this.cc.IndexOf(row) < 0)
			{
				if (row.TableRowSectionSet)
				{
					this.owner.GenerateTableSections = true;
				}
				row.Container = this;
				this.cc.AddAt(index, row);
			}
		}

		/// <summary>Appends the <see cref="T:System.Web.UI.WebControls.TableRow" /> objects from the specified array to the end of the collection.</summary>
		/// <param name="rows">The array containing the <see cref="T:System.Web.UI.WebControls.TableRow" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="rows" /> parameter is null. </exception>
		// Token: 0x06002FC1 RID: 12225 RVA: 0x0007DEC0 File Offset: 0x0007C0C0
		public void AddRange(TableRow[] rows)
		{
			foreach (TableRow tableRow in rows)
			{
				if (tableRow == null)
				{
					throw new NullReferenceException();
				}
				if (this.cc.IndexOf(tableRow) < 0)
				{
					if (tableRow.TableRowSectionSet)
					{
						this.owner.GenerateTableSections = true;
					}
					tableRow.Container = this;
					this.cc.Add(tableRow);
				}
			}
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.TableRow" /> controls from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		// Token: 0x06002FC2 RID: 12226 RVA: 0x0007DF20 File Offset: 0x0007C120
		public void Clear()
		{
			this.owner.GenerateTableSections = false;
			this.cc.Clear();
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> to the specified <see cref="T:System.Array" />, starting with the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive copied contents. </param>
		// Token: 0x06002FC3 RID: 12227 RVA: 0x0007DF39 File Offset: 0x0007C139
		public void CopyTo(Array array, int index)
		{
			this.cc.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableRow" /> objects within the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableRow" /> objects within the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</returns>
		// Token: 0x06002FC4 RID: 12228 RVA: 0x0007DF48 File Offset: 0x0007C148
		public IEnumerator GetEnumerator()
		{
			return this.cc.GetEnumerator();
		}

		/// <summary>Returns a value that represents the index of the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <returns>The ordinal index position of the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> within the collection. The default is -1, which indicates that the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> has not been found.</returns>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to search for in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002FC5 RID: 12229 RVA: 0x0007DF55 File Offset: 0x0007C155
		public int GetRowIndex(TableRow row)
		{
			return this.cc.IndexOf(row);
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x0007DF63 File Offset: 0x0007C163
		internal void RowTableSectionSet()
		{
			this.owner.GenerateTableSections = true;
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to remove from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002FC7 RID: 12231 RVA: 0x0007DF71 File Offset: 0x0007C171
		public void Remove(TableRow row)
		{
			if (row != null)
			{
				row.Container = null;
			}
			this.cc.Remove(row);
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.TableRow" /> object to remove from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
		// Token: 0x06002FC8 RID: 12232 RVA: 0x0007DF8C File Offset: 0x0007C18C
		public void RemoveAt(int index)
		{
			TableRow tableRow = this[index];
			if (tableRow != null)
			{
				tableRow.Container = null;
			}
			this.cc.RemoveAt(index);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the list has a fixed size; otherwise, false</returns>
		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x00008A69 File Offset: 0x00006C69
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
		// Token: 0x17000F2B RID: 3883
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
		// Token: 0x06002FCC RID: 12236 RVA: 0x0007DFE7 File Offset: 0x0007C1E7
		int IList.Add(object value)
		{
			return this.Add(value as TableRow);
		}

		/// <summary>Determines whether the specified object is contained within the collection.</summary>
		/// <returns>true if the object is in the collection; otherwise, false.</returns>
		/// <param name="o">The object to locate within the collection.</param>
		// Token: 0x06002FCD RID: 12237 RVA: 0x0007DFF5 File Offset: 0x0007C1F5
		bool IList.Contains(object value)
		{
			return this.cc.Contains(value as TableRow);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of the object within the collection; otherwise, -1 if the object is not in the collection.</returns>
		/// <param name="o">The object to locate within the collection.</param>
		// Token: 0x06002FCE RID: 12238 RVA: 0x0007E008 File Offset: 0x0007C208
		int IList.IndexOf(object value)
		{
			return this.cc.IndexOf(value as TableRow);
		}

		/// <summary>Inserts an object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index within the collection at which to insert the object.</param>
		/// <param name="o">The object to insert into the collection.</param>
		// Token: 0x06002FCF RID: 12239 RVA: 0x0007E01B File Offset: 0x0007C21B
		void IList.Insert(int index, object value)
		{
			this.AddAt(index, value as TableRow);
		}

		/// <summary>Removes an object from the collection.</summary>
		/// <param name="o">The object to remove from the collection.</param>
		// Token: 0x06002FD0 RID: 12240 RVA: 0x0007E02A File Offset: 0x0007C22A
		void IList.Remove(object value)
		{
			this.Remove(value as TableRow);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal TableRowCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001BF2 RID: 7154
		private ControlCollection cc;

		// Token: 0x04001BF3 RID: 7155
		private Table owner;
	}
}

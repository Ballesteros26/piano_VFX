using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality for a collection of table layout styles.</summary>
	// Token: 0x0200030E RID: 782
	[Editor("System.Windows.Forms.Design.StyleCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public abstract class TableLayoutStyleCollection : ICollection, IEnumerable, IList
	{
		// Token: 0x060033D9 RID: 13273 RVA: 0x000C43E8 File Offset: 0x000C25E8
		internal TableLayoutStyleCollection(TableLayoutPanel table)
		{
			this.table = table;
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Add(System.Object)" /> method.</summary>
		/// <returns>The position into which <paramref name="style" /> was inserted.</returns>
		/// <param name="style">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060033DA RID: 13274 RVA: 0x000C4404 File Offset: 0x000C2604
		int IList.Add(object style)
		{
			TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)style;
			if (tableLayoutStyle.Owner != null)
			{
				throw new ArgumentException("Style is already owned");
			}
			tableLayoutStyle.Owner = this.table;
			int num = this.al.Add(tableLayoutStyle);
			if (this.table != null)
			{
				this.table.PerformLayout();
			}
			return num;
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method.</summary>
		/// <returns>true if <paramref name="style" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="style">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060033DB RID: 13275 RVA: 0x000C4460 File Offset: 0x000C2660
		bool IList.Contains(object style)
		{
			return this.al.Contains((TableLayoutStyle)style);
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method.</summary>
		/// <returns>The index of <paramref name="style" /> if found in the list; otherwise, -1.</returns>
		/// <param name="style">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060033DC RID: 13276 RVA: 0x000C4474 File Offset: 0x000C2674
		int IList.IndexOf(object style)
		{
			return this.al.IndexOf((TableLayoutStyle)style);
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which <paramref name="style" /> should be inserted.</param>
		/// <param name="style">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is already assigned to another owner. You must first remove it from its current location or clone it.</exception>
		// Token: 0x060033DD RID: 13277 RVA: 0x000C4488 File Offset: 0x000C2688
		void IList.Insert(int index, object style)
		{
			if (((TableLayoutStyle)style).Owner != null)
			{
				throw new ArgumentException("Style is already owned");
			}
			((TableLayoutStyle)style).Owner = this.table;
			this.al.Insert(index, (TableLayoutStyle)style);
			this.table.PerformLayout();
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method.</summary>
		/// <param name="style">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060033DE RID: 13278 RVA: 0x000C44E0 File Offset: 0x000C26E0
		void IList.Remove(object style)
		{
			((TableLayoutStyle)style).Owner = null;
			this.al.Remove((TableLayoutStyle)style);
			this.table.PerformLayout();
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.IList.IsFixedSize" /> property.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000C4518 File Offset: 0x000C2718
		bool IList.IsFixedSize
		{
			get
			{
				return this.al.IsFixedSize;
			}
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.IList.IsReadOnly" /> property.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000C4528 File Offset: 0x000C2728
		bool IList.IsReadOnly
		{
			get
			{
				return this.al.IsReadOnly;
			}
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.IList.Item(System.Int32)" /> property.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000C4538 File Offset: 0x000C2738
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x000C4548 File Offset: 0x000C2748
		object IList.Item
		{
			get
			{
				return this.al[index];
			}
			set
			{
				if (((TableLayoutStyle)value).Owner != null)
				{
					throw new ArgumentException("Style is already owned");
				}
				((TableLayoutStyle)value).Owner = this.table;
				this.al[index] = value;
				this.table.PerformLayout();
			}
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" /> method.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="startIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060033E3 RID: 13283 RVA: 0x000C459C File Offset: 0x000C279C
		void ICollection.CopyTo(Array array, int startIndex)
		{
			this.al.CopyTo(array, startIndex);
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.ICollection.SyncRoot" /> property.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x000C45AC File Offset: 0x000C27AC
		object ICollection.SyncRoot
		{
			get
			{
				return this.al.SyncRoot;
			}
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.ICollection.IsSynchronized" /> property.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000C45BC File Offset: 0x000C27BC
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.al.IsSynchronized;
			}
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060033E6 RID: 13286 RVA: 0x000C45CC File Offset: 0x000C27CC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.al.GetEnumerator();
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to the end of the current collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="style">The <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to add to the <see cref="T:System.Windows.Forms.TableLayoutStyleCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is already assigned to another owner. You must first remove it from its current location or clone it.</exception>
		// Token: 0x060033E7 RID: 13287 RVA: 0x000C45DC File Offset: 0x000C27DC
		public int Add(TableLayoutStyle style)
		{
			return this.Add(style);
		}

		/// <summary>Disassociates the collection from its associated <see cref="T:System.Windows.Forms.TableLayoutPanel" /> and empties the collection.</summary>
		// Token: 0x060033E8 RID: 13288 RVA: 0x000C45E8 File Offset: 0x000C27E8
		public void Clear()
		{
			foreach (object obj in this.al)
			{
				TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)obj;
				tableLayoutStyle.Owner = null;
			}
			this.al.Clear();
			this.table.PerformLayout();
		}

		/// <summary>Gets the number of styles actually contained in the <see cref="T:System.Windows.Forms.TableLayoutStyleCollection" />.</summary>
		/// <returns>The number of styles actually contained in the <see cref="T:System.Windows.Forms.TableLayoutStyleCollection" />.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000C4670 File Offset: 0x000C2870
		public int Count
		{
			get
			{
				return this.al.Count;
			}
		}

		/// <summary>Removes the style at the specified index of the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to be removed.</param>
		// Token: 0x060033EA RID: 13290 RVA: 0x000C4680 File Offset: 0x000C2880
		public void RemoveAt(int index)
		{
			((TableLayoutStyle)this.al[index]).Owner = null;
			this.al.RemoveAt(index);
			this.table.PerformLayout();
		}

		/// <summary>Gets or sets <see cref="T:System.Windows.Forms.TableLayoutStyle" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutStyle" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to get or set.</param>
		/// <exception cref="T:System.ArgumentException">The property value is already assigned to another owner. You must first remove it from its current location or clone it.</exception>
		// Token: 0x17000D8C RID: 3468
		public TableLayoutStyle this[int index]
		{
			get
			{
				return (TableLayoutStyle)this[index];
			}
			set
			{
				this[index] = value;
			}
		}

		// Token: 0x04001889 RID: 6281
		private ArrayList al = new ArrayList();

		// Token: 0x0400188A RID: 6282
		private TableLayoutPanel table;
	}
}

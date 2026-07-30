using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects that are selected in a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000135 RID: 309
	[ListBindable(false)]
	public class DataGridViewSelectedRowCollection : BaseCollection, ICollection, IEnumerable, IList
	{
		// Token: 0x060015BC RID: 5564 RVA: 0x00050F74 File Offset: 0x0004F174
		internal DataGridViewSelectedRowCollection(DataGridView dataGridView)
		{
			this.dataGridView = dataGridView;
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00050F84 File Offset: 0x0004F184
		bool IList.IsFixedSize
		{
			get
			{
				return base.List.IsFixedSize;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <returns>The element at the specified index. </returns>
		/// <param name="index">The index of the element to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of rows in the collection.</exception>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00050F94 File Offset: 0x0004F194
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x00050FA0 File Offset: 0x0004F1A0
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Can't insert or modify this collection.");
			}
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Add(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The index at which <paramref name="value" /> was inserted.</returns>
		/// <param name="value">The item to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015C0 RID: 5568 RVA: 0x00050FAC File Offset: 0x0004F1AC
		int IList.Add(object value)
		{
			throw new NotSupportedException("Can't add elements to this collection.");
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Clear" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015C1 RID: 5569 RVA: 0x00050FB8 File Offset: 0x0004F1B8
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the specified value is contained in the collection. </summary>
		/// <returns>true if the <paramref name="value" /> parameter is in the collection; otherwise, false.</returns>
		/// <param name="value">An object to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		// Token: 0x060015C2 RID: 5570 RVA: 0x00050FC0 File Offset: 0x0004F1C0
		bool IList.Contains(object value)
		{
			return this.Contains(value as DataGridViewRow);
		}

		/// <summary>Returns the index of the specified element. </summary>
		/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
		/// <param name="value">The element to locate in the collection.</param>
		// Token: 0x060015C3 RID: 5571 RVA: 0x00050FD0 File Offset: 0x0004F1D0
		int IList.IndexOf(object value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to add to the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015C4 RID: 5572 RVA: 0x00050FE0 File Offset: 0x0004F1E0
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as DataGridViewRow);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015C5 RID: 5573 RVA: 0x00050FF0 File Offset: 0x0004F1F0
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Can't remove elements of this collection.");
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015C6 RID: 5574 RVA: 0x00050FFC File Offset: 0x0004F1FC
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Can't remove elements of this collection.");
		}

		/// <summary>Gets the row at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the current index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> in the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of rows in the collection.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000522 RID: 1314
		public DataGridViewRow this[int index]
		{
			get
			{
				return (DataGridViewRow)base.List[index];
			}
		}

		/// <summary>Clears the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015C8 RID: 5576 RVA: 0x0005101C File Offset: 0x0004F21C
		[EditorBrowsable(1)]
		public void Clear()
		{
			throw new NotSupportedException("This collection cannot be cleared.");
		}

		/// <summary>Determines whether the specified row is contained in the collection.</summary>
		/// <returns>true if <paramref name="dataGridViewRow" /> is in the collection; otherwise, false.</returns>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015C9 RID: 5577 RVA: 0x00051028 File Offset: 0x0004F228
		public bool Contains(DataGridViewRow dataGridViewRow)
		{
			return base.List.Contains(dataGridViewRow);
		}

		/// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015CA RID: 5578 RVA: 0x00051038 File Offset: 0x0004F238
		public void CopyTo(DataGridViewRow[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Inserts a row into the collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="dataGridViewRow" /> should be inserted. </param>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015CB RID: 5579 RVA: 0x00051048 File Offset: 0x0004F248
		[EditorBrowsable(1)]
		public void Insert(int index, DataGridViewRow dataGridViewRow)
		{
			throw new NotSupportedException("Insert is not allowed.");
		}

		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns null unless overridden in a derived class.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x00051054 File Offset: 0x0004F254
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0005105C File Offset: 0x0004F25C
		internal void InternalAdd(DataGridViewRow dataGridViewRow)
		{
			base.List.Add(dataGridViewRow);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0005106C File Offset: 0x0004F26C
		internal void InternalAddRange(DataGridViewSelectedRowCollection rows)
		{
			if (rows == null)
			{
				return;
			}
			DataGridViewRow dataGridViewRow = ((this.dataGridView == null) ? null : this.dataGridView.EditingRow);
			for (int i = rows.Count - 1; i >= 0; i--)
			{
				if (rows[i] != dataGridViewRow)
				{
					base.List.Add(rows[i]);
				}
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000510DC File Offset: 0x0004F2DC
		internal void InternalClear()
		{
			this.List.Clear();
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000510EC File Offset: 0x0004F2EC
		internal void InternalRemove(DataGridViewRow dataGridViewRow)
		{
			base.List.Remove(dataGridViewRow);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000510FC File Offset: 0x0004F2FC
		virtual bool System.Collections.IList.get_IsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x04000C24 RID: 3108
		private DataGridView dataGridView;
	}
}

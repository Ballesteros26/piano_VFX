using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of cells that are selected in a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000133 RID: 307
	[ListBindable(false)]
	public class DataGridViewSelectedCellCollection : BaseCollection, ICollection, IEnumerable, IList
	{
		// Token: 0x06001592 RID: 5522 RVA: 0x00050D18 File Offset: 0x0004EF18
		internal DataGridViewSelectedCellCollection()
		{
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00050D20 File Offset: 0x0004EF20
		bool IList.IsFixedSize
		{
			get
			{
				return base.List.IsFixedSize;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00050D30 File Offset: 0x0004EF30
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x00050D3C File Offset: 0x0004EF3C
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Add(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The item to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06001596 RID: 5526 RVA: 0x00050D44 File Offset: 0x0004EF44
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Clear" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06001597 RID: 5527 RVA: 0x00050D4C File Offset: 0x0004EF4C
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the specified cell is contained in the collection.</summary>
		/// <returns>true if <paramref name="value" /> is in the collection; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.Windows.Forms.DataGridViewCell" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		// Token: 0x06001598 RID: 5528 RVA: 0x00050D54 File Offset: 0x0004EF54
		bool IList.Contains(object value)
		{
			return this.Contains(value as DataGridViewCell);
		}

		/// <summary>Returns the index of the specified cell.</summary>
		/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
		/// <param name="value">The cell to locate in the collection.</param>
		// Token: 0x06001599 RID: 5529 RVA: 0x00050D64 File Offset: 0x0004EF64
		int IList.IndexOf(object value)
		{
			return base.List.IndexOf(value as DataGridViewCell);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x0600159A RID: 5530 RVA: 0x00050D78 File Offset: 0x0004EF78
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as DataGridViewCell);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x0600159B RID: 5531 RVA: 0x00050D88 File Offset: 0x0004EF88
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Can't remove elements of selected cell base.List.");
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x0600159C RID: 5532 RVA: 0x00050D94 File Offset: 0x0004EF94
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Can't remove elements of selected cell base.List.");
		}

		/// <summary>Gets the cell at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700051A RID: 1306
		public DataGridViewCell this[int index]
		{
			get
			{
				return (DataGridViewCell)base.List[index];
			}
		}

		/// <summary>Gets a list of elements in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection.</returns>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00050DB4 File Offset: 0x0004EFB4
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		/// <summary>Clears the collection. </summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600159F RID: 5535 RVA: 0x00050DBC File Offset: 0x0004EFBC
		[EditorBrowsable(1)]
		public void Clear()
		{
			throw new NotSupportedException("Cannot clear this base.List");
		}

		/// <summary>Determines whether the specified cell is contained in the collection.</summary>
		/// <returns>true if <paramref name="dataGridViewCell" /> is in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />; otherwise, false.</returns>
		/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015A0 RID: 5536 RVA: 0x00050DC8 File Offset: 0x0004EFC8
		public bool Contains(DataGridViewCell dataGridViewCell)
		{
			return base.List.Contains(dataGridViewCell);
		}

		/// <summary>Copies the elements of the collection to the specified <see cref="T:System.Windows.Forms.DataGridViewCell" /> array, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional array of type <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015A1 RID: 5537 RVA: 0x00050DD8 File Offset: 0x0004EFD8
		public void CopyTo(DataGridViewCell[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Inserts a cell into the collection.</summary>
		/// <param name="index">The index at which <paramref name="dataGridViewCell" /> should be inserted.</param>
		/// <param name="dataGridViewCell">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015A2 RID: 5538 RVA: 0x00050DE8 File Offset: 0x0004EFE8
		[EditorBrowsable(1)]
		public void Insert(int index, DataGridViewCell dataGridViewCell)
		{
			throw new NotSupportedException("Can't insert to selected cell base.List");
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00050DF4 File Offset: 0x0004EFF4
		internal void InternalAdd(DataGridViewCell dataGridViewCell)
		{
			base.List.Add(dataGridViewCell);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00050E04 File Offset: 0x0004F004
		internal void InternalRemove(DataGridViewCell dataGridViewCell)
		{
			base.List.Remove(dataGridViewCell);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00050E14 File Offset: 0x0004F014
		virtual bool System.Collections.IList.get_IsReadOnly()
		{
			return base.IsReadOnly;
		}
	}
}

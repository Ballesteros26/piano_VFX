using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewColumn" /> objects that are selected in a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000134 RID: 308
	[ListBindable(false)]
	public class DataGridViewSelectedColumnCollection : BaseCollection, ICollection, IEnumerable, IList
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x00050E1C File Offset: 0x0004F01C
		internal DataGridViewSelectedColumnCollection()
		{
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x00050E24 File Offset: 0x0004F024
		bool IList.IsFixedSize
		{
			get
			{
				return base.List.IsFixedSize;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The index of the element to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of columns in the collection.</exception>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00050E34 File Offset: 0x0004F034
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00050E40 File Offset: 0x0004F040
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
		/// <returns>Not applicable. Always throws an exception.</returns>
		/// <param name="value">The item to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015AA RID: 5546 RVA: 0x00050E4C File Offset: 0x0004F04C
		int IList.Add(object value)
		{
			throw new NotSupportedException("Can't add elements to this collection.");
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Clear" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015AB RID: 5547 RVA: 0x00050E58 File Offset: 0x0004F058
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the specified value is contained in the collection.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is in the collection; otherwise, false.</returns>
		/// <param name="value">An object to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		// Token: 0x060015AC RID: 5548 RVA: 0x00050E60 File Offset: 0x0004F060
		bool IList.Contains(object value)
		{
			return this.Contains(value as DataGridViewColumn);
		}

		/// <summary>Returns the index of the specified element.</summary>
		/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
		/// <param name="value">The element to locate in the collection.</param>
		// Token: 0x060015AD RID: 5549 RVA: 0x00050E70 File Offset: 0x0004F070
		int IList.IndexOf(object value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015AE RID: 5550 RVA: 0x00050E80 File Offset: 0x0004F080
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as DataGridViewColumn);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015AF RID: 5551 RVA: 0x00050E90 File Offset: 0x0004F090
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Can't remove elements of this collection.");
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060015B0 RID: 5552 RVA: 0x00050E9C File Offset: 0x0004F09C
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Can't remove elements of this collection.");
		}

		/// <summary>Gets the column at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of columns in the collection.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700051E RID: 1310
		public DataGridViewColumn this[int index]
		{
			get
			{
				return (DataGridViewColumn)base.List[index];
			}
		}

		/// <summary>Clears the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015B2 RID: 5554 RVA: 0x00050EBC File Offset: 0x0004F0BC
		[EditorBrowsable(1)]
		public void Clear()
		{
			throw new NotSupportedException("This collection cannot be cleared.");
		}

		/// <summary>Determines whether the specified column is contained in the collection.</summary>
		/// <returns>true if the <paramref name="dataGridViewColumn" /> parameter is in the collection; otherwise, false.</returns>
		/// <param name="dataGridViewColumn">A <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015B3 RID: 5555 RVA: 0x00050EC8 File Offset: 0x0004F0C8
		public bool Contains(DataGridViewColumn dataGridViewColumn)
		{
			return base.List.Contains(dataGridViewColumn);
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
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015B4 RID: 5556 RVA: 0x00050ED8 File Offset: 0x0004F0D8
		public void CopyTo(DataGridViewColumn[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Inserts a column into the collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which the column should be inserted. </param>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015B5 RID: 5557 RVA: 0x00050EE8 File Offset: 0x0004F0E8
		[EditorBrowsable(1)]
		public void Insert(int index, DataGridViewColumn dataGridViewColumn)
		{
			throw new NotSupportedException("Insert is not allowed.");
		}

		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns null unless overridden in a derived class.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00050EF4 File Offset: 0x0004F0F4
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00050EFC File Offset: 0x0004F0FC
		internal void InternalAdd(DataGridViewColumn dataGridViewColumn)
		{
			base.List.Add(dataGridViewColumn);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00050F0C File Offset: 0x0004F10C
		internal void InternalAddRange(DataGridViewSelectedColumnCollection columns)
		{
			if (columns == null)
			{
				return;
			}
			for (int i = columns.Count - 1; i >= 0; i--)
			{
				base.List.Add(columns[i]);
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00050F4C File Offset: 0x0004F14C
		internal void InternalClear()
		{
			this.List.Clear();
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00050F5C File Offset: 0x0004F15C
		internal void InternalRemove(DataGridViewColumn dataGridViewColumn)
		{
			base.List.Remove(dataGridViewColumn);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00050F6C File Offset: 0x0004F16C
		virtual bool System.Collections.IList.get_IsReadOnly()
		{
			return base.IsReadOnly;
		}
	}
}

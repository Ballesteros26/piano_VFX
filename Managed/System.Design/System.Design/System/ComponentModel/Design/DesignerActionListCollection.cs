using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects.</summary>
	// Token: 0x02000111 RID: 273
	[ComVisible(true)]
	public class DesignerActionListCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> class with default settings.</summary>
		// Token: 0x060007F4 RID: 2036 RVA: 0x00008C76 File Offset: 0x00006E76
		public DesignerActionListCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> class with the specified panel items.</summary>
		/// <param name="value">The array of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects to populate the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060007F5 RID: 2037 RVA: 0x0000D746 File Offset: 0x0000B946
		public DesignerActionListCollection(DesignerActionList[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x170001D6 RID: 470
		public DesignerActionList this[int index]
		{
			get
			{
				return (DesignerActionList)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the supplied <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to the current collection.</summary>
		/// <returns>The position into which the new element is inserted into the collection's internal list.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to add.</param>
		// Token: 0x060007F8 RID: 2040 RVA: 0x0000D6B5 File Offset: 0x0000B8B5
		public int Add(DesignerActionList value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the elements of the supplied <see cref="T:System.ComponentModel.Design.DesignerActionList" /> array to the end of the current collection.</summary>
		/// <param name="value">The array of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060007F9 RID: 2041 RVA: 0x0000D768 File Offset: 0x0000B968
		public void AddRange(DesignerActionList[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (DesignerActionList designerActionList in value)
			{
				this.Add(designerActionList);
			}
		}

		/// <summary>Adds the elements of the supplied <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> to the end of the current collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060007FA RID: 2042 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public void AddRange(DesignerActionListCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (object obj in value)
			{
				DesignerActionList designerActionList = (DesignerActionList)obj;
				this.Add(designerActionList);
			}
		}

		/// <summary>Indicates whether the collection contains a specific value.</summary>
		/// <returns>true if the collection contains the specified value; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to search for.</param>
		// Token: 0x060007FB RID: 2043 RVA: 0x0000D6C3 File Offset: 0x0000B8C3
		public bool Contains(DesignerActionList value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the elements of the current collection into the supplied array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array of type <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the current collection is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.InvalidCastException">A problem occurred casting the elements of the current collection to the type of the destination array, perhaps as the result of a failed downcast.</exception>
		// Token: 0x060007FC RID: 2044 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
		public void CopyTo(DesignerActionList[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the internal list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to locate in the collection.</param>
		// Token: 0x060007FD RID: 2045 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public int IndexOf(DesignerActionList value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the supplied <see cref="T:System.ComponentModel.Design.DesignerActionList" /> into the collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the count of elements in the current collection.</exception>
		// Token: 0x060007FE RID: 2046 RVA: 0x0000D6EE File Offset: 0x0000B8EE
		public void Insert(int index, DesignerActionList value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.ComponentModel.Design.DesignerActionList" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to remove from the current collection.</param>
		// Token: 0x060007FF RID: 2047 RVA: 0x0000D6FD File Offset: 0x0000B8FD
		public void Remove(DesignerActionList value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00002432 File Offset: 0x00000632
		protected override void OnClear()
		{
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> instance.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x06000801 RID: 2049 RVA: 0x00002432 File Offset: 0x00000632
		protected override void OnInsert(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes when removing an element from the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
		// Token: 0x06000802 RID: 2050 RVA: 0x00002432 File Offset: 0x00000632
		protected override void OnRemove(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x06000803 RID: 2051 RVA: 0x00002432 File Offset: 0x00000632
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes when validating a value.</summary>
		/// <param name="value">The object to validate.</param>
		// Token: 0x06000804 RID: 2052 RVA: 0x00002432 File Offset: 0x00000632
		protected override void OnValidate(object value)
		{
		}
	}
}

using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeMember" /> objects.</summary>
	// Token: 0x02000798 RID: 1944
	[Serializable]
	public class CodeTypeMemberCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> class.</summary>
		// Token: 0x06003D90 RID: 15760 RVA: 0x00046A70 File Offset: 0x00044C70
		public CodeTypeMemberCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> with which to initialize the collection. </param>
		// Token: 0x06003D91 RID: 15761 RVA: 0x000DA8C7 File Offset: 0x000D8AC7
		public CodeTypeMemberCollection(CodeTypeMemberCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeTypeMember" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeTypeMember" /> objects with which to initialize the collection. </param>
		// Token: 0x06003D92 RID: 15762 RVA: 0x000DA8D6 File Offset: 0x000D8AD6
		public CodeTypeMemberCollection(CodeTypeMember[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeMember" /> at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeMember" /> at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000EF9 RID: 3833
		public CodeTypeMember this[int index]
		{
			get
			{
				return (CodeTypeMember)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeTypeMember" /> with the specified value to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to add. </param>
		// Token: 0x06003D95 RID: 15765 RVA: 0x00049742 File Offset: 0x00047942
		public int Add(CodeTypeMember value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeTypeMember" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeTypeMember" /> containing the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003D96 RID: 15766 RVA: 0x000DA8F8 File Offset: 0x000D8AF8
		public void AddRange(CodeTypeMember[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the contents of another <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> containing the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003D97 RID: 15767 RVA: 0x000DA92C File Offset: 0x000D8B2C
		public void AddRange(CodeTypeMemberCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Gets a value indicating whether the collection contains the specified <see cref="T:System.CodeDom.CodeTypeMember" />.</summary>
		/// <returns>true if the collection contains the specified object; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to search for in the collection. </param>
		// Token: 0x06003D98 RID: 15768 RVA: 0x000497DC File Offset: 0x000479DC
		public bool Contains(CodeTypeMember value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance, beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x06003D99 RID: 15769 RVA: 0x000497EA File Offset: 0x000479EA
		public void CopyTo(CodeTypeMember[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeTypeMember" />, if it exists in the collection.</summary>
		/// <returns>The index in the collection of the specified object, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to locate in the collection. </param>
		// Token: 0x06003D9A RID: 15770 RVA: 0x000497F9 File Offset: 0x000479F9
		public int IndexOf(CodeTypeMember value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeTypeMember" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to insert. </param>
		// Token: 0x06003D9B RID: 15771 RVA: 0x00049807 File Offset: 0x00047A07
		public void Insert(int index, CodeTypeMember value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes a specific <see cref="T:System.CodeDom.CodeTypeMember" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x06003D9C RID: 15772 RVA: 0x00049859 File Offset: 0x00047A59
		public void Remove(CodeTypeMember value)
		{
			base.List.Remove(value);
		}
	}
}

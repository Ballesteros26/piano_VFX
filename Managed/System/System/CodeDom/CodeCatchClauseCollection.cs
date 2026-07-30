using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeCatchClause" /> objects.</summary>
	// Token: 0x0200075E RID: 1886
	[Serializable]
	public class CodeCatchClauseCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> class.</summary>
		// Token: 0x06003BE3 RID: 15331 RVA: 0x00046A70 File Offset: 0x00044C70
		public CodeCatchClauseCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003BE4 RID: 15332 RVA: 0x000D89A5 File Offset: 0x000D6BA5
		public CodeCatchClauseCollection(CodeCatchClauseCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are null.</exception>
		// Token: 0x06003BE5 RID: 15333 RVA: 0x000D89B4 File Offset: 0x000D6BB4
		public CodeCatchClauseCollection(CodeCatchClause[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeCatchClause" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCatchClause" /> object at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000E79 RID: 3705
		public CodeCatchClause this[int index]
		{
			get
			{
				return (CodeCatchClause)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to add. </param>
		// Token: 0x06003BE8 RID: 15336 RVA: 0x00049742 File Offset: 0x00047942
		public int Add(CodeCatchClause value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeCatchClause" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeCatchClause" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003BE9 RID: 15337 RVA: 0x000D89D8 File Offset: 0x000D6BD8
		public void AddRange(CodeCatchClause[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003BEA RID: 15338 RVA: 0x000D8A0C File Offset: 0x000D6C0C
		public void AddRange(CodeCatchClauseCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object.</summary>
		/// <returns>true if the collection contains the specified object; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to locate in the collection. </param>
		// Token: 0x06003BEB RID: 15339 RVA: 0x000497DC File Offset: 0x000479DC
		public bool Contains(CodeCatchClause value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x06003BEC RID: 15340 RVA: 0x000497EA File Offset: 0x000479EA
		public void CopyTo(CodeCatchClause[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object in the collection, if it exists in the collection.</summary>
		/// <returns>The index of the specified object, if found, in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to locate in the collection. </param>
		// Token: 0x06003BED RID: 15341 RVA: 0x000497F9 File Offset: 0x000479F9
		public int IndexOf(CodeCatchClause value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to insert. </param>
		// Token: 0x06003BEE RID: 15342 RVA: 0x00049807 File Offset: 0x00047A07
		public void Insert(int index, CodeCatchClause value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x06003BEF RID: 15343 RVA: 0x00049859 File Offset: 0x00047A59
		public void Remove(CodeCatchClause value)
		{
			base.List.Remove(value);
		}
	}
}

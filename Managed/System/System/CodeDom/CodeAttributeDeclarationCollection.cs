using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects.</summary>
	// Token: 0x02000758 RID: 1880
	[Serializable]
	public class CodeAttributeDeclarationCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> class.</summary>
		// Token: 0x06003BBC RID: 15292 RVA: 0x00046A70 File Offset: 0x00044C70
		public CodeAttributeDeclarationCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003BBD RID: 15293 RVA: 0x000D8760 File Offset: 0x000D6960
		public CodeAttributeDeclarationCollection(CodeAttributeDeclarationCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are null.</exception>
		// Token: 0x06003BBE RID: 15294 RVA: 0x000D876F File Offset: 0x000D696F
		public CodeAttributeDeclarationCollection(CodeAttributeDeclaration[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object at the specified index.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000E70 RID: 3696
		public CodeAttributeDeclaration this[int index]
		{
			get
			{
				return (CodeAttributeDeclaration)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object with the specified value to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to add. </param>
		// Token: 0x06003BC1 RID: 15297 RVA: 0x00049742 File Offset: 0x00047942
		public int Add(CodeAttributeDeclaration value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003BC2 RID: 15298 RVA: 0x000D8794 File Offset: 0x000D6994
		public void AddRange(CodeAttributeDeclaration[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003BC3 RID: 15299 RVA: 0x000D87C8 File Offset: 0x000D69C8
		public void AddRange(CodeAttributeDeclarationCollection value)
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

		/// <summary>Gets or sets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object.</summary>
		/// <returns>true if the collection contains the specified object; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to locate. </param>
		// Token: 0x06003BC4 RID: 15300 RVA: 0x000497DC File Offset: 0x000479DC
		public bool Contains(CodeAttributeDeclaration value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x06003BC5 RID: 15301 RVA: 0x000497EA File Offset: 0x000479EA
		public void CopyTo(CodeAttributeDeclaration[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object in the collection, if it exists in the collection.</summary>
		/// <returns>The index in the collection of the specified object, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to locate in the collection. </param>
		// Token: 0x06003BC6 RID: 15302 RVA: 0x000497F9 File Offset: 0x000479F9
		public int IndexOf(CodeAttributeDeclaration value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to insert. </param>
		// Token: 0x06003BC7 RID: 15303 RVA: 0x00049807 File Offset: 0x00047A07
		public void Insert(int index, CodeAttributeDeclaration value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x06003BC8 RID: 15304 RVA: 0x00049859 File Offset: 0x00047A59
		public void Remove(CodeAttributeDeclaration value)
		{
			base.List.Remove(value);
		}
	}
}

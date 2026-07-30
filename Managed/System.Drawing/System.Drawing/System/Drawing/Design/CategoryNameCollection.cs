using System;
using System.Collections;

namespace System.Drawing.Design
{
	/// <summary>Represents a collection of category name strings.</summary>
	// Token: 0x0200011B RID: 283
	public sealed class CategoryNameCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.CategoryNameCollection" /> class using the specified collection.</summary>
		/// <param name="value">A <see cref="T:System.Drawing.Design.CategoryNameCollection" /> that contains the names to initialize the collection values to. </param>
		// Token: 0x06000D3A RID: 3386 RVA: 0x0001D89C File Offset: 0x0001BA9C
		public CategoryNameCollection(CategoryNameCollection value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.CategoryNameCollection" /> class using the specified array of names.</summary>
		/// <param name="value">An array of strings that contains the names of the categories to initialize the collection values to. </param>
		// Token: 0x06000D3B RID: 3387 RVA: 0x0001D89C File Offset: 0x0001BA9C
		public CategoryNameCollection(string[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets the category name at the specified index.</summary>
		/// <returns>The category name at the specified index.</returns>
		/// <param name="index">The index of the collection element to access. </param>
		// Token: 0x1700039A RID: 922
		public string this[int index]
		{
			get
			{
				return (string)base.InnerList[index];
			}
		}

		/// <summary>Indicates whether the specified category is contained in the collection.</summary>
		/// <returns>true if the specified category is contained in the collection; otherwise, false.</returns>
		/// <param name="value">The string to check for in the collection. </param>
		// Token: 0x06000D3D RID: 3389 RVA: 0x0001D8C3 File Offset: 0x0001BAC3
		public bool Contains(string value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the collection elements to the specified array at the specified index.</summary>
		/// <param name="array">The array to copy to. </param>
		/// <param name="index">The index of the destination array at which to begin copying. </param>
		// Token: 0x06000D3E RID: 3390 RVA: 0x0001D8D1 File Offset: 0x0001BAD1
		public void CopyTo(string[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified value.</summary>
		/// <returns>The index in the collection, or null if the string does not exist in the collection.</returns>
		/// <param name="value">The category name to retrieve the index of in the collection. </param>
		// Token: 0x06000D3F RID: 3391 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		public int IndexOf(string value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}

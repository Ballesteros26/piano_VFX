using System;
using System.Collections;

namespace System.DirectoryServices
{
	/// <summary>Contains the values of a <see cref="T:System.DirectoryServices.SearchResult" /> property.          </summary>
	// Token: 0x0200002B RID: 43
	public class ResultPropertyValueCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00004778 File Offset: 0x00002978
		internal ResultPropertyValueCollection()
		{
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004780 File Offset: 0x00002980
		internal void Add(object component)
		{
			base.InnerList.Add(component);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000478F File Offset: 0x0000298F
		internal void AddRange(object[] components)
		{
			base.InnerList.AddRange(components);
		}

		/// <summary>The <see cref="P:System.DirectoryServices.ResultPropertyValueCollection.Item(System.Int32)" /> property gets the property value that is located at a specified index.          </summary>
		/// <returns>The property value that is located at the specified index.</returns>
		/// <param name="index">The zero-based index of the property value to retrieve.</param>
		// Token: 0x17000060 RID: 96
		public virtual object this[int index]
		{
			get
			{
				return base.InnerList[index];
			}
		}

		/// <summary>The <see cref="M:System.DirectoryServices.ResultPropertyValueCollection.Contains(System.Object)" /> method determines whether a specified property value is in this collection.          </summary>
		/// <returns>The return value is true if the specified property belongs to this collection; otherwise, false.</returns>
		/// <param name="value">The property value to find.</param>
		// Token: 0x06000167 RID: 359 RVA: 0x000047AB File Offset: 0x000029AB
		public bool Contains(object value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>The <see cref="M:System.DirectoryServices.ResultPropertyValueCollection.CopyTo(System.Object[],System.Int32)" /> method copies the property values from this collection to an array, starting at a particular index of the array.          </summary>
		/// <param name="values">An array of type <see cref="T:System.Object" /> that receives this collection's property values.</param>
		/// <param name="index">The zero-based array index at which to begin copying the property values.</param>
		// Token: 0x06000168 RID: 360 RVA: 0x000047B9 File Offset: 0x000029B9
		public void CopyTo(object[] values, int index)
		{
			base.InnerList.CopyTo(values, index);
		}

		/// <summary>The <see cref="M:System.DirectoryServices.ResultPropertyValueCollection.IndexOf(System.Object)" /> method retrieves the index of a specified property value in this collection.          </summary>
		/// <returns>The zero-based index of the specified property value. If the object is not found, the return value is -1.</returns>
		/// <param name="value">The property value to find.</param>
		// Token: 0x06000169 RID: 361 RVA: 0x000047C8 File Offset: 0x000029C8
		public int IndexOf(object value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}

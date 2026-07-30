using System;
using System.Collections;

namespace System.DirectoryServices
{
	/// <summary>Contains the properties of a <see cref="T:System.DirectoryServices.SearchResult" /> instance.          </summary>
	// Token: 0x0200002A RID: 42
	public class ResultPropertyCollection : DictionaryBase
	{
		// Token: 0x0600015C RID: 348 RVA: 0x000046B8 File Offset: 0x000028B8
		internal ResultPropertyCollection()
		{
		}

		/// <summary>Gets the property from this collection that has the specified name.          </summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ResultPropertyValueCollection" /> that has the specified name.</returns>
		/// <param name="name">The name of the property to retrieve.</param>
		// Token: 0x1700005D RID: 93
		public ResultPropertyValueCollection this[string name]
		{
			get
			{
				return (ResultPropertyValueCollection)base.Dictionary[name.ToLower()];
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000046D8 File Offset: 0x000028D8
		internal void Add(string key, ResultPropertyValueCollection rpcoll)
		{
			base.Dictionary.Add(key.ToLower(), rpcoll);
		}

		/// <summary>Determines whether the property that has the specified name belongs to this collection.          </summary>
		/// <returns>The return value is true if the specified property belongs to this collection; otherwise, false.</returns>
		/// <param name="propertyName">The name of the property to find.</param>
		// Token: 0x0600015F RID: 351 RVA: 0x000046EC File Offset: 0x000028EC
		public bool Contains(string propertyName)
		{
			return base.Dictionary.Contains(propertyName.ToLower());
		}

		/// <summary>Gets the names of the properties in this collection.          </summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the properties in this collection.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000046FF File Offset: 0x000028FF
		public ICollection PropertyNames
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets the values of the properties in this collection.          </summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the values of the properties in this collection.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000470C File Offset: 0x0000290C
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Copies the properties from this collection to an array, starting at a particular index of the array.          </summary>
		/// <param name="array">An array of type <see cref="T:System.DirectoryServices.ResultPropertyValueCollection" /> that receives this collection's properties.</param>
		/// <param name="index">The zero-based array index at which to begin copying the properties.</param>
		// Token: 0x06000162 RID: 354 RVA: 0x0000471C File Offset: 0x0000291C
		public void CopyTo(ResultPropertyValueCollection[] array, int index)
		{
			foreach (object obj in this.Values)
			{
				ResultPropertyValueCollection resultPropertyValueCollection = (ResultPropertyValueCollection)obj;
				array[index++] = resultPropertyValueCollection;
			}
		}
	}
}

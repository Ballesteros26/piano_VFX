using System;
using System.Collections;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for all model binders for the application, listed by binder type.</summary>
	// Token: 0x02000721 RID: 1825
	public class ModelBinderDictionary : ICollection<KeyValuePair<Type, IModelBinder>>, IEnumerable<KeyValuePair<Type, IModelBinder>>, IEnumerable, IDictionary<Type, IModelBinder>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBinderDictionary" /> class.</summary>
		// Token: 0x06004C0D RID: 19469 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelBinderDictionary()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the number of items in the dictionary.</summary>
		/// <returns>The number of items in the dictionary.</returns>
		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x000CAD58 File Offset: 0x000C8F58
		public int Count
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets the default model binder.</summary>
		/// <returns>The default model binder.</returns>
		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004C10 RID: 19472 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public IModelBinder DefaultBinder
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary is read-only.</summary>
		/// <returns>true if the dictionary is read-only; otherwise, false.</returns>
		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x06004C11 RID: 19473 RVA: 0x000CAD74 File Offset: 0x000C8F74
		public bool IsReadOnly
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the specified key in an object that implements the <see cref="T:System.Web.ModelBinding.IModelBinder" /> interface.</summary>
		/// <returns>The element that has the specified key.</returns>
		/// <param name="key">The item key.</param>
		// Token: 0x17001776 RID: 6006
		public IModelBinder this[Type key]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a collection that contains the keys in the dictionary.</summary>
		/// <returns>A collection that contains the keys in the dictionary.</returns>
		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public ICollection<Type> Keys
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a collection that contains the values in the model binder dictionary.</summary>
		/// <returns>A collection that contains the values in the model binder dictionary.</returns>
		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06004C15 RID: 19477 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public ICollection<IModelBinder> Values
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Adds the specified item to the dictionary.</summary>
		/// <param name="item">The object to add to the dictionary.</param>
		// Token: 0x06004C16 RID: 19478 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Add(KeyValuePair<Type, IModelBinder> item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds the specified item to the dictionary.</summary>
		/// <param name="key">The key of the item to add.</param>
		/// <param name="value">The value of the item to add.</param>
		// Token: 0x06004C17 RID: 19479 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Add(Type key, IModelBinder value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all items from the dictionary.</summary>
		// Token: 0x06004C18 RID: 19480 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Determines whether the model binder dictionary contains a specified value.</summary>
		/// <returns>true if <paramref name="item" /> is found; otherwise, false.</returns>
		/// <param name="item">The item to search for.</param>
		// Token: 0x06004C19 RID: 19481 RVA: 0x000CAD90 File Offset: 0x000C8F90
		public bool Contains(KeyValuePair<Type, IModelBinder> item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Determines whether the model binder dictionary contains an element that has the specified key.</summary>
		/// <returns>true if <paramref name="key" /> is found; otherwise, false.</returns>
		/// <param name="key">The key to search for.</param>
		// Token: 0x06004C1A RID: 19482 RVA: 0x000CADAC File Offset: 0x000C8FAC
		public bool ContainsKey(Type key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the elements of the model binder dictionary to an array, starting at a specified index.</summary>
		/// <param name="array">The destination array. The array must be one-dimensional and have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying starts.</param>
		// Token: 0x06004C1B RID: 19483 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(KeyValuePair<Type, IModelBinder>[] array, int arrayIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06004C1C RID: 19484 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public IEnumerator<KeyValuePair<Type, IModelBinder>> GetEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Removes the first occurrence of the specified element from the model binder dictionary.</summary>
		/// <returns>true if <paramref name="item" /> was successfully removed from the dictionary; false if <paramref name="item" /> was not removed or was not found in the dictionary.</returns>
		/// <param name="item">The item to remove.</param>
		// Token: 0x06004C1D RID: 19485 RVA: 0x000CADC8 File Offset: 0x000C8FC8
		public bool Remove(KeyValuePair<Type, IModelBinder> item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Removes the element that has the specified key from the model binder dictionary.</summary>
		/// <returns>true if the element was successfully removed; false if <paramref name="key" /> was note removed or was not found in the dictionary.</returns>
		/// <param name="key">The key of the item to remove.</param>
		// Token: 0x06004C1E RID: 19486 RVA: 0x000CADE4 File Offset: 0x000C8FE4
		public bool Remove(Type key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Returns an enumerator that can be used to iterate through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06004C1F RID: 19487 RVA: 0x0000E80B File Offset: 0x0000CA0B
		IEnumerator IEnumerable.GetEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value that is associated with the specified key.</summary>
		/// <returns>true if the dictionary contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, the value that is associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		// Token: 0x06004C20 RID: 19488 RVA: 0x000CAE00 File Offset: 0x000C9000
		public bool TryGetValue(Type key, out IModelBinder value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}

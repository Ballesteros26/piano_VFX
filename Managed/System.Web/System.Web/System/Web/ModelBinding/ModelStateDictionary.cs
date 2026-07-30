using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the state of model binding.</summary>
	// Token: 0x02000525 RID: 1317
	[Serializable]
	public class ModelStateDictionary : IDictionary<string, ModelState>, ICollection<KeyValuePair<string, ModelState>>, IEnumerable<KeyValuePair<string, ModelState>>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelStateDictionary" /> class.</summary>
		// Token: 0x06003A02 RID: 14850 RVA: 0x0009D0FA File Offset: 0x0009B2FA
		public ModelStateDictionary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelStateDictionary" /> class using an existing dictionary collection.</summary>
		/// <param name="dictionary">The dictionary.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dictionary" /> parameter is null.</exception>
		// Token: 0x06003A03 RID: 14851 RVA: 0x0009D114 File Offset: 0x0009B314
		public ModelStateDictionary(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			foreach (KeyValuePair<string, ModelState> keyValuePair in dictionary)
			{
				this._innerDictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		/// <summary>Gets the number of key/value pairs in the collection.</summary>
		/// <returns>The number of key/value pairs in the collection.</returns>
		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06003A04 RID: 14852 RVA: 0x0009D194 File Offset: 0x0009B394
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary is read-only.</summary>
		/// <returns>true if the dictionary is read-only; otherwise, false.</returns>
		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x0009D1A1 File Offset: 0x0009B3A1
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).IsReadOnly;
			}
		}

		/// <summary>Gets a value that indicates whether there are any errors in any of the model state objects in the dictionary.</summary>
		/// <returns>false if any errors were found; otherwise, true.</returns>
		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x0009D1AE File Offset: 0x0009B3AE
		public bool IsValid
		{
			get
			{
				return this.Values.All((ModelState modelState) => modelState.Errors.Count == 0);
			}
		}

		/// <summary>Gets a collection that contains the keys of the dictionary.</summary>
		/// <returns>The keys of the dictionary.</returns>
		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x0009D1DA File Offset: 0x0009B3DA
		public ICollection<string> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		/// <summary>Gets or sets the value that is associated with the specified key.</summary>
		/// <returns>The item.</returns>
		/// <param name="key">The key.</param>
		// Token: 0x170011F3 RID: 4595
		public ModelState this[string key]
		{
			get
			{
				ModelState modelState;
				this._innerDictionary.TryGetValue(key, out modelState);
				return modelState;
			}
			set
			{
				this._innerDictionary[key] = value;
			}
		}

		/// <summary>Gets a collection that contains the values of the dictionary.</summary>
		/// <returns>The values of the dictionary.</returns>
		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06003A0A RID: 14858 RVA: 0x0009D214 File Offset: 0x0009B414
		public ICollection<ModelState> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		/// <summary>Adds the specified item to the dictionary.</summary>
		/// <param name="item">The item.</param>
		// Token: 0x06003A0B RID: 14859 RVA: 0x0009D221 File Offset: 0x0009B421
		public void Add(KeyValuePair<string, ModelState> item)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Add(item);
		}

		/// <summary>Adds an item that has the specified key and value to the dictionary.</summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		// Token: 0x06003A0C RID: 14860 RVA: 0x0009D22F File Offset: 0x0009B42F
		public void Add(string key, ModelState value)
		{
			this._innerDictionary.Add(key, value);
		}

		/// <summary>Adds a model error to the errors collection using the specified key and using the specified exception for the value.</summary>
		/// <param name="key">The key.</param>
		/// <param name="exception">The exception object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null.</exception>
		// Token: 0x06003A0D RID: 14861 RVA: 0x0009D23E File Offset: 0x0009B43E
		public void AddModelError(string key, Exception exception)
		{
			this.GetModelStateForKey(key).Errors.Add(exception);
		}

		/// <summary>Adds the specified model error to the errors collection using the specified key and using the specified error message string for the value.</summary>
		/// <param name="key">The key.</param>
		/// <param name="errorMessage">The error message.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null.</exception>
		// Token: 0x06003A0E RID: 14862 RVA: 0x0009D252 File Offset: 0x0009B452
		public void AddModelError(string key, string errorMessage)
		{
			this.GetModelStateForKey(key).Errors.Add(errorMessage);
		}

		/// <summary>Removes all items from the dictionary.</summary>
		// Token: 0x06003A0F RID: 14863 RVA: 0x0009D266 File Offset: 0x0009B466
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		/// <summary>Determines whether the model-state dictionary contains a specific item.</summary>
		/// <returns>true if <paramref name="item" /> is found in the dictionary; otherwise, false.</returns>
		/// <param name="item">The item to locate in the model-state dictionary.</param>
		// Token: 0x06003A10 RID: 14864 RVA: 0x0009D273 File Offset: 0x0009B473
		public bool Contains(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Contains(item);
		}

		/// <summary>Determines whether the model-state dictionary contains the specified key.</summary>
		/// <returns>true if the dictionary contains the specified key; otherwise, false.</returns>
		/// <param name="key">The key.</param>
		// Token: 0x06003A11 RID: 14865 RVA: 0x0009D281 File Offset: 0x0009B481
		public bool ContainsKey(string key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		/// <summary>Copies the elements of the dictionary to an array, starting at a specified index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying starts.</param>
		// Token: 0x06003A12 RID: 14866 RVA: 0x0009D28F File Offset: 0x0009B48F
		public void CopyTo(KeyValuePair<string, ModelState>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).CopyTo(array, arrayIndex);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the dictionary.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06003A13 RID: 14867 RVA: 0x0009D29E File Offset: 0x0009B49E
		public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x0009D2B0 File Offset: 0x0009B4B0
		private ModelState GetModelStateForKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ModelState modelState;
			if (!this.TryGetValue(key, out modelState))
			{
				modelState = new ModelState();
				this[key] = modelState;
			}
			return modelState;
		}

		/// <summary>Determines whether there are any <see cref="T:System.Web.ModelBinding.ModelError" /> objects that are associated with the specified key or that are prefixed with the specified key.</summary>
		/// <returns>true if any <see cref="T:System.Web.ModelBinding.ModelError" /> objects are associated with the specified key or prefixed with the specified key; otherwise, false. If the key is not found in the dictionary, this method returns true.</returns>
		/// <param name="key">The key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null.</exception>
		// Token: 0x06003A15 RID: 14869 RVA: 0x0009D2E5 File Offset: 0x0009B4E5
		public bool IsValidField(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return DictionaryHelpers.FindKeysWithPrefix<ModelState>(this, key).All((KeyValuePair<string, ModelState> entry) => entry.Value.Errors.Count == 0);
		}

		/// <summary>Copies the values from the specified model-state dictionary object into this dictionary, overwriting existing values if the keys are the same.</summary>
		/// <param name="dictionary">The model-state dictionary to be merged into this one.</param>
		// Token: 0x06003A16 RID: 14870 RVA: 0x0009D320 File Offset: 0x0009B520
		public void Merge(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				return;
			}
			foreach (KeyValuePair<string, ModelState> keyValuePair in dictionary)
			{
				this[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		/// <summary>Removes the first occurrence of the specified item from the model-state dictionary.</summary>
		/// <returns>true if the item was successfully removed from the dictionary, or false if the item was not removed or was not found in the dictionary.</returns>
		/// <param name="item">The item to remove.</param>
		// Token: 0x06003A17 RID: 14871 RVA: 0x0009D37C File Offset: 0x0009B57C
		public bool Remove(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Remove(item);
		}

		/// <summary>Removes the item that has the specified key from the dictionary.</summary>
		/// <returns>true if the item was successfully removed from the dictionary, or false if the item was not removed or was not found in the dictionary.</returns>
		/// <param name="key">The key of the item to remove.</param>
		// Token: 0x06003A18 RID: 14872 RVA: 0x0009D38A File Offset: 0x0009B58A
		public bool Remove(string key)
		{
			return this._innerDictionary.Remove(key);
		}

		/// <summary>Sets the value for the specified key.</summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		// Token: 0x06003A19 RID: 14873 RVA: 0x0009D398 File Offset: 0x0009B598
		public void SetModelValue(string key, ValueProviderResult value)
		{
			this.GetModelStateForKey(key).Value = value;
		}

		/// <summary>Attempts to gets the value that is associated with the specified key.</summary>
		/// <returns>true if the dictionary contains an item that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the value that is associated with the specified key, if the key was found; otherwise, contains the default value for the type of this parameter. This parameter is passed uninitialized.</param>
		// Token: 0x06003A1A RID: 14874 RVA: 0x0009D3A7 File Offset: 0x0009B5A7
		public bool TryGetValue(string key, out ModelState value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06003A1B RID: 14875 RVA: 0x0009D3B6 File Offset: 0x0009B5B6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._innerDictionary).GetEnumerator();
		}

		// Token: 0x04001F59 RID: 8025
		private readonly Dictionary<string, ModelState> _innerDictionary = new Dictionary<string, ModelState>(StringComparer.OrdinalIgnoreCase);
	}
}

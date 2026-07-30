using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
	/// <summary>Provides the abstract base class for a collection whose keys are embedded in the values.</summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TItem">The type of items in the collection.</typeparam>
	// Token: 0x020009EF RID: 2543
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(Mscorlib_KeyedCollectionDebugView<, >))]
	[Serializable]
	public abstract class KeyedCollection<TKey, TItem> : Collection<TItem>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the default equality comparer.</summary>
		// Token: 0x06005E22 RID: 24098 RVA: 0x00136985 File Offset: 0x00134B85
		protected KeyedCollection()
			: this(null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer.</summary>
		/// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or null to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
		// Token: 0x06005E23 RID: 24099 RVA: 0x0013698F File Offset: 0x00134B8F
		protected KeyedCollection(IEqualityComparer<TKey> comparer)
			: this(comparer, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer and creates a lookup dictionary when the specified threshold is exceeded.</summary>
		/// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or null to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
		/// <param name="dictionaryCreationThreshold">The number of elements the collection can hold without creating a lookup dictionary (0 creates the lookup dictionary when the first item is added), or –1 to specify that a lookup dictionary is never created.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dictionaryCreationThreshold" /> is less than –1.</exception>
		// Token: 0x06005E24 RID: 24100 RVA: 0x00136999 File Offset: 0x00134B99
		protected KeyedCollection(IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			if (dictionaryCreationThreshold == -1)
			{
				dictionaryCreationThreshold = int.MaxValue;
			}
			if (dictionaryCreationThreshold < -1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.dictionaryCreationThreshold, ExceptionResource.ArgumentOutOfRange_InvalidThreshold);
			}
			this.comparer = comparer;
			this.threshold = dictionaryCreationThreshold;
		}

		/// <summary>Gets the generic equality comparer that is used to determine equality of keys in the collection.</summary>
		/// <returns>The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface that is used to determine equality of keys in the collection.</returns>
		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x001369D0 File Offset: 0x00134BD0
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Gets the element with the specified key. </summary>
		/// <returns>The element with the specified key. If an element with the specified key is not found, an exception is thrown.</returns>
		/// <param name="key">The key of the element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">An element with the specified key does not exist in the collection.</exception>
		// Token: 0x170010B2 RID: 4274
		public TItem this[TKey key]
		{
			get
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				if (this.dict != null)
				{
					return this.dict[key];
				}
				foreach (TItem titem in base.Items)
				{
					if (this.comparer.Equals(this.GetKeyForItem(titem), key))
					{
						return titem;
					}
				}
				ThrowHelper.ThrowKeyNotFoundException();
				return default(TItem);
			}
		}

		/// <summary>Determines whether the collection contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06005E27 RID: 24103 RVA: 0x00136A6C File Offset: 0x00134C6C
		public bool Contains(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key);
			}
			if (key != null)
			{
				foreach (TItem titem in base.Items)
				{
					if (this.comparer.Equals(this.GetKeyForItem(titem), key))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x00136AFC File Offset: 0x00134CFC
		private bool ContainsItem(TItem item)
		{
			TKey keyForItem;
			if (this.dict == null || (keyForItem = this.GetKeyForItem(item)) == null)
			{
				return base.Items.Contains(item);
			}
			TItem titem;
			return this.dict.TryGetValue(keyForItem, out titem) && EqualityComparer<TItem>.Default.Equals(titem, item);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> is not found in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06005E29 RID: 24105 RVA: 0x00136B4C File Offset: 0x00134D4C
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key) && base.Remove(this.dict[key]);
			}
			if (key != null)
			{
				for (int i = 0; i < base.Items.Count; i++)
				{
					if (this.comparer.Equals(this.GetKeyForItem(base.Items[i]), key))
					{
						this.RemoveItem(i);
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Gets the lookup dictionary of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <returns>The lookup dictionary of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />, if it exists; otherwise, null.</returns>
		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06005E2A RID: 24106 RVA: 0x00136BDA File Offset: 0x00134DDA
		protected IDictionary<TKey, TItem> Dictionary
		{
			get
			{
				return this.dict;
			}
		}

		/// <summary>Changes the key associated with the specified element in the lookup dictionary.</summary>
		/// <param name="item">The element to change the key of.</param>
		/// <param name="newKey">The new key for <paramref name="item" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.-or-<paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is not found.-or-<paramref name="key" /> already exists in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</exception>
		// Token: 0x06005E2B RID: 24107 RVA: 0x00136BE4 File Offset: 0x00134DE4
		protected void ChangeItemKey(TItem item, TKey newKey)
		{
			if (!this.ContainsItem(item))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_ItemNotExist);
			}
			TKey keyForItem = this.GetKeyForItem(item);
			if (!this.comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		// Token: 0x06005E2C RID: 24108 RVA: 0x00136C37 File Offset: 0x00134E37
		protected override void ClearItems()
		{
			base.ClearItems();
			if (this.dict != null)
			{
				this.dict.Clear();
			}
			this.keyCount = 0;
		}

		/// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
		/// <returns>The key for the specified element.</returns>
		/// <param name="item">The element from which to extract the key.</param>
		// Token: 0x06005E2D RID: 24109
		protected abstract TKey GetKeyForItem(TItem item);

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06005E2E RID: 24110 RVA: 0x00136C5C File Offset: 0x00134E5C
		protected override void InsertItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			if (keyForItem != null)
			{
				this.AddKey(keyForItem, item);
			}
			base.InsertItem(index, item);
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <param name="index">The index of the element to remove.</param>
		// Token: 0x06005E2F RID: 24111 RVA: 0x00136C8C File Offset: 0x00134E8C
		protected override void RemoveItem(int index)
		{
			TKey keyForItem = this.GetKeyForItem(base.Items[index]);
			if (keyForItem != null)
			{
				this.RemoveKey(keyForItem);
			}
			base.RemoveItem(index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The zero-based index of the item to be replaced.</param>
		/// <param name="item">The new item.</param>
		// Token: 0x06005E30 RID: 24112 RVA: 0x00136CC4 File Offset: 0x00134EC4
		protected override void SetItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			TKey keyForItem2 = this.GetKeyForItem(base.Items[index]);
			if (this.comparer.Equals(keyForItem2, keyForItem))
			{
				if (keyForItem != null && this.dict != null)
				{
					this.dict[keyForItem] = item;
				}
			}
			else
			{
				if (keyForItem != null)
				{
					this.AddKey(keyForItem, item);
				}
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x00136D44 File Offset: 0x00134F44
		private void AddKey(TKey key, TItem item)
		{
			if (this.dict != null)
			{
				this.dict.Add(key, item);
				return;
			}
			if (this.keyCount == this.threshold)
			{
				this.CreateDictionary();
				this.dict.Add(key, item);
				return;
			}
			if (this.Contains(key))
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
			}
			this.keyCount++;
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x00136DA8 File Offset: 0x00134FA8
		private void CreateDictionary()
		{
			this.dict = new Dictionary<TKey, TItem>(this.comparer);
			foreach (TItem titem in base.Items)
			{
				TKey keyForItem = this.GetKeyForItem(titem);
				if (keyForItem != null)
				{
					this.dict.Add(keyForItem, titem);
				}
			}
		}

		// Token: 0x06005E33 RID: 24115 RVA: 0x00136E1C File Offset: 0x0013501C
		private void RemoveKey(TKey key)
		{
			if (this.dict != null)
			{
				this.dict.Remove(key);
				return;
			}
			this.keyCount--;
		}

		// Token: 0x04002FB6 RID: 12214
		private const int defaultThreshold = 0;

		// Token: 0x04002FB7 RID: 12215
		private IEqualityComparer<TKey> comparer;

		// Token: 0x04002FB8 RID: 12216
		private Dictionary<TKey, TItem> dict;

		// Token: 0x04002FB9 RID: 12217
		private int keyCount;

		// Token: 0x04002FBA RID: 12218
		private int threshold;
	}
}

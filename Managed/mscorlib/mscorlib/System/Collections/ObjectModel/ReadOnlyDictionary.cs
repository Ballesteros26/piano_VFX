using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Unity;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a read-only, generic collection of key/value pairs.</summary>
	/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
	// Token: 0x020009F1 RID: 2545
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<, >))]
	[Serializable]
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> class that is a wrapper around the specified dictionary.</summary>
		/// <param name="dictionary">The dictionary to wrap.</param>
		// Token: 0x06005E54 RID: 24148 RVA: 0x001370A5 File Offset: 0x001352A5
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.m_dictionary = dictionary;
		}

		/// <summary>Gets the dictionary that is wrapped by this <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		/// <returns>The dictionary that is wrapped by this object.</returns>
		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06005E55 RID: 24149 RVA: 0x001370C2 File Offset: 0x001352C2
		protected IDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this.m_dictionary;
			}
		}

		/// <summary>Gets a key collection that contains the keys of the dictionary.</summary>
		/// <returns>A key collection that contains the keys of the dictionary.</returns>
		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06005E56 RID: 24150 RVA: 0x001370CA File Offset: 0x001352CA
		public ReadOnlyDictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				if (this.m_keys == null)
				{
					this.m_keys = new ReadOnlyDictionary<TKey, TValue>.KeyCollection(this.m_dictionary.Keys);
				}
				return this.m_keys;
			}
		}

		/// <summary>Gets a collection that contains the values in the dictionary.</summary>
		/// <returns>A collection that contains the values in the object that implements <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.</returns>
		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06005E57 RID: 24151 RVA: 0x001370F0 File Offset: 0x001352F0
		public ReadOnlyDictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				if (this.m_values == null)
				{
					this.m_values = new ReadOnlyDictionary<TKey, TValue>.ValueCollection(this.m_dictionary.Values);
				}
				return this.m_values;
			}
		}

		/// <summary>Determines whether the dictionary contains an element that has the specified key.</summary>
		/// <returns>true if the dictionary contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the dictionary.</param>
		// Token: 0x06005E58 RID: 24152 RVA: 0x00137116 File Offset: 0x00135316
		public bool ContainsKey(TKey key)
		{
			return this.m_dictionary.ContainsKey(key);
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06005E59 RID: 24153 RVA: 0x00137124 File Offset: 0x00135324
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Retrieves the value that is associated with the specified key.</summary>
		/// <returns>true if the object that implements <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key whose value will be retrieved.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		// Token: 0x06005E5A RID: 24154 RVA: 0x0013712C File Offset: 0x0013532C
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.m_dictionary.TryGetValue(key, out value);
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06005E5B RID: 24155 RVA: 0x0013713B File Offset: 0x0013533B
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets the element that has the specified key.</summary>
		/// <returns>The element that has the specified key.</returns>
		/// <param name="key">The key of the element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found.</exception>
		// Token: 0x170010C3 RID: 4291
		public TValue this[TKey key]
		{
			get
			{
				return this.m_dictionary[key];
			}
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x00136EB5 File Offset: 0x001350B5
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x00136EBE File Offset: 0x001350BE
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		// Token: 0x170010C4 RID: 4292
		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get
			{
				return this.m_dictionary[key];
			}
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		/// <summary>Gets the number of items in the dictionary.</summary>
		/// <returns>The number of items in the dictionary.</returns>
		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06005E61 RID: 24161 RVA: 0x00137151 File Offset: 0x00135351
		public int Count
		{
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x0013715E File Offset: 0x0013535E
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.m_dictionary.Contains(item);
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x0013716C File Offset: 0x0013536C
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.m_dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06005E64 RID: 24164 RVA: 0x00003B29 File Offset: 0x00001D29
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x00136EB5 File Offset: 0x001350B5
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06005E66 RID: 24166 RVA: 0x00136EB5 File Offset: 0x001350B5
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x00136EBE File Offset: 0x001350BE
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06005E68 RID: 24168 RVA: 0x0013717B File Offset: 0x0013537B
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06005E69 RID: 24169 RVA: 0x00137188 File Offset: 0x00135388
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x00137195 File Offset: 0x00135395
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="key">The key of the element to add. </param>
		/// <param name="value">The value of the element to add. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005E6B RID: 24171 RVA: 0x00136EB5 File Offset: 0x001350B5
		void IDictionary.Add(object key, object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005E6C RID: 24172 RVA: 0x00136EB5 File Offset: 0x001350B5
		void IDictionary.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Determines whether the dictionary contains an element that has the specified key.</summary>
		/// <returns>true if the dictionary contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the dictionary.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06005E6D RID: 24173 RVA: 0x001371A9 File Offset: 0x001353A9
		bool IDictionary.Contains(object key)
		{
			return ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Returns an enumerator for the dictionary.</summary>
		/// <returns>An enumerator for the dictionary.</returns>
		// Token: 0x06005E6E RID: 24174 RVA: 0x001371C4 File Offset: 0x001353C4
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			IDictionary dictionary = this.m_dictionary as IDictionary;
			if (dictionary != null)
			{
				return dictionary.GetEnumerator();
			}
			return new ReadOnlyDictionary<TKey, TValue>.DictionaryEnumerator(this.m_dictionary);
		}

		/// <summary>Gets a value that indicates whether the dictionary has a fixed size.</summary>
		/// <returns>true if the dictionary has a fixed size; otherwise, false.</returns>
		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06005E6F RID: 24175 RVA: 0x00003B29 File Offset: 0x00001D29
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06005E70 RID: 24176 RVA: 0x00003B29 File Offset: 0x00001D29
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a collection that contains the keys of the dictionary.</summary>
		/// <returns>A collection that contains the keys of the dictionary.</returns>
		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06005E71 RID: 24177 RVA: 0x00137124 File Offset: 0x00135324
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005E72 RID: 24178 RVA: 0x00136EB5 File Offset: 0x001350B5
		void IDictionary.Remove(object key)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Gets a collection that contains the values in the dictionary.</summary>
		/// <returns>A collection that contains the values in the dictionary.</returns>
		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06005E73 RID: 24179 RVA: 0x0013713B File Offset: 0x0013533B
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets the element that has the specified key.</summary>
		/// <returns>The element that has the specified key.</returns>
		/// <param name="key">The key of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the dictionary has a fixed size. </exception>
		// Token: 0x170010CB RID: 4299
		object IDictionary.this[object key]
		{
			get
			{
				if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					return this[(TKey)((object)key)];
				}
				return null;
			}
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		/// <summary>Copies the elements of the dictionary to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source dictionary is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or- The type of the source dictionary cannot be cast automatically to the type of the destination <paramref name="array" /><paramref name="." /></exception>
		// Token: 0x06005E76 RID: 24182 RVA: 0x00137214 File Offset: 0x00135414
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0 || index > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.m_dictionary.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = this.m_dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TKey, TValue> keyValuePair = enumerator.Current;
						array3[index++] = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair2 in this.m_dictionary)
				{
					array4[index++] = new KeyValuePair<TKey, TValue>(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		/// <summary>Gets a value that indicates whether access to the dictionary is synchronized (thread safe).</summary>
		/// <returns>true if access to the dictionary is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06005E77 RID: 24183 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the dictionary.</summary>
		/// <returns>An object that can be used to synchronize access to the dictionary.</returns>
		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06005E78 RID: 24184 RVA: 0x0013737C File Offset: 0x0013557C
		object ICollection.SyncRoot
		{
			get
			{
				if (this.m_syncRoot == null)
				{
					ICollection collection = this.m_dictionary as ICollection;
					if (collection != null)
					{
						this.m_syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
					}
				}
				return this.m_syncRoot;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06005E79 RID: 24185 RVA: 0x00137124 File Offset: 0x00135324
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06005E7A RID: 24186 RVA: 0x0013713B File Offset: 0x0013533B
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x04002FBD RID: 12221
		private readonly IDictionary<TKey, TValue> m_dictionary;

		// Token: 0x04002FBE RID: 12222
		[NonSerialized]
		private object m_syncRoot;

		// Token: 0x04002FBF RID: 12223
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.KeyCollection m_keys;

		// Token: 0x04002FC0 RID: 12224
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.ValueCollection m_values;

		// Token: 0x020009F2 RID: 2546
		[Serializable]
		private struct DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06005E7B RID: 24187 RVA: 0x001373C6 File Offset: 0x001355C6
			public DictionaryEnumerator(IDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
				this.m_enumerator = this.m_dictionary.GetEnumerator();
			}

			// Token: 0x170010D0 RID: 4304
			// (get) Token: 0x06005E7C RID: 24188 RVA: 0x001373E0 File Offset: 0x001355E0
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this.m_enumerator.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x170010D1 RID: 4305
			// (get) Token: 0x06005E7D RID: 24189 RVA: 0x00137424 File Offset: 0x00135624
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x170010D2 RID: 4306
			// (get) Token: 0x06005E7E RID: 24190 RVA: 0x0013744C File Offset: 0x0013564C
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x170010D3 RID: 4307
			// (get) Token: 0x06005E7F RID: 24191 RVA: 0x00137471 File Offset: 0x00135671
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06005E80 RID: 24192 RVA: 0x0013747E File Offset: 0x0013567E
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06005E81 RID: 24193 RVA: 0x0013748B File Offset: 0x0013568B
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x04002FC1 RID: 12225
			private readonly IDictionary<TKey, TValue> m_dictionary;

			// Token: 0x04002FC2 RID: 12226
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}

		/// <summary>Represents a read-only collection of the keys of a <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		// Token: 0x020009F3 RID: 2547
		[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x06005E82 RID: 24194 RVA: 0x00137498 File Offset: 0x00135698
			internal KeyCollection(ICollection<TKey> collection)
			{
				if (collection == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
				}
				this.m_collection = collection;
			}

			// Token: 0x06005E83 RID: 24195 RVA: 0x00136EB5 File Offset: 0x001350B5
			void ICollection<TKey>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005E84 RID: 24196 RVA: 0x00136EB5 File Offset: 0x001350B5
			void ICollection<TKey>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005E85 RID: 24197 RVA: 0x001374B0 File Offset: 0x001356B0
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this.m_collection.Contains(item);
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06005E86 RID: 24198 RVA: 0x001374BE File Offset: 0x001356BE
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				this.m_collection.CopyTo(array, arrayIndex);
			}

			/// <summary>Gets the number of elements in the collection.</summary>
			/// <returns>The number of elements in the collection.</returns>
			// Token: 0x170010D4 RID: 4308
			// (get) Token: 0x06005E87 RID: 24199 RVA: 0x001374CD File Offset: 0x001356CD
			public int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x170010D5 RID: 4309
			// (get) Token: 0x06005E88 RID: 24200 RVA: 0x00003B29 File Offset: 0x00001D29
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06005E89 RID: 24201 RVA: 0x00136EBE File Offset: 0x001350BE
			bool ICollection<TKey>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				return false;
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06005E8A RID: 24202 RVA: 0x001374DA File Offset: 0x001356DA
			public IEnumerator<TKey> GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06005E8B RID: 24203 RVA: 0x001374E7 File Offset: 0x001356E7
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06005E8C RID: 24204 RVA: 0x001374F4 File Offset: 0x001356F4
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TKey>(this.m_collection, array, index);
			}

			/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
			// Token: 0x170010D6 RID: 4310
			// (get) Token: 0x06005E8D RID: 24205 RVA: 0x00015ED5 File Offset: 0x000140D5
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x170010D7 RID: 4311
			// (get) Token: 0x06005E8E RID: 24206 RVA: 0x00137504 File Offset: 0x00135704
			object ICollection.SyncRoot
			{
				get
				{
					if (this.m_syncRoot == null)
					{
						ICollection collection = this.m_collection as ICollection;
						if (collection != null)
						{
							this.m_syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
						}
					}
					return this.m_syncRoot;
				}
			}

			// Token: 0x06005E8F RID: 24207 RVA: 0x0001FB35 File Offset: 0x0001DD35
			internal KeyCollection()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x04002FC3 RID: 12227
			private readonly ICollection<TKey> m_collection;

			// Token: 0x04002FC4 RID: 12228
			[NonSerialized]
			private object m_syncRoot;
		}

		/// <summary>Represents a read-only collection of the values of a <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		// Token: 0x020009F4 RID: 2548
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06005E90 RID: 24208 RVA: 0x0013754E File Offset: 0x0013574E
			internal ValueCollection(ICollection<TValue> collection)
			{
				if (collection == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
				}
				this.m_collection = collection;
			}

			// Token: 0x06005E91 RID: 24209 RVA: 0x00136EB5 File Offset: 0x001350B5
			void ICollection<TValue>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005E92 RID: 24210 RVA: 0x00136EB5 File Offset: 0x001350B5
			void ICollection<TValue>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06005E93 RID: 24211 RVA: 0x00137566 File Offset: 0x00135766
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this.m_collection.Contains(item);
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06005E94 RID: 24212 RVA: 0x00137574 File Offset: 0x00135774
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				this.m_collection.CopyTo(array, arrayIndex);
			}

			/// <summary>Gets the number of elements in the collection.</summary>
			/// <returns>The number of elements in the collection.</returns>
			// Token: 0x170010D8 RID: 4312
			// (get) Token: 0x06005E95 RID: 24213 RVA: 0x00137583 File Offset: 0x00135783
			public int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x170010D9 RID: 4313
			// (get) Token: 0x06005E96 RID: 24214 RVA: 0x00003B29 File Offset: 0x00001D29
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06005E97 RID: 24215 RVA: 0x00136EBE File Offset: 0x001350BE
			bool ICollection<TValue>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				return false;
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06005E98 RID: 24216 RVA: 0x00137590 File Offset: 0x00135790
			public IEnumerator<TValue> GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06005E99 RID: 24217 RVA: 0x0013759D File Offset: 0x0013579D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06005E9A RID: 24218 RVA: 0x001375AA File Offset: 0x001357AA
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TValue>(this.m_collection, array, index);
			}

			/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
			// Token: 0x170010DA RID: 4314
			// (get) Token: 0x06005E9B RID: 24219 RVA: 0x00015ED5 File Offset: 0x000140D5
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x170010DB RID: 4315
			// (get) Token: 0x06005E9C RID: 24220 RVA: 0x001375BC File Offset: 0x001357BC
			object ICollection.SyncRoot
			{
				get
				{
					if (this.m_syncRoot == null)
					{
						ICollection collection = this.m_collection as ICollection;
						if (collection != null)
						{
							this.m_syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
						}
					}
					return this.m_syncRoot;
				}
			}

			// Token: 0x06005E9D RID: 24221 RVA: 0x0001FB35 File Offset: 0x0001DD35
			internal ValueCollection()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x04002FC5 RID: 12229
			private readonly ICollection<TValue> m_collection;

			// Token: 0x04002FC6 RID: 12230
			[NonSerialized]
			private object m_syncRoot;
		}
	}
}

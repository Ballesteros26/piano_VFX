using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents an object whose members can be dynamically added and removed at run time.</summary>
	// Token: 0x02000323 RID: 803
	public sealed class ExpandoObject : IDynamicMetaObjectProvider, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, INotifyPropertyChanged
	{
		/// <summary>Initializes a new ExpandoObject that does not have members.</summary>
		// Token: 0x06001837 RID: 6199 RVA: 0x0004E9F4 File Offset: 0x0004CBF4
		public ExpandoObject()
		{
			this._data = ExpandoObject.ExpandoData.Empty;
			this.LockObject = new object();
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0004EA14 File Offset: 0x0004CC14
		internal bool TryGetValue(object indexClass, int index, string name, bool ignoreCase, out object value)
		{
			ExpandoObject.ExpandoData data = this._data;
			if (data.Class != indexClass || ignoreCase)
			{
				index = data.Class.GetValueIndex(name, ignoreCase, this);
				if (index == -2)
				{
					throw Error.AmbiguousMatchInExpandoObject(name);
				}
			}
			if (index == -1)
			{
				value = null;
				return false;
			}
			object obj = data[index];
			if (obj == ExpandoObject.Uninitialized)
			{
				value = null;
				return false;
			}
			value = obj;
			return true;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0004EA7C File Offset: 0x0004CC7C
		internal void TrySetValue(object indexClass, int index, object value, string name, bool ignoreCase, bool add)
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData expandoData;
			object obj;
			lock (lockObject)
			{
				expandoData = this._data;
				if (expandoData.Class != indexClass || ignoreCase)
				{
					index = expandoData.Class.GetValueIndex(name, ignoreCase, this);
					if (index == -2)
					{
						throw Error.AmbiguousMatchInExpandoObject(name);
					}
					if (index == -1)
					{
						int num = (ignoreCase ? expandoData.Class.GetValueIndexCaseSensitive(name) : index);
						if (num != -1)
						{
							index = num;
						}
						else
						{
							ExpandoClass expandoClass = expandoData.Class.FindNewClass(name);
							expandoData = this.PromoteClassCore(expandoData.Class, expandoClass);
							index = expandoData.Class.GetValueIndexCaseSensitive(name);
						}
					}
				}
				obj = expandoData[index];
				if (obj == ExpandoObject.Uninitialized)
				{
					this._count++;
				}
				else if (add)
				{
					throw Error.SameKeyExistsInExpando(name);
				}
				expandoData[index] = value;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null && value != obj)
			{
				propertyChanged(this, new PropertyChangedEventArgs(expandoData.Class.Keys[index]));
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0004EB9C File Offset: 0x0004CD9C
		internal bool TryDeleteValue(object indexClass, int index, string name, bool ignoreCase, object deleteValue)
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData data;
			lock (lockObject)
			{
				data = this._data;
				if (data.Class != indexClass || ignoreCase)
				{
					index = data.Class.GetValueIndex(name, ignoreCase, this);
					if (index == -2)
					{
						throw Error.AmbiguousMatchInExpandoObject(name);
					}
				}
				if (index == -1)
				{
					return false;
				}
				object obj = data[index];
				if (obj == ExpandoObject.Uninitialized)
				{
					return false;
				}
				if (deleteValue != ExpandoObject.Uninitialized && !object.Equals(obj, deleteValue))
				{
					return false;
				}
				data[index] = ExpandoObject.Uninitialized;
				this._count--;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(data.Class.Keys[index]));
			}
			return true;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0004EC84 File Offset: 0x0004CE84
		internal bool IsDeletedMember(int index)
		{
			return index != this._data.Length && this._data[index] == ExpandoObject.Uninitialized;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0004ECA9 File Offset: 0x0004CEA9
		internal ExpandoClass Class
		{
			get
			{
				return this._data.Class;
			}
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0004ECB6 File Offset: 0x0004CEB6
		private ExpandoObject.ExpandoData PromoteClassCore(ExpandoClass oldClass, ExpandoClass newClass)
		{
			if (this._data.Class == oldClass)
			{
				this._data = this._data.UpdateClass(newClass);
			}
			return this._data;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0004ECE0 File Offset: 0x0004CEE0
		internal void PromoteClass(object oldClass, object newClass)
		{
			object lockObject = this.LockObject;
			lock (lockObject)
			{
				this.PromoteClassCore((ExpandoClass)oldClass, (ExpandoClass)newClass);
			}
		}

		/// <summary>The provided MetaObject will dispatch to the dynamic virtual methods. The object can be encapsulated inside another MetaObject to provide custom behavior for individual actions.</summary>
		/// <returns>The object of the <see cref="T:System.Dynamic.DynamicMetaObject" /> type.</returns>
		/// <param name="parameter">The expression that represents the MetaObject to dispatch to the Dynamic virtual methods.</param>
		// Token: 0x0600183F RID: 6207 RVA: 0x0004ED30 File Offset: 0x0004CF30
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new ExpandoObject.MetaExpando(parameter, this);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0004ED39 File Offset: 0x0004CF39
		private void TryAddMember(string key, object value)
		{
			ContractUtils.RequiresNotNull(key, "key");
			this.TrySetValue(null, -1, value, key, false, true);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0004ED52 File Offset: 0x0004CF52
		private bool TryGetValueForKey(string key, out object value)
		{
			return this.TryGetValue(null, -1, key, false, out value);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0004ED5F File Offset: 0x0004CF5F
		private bool ExpandoContainsKey(string key)
		{
			return this._data.Class.GetValueIndexCaseSensitive(key) >= 0;
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0004ED78 File Offset: 0x0004CF78
		ICollection<string> IDictionary<string, object>.Keys
		{
			get
			{
				return new ExpandoObject.KeyCollection(this);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0004ED80 File Offset: 0x0004CF80
		ICollection<object> IDictionary<string, object>.Values
		{
			get
			{
				return new ExpandoObject.ValueCollection(this);
			}
		}

		// Token: 0x17000444 RID: 1092
		object IDictionary<string, object>.this[string key]
		{
			get
			{
				object obj;
				if (!this.TryGetValueForKey(key, out obj))
				{
					throw Error.KeyDoesNotExistInExpando(key);
				}
				return obj;
			}
			set
			{
				ContractUtils.RequiresNotNull(key, "key");
				this.TrySetValue(null, -1, value, key, false, false);
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0004EDC1 File Offset: 0x0004CFC1
		void IDictionary<string, object>.Add(string key, object value)
		{
			this.TryAddMember(key, value);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0004EDCC File Offset: 0x0004CFCC
		bool IDictionary<string, object>.ContainsKey(string key)
		{
			ContractUtils.RequiresNotNull(key, "key");
			ExpandoObject.ExpandoData data = this._data;
			int valueIndexCaseSensitive = data.Class.GetValueIndexCaseSensitive(key);
			return valueIndexCaseSensitive >= 0 && data[valueIndexCaseSensitive] != ExpandoObject.Uninitialized;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0004EE0F File Offset: 0x0004D00F
		bool IDictionary<string, object>.Remove(string key)
		{
			ContractUtils.RequiresNotNull(key, "key");
			return this.TryDeleteValue(null, -1, key, false, ExpandoObject.Uninitialized);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0004EE2B File Offset: 0x0004D02B
		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			return this.TryGetValueForKey(key, out value);
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0004EE35 File Offset: 0x0004D035
		int ICollection<KeyValuePair<string, object>>.Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00002285 File Offset: 0x00000485
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0004EE3D File Offset: 0x0004D03D
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			this.TryAddMember(item.Key, item.Value);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0004EE54 File Offset: 0x0004D054
		void ICollection<KeyValuePair<string, object>>.Clear()
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData data;
			lock (lockObject)
			{
				data = this._data;
				this._data = ExpandoObject.ExpandoData.Empty;
				this._count = 0;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null)
			{
				int i = 0;
				int num = data.Class.Keys.Length;
				while (i < num)
				{
					if (data[i] != ExpandoObject.Uninitialized)
					{
						propertyChanged(this, new PropertyChangedEventArgs(data.Class.Keys[i]));
					}
					i++;
				}
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0004EEFC File Offset: 0x0004D0FC
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			object obj;
			return this.TryGetValueForKey(item.Key, out obj) && object.Equals(obj, item.Value);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0004EF2C File Offset: 0x0004D12C
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			ContractUtils.RequiresNotNull(array, "array");
			object lockObject = this.LockObject;
			lock (lockObject)
			{
				ContractUtils.RequiresArrayRange<KeyValuePair<string, object>>(array, arrayIndex, this._count, "arrayIndex", "Count");
				foreach (KeyValuePair<string, object> keyValuePair in ((IEnumerable<KeyValuePair<string, object>>)this))
				{
					array[arrayIndex++] = keyValuePair;
				}
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0004EFC4 File Offset: 0x0004D1C4
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return this.TryDeleteValue(null, -1, item.Key, false, item.Value);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			ExpandoObject.ExpandoData data = this._data;
			return this.GetExpandoEnumerator(data, data.Version);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001853 RID: 6227 RVA: 0x0004F004 File Offset: 0x0004D204
		IEnumerator IEnumerable.GetEnumerator()
		{
			ExpandoObject.ExpandoData data = this._data;
			return this.GetExpandoEnumerator(data, data.Version);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0004F025 File Offset: 0x0004D225
		private IEnumerator<KeyValuePair<string, object>> GetExpandoEnumerator(ExpandoObject.ExpandoData data, int version)
		{
			int num;
			for (int i = 0; i < data.Class.Keys.Length; i = num + 1)
			{
				if (this._data.Version != version || data != this._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				object obj = data[i];
				if (obj != ExpandoObject.Uninitialized)
				{
					yield return new KeyValuePair<string, object>(data.Class.Keys[i], obj);
				}
				num = i;
			}
			yield break;
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06001855 RID: 6229 RVA: 0x0004F042 File Offset: 0x0004D242
		// (remove) Token: 0x06001856 RID: 6230 RVA: 0x0004F05B File Offset: 0x0004D25B
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this._propertyChanged = (PropertyChangedEventHandler)Delegate.Combine(this._propertyChanged, value);
			}
			remove
			{
				this._propertyChanged = (PropertyChangedEventHandler)Delegate.Remove(this._propertyChanged, value);
			}
		}

		// Token: 0x04000B07 RID: 2823
		private static readonly MethodInfo ExpandoTryGetValue = typeof(RuntimeOps).GetMethod("ExpandoTryGetValue");

		// Token: 0x04000B08 RID: 2824
		private static readonly MethodInfo ExpandoTrySetValue = typeof(RuntimeOps).GetMethod("ExpandoTrySetValue");

		// Token: 0x04000B09 RID: 2825
		private static readonly MethodInfo ExpandoTryDeleteValue = typeof(RuntimeOps).GetMethod("ExpandoTryDeleteValue");

		// Token: 0x04000B0A RID: 2826
		private static readonly MethodInfo ExpandoPromoteClass = typeof(RuntimeOps).GetMethod("ExpandoPromoteClass");

		// Token: 0x04000B0B RID: 2827
		private static readonly MethodInfo ExpandoCheckVersion = typeof(RuntimeOps).GetMethod("ExpandoCheckVersion");

		// Token: 0x04000B0C RID: 2828
		internal readonly object LockObject;

		// Token: 0x04000B0D RID: 2829
		private ExpandoObject.ExpandoData _data;

		// Token: 0x04000B0E RID: 2830
		private int _count;

		// Token: 0x04000B0F RID: 2831
		internal static readonly object Uninitialized = new object();

		// Token: 0x04000B10 RID: 2832
		internal const int AmbiguousMatchFound = -2;

		// Token: 0x04000B11 RID: 2833
		internal const int NoMatch = -1;

		// Token: 0x04000B12 RID: 2834
		private PropertyChangedEventHandler _propertyChanged;

		// Token: 0x02000324 RID: 804
		private sealed class KeyCollectionDebugView
		{
			// Token: 0x06001858 RID: 6232 RVA: 0x0004F108 File Offset: 0x0004D308
			public KeyCollectionDebugView(ICollection<string> collection)
			{
				ContractUtils.RequiresNotNull(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x17000447 RID: 1095
			// (get) Token: 0x06001859 RID: 6233 RVA: 0x0004F124 File Offset: 0x0004D324
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public string[] Items
			{
				get
				{
					string[] array = new string[this._collection.Count];
					this._collection.CopyTo(array, 0);
					return array;
				}
			}

			// Token: 0x04000B13 RID: 2835
			private readonly ICollection<string> _collection;
		}

		// Token: 0x02000325 RID: 805
		[DebuggerTypeProxy(typeof(ExpandoObject.KeyCollectionDebugView))]
		[DebuggerDisplay("Count = {Count}")]
		private class KeyCollection : ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x0600185A RID: 6234 RVA: 0x0004F150 File Offset: 0x0004D350
			internal KeyCollection(ExpandoObject expando)
			{
				object lockObject = expando.LockObject;
				lock (lockObject)
				{
					this._expando = expando;
					this._expandoVersion = expando._data.Version;
					this._expandoCount = expando._count;
					this._expandoData = expando._data;
				}
			}

			// Token: 0x0600185B RID: 6235 RVA: 0x0004F1C0 File Offset: 0x0004D3C0
			private void CheckVersion()
			{
				if (this._expando._data.Version != this._expandoVersion || this._expandoData != this._expando._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
			}

			// Token: 0x0600185C RID: 6236 RVA: 0x0004F1F3 File Offset: 0x0004D3F3
			public void Add(string item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x0600185D RID: 6237 RVA: 0x0004F1F3 File Offset: 0x0004D3F3
			public void Clear()
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x0600185E RID: 6238 RVA: 0x0004F1FC File Offset: 0x0004D3FC
			public bool Contains(string item)
			{
				object lockObject = this._expando.LockObject;
				bool flag2;
				lock (lockObject)
				{
					this.CheckVersion();
					flag2 = this._expando.ExpandoContainsKey(item);
				}
				return flag2;
			}

			// Token: 0x0600185F RID: 6239 RVA: 0x0004F250 File Offset: 0x0004D450
			public void CopyTo(string[] array, int arrayIndex)
			{
				ContractUtils.RequiresNotNull(array, "array");
				ContractUtils.RequiresArrayRange<string>(array, arrayIndex, this._expandoCount, "arrayIndex", "Count");
				object lockObject = this._expando.LockObject;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (data[i] != ExpandoObject.Uninitialized)
						{
							array[arrayIndex++] = data.Class.Keys[i];
						}
					}
				}
			}

			// Token: 0x17000448 RID: 1096
			// (get) Token: 0x06001860 RID: 6240 RVA: 0x0004F300 File Offset: 0x0004D500
			public int Count
			{
				get
				{
					this.CheckVersion();
					return this._expandoCount;
				}
			}

			// Token: 0x17000449 RID: 1097
			// (get) Token: 0x06001861 RID: 6241 RVA: 0x0000AA13 File Offset: 0x00008C13
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001862 RID: 6242 RVA: 0x0004F1F3 File Offset: 0x0004D3F3
			public bool Remove(string item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001863 RID: 6243 RVA: 0x0004F30E File Offset: 0x0004D50E
			public IEnumerator<string> GetEnumerator()
			{
				int i = 0;
				int j = this._expandoData.Class.Keys.Length;
				while (i < j)
				{
					this.CheckVersion();
					if (this._expandoData[i] != ExpandoObject.Uninitialized)
					{
						yield return this._expandoData.Class.Keys[i];
					}
					int num = i;
					i = num + 1;
				}
				yield break;
			}

			// Token: 0x06001864 RID: 6244 RVA: 0x0004F31D File Offset: 0x0004D51D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000B14 RID: 2836
			private readonly ExpandoObject _expando;

			// Token: 0x04000B15 RID: 2837
			private readonly int _expandoVersion;

			// Token: 0x04000B16 RID: 2838
			private readonly int _expandoCount;

			// Token: 0x04000B17 RID: 2839
			private readonly ExpandoObject.ExpandoData _expandoData;
		}

		// Token: 0x02000327 RID: 807
		private sealed class ValueCollectionDebugView
		{
			// Token: 0x0600186B RID: 6251 RVA: 0x0004F3F2 File Offset: 0x0004D5F2
			public ValueCollectionDebugView(ICollection<object> collection)
			{
				ContractUtils.RequiresNotNull(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x1700044C RID: 1100
			// (get) Token: 0x0600186C RID: 6252 RVA: 0x0004F40C File Offset: 0x0004D60C
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					object[] array = new object[this._collection.Count];
					this._collection.CopyTo(array, 0);
					return array;
				}
			}

			// Token: 0x04000B1D RID: 2845
			private readonly ICollection<object> _collection;
		}

		// Token: 0x02000328 RID: 808
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ExpandoObject.ValueCollectionDebugView))]
		private class ValueCollection : ICollection<object>, IEnumerable<object>, IEnumerable
		{
			// Token: 0x0600186D RID: 6253 RVA: 0x0004F438 File Offset: 0x0004D638
			internal ValueCollection(ExpandoObject expando)
			{
				object lockObject = expando.LockObject;
				lock (lockObject)
				{
					this._expando = expando;
					this._expandoVersion = expando._data.Version;
					this._expandoCount = expando._count;
					this._expandoData = expando._data;
				}
			}

			// Token: 0x0600186E RID: 6254 RVA: 0x0004F4A8 File Offset: 0x0004D6A8
			private void CheckVersion()
			{
				if (this._expando._data.Version != this._expandoVersion || this._expandoData != this._expando._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
			}

			// Token: 0x0600186F RID: 6255 RVA: 0x0004F1F3 File Offset: 0x0004D3F3
			public void Add(object item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001870 RID: 6256 RVA: 0x0004F1F3 File Offset: 0x0004D3F3
			public void Clear()
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001871 RID: 6257 RVA: 0x0004F4DC File Offset: 0x0004D6DC
			public bool Contains(object item)
			{
				object lockObject = this._expando.LockObject;
				bool flag2;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (object.Equals(data[i], item))
						{
							return true;
						}
					}
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x06001872 RID: 6258 RVA: 0x0004F560 File Offset: 0x0004D760
			public void CopyTo(object[] array, int arrayIndex)
			{
				ContractUtils.RequiresNotNull(array, "array");
				ContractUtils.RequiresArrayRange<object>(array, arrayIndex, this._expandoCount, "arrayIndex", "Count");
				object lockObject = this._expando.LockObject;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (data[i] != ExpandoObject.Uninitialized)
						{
							array[arrayIndex++] = data[i];
						}
					}
				}
			}

			// Token: 0x1700044D RID: 1101
			// (get) Token: 0x06001873 RID: 6259 RVA: 0x0004F608 File Offset: 0x0004D808
			public int Count
			{
				get
				{
					this.CheckVersion();
					return this._expandoCount;
				}
			}

			// Token: 0x1700044E RID: 1102
			// (get) Token: 0x06001874 RID: 6260 RVA: 0x0000AA13 File Offset: 0x00008C13
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001875 RID: 6261 RVA: 0x0004F1F3 File Offset: 0x0004D3F3
			public bool Remove(object item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001876 RID: 6262 RVA: 0x0004F616 File Offset: 0x0004D816
			public IEnumerator<object> GetEnumerator()
			{
				ExpandoObject.ExpandoData data = this._expando._data;
				int num;
				for (int i = 0; i < data.Class.Keys.Length; i = num + 1)
				{
					this.CheckVersion();
					object obj = data[i];
					if (obj != ExpandoObject.Uninitialized)
					{
						yield return obj;
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06001877 RID: 6263 RVA: 0x0004F625 File Offset: 0x0004D825
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000B1E RID: 2846
			private readonly ExpandoObject _expando;

			// Token: 0x04000B1F RID: 2847
			private readonly int _expandoVersion;

			// Token: 0x04000B20 RID: 2848
			private readonly int _expandoCount;

			// Token: 0x04000B21 RID: 2849
			private readonly ExpandoObject.ExpandoData _expandoData;
		}

		// Token: 0x0200032A RID: 810
		private class MetaExpando : DynamicMetaObject
		{
			// Token: 0x0600187E RID: 6270 RVA: 0x0004DBC7 File Offset: 0x0004BDC7
			public MetaExpando(Expression expression, ExpandoObject value)
				: base(expression, BindingRestrictions.Empty, value)
			{
			}

			// Token: 0x0600187F RID: 6271 RVA: 0x0004F6EC File Offset: 0x0004D8EC
			private DynamicMetaObject BindGetOrInvokeMember(DynamicMetaObjectBinder binder, string name, bool ignoreCase, DynamicMetaObject fallback, Func<DynamicMetaObject, DynamicMetaObject> fallbackInvoke)
			{
				ExpandoClass @class = this.Value.Class;
				int valueIndex = @class.GetValueIndex(name, ignoreCase, this.Value);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "value");
				Expression expression = Expression.Call(ExpandoObject.ExpandoTryGetValue, new Expression[]
				{
					this.GetLimitedSelf(),
					Expression.Constant(@class, typeof(object)),
					Utils.Constant(valueIndex),
					Expression.Constant(name),
					Utils.Constant(ignoreCase),
					parameterExpression
				});
				DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
				if (fallbackInvoke != null)
				{
					dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
				}
				dynamicMetaObject = new DynamicMetaObject(Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[] { Expression.Condition(expression, dynamicMetaObject.Expression, fallback.Expression, typeof(object)) })), dynamicMetaObject.Restrictions.Merge(fallback.Restrictions));
				return this.AddDynamicTestAndDefer(binder, this.Value.Class, null, dynamicMetaObject);
			}

			// Token: 0x06001880 RID: 6272 RVA: 0x0004F800 File Offset: 0x0004DA00
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				return this.BindGetOrInvokeMember(binder, binder.Name, binder.IgnoreCase, binder.FallbackGetMember(this), null);
			}

			// Token: 0x06001881 RID: 6273 RVA: 0x0004F828 File Offset: 0x0004DA28
			public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				return this.BindGetOrInvokeMember(binder, binder.Name, binder.IgnoreCase, binder.FallbackInvokeMember(this, args), (DynamicMetaObject value) => binder.FallbackInvoke(value, args, null));
			}

			// Token: 0x06001882 RID: 6274 RVA: 0x0004F89C File Offset: 0x0004DA9C
			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				ContractUtils.RequiresNotNull(value, "value");
				ExpandoClass expandoClass;
				int num;
				ExpandoClass classEnsureIndex = this.GetClassEnsureIndex(binder.Name, binder.IgnoreCase, this.Value, out expandoClass, out num);
				return this.AddDynamicTestAndDefer(binder, expandoClass, classEnsureIndex, new DynamicMetaObject(Expression.Call(ExpandoObject.ExpandoTrySetValue, new Expression[]
				{
					this.GetLimitedSelf(),
					Expression.Constant(expandoClass, typeof(object)),
					Utils.Constant(num),
					Expression.Convert(value.Expression, typeof(object)),
					Expression.Constant(binder.Name),
					Utils.Constant(binder.IgnoreCase)
				}), BindingRestrictions.Empty));
			}

			// Token: 0x06001883 RID: 6275 RVA: 0x0004F958 File Offset: 0x0004DB58
			public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				int valueIndex = this.Value.Class.GetValueIndex(binder.Name, binder.IgnoreCase, this.Value);
				Expression expression = Expression.Call(ExpandoObject.ExpandoTryDeleteValue, this.GetLimitedSelf(), Expression.Constant(this.Value.Class, typeof(object)), Utils.Constant(valueIndex), Expression.Constant(binder.Name), Utils.Constant(binder.IgnoreCase));
				DynamicMetaObject dynamicMetaObject = binder.FallbackDeleteMember(this);
				DynamicMetaObject dynamicMetaObject2 = new DynamicMetaObject(Expression.IfThen(Expression.Not(expression), dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
				return this.AddDynamicTestAndDefer(binder, this.Value.Class, null, dynamicMetaObject2);
			}

			// Token: 0x06001884 RID: 6276 RVA: 0x0004FA11 File Offset: 0x0004DC11
			public override IEnumerable<string> GetDynamicMemberNames()
			{
				ExpandoObject.ExpandoData expandoData = this.Value._data;
				ExpandoClass klass = expandoData.Class;
				int num;
				for (int i = 0; i < klass.Keys.Length; i = num + 1)
				{
					if (expandoData[i] != ExpandoObject.Uninitialized)
					{
						yield return klass.Keys[i];
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06001885 RID: 6277 RVA: 0x0004FA24 File Offset: 0x0004DC24
			private DynamicMetaObject AddDynamicTestAndDefer(DynamicMetaObjectBinder binder, ExpandoClass klass, ExpandoClass originalClass, DynamicMetaObject succeeds)
			{
				Expression expression = succeeds.Expression;
				if (originalClass != null)
				{
					expression = Expression.Block(Expression.Call(null, ExpandoObject.ExpandoPromoteClass, this.GetLimitedSelf(), Expression.Constant(originalClass, typeof(object)), Expression.Constant(klass, typeof(object))), succeeds.Expression);
				}
				return new DynamicMetaObject(Expression.Condition(Expression.Call(null, ExpandoObject.ExpandoCheckVersion, this.GetLimitedSelf(), Expression.Constant(originalClass ?? klass, typeof(object))), expression, binder.GetUpdateExpression(expression.Type)), this.GetRestrictions().Merge(succeeds.Restrictions));
			}

			// Token: 0x06001886 RID: 6278 RVA: 0x0004FACC File Offset: 0x0004DCCC
			private ExpandoClass GetClassEnsureIndex(string name, bool caseInsensitive, ExpandoObject obj, out ExpandoClass klass, out int index)
			{
				ExpandoClass @class = this.Value.Class;
				index = @class.GetValueIndex(name, caseInsensitive, obj);
				if (index == -2)
				{
					klass = @class;
					return null;
				}
				if (index == -1)
				{
					ExpandoClass expandoClass = @class.FindNewClass(name);
					klass = expandoClass;
					index = expandoClass.GetValueIndexCaseSensitive(name);
					return @class;
				}
				klass = @class;
				return null;
			}

			// Token: 0x06001887 RID: 6279 RVA: 0x0004FB21 File Offset: 0x0004DD21
			private Expression GetLimitedSelf()
			{
				if (TypeUtils.AreEquivalent(base.Expression.Type, base.LimitType))
				{
					return base.Expression;
				}
				return Expression.Convert(base.Expression, base.LimitType);
			}

			// Token: 0x06001888 RID: 6280 RVA: 0x0004E66E File Offset: 0x0004C86E
			private BindingRestrictions GetRestrictions()
			{
				return BindingRestrictions.GetTypeRestriction(this);
			}

			// Token: 0x17000451 RID: 1105
			// (get) Token: 0x06001889 RID: 6281 RVA: 0x0004FB53 File Offset: 0x0004DD53
			public new ExpandoObject Value
			{
				get
				{
					return (ExpandoObject)base.Value;
				}
			}
		}

		// Token: 0x0200032D RID: 813
		private class ExpandoData
		{
			// Token: 0x17000454 RID: 1108
			internal object this[int index]
			{
				get
				{
					return this._dataArray[index];
				}
				set
				{
					this._version++;
					this._dataArray[index] = value;
				}
			}

			// Token: 0x17000455 RID: 1109
			// (get) Token: 0x06001896 RID: 6294 RVA: 0x0004FCC2 File Offset: 0x0004DEC2
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17000456 RID: 1110
			// (get) Token: 0x06001897 RID: 6295 RVA: 0x0004FCCA File Offset: 0x0004DECA
			internal int Length
			{
				get
				{
					return this._dataArray.Length;
				}
			}

			// Token: 0x06001898 RID: 6296 RVA: 0x0004FCD4 File Offset: 0x0004DED4
			private ExpandoData()
			{
				this.Class = ExpandoClass.Empty;
				this._dataArray = Array.Empty<object>();
			}

			// Token: 0x06001899 RID: 6297 RVA: 0x0004FCF2 File Offset: 0x0004DEF2
			internal ExpandoData(ExpandoClass klass, object[] data, int version)
			{
				this.Class = klass;
				this._dataArray = data;
				this._version = version;
			}

			// Token: 0x0600189A RID: 6298 RVA: 0x0004FD10 File Offset: 0x0004DF10
			internal ExpandoObject.ExpandoData UpdateClass(ExpandoClass newClass)
			{
				if (this._dataArray.Length >= newClass.Keys.Length)
				{
					this[newClass.Keys.Length - 1] = ExpandoObject.Uninitialized;
					return new ExpandoObject.ExpandoData(newClass, this._dataArray, this._version);
				}
				int num = this._dataArray.Length;
				object[] array = new object[ExpandoObject.ExpandoData.GetAlignedSize(newClass.Keys.Length)];
				Array.Copy(this._dataArray, 0, array, 0, this._dataArray.Length);
				ExpandoObject.ExpandoData expandoData = new ExpandoObject.ExpandoData(newClass, array, this._version);
				expandoData[num] = ExpandoObject.Uninitialized;
				return expandoData;
			}

			// Token: 0x0600189B RID: 6299 RVA: 0x0004FDA2 File Offset: 0x0004DFA2
			private static int GetAlignedSize(int len)
			{
				return (len + 7) & -8;
			}

			// Token: 0x04000B30 RID: 2864
			internal static ExpandoObject.ExpandoData Empty = new ExpandoObject.ExpandoData();

			// Token: 0x04000B31 RID: 2865
			internal readonly ExpandoClass Class;

			// Token: 0x04000B32 RID: 2866
			private readonly object[] _dataArray;

			// Token: 0x04000B33 RID: 2867
			private int _version;
		}
	}
}

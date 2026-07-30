using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000791 RID: 1937
	[ComVisible(true)]
	internal class AggregateDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06004F53 RID: 20307 RVA: 0x0011D9C0 File Offset: 0x0011BBC0
		public AggregateDictionary(IDictionary[] dics)
		{
			this.dictionaries = dics;
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D3E RID: 3390
		public object this[object key]
		{
			get
			{
				foreach (IDictionary dictionary in this.dictionaries)
				{
					if (dictionary.Contains(key))
					{
						return dictionary[key];
					}
				}
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06004F58 RID: 20312 RVA: 0x0011DA08 File Offset: 0x0011BC08
		public ICollection Keys
		{
			get
			{
				if (this._keys != null)
				{
					return this._keys;
				}
				this._keys = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					this._keys.AddRange(dictionary.Keys);
				}
				return this._keys;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06004F59 RID: 20313 RVA: 0x0011DA60 File Offset: 0x0011BC60
		public ICollection Values
		{
			get
			{
				if (this._values != null)
				{
					return this._values;
				}
				this._values = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					this._values.AddRange(dictionary.Values);
				}
				return this._values;
			}
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x00014B5A File Offset: 0x00012D5A
		public void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x00014B5A File Offset: 0x00012D5A
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x0011DAB8 File Offset: 0x0011BCB8
		public bool Contains(object ob)
		{
			IDictionary[] array = this.dictionaries;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains(ob))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x0011DAE8 File Offset: 0x0011BCE8
		public IDictionaryEnumerator GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x0011DAE8 File Offset: 0x0011BCE8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00014B5A File Offset: 0x00012D5A
		public void Remove(object ob)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x0011DAF8 File Offset: 0x0011BCF8
		public void CopyTo(Array array, int index)
		{
			foreach (object obj in this)
			{
				array.SetValue(obj, index++);
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06004F61 RID: 20321 RVA: 0x0011DB50 File Offset: 0x0011BD50
		public int Count
		{
			get
			{
				int num = 0;
				foreach (IDictionary dictionary in this.dictionaries)
				{
					num += dictionary.Count;
				}
				return num;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06004F62 RID: 20322 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06004F63 RID: 20323 RVA: 0x00002119 File Offset: 0x00000319
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04002A3D RID: 10813
		private IDictionary[] dictionaries;

		// Token: 0x04002A3E RID: 10814
		private ArrayList _values;

		// Token: 0x04002A3F RID: 10815
		private ArrayList _keys;
	}
}

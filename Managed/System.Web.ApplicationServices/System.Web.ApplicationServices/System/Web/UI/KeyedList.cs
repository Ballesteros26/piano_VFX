using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x0200000A RID: 10
	internal class KeyedList : IOrderedDictionary, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002078 File Offset: 0x00000278
		public void Add(object key, object value)
		{
			this.objectTable.Add(key, value);
			this.objectList.Add(new DictionaryEntry(key, value));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000209F File Offset: 0x0000029F
		public void Clear()
		{
			this.objectTable.Clear();
			this.objectList.Clear();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020B7 File Offset: 0x000002B7
		public bool Contains(object key)
		{
			return this.objectTable.Contains(key);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020C5 File Offset: 0x000002C5
		public void CopyTo(Array array, int idx)
		{
			this.objectTable.CopyTo(array, idx);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Insert(int idx, object key, object value)
		{
			if (idx > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.objectTable.Add(key, value);
			this.objectList.Insert(idx, new DictionaryEntry(key, value));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002110 File Offset: 0x00000310
		public void Remove(object key)
		{
			this.objectTable.Remove(key);
			int num = this.IndexOf(key);
			if (num >= 0)
			{
				this.objectList.RemoveAt(num);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002144 File Offset: 0x00000344
		public void RemoveAt(int idx)
		{
			if (idx >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.objectTable.Remove(((DictionaryEntry)this.objectList[idx]).Key);
			this.objectList.RemoveAt(idx);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002195 File Offset: 0x00000395
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new KeyedListEnumerator(this.objectList);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002195 File Offset: 0x00000395
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			return new KeyedListEnumerator(this.objectList);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002195 File Offset: 0x00000395
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new KeyedListEnumerator(this.objectList);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021A2 File Offset: 0x000003A2
		public int Count
		{
			get
			{
				return this.objectList.Count;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021AF File Offset: 0x000003AF
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021AF File Offset: 0x000003AF
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021AF File Offset: 0x000003AF
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000006 RID: 6
		public object this[int idx]
		{
			get
			{
				return ((DictionaryEntry)this.objectList[idx]).Value;
			}
			set
			{
				if (idx < 0 || idx >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				object key = ((DictionaryEntry)this.objectList[idx]).Key;
				this.objectList[idx] = new DictionaryEntry(key, value);
				this.objectTable[key] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		public object this[object key]
		{
			get
			{
				return this.objectTable[key];
			}
			set
			{
				if (this.objectTable.Contains(key))
				{
					this.objectTable[key] = value;
					this.objectTable[this.IndexOf(key)] = new DictionaryEntry(key, value);
					return;
				}
				this.Add(key, value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022A4 File Offset: 0x000004A4
		public ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this.objectList.Count; i++)
				{
					arrayList.Add(((DictionaryEntry)this.objectList[i]).Key);
				}
				return arrayList;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022F0 File Offset: 0x000004F0
		public ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this.objectList.Count; i++)
				{
					arrayList.Add(((DictionaryEntry)this.objectList[i]).Value);
				}
				return arrayList;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000233A File Offset: 0x0000053A
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002340 File Offset: 0x00000540
		private int IndexOf(object key)
		{
			for (int i = 0; i < this.objectList.Count; i++)
			{
				if (((DictionaryEntry)this.objectList[i]).Key.Equals(key))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000040 RID: 64
		private Hashtable objectTable = new Hashtable();

		// Token: 0x04000041 RID: 65
		private ArrayList objectList = new ArrayList();
	}
}

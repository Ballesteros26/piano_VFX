using System;
using System.Threading;

namespace System.Collections
{
	// Token: 0x020009D7 RID: 2519
	[Serializable]
	internal class ListDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x17001061 RID: 4193
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
				{
					if (next.key.Equals(key))
					{
						return next.value;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				if (!key.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "key");
				}
				if (value != null && !value.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "value");
				}
				this.version++;
				ListDictionaryInternal.DictionaryNode dictionaryNode = null;
				ListDictionaryInternal.DictionaryNode next = this.head;
				while (next != null && !next.key.Equals(key))
				{
					dictionaryNode = next;
					next = next.next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06005D13 RID: 23827 RVA: 0x001335C7 File Offset: 0x001317C7
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06005D14 RID: 23828 RVA: 0x001335CF File Offset: 0x001317CF
		public ICollection Keys
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, true);
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06005D15 RID: 23829 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06005D16 RID: 23830 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06005D17 RID: 23831 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06005D18 RID: 23832 RVA: 0x001335D8 File Offset: 0x001317D8
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06005D19 RID: 23833 RVA: 0x001335FA File Offset: 0x001317FA
		public ICollection Values
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, false);
			}
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x00133604 File Offset: 0x00131804
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			if (!key.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "key");
			}
			if (value != null && !value.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "value");
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					throw new ArgumentException(Environment.GetResourceString("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", new object[] { next.key, key }));
				}
				dictionaryNode = next;
			}
			if (next != null)
			{
				next.value = value;
				return;
			}
			ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x00133706 File Offset: 0x00131906
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x00133724 File Offset: 0x00131924
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x00133770 File Offset: 0x00131970
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."), "index");
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x00133817 File Offset: 0x00131A17
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x00133817 File Offset: 0x00131A17
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06005D20 RID: 23840 RVA: 0x00133820 File Offset: 0x00131A20
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next = this.head;
			while (next != null && !next.key.Equals(key))
			{
				dictionaryNode = next;
				next = next.next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x04002F68 RID: 12136
		private ListDictionaryInternal.DictionaryNode head;

		// Token: 0x04002F69 RID: 12137
		private int version;

		// Token: 0x04002F6A RID: 12138
		private int count;

		// Token: 0x04002F6B RID: 12139
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x020009D8 RID: 2520
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06005D21 RID: 23841 RVA: 0x001338AD File Offset: 0x00131AAD
			public NodeEnumerator(ListDictionaryInternal list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x17001069 RID: 4201
			// (get) Token: 0x06005D22 RID: 23842 RVA: 0x001338D6 File Offset: 0x00131AD6
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x1700106A RID: 4202
			// (get) Token: 0x06005D23 RID: 23843 RVA: 0x001338E3 File Offset: 0x00131AE3
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x1700106B RID: 4203
			// (get) Token: 0x06005D24 RID: 23844 RVA: 0x00133918 File Offset: 0x00131B18
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					return this.current.key;
				}
			}

			// Token: 0x1700106C RID: 4204
			// (get) Token: 0x06005D25 RID: 23845 RVA: 0x0013393D File Offset: 0x00131B3D
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					return this.current.value;
				}
			}

			// Token: 0x06005D26 RID: 23846 RVA: 0x00133964 File Offset: 0x00131B64
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x06005D27 RID: 23847 RVA: 0x001339D8 File Offset: 0x00131BD8
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x04002F6C RID: 12140
			private ListDictionaryInternal list;

			// Token: 0x04002F6D RID: 12141
			private ListDictionaryInternal.DictionaryNode current;

			// Token: 0x04002F6E RID: 12142
			private int version;

			// Token: 0x04002F6F RID: 12143
			private bool start;
		}

		// Token: 0x020009D9 RID: 2521
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06005D28 RID: 23848 RVA: 0x00133A0B File Offset: 0x00131C0B
			public NodeKeyValueCollection(ListDictionaryInternal list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x06005D29 RID: 23849 RVA: 0x00133A24 File Offset: 0x00131C24
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
				}
				if (array.Length - index < this.list.Count)
				{
					throw new ArgumentException(Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."), "index");
				}
				for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x1700106D RID: 4205
			// (get) Token: 0x06005D2A RID: 23850 RVA: 0x00133AD8 File Offset: 0x00131CD8
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x1700106E RID: 4206
			// (get) Token: 0x06005D2B RID: 23851 RVA: 0x00015ED5 File Offset: 0x000140D5
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700106F RID: 4207
			// (get) Token: 0x06005D2C RID: 23852 RVA: 0x00133B04 File Offset: 0x00131D04
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x06005D2D RID: 23853 RVA: 0x00133B11 File Offset: 0x00131D11
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionaryInternal.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x04002F70 RID: 12144
			private ListDictionaryInternal list;

			// Token: 0x04002F71 RID: 12145
			private bool isKeys;

			// Token: 0x020009DA RID: 2522
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x06005D2E RID: 23854 RVA: 0x00133B24 File Offset: 0x00131D24
				public NodeKeyValueEnumerator(ListDictionaryInternal list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x17001070 RID: 4208
				// (get) Token: 0x06005D2F RID: 23855 RVA: 0x00133B54 File Offset: 0x00131D54
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x06005D30 RID: 23856 RVA: 0x00133B90 File Offset: 0x00131D90
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x06005D31 RID: 23857 RVA: 0x00133C04 File Offset: 0x00131E04
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04002F72 RID: 12146
				private ListDictionaryInternal list;

				// Token: 0x04002F73 RID: 12147
				private ListDictionaryInternal.DictionaryNode current;

				// Token: 0x04002F74 RID: 12148
				private int version;

				// Token: 0x04002F75 RID: 12149
				private bool isKeys;

				// Token: 0x04002F76 RID: 12150
				private bool start;
			}
		}

		// Token: 0x020009DB RID: 2523
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x04002F77 RID: 12151
			public object key;

			// Token: 0x04002F78 RID: 12152
			public object value;

			// Token: 0x04002F79 RID: 12153
			public ListDictionaryInternal.DictionaryNode next;
		}
	}
}

using System;

namespace System.Collections
{
	// Token: 0x020009C1 RID: 2497
	[Serializable]
	internal sealed class EmptyReadOnlyDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005C61 RID: 23649 RVA: 0x00131515 File Offset: 0x0012F715
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x0013151C File Offset: 0x0012F71C
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
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06005C63 RID: 23651 RVA: 0x00015ED5 File Offset: 0x000140D5
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06005C64 RID: 23652 RVA: 0x00002119 File Offset: 0x00000319
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06005C65 RID: 23653 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001028 RID: 4136
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
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
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06005C68 RID: 23656 RVA: 0x00131627 File Offset: 0x0012F827
		public ICollection Keys
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06005C69 RID: 23657 RVA: 0x00131627 File Offset: 0x0012F827
		public ICollection Values
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x06005C6A RID: 23658 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool Contains(object key)
		{
			return false;
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x00131630 File Offset: 0x0012F830
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
			throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x001316AB File Offset: 0x0012F8AB
		public void Clear()
		{
			throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06005C6D RID: 23661 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06005C6E RID: 23662 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x00131515 File Offset: 0x0012F715
		public IDictionaryEnumerator GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x001316AB File Offset: 0x0012F8AB
		public void Remove(object key)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
		}

		// Token: 0x020009C2 RID: 2498
		private sealed class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06005C72 RID: 23666 RVA: 0x00015ED5 File Offset: 0x000140D5
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x1700102D RID: 4141
			// (get) Token: 0x06005C73 RID: 23667 RVA: 0x001316BC File Offset: 0x0012F8BC
			public object Current
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x06005C74 RID: 23668 RVA: 0x00002194 File Offset: 0x00000394
			public void Reset()
			{
			}

			// Token: 0x1700102E RID: 4142
			// (get) Token: 0x06005C75 RID: 23669 RVA: 0x001316BC File Offset: 0x0012F8BC
			public object Key
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x1700102F RID: 4143
			// (get) Token: 0x06005C76 RID: 23670 RVA: 0x001316BC File Offset: 0x0012F8BC
			public object Value
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x17001030 RID: 4144
			// (get) Token: 0x06005C77 RID: 23671 RVA: 0x001316BC File Offset: 0x0012F8BC
			public DictionaryEntry Entry
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}
		}
	}
}

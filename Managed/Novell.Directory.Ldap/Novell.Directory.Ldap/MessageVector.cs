using System;
using System.Collections;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003F RID: 63
	internal class MessageVector : IList, ICollection, IEnumerable
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000C488 File Offset: 0x0000A688
		internal object[] ObjectArray
		{
			get
			{
				object syncRoot = this.SyncRoot;
				object[] array2;
				lock (syncRoot)
				{
					object[] array = this.ToArray();
					this.Clear();
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		internal MessageVector(int cap, int incr)
		{
			this._innerList = ArrayList.Synchronized(new ArrayList(cap));
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		internal Message findMessageById(int msgId)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				for (int i = 0; i < this.Count; i++)
				{
					Message message;
					if ((message = (Message)this[i]) == null)
					{
						throw new FieldAccessException();
					}
					if (message.MessageID == msgId)
					{
						return message;
					}
				}
				throw new FieldAccessException();
			}
			Message message2;
			return message2;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C564 File Offset: 0x0000A764
		public object[] ToArray()
		{
			return this._innerList.ToArray();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C571 File Offset: 0x0000A771
		public int Add(object value)
		{
			return this._innerList.Add(value);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C57F File Offset: 0x0000A77F
		public void Clear()
		{
			this._innerList.Clear();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C58C File Offset: 0x0000A78C
		public bool Contains(object value)
		{
			return this._innerList.Contains(value);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C59A File Offset: 0x0000A79A
		public int IndexOf(object value)
		{
			return this._innerList.IndexOf(value);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		public void Insert(int index, object value)
		{
			this._innerList.Insert(index, value);
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000C5B7 File Offset: 0x0000A7B7
		public bool IsFixedSize
		{
			get
			{
				return this._innerList.IsFixedSize;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		public bool IsReadOnly
		{
			get
			{
				return this._innerList.IsReadOnly;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C5D1 File Offset: 0x0000A7D1
		public void Remove(object value)
		{
			this._innerList.Remove(value);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C5DF File Offset: 0x0000A7DF
		public void RemoveAt(int index)
		{
			this._innerList.RemoveAt(index);
		}

		// Token: 0x170000BE RID: 190
		public object this[int index]
		{
			get
			{
				return this._innerList[index];
			}
			set
			{
				this._innerList[index] = value;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C60A File Offset: 0x0000A80A
		public void CopyTo(Array array, int index)
		{
			this._innerList.CopyTo(array, index);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000C619 File Offset: 0x0000A819
		public int Count
		{
			get
			{
				return this._innerList.Count;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000C626 File Offset: 0x0000A826
		public bool IsSynchronized
		{
			get
			{
				return this._innerList.IsSynchronized;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000C633 File Offset: 0x0000A833
		public object SyncRoot
		{
			get
			{
				return this._innerList.SyncRoot;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C640 File Offset: 0x0000A840
		public IEnumerator GetEnumerator()
		{
			return this._innerList.GetEnumerator();
		}

		// Token: 0x0400018C RID: 396
		private readonly ArrayList _innerList;
	}
}

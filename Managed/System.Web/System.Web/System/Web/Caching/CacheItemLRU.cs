using System;
using System.Collections.Generic;

namespace System.Web.Caching
{
	// Token: 0x02000681 RID: 1665
	internal sealed class CacheItemLRU
	{
		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06004738 RID: 18232 RVA: 0x000C8304 File Offset: 0x000C6504
		public int Count
		{
			get
			{
				return this.dict.Count;
			}
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x000C8314 File Offset: 0x000C6514
		public CacheItemLRU(Cache owner, int highWaterMark, int lowWaterMark)
		{
			this.list = new LinkedList<CacheItem>();
			this.dict = new Dictionary<string, LinkedListNode<CacheItem>>(StringComparer.Ordinal);
			this.revdict = new Dictionary<LinkedListNode<CacheItem>, string>();
			this.highWaterMark = highWaterMark;
			this.lowWaterMark = lowWaterMark;
			this.owner = owner;
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x000C8364 File Offset: 0x000C6564
		public bool TryGetValue(string key, out CacheItem value)
		{
			LinkedListNode<CacheItem> linkedListNode;
			if (this.dict.TryGetValue(key, out linkedListNode))
			{
				value = linkedListNode.Value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x000C8390 File Offset: 0x000C6590
		public void EvictIfNecessary()
		{
			if (!this.needsEviction)
			{
				return;
			}
			for (int i = this.dict.Count; i > this.lowWaterMark; i--)
			{
				string text = this.revdict[this.list.Last];
				this.owner.Remove(text, CacheItemRemovedReason.Underused, false, true);
			}
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x000C83E8 File Offset: 0x000C65E8
		public void InvokePrivateCallbacks()
		{
			foreach (KeyValuePair<string, LinkedListNode<CacheItem>> keyValuePair in this.dict)
			{
				CacheItem value = keyValuePair.Value.Value;
				if (value != null && !value.Disabled && value.OnRemoveCallback != null)
				{
					try
					{
						value.OnRemoveCallback(keyValuePair.Key, value.Value, CacheItemRemovedReason.Removed);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x000C8480 File Offset: 0x000C6680
		public List<CacheItem> SelectItems(CacheItemLRU.SelectItemsQualifier qualifier)
		{
			List<CacheItem> list = new List<CacheItem>();
			foreach (LinkedListNode<CacheItem> linkedListNode in this.dict.Values)
			{
				CacheItem value = linkedListNode.Value;
				if (qualifier(value))
				{
					list.Add(value);
				}
			}
			return list;
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x000C84F0 File Offset: 0x000C66F0
		public List<CacheItem> ToList()
		{
			List<CacheItem> list = new List<CacheItem>();
			if (this.dict.Count == 0)
			{
				return list;
			}
			foreach (LinkedListNode<CacheItem> linkedListNode in this.dict.Values)
			{
				list.Add(linkedListNode.Value);
			}
			return list;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x000C8564 File Offset: 0x000C6764
		public void Remove(string key)
		{
			if (key == null)
			{
				return;
			}
			LinkedListNode<CacheItem> linkedListNode;
			if (!this.dict.TryGetValue(key, out linkedListNode))
			{
				return;
			}
			CacheItem value = linkedListNode.Value;
			this.dict.Remove(key);
			if (value == null || value.Priority != CacheItemPriority.NotRemovable)
			{
				this.revdict.Remove(linkedListNode);
				this.list.Remove(linkedListNode);
			}
		}

		// Token: 0x17001600 RID: 5632
		public CacheItem this[string key]
		{
			get
			{
				if (key == null)
				{
					return null;
				}
				LinkedListNode<CacheItem> linkedListNode;
				if (this.dict.TryGetValue(key, out linkedListNode))
				{
					CacheItem value = linkedListNode.Value;
					if (value == null || value.Priority != CacheItemPriority.NotRemovable)
					{
						this.list.Remove(linkedListNode);
						this.list.AddFirst(linkedListNode);
					}
					return value;
				}
				return null;
			}
			set
			{
				LinkedListNode<CacheItem> linkedListNode;
				if (this.dict.TryGetValue(key, out linkedListNode))
				{
					this.list.Remove(linkedListNode);
					if (value == null || value.Priority != CacheItemPriority.NotRemovable)
					{
						this.list.AddFirst(linkedListNode);
					}
					else
					{
						this.revdict.Remove(linkedListNode);
					}
					linkedListNode.Value = value;
					return;
				}
				this.needsEviction = this.dict.Count >= this.highWaterMark;
				linkedListNode = new LinkedListNode<CacheItem>(value);
				if (value == null || value.Priority != CacheItemPriority.NotRemovable)
				{
					this.list.AddFirst(linkedListNode);
					this.revdict[linkedListNode] = key;
				}
				this.dict[key] = linkedListNode;
			}
		}

		// Token: 0x0400257F RID: 9599
		private Dictionary<string, LinkedListNode<CacheItem>> dict;

		// Token: 0x04002580 RID: 9600
		private Dictionary<LinkedListNode<CacheItem>, string> revdict;

		// Token: 0x04002581 RID: 9601
		private LinkedList<CacheItem> list;

		// Token: 0x04002582 RID: 9602
		private Cache owner;

		// Token: 0x04002583 RID: 9603
		private int highWaterMark;

		// Token: 0x04002584 RID: 9604
		private int lowWaterMark;

		// Token: 0x04002585 RID: 9605
		private bool needsEviction;

		// Token: 0x02000682 RID: 1666
		// (Invoke) Token: 0x06004743 RID: 18243
		public delegate bool SelectItemsQualifier(CacheItem item);
	}
}

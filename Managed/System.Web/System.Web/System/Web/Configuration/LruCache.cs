using System;
using System.Collections.Generic;

namespace System.Web.Configuration
{
	// Token: 0x020005B6 RID: 1462
	internal class LruCache<TKey, TValue>
	{
		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06003EB3 RID: 16051 RVA: 0x000A5EA6 File Offset: 0x000A40A6
		// (set) Token: 0x06003EB2 RID: 16050 RVA: 0x000A5E9D File Offset: 0x000A409D
		internal string EvictionWarning { private get; set; }

		// Token: 0x06003EB4 RID: 16052 RVA: 0x000A5EAE File Offset: 0x000A40AE
		public LruCache(int entryLimit)
		{
			this.entry_limit = entryLimit;
			this.dict = new Dictionary<TKey, LinkedListNode<TValue>>();
			this.revdict = new Dictionary<LinkedListNode<TValue>, TKey>();
			this.list = new LinkedList<TValue>();
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x000A5EE0 File Offset: 0x000A40E0
		private void Evict()
		{
			LinkedListNode<TValue> last = this.list.Last;
			if (last == null)
			{
				return;
			}
			TKey tkey = this.revdict[last];
			this.dict.Remove(tkey);
			this.revdict.Remove(last);
			this.list.RemoveLast();
			this.DisposeValue(last.Value);
			this.evictions++;
			if (!string.IsNullOrEmpty(this.EvictionWarning) && !this.eviction_warning_shown && this.evictions >= this.entry_limit)
			{
				Console.Error.WriteLine("WARNING: " + this.EvictionWarning);
				this.eviction_warning_shown = true;
			}
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x000A5F90 File Offset: 0x000A4190
		public void Clear()
		{
			foreach (TValue tvalue in this.list)
			{
				this.DisposeValue(tvalue);
			}
			this.dict.Clear();
			this.revdict.Clear();
			this.list.Clear();
			this.eviction_warning_shown = false;
			this.evictions = 0;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x000A6014 File Offset: 0x000A4214
		private void DisposeValue(TValue value)
		{
			if (value is IDisposable)
			{
				((IDisposable)((object)value)).Dispose();
			}
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x000A6034 File Offset: 0x000A4234
		public bool TryGetValue(TKey key, out TValue value)
		{
			LinkedListNode<TValue> linkedListNode;
			if (this.dict.TryGetValue(key, out linkedListNode))
			{
				this.list.Remove(linkedListNode);
				this.list.AddFirst(linkedListNode);
				value = linkedListNode.Value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x000A6080 File Offset: 0x000A4280
		public void Add(TKey key, TValue value)
		{
			LinkedListNode<TValue> linkedListNode;
			if (this.dict.TryGetValue(key, out linkedListNode))
			{
				this.list.Remove(linkedListNode);
				this.list.AddFirst(linkedListNode);
				this.DisposeValue(linkedListNode.Value);
				linkedListNode.Value = value;
				return;
			}
			if (this.dict.Count >= this.entry_limit)
			{
				this.Evict();
			}
			linkedListNode = new LinkedListNode<TValue>(value);
			this.list.AddFirst(linkedListNode);
			this.dict[key] = linkedListNode;
			this.revdict[linkedListNode] = key;
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x000A610F File Offset: 0x000A430F
		public override string ToString()
		{
			return "LRUCache dict={0} revdict={1} list={2}";
		}

		// Token: 0x0400223C RID: 8764
		private Dictionary<TKey, LinkedListNode<TValue>> dict;

		// Token: 0x0400223D RID: 8765
		private Dictionary<LinkedListNode<TValue>, TKey> revdict;

		// Token: 0x0400223E RID: 8766
		private LinkedList<TValue> list;

		// Token: 0x0400223F RID: 8767
		private int entry_limit;

		// Token: 0x04002240 RID: 8768
		private bool eviction_warning_shown;

		// Token: 0x04002241 RID: 8769
		private int evictions;
	}
}

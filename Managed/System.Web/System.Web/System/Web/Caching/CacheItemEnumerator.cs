using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.Caching
{
	// Token: 0x02000680 RID: 1664
	internal sealed class CacheItemEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06004730 RID: 18224 RVA: 0x000C822A File Offset: 0x000C642A
		public CacheItemEnumerator(List<CacheItem> list)
		{
			this.list = list;
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06004731 RID: 18225 RVA: 0x000C8240 File Offset: 0x000C6440
		private CacheItem Item
		{
			get
			{
				if (this.pos < 0 || this.pos >= this.list.Count)
				{
					throw new InvalidOperationException();
				}
				return this.list[this.pos];
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x000C8278 File Offset: 0x000C6478
		public DictionaryEntry Entry
		{
			get
			{
				CacheItem item = this.Item;
				if (item == null)
				{
					return new DictionaryEntry(null, null);
				}
				return new DictionaryEntry(item.Key, item.Value);
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06004733 RID: 18227 RVA: 0x000C82A8 File Offset: 0x000C64A8
		public object Key
		{
			get
			{
				return this.Item.Key;
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06004734 RID: 18228 RVA: 0x000C82B5 File Offset: 0x000C64B5
		public object Value
		{
			get
			{
				return this.Item.Value;
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06004735 RID: 18229 RVA: 0x000C82C2 File Offset: 0x000C64C2
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x000C82D0 File Offset: 0x000C64D0
		public bool MoveNext()
		{
			int num = this.pos + 1;
			this.pos = num;
			return num < this.list.Count;
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x000C82FB File Offset: 0x000C64FB
		public void Reset()
		{
			this.pos = -1;
		}

		// Token: 0x0400257D RID: 9597
		private List<CacheItem> list;

		// Token: 0x0400257E RID: 9598
		private int pos = -1;
	}
}

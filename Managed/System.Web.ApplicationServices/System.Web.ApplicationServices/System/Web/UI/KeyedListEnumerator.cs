using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x0200000B RID: 11
	internal class KeyedListEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000023A5 File Offset: 0x000005A5
		internal KeyedListEnumerator(ArrayList list)
		{
			this.objs = list;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023BB File Offset: 0x000005BB
		public bool MoveNext()
		{
			this.index++;
			return this.index < this.objs.Count;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023E1 File Offset: 0x000005E1
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023EC File Offset: 0x000005EC
		public object Current
		{
			get
			{
				if (this.index < 0 || this.index >= this.objs.Count)
				{
					throw new InvalidOperationException();
				}
				return ((DictionaryEntry)this.objs[this.index]).Value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002439 File Offset: 0x00000639
		public DictionaryEntry Entry
		{
			get
			{
				return (DictionaryEntry)this.Current;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002448 File Offset: 0x00000648
		public object Key
		{
			get
			{
				return this.Entry.Key;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002464 File Offset: 0x00000664
		public object Value
		{
			get
			{
				return this.Entry.Value;
			}
		}

		// Token: 0x04000042 RID: 66
		private int index = -1;

		// Token: 0x04000043 RID: 67
		private ArrayList objs;
	}
}

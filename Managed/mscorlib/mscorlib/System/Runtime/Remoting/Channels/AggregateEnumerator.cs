using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000792 RID: 1938
	internal class AggregateEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06004F64 RID: 20324 RVA: 0x0011DB82 File Offset: 0x0011BD82
		public AggregateEnumerator(IDictionary[] dics)
		{
			this.dictionaries = dics;
			this.Reset();
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06004F65 RID: 20325 RVA: 0x0011DB97 File Offset: 0x0011BD97
		public DictionaryEntry Entry
		{
			get
			{
				return this.currente.Entry;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06004F66 RID: 20326 RVA: 0x0011DBA4 File Offset: 0x0011BDA4
		public object Key
		{
			get
			{
				return this.currente.Key;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06004F67 RID: 20327 RVA: 0x0011DBB1 File Offset: 0x0011BDB1
		public object Value
		{
			get
			{
				return this.currente.Value;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06004F68 RID: 20328 RVA: 0x0011DBBE File Offset: 0x0011BDBE
		public object Current
		{
			get
			{
				return this.currente.Current;
			}
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x0011DBCC File Offset: 0x0011BDCC
		public bool MoveNext()
		{
			if (this.pos >= this.dictionaries.Length)
			{
				return false;
			}
			if (this.currente.MoveNext())
			{
				return true;
			}
			this.pos++;
			if (this.pos >= this.dictionaries.Length)
			{
				return false;
			}
			this.currente = this.dictionaries[this.pos].GetEnumerator();
			return this.MoveNext();
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x0011DC38 File Offset: 0x0011BE38
		public void Reset()
		{
			this.pos = 0;
			if (this.dictionaries.Length != 0)
			{
				this.currente = this.dictionaries[0].GetEnumerator();
			}
		}

		// Token: 0x04002A40 RID: 10816
		private IDictionary[] dictionaries;

		// Token: 0x04002A41 RID: 10817
		private int pos;

		// Token: 0x04002A42 RID: 10818
		private IDictionaryEnumerator currente;
	}
}

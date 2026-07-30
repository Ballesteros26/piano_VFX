using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x020000E8 RID: 232
	[DebuggerTypeProxy(typeof(SystemLinq_GroupingDebugView<, >))]
	[DebuggerDisplay("Key = {Key}")]
	internal class Grouping<TKey, TElement> : IGrouping<TKey, TElement>, IEnumerable<TElement>, IEnumerable, IList<TElement>, ICollection<TElement>
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00002320 File Offset: 0x00000520
		internal Grouping()
		{
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001B0B0 File Offset: 0x000192B0
		internal void Add(TElement element)
		{
			if (this._elements.Length == this._count)
			{
				Array.Resize<TElement>(ref this._elements, checked(this._count * 2));
			}
			this._elements[this._count] = element;
			this._count++;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001B100 File Offset: 0x00019300
		internal void Trim()
		{
			if (this._elements.Length != this._count)
			{
				Array.Resize<TElement>(ref this._elements, this._count);
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001B123 File Offset: 0x00019323
		public IEnumerator<TElement> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this._count; i = num + 1)
			{
				yield return this._elements[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001B132 File Offset: 0x00019332
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001B13A File Offset: 0x0001933A
		public TKey Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001B142 File Offset: 0x00019342
		int ICollection<TElement>.Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0000AA13 File Offset: 0x00008C13
		bool ICollection<TElement>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00015E57 File Offset: 0x00014057
		void ICollection<TElement>.Add(TElement item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00015E57 File Offset: 0x00014057
		void ICollection<TElement>.Clear()
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001B14A File Offset: 0x0001934A
		bool ICollection<TElement>.Contains(TElement item)
		{
			return Array.IndexOf<TElement>(this._elements, item, 0, this._count) >= 0;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001B165 File Offset: 0x00019365
		void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex)
		{
			Array.Copy(this._elements, 0, array, arrayIndex, this._count);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00015E57 File Offset: 0x00014057
		bool ICollection<TElement>.Remove(TElement item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001B17B File Offset: 0x0001937B
		int IList<TElement>.IndexOf(TElement item)
		{
			return Array.IndexOf<TElement>(this._elements, item, 0, this._count);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00015E57 File Offset: 0x00014057
		void IList<TElement>.Insert(int index, TElement item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00015E57 File Offset: 0x00014057
		void IList<TElement>.RemoveAt(int index)
		{
			throw Error.NotSupported();
		}

		// Token: 0x17000114 RID: 276
		TElement IList<TElement>.this[int index]
		{
			get
			{
				if (index < 0 || index >= this._count)
				{
					throw Error.ArgumentOutOfRange("index");
				}
				return this._elements[index];
			}
			set
			{
				throw Error.NotSupported();
			}
		}

		// Token: 0x040004E0 RID: 1248
		internal TKey _key;

		// Token: 0x040004E1 RID: 1249
		internal int _hashCode;

		// Token: 0x040004E2 RID: 1250
		internal TElement[] _elements;

		// Token: 0x040004E3 RID: 1251
		internal int _count;

		// Token: 0x040004E4 RID: 1252
		internal Grouping<TKey, TElement> _hashNext;

		// Token: 0x040004E5 RID: 1253
		internal Grouping<TKey, TElement> _next;
	}
}

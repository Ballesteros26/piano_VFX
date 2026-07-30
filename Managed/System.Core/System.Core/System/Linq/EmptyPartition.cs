using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Linq
{
	// Token: 0x020000FE RID: 254
	internal sealed class EmptyPartition<TElement> : IPartition<TElement>, IIListProvider<TElement>, IEnumerable<TElement>, IEnumerable, IEnumerator<TElement>, IDisposable, IEnumerator
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x00002320 File Offset: 0x00000520
		private EmptyPartition()
		{
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000021A0 File Offset: 0x000003A0
		public IEnumerator<TElement> GetEnumerator()
		{
			return this;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000021A0 File Offset: 0x000003A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00002285 File Offset: 0x00000485
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0001CA04 File Offset: 0x0001AC04
		[ExcludeFromCodeCoverage]
		public TElement Current
		{
			get
			{
				return default(TElement);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0001CA1C File Offset: 0x0001AC1C
		[ExcludeFromCodeCoverage]
		object IEnumerator.Current
		{
			get
			{
				return default(TElement);
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00015E57 File Offset: 0x00014057
		void IEnumerator.Reset()
		{
			throw Error.NotSupported();
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00003C4C File Offset: 0x00001E4C
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000021A0 File Offset: 0x000003A0
		public IPartition<TElement> Skip(int count)
		{
			return this;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000021A0 File Offset: 0x000003A0
		public IPartition<TElement> Take(int count)
		{
			return this;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001CA38 File Offset: 0x0001AC38
		public TElement TryGetElementAt(int index, out bool found)
		{
			found = false;
			return default(TElement);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001CA54 File Offset: 0x0001AC54
		public TElement TryGetFirst(out bool found)
		{
			found = false;
			return default(TElement);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001CA70 File Offset: 0x0001AC70
		public TElement TryGetLast(out bool found)
		{
			found = false;
			return default(TElement);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001CA89 File Offset: 0x0001AC89
		public TElement[] ToArray()
		{
			return Array.Empty<TElement>();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001CA90 File Offset: 0x0001AC90
		public List<TElement> ToList()
		{
			return new List<TElement>();
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00002285 File Offset: 0x00000485
		public int GetCount(bool onlyIfCheap)
		{
			return 0;
		}

		// Token: 0x04000525 RID: 1317
		public static readonly IPartition<TElement> Instance = new EmptyPartition<TElement>();
	}
}

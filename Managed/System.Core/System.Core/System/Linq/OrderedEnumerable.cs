using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000F3 RID: 243
	internal abstract class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>, IEnumerable<TElement>, IEnumerable, IPartition<TElement>, IIListProvider<TElement>
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x0001BC3F File Offset: 0x00019E3F
		private int[] SortedMap(Buffer<TElement> buffer)
		{
			return this.GetEnumerableSorter().Sort(buffer._items, buffer._count);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001BC58 File Offset: 0x00019E58
		private int[] SortedMap(Buffer<TElement> buffer, int minIdx, int maxIdx)
		{
			return this.GetEnumerableSorter().Sort(buffer._items, buffer._count, minIdx, maxIdx);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001BC73 File Offset: 0x00019E73
		public IEnumerator<TElement> GetEnumerator()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			if (buffer._count > 0)
			{
				int[] map = this.SortedMap(buffer);
				int num;
				for (int i = 0; i < buffer._count; i = num + 1)
				{
					yield return buffer._items[map[i]];
					num = i;
				}
				map = null;
			}
			yield break;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001BC84 File Offset: 0x00019E84
		public TElement[] ToArray()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count == 0)
			{
				return buffer._items;
			}
			TElement[] array = new TElement[count];
			int[] array2 = this.SortedMap(buffer);
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = buffer._items[array2[num]];
			}
			return array;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001BCE8 File Offset: 0x00019EE8
		public List<TElement> ToList()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			List<TElement> list = new List<TElement>(count);
			if (count > 0)
			{
				int[] array = this.SortedMap(buffer);
				for (int num = 0; num != count; num++)
				{
					list.Add(buffer._items[array[num]]);
				}
			}
			return list;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001BD44 File Offset: 0x00019F44
		public int GetCount(bool onlyIfCheap)
		{
			IIListProvider<TElement> iilistProvider;
			if ((iilistProvider = this._source as IIListProvider<TElement>) != null)
			{
				return iilistProvider.GetCount(onlyIfCheap);
			}
			if (onlyIfCheap && !(this._source is ICollection<TElement>) && !(this._source is ICollection))
			{
				return -1;
			}
			return this._source.Count<TElement>();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001BD92 File Offset: 0x00019F92
		internal IEnumerator<TElement> GetEnumerator(int minIdx, int maxIdx)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count > minIdx)
			{
				if (count <= maxIdx)
				{
					maxIdx = count - 1;
				}
				if (minIdx == maxIdx)
				{
					yield return this.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx);
				}
				else
				{
					int[] map = this.SortedMap(buffer, minIdx, maxIdx);
					while (minIdx <= maxIdx)
					{
						yield return buffer._items[map[minIdx]];
						int num = minIdx + 1;
						minIdx = num;
					}
					map = null;
				}
			}
			yield break;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		internal TElement[] ToArray(int minIdx, int maxIdx)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count <= minIdx)
			{
				return Array.Empty<TElement>();
			}
			if (count <= maxIdx)
			{
				maxIdx = count - 1;
			}
			if (minIdx == maxIdx)
			{
				return new TElement[] { this.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx) };
			}
			int[] array = this.SortedMap(buffer, minIdx, maxIdx);
			TElement[] array2 = new TElement[maxIdx - minIdx + 1];
			int num = 0;
			while (minIdx <= maxIdx)
			{
				array2[num] = buffer._items[array[minIdx]];
				num++;
				minIdx++;
			}
			return array2;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001BE4C File Offset: 0x0001A04C
		internal List<TElement> ToList(int minIdx, int maxIdx)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count <= minIdx)
			{
				return new List<TElement>();
			}
			if (count <= maxIdx)
			{
				maxIdx = count - 1;
			}
			if (minIdx == maxIdx)
			{
				return new List<TElement>(1) { this.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx) };
			}
			int[] array = this.SortedMap(buffer, minIdx, maxIdx);
			List<TElement> list = new List<TElement>(maxIdx - minIdx + 1);
			while (minIdx <= maxIdx)
			{
				list.Add(buffer._items[array[minIdx]]);
				minIdx++;
			}
			return list;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001BEDC File Offset: 0x0001A0DC
		internal int GetCount(int minIdx, int maxIdx, bool onlyIfCheap)
		{
			int count = this.GetCount(onlyIfCheap);
			if (count <= 0)
			{
				return count;
			}
			if (count <= minIdx)
			{
				return 0;
			}
			return ((count <= maxIdx) ? count : (maxIdx + 1)) - minIdx;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001BF09 File Offset: 0x0001A109
		private EnumerableSorter<TElement> GetEnumerableSorter()
		{
			return this.GetEnumerableSorter(null);
		}

		// Token: 0x06000897 RID: 2199
		internal abstract EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next);

		// Token: 0x06000898 RID: 2200 RVA: 0x0001BF12 File Offset: 0x0001A112
		private CachingComparer<TElement> GetComparer()
		{
			return this.GetComparer(null);
		}

		// Token: 0x06000899 RID: 2201
		internal abstract CachingComparer<TElement> GetComparer(CachingComparer<TElement> childComparer);

		// Token: 0x0600089A RID: 2202 RVA: 0x0001BF1B File Offset: 0x0001A11B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001BF23 File Offset: 0x0001A123
		IOrderedEnumerable<TElement> IOrderedEnumerable<TElement>.CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			return new OrderedEnumerable<TElement, TKey>(this._source, keySelector, comparer, descending, this);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001BF34 File Offset: 0x0001A134
		public IPartition<TElement> Skip(int count)
		{
			return new OrderedPartition<TElement>(this, count, int.MaxValue);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001BF42 File Offset: 0x0001A142
		public IPartition<TElement> Take(int count)
		{
			return new OrderedPartition<TElement>(this, 0, count - 1);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001BF50 File Offset: 0x0001A150
		public TElement TryGetElementAt(int index, out bool found)
		{
			if (index == 0)
			{
				return this.TryGetFirst(out found);
			}
			if (index > 0)
			{
				Buffer<TElement> buffer = new Buffer<TElement>(this._source);
				int count = buffer._count;
				if (index < count)
				{
					found = true;
					return this.GetEnumerableSorter().ElementAt(buffer._items, count, index);
				}
			}
			found = false;
			return default(TElement);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public TElement TryGetFirst(out bool found)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement telement;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					found = false;
					telement = default(TElement);
					telement = telement;
				}
				else
				{
					TElement telement2 = enumerator.Current;
					comparer.SetElement(telement2);
					while (enumerator.MoveNext())
					{
						TElement telement3 = enumerator.Current;
						if (comparer.Compare(telement3, true) < 0)
						{
							telement2 = telement3;
						}
					}
					found = true;
					telement = telement2;
				}
			}
			return telement;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001C030 File Offset: 0x0001A230
		public TElement TryGetFirst(Func<TElement, bool> predicate, out bool found)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement telement3;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TElement telement = enumerator.Current;
					if (predicate(telement))
					{
						comparer.SetElement(telement);
						while (enumerator.MoveNext())
						{
							TElement telement2 = enumerator.Current;
							if (predicate(telement2) && comparer.Compare(telement2, true) < 0)
							{
								telement = telement2;
							}
						}
						found = true;
						return telement;
					}
				}
				found = false;
				telement3 = default(TElement);
				telement3 = telement3;
			}
			return telement3;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001C0CC File Offset: 0x0001A2CC
		public TElement TryGetLast(out bool found)
		{
			TElement telement;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					found = false;
					telement = default(TElement);
					telement = telement;
				}
				else
				{
					CachingComparer<TElement> comparer = this.GetComparer();
					TElement telement2 = enumerator.Current;
					comparer.SetElement(telement2);
					while (enumerator.MoveNext())
					{
						TElement telement3 = enumerator.Current;
						if (comparer.Compare(telement3, false) >= 0)
						{
							telement2 = telement3;
						}
					}
					found = true;
					telement = telement2;
				}
			}
			return telement;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001C154 File Offset: 0x0001A354
		public TElement TryGetLast(int minIdx, int maxIdx, out bool found)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (minIdx >= count)
			{
				found = false;
				return default(TElement);
			}
			found = true;
			if (maxIdx >= count - 1)
			{
				return this.Last(buffer);
			}
			return this.GetEnumerableSorter().ElementAt(buffer._items, count, maxIdx);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		private TElement Last(Buffer<TElement> buffer)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement[] items = buffer._items;
			int count = buffer._count;
			TElement telement = items[0];
			comparer.SetElement(telement);
			for (int num = 1; num != count; num++)
			{
				TElement telement2 = items[num];
				if (comparer.Compare(telement2, false) >= 0)
				{
					telement = telement2;
				}
			}
			return telement;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001C208 File Offset: 0x0001A408
		public TElement TryGetLast(Func<TElement, bool> predicate, out bool found)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement telement3;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TElement telement = enumerator.Current;
					if (predicate(telement))
					{
						comparer.SetElement(telement);
						while (enumerator.MoveNext())
						{
							TElement telement2 = enumerator.Current;
							if (predicate(telement2) && comparer.Compare(telement2, false) >= 0)
							{
								telement = telement2;
							}
						}
						found = true;
						return telement;
					}
				}
				found = false;
				telement3 = default(TElement);
				telement3 = telement3;
			}
			return telement3;
		}

		// Token: 0x04000509 RID: 1289
		internal IEnumerable<TElement> _source;
	}
}

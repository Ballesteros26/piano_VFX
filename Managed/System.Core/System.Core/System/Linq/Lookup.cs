using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;

namespace System.Linq
{
	/// <summary>Represents a collection of keys each mapped to one or more values.</summary>
	/// <typeparam name="TKey">The type of the keys in the <see cref="T:System.Linq.Lookup`2" />.</typeparam>
	/// <typeparam name="TElement">The type of the elements of each <see cref="T:System.Collections.Generic.IEnumerable`1" /> value in the <see cref="T:System.Linq.Lookup`2" />.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EF RID: 239
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemLinq_LookupDebugView<, >))]
	public class Lookup<TKey, TElement> : ILookup<TKey, TElement>, IEnumerable<IGrouping<TKey, TElement>>, IEnumerable, IIListProvider<IGrouping<TKey, TElement>>
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x0001B638 File Offset: 0x00019838
		internal static Lookup<TKey, TElement> Create<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			foreach (TSource tsource in source)
			{
				lookup.GetGrouping(keySelector(tsource), true).Add(elementSelector(tsource));
			}
			return lookup;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001B69C File Offset: 0x0001989C
		internal static Lookup<TKey, TElement> Create(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			foreach (TElement telement in source)
			{
				lookup.GetGrouping(keySelector(telement), true).Add(telement);
			}
			return lookup;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001B6FC File Offset: 0x000198FC
		internal static Lookup<TKey, TElement> CreateForJoin(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			foreach (TElement telement in source)
			{
				TKey tkey = keySelector(telement);
				if (tkey != null)
				{
					lookup.GetGrouping(tkey, true).Add(telement);
				}
			}
			return lookup;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001B764 File Offset: 0x00019964
		private Lookup(IEqualityComparer<TKey> comparer)
		{
			this._comparer = comparer ?? EqualityComparer<TKey>.Default;
			this._groupings = new Grouping<TKey, TElement>[7];
		}

		/// <summary>Gets the number of key/value collection pairs in the <see cref="T:System.Linq.Lookup`2" />.</summary>
		/// <returns>The number of key/value collection pairs in the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0001B788 File Offset: 0x00019988
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Gets the collection of values indexed by the specified key.</summary>
		/// <returns>The collection of values indexed by the specified key.</returns>
		/// <param name="key">The key of the desired collection of values.</param>
		// Token: 0x1700011A RID: 282
		public IEnumerable<TElement> this[TKey key]
		{
			get
			{
				Grouping<TKey, TElement> grouping = this.GetGrouping(key, false);
				if (grouping != null)
				{
					return grouping;
				}
				return Array.Empty<TElement>();
			}
		}

		/// <summary>Determines whether a specified key is in the <see cref="T:System.Linq.Lookup`2" />.</summary>
		/// <returns>true if <paramref name="key" /> is in the <see cref="T:System.Linq.Lookup`2" />; otherwise, false.</returns>
		/// <param name="key">The key to find in the <see cref="T:System.Linq.Lookup`2" />.</param>
		// Token: 0x06000870 RID: 2160 RVA: 0x0001B7B0 File Offset: 0x000199B0
		public bool Contains(TKey key)
		{
			return this.GetGrouping(key, false) != null;
		}

		/// <summary>Returns a generic enumerator that iterates through the <see cref="T:System.Linq.Lookup`2" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x06000871 RID: 2161 RVA: 0x0001B7BD File Offset: 0x000199BD
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			Grouping<TKey, TElement> g = this._lastGrouping;
			if (g != null)
			{
				do
				{
					g = g._next;
					yield return g;
				}
				while (g != this._lastGrouping);
			}
			yield break;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001B7CC File Offset: 0x000199CC
		IGrouping<TKey, TElement>[] IIListProvider<IGrouping<TKey, TElement>>.ToArray()
		{
			IGrouping<TKey, TElement>[] array = new IGrouping<TKey, TElement>[this._count];
			int num = 0;
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					array[num] = grouping;
					num++;
				}
				while (grouping != this._lastGrouping);
			}
			return array;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001B80C File Offset: 0x00019A0C
		internal TResult[] ToArray<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			TResult[] array = new TResult[this._count];
			int num = 0;
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					grouping.Trim();
					array[num] = resultSelector(grouping._key, grouping._elements);
					num++;
				}
				while (grouping != this._lastGrouping);
			}
			return array;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001B868 File Offset: 0x00019A68
		List<IGrouping<TKey, TElement>> IIListProvider<IGrouping<TKey, TElement>>.ToList()
		{
			List<IGrouping<TKey, TElement>> list = new List<IGrouping<TKey, TElement>>(this._count);
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					list.Add(grouping);
				}
				while (grouping != this._lastGrouping);
			}
			return list;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001B8A4 File Offset: 0x00019AA4
		internal List<TResult> ToList<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			List<TResult> list = new List<TResult>(this._count);
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					grouping.Trim();
					list.Add(resultSelector(grouping._key, grouping._elements));
				}
				while (grouping != this._lastGrouping);
			}
			return list;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001B788 File Offset: 0x00019988
		int IIListProvider<IGrouping<TKey, TElement>>.GetCount(bool onlyIfCheap)
		{
			return this._count;
		}

		/// <summary>Applies a transform function to each key and its associated values and returns the results.</summary>
		/// <returns>A collection that contains one value for each key/value collection pair in the <see cref="T:System.Linq.Lookup`2" />.</returns>
		/// <param name="resultSelector">A function to project a result value from each key and its associated values.</param>
		/// <typeparam name="TResult">The type of the result values produced by <paramref name="resultSelector" />.</typeparam>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000877 RID: 2167 RVA: 0x0001B8F6 File Offset: 0x00019AF6
		public IEnumerable<TResult> ApplyResultSelector<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			Grouping<TKey, TElement> g = this._lastGrouping;
			if (g != null)
			{
				do
				{
					g = g._next;
					g.Trim();
					yield return resultSelector(g._key, g._elements);
				}
				while (g != this._lastGrouping);
			}
			yield break;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Linq.Lookup`2" />. This class cannot be inherited.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x06000878 RID: 2168 RVA: 0x0001B90D File Offset: 0x00019B0D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001B915 File Offset: 0x00019B15
		private int InternalGetHashCode(TKey key)
		{
			if (key != null)
			{
				return this._comparer.GetHashCode(key) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001B934 File Offset: 0x00019B34
		internal Grouping<TKey, TElement> GetGrouping(TKey key, bool create)
		{
			int num = this.InternalGetHashCode(key);
			for (Grouping<TKey, TElement> grouping = this._groupings[num % this._groupings.Length]; grouping != null; grouping = grouping._hashNext)
			{
				if (grouping._hashCode == num && this._comparer.Equals(grouping._key, key))
				{
					return grouping;
				}
			}
			if (create)
			{
				if (this._count == this._groupings.Length)
				{
					this.Resize();
				}
				int num2 = num % this._groupings.Length;
				Grouping<TKey, TElement> grouping2 = new Grouping<TKey, TElement>();
				grouping2._key = key;
				grouping2._hashCode = num;
				grouping2._elements = new TElement[1];
				grouping2._hashNext = this._groupings[num2];
				this._groupings[num2] = grouping2;
				if (this._lastGrouping == null)
				{
					grouping2._next = grouping2;
				}
				else
				{
					grouping2._next = this._lastGrouping._next;
					this._lastGrouping._next = grouping2;
				}
				this._lastGrouping = grouping2;
				this._count++;
				return grouping2;
			}
			return null;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001BA2C File Offset: 0x00019C2C
		private void Resize()
		{
			int num = checked(this._count * 2 + 1);
			Grouping<TKey, TElement>[] array = new Grouping<TKey, TElement>[num];
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			do
			{
				grouping = grouping._next;
				int num2 = grouping._hashCode % num;
				grouping._hashNext = array[num2];
				array[num2] = grouping;
			}
			while (grouping != this._lastGrouping);
			this._groupings = array;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000220F File Offset: 0x0000040F
		internal Lookup()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040004FA RID: 1274
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x040004FB RID: 1275
		private Grouping<TKey, TElement>[] _groupings;

		// Token: 0x040004FC RID: 1276
		private Grouping<TKey, TElement> _lastGrouping;

		// Token: 0x040004FD RID: 1277
		private int _count;
	}
}

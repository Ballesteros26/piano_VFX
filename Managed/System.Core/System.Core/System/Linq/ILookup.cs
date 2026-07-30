using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Defines an indexer, size property, and Boolean search method for data structures that map keys to <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequences of values.</summary>
	/// <typeparam name="TKey">The type of the keys in the <see cref="T:System.Linq.ILookup`2" />.</typeparam>
	/// <typeparam name="TElement">The type of the elements in the <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequences that make up the values in the <see cref="T:System.Linq.ILookup`2" />.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EE RID: 238
	public interface ILookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
	{
		/// <summary>Gets the number of key/value collection pairs in the <see cref="T:System.Linq.ILookup`2" />.</summary>
		/// <returns>The number of key/value collection pairs in the <see cref="T:System.Linq.ILookup`2" />.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000867 RID: 2151
		int Count { get; }

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence of values indexed by a specified key.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence of values indexed by the specified key.</returns>
		/// <param name="key">The key of the desired sequence of values.</param>
		// Token: 0x17000118 RID: 280
		IEnumerable<TElement> this[TKey key] { get; }

		/// <summary>Determines whether a specified key exists in the <see cref="T:System.Linq.ILookup`2" />.</summary>
		/// <returns>true if <paramref name="key" /> is in the <see cref="T:System.Linq.ILookup`2" />; otherwise, false.</returns>
		/// <param name="key">The key to search for in the <see cref="T:System.Linq.ILookup`2" />.</param>
		// Token: 0x06000869 RID: 2153
		bool Contains(TKey key);
	}
}

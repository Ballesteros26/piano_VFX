using System;

namespace System.Collections.Generic
{
	/// <summary>Provides the base interface for the abstraction of sets.</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x0200074B RID: 1867
	public interface ISet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		/// <summary>Adds an element to the current set and returns a value to indicate if the element was successfully added. </summary>
		/// <returns>true if the element is added to the set; false if the element is already in the set.</returns>
		/// <param name="item">The element to add to the set.</param>
		// Token: 0x06003B44 RID: 15172
		bool Add(T item);

		/// <summary>Modifies the current set so that it contains all elements that are present in either the current set or the specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B45 RID: 15173
		void UnionWith(IEnumerable<T> other);

		/// <summary>Modifies the current set so that it contains only elements that are also in a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B46 RID: 15174
		void IntersectWith(IEnumerable<T> other);

		/// <summary>Removes all elements in the specified collection from the current set.</summary>
		/// <param name="other">The collection of items to remove from the set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B47 RID: 15175
		void ExceptWith(IEnumerable<T> other);

		/// <summary>Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both. </summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B48 RID: 15176
		void SymmetricExceptWith(IEnumerable<T> other);

		/// <summary>Determines whether a set is a subset of a specified collection.</summary>
		/// <returns>true if the current set is a subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B49 RID: 15177
		bool IsSubsetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set is a superset of a specified collection.</summary>
		/// <returns>true if the current set is a superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B4A RID: 15178
		bool IsSupersetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set is a proper (strict) superset of a specified collection.</summary>
		/// <returns>true if the current set is a proper superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B4B RID: 15179
		bool IsProperSupersetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set is a proper (strict) subset of a specified collection.</summary>
		/// <returns>true if the current set is a proper subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B4C RID: 15180
		bool IsProperSubsetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set overlaps with the specified collection.</summary>
		/// <returns>true if the current set and <paramref name="other" /> share at least one common element; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B4D RID: 15181
		bool Overlaps(IEnumerable<T> other);

		/// <summary>Determines whether the current set and the specified collection contain the same elements.</summary>
		/// <returns>true if the current set is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06003B4E RID: 15182
		bool SetEquals(IEnumerable<T> other);
	}
}

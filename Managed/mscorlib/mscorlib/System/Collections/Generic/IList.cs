using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of objects that can be individually accessed by index.</summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A53 RID: 2643
	[TypeDependency("System.SZArrayHelper")]
	public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
		// Token: 0x17001157 RID: 4439
		T this[int index] { get; set; }

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.</summary>
		/// <returns>The index of <paramref name="item" /> if found in the list; otherwise, -1.</returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		// Token: 0x060060EC RID: 24812
		int IndexOf(T item);

		/// <summary>Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
		// Token: 0x060060ED RID: 24813
		void Insert(int index, T item);

		/// <summary>Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
		// Token: 0x060060EE RID: 24814
		void RemoveAt(int index);
	}
}

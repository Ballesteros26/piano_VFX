using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	/// <summary>Represents a read-only collection of elements that can be accessed by index. </summary>
	/// <typeparam name="T">The type of elements in the read-only list. This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
	// Token: 0x02000A56 RID: 2646
	[TypeDependency("System.SZArrayHelper")]
	public interface IReadOnlyList<out T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		/// <summary>Gets the element at the specified index in the read-only list.</summary>
		/// <returns>The element at the specified index in the read-only list.</returns>
		/// <param name="index">The zero-based index of the element to get. </param>
		// Token: 0x1700115C RID: 4444
		T this[int index] { get; }
	}
}

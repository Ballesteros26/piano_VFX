using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	/// <summary>Represents a strongly-typed, read-only collection of elements.</summary>
	/// <typeparam name="T">The type of the elements.This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
	// Token: 0x02000A54 RID: 2644
	[TypeDependency("System.SZArrayHelper")]
	public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
	{
		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection. </returns>
		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060060EF RID: 24815
		int Count { get; }
	}
}

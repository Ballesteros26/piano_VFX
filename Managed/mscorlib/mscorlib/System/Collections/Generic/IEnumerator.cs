using System;

namespace System.Collections.Generic
{
	/// <summary>Supports a simple iteration over a generic collection.</summary>
	/// <typeparam name="T">The type of objects to enumerate.This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A51 RID: 2641
	public interface IEnumerator<out T> : IDisposable, IEnumerator
	{
		/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
		/// <returns>The element in the collection at the current position of the enumerator.</returns>
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060060E7 RID: 24807
		T Current { get; }
	}
}

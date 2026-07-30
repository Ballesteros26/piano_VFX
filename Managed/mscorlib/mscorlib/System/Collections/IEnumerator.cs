using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Supports a simple iteration over a non-generic collection.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020009D0 RID: 2512
	[ComVisible(true)]
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	public interface IEnumerator
	{
		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005CF9 RID: 23801
		bool MoveNext();

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06005CFA RID: 23802
		object Current { get; }

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005CFB RID: 23803
		void Reset();
	}
}

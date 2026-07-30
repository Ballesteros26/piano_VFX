using System;

namespace System.Dynamic
{
	/// <summary>Represents information about a dynamic get member operation that indicates if the get member should invoke properties when they perform the get operation.</summary>
	// Token: 0x02000332 RID: 818
	public interface IInvokeOnGetBinder
	{
		/// <summary>Gets the value indicating if this get member operation should invoke properties when they perform the get operation. The default value when this interface is not present is true.</summary>
		/// <returns>True if this get member operation should invoke properties when they perform the get operation; otherwise false.</returns>
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060018B3 RID: 6323
		bool InvokeOnGet { get; }
	}
}

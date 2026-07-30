using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents the values of run-time variables.</summary>
	// Token: 0x020002FB RID: 763
	public interface IRuntimeVariables
	{
		/// <summary>Gets a count of the run-time variables.</summary>
		/// <returns>The number of run-time variables.</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001733 RID: 5939
		int Count { get; }

		/// <summary>Gets the value of the run-time variable at the specified index.</summary>
		/// <returns>The value of the run-time variable.</returns>
		/// <param name="index">The zero-based index of the run-time variable whose value is to be returned.</param>
		// Token: 0x17000411 RID: 1041
		object this[int index] { get; set; }
	}
}

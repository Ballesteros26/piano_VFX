using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies the type of contract that failed. </summary>
	// Token: 0x02000A8A RID: 2698
	public enum ContractFailureKind
	{
		/// <summary>A <see cref="Overload:System.Diagnostics.Contracts.Contract.Requires" /> contract failed.</summary>
		// Token: 0x040030F4 RID: 12532
		Precondition,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Ensures" /> contract failed. </summary>
		// Token: 0x040030F5 RID: 12533
		Postcondition,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.EnsuresOnThrow" /> contract failed.</summary>
		// Token: 0x040030F6 RID: 12534
		PostconditionOnException,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Invariant" /> contract failed.</summary>
		// Token: 0x040030F7 RID: 12535
		Invariant,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Assert" /> contract failed.</summary>
		// Token: 0x040030F8 RID: 12536
		Assert,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Assume" /> contract failed.</summary>
		// Token: 0x040030F9 RID: 12537
		Assume
	}
}

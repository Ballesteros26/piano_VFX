using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Enables the factoring of legacy if-then-throw code into separate methods for reuse, and provides full control over thrown exceptions and arguments.</summary>
	// Token: 0x02000A86 RID: 2694
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class ContractArgumentValidatorAttribute : Attribute
	{
	}
}

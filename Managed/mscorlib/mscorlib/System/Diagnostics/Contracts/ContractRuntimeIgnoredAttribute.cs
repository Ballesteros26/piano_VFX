using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Identifies a member that has no run-time behavior.</summary>
	// Token: 0x02000A83 RID: 2691
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractRuntimeIgnoredAttribute : Attribute
	{
	}
}

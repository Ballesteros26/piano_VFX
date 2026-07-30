using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Defines abbreviations that you can use in place of the full contract syntax.</summary>
	// Token: 0x02000A87 RID: 2695
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class ContractAbbreviatorAttribute : Attribute
	{
	}
}

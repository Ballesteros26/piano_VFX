using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies that a property participates in optimistic concurrency checks.</summary>
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class ConcurrencyCheckAttribute : Attribute
	{
	}
}

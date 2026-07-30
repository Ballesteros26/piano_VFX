using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Denotes one or more properties that uniquely identify an entity.</summary>
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class KeyAttribute : Attribute
	{
	}
}

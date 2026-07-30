using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Identifies a method that is not generating an event.</summary>
	// Token: 0x02000B07 RID: 2823
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class NonEventAttribute : Attribute
	{
	}
}

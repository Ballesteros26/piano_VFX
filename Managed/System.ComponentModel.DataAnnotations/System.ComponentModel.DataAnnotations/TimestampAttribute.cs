using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the data type of the column as a row version.</summary>
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class TimestampAttribute : Attribute
	{
	}
}

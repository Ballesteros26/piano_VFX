using System;

namespace System.ComponentModel.DataAnnotations.Schema
{
	/// <summary>Denotes that a property or class should be excluded from database mapping.</summary>
	// Token: 0x0200004D RID: 77
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class NotMappedAttribute : Attribute
	{
	}
}

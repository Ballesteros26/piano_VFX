using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Fixes the address of a static value type field throughout its lifetime. This class cannot be inherited.</summary>
	// Token: 0x02000876 RID: 2166
	[AttributeUsage(AttributeTargets.Field)]
	[Serializable]
	public sealed class FixedAddressValueTypeAttribute : Attribute
	{
	}
}

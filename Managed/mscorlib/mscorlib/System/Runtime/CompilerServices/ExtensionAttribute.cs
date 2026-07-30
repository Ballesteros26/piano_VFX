using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a method is an extension method, or that a class or assembly contains extension methods.</summary>
	// Token: 0x02000875 RID: 2165
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class ExtensionAttribute : Attribute
	{
	}
}

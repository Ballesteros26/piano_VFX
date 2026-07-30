using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Marks modules containing unverifiable code. This class cannot be inherited.</summary>
	// Token: 0x0200052D RID: 1325
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class UnverifiableCodeAttribute : Attribute
	{
	}
}

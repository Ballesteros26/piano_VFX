using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the method to call when you register an assembly for use from COM; this enables the execution of user-written code during the registration process.</summary>
	// Token: 0x020008AF RID: 2223
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class ComRegisterFunctionAttribute : Attribute
	{
	}
}

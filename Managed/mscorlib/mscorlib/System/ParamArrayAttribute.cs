using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that a method will allow a variable number of arguments in its invocation. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001AE RID: 430
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class ParamArrayAttribute : Attribute
	{
	}
}

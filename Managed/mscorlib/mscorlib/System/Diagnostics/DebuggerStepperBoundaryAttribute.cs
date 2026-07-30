using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Indicates the code following the attribute is to be executed in run, not step, mode.</summary>
	// Token: 0x02000A61 RID: 2657
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DebuggerStepperBoundaryAttribute : Attribute
	{
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies the <see cref="T:System.Diagnostics.DebuggerHiddenAttribute" />. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A62 RID: 2658
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DebuggerHiddenAttribute : Attribute
	{
	}
}

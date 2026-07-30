using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a class should be treated as if it has global scope.</summary>
	// Token: 0x0200086D RID: 2157
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class CompilerGlobalScopeAttribute : Attribute
	{
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides information about the type of code contained in an assembly.</summary>
	// Token: 0x020002D5 RID: 725
	[ComVisible(false)]
	[Serializable]
	public enum AssemblyContentType
	{
		/// <summary>The assembly contains .NET Framework code.</summary>
		// Token: 0x0400117D RID: 4477
		Default,
		/// <summary>The assembly contains Windows Runtime code.</summary>
		// Token: 0x0400117E RID: 4478
		WindowsRuntime
	}
}

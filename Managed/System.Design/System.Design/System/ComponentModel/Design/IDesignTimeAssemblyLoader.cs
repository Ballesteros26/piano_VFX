using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System.ComponentModel.Design
{
	/// <summary>Utility for loading assemblies in a designer. </summary>
	// Token: 0x020001D2 RID: 466
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("665f0ba5-ce72-4e87-9ba0-3c461de74d0b")]
	[ComVisible(false)]
	public interface IDesignTimeAssemblyLoader
	{
		/// <summary>Determines the load path for the specified assembly in the target framework.</summary>
		/// <returns>The actual load path for the assembly, or null if the assembly is not loadable.</returns>
		/// <param name="runtimeOrTargetAssemblyName">The full assembly name for the runtime or target assembly.</param>
		/// <param name="suggestedAssemblyPath">The suggested path from which to load the assembly.</param>
		/// <param name="targetFramework">The target framework for the designer.</param>
		// Token: 0x06000BEE RID: 3054
		string GetTargetAssemblyPath(AssemblyName runtimeOrTargetAssemblyName, string suggestedAssemblyPath, FrameworkName targetFramework);

		/// <summary>Loads the specified runtime assembly.</summary>
		/// <returns>The loaded runtime assembly, or null if the assembly could not be loaded.</returns>
		/// <param name="targetAssemblyName">The full target assembly name.</param>
		// Token: 0x06000BEF RID: 3055
		Assembly LoadRuntimeAssembly(AssemblyName targetAssemblyName);
	}
}

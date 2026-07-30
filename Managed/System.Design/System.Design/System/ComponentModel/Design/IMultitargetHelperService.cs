using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines multi-target type name resolution services in a design-time environment.</summary>
	// Token: 0x020001D3 RID: 467
	public interface IMultitargetHelperService
	{
		/// <summary>Resolves a type for the target framework to an assembly-qualified name.</summary>
		/// <returns>The <see cref="P:System.Type.AssemblyQualifiedName" /> for <paramref name="type" /> in the target framework. </returns>
		/// <param name="type">The type to resolve.</param>
		// Token: 0x06000BF0 RID: 3056
		string GetAssemblyQualifiedName(Type type);
	}
}

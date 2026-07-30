using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines the access modes for a dynamic assembly. </summary>
	// Token: 0x02000349 RID: 841
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum AssemblyBuilderAccess
	{
		/// <summary>The dynamic assembly can be executed, but not saved.</summary>
		// Token: 0x040013BE RID: 5054
		Run = 1,
		/// <summary>The dynamic assembly can be saved, but not executed.</summary>
		// Token: 0x040013BF RID: 5055
		Save = 2,
		/// <summary>The dynamic assembly can be executed and saved.</summary>
		// Token: 0x040013C0 RID: 5056
		RunAndSave = 3,
		/// <summary>The dynamic assembly is loaded into the reflection-only context, and cannot be executed.</summary>
		// Token: 0x040013C1 RID: 5057
		ReflectionOnly = 6,
		/// <summary>The dynamic assembly can be unloaded and its memory reclaimed, subject to the restrictions described in Collectible Assemblies for Dynamic Type Generation.</summary>
		// Token: 0x040013C2 RID: 5058
		RunAndCollect = 9
	}
}

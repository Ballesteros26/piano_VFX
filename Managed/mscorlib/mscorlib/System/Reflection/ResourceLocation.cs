using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the resource location.</summary>
	// Token: 0x020002E6 RID: 742
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum ResourceLocation
	{
		/// <summary>Specifies an embedded (that is, non-linked) resource.</summary>
		// Token: 0x040011CC RID: 4556
		Embedded = 1,
		/// <summary>Specifies that the resource is contained in another assembly.</summary>
		// Token: 0x040011CD RID: 4557
		ContainedInAnotherAssembly = 2,
		/// <summary>Specifies that the resource is contained in the manifest file.</summary>
		// Token: 0x040011CE RID: 4558
		ContainedInManifestFile = 4
	}
}

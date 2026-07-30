using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002A RID: 42
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public enum MeshChangeState
	{
		// Token: 0x040000F3 RID: 243
		Added,
		// Token: 0x040000F4 RID: 244
		Updated,
		// Token: 0x040000F5 RID: 245
		Removed,
		// Token: 0x040000F6 RID: 246
		Unchanged
	}
}

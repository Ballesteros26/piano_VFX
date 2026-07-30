using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000026 RID: 38
	[RequiredByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public enum MeshGenerationStatus
	{
		// Token: 0x040000E1 RID: 225
		Success,
		// Token: 0x040000E2 RID: 226
		InvalidMeshId,
		// Token: 0x040000E3 RID: 227
		GenerationAlreadyInProgress,
		// Token: 0x040000E4 RID: 228
		Canceled,
		// Token: 0x040000E5 RID: 229
		UnknownError
	}
}

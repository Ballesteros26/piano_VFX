using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000029 RID: 41
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[Flags]
	public enum MeshVertexAttributes
	{
		// Token: 0x040000ED RID: 237
		None = 0,
		// Token: 0x040000EE RID: 238
		Normals = 1,
		// Token: 0x040000EF RID: 239
		Tangents = 2,
		// Token: 0x040000F0 RID: 240
		UVs = 4,
		// Token: 0x040000F1 RID: 241
		Colors = 8
	}
}

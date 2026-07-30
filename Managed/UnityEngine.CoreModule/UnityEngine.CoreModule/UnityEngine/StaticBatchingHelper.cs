using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000147 RID: 327
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	internal struct StaticBatchingHelper
	{
		// Token: 0x06000D7A RID: 3450
		[FreeFunction("MeshScripting::CombineMeshVerticesForStaticBatching")]
		[MethodImpl(4096)]
		internal static extern Mesh InternalCombineVertices(MeshSubsetCombineUtility.MeshInstance[] meshes, string meshName);

		// Token: 0x06000D7B RID: 3451
		[FreeFunction("MeshScripting::CombineMeshIndicesForStaticBatching")]
		[MethodImpl(4096)]
		internal static extern void InternalCombineIndices(MeshSubsetCombineUtility.SubMeshInstance[] submeshes, Mesh combinedMesh);
	}
}

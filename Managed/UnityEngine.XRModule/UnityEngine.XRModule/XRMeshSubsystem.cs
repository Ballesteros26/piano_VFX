using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002C RID: 44
	[UsedByNativeCode]
	[NativeConditional("ENABLE_XR")]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshingSubsystem.h")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	public class XRMeshSubsystem : IntegratedSubsystem<XRMeshSubsystemDescriptor>
	{
		// Token: 0x06000144 RID: 324 RVA: 0x000049E0 File Offset: 0x00002BE0
		public bool TryGetMeshInfos(List<MeshInfo> meshInfosOut)
		{
			bool flag = meshInfosOut == null;
			if (flag)
			{
				throw new ArgumentNullException("meshInfosOut");
			}
			return this.GetMeshInfosAsList(meshInfosOut);
		}

		// Token: 0x06000145 RID: 325
		[MethodImpl(4096)]
		private extern bool GetMeshInfosAsList(List<MeshInfo> meshInfos);

		// Token: 0x06000146 RID: 326
		[MethodImpl(4096)]
		private extern MeshInfo[] GetMeshInfosAsFixedArray();

		// Token: 0x06000147 RID: 327 RVA: 0x00004A0C File Offset: 0x00002C0C
		public void GenerateMeshAsync(MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			this.GenerateMeshAsync_Injected(ref meshId, mesh, meshCollider, attributes, onMeshGenerationComplete);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004A1C File Offset: 0x00002C1C
		[RequiredByNativeCode]
		private void InvokeMeshReadyDelegate(MeshGenerationResult result, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			bool flag = onMeshGenerationComplete != null;
			if (flag)
			{
				onMeshGenerationComplete.Invoke(result);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000149 RID: 329
		// (set) Token: 0x0600014A RID: 330
		public extern float meshDensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004A3A File Offset: 0x00002C3A
		public bool SetBoundingVolume(Vector3 origin, Vector3 extents)
		{
			return this.SetBoundingVolume_Injected(ref origin, ref extents);
		}

		// Token: 0x0600014D RID: 333
		[MethodImpl(4096)]
		private extern void GenerateMeshAsync_Injected(ref MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete);

		// Token: 0x0600014E RID: 334
		[MethodImpl(4096)]
		private extern bool SetBoundingVolume_Injected(ref Vector3 origin, ref Vector3 extents);
	}
}

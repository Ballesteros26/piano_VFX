using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000105 RID: 261
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Graphics/Mesh/MeshFilter.h")]
	public sealed class MeshFilter : Component
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x00002EC3 File Offset: 0x000010C3
		[RequiredByNativeCode]
		private void DontStripMeshFilter()
		{
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000BB6 RID: 2998
		// (set) Token: 0x06000BB7 RID: 2999
		public extern Mesh sharedMesh
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000BB8 RID: 3000
		// (set) Token: 0x06000BB9 RID: 3001
		public extern Mesh mesh
		{
			[NativeName("GetInstantiatedMeshFromScript")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetInstantiatedMesh")]
			[MethodImpl(4096)]
			set;
		}
	}
}

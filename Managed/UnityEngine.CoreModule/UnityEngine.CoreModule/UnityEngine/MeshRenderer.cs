using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200013E RID: 318
	[NativeHeader("Runtime/Graphics/Mesh/MeshRenderer.h")]
	public class MeshRenderer : Renderer
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x00002EC3 File Offset: 0x000010C3
		[RequiredByNativeCode]
		private void DontStripMeshRenderer()
		{
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000BF5 RID: 3061
		// (set) Token: 0x06000BF6 RID: 3062
		public extern Mesh additionalVertexStreams
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000BF7 RID: 3063
		// (set) Token: 0x06000BF8 RID: 3064
		public extern Mesh enlightenVertexStream
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000BF9 RID: 3065
		public extern int subMeshStartIndex
		{
			[NativeName("GetSubMeshStartIndex")]
			[MethodImpl(4096)]
			get;
		}
	}
}

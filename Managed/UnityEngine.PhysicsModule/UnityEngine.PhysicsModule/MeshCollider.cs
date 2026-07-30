using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	[NativeHeader("Runtime/Graphics/Mesh/Mesh.h")]
	[NativeHeader("Modules/Physics/MeshCollider.h")]
	[RequiredByNativeCode]
	public class MeshCollider : Collider
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600012A RID: 298
		// (set) Token: 0x0600012B RID: 299
		public extern Mesh sharedMesh
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600012C RID: 300
		// (set) Token: 0x0600012D RID: 301
		public extern bool convex
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003040 File Offset: 0x00001240
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("MeshCollider.inflateMesh is no longer supported. The new cooking algorithm doesn't need inflation to be used.")]
		public bool inflateMesh
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000130 RID: 304
		// (set) Token: 0x06000131 RID: 305
		public extern MeshColliderCookingOptions cookingOptions
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00003054 File Offset: 0x00001254
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("MeshCollider.skinWidth is no longer used.")]
		public float skinWidth
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000306C File Offset: 0x0000126C
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("Configuring smooth sphere collisions is no longer needed.")]
		public bool smoothSphereCollisions
		{
			get
			{
				return true;
			}
			set
			{
			}
		}
	}
}

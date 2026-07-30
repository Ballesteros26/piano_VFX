using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200013D RID: 317
	[NativeHeader("Runtime/Graphics/Mesh/SkinnedMeshRenderer.h")]
	public class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000BDC RID: 3036
		// (set) Token: 0x06000BDD RID: 3037
		public extern SkinQuality quality
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000BDE RID: 3038
		// (set) Token: 0x06000BDF RID: 3039
		public extern bool updateWhenOffscreen
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000BE0 RID: 3040
		// (set) Token: 0x06000BE1 RID: 3041
		public extern bool forceMatrixRecalculationPerRender
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000BE2 RID: 3042
		// (set) Token: 0x06000BE3 RID: 3043
		public extern Transform rootBone
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000BE4 RID: 3044
		// (set) Token: 0x06000BE5 RID: 3045
		public extern Transform[] bones
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000BE6 RID: 3046
		// (set) Token: 0x06000BE7 RID: 3047
		[NativeProperty("Mesh")]
		public extern Mesh sharedMesh
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000BE8 RID: 3048
		// (set) Token: 0x06000BE9 RID: 3049
		[NativeProperty("SkinnedMeshMotionVectors")]
		public extern bool skinnedMotionVectors
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000BEA RID: 3050
		[MethodImpl(4096)]
		public extern float GetBlendShapeWeight(int index);

		// Token: 0x06000BEB RID: 3051
		[MethodImpl(4096)]
		public extern void SetBlendShapeWeight(int index, float value);

		// Token: 0x06000BEC RID: 3052
		[MethodImpl(4096)]
		public extern void BakeMesh(Mesh mesh);

		// Token: 0x06000BED RID: 3053 RVA: 0x0000F568 File Offset: 0x0000D768
		[FreeFunction(Name = "SkinnedMeshRendererScripting::GetLocalAABB", HasExplicitThis = true)]
		private Bounds GetLocalAABB()
		{
			Bounds bounds;
			this.GetLocalAABB_Injected(out bounds);
			return bounds;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0000F57E File Offset: 0x0000D77E
		private void SetLocalAABB(Bounds b)
		{
			this.SetLocalAABB_Injected(ref b);
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0000F588 File Offset: 0x0000D788
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		public Bounds localBounds
		{
			get
			{
				return this.GetLocalAABB();
			}
			set
			{
				this.SetLocalAABB(value);
			}
		}

		// Token: 0x06000BF2 RID: 3058
		[MethodImpl(4096)]
		private extern void GetLocalAABB_Injected(out Bounds ret);

		// Token: 0x06000BF3 RID: 3059
		[MethodImpl(4096)]
		private extern void SetLocalAABB_Injected(ref Bounds b);
	}
}

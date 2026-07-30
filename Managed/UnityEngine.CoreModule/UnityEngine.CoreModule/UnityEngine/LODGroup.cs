using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000143 RID: 323
	[NativeHeader("Runtime/Graphics/LOD/LODGroupManager.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODUtility.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODGroup.h")]
	[StaticAccessor("GetLODGroupManager()", StaticAccessorType.Dot)]
	public class LODGroup : Component
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0000F6F2 File Offset: 0x0000D8F2
		public Vector3 localReferencePoint
		{
			get
			{
				Vector3 vector;
				this.get_localReferencePoint_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_localReferencePoint_Injected(ref value);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000C09 RID: 3081
		// (set) Token: 0x06000C0A RID: 3082
		public extern float size
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000C0B RID: 3083
		public extern int lodCount
		{
			[NativeMethod("GetLODCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000C0C RID: 3084
		// (set) Token: 0x06000C0D RID: 3085
		public extern LODFadeMode fadeMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000C0E RID: 3086
		// (set) Token: 0x06000C0F RID: 3087
		public extern bool animateCrossFading
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000C10 RID: 3088
		// (set) Token: 0x06000C11 RID: 3089
		public extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000C12 RID: 3090
		[FreeFunction("UpdateLODGroupBoundingBox", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void RecalculateBounds();

		// Token: 0x06000C13 RID: 3091
		[FreeFunction("GetLODs_Binding", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern LOD[] GetLODs();

		// Token: 0x06000C14 RID: 3092 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		[Obsolete("Use SetLODs instead.")]
		public void SetLODS(LOD[] lods)
		{
			this.SetLODs(lods);
		}

		// Token: 0x06000C15 RID: 3093
		[FreeFunction("SetLODs_Binding", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetLODs(LOD[] lods);

		// Token: 0x06000C16 RID: 3094
		[FreeFunction("ForceLODLevel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ForceLOD(int index);

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000C17 RID: 3095
		// (set) Token: 0x06000C18 RID: 3096
		[StaticAccessor("GetLODGroupManager()")]
		public static extern float crossFadeAnimationDuration
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0000F708 File Offset: 0x0000D908
		internal Vector3 worldReferencePoint
		{
			get
			{
				Vector3 vector;
				this.get_worldReferencePoint_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x06000C1B RID: 3099
		[MethodImpl(4096)]
		private extern void get_localReferencePoint_Injected(out Vector3 ret);

		// Token: 0x06000C1C RID: 3100
		[MethodImpl(4096)]
		private extern void set_localReferencePoint_Injected(ref Vector3 value);

		// Token: 0x06000C1D RID: 3101
		[MethodImpl(4096)]
		private extern void get_worldReferencePoint_Injected(out Vector3 ret);
	}
}

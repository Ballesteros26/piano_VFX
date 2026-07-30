using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000FD RID: 253
	[NativeHeader("Runtime/Camera/OcclusionArea.h")]
	public sealed class OcclusionArea : Component
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0000F330 File Offset: 0x0000D530
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0000F346 File Offset: 0x0000D546
		public Vector3 center
		{
			get
			{
				Vector3 vector;
				this.get_center_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0000F350 File Offset: 0x0000D550
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0000F366 File Offset: 0x0000D566
		public Vector3 size
		{
			get
			{
				Vector3 vector;
				this.get_size_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x06000B32 RID: 2866
		[MethodImpl(4096)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000B33 RID: 2867
		[MethodImpl(4096)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x06000B34 RID: 2868
		[MethodImpl(4096)]
		private extern void get_size_Injected(out Vector3 ret);

		// Token: 0x06000B35 RID: 2869
		[MethodImpl(4096)]
		private extern void set_size_Injected(ref Vector3 value);
	}
}

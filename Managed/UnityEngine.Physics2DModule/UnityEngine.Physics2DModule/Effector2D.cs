using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	[NativeHeader("Modules/Physics2D/Effector2D.h")]
	public class Effector2D : Behaviour
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600040C RID: 1036
		// (set) Token: 0x0600040D RID: 1037
		public extern bool useColliderMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600040E RID: 1038
		// (set) Token: 0x0600040F RID: 1039
		public extern int colliderMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000410 RID: 1040
		internal extern bool requiresCollider
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000411 RID: 1041
		internal extern bool designedForTrigger
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000412 RID: 1042
		internal extern bool designedForNonTrigger
		{
			[MethodImpl(4096)]
			get;
		}
	}
}

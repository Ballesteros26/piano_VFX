using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	[NativeHeader("Modules/Physics2D/SurfaceEffector2D.h")]
	public class SurfaceEffector2D : Effector2D
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000452 RID: 1106
		// (set) Token: 0x06000453 RID: 1107
		public extern float speed
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000454 RID: 1108
		// (set) Token: 0x06000455 RID: 1109
		public extern float speedVariation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000456 RID: 1110
		// (set) Token: 0x06000457 RID: 1111
		public extern float forceScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000458 RID: 1112
		// (set) Token: 0x06000459 RID: 1113
		public extern bool useContactForce
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600045A RID: 1114
		// (set) Token: 0x0600045B RID: 1115
		public extern bool useFriction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600045C RID: 1116
		// (set) Token: 0x0600045D RID: 1117
		public extern bool useBounce
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}

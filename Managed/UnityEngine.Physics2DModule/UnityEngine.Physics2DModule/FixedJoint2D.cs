using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002E RID: 46
	[NativeHeader("Modules/Physics2D/FixedJoint2D.h")]
	public sealed class FixedJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003F6 RID: 1014
		// (set) Token: 0x060003F7 RID: 1015
		public extern float dampingRatio
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003F8 RID: 1016
		// (set) Token: 0x060003F9 RID: 1017
		public extern float frequency
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003FA RID: 1018
		public extern float referenceAngle
		{
			[MethodImpl(4096)]
			get;
		}
	}
}

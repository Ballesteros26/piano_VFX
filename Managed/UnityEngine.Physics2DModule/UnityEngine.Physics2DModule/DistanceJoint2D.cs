using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/Physics2D/DistanceJoint2D.h")]
	public sealed class DistanceJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003A0 RID: 928
		// (set) Token: 0x060003A1 RID: 929
		public extern bool autoConfigureDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003A2 RID: 930
		// (set) Token: 0x060003A3 RID: 931
		public extern float distance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003A4 RID: 932
		// (set) Token: 0x060003A5 RID: 933
		public extern bool maxDistanceOnly
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}

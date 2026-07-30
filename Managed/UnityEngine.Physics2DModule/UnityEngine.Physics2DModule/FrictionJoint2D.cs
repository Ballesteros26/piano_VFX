using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000029 RID: 41
	[NativeHeader("Modules/Physics2D/FrictionJoint2D.h")]
	public sealed class FrictionJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003A7 RID: 935
		// (set) Token: 0x060003A8 RID: 936
		public extern float maxForce
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003A9 RID: 937
		// (set) Token: 0x060003AA RID: 938
		public extern float maxTorque
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}

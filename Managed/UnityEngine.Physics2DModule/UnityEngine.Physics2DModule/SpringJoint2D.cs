using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[NativeHeader("Modules/Physics2D/SpringJoint2D.h")]
	public sealed class SpringJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000397 RID: 919
		// (set) Token: 0x06000398 RID: 920
		public extern bool autoConfigureDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000399 RID: 921
		// (set) Token: 0x0600039A RID: 922
		public extern float distance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600039B RID: 923
		// (set) Token: 0x0600039C RID: 924
		public extern float dampingRatio
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600039D RID: 925
		// (set) Token: 0x0600039E RID: 926
		public extern float frequency
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Modules/Physics2D/AreaEffector2D.h")]
	public class AreaEffector2D : Effector2D
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000414 RID: 1044
		// (set) Token: 0x06000415 RID: 1045
		public extern float forceAngle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000416 RID: 1046
		// (set) Token: 0x06000417 RID: 1047
		public extern bool useGlobalAngle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000418 RID: 1048
		// (set) Token: 0x06000419 RID: 1049
		public extern float forceMagnitude
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600041A RID: 1050
		// (set) Token: 0x0600041B RID: 1051
		public extern float forceVariation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600041C RID: 1052
		// (set) Token: 0x0600041D RID: 1053
		public extern float drag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600041E RID: 1054
		// (set) Token: 0x0600041F RID: 1055
		public extern float angularDrag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000420 RID: 1056
		// (set) Token: 0x06000421 RID: 1057
		public extern EffectorSelection2D forceTarget
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}

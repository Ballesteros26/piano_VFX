using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	[NativeHeader("Modules/Physics2D/PointEffector2D.h")]
	public class PointEffector2D : Effector2D
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000432 RID: 1074
		// (set) Token: 0x06000433 RID: 1075
		public extern float forceMagnitude
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000434 RID: 1076
		// (set) Token: 0x06000435 RID: 1077
		public extern float forceVariation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000436 RID: 1078
		// (set) Token: 0x06000437 RID: 1079
		public extern float distanceScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000438 RID: 1080
		// (set) Token: 0x06000439 RID: 1081
		public extern float drag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600043A RID: 1082
		// (set) Token: 0x0600043B RID: 1083
		public extern float angularDrag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600043C RID: 1084
		// (set) Token: 0x0600043D RID: 1085
		public extern EffectorSelection2D forceSource
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600043E RID: 1086
		// (set) Token: 0x0600043F RID: 1087
		public extern EffectorSelection2D forceTarget
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000440 RID: 1088
		// (set) Token: 0x06000441 RID: 1089
		public extern EffectorForceMode2D forceMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}

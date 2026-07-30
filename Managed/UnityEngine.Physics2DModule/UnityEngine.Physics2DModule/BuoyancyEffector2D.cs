using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	[NativeHeader("Modules/Physics2D/BuoyancyEffector2D.h")]
	public class BuoyancyEffector2D : Effector2D
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000423 RID: 1059
		// (set) Token: 0x06000424 RID: 1060
		public extern float surfaceLevel
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000425 RID: 1061
		// (set) Token: 0x06000426 RID: 1062
		public extern float density
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000427 RID: 1063
		// (set) Token: 0x06000428 RID: 1064
		public extern float linearDrag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000429 RID: 1065
		// (set) Token: 0x0600042A RID: 1066
		public extern float angularDrag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600042B RID: 1067
		// (set) Token: 0x0600042C RID: 1068
		public extern float flowAngle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600042D RID: 1069
		// (set) Token: 0x0600042E RID: 1070
		public extern float flowMagnitude
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600042F RID: 1071
		// (set) Token: 0x06000430 RID: 1072
		public extern float flowVariation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}

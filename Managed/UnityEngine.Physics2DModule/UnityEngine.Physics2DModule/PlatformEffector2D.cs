using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/Physics2D/PlatformEffector2D.h")]
	public class PlatformEffector2D : Effector2D
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000443 RID: 1091
		// (set) Token: 0x06000444 RID: 1092
		public extern bool useOneWay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000445 RID: 1093
		// (set) Token: 0x06000446 RID: 1094
		public extern bool useOneWayGrouping
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000447 RID: 1095
		// (set) Token: 0x06000448 RID: 1096
		public extern bool useSideFriction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000449 RID: 1097
		// (set) Token: 0x0600044A RID: 1098
		public extern bool useSideBounce
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600044B RID: 1099
		// (set) Token: 0x0600044C RID: 1100
		public extern float surfaceArc
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600044D RID: 1101
		// (set) Token: 0x0600044E RID: 1102
		public extern float sideArc
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600044F RID: 1103
		// (set) Token: 0x06000450 RID: 1104
		public extern float rotationalOffset
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}

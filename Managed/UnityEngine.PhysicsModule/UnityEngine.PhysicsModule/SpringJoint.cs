using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Physics/SpringJoint.h")]
	[NativeClass("Unity::SpringJoint")]
	public class SpringJoint : Joint
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600019E RID: 414
		// (set) Token: 0x0600019F RID: 415
		public extern float spring
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001A0 RID: 416
		// (set) Token: 0x060001A1 RID: 417
		public extern float damper
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001A2 RID: 418
		// (set) Token: 0x060001A3 RID: 419
		public extern float minDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001A4 RID: 420
		// (set) Token: 0x060001A5 RID: 421
		public extern float maxDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001A6 RID: 422
		// (set) Token: 0x060001A7 RID: 423
		public extern float tolerance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}

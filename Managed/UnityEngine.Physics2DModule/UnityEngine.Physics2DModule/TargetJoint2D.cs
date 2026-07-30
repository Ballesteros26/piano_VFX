using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("Modules/Physics2D/TargetJoint2D.h")]
	public sealed class TargetJoint2D : Joint2D
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000071C0 File Offset: 0x000053C0
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x000071D6 File Offset: 0x000053D6
		public Vector2 anchor
		{
			get
			{
				Vector2 vector;
				this.get_anchor_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchor_Injected(ref value);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000071E0 File Offset: 0x000053E0
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x000071F6 File Offset: 0x000053F6
		public Vector2 target
		{
			get
			{
				Vector2 vector;
				this.get_target_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_target_Injected(ref value);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003E9 RID: 1001
		// (set) Token: 0x060003EA RID: 1002
		public extern bool autoConfigureTarget
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003EB RID: 1003
		// (set) Token: 0x060003EC RID: 1004
		public extern float maxForce
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003ED RID: 1005
		// (set) Token: 0x060003EE RID: 1006
		public extern float dampingRatio
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003EF RID: 1007
		// (set) Token: 0x060003F0 RID: 1008
		public extern float frequency
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060003F2 RID: 1010
		[MethodImpl(4096)]
		private extern void get_anchor_Injected(out Vector2 ret);

		// Token: 0x060003F3 RID: 1011
		[MethodImpl(4096)]
		private extern void set_anchor_Injected(ref Vector2 value);

		// Token: 0x060003F4 RID: 1012
		[MethodImpl(4096)]
		private extern void get_target_Injected(out Vector2 ret);

		// Token: 0x060003F5 RID: 1013
		[MethodImpl(4096)]
		private extern void set_target_Injected(ref Vector2 value);
	}
}

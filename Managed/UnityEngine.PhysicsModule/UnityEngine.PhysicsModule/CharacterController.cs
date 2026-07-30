using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	[NativeHeader("Modules/Physics/CharacterController.h")]
	public class CharacterController : Collider
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00002FE7 File Offset: 0x000011E7
		public bool SimpleMove(Vector3 speed)
		{
			return this.SimpleMove_Injected(ref speed);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002FF1 File Offset: 0x000011F1
		public CollisionFlags Move(Vector3 motion)
		{
			return this.Move_Injected(ref motion);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002FFC File Offset: 0x000011FC
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.get_velocity_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000110 RID: 272
		public extern bool isGrounded
		{
			[NativeName("IsGrounded")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000111 RID: 273
		public extern CollisionFlags collisionFlags
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000112 RID: 274
		// (set) Token: 0x06000113 RID: 275
		public extern float radius
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000114 RID: 276
		// (set) Token: 0x06000115 RID: 277
		public extern float height
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003014 File Offset: 0x00001214
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000302A File Offset: 0x0000122A
		public Vector3 center
		{
			get
			{
				Vector3 vector;
				this.get_center_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000118 RID: 280
		// (set) Token: 0x06000119 RID: 281
		public extern float slopeLimit
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600011A RID: 282
		// (set) Token: 0x0600011B RID: 283
		public extern float stepOffset
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600011C RID: 284
		// (set) Token: 0x0600011D RID: 285
		public extern float skinWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600011E RID: 286
		// (set) Token: 0x0600011F RID: 287
		public extern float minMoveDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000120 RID: 288
		// (set) Token: 0x06000121 RID: 289
		public extern bool detectCollisions
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000122 RID: 290
		// (set) Token: 0x06000123 RID: 291
		public extern bool enableOverlapRecovery
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000125 RID: 293
		[MethodImpl(4096)]
		private extern bool SimpleMove_Injected(ref Vector3 speed);

		// Token: 0x06000126 RID: 294
		[MethodImpl(4096)]
		private extern CollisionFlags Move_Injected(ref Vector3 motion);

		// Token: 0x06000127 RID: 295
		[MethodImpl(4096)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x06000128 RID: 296
		[MethodImpl(4096)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000129 RID: 297
		[MethodImpl(4096)]
		private extern void set_center_Injected(ref Vector3 value);
	}
}

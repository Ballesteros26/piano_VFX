using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	[NativeHeader("Modules/Physics2D/ConstantForce2D.h")]
	[RequireComponent(typeof(Rigidbody2D))]
	public sealed class ConstantForce2D : PhysicsUpdateBehaviour2D
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000724C File Offset: 0x0000544C
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00007262 File Offset: 0x00005462
		public Vector2 force
		{
			get
			{
				Vector2 vector;
				this.get_force_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_force_Injected(ref value);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000726C File Offset: 0x0000546C
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00007282 File Offset: 0x00005482
		public Vector2 relativeForce
		{
			get
			{
				Vector2 vector;
				this.get_relativeForce_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_relativeForce_Injected(ref value);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000464 RID: 1124
		// (set) Token: 0x06000465 RID: 1125
		public extern float torque
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000467 RID: 1127
		[MethodImpl(4096)]
		private extern void get_force_Injected(out Vector2 ret);

		// Token: 0x06000468 RID: 1128
		[MethodImpl(4096)]
		private extern void set_force_Injected(ref Vector2 value);

		// Token: 0x06000469 RID: 1129
		[MethodImpl(4096)]
		private extern void get_relativeForce_Injected(out Vector2 ret);

		// Token: 0x0600046A RID: 1130
		[MethodImpl(4096)]
		private extern void set_relativeForce_Injected(ref Vector2 value);
	}
}

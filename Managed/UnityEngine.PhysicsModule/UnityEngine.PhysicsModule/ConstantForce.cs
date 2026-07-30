using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	[NativeHeader("Modules/Physics/ConstantForce.h")]
	[RequireComponent(typeof(Rigidbody))]
	public class ConstantForce : Behaviour
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003168 File Offset: 0x00001368
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000317E File Offset: 0x0000137E
		public Vector3 force
		{
			get
			{
				Vector3 vector;
				this.get_force_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_force_Injected(ref value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00003188 File Offset: 0x00001388
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000319E File Offset: 0x0000139E
		public Vector3 relativeForce
		{
			get
			{
				Vector3 vector;
				this.get_relativeForce_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_relativeForce_Injected(ref value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000031A8 File Offset: 0x000013A8
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000031BE File Offset: 0x000013BE
		public Vector3 torque
		{
			get
			{
				Vector3 vector;
				this.get_torque_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_torque_Injected(ref value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000031C8 File Offset: 0x000013C8
		// (set) Token: 0x0600015F RID: 351 RVA: 0x000031DE File Offset: 0x000013DE
		public Vector3 relativeTorque
		{
			get
			{
				Vector3 vector;
				this.get_relativeTorque_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_relativeTorque_Injected(ref value);
			}
		}

		// Token: 0x06000161 RID: 353
		[MethodImpl(4096)]
		private extern void get_force_Injected(out Vector3 ret);

		// Token: 0x06000162 RID: 354
		[MethodImpl(4096)]
		private extern void set_force_Injected(ref Vector3 value);

		// Token: 0x06000163 RID: 355
		[MethodImpl(4096)]
		private extern void get_relativeForce_Injected(out Vector3 ret);

		// Token: 0x06000164 RID: 356
		[MethodImpl(4096)]
		private extern void set_relativeForce_Injected(ref Vector3 value);

		// Token: 0x06000165 RID: 357
		[MethodImpl(4096)]
		private extern void get_torque_Injected(out Vector3 ret);

		// Token: 0x06000166 RID: 358
		[MethodImpl(4096)]
		private extern void set_torque_Injected(ref Vector3 value);

		// Token: 0x06000167 RID: 359
		[MethodImpl(4096)]
		private extern void get_relativeTorque_Injected(out Vector3 ret);

		// Token: 0x06000168 RID: 360
		[MethodImpl(4096)]
		private extern void set_relativeTorque_Injected(ref Vector3 value);
	}
}

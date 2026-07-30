using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	[NativeHeader("Modules/Physics2D/WheelJoint2D.h")]
	public sealed class WheelJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00007200 File Offset: 0x00005400
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00007216 File Offset: 0x00005416
		public JointSuspension2D suspension
		{
			get
			{
				JointSuspension2D jointSuspension2D;
				this.get_suspension_Injected(out jointSuspension2D);
				return jointSuspension2D;
			}
			set
			{
				this.set_suspension_Injected(ref value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003FE RID: 1022
		// (set) Token: 0x060003FF RID: 1023
		public extern bool useMotor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00007220 File Offset: 0x00005420
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00007236 File Offset: 0x00005436
		public JointMotor2D motor
		{
			get
			{
				JointMotor2D jointMotor2D;
				this.get_motor_Injected(out jointMotor2D);
				return jointMotor2D;
			}
			set
			{
				this.set_motor_Injected(ref value);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000402 RID: 1026
		public extern float jointTranslation
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000403 RID: 1027
		public extern float jointLinearSpeed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000404 RID: 1028
		public extern float jointSpeed
		{
			[NativeMethod("GetJointAngularSpeed")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000405 RID: 1029
		public extern float jointAngle
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000406 RID: 1030
		[MethodImpl(4096)]
		public extern float GetMotorTorque(float timeStep);

		// Token: 0x06000408 RID: 1032
		[MethodImpl(4096)]
		private extern void get_suspension_Injected(out JointSuspension2D ret);

		// Token: 0x06000409 RID: 1033
		[MethodImpl(4096)]
		private extern void set_suspension_Injected(ref JointSuspension2D value);

		// Token: 0x0600040A RID: 1034
		[MethodImpl(4096)]
		private extern void get_motor_Injected(out JointMotor2D ret);

		// Token: 0x0600040B RID: 1035
		[MethodImpl(4096)]
		private extern void set_motor_Injected(ref JointMotor2D value);
	}
}

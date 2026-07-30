using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	[NativeHeader("Modules/Physics2D/HingeJoint2D.h")]
	public sealed class HingeJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003AC RID: 940
		// (set) Token: 0x060003AD RID: 941
		public extern bool useMotor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003AE RID: 942
		// (set) Token: 0x060003AF RID: 943
		public extern bool useLimits
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00007108 File Offset: 0x00005308
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000711E File Offset: 0x0000531E
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00007128 File Offset: 0x00005328
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000713E File Offset: 0x0000533E
		public JointAngleLimits2D limits
		{
			get
			{
				JointAngleLimits2D jointAngleLimits2D;
				this.get_limits_Injected(out jointAngleLimits2D);
				return jointAngleLimits2D;
			}
			set
			{
				this.set_limits_Injected(ref value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003B4 RID: 948
		public extern JointLimitState2D limitState
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003B5 RID: 949
		public extern float referenceAngle
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003B6 RID: 950
		public extern float jointAngle
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003B7 RID: 951
		public extern float jointSpeed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003B8 RID: 952
		[MethodImpl(4096)]
		public extern float GetMotorTorque(float timeStep);

		// Token: 0x060003BA RID: 954
		[MethodImpl(4096)]
		private extern void get_motor_Injected(out JointMotor2D ret);

		// Token: 0x060003BB RID: 955
		[MethodImpl(4096)]
		private extern void set_motor_Injected(ref JointMotor2D value);

		// Token: 0x060003BC RID: 956
		[MethodImpl(4096)]
		private extern void get_limits_Injected(out JointAngleLimits2D ret);

		// Token: 0x060003BD RID: 957
		[MethodImpl(4096)]
		private extern void set_limits_Injected(ref JointAngleLimits2D value);
	}
}

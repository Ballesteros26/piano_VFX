using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/Physics2D/SliderJoint2D.h")]
	public sealed class SliderJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003CF RID: 975
		// (set) Token: 0x060003D0 RID: 976
		public extern bool autoConfigureAngle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003D1 RID: 977
		// (set) Token: 0x060003D2 RID: 978
		public extern float angle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003D3 RID: 979
		// (set) Token: 0x060003D4 RID: 980
		public extern bool useMotor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003D5 RID: 981
		// (set) Token: 0x060003D6 RID: 982
		public extern bool useLimits
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00007180 File Offset: 0x00005380
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00007196 File Offset: 0x00005396
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000071A0 File Offset: 0x000053A0
		// (set) Token: 0x060003DA RID: 986 RVA: 0x000071B6 File Offset: 0x000053B6
		public JointTranslationLimits2D limits
		{
			get
			{
				JointTranslationLimits2D jointTranslationLimits2D;
				this.get_limits_Injected(out jointTranslationLimits2D);
				return jointTranslationLimits2D;
			}
			set
			{
				this.set_limits_Injected(ref value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003DB RID: 987
		public extern JointLimitState2D limitState
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003DC RID: 988
		public extern float referenceAngle
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003DD RID: 989
		public extern float jointTranslation
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003DE RID: 990
		public extern float jointSpeed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003DF RID: 991
		[MethodImpl(4096)]
		public extern float GetMotorForce(float timeStep);

		// Token: 0x060003E1 RID: 993
		[MethodImpl(4096)]
		private extern void get_motor_Injected(out JointMotor2D ret);

		// Token: 0x060003E2 RID: 994
		[MethodImpl(4096)]
		private extern void set_motor_Injected(ref JointMotor2D value);

		// Token: 0x060003E3 RID: 995
		[MethodImpl(4096)]
		private extern void get_limits_Injected(out JointTranslationLimits2D ret);

		// Token: 0x060003E4 RID: 996
		[MethodImpl(4096)]
		private extern void set_limits_Injected(ref JointTranslationLimits2D value);
	}
}

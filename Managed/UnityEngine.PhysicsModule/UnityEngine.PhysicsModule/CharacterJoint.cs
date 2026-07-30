using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeClass("Unity::CharacterJoint")]
	[NativeHeader("Modules/Physics/CharacterJoint.h")]
	public class CharacterJoint : Joint
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00003318 File Offset: 0x00001518
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000332E File Offset: 0x0000152E
		public Vector3 swingAxis
		{
			get
			{
				Vector3 vector;
				this.get_swingAxis_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_swingAxis_Injected(ref value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00003338 File Offset: 0x00001538
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000334E File Offset: 0x0000154E
		public SoftJointLimitSpring twistLimitSpring
		{
			get
			{
				SoftJointLimitSpring softJointLimitSpring;
				this.get_twistLimitSpring_Injected(out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				this.set_twistLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00003358 File Offset: 0x00001558
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000336E File Offset: 0x0000156E
		public SoftJointLimitSpring swingLimitSpring
		{
			get
			{
				SoftJointLimitSpring softJointLimitSpring;
				this.get_swingLimitSpring_Injected(out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				this.set_swingLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00003378 File Offset: 0x00001578
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000338E File Offset: 0x0000158E
		public SoftJointLimit lowTwistLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.get_lowTwistLimit_Injected(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.set_lowTwistLimit_Injected(ref value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00003398 File Offset: 0x00001598
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000033AE File Offset: 0x000015AE
		public SoftJointLimit highTwistLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.get_highTwistLimit_Injected(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.set_highTwistLimit_Injected(ref value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000033B8 File Offset: 0x000015B8
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000033CE File Offset: 0x000015CE
		public SoftJointLimit swing1Limit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.get_swing1Limit_Injected(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.set_swing1Limit_Injected(ref value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000033D8 File Offset: 0x000015D8
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000033EE File Offset: 0x000015EE
		public SoftJointLimit swing2Limit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.get_swing2Limit_Injected(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.set_swing2Limit_Injected(ref value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001B8 RID: 440
		// (set) Token: 0x060001B9 RID: 441
		public extern bool enableProjection
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001BA RID: 442
		// (set) Token: 0x060001BB RID: 443
		public extern float projectionDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001BC RID: 444
		// (set) Token: 0x060001BD RID: 445
		public extern float projectionAngle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060001BF RID: 447
		[MethodImpl(4096)]
		private extern void get_swingAxis_Injected(out Vector3 ret);

		// Token: 0x060001C0 RID: 448
		[MethodImpl(4096)]
		private extern void set_swingAxis_Injected(ref Vector3 value);

		// Token: 0x060001C1 RID: 449
		[MethodImpl(4096)]
		private extern void get_twistLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x060001C2 RID: 450
		[MethodImpl(4096)]
		private extern void set_twistLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x060001C3 RID: 451
		[MethodImpl(4096)]
		private extern void get_swingLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x060001C4 RID: 452
		[MethodImpl(4096)]
		private extern void set_swingLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x060001C5 RID: 453
		[MethodImpl(4096)]
		private extern void get_lowTwistLimit_Injected(out SoftJointLimit ret);

		// Token: 0x060001C6 RID: 454
		[MethodImpl(4096)]
		private extern void set_lowTwistLimit_Injected(ref SoftJointLimit value);

		// Token: 0x060001C7 RID: 455
		[MethodImpl(4096)]
		private extern void get_highTwistLimit_Injected(out SoftJointLimit ret);

		// Token: 0x060001C8 RID: 456
		[MethodImpl(4096)]
		private extern void set_highTwistLimit_Injected(ref SoftJointLimit value);

		// Token: 0x060001C9 RID: 457
		[MethodImpl(4096)]
		private extern void get_swing1Limit_Injected(out SoftJointLimit ret);

		// Token: 0x060001CA RID: 458
		[MethodImpl(4096)]
		private extern void set_swing1Limit_Injected(ref SoftJointLimit value);

		// Token: 0x060001CB RID: 459
		[MethodImpl(4096)]
		private extern void get_swing2Limit_Injected(out SoftJointLimit ret);

		// Token: 0x060001CC RID: 460
		[MethodImpl(4096)]
		private extern void set_swing2Limit_Injected(ref SoftJointLimit value);

		// Token: 0x04000070 RID: 112
		[Obsolete("TargetRotation not in use for Unity 5 and assumed disabled.", true)]
		public Quaternion targetRotation;

		// Token: 0x04000071 RID: 113
		[Obsolete("TargetAngularVelocity not in use for Unity 5 and assumed disabled.", true)]
		public Vector3 targetAngularVelocity;

		// Token: 0x04000072 RID: 114
		[Obsolete("RotationDrive not in use for Unity 5 and assumed disabled.")]
		public JointDrive rotationDrive;
	}
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	[NativeClass("Unity::ConfigurableJoint")]
	[NativeHeader("Modules/Physics/ConfigurableJoint.h")]
	public class ConfigurableJoint : Joint
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000033F8 File Offset: 0x000015F8
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000340E File Offset: 0x0000160E
		public Vector3 secondaryAxis
		{
			get
			{
				Vector3 vector;
				this.get_secondaryAxis_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_secondaryAxis_Injected(ref value);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001CF RID: 463
		// (set) Token: 0x060001D0 RID: 464
		public extern ConfigurableJointMotion xMotion
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001D1 RID: 465
		// (set) Token: 0x060001D2 RID: 466
		public extern ConfigurableJointMotion yMotion
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001D3 RID: 467
		// (set) Token: 0x060001D4 RID: 468
		public extern ConfigurableJointMotion zMotion
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001D5 RID: 469
		// (set) Token: 0x060001D6 RID: 470
		public extern ConfigurableJointMotion angularXMotion
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001D7 RID: 471
		// (set) Token: 0x060001D8 RID: 472
		public extern ConfigurableJointMotion angularYMotion
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001D9 RID: 473
		// (set) Token: 0x060001DA RID: 474
		public extern ConfigurableJointMotion angularZMotion
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00003418 File Offset: 0x00001618
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000342E File Offset: 0x0000162E
		public SoftJointLimitSpring linearLimitSpring
		{
			get
			{
				SoftJointLimitSpring softJointLimitSpring;
				this.get_linearLimitSpring_Injected(out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				this.set_linearLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00003438 File Offset: 0x00001638
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000344E File Offset: 0x0000164E
		public SoftJointLimitSpring angularXLimitSpring
		{
			get
			{
				SoftJointLimitSpring softJointLimitSpring;
				this.get_angularXLimitSpring_Injected(out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				this.set_angularXLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00003458 File Offset: 0x00001658
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000346E File Offset: 0x0000166E
		public SoftJointLimitSpring angularYZLimitSpring
		{
			get
			{
				SoftJointLimitSpring softJointLimitSpring;
				this.get_angularYZLimitSpring_Injected(out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				this.set_angularYZLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00003478 File Offset: 0x00001678
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000348E File Offset: 0x0000168E
		public SoftJointLimit linearLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.get_linearLimit_Injected(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.set_linearLimit_Injected(ref value);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00003498 File Offset: 0x00001698
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000034AE File Offset: 0x000016AE
		public SoftJointLimit lowAngularXLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.get_lowAngularXLimit_Injected(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.set_lowAngularXLimit_Injected(ref value);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000034B8 File Offset: 0x000016B8
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000034CE File Offset: 0x000016CE
		public SoftJointLimit highAngularXLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.get_highAngularXLimit_Injected(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.set_highAngularXLimit_Injected(ref value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000034D8 File Offset: 0x000016D8
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x000034EE File Offset: 0x000016EE
		public SoftJointLimit angularYLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.get_angularYLimit_Injected(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.set_angularYLimit_Injected(ref value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000034F8 File Offset: 0x000016F8
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000350E File Offset: 0x0000170E
		public SoftJointLimit angularZLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.get_angularZLimit_Injected(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.set_angularZLimit_Injected(ref value);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00003518 File Offset: 0x00001718
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000352E File Offset: 0x0000172E
		public Vector3 targetPosition
		{
			get
			{
				Vector3 vector;
				this.get_targetPosition_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_targetPosition_Injected(ref value);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00003538 File Offset: 0x00001738
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000354E File Offset: 0x0000174E
		public Vector3 targetVelocity
		{
			get
			{
				Vector3 vector;
				this.get_targetVelocity_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_targetVelocity_Injected(ref value);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00003558 File Offset: 0x00001758
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000356E File Offset: 0x0000176E
		public JointDrive xDrive
		{
			get
			{
				JointDrive jointDrive;
				this.get_xDrive_Injected(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.set_xDrive_Injected(ref value);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00003578 File Offset: 0x00001778
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000358E File Offset: 0x0000178E
		public JointDrive yDrive
		{
			get
			{
				JointDrive jointDrive;
				this.get_yDrive_Injected(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.set_yDrive_Injected(ref value);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00003598 File Offset: 0x00001798
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000035AE File Offset: 0x000017AE
		public JointDrive zDrive
		{
			get
			{
				JointDrive jointDrive;
				this.get_zDrive_Injected(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.set_zDrive_Injected(ref value);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000035B8 File Offset: 0x000017B8
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000035CE File Offset: 0x000017CE
		public Quaternion targetRotation
		{
			get
			{
				Quaternion quaternion;
				this.get_targetRotation_Injected(out quaternion);
				return quaternion;
			}
			set
			{
				this.set_targetRotation_Injected(ref value);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000035D8 File Offset: 0x000017D8
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000035EE File Offset: 0x000017EE
		public Vector3 targetAngularVelocity
		{
			get
			{
				Vector3 vector;
				this.get_targetAngularVelocity_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_targetAngularVelocity_Injected(ref value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001F9 RID: 505
		// (set) Token: 0x060001FA RID: 506
		public extern RotationDriveMode rotationDriveMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000035F8 File Offset: 0x000017F8
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000360E File Offset: 0x0000180E
		public JointDrive angularXDrive
		{
			get
			{
				JointDrive jointDrive;
				this.get_angularXDrive_Injected(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.set_angularXDrive_Injected(ref value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00003618 File Offset: 0x00001818
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000362E File Offset: 0x0000182E
		public JointDrive angularYZDrive
		{
			get
			{
				JointDrive jointDrive;
				this.get_angularYZDrive_Injected(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.set_angularYZDrive_Injected(ref value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00003638 File Offset: 0x00001838
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000364E File Offset: 0x0000184E
		public JointDrive slerpDrive
		{
			get
			{
				JointDrive jointDrive;
				this.get_slerpDrive_Injected(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.set_slerpDrive_Injected(ref value);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000201 RID: 513
		// (set) Token: 0x06000202 RID: 514
		public extern JointProjectionMode projectionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000203 RID: 515
		// (set) Token: 0x06000204 RID: 516
		public extern float projectionDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000205 RID: 517
		// (set) Token: 0x06000206 RID: 518
		public extern float projectionAngle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000207 RID: 519
		// (set) Token: 0x06000208 RID: 520
		public extern bool configuredInWorldSpace
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000209 RID: 521
		// (set) Token: 0x0600020A RID: 522
		public extern bool swapBodies
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600020C RID: 524
		[MethodImpl(4096)]
		private extern void get_secondaryAxis_Injected(out Vector3 ret);

		// Token: 0x0600020D RID: 525
		[MethodImpl(4096)]
		private extern void set_secondaryAxis_Injected(ref Vector3 value);

		// Token: 0x0600020E RID: 526
		[MethodImpl(4096)]
		private extern void get_linearLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x0600020F RID: 527
		[MethodImpl(4096)]
		private extern void set_linearLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x06000210 RID: 528
		[MethodImpl(4096)]
		private extern void get_angularXLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x06000211 RID: 529
		[MethodImpl(4096)]
		private extern void set_angularXLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x06000212 RID: 530
		[MethodImpl(4096)]
		private extern void get_angularYZLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x06000213 RID: 531
		[MethodImpl(4096)]
		private extern void set_angularYZLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x06000214 RID: 532
		[MethodImpl(4096)]
		private extern void get_linearLimit_Injected(out SoftJointLimit ret);

		// Token: 0x06000215 RID: 533
		[MethodImpl(4096)]
		private extern void set_linearLimit_Injected(ref SoftJointLimit value);

		// Token: 0x06000216 RID: 534
		[MethodImpl(4096)]
		private extern void get_lowAngularXLimit_Injected(out SoftJointLimit ret);

		// Token: 0x06000217 RID: 535
		[MethodImpl(4096)]
		private extern void set_lowAngularXLimit_Injected(ref SoftJointLimit value);

		// Token: 0x06000218 RID: 536
		[MethodImpl(4096)]
		private extern void get_highAngularXLimit_Injected(out SoftJointLimit ret);

		// Token: 0x06000219 RID: 537
		[MethodImpl(4096)]
		private extern void set_highAngularXLimit_Injected(ref SoftJointLimit value);

		// Token: 0x0600021A RID: 538
		[MethodImpl(4096)]
		private extern void get_angularYLimit_Injected(out SoftJointLimit ret);

		// Token: 0x0600021B RID: 539
		[MethodImpl(4096)]
		private extern void set_angularYLimit_Injected(ref SoftJointLimit value);

		// Token: 0x0600021C RID: 540
		[MethodImpl(4096)]
		private extern void get_angularZLimit_Injected(out SoftJointLimit ret);

		// Token: 0x0600021D RID: 541
		[MethodImpl(4096)]
		private extern void set_angularZLimit_Injected(ref SoftJointLimit value);

		// Token: 0x0600021E RID: 542
		[MethodImpl(4096)]
		private extern void get_targetPosition_Injected(out Vector3 ret);

		// Token: 0x0600021F RID: 543
		[MethodImpl(4096)]
		private extern void set_targetPosition_Injected(ref Vector3 value);

		// Token: 0x06000220 RID: 544
		[MethodImpl(4096)]
		private extern void get_targetVelocity_Injected(out Vector3 ret);

		// Token: 0x06000221 RID: 545
		[MethodImpl(4096)]
		private extern void set_targetVelocity_Injected(ref Vector3 value);

		// Token: 0x06000222 RID: 546
		[MethodImpl(4096)]
		private extern void get_xDrive_Injected(out JointDrive ret);

		// Token: 0x06000223 RID: 547
		[MethodImpl(4096)]
		private extern void set_xDrive_Injected(ref JointDrive value);

		// Token: 0x06000224 RID: 548
		[MethodImpl(4096)]
		private extern void get_yDrive_Injected(out JointDrive ret);

		// Token: 0x06000225 RID: 549
		[MethodImpl(4096)]
		private extern void set_yDrive_Injected(ref JointDrive value);

		// Token: 0x06000226 RID: 550
		[MethodImpl(4096)]
		private extern void get_zDrive_Injected(out JointDrive ret);

		// Token: 0x06000227 RID: 551
		[MethodImpl(4096)]
		private extern void set_zDrive_Injected(ref JointDrive value);

		// Token: 0x06000228 RID: 552
		[MethodImpl(4096)]
		private extern void get_targetRotation_Injected(out Quaternion ret);

		// Token: 0x06000229 RID: 553
		[MethodImpl(4096)]
		private extern void set_targetRotation_Injected(ref Quaternion value);

		// Token: 0x0600022A RID: 554
		[MethodImpl(4096)]
		private extern void get_targetAngularVelocity_Injected(out Vector3 ret);

		// Token: 0x0600022B RID: 555
		[MethodImpl(4096)]
		private extern void set_targetAngularVelocity_Injected(ref Vector3 value);

		// Token: 0x0600022C RID: 556
		[MethodImpl(4096)]
		private extern void get_angularXDrive_Injected(out JointDrive ret);

		// Token: 0x0600022D RID: 557
		[MethodImpl(4096)]
		private extern void set_angularXDrive_Injected(ref JointDrive value);

		// Token: 0x0600022E RID: 558
		[MethodImpl(4096)]
		private extern void get_angularYZDrive_Injected(out JointDrive ret);

		// Token: 0x0600022F RID: 559
		[MethodImpl(4096)]
		private extern void set_angularYZDrive_Injected(ref JointDrive value);

		// Token: 0x06000230 RID: 560
		[MethodImpl(4096)]
		private extern void get_slerpDrive_Injected(out JointDrive ret);

		// Token: 0x06000231 RID: 561
		[MethodImpl(4096)]
		private extern void set_slerpDrive_Injected(ref JointDrive value);
	}
}

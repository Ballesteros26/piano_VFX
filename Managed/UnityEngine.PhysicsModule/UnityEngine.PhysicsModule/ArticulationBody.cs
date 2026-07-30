using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	[NativeClass("Unity::ArticulationBody")]
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public class ArticulationBody : Behaviour
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000277 RID: 631
		// (set) Token: 0x06000278 RID: 632
		public extern ArticulationJointType jointType
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00003F38 File Offset: 0x00002138
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00003F4E File Offset: 0x0000214E
		public Vector3 anchorPosition
		{
			get
			{
				Vector3 vector;
				this.get_anchorPosition_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchorPosition_Injected(ref value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00003F58 File Offset: 0x00002158
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00003F6E File Offset: 0x0000216E
		public Vector3 parentAnchorPosition
		{
			get
			{
				Vector3 vector;
				this.get_parentAnchorPosition_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_parentAnchorPosition_Injected(ref value);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00003F78 File Offset: 0x00002178
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00003F8E File Offset: 0x0000218E
		public Quaternion anchorRotation
		{
			get
			{
				Quaternion quaternion;
				this.get_anchorRotation_Injected(out quaternion);
				return quaternion;
			}
			set
			{
				this.set_anchorRotation_Injected(ref value);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00003F98 File Offset: 0x00002198
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00003FAE File Offset: 0x000021AE
		public Quaternion parentAnchorRotation
		{
			get
			{
				Quaternion quaternion;
				this.get_parentAnchorRotation_Injected(out quaternion);
				return quaternion;
			}
			set
			{
				this.set_parentAnchorRotation_Injected(ref value);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000281 RID: 641
		public extern bool isRoot
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000282 RID: 642
		// (set) Token: 0x06000283 RID: 643
		public extern ArticulationDofLock linearLockX
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000284 RID: 644
		// (set) Token: 0x06000285 RID: 645
		public extern ArticulationDofLock linearLockY
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000286 RID: 646
		// (set) Token: 0x06000287 RID: 647
		public extern ArticulationDofLock linearLockZ
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000288 RID: 648
		// (set) Token: 0x06000289 RID: 649
		public extern ArticulationDofLock swingYLock
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600028A RID: 650
		// (set) Token: 0x0600028B RID: 651
		public extern ArticulationDofLock swingZLock
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600028C RID: 652
		// (set) Token: 0x0600028D RID: 653
		public extern ArticulationDofLock twistLock
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00003FB8 File Offset: 0x000021B8
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00003FCE File Offset: 0x000021CE
		public ArticulationDrive xDrive
		{
			get
			{
				ArticulationDrive articulationDrive;
				this.get_xDrive_Injected(out articulationDrive);
				return articulationDrive;
			}
			set
			{
				this.set_xDrive_Injected(ref value);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00003FD8 File Offset: 0x000021D8
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00003FEE File Offset: 0x000021EE
		public ArticulationDrive yDrive
		{
			get
			{
				ArticulationDrive articulationDrive;
				this.get_yDrive_Injected(out articulationDrive);
				return articulationDrive;
			}
			set
			{
				this.set_yDrive_Injected(ref value);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00003FF8 File Offset: 0x000021F8
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000400E File Offset: 0x0000220E
		public ArticulationDrive zDrive
		{
			get
			{
				ArticulationDrive articulationDrive;
				this.get_zDrive_Injected(out articulationDrive);
				return articulationDrive;
			}
			set
			{
				this.set_zDrive_Injected(ref value);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000294 RID: 660
		// (set) Token: 0x06000295 RID: 661
		public extern bool immovable
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000296 RID: 662
		// (set) Token: 0x06000297 RID: 663
		public extern bool useGravity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000298 RID: 664
		// (set) Token: 0x06000299 RID: 665
		public extern float linearDamping
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600029A RID: 666
		// (set) Token: 0x0600029B RID: 667
		public extern float angularDamping
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600029C RID: 668
		// (set) Token: 0x0600029D RID: 669
		public extern float jointFriction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00004018 File Offset: 0x00002218
		public void AddForce(Vector3 force)
		{
			this.AddForce_Injected(ref force);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00004022 File Offset: 0x00002222
		public void AddRelativeForce(Vector3 force)
		{
			this.AddRelativeForce_Injected(ref force);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000402C File Offset: 0x0000222C
		public void AddTorque(Vector3 torque)
		{
			this.AddTorque_Injected(ref torque);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00004036 File Offset: 0x00002236
		public void AddRelativeTorque(Vector3 torque)
		{
			this.AddRelativeTorque_Injected(ref torque);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00004040 File Offset: 0x00002240
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			this.AddForceAtPosition_Injected(ref force, ref position);
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000404C File Offset: 0x0000224C
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.get_velocity_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00004064 File Offset: 0x00002264
		public Vector3 angularVelocity
		{
			get
			{
				Vector3 vector;
				this.get_angularVelocity_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002A5 RID: 677
		// (set) Token: 0x060002A6 RID: 678
		public extern float mass
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000407C File Offset: 0x0000227C
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00004092 File Offset: 0x00002292
		public Vector3 centerOfMass
		{
			get
			{
				Vector3 vector;
				this.get_centerOfMass_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_centerOfMass_Injected(ref value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000409C File Offset: 0x0000229C
		public Vector3 worldCenterOfMass
		{
			get
			{
				Vector3 vector;
				this.get_worldCenterOfMass_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000040B4 File Offset: 0x000022B4
		// (set) Token: 0x060002AB RID: 683 RVA: 0x000040CA File Offset: 0x000022CA
		public Vector3 inertiaTensor
		{
			get
			{
				Vector3 vector;
				this.get_inertiaTensor_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_inertiaTensor_Injected(ref value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000040D4 File Offset: 0x000022D4
		// (set) Token: 0x060002AD RID: 685 RVA: 0x000040EA File Offset: 0x000022EA
		public Quaternion inertiaTensorRotation
		{
			get
			{
				Quaternion quaternion;
				this.get_inertiaTensorRotation_Injected(out quaternion);
				return quaternion;
			}
			set
			{
				this.set_inertiaTensorRotation_Injected(ref value);
			}
		}

		// Token: 0x060002AE RID: 686
		[MethodImpl(4096)]
		public extern void ResetCenterOfMass();

		// Token: 0x060002AF RID: 687
		[MethodImpl(4096)]
		public extern void ResetInertiaTensor();

		// Token: 0x060002B0 RID: 688
		[MethodImpl(4096)]
		public extern void Sleep();

		// Token: 0x060002B1 RID: 689
		[MethodImpl(4096)]
		public extern bool IsSleeping();

		// Token: 0x060002B2 RID: 690
		[MethodImpl(4096)]
		public extern void WakeUp();

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002B3 RID: 691
		// (set) Token: 0x060002B4 RID: 692
		public extern float sleepThreshold
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002B5 RID: 693
		// (set) Token: 0x060002B6 RID: 694
		public extern int solverIterations
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002B7 RID: 695
		// (set) Token: 0x060002B8 RID: 696
		public extern int solverVelocityIterations
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002B9 RID: 697
		// (set) Token: 0x060002BA RID: 698
		public extern float maxAngularVelocity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002BB RID: 699
		// (set) Token: 0x060002BC RID: 700
		public extern float maxDepenetrationVelocity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000040F4 File Offset: 0x000022F4
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000410A File Offset: 0x0000230A
		public ArticulationReducedSpace jointPosition
		{
			get
			{
				ArticulationReducedSpace articulationReducedSpace;
				this.get_jointPosition_Injected(out articulationReducedSpace);
				return articulationReducedSpace;
			}
			set
			{
				this.set_jointPosition_Injected(ref value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00004114 File Offset: 0x00002314
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000412A File Offset: 0x0000232A
		public ArticulationReducedSpace jointVelocity
		{
			get
			{
				ArticulationReducedSpace articulationReducedSpace;
				this.get_jointVelocity_Injected(out articulationReducedSpace);
				return articulationReducedSpace;
			}
			set
			{
				this.set_jointVelocity_Injected(ref value);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00004134 File Offset: 0x00002334
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000414A File Offset: 0x0000234A
		public ArticulationReducedSpace jointAcceleration
		{
			get
			{
				ArticulationReducedSpace articulationReducedSpace;
				this.get_jointAcceleration_Injected(out articulationReducedSpace);
				return articulationReducedSpace;
			}
			set
			{
				this.set_jointAcceleration_Injected(ref value);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00004154 File Offset: 0x00002354
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000416A File Offset: 0x0000236A
		public ArticulationReducedSpace jointForce
		{
			get
			{
				ArticulationReducedSpace articulationReducedSpace;
				this.get_jointForce_Injected(out articulationReducedSpace);
				return articulationReducedSpace;
			}
			set
			{
				this.set_jointForce_Injected(ref value);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002C5 RID: 709
		public extern int dofCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00004174 File Offset: 0x00002374
		public void TeleportRoot(Vector3 position, Quaternion rotation)
		{
			this.TeleportRoot_Injected(ref position, ref rotation);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00004180 File Offset: 0x00002380
		public Vector3 GetClosestPoint(Vector3 point)
		{
			Vector3 vector;
			this.GetClosestPoint_Injected(ref point, out vector);
			return vector;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00004198 File Offset: 0x00002398
		public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
		{
			Vector3 vector;
			this.GetRelativePointVelocity_Injected(ref relativePoint, out vector);
			return vector;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000041B0 File Offset: 0x000023B0
		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			Vector3 vector;
			this.GetPointVelocity_Injected(ref worldPoint, out vector);
			return vector;
		}

		// Token: 0x060002CB RID: 715
		[MethodImpl(4096)]
		private extern void get_anchorPosition_Injected(out Vector3 ret);

		// Token: 0x060002CC RID: 716
		[MethodImpl(4096)]
		private extern void set_anchorPosition_Injected(ref Vector3 value);

		// Token: 0x060002CD RID: 717
		[MethodImpl(4096)]
		private extern void get_parentAnchorPosition_Injected(out Vector3 ret);

		// Token: 0x060002CE RID: 718
		[MethodImpl(4096)]
		private extern void set_parentAnchorPosition_Injected(ref Vector3 value);

		// Token: 0x060002CF RID: 719
		[MethodImpl(4096)]
		private extern void get_anchorRotation_Injected(out Quaternion ret);

		// Token: 0x060002D0 RID: 720
		[MethodImpl(4096)]
		private extern void set_anchorRotation_Injected(ref Quaternion value);

		// Token: 0x060002D1 RID: 721
		[MethodImpl(4096)]
		private extern void get_parentAnchorRotation_Injected(out Quaternion ret);

		// Token: 0x060002D2 RID: 722
		[MethodImpl(4096)]
		private extern void set_parentAnchorRotation_Injected(ref Quaternion value);

		// Token: 0x060002D3 RID: 723
		[MethodImpl(4096)]
		private extern void get_xDrive_Injected(out ArticulationDrive ret);

		// Token: 0x060002D4 RID: 724
		[MethodImpl(4096)]
		private extern void set_xDrive_Injected(ref ArticulationDrive value);

		// Token: 0x060002D5 RID: 725
		[MethodImpl(4096)]
		private extern void get_yDrive_Injected(out ArticulationDrive ret);

		// Token: 0x060002D6 RID: 726
		[MethodImpl(4096)]
		private extern void set_yDrive_Injected(ref ArticulationDrive value);

		// Token: 0x060002D7 RID: 727
		[MethodImpl(4096)]
		private extern void get_zDrive_Injected(out ArticulationDrive ret);

		// Token: 0x060002D8 RID: 728
		[MethodImpl(4096)]
		private extern void set_zDrive_Injected(ref ArticulationDrive value);

		// Token: 0x060002D9 RID: 729
		[MethodImpl(4096)]
		private extern void AddForce_Injected(ref Vector3 force);

		// Token: 0x060002DA RID: 730
		[MethodImpl(4096)]
		private extern void AddRelativeForce_Injected(ref Vector3 force);

		// Token: 0x060002DB RID: 731
		[MethodImpl(4096)]
		private extern void AddTorque_Injected(ref Vector3 torque);

		// Token: 0x060002DC RID: 732
		[MethodImpl(4096)]
		private extern void AddRelativeTorque_Injected(ref Vector3 torque);

		// Token: 0x060002DD RID: 733
		[MethodImpl(4096)]
		private extern void AddForceAtPosition_Injected(ref Vector3 force, ref Vector3 position);

		// Token: 0x060002DE RID: 734
		[MethodImpl(4096)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x060002DF RID: 735
		[MethodImpl(4096)]
		private extern void get_angularVelocity_Injected(out Vector3 ret);

		// Token: 0x060002E0 RID: 736
		[MethodImpl(4096)]
		private extern void get_centerOfMass_Injected(out Vector3 ret);

		// Token: 0x060002E1 RID: 737
		[MethodImpl(4096)]
		private extern void set_centerOfMass_Injected(ref Vector3 value);

		// Token: 0x060002E2 RID: 738
		[MethodImpl(4096)]
		private extern void get_worldCenterOfMass_Injected(out Vector3 ret);

		// Token: 0x060002E3 RID: 739
		[MethodImpl(4096)]
		private extern void get_inertiaTensor_Injected(out Vector3 ret);

		// Token: 0x060002E4 RID: 740
		[MethodImpl(4096)]
		private extern void set_inertiaTensor_Injected(ref Vector3 value);

		// Token: 0x060002E5 RID: 741
		[MethodImpl(4096)]
		private extern void get_inertiaTensorRotation_Injected(out Quaternion ret);

		// Token: 0x060002E6 RID: 742
		[MethodImpl(4096)]
		private extern void set_inertiaTensorRotation_Injected(ref Quaternion value);

		// Token: 0x060002E7 RID: 743
		[MethodImpl(4096)]
		private extern void get_jointPosition_Injected(out ArticulationReducedSpace ret);

		// Token: 0x060002E8 RID: 744
		[MethodImpl(4096)]
		private extern void set_jointPosition_Injected(ref ArticulationReducedSpace value);

		// Token: 0x060002E9 RID: 745
		[MethodImpl(4096)]
		private extern void get_jointVelocity_Injected(out ArticulationReducedSpace ret);

		// Token: 0x060002EA RID: 746
		[MethodImpl(4096)]
		private extern void set_jointVelocity_Injected(ref ArticulationReducedSpace value);

		// Token: 0x060002EB RID: 747
		[MethodImpl(4096)]
		private extern void get_jointAcceleration_Injected(out ArticulationReducedSpace ret);

		// Token: 0x060002EC RID: 748
		[MethodImpl(4096)]
		private extern void set_jointAcceleration_Injected(ref ArticulationReducedSpace value);

		// Token: 0x060002ED RID: 749
		[MethodImpl(4096)]
		private extern void get_jointForce_Injected(out ArticulationReducedSpace ret);

		// Token: 0x060002EE RID: 750
		[MethodImpl(4096)]
		private extern void set_jointForce_Injected(ref ArticulationReducedSpace value);

		// Token: 0x060002EF RID: 751
		[MethodImpl(4096)]
		private extern void TeleportRoot_Injected(ref Vector3 position, ref Quaternion rotation);

		// Token: 0x060002F0 RID: 752
		[MethodImpl(4096)]
		private extern void GetClosestPoint_Injected(ref Vector3 point, out Vector3 ret);

		// Token: 0x060002F1 RID: 753
		[MethodImpl(4096)]
		private extern void GetRelativePointVelocity_Injected(ref Vector3 relativePoint, out Vector3 ret);

		// Token: 0x060002F2 RID: 754
		[MethodImpl(4096)]
		private extern void GetPointVelocity_Injected(ref Vector3 worldPoint, out Vector3 ret);
	}
}

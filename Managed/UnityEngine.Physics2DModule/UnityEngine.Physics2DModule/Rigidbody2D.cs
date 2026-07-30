using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	[NativeHeader("Modules/Physics2D/Public/Rigidbody2D.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class Rigidbody2D : Component
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00006284 File Offset: 0x00004484
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000629A File Offset: 0x0000449A
		public Vector2 position
		{
			get
			{
				Vector2 vector;
				this.get_position_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_position_Injected(ref value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000257 RID: 599
		// (set) Token: 0x06000258 RID: 600
		public extern float rotation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000062A4 File Offset: 0x000044A4
		public void SetRotation(float angle)
		{
			this.SetRotation_Angle(angle);
		}

		// Token: 0x0600025A RID: 602
		[NativeMethod("SetRotation")]
		[MethodImpl(4096)]
		private extern void SetRotation_Angle(float angle);

		// Token: 0x0600025B RID: 603 RVA: 0x000062AF File Offset: 0x000044AF
		public void SetRotation(Quaternion rotation)
		{
			this.SetRotation_Quaternion(rotation);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000062BA File Offset: 0x000044BA
		[NativeMethod("SetRotation")]
		private void SetRotation_Quaternion(Quaternion rotation)
		{
			this.SetRotation_Quaternion_Injected(ref rotation);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000062C4 File Offset: 0x000044C4
		public void MovePosition(Vector2 position)
		{
			this.MovePosition_Injected(ref position);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000062CE File Offset: 0x000044CE
		public void MoveRotation(float angle)
		{
			this.MoveRotation_Angle(angle);
		}

		// Token: 0x0600025F RID: 607
		[NativeMethod("MoveRotation")]
		[MethodImpl(4096)]
		private extern void MoveRotation_Angle(float angle);

		// Token: 0x06000260 RID: 608 RVA: 0x000062D9 File Offset: 0x000044D9
		public void MoveRotation(Quaternion rotation)
		{
			this.MoveRotation_Quaternion(rotation);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000062E4 File Offset: 0x000044E4
		[NativeMethod("MoveRotation")]
		private void MoveRotation_Quaternion(Quaternion rotation)
		{
			this.MoveRotation_Quaternion_Injected(ref rotation);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000062F0 File Offset: 0x000044F0
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00006306 File Offset: 0x00004506
		public Vector2 velocity
		{
			get
			{
				Vector2 vector;
				this.get_velocity_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_velocity_Injected(ref value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000264 RID: 612
		// (set) Token: 0x06000265 RID: 613
		public extern float angularVelocity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000266 RID: 614
		// (set) Token: 0x06000267 RID: 615
		public extern bool useAutoMass
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000268 RID: 616
		// (set) Token: 0x06000269 RID: 617
		public extern float mass
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600026A RID: 618
		// (set) Token: 0x0600026B RID: 619
		[NativeMethod("Material")]
		public extern PhysicsMaterial2D sharedMaterial
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00006310 File Offset: 0x00004510
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00006326 File Offset: 0x00004526
		public Vector2 centerOfMass
		{
			get
			{
				Vector2 vector;
				this.get_centerOfMass_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_centerOfMass_Injected(ref value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00006330 File Offset: 0x00004530
		public Vector2 worldCenterOfMass
		{
			get
			{
				Vector2 vector;
				this.get_worldCenterOfMass_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600026F RID: 623
		// (set) Token: 0x06000270 RID: 624
		public extern float inertia
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000271 RID: 625
		// (set) Token: 0x06000272 RID: 626
		public extern float drag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000273 RID: 627
		// (set) Token: 0x06000274 RID: 628
		public extern float angularDrag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000275 RID: 629
		// (set) Token: 0x06000276 RID: 630
		public extern float gravityScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000277 RID: 631
		// (set) Token: 0x06000278 RID: 632
		public extern RigidbodyType2D bodyType
		{
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetBodyType_Binding")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000279 RID: 633
		[MethodImpl(4096)]
		internal extern void SetDragBehaviour(bool dragged);

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600027A RID: 634
		// (set) Token: 0x0600027B RID: 635
		public extern bool useFullKinematicContacts
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00006348 File Offset: 0x00004548
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00006363 File Offset: 0x00004563
		public bool isKinematic
		{
			get
			{
				return this.bodyType == RigidbodyType2D.Kinematic;
			}
			set
			{
				this.bodyType = (value ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600027E RID: 638
		// (set) Token: 0x0600027F RID: 639
		[NativeMethod("FreezeRotation")]
		[Obsolete("'fixedAngle' is no longer supported. Use constraints instead.", false)]
		public extern bool fixedAngle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000280 RID: 640
		// (set) Token: 0x06000281 RID: 641
		public extern bool freezeRotation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000282 RID: 642
		// (set) Token: 0x06000283 RID: 643
		public extern RigidbodyConstraints2D constraints
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000284 RID: 644
		[MethodImpl(4096)]
		public extern bool IsSleeping();

		// Token: 0x06000285 RID: 645
		[MethodImpl(4096)]
		public extern bool IsAwake();

		// Token: 0x06000286 RID: 646
		[MethodImpl(4096)]
		public extern void Sleep();

		// Token: 0x06000287 RID: 647
		[NativeMethod("Wake")]
		[MethodImpl(4096)]
		public extern void WakeUp();

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000288 RID: 648
		// (set) Token: 0x06000289 RID: 649
		public extern bool simulated
		{
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetSimulated_Binding")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600028A RID: 650
		// (set) Token: 0x0600028B RID: 651
		public extern RigidbodyInterpolation2D interpolation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600028C RID: 652
		// (set) Token: 0x0600028D RID: 653
		public extern RigidbodySleepMode2D sleepMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600028E RID: 654
		// (set) Token: 0x0600028F RID: 655
		public extern CollisionDetectionMode2D collisionDetectionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000290 RID: 656
		public extern int attachedColliderCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000291 RID: 657
		[MethodImpl(4096)]
		public extern bool IsTouching([NotNull] [Writable] Collider2D collider);

		// Token: 0x06000292 RID: 658 RVA: 0x00006374 File Offset: 0x00004574
		public bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter_Internal(collider, contactFilter);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000638E File Offset: 0x0000458E
		[NativeMethod("IsTouching")]
		private bool IsTouching_OtherColliderWithFilter_Internal([NotNull] [Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter_Internal_Injected(collider, ref contactFilter);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000639C File Offset: 0x0000459C
		public bool IsTouching(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter_Internal(contactFilter);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000063B5 File Offset: 0x000045B5
		[NativeMethod("IsTouching")]
		private bool IsTouching_AnyColliderWithFilter_Internal(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter_Internal_Injected(ref contactFilter);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000063C0 File Offset: 0x000045C0
		[ExcludeFromDocs]
		public bool IsTouchingLayers()
		{
			return this.IsTouchingLayers(-1);
		}

		// Token: 0x06000297 RID: 663
		[MethodImpl(4096)]
		public extern bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask);

		// Token: 0x06000298 RID: 664 RVA: 0x000063D9 File Offset: 0x000045D9
		public bool OverlapPoint(Vector2 point)
		{
			return this.OverlapPoint_Injected(ref point);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000063E4 File Offset: 0x000045E4
		public ColliderDistance2D Distance([Writable] Collider2D collider)
		{
			bool flag = collider == null;
			if (flag)
			{
				throw new ArgumentNullException("Collider cannot be null.");
			}
			bool flag2 = collider.attachedRigidbody == this;
			if (flag2)
			{
				throw new ArgumentException("The collider cannot be attached to the Rigidbody2D being searched.");
			}
			return this.Distance_Internal(collider);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00006430 File Offset: 0x00004630
		[NativeMethod("Distance")]
		private ColliderDistance2D Distance_Internal([Writable] [NotNull] Collider2D collider)
		{
			ColliderDistance2D colliderDistance2D;
			this.Distance_Internal_Injected(collider, out colliderDistance2D);
			return colliderDistance2D;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00006448 File Offset: 0x00004648
		public Vector2 ClosestPoint(Vector2 position)
		{
			return Physics2D.ClosestPoint(position, this);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00006461 File Offset: 0x00004661
		[ExcludeFromDocs]
		public void AddForce(Vector2 force)
		{
			this.AddForce(force, ForceMode2D.Force);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000646D File Offset: 0x0000466D
		public void AddForce(Vector2 force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			this.AddForce_Injected(ref force, mode);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00006478 File Offset: 0x00004678
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector2 relativeForce)
		{
			this.AddRelativeForce(relativeForce, ForceMode2D.Force);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00006484 File Offset: 0x00004684
		public void AddRelativeForce(Vector2 relativeForce, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			this.AddRelativeForce_Injected(ref relativeForce, mode);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000648F File Offset: 0x0000468F
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector2 force, Vector2 position)
		{
			this.AddForceAtPosition(force, position, ForceMode2D.Force);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000649C File Offset: 0x0000469C
		public void AddForceAtPosition(Vector2 force, Vector2 position, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			this.AddForceAtPosition_Injected(ref force, ref position, mode);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000064A9 File Offset: 0x000046A9
		[ExcludeFromDocs]
		public void AddTorque(float torque)
		{
			this.AddTorque(torque, ForceMode2D.Force);
		}

		// Token: 0x060002A3 RID: 675
		[MethodImpl(4096)]
		public extern void AddTorque(float torque, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060002A4 RID: 676 RVA: 0x000064B8 File Offset: 0x000046B8
		public Vector2 GetPoint(Vector2 point)
		{
			Vector2 vector;
			this.GetPoint_Injected(ref point, out vector);
			return vector;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000064D0 File Offset: 0x000046D0
		public Vector2 GetRelativePoint(Vector2 relativePoint)
		{
			Vector2 vector;
			this.GetRelativePoint_Injected(ref relativePoint, out vector);
			return vector;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000064E8 File Offset: 0x000046E8
		public Vector2 GetVector(Vector2 vector)
		{
			Vector2 vector2;
			this.GetVector_Injected(ref vector, out vector2);
			return vector2;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00006500 File Offset: 0x00004700
		public Vector2 GetRelativeVector(Vector2 relativeVector)
		{
			Vector2 vector;
			this.GetRelativeVector_Injected(ref relativeVector, out vector);
			return vector;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00006518 File Offset: 0x00004718
		public Vector2 GetPointVelocity(Vector2 point)
		{
			Vector2 vector;
			this.GetPointVelocity_Injected(ref point, out vector);
			return vector;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00006530 File Offset: 0x00004730
		public Vector2 GetRelativePointVelocity(Vector2 relativePoint)
		{
			Vector2 vector;
			this.GetRelativePointVelocity_Injected(ref relativePoint, out vector);
			return vector;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00006548 File Offset: 0x00004748
		public int OverlapCollider(ContactFilter2D contactFilter, [Out] Collider2D[] results)
		{
			return this.OverlapColliderArray_Internal(contactFilter, results);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00006562 File Offset: 0x00004762
		[NativeMethod("OverlapColliderArray_Binding")]
		private int OverlapColliderArray_Internal(ContactFilter2D contactFilter, [NotNull] Collider2D[] results)
		{
			return this.OverlapColliderArray_Internal_Injected(ref contactFilter, results);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00006570 File Offset: 0x00004770
		public int OverlapCollider(ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return this.OverlapColliderList_Internal(contactFilter, results);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000658A File Offset: 0x0000478A
		[NativeMethod("OverlapColliderList_Binding")]
		private int OverlapColliderList_Internal(ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			return this.OverlapColliderList_Internal_Injected(ref contactFilter, results);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00006598 File Offset: 0x00004798
		public int GetContacts(ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000065C0 File Offset: 0x000047C0
		public int GetContacts(List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000065E8 File Offset: 0x000047E8
		public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00006604 File Offset: 0x00004804
		public int GetContacts(ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00006620 File Offset: 0x00004820
		public int GetContacts(Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00006648 File Offset: 0x00004848
		public int GetContacts(List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00006670 File Offset: 0x00004870
		public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000668C File Offset: 0x0000488C
		public int GetContacts(ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000066A8 File Offset: 0x000048A8
		public int GetAttachedColliders([Out] Collider2D[] results)
		{
			return this.GetAttachedCollidersArray_Internal(results);
		}

		// Token: 0x060002B7 RID: 695
		[NativeMethod("GetAttachedCollidersArray_Binding")]
		[MethodImpl(4096)]
		private extern int GetAttachedCollidersArray_Internal([NotNull] Collider2D[] results);

		// Token: 0x060002B8 RID: 696 RVA: 0x000066C4 File Offset: 0x000048C4
		public int GetAttachedColliders(List<Collider2D> results)
		{
			return this.GetAttachedCollidersList_Internal(results);
		}

		// Token: 0x060002B9 RID: 697
		[NativeMethod("GetAttachedCollidersList_Binding")]
		[MethodImpl(4096)]
		private extern int GetAttachedCollidersList_Internal([NotNull] List<Collider2D> results);

		// Token: 0x060002BA RID: 698 RVA: 0x000066E0 File Offset: 0x000048E0
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results)
		{
			return this.CastArray_Internal(direction, float.PositiveInfinity, results);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00006700 File Offset: 0x00004900
		public int Cast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.CastArray_Internal(direction, distance, results);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000671B File Offset: 0x0000491B
		[NativeMethod("CastArray_Binding")]
		private int CastArray_Internal(Vector2 direction, float distance, [NotNull] RaycastHit2D[] results)
		{
			return this.CastArray_Internal_Injected(ref direction, distance, results);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00006728 File Offset: 0x00004928
		public int Cast(Vector2 direction, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.CastList_Internal(direction, distance, results);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00006743 File Offset: 0x00004943
		[NativeMethod("CastList_Binding")]
		private int CastList_Internal(Vector2 direction, float distance, [NotNull] List<RaycastHit2D> results)
		{
			return this.CastList_Internal_Injected(ref direction, distance, results);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00006750 File Offset: 0x00004950
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.CastFilteredArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00006770 File Offset: 0x00004970
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.CastFilteredArray_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000678D File Offset: 0x0000498D
		[NativeMethod("CastFilteredArray_Binding")]
		private int CastFilteredArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			return this.CastFilteredArray_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000679C File Offset: 0x0000499C
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.CastFilteredList_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000067B9 File Offset: 0x000049B9
		[NativeMethod("CastFilteredList_Binding")]
		private int CastFilteredList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			return this.CastFilteredList_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		// Token: 0x060002C5 RID: 709
		[MethodImpl(4096)]
		private extern void get_position_Injected(out Vector2 ret);

		// Token: 0x060002C6 RID: 710
		[MethodImpl(4096)]
		private extern void set_position_Injected(ref Vector2 value);

		// Token: 0x060002C7 RID: 711
		[MethodImpl(4096)]
		private extern void SetRotation_Quaternion_Injected(ref Quaternion rotation);

		// Token: 0x060002C8 RID: 712
		[MethodImpl(4096)]
		private extern void MovePosition_Injected(ref Vector2 position);

		// Token: 0x060002C9 RID: 713
		[MethodImpl(4096)]
		private extern void MoveRotation_Quaternion_Injected(ref Quaternion rotation);

		// Token: 0x060002CA RID: 714
		[MethodImpl(4096)]
		private extern void get_velocity_Injected(out Vector2 ret);

		// Token: 0x060002CB RID: 715
		[MethodImpl(4096)]
		private extern void set_velocity_Injected(ref Vector2 value);

		// Token: 0x060002CC RID: 716
		[MethodImpl(4096)]
		private extern void get_centerOfMass_Injected(out Vector2 ret);

		// Token: 0x060002CD RID: 717
		[MethodImpl(4096)]
		private extern void set_centerOfMass_Injected(ref Vector2 value);

		// Token: 0x060002CE RID: 718
		[MethodImpl(4096)]
		private extern void get_worldCenterOfMass_Injected(out Vector2 ret);

		// Token: 0x060002CF RID: 719
		[MethodImpl(4096)]
		private extern bool IsTouching_OtherColliderWithFilter_Internal_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

		// Token: 0x060002D0 RID: 720
		[MethodImpl(4096)]
		private extern bool IsTouching_AnyColliderWithFilter_Internal_Injected(ref ContactFilter2D contactFilter);

		// Token: 0x060002D1 RID: 721
		[MethodImpl(4096)]
		private extern bool OverlapPoint_Injected(ref Vector2 point);

		// Token: 0x060002D2 RID: 722
		[MethodImpl(4096)]
		private extern void Distance_Internal_Injected([Writable] Collider2D collider, out ColliderDistance2D ret);

		// Token: 0x060002D3 RID: 723
		[MethodImpl(4096)]
		private extern void AddForce_Injected(ref Vector2 force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060002D4 RID: 724
		[MethodImpl(4096)]
		private extern void AddRelativeForce_Injected(ref Vector2 relativeForce, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060002D5 RID: 725
		[MethodImpl(4096)]
		private extern void AddForceAtPosition_Injected(ref Vector2 force, ref Vector2 position, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060002D6 RID: 726
		[MethodImpl(4096)]
		private extern void GetPoint_Injected(ref Vector2 point, out Vector2 ret);

		// Token: 0x060002D7 RID: 727
		[MethodImpl(4096)]
		private extern void GetRelativePoint_Injected(ref Vector2 relativePoint, out Vector2 ret);

		// Token: 0x060002D8 RID: 728
		[MethodImpl(4096)]
		private extern void GetVector_Injected(ref Vector2 vector, out Vector2 ret);

		// Token: 0x060002D9 RID: 729
		[MethodImpl(4096)]
		private extern void GetRelativeVector_Injected(ref Vector2 relativeVector, out Vector2 ret);

		// Token: 0x060002DA RID: 730
		[MethodImpl(4096)]
		private extern void GetPointVelocity_Injected(ref Vector2 point, out Vector2 ret);

		// Token: 0x060002DB RID: 731
		[MethodImpl(4096)]
		private extern void GetRelativePointVelocity_Injected(ref Vector2 relativePoint, out Vector2 ret);

		// Token: 0x060002DC RID: 732
		[MethodImpl(4096)]
		private extern int OverlapColliderArray_Internal_Injected(ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x060002DD RID: 733
		[MethodImpl(4096)]
		private extern int OverlapColliderList_Internal_Injected(ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x060002DE RID: 734
		[MethodImpl(4096)]
		private extern int CastArray_Internal_Injected(ref Vector2 direction, float distance, RaycastHit2D[] results);

		// Token: 0x060002DF RID: 735
		[MethodImpl(4096)]
		private extern int CastList_Internal_Injected(ref Vector2 direction, float distance, List<RaycastHit2D> results);

		// Token: 0x060002E0 RID: 736
		[MethodImpl(4096)]
		private extern int CastFilteredArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		// Token: 0x060002E1 RID: 737
		[MethodImpl(4096)]
		private extern int CastFilteredList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);
	}
}

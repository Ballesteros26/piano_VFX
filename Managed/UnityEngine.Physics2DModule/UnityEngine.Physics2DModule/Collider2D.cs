using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	[RequiredByNativeCode(Optional = true)]
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
	[RequireComponent(typeof(Transform))]
	public class Collider2D : Behaviour
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002E2 RID: 738
		// (set) Token: 0x060002E3 RID: 739
		public extern float density
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002E4 RID: 740
		// (set) Token: 0x060002E5 RID: 741
		public extern bool isTrigger
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002E6 RID: 742
		// (set) Token: 0x060002E7 RID: 743
		public extern bool usedByEffector
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002E8 RID: 744
		// (set) Token: 0x060002E9 RID: 745
		public extern bool usedByComposite
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002EA RID: 746
		public extern CompositeCollider2D composite
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000067D4 File Offset: 0x000049D4
		// (set) Token: 0x060002EC RID: 748 RVA: 0x000067EA File Offset: 0x000049EA
		public Vector2 offset
		{
			get
			{
				Vector2 vector;
				this.get_offset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_offset_Injected(ref value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002ED RID: 749
		public extern Rigidbody2D attachedRigidbody
		{
			[NativeMethod("GetAttachedRigidbody_Binding")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002EE RID: 750
		public extern int shapeCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060002EF RID: 751
		[NativeMethod("CreateMesh_Binding")]
		[MethodImpl(4096)]
		public extern Mesh CreateMesh(bool useBodyPosition, bool useBodyRotation);

		// Token: 0x060002F0 RID: 752
		[NativeMethod("GetShapeHash_Binding")]
		[MethodImpl(4096)]
		public extern uint GetShapeHash();

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000067F4 File Offset: 0x000049F4
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.get_bounds_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002F2 RID: 754
		internal extern ColliderErrorState2D errorState
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002F3 RID: 755
		internal extern bool compositeCapable
		{
			[NativeMethod("GetCompositeCapable_Binding")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002F4 RID: 756
		// (set) Token: 0x060002F5 RID: 757
		public extern PhysicsMaterial2D sharedMaterial
		{
			[NativeMethod("GetMaterial")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetMaterial")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002F6 RID: 758
		public extern float friction
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002F7 RID: 759
		public extern float bounciness
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060002F8 RID: 760
		[MethodImpl(4096)]
		public extern bool IsTouching([NotNull] [Writable] Collider2D collider);

		// Token: 0x060002F9 RID: 761 RVA: 0x0000680C File Offset: 0x00004A0C
		public bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter(collider, contactFilter);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00006826 File Offset: 0x00004A26
		[NativeMethod("IsTouching")]
		private bool IsTouching_OtherColliderWithFilter([NotNull] [Writable] Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter_Injected(collider, ref contactFilter);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00006834 File Offset: 0x00004A34
		public bool IsTouching(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter(contactFilter);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000684D File Offset: 0x00004A4D
		[NativeMethod("IsTouching")]
		private bool IsTouching_AnyColliderWithFilter(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter_Injected(ref contactFilter);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00006858 File Offset: 0x00004A58
		[ExcludeFromDocs]
		public bool IsTouchingLayers()
		{
			return this.IsTouchingLayers(-1);
		}

		// Token: 0x060002FE RID: 766
		[MethodImpl(4096)]
		public extern bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask);

		// Token: 0x060002FF RID: 767 RVA: 0x00006871 File Offset: 0x00004A71
		public bool OverlapPoint(Vector2 point)
		{
			return this.OverlapPoint_Injected(ref point);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000687C File Offset: 0x00004A7C
		public ColliderDistance2D Distance([Writable] Collider2D collider)
		{
			return Physics2D.Distance(this, collider);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00006898 File Offset: 0x00004A98
		public int OverlapCollider(ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000068B4 File Offset: 0x00004AB4
		public int OverlapCollider(ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000068D0 File Offset: 0x00004AD0
		public int GetContacts(ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000068F8 File Offset: 0x00004AF8
		public int GetContacts(List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00006920 File Offset: 0x00004B20
		public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000693C File Offset: 0x00004B3C
		public int GetContacts(ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00006958 File Offset: 0x00004B58
		public int GetContacts(Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00006980 File Offset: 0x00004B80
		public int GetContacts(List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000069A8 File Offset: 0x00004BA8
		public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000069C4 File Offset: 0x00004BC4
		public int GetContacts(ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000069E0 File Offset: 0x00004BE0
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results)
		{
			ContactFilter2D contactFilter2D = default(ContactFilter2D);
			contactFilter2D.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
			return this.CastArray_Internal(direction, float.PositiveInfinity, contactFilter2D, true, results);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00006A34 File Offset: 0x00004C34
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results, float distance)
		{
			ContactFilter2D contactFilter2D = default(ContactFilter2D);
			contactFilter2D.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
			return this.CastArray_Internal(direction, distance, contactFilter2D, true, results);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00006A84 File Offset: 0x00004C84
		public int Cast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
		{
			ContactFilter2D contactFilter2D = default(ContactFilter2D);
			contactFilter2D.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
			return this.CastArray_Internal(direction, distance, contactFilter2D, ignoreSiblingColliders, results);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00006AD4 File Offset: 0x00004CD4
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.CastArray_Internal(direction, float.PositiveInfinity, contactFilter, true, results);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00006AF8 File Offset: 0x00004CF8
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
		{
			return this.CastArray_Internal(direction, distance, contactFilter, true, results);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00006B18 File Offset: 0x00004D18
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
		{
			return this.CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00006B37 File Offset: 0x00004D37
		[NativeMethod("CastArray_Binding")]
		private int CastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, [NotNull] RaycastHit2D[] results)
		{
			return this.CastArray_Internal_Injected(ref direction, distance, ref contactFilter, ignoreSiblingColliders, results);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00006B48 File Offset: 0x00004D48
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity, [DefaultValue("true")] bool ignoreSiblingColliders = true)
		{
			return this.CastList_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00006B67 File Offset: 0x00004D67
		[NativeMethod("CastList_Binding")]
		private int CastList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, [NotNull] List<RaycastHit2D> results)
		{
			return this.CastList_Internal_Injected(ref direction, distance, ref contactFilter, ignoreSiblingColliders, results);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00006B78 File Offset: 0x00004D78
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter2D, results);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00006BAC File Offset: 0x00004DAC
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter2D, results);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00006BDC File Offset: 0x00004DDC
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter2D, results);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00006C0C File Offset: 0x00004E0C
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter2D, results);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00006C38 File Offset: 0x00004E38
		public int Raycast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("Physics2D.AllLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return this.RaycastArray_Internal(direction, distance, contactFilter2D, results);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00006C60 File Offset: 0x00004E60
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00006C80 File Offset: 0x00004E80
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00006C9D File Offset: 0x00004E9D
		[NativeMethod("RaycastArray_Binding")]
		private int RaycastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			return this.RaycastArray_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00006CAC File Offset: 0x00004EAC
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.RaycastList_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00006CC9 File Offset: 0x00004EC9
		[NativeMethod("RaycastList_Binding")]
		private int RaycastList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			return this.RaycastList_Internal_Injected(ref direction, distance, ref contactFilter, results);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00006CD8 File Offset: 0x00004ED8
		public Vector2 ClosestPoint(Vector2 position)
		{
			return Physics2D.ClosestPoint(position, this);
		}

		// Token: 0x06000320 RID: 800
		[MethodImpl(4096)]
		private extern void get_offset_Injected(out Vector2 ret);

		// Token: 0x06000321 RID: 801
		[MethodImpl(4096)]
		private extern void set_offset_Injected(ref Vector2 value);

		// Token: 0x06000322 RID: 802
		[MethodImpl(4096)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x06000323 RID: 803
		[MethodImpl(4096)]
		private extern bool IsTouching_OtherColliderWithFilter_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

		// Token: 0x06000324 RID: 804
		[MethodImpl(4096)]
		private extern bool IsTouching_AnyColliderWithFilter_Injected(ref ContactFilter2D contactFilter);

		// Token: 0x06000325 RID: 805
		[MethodImpl(4096)]
		private extern bool OverlapPoint_Injected(ref Vector2 point);

		// Token: 0x06000326 RID: 806
		[MethodImpl(4096)]
		private extern int CastArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, RaycastHit2D[] results);

		// Token: 0x06000327 RID: 807
		[MethodImpl(4096)]
		private extern int CastList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, List<RaycastHit2D> results);

		// Token: 0x06000328 RID: 808
		[MethodImpl(4096)]
		private extern int RaycastArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

		// Token: 0x06000329 RID: 809
		[MethodImpl(4096)]
		private extern int RaycastList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);
	}
}

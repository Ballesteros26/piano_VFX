using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/Physics/Public/PhysicsSceneHandle.h")]
	public struct PhysicsScene : IEquatable<PhysicsScene>
	{
		// Token: 0x06000238 RID: 568 RVA: 0x000036E0 File Offset: 0x000018E0
		public override string ToString()
		{
			return UnityString.Format("({0})", new object[] { this.m_Handle });
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00003710 File Offset: 0x00001910
		public static bool operator ==(PhysicsScene lhs, PhysicsScene rhs)
		{
			return lhs.m_Handle == rhs.m_Handle;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00003730 File Offset: 0x00001930
		public static bool operator !=(PhysicsScene lhs, PhysicsScene rhs)
		{
			return lhs.m_Handle != rhs.m_Handle;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00003754 File Offset: 0x00001954
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000376C File Offset: 0x0000196C
		public override bool Equals(object other)
		{
			bool flag = !(other is PhysicsScene);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				PhysicsScene physicsScene = (PhysicsScene)other;
				flag2 = this.m_Handle == physicsScene.m_Handle;
			}
			return flag2;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000037A8 File Offset: 0x000019A8
		public bool Equals(PhysicsScene other)
		{
			return this.m_Handle == other.m_Handle;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000037C8 File Offset: 0x000019C8
		public bool IsValid()
		{
			return PhysicsScene.IsValid_Internal(this);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000037E5 File Offset: 0x000019E5
		[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		[NativeMethod("IsPhysicsSceneValid")]
		private static bool IsValid_Internal(PhysicsScene physicsScene)
		{
			return PhysicsScene.IsValid_Internal_Injected(ref physicsScene);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000037F0 File Offset: 0x000019F0
		public bool IsEmpty()
		{
			bool flag = this.IsValid();
			if (flag)
			{
				return PhysicsScene.IsEmpty_Internal(this);
			}
			throw new InvalidOperationException("Cannot check if physics scene is empty as it is invalid.");
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00003822 File Offset: 0x00001A22
		[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		[NativeMethod("IsPhysicsWorldEmpty")]
		private static bool IsEmpty_Internal(PhysicsScene physicsScene)
		{
			return PhysicsScene.IsEmpty_Internal_Injected(ref physicsScene);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000382C File Offset: 0x00001A2C
		public void Simulate(float step)
		{
			bool flag = this.IsValid();
			if (flag)
			{
				bool flag2 = this == Physics.defaultPhysicsScene && Physics.autoSimulation;
				if (flag2)
				{
					Debug.LogWarning("PhysicsScene.Simulate(...) was called but auto simulation is active. You should disable auto simulation first before calling this function therefore the simulation was not run.");
				}
				else
				{
					Physics.Simulate_Internal(this, step);
				}
				return;
			}
			throw new InvalidOperationException("Cannot simulate the physics scene as it is invalid.");
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000388C File Offset: 0x00001A8C
		public bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				Ray ray = new Ray(origin, vector);
				flag2 = PhysicsScene.Internal_RaycastTest(this, ray, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000038DC File Offset: 0x00001ADC
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		[NativeName("RaycastTest")]
		private static bool Internal_RaycastTest(PhysicsScene physicsScene, Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_RaycastTest_Injected(ref physicsScene, ref ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000038EC File Offset: 0x00001AEC
		public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				Ray ray = new Ray(origin, vector);
				flag2 = PhysicsScene.Internal_Raycast(this, ray, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00003945 File Offset: 0x00001B45
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		[NativeName("Raycast")]
		private static bool Internal_Raycast(PhysicsScene physicsScene, Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_Raycast_Injected(ref physicsScene, ref ray, maxDistance, ref hit, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00003958 File Offset: 0x00001B58
		public int Raycast(Vector3 origin, Vector3 direction, RaycastHit[] raycastHits, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			int num;
			if (flag)
			{
				Ray ray = new Ray(origin, direction.normalized);
				num = PhysicsScene.Internal_RaycastNonAlloc(this, ray, raycastHits, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000039A5 File Offset: 0x00001BA5
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		[NativeName("RaycastNonAlloc")]
		private static int Internal_RaycastNonAlloc(PhysicsScene physicsScene, Ray ray, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_RaycastNonAlloc_Injected(ref physicsScene, ref ray, raycastHits, maxDistance, mask, queryTriggerInteraction);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000039B8 File Offset: 0x00001BB8
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		[NativeName("CapsuleCast")]
		private static bool Query_CapsuleCast(PhysicsScene physicsScene, Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Query_CapsuleCast_Injected(ref physicsScene, ref point1, ref point2, radius, ref direction, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000039DC File Offset: 0x00001BDC
		private static bool Internal_CapsuleCast(PhysicsScene physicsScene, Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			hitInfo = default(RaycastHit);
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				flag2 = PhysicsScene.Query_CapsuleCast(physicsScene, point1, point2, radius, vector, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00003A2C File Offset: 0x00001C2C
		public bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.Internal_CapsuleCast(this, point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00003A58 File Offset: 0x00001C58
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		[NativeName("CapsuleCastNonAlloc")]
		private static int Internal_CapsuleCastNonAlloc(PhysicsScene physicsScene, Vector3 p0, Vector3 p1, float radius, Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_CapsuleCastNonAlloc_Injected(ref physicsScene, ref p0, ref p1, radius, ref direction, raycastHits, maxDistance, mask, queryTriggerInteraction);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00003A7C File Offset: 0x00001C7C
		public int CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			int num;
			if (flag)
			{
				num = PhysicsScene.Internal_CapsuleCastNonAlloc(this, point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00003AC0 File Offset: 0x00001CC0
		[NativeName("OverlapCapsuleNonAlloc")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int OverlapCapsuleNonAlloc_Internal(PhysicsScene physicsScene, Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapCapsuleNonAlloc_Internal_Injected(ref physicsScene, ref point0, ref point1, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public int OverlapCapsule(Vector3 point0, Vector3 point1, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask = -1, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.OverlapCapsuleNonAlloc_Internal(this, point0, point1, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00003AFA File Offset: 0x00001CFA
		[NativeName("SphereCast")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static bool Query_SphereCast(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Query_SphereCast_Injected(ref physicsScene, ref origin, radius, ref direction, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00003B10 File Offset: 0x00001D10
		private static bool Internal_SphereCast(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			hitInfo = default(RaycastHit);
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				flag2 = PhysicsScene.Query_SphereCast(physicsScene, origin, radius, vector, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00003B5C File Offset: 0x00001D5C
		public bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.Internal_SphereCast(this, origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00003B84 File Offset: 0x00001D84
		[NativeName("SphereCastNonAlloc")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int Internal_SphereCastNonAlloc(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_SphereCastNonAlloc_Injected(ref physicsScene, ref origin, radius, ref direction, raycastHits, maxDistance, mask, queryTriggerInteraction);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00003B9C File Offset: 0x00001D9C
		public int SphereCast(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			int num;
			if (flag)
			{
				num = PhysicsScene.Internal_SphereCastNonAlloc(this, origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00003BDE File Offset: 0x00001DDE
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		[NativeName("OverlapSphereNonAlloc")]
		private static int OverlapSphereNonAlloc_Internal(PhysicsScene physicsScene, Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapSphereNonAlloc_Internal_Injected(ref physicsScene, ref position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public int OverlapSphere(Vector3 position, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapSphereNonAlloc_Internal(this, position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00003C14 File Offset: 0x00001E14
		[NativeName("BoxCast")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static bool Query_BoxCast(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, ref RaycastHit outHit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Query_BoxCast_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, ref orientation, maxDistance, ref outHit, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00003C38 File Offset: 0x00001E38
		private static bool Internal_BoxCast(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			hitInfo = default(RaycastHit);
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				flag2 = PhysicsScene.Query_BoxCast(physicsScene, center, halfExtents, vector, orientation, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00003C88 File Offset: 0x00001E88
		public bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.Internal_BoxCast(this, center, halfExtents, orientation, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00003CB4 File Offset: 0x00001EB4
		[ExcludeFromDocs]
		public bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
		{
			return PhysicsScene.Internal_BoxCast(this, center, halfExtents, Quaternion.identity, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00003CE3 File Offset: 0x00001EE3
		[NativeName("OverlapBoxNonAlloc")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int OverlapBoxNonAlloc_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapBoxNonAlloc_Internal_Injected(ref physicsScene, ref center, ref halfExtents, results, ref orientation, mask, queryTriggerInteraction);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public int OverlapBox(Vector3 center, Vector3 halfExtents, Collider[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.OverlapBoxNonAlloc_Internal(this, center, halfExtents, results, orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00003D20 File Offset: 0x00001F20
		[ExcludeFromDocs]
		public int OverlapBox(Vector3 center, Vector3 halfExtents, Collider[] results)
		{
			return PhysicsScene.OverlapBoxNonAlloc_Internal(this, center, halfExtents, results, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00003D48 File Offset: 0x00001F48
		[NativeName("BoxCastNonAlloc")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static int Internal_BoxCastNonAlloc(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] raycastHits, Quaternion orientation, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_BoxCastNonAlloc_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, raycastHits, ref orientation, maxDistance, mask, queryTriggerInteraction);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00003D6C File Offset: 0x00001F6C
		public int BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			int num;
			if (flag)
			{
				num = PhysicsScene.Internal_BoxCastNonAlloc(this, center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00003DB0 File Offset: 0x00001FB0
		[ExcludeFromDocs]
		public int BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)
		{
			return this.BoxCast(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000261 RID: 609
		[MethodImpl(4096)]
		private static extern bool IsValid_Internal_Injected(ref PhysicsScene physicsScene);

		// Token: 0x06000262 RID: 610
		[MethodImpl(4096)]
		private static extern bool IsEmpty_Internal_Injected(ref PhysicsScene physicsScene);

		// Token: 0x06000263 RID: 611
		[MethodImpl(4096)]
		private static extern bool Internal_RaycastTest_Injected(ref PhysicsScene physicsScene, ref Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06000264 RID: 612
		[MethodImpl(4096)]
		private static extern bool Internal_Raycast_Injected(ref PhysicsScene physicsScene, ref Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06000265 RID: 613
		[MethodImpl(4096)]
		private static extern int Internal_RaycastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Ray ray, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06000266 RID: 614
		[MethodImpl(4096)]
		private static extern bool Query_CapsuleCast_Injected(ref PhysicsScene physicsScene, ref Vector3 point1, ref Vector3 point2, float radius, ref Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06000267 RID: 615
		[MethodImpl(4096)]
		private static extern int Internal_CapsuleCastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Vector3 p0, ref Vector3 p1, float radius, ref Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06000268 RID: 616
		[MethodImpl(4096)]
		private static extern int OverlapCapsuleNonAlloc_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 point0, ref Vector3 point1, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06000269 RID: 617
		[MethodImpl(4096)]
		private static extern bool Query_SphereCast_Injected(ref PhysicsScene physicsScene, ref Vector3 origin, float radius, ref Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600026A RID: 618
		[MethodImpl(4096)]
		private static extern int Internal_SphereCastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Vector3 origin, float radius, ref Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600026B RID: 619
		[MethodImpl(4096)]
		private static extern int OverlapSphereNonAlloc_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600026C RID: 620
		[MethodImpl(4096)]
		private static extern bool Query_BoxCast_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, ref Quaternion orientation, float maxDistance, ref RaycastHit outHit, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600026D RID: 621
		[MethodImpl(4096)]
		private static extern int OverlapBoxNonAlloc_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, Collider[] results, ref Quaternion orientation, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0600026E RID: 622
		[MethodImpl(4096)]
		private static extern int Internal_BoxCastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, RaycastHit[] raycastHits, ref Quaternion orientation, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x04000078 RID: 120
		private int m_Handle;
	}
}

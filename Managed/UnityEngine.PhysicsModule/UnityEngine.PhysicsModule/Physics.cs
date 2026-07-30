using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/Physics/PhysicsManager.h")]
	public class Physics
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000041C8 File Offset: 0x000023C8
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("Use Physics.defaultContactOffset or Collider.contactOffset instead.", true)]
		[EditorBrowsable(1)]
		public static float minPenetrationForPenalty
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000041E0 File Offset: 0x000023E0
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x000041F5 File Offset: 0x000023F5
		public static Vector3 gravity
		{
			[ThreadSafe]
			get
			{
				Vector3 vector;
				Physics.get_gravity_Injected(out vector);
				return vector;
			}
			set
			{
				Physics.set_gravity_Injected(ref value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002F7 RID: 759
		// (set) Token: 0x060002F8 RID: 760
		public static extern float defaultContactOffset
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002F9 RID: 761
		// (set) Token: 0x060002FA RID: 762
		public static extern float sleepThreshold
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002FB RID: 763
		// (set) Token: 0x060002FC RID: 764
		public static extern bool queriesHitTriggers
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002FD RID: 765
		// (set) Token: 0x060002FE RID: 766
		public static extern bool queriesHitBackfaces
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002FF RID: 767
		// (set) Token: 0x06000300 RID: 768
		public static extern float bounceThreshold
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000301 RID: 769
		// (set) Token: 0x06000302 RID: 770
		public static extern float defaultMaxDepenetrationVelocity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000303 RID: 771
		// (set) Token: 0x06000304 RID: 772
		public static extern int defaultSolverIterations
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000305 RID: 773
		// (set) Token: 0x06000306 RID: 774
		public static extern int defaultSolverVelocityIterations
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00004200 File Offset: 0x00002400
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00004217 File Offset: 0x00002417
		[Obsolete("Please use bounceThreshold instead.")]
		public static float bounceTreshold
		{
			get
			{
				return Physics.bounceThreshold;
			}
			set
			{
				Physics.bounceThreshold = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00004224 File Offset: 0x00002424
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.")]
		public static float sleepVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000423C File Offset: 0x0000243C
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("The sleepAngularVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.")]
		public static float sleepAngularVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00004254 File Offset: 0x00002454
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000213F File Offset: 0x0000033F
		[EditorBrowsable(1)]
		[Obsolete("Use Rigidbody.maxAngularVelocity instead.", true)]
		public static float maxAngularVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000426C File Offset: 0x0000246C
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00004283 File Offset: 0x00002483
		[Obsolete("Please use Physics.defaultSolverIterations instead. (UnityUpgradable) -> defaultSolverIterations")]
		public static int solverIterationCount
		{
			get
			{
				return Physics.defaultSolverIterations;
			}
			set
			{
				Physics.defaultSolverIterations = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00004290 File Offset: 0x00002490
		// (set) Token: 0x06000312 RID: 786 RVA: 0x000042A7 File Offset: 0x000024A7
		[Obsolete("Please use Physics.defaultSolverVelocityIterations instead. (UnityUpgradable) -> defaultSolverVelocityIterations")]
		public static int solverVelocityIterationCount
		{
			get
			{
				return Physics.defaultSolverVelocityIterations;
			}
			set
			{
				Physics.defaultSolverVelocityIterations = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000042B4 File Offset: 0x000024B4
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("penetrationPenaltyForce has no effect.")]
		public static float penetrationPenaltyForce
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000315 RID: 789
		// (set) Token: 0x06000316 RID: 790
		public static extern float defaultMaxAngularSpeed
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000317 RID: 791 RVA: 0x000042CC File Offset: 0x000024CC
		[NativeProperty("DefaultPhysicsSceneHandle")]
		public static PhysicsScene defaultPhysicsScene
		{
			get
			{
				PhysicsScene physicsScene;
				Physics.get_defaultPhysicsScene_Injected(out physicsScene);
				return physicsScene;
			}
		}

		// Token: 0x06000318 RID: 792
		[MethodImpl(4096)]
		public static extern void IgnoreCollision(Collider collider1, Collider collider2, [DefaultValue("true")] bool ignore);

		// Token: 0x06000319 RID: 793 RVA: 0x000042E1 File Offset: 0x000024E1
		[ExcludeFromDocs]
		public static void IgnoreCollision(Collider collider1, Collider collider2)
		{
			Physics.IgnoreCollision(collider1, collider2, true);
		}

		// Token: 0x0600031A RID: 794
		[NativeName("IgnoreCollision")]
		[MethodImpl(4096)]
		public static extern void IgnoreLayerCollision(int layer1, int layer2, [DefaultValue("true")] bool ignore);

		// Token: 0x0600031B RID: 795 RVA: 0x000042ED File Offset: 0x000024ED
		[ExcludeFromDocs]
		public static void IgnoreLayerCollision(int layer1, int layer2)
		{
			Physics.IgnoreLayerCollision(layer1, layer2, true);
		}

		// Token: 0x0600031C RID: 796
		[MethodImpl(4096)]
		public static extern bool GetIgnoreLayerCollision(int layer1, int layer2);

		// Token: 0x0600031D RID: 797
		[MethodImpl(4096)]
		public static extern bool GetIgnoreCollision(Collider collider1, Collider collider2);

		// Token: 0x0600031E RID: 798 RVA: 0x000042FC File Offset: 0x000024FC
		public static bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00004324 File Offset: 0x00002524
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00004348 File Offset: 0x00002548
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00004370 File Offset: 0x00002570
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000439C File Offset: 0x0000259C
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000043C4 File Offset: 0x000025C4
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000043EC File Offset: 0x000025EC
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00004414 File Offset: 0x00002614
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00004440 File Offset: 0x00002640
		public static bool Raycast(Ray ray, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00004470 File Offset: 0x00002670
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000044A0 File Offset: 0x000026A0
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000044D4 File Offset: 0x000026D4
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000450C File Offset: 0x0000270C
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00004540 File Offset: 0x00002740
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000456C File Offset: 0x0000276C
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000045A0 File Offset: 0x000027A0
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000045D8 File Offset: 0x000027D8
		public static bool Linecast(Vector3 start, Vector3 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			Vector3 vector = end - start;
			return Physics.defaultPhysicsScene.Raycast(start, vector, vector.magnitude, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000460C File Offset: 0x0000280C
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, int layerMask)
		{
			return Physics.Linecast(start, end, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00004628 File Offset: 0x00002828
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end)
		{
			return Physics.Linecast(start, end, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00004644 File Offset: 0x00002844
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			Vector3 vector = end - start;
			return Physics.defaultPhysicsScene.Raycast(start, vector, out hitInfo, vector.magnitude, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00004678 File Offset: 0x00002878
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask)
		{
			return Physics.Linecast(start, end, out hitInfo, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00004694 File Offset: 0x00002894
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo)
		{
			return Physics.Linecast(start, end, out hitInfo, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000046B4 File Offset: 0x000028B4
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit raycastHit;
			return Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000046E0 File Offset: 0x000028E0
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00004700 File Offset: 0x00002900
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00004720 File Offset: 0x00002920
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00004744 File Offset: 0x00002944
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00004770 File Offset: 0x00002970
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00004794 File Offset: 0x00002994
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000047B8 File Offset: 0x000029B8
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000047E0 File Offset: 0x000029E0
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000480C File Offset: 0x00002A0C
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000482C File Offset: 0x00002A2C
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000484C File Offset: 0x00002A4C
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00004870 File Offset: 0x00002A70
		public static bool SphereCast(Ray ray, float radius, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit raycastHit;
			return Physics.SphereCast(ray.origin, radius, ray.direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000489C File Offset: 0x00002A9C
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask)
		{
			return Physics.SphereCast(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000048B8 File Offset: 0x00002AB8
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, float maxDistance)
		{
			return Physics.SphereCast(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000048D8 File Offset: 0x00002AD8
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius)
		{
			return Physics.SphereCast(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000048FC File Offset: 0x00002AFC
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCast(ray.origin, radius, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00004928 File Offset: 0x00002B28
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00004948 File Offset: 0x00002B48
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00004968 File Offset: 0x00002B68
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo)
		{
			return Physics.SphereCast(ray, radius, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000498C File Offset: 0x00002B8C
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit raycastHit;
			return Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, out raycastHit, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000049B8 File Offset: 0x00002BB8
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000049D8 File Offset: 0x00002BD8
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000049F8 File Offset: 0x00002BF8
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00004A1C File Offset: 0x00002C1C
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction)
		{
			return Physics.BoxCast(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00004A44 File Offset: 0x00002C44
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00004A70 File Offset: 0x00002C70
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00004A94 File Offset: 0x00002C94
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00004AB8 File Offset: 0x00002CB8
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00004AE0 File Offset: 0x00002CE0
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00004B08 File Offset: 0x00002D08
		[NativeName("RaycastAll")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static RaycastHit[] Internal_RaycastAll(PhysicsScene physicsScene, Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Internal_RaycastAll_Injected(ref physicsScene, ref ray, maxDistance, mask, queryTriggerInteraction);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00004B18 File Offset: 0x00002D18
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				Ray ray = new Ray(origin, vector);
				array = Physics.Internal_RaycastAll(Physics.defaultPhysicsScene, ray, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00004B6C File Offset: 0x00002D6C
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.RaycastAll(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00004B88 File Offset: 0x00002D88
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance)
		{
			return Physics.RaycastAll(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00004BA8 File Offset: 0x00002DA8
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction)
		{
			return Physics.RaycastAll(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00004BCC File Offset: 0x00002DCC
		public static RaycastHit[] RaycastAll(Ray ray, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00004BF4 File Offset: 0x00002DF4
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00004C1C File Offset: 0x00002E1C
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00004C48 File Offset: 0x00002E48
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00004C78 File Offset: 0x00002E78
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00004CAC File Offset: 0x00002EAC
		[RequiredByNativeCode]
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00004CE0 File Offset: 0x00002EE0
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00004D14 File Offset: 0x00002F14
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00004D4C File Offset: 0x00002F4C
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00004D74 File Offset: 0x00002F74
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00004D9C File Offset: 0x00002F9C
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00004DC4 File Offset: 0x00002FC4
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00004DEE File Offset: 0x00002FEE
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		[NativeName("CapsuleCastAll")]
		private static RaycastHit[] Query_CapsuleCastAll(PhysicsScene physicsScene, Vector3 p0, Vector3 p1, float radius, Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Query_CapsuleCastAll_Injected(ref physicsScene, ref p0, ref p1, radius, ref direction, maxDistance, mask, queryTriggerInteraction);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00004E04 File Offset: 0x00003004
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				array = Physics.Query_CapsuleCastAll(Physics.defaultPhysicsScene, point1, point2, radius, vector, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00004E54 File Offset: 0x00003054
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00004E74 File Offset: 0x00003074
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00004E94 File Offset: 0x00003094
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00004EB7 File Offset: 0x000030B7
		[NativeName("SphereCastAll")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static RaycastHit[] Query_SphereCastAll(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Query_SphereCastAll_Injected(ref physicsScene, ref origin, radius, ref direction, maxDistance, mask, queryTriggerInteraction);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00004ECC File Offset: 0x000030CC
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				array = Physics.Query_SphereCastAll(Physics.defaultPhysicsScene, origin, radius, vector, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00004F18 File Offset: 0x00003118
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00004F38 File Offset: 0x00003138
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance)
		{
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00004F58 File Offset: 0x00003158
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction)
		{
			return Physics.SphereCastAll(origin, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00004F7C File Offset: 0x0000317C
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCastAll(ray.origin, radius, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00004FA8 File Offset: 0x000031A8
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance, int layerMask)
		{
			return Physics.SphereCastAll(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00004FC4 File Offset: 0x000031C4
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance)
		{
			return Physics.SphereCastAll(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00004FE4 File Offset: 0x000031E4
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius)
		{
			return Physics.SphereCastAll(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00005005 File Offset: 0x00003205
		[NativeName("OverlapCapsule")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static Collider[] OverlapCapsule_Internal(PhysicsScene physicsScene, Vector3 point0, Vector3 point1, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapCapsule_Internal_Injected(ref physicsScene, ref point0, ref point1, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00005018 File Offset: 0x00003218
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapCapsule_Internal(Physics.defaultPhysicsScene, point0, point1, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000503C File Offset: 0x0000323C
		[ExcludeFromDocs]
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, int layerMask)
		{
			return Physics.OverlapCapsule(point0, point1, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00005058 File Offset: 0x00003258
		[ExcludeFromDocs]
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius)
		{
			return Physics.OverlapCapsule(point0, point1, radius, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00005074 File Offset: 0x00003274
		[NativeName("OverlapSphere")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
		private static Collider[] OverlapSphere_Internal(PhysicsScene physicsScene, Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapSphere_Internal_Injected(ref physicsScene, ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00005084 File Offset: 0x00003284
		public static Collider[] OverlapSphere(Vector3 position, float radius, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapSphere_Internal(Physics.defaultPhysicsScene, position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000050A4 File Offset: 0x000032A4
		[ExcludeFromDocs]
		public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask)
		{
			return Physics.OverlapSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000050C0 File Offset: 0x000032C0
		[ExcludeFromDocs]
		public static Collider[] OverlapSphere(Vector3 position, float radius)
		{
			return Physics.OverlapSphere(position, radius, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000050DB File Offset: 0x000032DB
		[NativeName("Simulate")]
		internal static void Simulate_Internal(PhysicsScene physicsScene, float step)
		{
			Physics.Simulate_Internal_Injected(ref physicsScene, step);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000050E8 File Offset: 0x000032E8
		public static void Simulate(float step)
		{
			bool autoSimulation = Physics.autoSimulation;
			if (autoSimulation)
			{
				Debug.LogWarning("Physics.Simulate(...) was called but auto simulation is active. You should disable auto simulation first before calling this function therefore the simulation was not run.");
			}
			else
			{
				Physics.Simulate_Internal(Physics.defaultPhysicsScene, step);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600037B RID: 891
		// (set) Token: 0x0600037C RID: 892
		public static extern bool autoSimulation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600037D RID: 893
		[MethodImpl(4096)]
		public static extern void SyncTransforms();

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600037E RID: 894
		// (set) Token: 0x0600037F RID: 895
		public static extern bool autoSyncTransforms
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000380 RID: 896
		// (set) Token: 0x06000381 RID: 897
		public static extern bool reuseCollisionCallbacks
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00005119 File Offset: 0x00003319
		[NativeName("ComputePenetration")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static bool Query_ComputePenetration([NotNull] Collider colliderA, Vector3 positionA, Quaternion rotationA, [NotNull] Collider colliderB, Vector3 positionB, Quaternion rotationB, ref Vector3 direction, ref float distance)
		{
			return Physics.Query_ComputePenetration_Injected(colliderA, ref positionA, ref rotationA, colliderB, ref positionB, ref rotationB, ref direction, ref distance);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00005130 File Offset: 0x00003330
		public static bool ComputePenetration(Collider colliderA, Vector3 positionA, Quaternion rotationA, Collider colliderB, Vector3 positionB, Quaternion rotationB, out Vector3 direction, out float distance)
		{
			direction = Vector3.zero;
			distance = 0f;
			return Physics.Query_ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, ref direction, ref distance);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00005168 File Offset: 0x00003368
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		[NativeName("ClosestPoint")]
		private static Vector3 Query_ClosestPoint([NotNull] Collider collider, Vector3 position, Quaternion rotation, Vector3 point)
		{
			Vector3 vector;
			Physics.Query_ClosestPoint_Injected(collider, ref position, ref rotation, ref point, out vector);
			return vector;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00005184 File Offset: 0x00003384
		public static Vector3 ClosestPoint(Vector3 point, Collider collider, Vector3 position, Quaternion rotation)
		{
			return Physics.Query_ClosestPoint(collider, position, rotation, point);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000386 RID: 902
		// (set) Token: 0x06000387 RID: 903
		[StaticAccessor("GetPhysicsManager()")]
		public static extern float interCollisionDistance
		{
			[NativeName("GetClothInterCollisionDistance")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetClothInterCollisionDistance")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000388 RID: 904
		// (set) Token: 0x06000389 RID: 905
		[StaticAccessor("GetPhysicsManager()")]
		public static extern float interCollisionStiffness
		{
			[NativeName("GetClothInterCollisionStiffness")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetClothInterCollisionStiffness")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600038A RID: 906
		// (set) Token: 0x0600038B RID: 907
		[StaticAccessor("GetPhysicsManager()")]
		public static extern bool interCollisionSettingsToggle
		{
			[NativeName("GetClothInterCollisionSettingsToggle")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetClothInterCollisionSettingsToggle")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600038C RID: 908 RVA: 0x000051A0 File Offset: 0x000033A0
		// (set) Token: 0x0600038D RID: 909 RVA: 0x000051B5 File Offset: 0x000033B5
		public static Vector3 clothGravity
		{
			[ThreadSafe]
			get
			{
				Vector3 vector;
				Physics.get_clothGravity_Injected(out vector);
				return vector;
			}
			set
			{
				Physics.set_clothGravity_Injected(ref value);
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000051C0 File Offset: 0x000033C0
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.OverlapSphere(position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000051E8 File Offset: 0x000033E8
		[ExcludeFromDocs]
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, int layerMask)
		{
			return Physics.OverlapSphereNonAlloc(position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00005204 File Offset: 0x00003404
		[ExcludeFromDocs]
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results)
		{
			return Physics.OverlapSphereNonAlloc(position, radius, results, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00005220 File Offset: 0x00003420
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		[NativeName("SphereTest")]
		private static bool CheckSphere_Internal(PhysicsScene physicsScene, Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckSphere_Internal_Injected(ref physicsScene, ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00005230 File Offset: 0x00003430
		public static bool CheckSphere(Vector3 position, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckSphere_Internal(Physics.defaultPhysicsScene, position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00005250 File Offset: 0x00003450
		[ExcludeFromDocs]
		public static bool CheckSphere(Vector3 position, float radius, int layerMask)
		{
			return Physics.CheckSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000526C File Offset: 0x0000346C
		[ExcludeFromDocs]
		public static bool CheckSphere(Vector3 position, float radius)
		{
			return Physics.CheckSphere(position, radius, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00005288 File Offset: 0x00003488
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000052B4 File Offset: 0x000034B4
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000052D8 File Offset: 0x000034D8
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000052FC File Offset: 0x000034FC
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00005324 File Offset: 0x00003524
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00005350 File Offset: 0x00003550
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00005370 File Offset: 0x00003570
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00005390 File Offset: 0x00003590
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000053B4 File Offset: 0x000035B4
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCastNonAlloc(ray.origin, radius, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000053E0 File Offset: 0x000035E0
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00005400 File Offset: 0x00003600
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00005420 File Offset: 0x00003620
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00005442 File Offset: 0x00003642
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		[NativeName("CapsuleTest")]
		private static bool CheckCapsule_Internal(PhysicsScene physicsScene, Vector3 start, Vector3 end, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckCapsule_Internal_Injected(ref physicsScene, ref start, ref end, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00005454 File Offset: 0x00003654
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckCapsule_Internal(Physics.defaultPhysicsScene, start, end, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00005478 File Offset: 0x00003678
		[ExcludeFromDocs]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask)
		{
			return Physics.CheckCapsule(start, end, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00005494 File Offset: 0x00003694
		[ExcludeFromDocs]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius)
		{
			return Physics.CheckCapsule(start, end, radius, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000054B1 File Offset: 0x000036B1
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		[NativeName("BoxTest")]
		private static bool CheckBox_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, int layermask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckBox_Internal_Injected(ref physicsScene, ref center, ref halfExtents, ref orientation, layermask, queryTriggerInteraction);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000054C4 File Offset: 0x000036C4
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckBox_Internal(Physics.defaultPhysicsScene, center, halfExtents, orientation, layermask, queryTriggerInteraction);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000054E8 File Offset: 0x000036E8
		[ExcludeFromDocs]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask)
		{
			return Physics.CheckBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00005504 File Offset: 0x00003704
		[ExcludeFromDocs]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
		{
			return Physics.CheckBox(center, halfExtents, orientation, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00005524 File Offset: 0x00003724
		[ExcludeFromDocs]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents)
		{
			return Physics.CheckBox(center, halfExtents, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00005545 File Offset: 0x00003745
		[NativeName("OverlapBox")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static Collider[] OverlapBox_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapBox_Internal_Injected(ref physicsScene, ref center, ref halfExtents, ref orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00005558 File Offset: 0x00003758
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapBox_Internal(Physics.defaultPhysicsScene, center, halfExtents, orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000557C File Offset: 0x0000377C
		[ExcludeFromDocs]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask)
		{
			return Physics.OverlapBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00005598 File Offset: 0x00003798
		[ExcludeFromDocs]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
		{
			return Physics.OverlapBox(center, halfExtents, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000055B4 File Offset: 0x000037B4
		[ExcludeFromDocs]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents)
		{
			return Physics.OverlapBox(center, halfExtents, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000055D4 File Offset: 0x000037D4
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("AllLayers")] int mask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.OverlapBox(center, halfExtents, results, orientation, mask, queryTriggerInteraction);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000055FC File Offset: 0x000037FC
		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int mask)
		{
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000561C File Offset: 0x0000381C
		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation)
		{
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000563C File Offset: 0x0000383C
		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results)
		{
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00005660 File Offset: 0x00003860
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000568C File Offset: 0x0000388C
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000056B4 File Offset: 0x000038B4
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000056D8 File Offset: 0x000038D8
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000056FC File Offset: 0x000038FC
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00005724 File Offset: 0x00003924
		[NativeName("BoxCastAll")]
		[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
		private static RaycastHit[] Internal_BoxCastAll(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Internal_BoxCastAll_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, ref orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000573C File Offset: 0x0000393C
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				array = Physics.Internal_BoxCastAll(Physics.defaultPhysicsScene, center, halfExtents, vector, orientation, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000578C File Offset: 0x0000398C
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000057AC File Offset: 0x000039AC
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000057CC File Offset: 0x000039CC
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000057F0 File Offset: 0x000039F0
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00005818 File Offset: 0x00003A18
		public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.OverlapCapsule(point0, point1, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00005840 File Offset: 0x00003A40
		[ExcludeFromDocs]
		public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask)
		{
			return Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00005860 File Offset: 0x00003A60
		[ExcludeFromDocs]
		public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results)
		{
			return Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000587D File Offset: 0x00003A7D
		[StaticAccessor("GetPhysicsManager()")]
		[NativeName("RebuildBroadphaseRegions")]
		private static void Internal_RebuildBroadphaseRegions(Bounds bounds, int subdivisions)
		{
			Physics.Internal_RebuildBroadphaseRegions_Injected(ref bounds, subdivisions);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00005888 File Offset: 0x00003A88
		public static void RebuildBroadphaseRegions(Bounds worldBounds, int subdivisions)
		{
			bool flag = subdivisions < 1 || subdivisions > 16;
			if (flag)
			{
				throw new ArgumentException("Physics.RebuildBroadphaseRegions requires the subdivisions to be greater than zero and less than 17.");
			}
			bool flag2 = worldBounds.extents.x <= 0f || worldBounds.extents.y <= 0f || worldBounds.extents.z <= 0f;
			if (flag2)
			{
				throw new ArgumentException("Physics.RebuildBroadphaseRegions requires the world bounds to be non-empty, and have positive extents.");
			}
			Physics.Internal_RebuildBroadphaseRegions(worldBounds, subdivisions);
		}

		// Token: 0x060003C3 RID: 963
		[StaticAccessor("GetPhysicsManager()")]
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void BakeMesh(int meshID, bool convex);

		// Token: 0x060003C5 RID: 965
		[MethodImpl(4096)]
		private static extern void get_gravity_Injected(out Vector3 ret);

		// Token: 0x060003C6 RID: 966
		[MethodImpl(4096)]
		private static extern void set_gravity_Injected(ref Vector3 value);

		// Token: 0x060003C7 RID: 967
		[MethodImpl(4096)]
		private static extern void get_defaultPhysicsScene_Injected(out PhysicsScene ret);

		// Token: 0x060003C8 RID: 968
		[MethodImpl(4096)]
		private static extern RaycastHit[] Internal_RaycastAll_Injected(ref PhysicsScene physicsScene, ref Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003C9 RID: 969
		[MethodImpl(4096)]
		private static extern RaycastHit[] Query_CapsuleCastAll_Injected(ref PhysicsScene physicsScene, ref Vector3 p0, ref Vector3 p1, float radius, ref Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003CA RID: 970
		[MethodImpl(4096)]
		private static extern RaycastHit[] Query_SphereCastAll_Injected(ref PhysicsScene physicsScene, ref Vector3 origin, float radius, ref Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003CB RID: 971
		[MethodImpl(4096)]
		private static extern Collider[] OverlapCapsule_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 point0, ref Vector3 point1, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003CC RID: 972
		[MethodImpl(4096)]
		private static extern Collider[] OverlapSphere_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003CD RID: 973
		[MethodImpl(4096)]
		private static extern void Simulate_Internal_Injected(ref PhysicsScene physicsScene, float step);

		// Token: 0x060003CE RID: 974
		[MethodImpl(4096)]
		private static extern bool Query_ComputePenetration_Injected(Collider colliderA, ref Vector3 positionA, ref Quaternion rotationA, Collider colliderB, ref Vector3 positionB, ref Quaternion rotationB, ref Vector3 direction, ref float distance);

		// Token: 0x060003CF RID: 975
		[MethodImpl(4096)]
		private static extern void Query_ClosestPoint_Injected(Collider collider, ref Vector3 position, ref Quaternion rotation, ref Vector3 point, out Vector3 ret);

		// Token: 0x060003D0 RID: 976
		[MethodImpl(4096)]
		private static extern void get_clothGravity_Injected(out Vector3 ret);

		// Token: 0x060003D1 RID: 977
		[MethodImpl(4096)]
		private static extern void set_clothGravity_Injected(ref Vector3 value);

		// Token: 0x060003D2 RID: 978
		[MethodImpl(4096)]
		private static extern bool CheckSphere_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003D3 RID: 979
		[MethodImpl(4096)]
		private static extern bool CheckCapsule_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 start, ref Vector3 end, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003D4 RID: 980
		[MethodImpl(4096)]
		private static extern bool CheckBox_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Quaternion orientation, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003D5 RID: 981
		[MethodImpl(4096)]
		private static extern Collider[] OverlapBox_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003D6 RID: 982
		[MethodImpl(4096)]
		private static extern RaycastHit[] Internal_BoxCastAll_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, ref Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003D7 RID: 983
		[MethodImpl(4096)]
		private static extern void Internal_RebuildBroadphaseRegions_Injected(ref Bounds bounds, int subdivisions);

		// Token: 0x0400008C RID: 140
		internal const float k_MaxFloatMinusEpsilon = 3.4028233E+38f;

		// Token: 0x0400008D RID: 141
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x0400008E RID: 142
		public const int DefaultRaycastLayers = -5;

		// Token: 0x0400008F RID: 143
		public const int AllLayers = -1;

		// Token: 0x04000090 RID: 144
		[Obsolete("Please use Physics.IgnoreRaycastLayer instead. (UnityUpgradable) -> IgnoreRaycastLayer", true)]
		[EditorBrowsable(1)]
		public const int kIgnoreRaycastLayer = 4;

		// Token: 0x04000091 RID: 145
		[EditorBrowsable(1)]
		[Obsolete("Please use Physics.DefaultRaycastLayers instead. (UnityUpgradable) -> DefaultRaycastLayers", true)]
		public const int kDefaultRaycastLayers = -5;

		// Token: 0x04000092 RID: 146
		[EditorBrowsable(1)]
		[Obsolete("Please use Physics.AllLayers instead. (UnityUpgradable) -> AllLayers", true)]
		public const int kAllLayers = -1;
	}
}

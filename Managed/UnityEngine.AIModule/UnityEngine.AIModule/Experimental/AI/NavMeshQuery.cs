using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.AI;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x02000021 RID: 33
	[NativeHeader("Runtime/Math/Matrix4x4.h")]
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	[NativeHeader("Modules/AI/NavMeshExperimental.bindings.h")]
	[NativeContainer]
	[StaticAccessor("NavMeshQueryBindings", StaticAccessorType.DoubleColon)]
	public struct NavMeshQuery : IDisposable
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000321B File Offset: 0x0000141B
		public NavMeshQuery(NavMeshWorld world, Allocator allocator, int pathNodePoolSize = 0)
		{
			this.m_NavMeshQuery = NavMeshQuery.Create(world, pathNodePoolSize);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000322B File Offset: 0x0000142B
		public void Dispose()
		{
			NavMeshQuery.Destroy(this.m_NavMeshQuery);
			this.m_NavMeshQuery = IntPtr.Zero;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00003245 File Offset: 0x00001445
		private static IntPtr Create(NavMeshWorld world, int nodePoolSize)
		{
			return NavMeshQuery.Create_Injected(ref world, nodePoolSize);
		}

		// Token: 0x06000179 RID: 377
		[MethodImpl(4096)]
		private static extern void Destroy(IntPtr navMeshQuery);

		// Token: 0x0600017A RID: 378 RVA: 0x00003250 File Offset: 0x00001450
		public unsafe PathQueryStatus BeginFindPath(NavMeshLocation start, NavMeshLocation end, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			void* ptr = ((costs.Length > 0) ? costs.GetUnsafePtr<float>() : null);
			return NavMeshQuery.BeginFindPath(this.m_NavMeshQuery, start, end, areaMask, ptr);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003288 File Offset: 0x00001488
		public PathQueryStatus UpdateFindPath(int iterations, out int iterationsPerformed)
		{
			return NavMeshQuery.UpdateFindPath(this.m_NavMeshQuery, iterations, out iterationsPerformed);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000032A8 File Offset: 0x000014A8
		public PathQueryStatus EndFindPath(out int pathSize)
		{
			return NavMeshQuery.EndFindPath(this.m_NavMeshQuery, out pathSize);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000032C8 File Offset: 0x000014C8
		public int GetPathResult(NativeSlice<PolygonId> path)
		{
			return NavMeshQuery.GetPathResult(this.m_NavMeshQuery, path.GetUnsafePtr<PolygonId>(), path.Length);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000032F2 File Offset: 0x000014F2
		[ThreadSafe]
		private unsafe static PathQueryStatus BeginFindPath(IntPtr navMeshQuery, NavMeshLocation start, NavMeshLocation end, int areaMask, void* costs)
		{
			return NavMeshQuery.BeginFindPath_Injected(navMeshQuery, ref start, ref end, areaMask, costs);
		}

		// Token: 0x0600017F RID: 383
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern PathQueryStatus UpdateFindPath(IntPtr navMeshQuery, int iterations, out int iterationsPerformed);

		// Token: 0x06000180 RID: 384
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern PathQueryStatus EndFindPath(IntPtr navMeshQuery, out int pathSize);

		// Token: 0x06000181 RID: 385
		[ThreadSafe]
		[MethodImpl(4096)]
		private unsafe static extern int GetPathResult(IntPtr navMeshQuery, void* path, int maxPath);

		// Token: 0x06000182 RID: 386 RVA: 0x00003301 File Offset: 0x00001501
		[ThreadSafe]
		private static bool IsValidPolygon(IntPtr navMeshQuery, PolygonId polygon)
		{
			return NavMeshQuery.IsValidPolygon_Injected(navMeshQuery, ref polygon);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000330C File Offset: 0x0000150C
		public bool IsValid(PolygonId polygon)
		{
			return polygon.polyRef != 0UL && NavMeshQuery.IsValidPolygon(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003338 File Offset: 0x00001538
		public bool IsValid(NavMeshLocation location)
		{
			return this.IsValid(location.polygon);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003357 File Offset: 0x00001557
		[ThreadSafe]
		private static int GetAgentTypeIdForPolygon(IntPtr navMeshQuery, PolygonId polygon)
		{
			return NavMeshQuery.GetAgentTypeIdForPolygon_Injected(navMeshQuery, ref polygon);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003364 File Offset: 0x00001564
		public int GetAgentTypeIdForPolygon(PolygonId polygon)
		{
			return NavMeshQuery.GetAgentTypeIdForPolygon(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00003382 File Offset: 0x00001582
		[ThreadSafe]
		private static bool IsPositionInPolygon(IntPtr navMeshQuery, Vector3 position, PolygonId polygon)
		{
			return NavMeshQuery.IsPositionInPolygon_Injected(navMeshQuery, ref position, ref polygon);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000338E File Offset: 0x0000158E
		[ThreadSafe]
		private static PathQueryStatus GetClosestPointOnPoly(IntPtr navMeshQuery, PolygonId polygon, Vector3 position, out Vector3 nearest)
		{
			return NavMeshQuery.GetClosestPointOnPoly_Injected(navMeshQuery, ref polygon, ref position, out nearest);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000339C File Offset: 0x0000159C
		public NavMeshLocation CreateLocation(Vector3 position, PolygonId polygon)
		{
			Vector3 vector;
			PathQueryStatus closestPointOnPoly = NavMeshQuery.GetClosestPointOnPoly(this.m_NavMeshQuery, polygon, position, out vector);
			return ((closestPointOnPoly & PathQueryStatus.Success) != (PathQueryStatus)0) ? new NavMeshLocation(vector, polygon) : default(NavMeshLocation);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000033DC File Offset: 0x000015DC
		[ThreadSafe]
		private static NavMeshLocation MapLocation(IntPtr navMeshQuery, Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
		{
			NavMeshLocation navMeshLocation;
			NavMeshQuery.MapLocation_Injected(navMeshQuery, ref position, ref extents, agentTypeID, areaMask, out navMeshLocation);
			return navMeshLocation;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000033FC File Offset: 0x000015FC
		public NavMeshLocation MapLocation(Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
		{
			return NavMeshQuery.MapLocation(this.m_NavMeshQuery, position, extents, agentTypeID, areaMask);
		}

		// Token: 0x0600018C RID: 396
		[ThreadSafe]
		[MethodImpl(4096)]
		private unsafe static extern void MoveLocations(IntPtr navMeshQuery, void* locations, void* targets, void* areaMasks, int count);

		// Token: 0x0600018D RID: 397 RVA: 0x0000341E File Offset: 0x0000161E
		public void MoveLocations(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, NativeSlice<int> areaMasks)
		{
			NavMeshQuery.MoveLocations(this.m_NavMeshQuery, locations.GetUnsafePtr<NavMeshLocation>(), targets.GetUnsafeReadOnlyPtr<Vector3>(), areaMasks.GetUnsafeReadOnlyPtr<int>(), locations.Length);
		}

		// Token: 0x0600018E RID: 398
		[ThreadSafe]
		[MethodImpl(4096)]
		private unsafe static extern void MoveLocationsInSameAreas(IntPtr navMeshQuery, void* locations, void* targets, int count, int areaMask);

		// Token: 0x0600018F RID: 399 RVA: 0x00003446 File Offset: 0x00001646
		public void MoveLocationsInSameAreas(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, int areaMask = -1)
		{
			NavMeshQuery.MoveLocationsInSameAreas(this.m_NavMeshQuery, locations.GetUnsafePtr<NavMeshLocation>(), targets.GetUnsafeReadOnlyPtr<Vector3>(), locations.Length, areaMask);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000346C File Offset: 0x0000166C
		[ThreadSafe]
		private static NavMeshLocation MoveLocation(IntPtr navMeshQuery, NavMeshLocation location, Vector3 target, int areaMask)
		{
			NavMeshLocation navMeshLocation;
			NavMeshQuery.MoveLocation_Injected(navMeshQuery, ref location, ref target, areaMask, out navMeshLocation);
			return navMeshLocation;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00003488 File Offset: 0x00001688
		public NavMeshLocation MoveLocation(NavMeshLocation location, Vector3 target, int areaMask = -1)
		{
			return NavMeshQuery.MoveLocation(this.m_NavMeshQuery, location, target, areaMask);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000034A8 File Offset: 0x000016A8
		[ThreadSafe]
		private static bool GetPortalPoints(IntPtr navMeshQuery, PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
		{
			return NavMeshQuery.GetPortalPoints_Injected(navMeshQuery, ref polygon, ref neighbourPolygon, out left, out right);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000034B8 File Offset: 0x000016B8
		public bool GetPortalPoints(PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
		{
			return NavMeshQuery.GetPortalPoints(this.m_NavMeshQuery, polygon, neighbourPolygon, out left, out right);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000034DC File Offset: 0x000016DC
		[ThreadSafe]
		private static Matrix4x4 PolygonLocalToWorldMatrix(IntPtr navMeshQuery, PolygonId polygon)
		{
			Matrix4x4 matrix4x;
			NavMeshQuery.PolygonLocalToWorldMatrix_Injected(navMeshQuery, ref polygon, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000034F4 File Offset: 0x000016F4
		public Matrix4x4 PolygonLocalToWorldMatrix(PolygonId polygon)
		{
			return NavMeshQuery.PolygonLocalToWorldMatrix(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003514 File Offset: 0x00001714
		[ThreadSafe]
		private static Matrix4x4 PolygonWorldToLocalMatrix(IntPtr navMeshQuery, PolygonId polygon)
		{
			Matrix4x4 matrix4x;
			NavMeshQuery.PolygonWorldToLocalMatrix_Injected(navMeshQuery, ref polygon, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000352C File Offset: 0x0000172C
		public Matrix4x4 PolygonWorldToLocalMatrix(PolygonId polygon)
		{
			return NavMeshQuery.PolygonWorldToLocalMatrix(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000354A File Offset: 0x0000174A
		[ThreadSafe]
		private static NavMeshPolyTypes GetPolygonType(IntPtr navMeshQuery, PolygonId polygon)
		{
			return NavMeshQuery.GetPolygonType_Injected(navMeshQuery, ref polygon);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003554 File Offset: 0x00001754
		public NavMeshPolyTypes GetPolygonType(PolygonId polygon)
		{
			return NavMeshQuery.GetPolygonType(this.m_NavMeshQuery, polygon);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003574 File Offset: 0x00001774
		[ThreadSafe]
		private unsafe static PathQueryStatus Raycast(IntPtr navMeshQuery, NavMeshLocation start, Vector3 targetPosition, int areaMask, void* costs, out NavMeshHit hit, void* path, out int pathCount, int maxPath)
		{
			return NavMeshQuery.Raycast_Injected(navMeshQuery, ref start, ref targetPosition, areaMask, costs, out hit, path, out pathCount, maxPath);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00003598 File Offset: 0x00001798
		public unsafe PathQueryStatus Raycast(out NavMeshHit hit, NavMeshLocation start, Vector3 targetPosition, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			void* ptr = ((costs.Length == 32) ? costs.GetUnsafePtr<float>() : null);
			int num;
			PathQueryStatus pathQueryStatus = NavMeshQuery.Raycast(this.m_NavMeshQuery, start, targetPosition, areaMask, ptr, out hit, null, out num, 0);
			return pathQueryStatus & ~PathQueryStatus.BufferTooSmall;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000035E0 File Offset: 0x000017E0
		public unsafe PathQueryStatus Raycast(out NavMeshHit hit, NativeSlice<PolygonId> path, out int pathCount, NavMeshLocation start, Vector3 targetPosition, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			void* ptr = ((costs.Length == 32) ? costs.GetUnsafePtr<float>() : null);
			void* ptr2 = ((path.Length > 0) ? path.GetUnsafePtr<PolygonId>() : null);
			int num = ((ptr2 != null) ? path.Length : 0);
			return NavMeshQuery.Raycast(this.m_NavMeshQuery, start, targetPosition, areaMask, ptr, out hit, ptr2, out pathCount, num);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00003648 File Offset: 0x00001848
		[ThreadSafe]
		private unsafe static PathQueryStatus GetEdgesAndNeighbors(IntPtr navMeshQuery, PolygonId node, int maxVerts, int maxNei, void* verts, void* neighbors, void* edgeIndices, out int vertCount, out int neighborsCount)
		{
			return NavMeshQuery.GetEdgesAndNeighbors_Injected(navMeshQuery, ref node, maxVerts, maxNei, verts, neighbors, edgeIndices, out vertCount, out neighborsCount);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000366C File Offset: 0x0000186C
		public unsafe PathQueryStatus GetEdgesAndNeighbors(PolygonId node, NativeSlice<Vector3> edgeVertices, NativeSlice<PolygonId> neighbors, NativeSlice<byte> edgeIndices, out int verticesCount, out int neighborsCount)
		{
			void* ptr = ((edgeVertices.Length > 0) ? edgeVertices.GetUnsafePtr<Vector3>() : null);
			void* ptr2 = ((neighbors.Length > 0) ? neighbors.GetUnsafePtr<PolygonId>() : null);
			void* ptr3 = ((edgeIndices.Length > 0) ? edgeIndices.GetUnsafePtr<byte>() : null);
			int length = edgeVertices.Length;
			int num = ((neighbors.Length > 0) ? neighbors.Length : edgeIndices.Length);
			return NavMeshQuery.GetEdgesAndNeighbors(this.m_NavMeshQuery, node, length, num, ptr, ptr2, ptr3, out verticesCount, out neighborsCount);
		}

		// Token: 0x0600019F RID: 415
		[MethodImpl(4096)]
		private static extern IntPtr Create_Injected(ref NavMeshWorld world, int nodePoolSize);

		// Token: 0x060001A0 RID: 416
		[MethodImpl(4096)]
		private unsafe static extern PathQueryStatus BeginFindPath_Injected(IntPtr navMeshQuery, ref NavMeshLocation start, ref NavMeshLocation end, int areaMask, void* costs);

		// Token: 0x060001A1 RID: 417
		[MethodImpl(4096)]
		private static extern bool IsValidPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		// Token: 0x060001A2 RID: 418
		[MethodImpl(4096)]
		private static extern int GetAgentTypeIdForPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		// Token: 0x060001A3 RID: 419
		[MethodImpl(4096)]
		private static extern bool IsPositionInPolygon_Injected(IntPtr navMeshQuery, ref Vector3 position, ref PolygonId polygon);

		// Token: 0x060001A4 RID: 420
		[MethodImpl(4096)]
		private static extern PathQueryStatus GetClosestPointOnPoly_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref Vector3 position, out Vector3 nearest);

		// Token: 0x060001A5 RID: 421
		[MethodImpl(4096)]
		private static extern void MapLocation_Injected(IntPtr navMeshQuery, ref Vector3 position, ref Vector3 extents, int agentTypeID, int areaMask = -1, out NavMeshLocation ret);

		// Token: 0x060001A6 RID: 422
		[MethodImpl(4096)]
		private static extern void MoveLocation_Injected(IntPtr navMeshQuery, ref NavMeshLocation location, ref Vector3 target, int areaMask, out NavMeshLocation ret);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(4096)]
		private static extern bool GetPortalPoints_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref PolygonId neighbourPolygon, out Vector3 left, out Vector3 right);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(4096)]
		private static extern void PolygonLocalToWorldMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(4096)]
		private static extern void PolygonWorldToLocalMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

		// Token: 0x060001AA RID: 426
		[MethodImpl(4096)]
		private static extern NavMeshPolyTypes GetPolygonType_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		// Token: 0x060001AB RID: 427
		[MethodImpl(4096)]
		private unsafe static extern PathQueryStatus Raycast_Injected(IntPtr navMeshQuery, ref NavMeshLocation start, ref Vector3 targetPosition, int areaMask, void* costs, out NavMeshHit hit, void* path, out int pathCount, int maxPath);

		// Token: 0x060001AC RID: 428
		[MethodImpl(4096)]
		private unsafe static extern PathQueryStatus GetEdgesAndNeighbors_Injected(IntPtr navMeshQuery, ref PolygonId node, int maxVerts, int maxNei, void* verts, void* neighbors, void* edgeIndices, out int vertCount, out int neighborsCount);

		// Token: 0x04000073 RID: 115
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr m_NavMeshQuery;
	}
}

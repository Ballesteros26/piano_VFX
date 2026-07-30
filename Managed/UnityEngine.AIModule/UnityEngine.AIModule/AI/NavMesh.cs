using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x02000013 RID: 19
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	[NativeHeader("Modules/AI/NavMeshManager.h")]
	[StaticAccessor("NavMeshBindings", StaticAccessorType.DoubleColon)]
	[MovedFrom("UnityEngine")]
	public static class NavMesh
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000029CC File Offset: 0x00000BCC
		[RequiredByNativeCode]
		private static void Internal_CallOnNavMeshPreUpdate()
		{
			bool flag = NavMesh.onPreUpdate != null;
			if (flag)
			{
				NavMesh.onPreUpdate();
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000029F1 File Offset: 0x00000BF1
		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.Raycast_Injected(ref sourcePosition, ref targetPosition, out hit, areaMask);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002A00 File Offset: 0x00000C00
		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathInternal(sourcePosition, targetPosition, areaMask, path);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002A22 File Offset: 0x00000C22
		private static bool CalculatePathInternal(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			return NavMesh.CalculatePathInternal_Injected(ref sourcePosition, ref targetPosition, areaMask, path);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002A2F File Offset: 0x00000C2F
		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, int areaMask)
		{
			return NavMesh.FindClosestEdge_Injected(ref sourcePosition, out hit, areaMask);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002A3A File Offset: 0x00000C3A
		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask)
		{
			return NavMesh.SamplePosition_Injected(ref sourcePosition, out hit, maxDistance, areaMask);
		}

		// Token: 0x060000F1 RID: 241
		[Obsolete("Use SetAreaCost instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("SetAreaCost")]
		[MethodImpl(4096)]
		public static extern void SetLayerCost(int layer, float cost);

		// Token: 0x060000F2 RID: 242
		[Obsolete("Use GetAreaCost instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaCost")]
		[MethodImpl(4096)]
		public static extern float GetLayerCost(int layer);

		// Token: 0x060000F3 RID: 243
		[Obsolete("Use GetAreaFromName instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaFromName")]
		[MethodImpl(4096)]
		public static extern int GetNavMeshLayerFromName(string layerName);

		// Token: 0x060000F4 RID: 244
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("SetAreaCost")]
		[MethodImpl(4096)]
		public static extern void SetAreaCost(int areaIndex, float cost);

		// Token: 0x060000F5 RID: 245
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaCost")]
		[MethodImpl(4096)]
		public static extern float GetAreaCost(int areaIndex);

		// Token: 0x060000F6 RID: 246
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaFromName")]
		[MethodImpl(4096)]
		public static extern int GetAreaFromName(string areaName);

		// Token: 0x060000F7 RID: 247 RVA: 0x00002A48 File Offset: 0x00000C48
		public static NavMeshTriangulation CalculateTriangulation()
		{
			NavMeshTriangulation navMeshTriangulation;
			NavMesh.CalculateTriangulation_Injected(out navMeshTriangulation);
			return navMeshTriangulation;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002A60 File Offset: 0x00000C60
		[Obsolete("use NavMesh.CalculateTriangulation() instead.")]
		public static void Triangulate(out Vector3[] vertices, out int[] indices)
		{
			NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
			vertices = navMeshTriangulation.vertices;
			indices = navMeshTriangulation.indices;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002A84 File Offset: 0x00000C84
		[Obsolete("AddOffMeshLinks has no effect and is deprecated.")]
		public static void AddOffMeshLinks()
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002A84 File Offset: 0x00000C84
		[Obsolete("RestoreNavMesh has no effect and is deprecated.")]
		public static void RestoreNavMesh()
		{
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FB RID: 251
		// (set) Token: 0x060000FC RID: 252
		[StaticAccessor("GetNavMeshManager()")]
		public static extern float avoidancePredictionTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FD RID: 253
		// (set) Token: 0x060000FE RID: 254
		[StaticAccessor("GetNavMeshManager()")]
		public static extern int pathfindingIterationsPerFrame
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002A88 File Offset: 0x00000C88
		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData)
		{
			bool flag = navMeshData == null;
			if (flag)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = NavMesh.AddNavMeshDataInternal(navMeshData)
			};
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			bool flag = navMeshData == null;
			if (flag)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = NavMesh.AddNavMeshDataTransformedInternal(navMeshData, position, rotation)
			};
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002B09 File Offset: 0x00000D09
		public static void RemoveNavMeshData(NavMeshDataInstance handle)
		{
			NavMesh.RemoveNavMeshDataInternal(handle.id);
		}

		// Token: 0x06000102 RID: 258
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("IsValidSurfaceID")]
		[MethodImpl(4096)]
		internal static extern bool IsValidNavMeshDataHandle(int handle);

		// Token: 0x06000103 RID: 259
		[StaticAccessor("GetNavMeshManager()")]
		[MethodImpl(4096)]
		internal static extern bool IsValidLinkHandle(int handle);

		// Token: 0x06000104 RID: 260
		[MethodImpl(4096)]
		internal static extern Object InternalGetOwner(int dataID);

		// Token: 0x06000105 RID: 261
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("SetSurfaceUserID")]
		[MethodImpl(4096)]
		internal static extern bool InternalSetOwner(int dataID, int ownerID);

		// Token: 0x06000106 RID: 262
		[MethodImpl(4096)]
		internal static extern Object InternalGetLinkOwner(int linkID);

		// Token: 0x06000107 RID: 263
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("SetLinkUserID")]
		[MethodImpl(4096)]
		internal static extern bool InternalSetLinkOwner(int linkID, int ownerID);

		// Token: 0x06000108 RID: 264
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("LoadData")]
		[MethodImpl(4096)]
		internal static extern int AddNavMeshDataInternal(NavMeshData navMeshData);

		// Token: 0x06000109 RID: 265 RVA: 0x00002B19 File Offset: 0x00000D19
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("LoadData")]
		internal static int AddNavMeshDataTransformedInternal(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			return NavMesh.AddNavMeshDataTransformedInternal_Injected(navMeshData, ref position, ref rotation);
		}

		// Token: 0x0600010A RID: 266
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("UnloadData")]
		[MethodImpl(4096)]
		internal static extern void RemoveNavMeshDataInternal(int handle);

		// Token: 0x0600010B RID: 267 RVA: 0x00002B28 File Offset: 0x00000D28
		public static NavMeshLinkInstance AddLink(NavMeshLinkData link)
		{
			return new NavMeshLinkInstance
			{
				id = NavMesh.AddLinkInternal(link, Vector3.zero, Quaternion.identity)
			};
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002B5C File Offset: 0x00000D5C
		public static NavMeshLinkInstance AddLink(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return new NavMeshLinkInstance
			{
				id = NavMesh.AddLinkInternal(link, position, rotation)
			};
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00002B87 File Offset: 0x00000D87
		public static void RemoveLink(NavMeshLinkInstance handle)
		{
			NavMesh.RemoveLinkInternal(handle.id);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002B97 File Offset: 0x00000D97
		[NativeName("AddLink")]
		[StaticAccessor("GetNavMeshManager()")]
		internal static int AddLinkInternal(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return NavMesh.AddLinkInternal_Injected(ref link, ref position, ref rotation);
		}

		// Token: 0x0600010F RID: 271
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("RemoveLink")]
		[MethodImpl(4096)]
		internal static extern void RemoveLinkInternal(int handle);

		// Token: 0x06000110 RID: 272 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, NavMeshQueryFilter filter)
		{
			return NavMesh.SamplePositionFilter(sourcePosition, out hit, maxDistance, filter.agentTypeID, filter.areaMask);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00002BCC File Offset: 0x00000DCC
		private static bool SamplePositionFilter(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask)
		{
			return NavMesh.SamplePositionFilter_Injected(ref sourcePosition, out hit, maxDistance, type, mask);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00002BDC File Offset: 0x00000DDC
		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return NavMesh.FindClosestEdgeFilter(sourcePosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00002C03 File Offset: 0x00000E03
		private static bool FindClosestEdgeFilter(Vector3 sourcePosition, out NavMeshHit hit, int type, int mask)
		{
			return NavMesh.FindClosestEdgeFilter_Injected(ref sourcePosition, out hit, type, mask);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002C10 File Offset: 0x00000E10
		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return NavMesh.RaycastFilter(sourcePosition, targetPosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002C38 File Offset: 0x00000E38
		private static bool RaycastFilter(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int type, int mask)
		{
			return NavMesh.RaycastFilter_Injected(ref sourcePosition, ref targetPosition, out hit, type, mask);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002C48 File Offset: 0x00000E48
		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter filter, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathFilterInternal(sourcePosition, targetPosition, path, filter.agentTypeID, filter.areaMask, filter.costs);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002C7E File Offset: 0x00000E7E
		private static bool CalculatePathFilterInternal(Vector3 sourcePosition, Vector3 targetPosition, NavMeshPath path, int type, int mask, float[] costs)
		{
			return NavMesh.CalculatePathFilterInternal_Injected(ref sourcePosition, ref targetPosition, path, type, mask, costs);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002C90 File Offset: 0x00000E90
		[StaticAccessor("GetNavMeshProjectSettings()")]
		public static NavMeshBuildSettings CreateSettings()
		{
			NavMeshBuildSettings navMeshBuildSettings;
			NavMesh.CreateSettings_Injected(out navMeshBuildSettings);
			return navMeshBuildSettings;
		}

		// Token: 0x06000119 RID: 281
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(4096)]
		public static extern void RemoveSettings(int agentTypeID);

		// Token: 0x0600011A RID: 282 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public static NavMeshBuildSettings GetSettingsByID(int agentTypeID)
		{
			NavMeshBuildSettings navMeshBuildSettings;
			NavMesh.GetSettingsByID_Injected(agentTypeID, out navMeshBuildSettings);
			return navMeshBuildSettings;
		}

		// Token: 0x0600011B RID: 283
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[MethodImpl(4096)]
		public static extern int GetSettingsCount();

		// Token: 0x0600011C RID: 284 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public static NavMeshBuildSettings GetSettingsByIndex(int index)
		{
			NavMeshBuildSettings navMeshBuildSettings;
			NavMesh.GetSettingsByIndex_Injected(index, out navMeshBuildSettings);
			return navMeshBuildSettings;
		}

		// Token: 0x0600011D RID: 285
		[MethodImpl(4096)]
		public static extern string GetSettingsNameFromID(int agentTypeID);

		// Token: 0x0600011E RID: 286
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("CleanupAfterCarving")]
		[MethodImpl(4096)]
		public static extern void RemoveAllNavMeshData();

		// Token: 0x0600011F RID: 287
		[MethodImpl(4096)]
		private static extern bool Raycast_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int areaMask);

		// Token: 0x06000120 RID: 288
		[MethodImpl(4096)]
		private static extern bool CalculatePathInternal_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, int areaMask, NavMeshPath path);

		// Token: 0x06000121 RID: 289
		[MethodImpl(4096)]
		private static extern bool FindClosestEdge_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, int areaMask);

		// Token: 0x06000122 RID: 290
		[MethodImpl(4096)]
		private static extern bool SamplePosition_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask);

		// Token: 0x06000123 RID: 291
		[MethodImpl(4096)]
		private static extern void CalculateTriangulation_Injected(out NavMeshTriangulation ret);

		// Token: 0x06000124 RID: 292
		[MethodImpl(4096)]
		private static extern int AddNavMeshDataTransformedInternal_Injected(NavMeshData navMeshData, ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000125 RID: 293
		[MethodImpl(4096)]
		private static extern int AddLinkInternal_Injected(ref NavMeshLinkData link, ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000126 RID: 294
		[MethodImpl(4096)]
		private static extern bool SamplePositionFilter_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask);

		// Token: 0x06000127 RID: 295
		[MethodImpl(4096)]
		private static extern bool FindClosestEdgeFilter_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, int type, int mask);

		// Token: 0x06000128 RID: 296
		[MethodImpl(4096)]
		private static extern bool RaycastFilter_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int type, int mask);

		// Token: 0x06000129 RID: 297
		[MethodImpl(4096)]
		private static extern bool CalculatePathFilterInternal_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, NavMeshPath path, int type, int mask, float[] costs);

		// Token: 0x0600012A RID: 298
		[MethodImpl(4096)]
		private static extern void CreateSettings_Injected(out NavMeshBuildSettings ret);

		// Token: 0x0600012B RID: 299
		[MethodImpl(4096)]
		private static extern void GetSettingsByID_Injected(int agentTypeID, out NavMeshBuildSettings ret);

		// Token: 0x0600012C RID: 300
		[MethodImpl(4096)]
		private static extern void GetSettingsByIndex_Injected(int index, out NavMeshBuildSettings ret);

		// Token: 0x0400002F RID: 47
		public const int AllAreas = -1;

		// Token: 0x04000030 RID: 48
		public static NavMesh.OnNavMeshPreUpdate onPreUpdate;

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x0600012E RID: 302
		public delegate void OnNavMeshPreUpdate();
	}
}

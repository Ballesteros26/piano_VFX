using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x02000004 RID: 4
	[StaticAccessor("NavMeshBuilderBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/AI/Builder/NavMeshBuilder.bindings.h")]
	public static class NavMeshBuilder
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002104 File Offset: 0x00000304
		public static void CollectSources(Bounds includedWorldBounds, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, List<NavMeshBuildMarkup> markups, List<NavMeshBuildSource> results)
		{
			bool flag = markups == null;
			if (flag)
			{
				throw new ArgumentNullException("markups");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentNullException("results");
			}
			includedWorldBounds.extents = Vector3.Max(includedWorldBounds.extents, 0.001f * Vector3.one);
			NavMeshBuildSource[] array = NavMeshBuilder.CollectSourcesInternal(includedLayerMask, includedWorldBounds, null, true, geometry, defaultArea, markups.ToArray());
			results.Clear();
			results.AddRange(array);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002184 File Offset: 0x00000384
		public static void CollectSources(Transform root, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, List<NavMeshBuildMarkup> markups, List<NavMeshBuildSource> results)
		{
			bool flag = markups == null;
			if (flag)
			{
				throw new ArgumentNullException("markups");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentNullException("results");
			}
			NavMeshBuildSource[] array = NavMeshBuilder.CollectSourcesInternal(includedLayerMask, default(Bounds), root, false, geometry, defaultArea, markups.ToArray());
			results.Clear();
			results.AddRange(array);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021E6 File Offset: 0x000003E6
		private static NavMeshBuildSource[] CollectSourcesInternal(int includedLayerMask, Bounds includedWorldBounds, Transform root, bool useBounds, NavMeshCollectGeometry geometry, int defaultArea, NavMeshBuildMarkup[] markups)
		{
			return NavMeshBuilder.CollectSourcesInternal_Injected(includedLayerMask, ref includedWorldBounds, root, useBounds, geometry, defaultArea, markups);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021F8 File Offset: 0x000003F8
		public static NavMeshData BuildNavMeshData(NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds, Vector3 position, Quaternion rotation)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			NavMeshData navMeshData = new NavMeshData(buildSettings.agentTypeID)
			{
				position = position,
				rotation = rotation
			};
			NavMeshBuilder.UpdateNavMeshDataListInternal(navMeshData, buildSettings, sources, localBounds);
			return navMeshData;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002248 File Offset: 0x00000448
		public static bool UpdateNavMeshData(NavMeshData data, NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = sources == null;
			if (flag2)
			{
				throw new ArgumentNullException("sources");
			}
			return NavMeshBuilder.UpdateNavMeshDataListInternal(data, buildSettings, sources, localBounds);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000228C File Offset: 0x0000048C
		private static bool UpdateNavMeshDataListInternal(NavMeshData data, NavMeshBuildSettings buildSettings, object sources, Bounds localBounds)
		{
			return NavMeshBuilder.UpdateNavMeshDataListInternal_Injected(data, ref buildSettings, sources, ref localBounds);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000229C File Offset: 0x0000049C
		public static AsyncOperation UpdateNavMeshDataAsync(NavMeshData data, NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			bool flag2 = sources == null;
			if (flag2)
			{
				throw new ArgumentNullException("sources");
			}
			return NavMeshBuilder.UpdateNavMeshDataAsyncListInternal(data, buildSettings, sources, localBounds);
		}

		// Token: 0x06000013 RID: 19
		[NativeMethod("Purge")]
		[NativeHeader("Modules/AI/NavMeshManager.h")]
		[StaticAccessor("GetNavMeshManager().GetNavMeshBuildManager()", StaticAccessorType.Arrow)]
		[MethodImpl(4096)]
		public static extern void Cancel(NavMeshData data);

		// Token: 0x06000014 RID: 20 RVA: 0x000022E0 File Offset: 0x000004E0
		private static AsyncOperation UpdateNavMeshDataAsyncListInternal(NavMeshData data, NavMeshBuildSettings buildSettings, object sources, Bounds localBounds)
		{
			return NavMeshBuilder.UpdateNavMeshDataAsyncListInternal_Injected(data, ref buildSettings, sources, ref localBounds);
		}

		// Token: 0x06000015 RID: 21
		[MethodImpl(4096)]
		private static extern NavMeshBuildSource[] CollectSourcesInternal_Injected(int includedLayerMask, ref Bounds includedWorldBounds, Transform root, bool useBounds, NavMeshCollectGeometry geometry, int defaultArea, NavMeshBuildMarkup[] markups);

		// Token: 0x06000016 RID: 22
		[MethodImpl(4096)]
		private static extern bool UpdateNavMeshDataListInternal_Injected(NavMeshData data, ref NavMeshBuildSettings buildSettings, object sources, ref Bounds localBounds);

		// Token: 0x06000017 RID: 23
		[MethodImpl(4096)]
		private static extern AsyncOperation UpdateNavMeshDataAsyncListInternal_Injected(NavMeshData data, ref NavMeshBuildSettings buildSettings, object sources, ref Bounds localBounds);
	}
}

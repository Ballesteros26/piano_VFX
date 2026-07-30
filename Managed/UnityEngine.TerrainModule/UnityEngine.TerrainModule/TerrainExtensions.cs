using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	public static class TerrainExtensions
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002274 File Offset: 0x00000474
		public static void UpdateGIMaterials(this Terrain terrain)
		{
			bool flag = terrain.terrainData == null;
			if (flag)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			TerrainExtensions.UpdateGIMaterialsForTerrain(terrain.GetInstanceID(), new Rect(0f, 0f, 1f, 1f));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000022C4 File Offset: 0x000004C4
		public static void UpdateGIMaterials(this Terrain terrain, int x, int y, int width, int height)
		{
			bool flag = terrain.terrainData == null;
			if (flag)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			float num = (float)terrain.terrainData.alphamapWidth;
			float num2 = (float)terrain.terrainData.alphamapHeight;
			TerrainExtensions.UpdateGIMaterialsForTerrain(terrain.GetInstanceID(), new Rect((float)x / num, (float)y / num2, (float)width / num, (float)height / num2));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002329 File Offset: 0x00000529
		[NativeConditional("INCLUDE_DYNAMIC_GI && ENABLE_RUNTIME_GI")]
		[FreeFunction]
		internal static void UpdateGIMaterialsForTerrain(int terrainInstanceID, Rect uvBounds)
		{
			TerrainExtensions.UpdateGIMaterialsForTerrain_Injected(terrainInstanceID, ref uvBounds);
		}

		// Token: 0x06000071 RID: 113
		[MethodImpl(4096)]
		private static extern void UpdateGIMaterialsForTerrain_Injected(int terrainInstanceID, ref Rect uvBounds);
	}
}

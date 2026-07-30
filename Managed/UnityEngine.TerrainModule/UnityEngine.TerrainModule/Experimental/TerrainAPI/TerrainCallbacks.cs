using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.TerrainAPI
{
	// Token: 0x02000021 RID: 33
	public static class TerrainCallbacks
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001C8 RID: 456 RVA: 0x00005B14 File Offset: 0x00003D14
		// (remove) Token: 0x060001C9 RID: 457 RVA: 0x00005B48 File Offset: 0x00003D48
		[field: DebuggerBrowsable(0)]
		public static event TerrainCallbacks.HeightmapChangedCallback heightmapChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001CA RID: 458 RVA: 0x00005B7C File Offset: 0x00003D7C
		// (remove) Token: 0x060001CB RID: 459 RVA: 0x00005BB0 File Offset: 0x00003DB0
		[field: DebuggerBrowsable(0)]
		public static event TerrainCallbacks.TextureChangedCallback textureChanged;

		// Token: 0x060001CC RID: 460 RVA: 0x00005BE4 File Offset: 0x00003DE4
		[RequiredByNativeCode]
		internal static void InvokeHeightmapChangedCallback(TerrainData terrainData, RectInt heightRegion, bool synched)
		{
			bool flag = TerrainCallbacks.heightmapChanged != null;
			if (flag)
			{
				foreach (Terrain terrain in terrainData.users)
				{
					TerrainCallbacks.heightmapChanged(terrain, heightRegion, synched);
				}
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005C28 File Offset: 0x00003E28
		[RequiredByNativeCode]
		internal static void InvokeTextureChangedCallback(TerrainData terrainData, string textureName, RectInt texelRegion, bool synched)
		{
			bool flag = TerrainCallbacks.textureChanged != null;
			if (flag)
			{
				foreach (Terrain terrain in terrainData.users)
				{
					TerrainCallbacks.textureChanged(terrain, textureName, texelRegion, synched);
				}
			}
		}

		// Token: 0x02000022 RID: 34
		// (Invoke) Token: 0x060001CF RID: 463
		public delegate void HeightmapChangedCallback(Terrain terrain, RectInt heightRegion, bool synched);

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x060001D3 RID: 467
		public delegate void TextureChangedCallback(Terrain terrain, string textureName, RectInt texelRegion, bool synched);
	}
}

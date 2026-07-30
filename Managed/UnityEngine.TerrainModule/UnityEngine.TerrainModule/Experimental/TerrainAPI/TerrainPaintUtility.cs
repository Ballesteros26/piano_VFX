using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.TerrainAPI
{
	// Token: 0x02000024 RID: 36
	public static class TerrainPaintUtility
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00005C70 File Offset: 0x00003E70
		public static Material GetBuiltinPaintMaterial()
		{
			bool flag = TerrainPaintUtility.s_BuiltinPaintMaterial == null;
			if (flag)
			{
				TerrainPaintUtility.s_BuiltinPaintMaterial = new Material(Shader.Find("Hidden/TerrainEngine/PaintHeight"));
			}
			return TerrainPaintUtility.s_BuiltinPaintMaterial;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005CAC File Offset: 0x00003EAC
		public static void GetBrushWorldSizeLimits(out float minBrushWorldSize, out float maxBrushWorldSize, float terrainTileWorldSize, int terrainTileTextureResolutionPixels, int minBrushResolutionPixels = 1, int maxBrushResolutionPixels = 8192)
		{
			bool flag = terrainTileTextureResolutionPixels <= 0;
			if (flag)
			{
				minBrushWorldSize = terrainTileWorldSize;
				maxBrushWorldSize = terrainTileWorldSize;
			}
			else
			{
				float num = terrainTileWorldSize / (float)terrainTileTextureResolutionPixels;
				minBrushWorldSize = (float)minBrushResolutionPixels * num;
				float num2 = (float)Mathf.Min(maxBrushResolutionPixels, SystemInfo.maxTextureSize);
				maxBrushWorldSize = num2 * num;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005CF0 File Offset: 0x00003EF0
		public static BrushTransform CalculateBrushTransform(Terrain terrain, Vector2 brushCenterTerrainUV, float brushSize, float brushRotationDegrees)
		{
			float num = brushRotationDegrees * 0.017453292f;
			float num2 = Mathf.Cos(num);
			float num3 = Mathf.Sin(num);
			Vector2 vector = new Vector2(num2, -num3) * brushSize;
			Vector2 vector2 = new Vector2(num3, num2) * brushSize;
			Vector3 size = terrain.terrainData.size;
			Vector2 vector3 = brushCenterTerrainUV * new Vector2(size.x, size.z);
			Vector2 vector4 = vector3 - 0.5f * vector - 0.5f * vector2;
			BrushTransform brushTransform = new BrushTransform(vector4, vector, vector2);
			return brushTransform;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005D94 File Offset: 0x00003F94
		public static void BuildTransformPaintContextUVToPaintContextUV(PaintContext src, PaintContext dst, out Vector4 scaleOffset)
		{
			float num = ((float)src.pixelRect.xMin - 0.5f) * src.pixelSize.x;
			float num2 = ((float)src.pixelRect.yMin - 0.5f) * src.pixelSize.y;
			float num3 = (float)src.pixelRect.width * src.pixelSize.x;
			float num4 = (float)src.pixelRect.height * src.pixelSize.y;
			float num5 = ((float)dst.pixelRect.xMin - 0.5f) * dst.pixelSize.x;
			float num6 = ((float)dst.pixelRect.yMin - 0.5f) * dst.pixelSize.y;
			float num7 = (float)dst.pixelRect.width * dst.pixelSize.x;
			float num8 = (float)dst.pixelRect.height * dst.pixelSize.y;
			scaleOffset = new Vector4(num3 / num7, num4 / num8, (num - num5) / num7, (num2 - num6) / num8);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005EC8 File Offset: 0x000040C8
		public static void SetupTerrainToolMaterialProperties(PaintContext paintContext, BrushTransform brushXform, Material material)
		{
			float num = ((float)paintContext.pixelRect.xMin - 0.5f) * paintContext.pixelSize.x;
			float num2 = ((float)paintContext.pixelRect.yMin - 0.5f) * paintContext.pixelSize.y;
			float num3 = (float)paintContext.pixelRect.width * paintContext.pixelSize.x;
			float num4 = (float)paintContext.pixelRect.height * paintContext.pixelSize.y;
			Vector2 vector = num3 * brushXform.targetX;
			Vector2 vector2 = num4 * brushXform.targetY;
			Vector2 vector3 = brushXform.targetOrigin + num * brushXform.targetX + num2 * brushXform.targetY;
			material.SetVector("_PCUVToBrushUVScales", new Vector4(vector.x, vector.y, vector2.x, vector2.y));
			material.SetVector("_PCUVToBrushUVOffset", new Vector4(vector3.x, vector3.y, 0f, 0f));
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005FF8 File Offset: 0x000041F8
		internal static bool paintTextureUsesCopyTexture
		{
			get
			{
				return (SystemInfo.copyTextureSupport & (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture)) == (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006018 File Offset: 0x00004218
		internal static PaintContext InitializePaintContext(Terrain terrain, int targetWidth, int targetHeight, RenderTextureFormat pcFormat, Rect boundsInTerrainSpace, int extraBorderPixels = 0, bool texelPadding = true)
		{
			PaintContext paintContext = PaintContext.CreateFromBounds(terrain, boundsInTerrainSpace, targetWidth, targetHeight, extraBorderPixels, texelPadding);
			paintContext.CreateRenderTargets(pcFormat);
			return paintContext;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006042 File Offset: 0x00004242
		public static void ReleaseContextResources(PaintContext ctx)
		{
			ctx.Cleanup(true);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006050 File Offset: 0x00004250
		public static PaintContext BeginPaintHeightmap(Terrain terrain, Rect boundsInTerrainSpace, int extraBorderPixels = 0)
		{
			int heightmapResolution = terrain.terrainData.heightmapResolution;
			PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, heightmapResolution, heightmapResolution, Terrain.heightmapRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, true);
			paintContext.GatherHeightmap();
			return paintContext;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006087 File Offset: 0x00004287
		public static void EndPaintHeightmap(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterHeightmap(editorUndoName);
			ctx.Cleanup(true);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000609C File Offset: 0x0000429C
		public static PaintContext BeginPaintHoles(Terrain terrain, Rect boundsInTerrainSpace, int extraBorderPixels = 0)
		{
			int holesResolution = terrain.terrainData.holesResolution;
			PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, holesResolution, holesResolution, Terrain.holesRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, false);
			paintContext.GatherHoles();
			return paintContext;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000060D3 File Offset: 0x000042D3
		public static void EndPaintHoles(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterHoles(editorUndoName);
			ctx.Cleanup(true);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000060E8 File Offset: 0x000042E8
		public static PaintContext CollectNormals(Terrain terrain, Rect boundsInTerrainSpace, int extraBorderPixels = 0)
		{
			int heightmapResolution = terrain.terrainData.heightmapResolution;
			PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, heightmapResolution, heightmapResolution, Terrain.normalmapRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, true);
			paintContext.GatherNormals();
			return paintContext;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006120 File Offset: 0x00004320
		public static PaintContext BeginPaintTexture(Terrain terrain, Rect boundsInTerrainSpace, TerrainLayer inputLayer, int extraBorderPixels = 0)
		{
			bool flag = inputLayer == null;
			PaintContext paintContext;
			if (flag)
			{
				paintContext = null;
			}
			else
			{
				int alphamapResolution = terrain.terrainData.alphamapResolution;
				PaintContext paintContext2 = TerrainPaintUtility.InitializePaintContext(terrain, alphamapResolution, alphamapResolution, RenderTextureFormat.R8, boundsInTerrainSpace, extraBorderPixels, true);
				paintContext2.GatherAlphamap(inputLayer, true);
				paintContext = paintContext2;
			}
			return paintContext;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006165 File Offset: 0x00004365
		public static void EndPaintTexture(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterAlphamap(editorUndoName);
			ctx.Cleanup(true);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006178 File Offset: 0x00004378
		public static Material GetBlitMaterial()
		{
			bool flag = !TerrainPaintUtility.s_BlitMaterial;
			if (flag)
			{
				TerrainPaintUtility.s_BlitMaterial = new Material(Shader.Find("Hidden/BlitCopy"));
			}
			return TerrainPaintUtility.s_BlitMaterial;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000061B4 File Offset: 0x000043B4
		public static Material GetHeightBlitMaterial()
		{
			bool flag = !TerrainPaintUtility.s_HeightBlitMaterial;
			if (flag)
			{
				TerrainPaintUtility.s_HeightBlitMaterial = new Material(Shader.Find("Hidden/TerrainEngine/HeightBlitCopy"));
			}
			return TerrainPaintUtility.s_HeightBlitMaterial;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000061F0 File Offset: 0x000043F0
		public static Material GetCopyTerrainLayerMaterial()
		{
			bool flag = !TerrainPaintUtility.s_CopyTerrainLayerMaterial;
			if (flag)
			{
				TerrainPaintUtility.s_CopyTerrainLayerMaterial = new Material(Shader.Find("Hidden/TerrainEngine/TerrainLayerUtils"));
			}
			return TerrainPaintUtility.s_CopyTerrainLayerMaterial;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000622C File Offset: 0x0000442C
		internal static void DrawQuad(RectInt destinationPixels, RectInt sourcePixels, Texture sourceTexture)
		{
			TerrainPaintUtility.DrawQuad2(destinationPixels, sourcePixels, sourceTexture, sourcePixels, sourceTexture);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000623C File Offset: 0x0000443C
		internal static void DrawQuad2(RectInt destinationPixels, RectInt sourcePixels, Texture sourceTexture, RectInt sourcePixels2, Texture sourceTexture2)
		{
			bool flag = destinationPixels.width > 0 && destinationPixels.height > 0;
			if (flag)
			{
				Rect rect = new Rect((float)sourcePixels.x / (float)sourceTexture.width, (float)sourcePixels.y / (float)sourceTexture.height, (float)sourcePixels.width / (float)sourceTexture.width, (float)sourcePixels.height / (float)sourceTexture.height);
				Rect rect2 = new Rect((float)sourcePixels2.x / (float)sourceTexture2.width, (float)sourcePixels2.y / (float)sourceTexture2.height, (float)sourcePixels2.width / (float)sourceTexture2.width, (float)sourcePixels2.height / (float)sourceTexture2.height);
				GL.Begin(7);
				GL.Color(new Color(1f, 1f, 1f, 1f));
				GL.MultiTexCoord2(0, rect.x, rect.y);
				GL.MultiTexCoord2(1, rect2.x, rect2.y);
				GL.Vertex3((float)destinationPixels.x, (float)destinationPixels.y, 0f);
				GL.MultiTexCoord2(0, rect.x, rect.yMax);
				GL.MultiTexCoord2(1, rect2.x, rect2.yMax);
				GL.Vertex3((float)destinationPixels.x, (float)destinationPixels.yMax, 0f);
				GL.MultiTexCoord2(0, rect.xMax, rect.yMax);
				GL.MultiTexCoord2(1, rect2.xMax, rect2.yMax);
				GL.Vertex3((float)destinationPixels.xMax, (float)destinationPixels.yMax, 0f);
				GL.MultiTexCoord2(0, rect.xMax, rect.y);
				GL.MultiTexCoord2(1, rect2.xMax, rect2.y);
				GL.Vertex3((float)destinationPixels.xMax, (float)destinationPixels.y, 0f);
				GL.End();
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000643C File Offset: 0x0000463C
		internal static RectInt CalcPixelRectFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int textureWidth, int textureHeight, int extraBorderPixels, bool texelPadding)
		{
			float num = ((float)textureWidth - (texelPadding ? 1f : 0f)) / terrain.terrainData.size.x;
			float num2 = ((float)textureHeight - (texelPadding ? 1f : 0f)) / terrain.terrainData.size.z;
			int num3 = Mathf.FloorToInt(boundsInTerrainSpace.xMin * num) - extraBorderPixels;
			int num4 = Mathf.FloorToInt(boundsInTerrainSpace.yMin * num2) - extraBorderPixels;
			int num5 = Mathf.CeilToInt(boundsInTerrainSpace.xMax * num) + extraBorderPixels;
			int num6 = Mathf.CeilToInt(boundsInTerrainSpace.yMax * num2) + extraBorderPixels;
			return new RectInt(num3, num4, num5 - num3 + 1, num6 - num4 + 1);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000064F8 File Offset: 0x000046F8
		public static Texture2D GetTerrainAlphaMapChecked(Terrain terrain, int mapIndex)
		{
			bool flag = mapIndex >= terrain.terrainData.alphamapTextureCount;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain alphamap information.");
			}
			return terrain.terrainData.GetAlphamapTexture(mapIndex);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006538 File Offset: 0x00004738
		public static int FindTerrainLayerIndex(Terrain terrain, TerrainLayer inputLayer)
		{
			TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
			for (int i = 0; i < terrainLayers.Length; i++)
			{
				bool flag = terrainLayers[i] == inputLayer;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006580 File Offset: 0x00004780
		internal static int AddTerrainLayer(Terrain terrain, TerrainLayer inputLayer)
		{
			TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
			int num = terrainLayers.Length;
			TerrainLayer[] array = new TerrainLayer[num + 1];
			Array.Copy(terrainLayers, 0, array, 0, num);
			array[num] = inputLayer;
			terrain.terrainData.terrainLayers = array;
			return num;
		}

		// Token: 0x04000099 RID: 153
		private static Material s_BuiltinPaintMaterial = null;

		// Token: 0x0400009A RID: 154
		private static Material s_BlitMaterial = null;

		// Token: 0x0400009B RID: 155
		private static Material s_HeightBlitMaterial = null;

		// Token: 0x0400009C RID: 156
		private static Material s_CopyTerrainLayerMaterial = null;

		// Token: 0x02000025 RID: 37
		public enum BuiltinPaintMaterialPasses
		{
			// Token: 0x0400009E RID: 158
			RaiseLowerHeight,
			// Token: 0x0400009F RID: 159
			StampHeight,
			// Token: 0x040000A0 RID: 160
			SetHeights,
			// Token: 0x040000A1 RID: 161
			SmoothHeights,
			// Token: 0x040000A2 RID: 162
			PaintTexture,
			// Token: 0x040000A3 RID: 163
			PaintHoles
		}
	}
}

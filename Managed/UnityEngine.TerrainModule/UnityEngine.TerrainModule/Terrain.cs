using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/Terrain/Public/Terrain.h")]
	[NativeHeader("Runtime/Interfaces/ITerrainManager.h")]
	[NativeHeader("TerrainScriptingClasses.h")]
	[StaticAccessor("GetITerrainManager()", StaticAccessorType.Arrow)]
	[UsedByNativeCode]
	public sealed class Terrain : Behaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		public extern TerrainData terrainData
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		public extern float treeDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5
		// (set) Token: 0x06000006 RID: 6
		public extern float treeBillboardDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7
		// (set) Token: 0x06000008 RID: 8
		public extern float treeCrossFadeLength
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9
		// (set) Token: 0x0600000A RID: 10
		public extern int treeMaximumFullLODCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		public extern float detailObjectDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13
		// (set) Token: 0x0600000E RID: 14
		public extern float detailObjectDensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15
		// (set) Token: 0x06000010 RID: 16
		public extern float heightmapPixelError
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17
		// (set) Token: 0x06000012 RID: 18
		public extern int heightmapMaximumLOD
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19
		// (set) Token: 0x06000014 RID: 20
		public extern float basemapDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21
		// (set) Token: 0x06000016 RID: 22
		[NativeProperty("StaticLightmapIndexInt")]
		public extern int lightmapIndex
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23
		// (set) Token: 0x06000018 RID: 24
		[NativeProperty("DynamicLightmapIndexInt")]
		public extern int realtimeLightmapIndex
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002066 File Offset: 0x00000266
		[NativeProperty("StaticLightmapST")]
		public Vector4 lightmapScaleOffset
		{
			get
			{
				Vector4 vector;
				this.get_lightmapScaleOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_lightmapScaleOffset_Injected(ref value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002070 File Offset: 0x00000270
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002086 File Offset: 0x00000286
		[NativeProperty("DynamicLightmapST")]
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				Vector4 vector;
				this.get_realtimeLightmapScaleOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_realtimeLightmapScaleOffset_Injected(ref value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29
		// (set) Token: 0x0600001E RID: 30
		[NativeProperty("GarbageCollectCameraData")]
		public extern bool freeUnusedRenderingResources
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31
		// (set) Token: 0x06000020 RID: 32
		public extern ShadowCastingMode shadowCastingMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33
		// (set) Token: 0x06000022 RID: 34
		public extern ReflectionProbeUsage reflectionProbeUsage
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000023 RID: 35
		[MethodImpl(4096)]
		public extern void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result);

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36
		// (set) Token: 0x06000025 RID: 37
		public extern Material materialTemplate
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38
		// (set) Token: 0x06000027 RID: 39
		public extern bool drawHeightmap
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000028 RID: 40
		// (set) Token: 0x06000029 RID: 41
		public extern bool allowAutoConnect
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002A RID: 42
		// (set) Token: 0x0600002B RID: 43
		public extern int groupingID
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002C RID: 44
		// (set) Token: 0x0600002D RID: 45
		public extern bool drawInstanced
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002E RID: 46
		public extern RenderTexture normalmapTexture
		{
			[NativeMethod("TryGetNormalMapTexture")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002F RID: 47
		// (set) Token: 0x06000030 RID: 48
		public extern bool drawTreesAndFoliage
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002090 File Offset: 0x00000290
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000020A6 File Offset: 0x000002A6
		public Vector3 patchBoundsMultiplier
		{
			get
			{
				Vector3 vector;
				this.get_patchBoundsMultiplier_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_patchBoundsMultiplier_Injected(ref value);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000020B0 File Offset: 0x000002B0
		public float SampleHeight(Vector3 worldPosition)
		{
			return this.SampleHeight_Injected(ref worldPosition);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000020BA File Offset: 0x000002BA
		public void AddTreeInstance(TreeInstance instance)
		{
			this.AddTreeInstance_Injected(ref instance);
		}

		// Token: 0x06000035 RID: 53
		[MethodImpl(4096)]
		public extern void SetNeighbors(Terrain left, Terrain top, Terrain right, Terrain bottom);

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000036 RID: 54
		// (set) Token: 0x06000037 RID: 55
		public extern float treeLODBiasMultiplier
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000038 RID: 56
		// (set) Token: 0x06000039 RID: 57
		public extern bool collectDetailPatches
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003A RID: 58
		// (set) Token: 0x0600003B RID: 59
		public extern TerrainRenderFlags editorRenderFlags
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000020C4 File Offset: 0x000002C4
		public Vector3 GetPosition()
		{
			Vector3 vector;
			this.GetPosition_Injected(out vector);
			return vector;
		}

		// Token: 0x0600003D RID: 61
		[MethodImpl(4096)]
		public extern void Flush();

		// Token: 0x0600003E RID: 62 RVA: 0x000020DA File Offset: 0x000002DA
		internal void RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			this.RemoveTrees_Injected(ref position, radius, prototypeIndex);
		}

		// Token: 0x0600003F RID: 63
		[NativeMethod("CopySplatMaterialCustomProps")]
		[MethodImpl(4096)]
		public extern void SetSplatMaterialPropertyBlock(MaterialPropertyBlock properties);

		// Token: 0x06000040 RID: 64 RVA: 0x000020E8 File Offset: 0x000002E8
		public void GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest)
		{
			bool flag = dest == null;
			if (flag)
			{
				throw new ArgumentNullException("dest");
			}
			this.Internal_GetSplatMaterialPropertyBlock(dest);
		}

		// Token: 0x06000041 RID: 65
		[NativeMethod("GetSplatMaterialCustomProps")]
		[MethodImpl(4096)]
		private extern void Internal_GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66
		// (set) Token: 0x06000043 RID: 67
		public extern bool preserveTreePrototypeLayers
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68
		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat heightmapFormat
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002114 File Offset: 0x00000314
		public static TextureFormat heightmapTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetTextureFormat(Terrain.heightmapFormat);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002130 File Offset: 0x00000330
		public static RenderTextureFormat heightmapRenderTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(Terrain.heightmapFormat);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000047 RID: 71
		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat normalmapFormat
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000214C File Offset: 0x0000034C
		public static TextureFormat normalmapTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetTextureFormat(Terrain.normalmapFormat);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002168 File Offset: 0x00000368
		public static RenderTextureFormat normalmapRenderTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(Terrain.normalmapFormat);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004A RID: 74
		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat holesFormat
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002184 File Offset: 0x00000384
		public static RenderTextureFormat holesRenderTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(Terrain.holesFormat);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004C RID: 76
		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat compressedHolesFormat
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000021A0 File Offset: 0x000003A0
		public static TextureFormat compressedHolesTextureFormat
		{
			get
			{
				return GraphicsFormatUtility.GetTextureFormat(Terrain.compressedHolesFormat);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600004E RID: 78
		public static extern Terrain activeTerrain
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600004F RID: 79
		[MethodImpl(4096)]
		public static extern void SetConnectivityDirty();

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000050 RID: 80
		[NativeProperty("ActiveTerrainsScriptingArray")]
		public static extern Terrain[] activeTerrains
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000051 RID: 81
		[UsedByNativeCode]
		[MethodImpl(4096)]
		public static extern GameObject CreateTerrainGameObject(TerrainData assignTerrain);

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000052 RID: 82
		public extern Terrain leftNeighbor
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000053 RID: 83
		public extern Terrain rightNeighbor
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000054 RID: 84
		public extern Terrain topNeighbor
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000055 RID: 85
		public extern Terrain bottomNeighbor
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000056 RID: 86
		// (set) Token: 0x06000057 RID: 87
		public extern uint renderingLayerMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000021BC File Offset: 0x000003BC
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000021D4 File Offset: 0x000003D4
		[Obsolete("splatmapDistance is deprecated, please use basemapDistance instead. (UnityUpgradable) -> basemapDistance", true)]
		[EditorBrowsable(1)]
		public float splatmapDistance
		{
			get
			{
				return this.basemapDistance;
			}
			set
			{
				this.basemapDistance = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000021E0 File Offset: 0x000003E0
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000021FB File Offset: 0x000003FB
		[Obsolete("castShadows is deprecated, please use shadowCastingMode instead.")]
		public bool castShadows
		{
			get
			{
				return this.shadowCastingMode > ShadowCastingMode.Off;
			}
			set
			{
				this.shadowCastingMode = (value ? ShadowCastingMode.TwoSided : ShadowCastingMode.Off);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000220C File Offset: 0x0000040C
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000221F File Offset: 0x0000041F
		[Obsolete("Property materialType is not used any more. Set materialTemplate directly.", false)]
		public Terrain.MaterialType materialType
		{
			get
			{
				return Terrain.MaterialType.Custom;
			}
			set
			{
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002224 File Offset: 0x00000424
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000221F File Offset: 0x0000041F
		[Obsolete("Property legacySpecular is not used any more. Set materialTemplate directly.", false)]
		public Color legacySpecular
		{
			get
			{
				return Color.gray;
			}
			set
			{
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000223C File Offset: 0x0000043C
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000221F File Offset: 0x0000041F
		[Obsolete("Property legacyShininess is not used any more. Set materialTemplate directly.", false)]
		public float legacyShininess
		{
			get
			{
				return 0.078125f;
			}
			set
			{
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002253 File Offset: 0x00000453
		[Obsolete("Use TerrainData.SyncHeightmap to notify all Terrain instances using the TerrainData.", false)]
		public void ApplyDelayedHeightmapModification()
		{
			TerrainData terrainData = this.terrainData;
			if (terrainData != null)
			{
				terrainData.SyncHeightmap();
			}
		}

		// Token: 0x06000064 RID: 100
		[MethodImpl(4096)]
		private extern void get_lightmapScaleOffset_Injected(out Vector4 ret);

		// Token: 0x06000065 RID: 101
		[MethodImpl(4096)]
		private extern void set_lightmapScaleOffset_Injected(ref Vector4 value);

		// Token: 0x06000066 RID: 102
		[MethodImpl(4096)]
		private extern void get_realtimeLightmapScaleOffset_Injected(out Vector4 ret);

		// Token: 0x06000067 RID: 103
		[MethodImpl(4096)]
		private extern void set_realtimeLightmapScaleOffset_Injected(ref Vector4 value);

		// Token: 0x06000068 RID: 104
		[MethodImpl(4096)]
		private extern void get_patchBoundsMultiplier_Injected(out Vector3 ret);

		// Token: 0x06000069 RID: 105
		[MethodImpl(4096)]
		private extern void set_patchBoundsMultiplier_Injected(ref Vector3 value);

		// Token: 0x0600006A RID: 106
		[MethodImpl(4096)]
		private extern float SampleHeight_Injected(ref Vector3 worldPosition);

		// Token: 0x0600006B RID: 107
		[MethodImpl(4096)]
		private extern void AddTreeInstance_Injected(ref TreeInstance instance);

		// Token: 0x0600006C RID: 108
		[MethodImpl(4096)]
		private extern void GetPosition_Injected(out Vector3 ret);

		// Token: 0x0600006D RID: 109
		[MethodImpl(4096)]
		private extern void RemoveTrees_Injected(ref Vector2 position, float radius, int prototypeIndex);

		// Token: 0x02000005 RID: 5
		[Obsolete("Enum type MaterialType is not used any more.", false)]
		public enum MaterialType
		{
			// Token: 0x04000015 RID: 21
			BuiltInStandard,
			// Token: 0x04000016 RID: 22
			BuiltInLegacyDiffuse,
			// Token: 0x04000017 RID: 23
			BuiltInLegacySpecular,
			// Token: 0x04000018 RID: 24
			Custom
		}
	}
}

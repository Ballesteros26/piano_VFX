using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000074 RID: 116
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, needAccessors = false, omitStructDeclaration = true)]
	internal struct ShaderVariablesLightLoop
	{
		// Token: 0x040004DE RID: 1246
		public const int s_MaxEnv2DLight = 32;

		// Token: 0x040004DF RID: 1247
		[FixedBuffer(typeof(float), 4)]
		[HLSLArray(0, typeof(Vector4))]
		public ShaderVariablesLightLoop.<_ShadowAtlasSize>e__FixedBuffer _ShadowAtlasSize;

		// Token: 0x040004E0 RID: 1248
		[FixedBuffer(typeof(float), 4)]
		[HLSLArray(0, typeof(Vector4))]
		public ShaderVariablesLightLoop.<_CascadeShadowAtlasSize>e__FixedBuffer _CascadeShadowAtlasSize;

		// Token: 0x040004E1 RID: 1249
		[FixedBuffer(typeof(float), 4)]
		[HLSLArray(0, typeof(Vector4))]
		public ShaderVariablesLightLoop.<_AreaShadowAtlasSize>e__FixedBuffer _AreaShadowAtlasSize;

		// Token: 0x040004E2 RID: 1250
		[FixedBuffer(typeof(float), 512)]
		[HLSLArray(32, typeof(Matrix4x4))]
		public ShaderVariablesLightLoop.<_Env2DCaptureVP>e__FixedBuffer _Env2DCaptureVP;

		// Token: 0x040004E3 RID: 1251
		[FixedBuffer(typeof(float), 96)]
		[HLSLArray(96, typeof(float))]
		public ShaderVariablesLightLoop.<_Env2DCaptureForward>e__FixedBuffer _Env2DCaptureForward;

		// Token: 0x040004E4 RID: 1252
		[FixedBuffer(typeof(float), 96)]
		[HLSLArray(32, typeof(Vector4))]
		public ShaderVariablesLightLoop.<_Env2DAtlasScaleOffset>e__FixedBuffer _Env2DAtlasScaleOffset;

		// Token: 0x040004E5 RID: 1253
		public uint _DirectionalLightCount;

		// Token: 0x040004E6 RID: 1254
		public uint _PunctualLightCount;

		// Token: 0x040004E7 RID: 1255
		public uint _AreaLightCount;

		// Token: 0x040004E8 RID: 1256
		public uint _EnvLightCount;

		// Token: 0x040004E9 RID: 1257
		public uint _EnvProxyCount;

		// Token: 0x040004EA RID: 1258
		public int _EnvLightSkyEnabled;

		// Token: 0x040004EB RID: 1259
		public int _DirectionalShadowIndex;

		// Token: 0x040004EC RID: 1260
		public Vector4 _CookieAtlasSize;

		// Token: 0x040004ED RID: 1261
		public Vector4 _CookieAtlasData;

		// Token: 0x040004EE RID: 1262
		public Vector4 _PlanarAtlasData;

		// Token: 0x040004EF RID: 1263
		public float _MicroShadowOpacity;

		// Token: 0x040004F0 RID: 1264
		public float _DirectionalTransmissionMultiplier;

		// Token: 0x040004F1 RID: 1265
		public uint _NumTileFtplX;

		// Token: 0x040004F2 RID: 1266
		public uint _NumTileFtplY;

		// Token: 0x040004F3 RID: 1267
		public float g_fClustScale;

		// Token: 0x040004F4 RID: 1268
		public float g_fClustBase;

		// Token: 0x040004F5 RID: 1269
		public float g_fNearPlane;

		// Token: 0x040004F6 RID: 1270
		public float g_fFarPlane;

		// Token: 0x040004F7 RID: 1271
		public int g_iLog2NumClusters;

		// Token: 0x040004F8 RID: 1272
		public uint g_isLogBaseBufferEnabled;

		// Token: 0x040004F9 RID: 1273
		public uint _NumTileClusteredX;

		// Token: 0x040004FA RID: 1274
		public uint _NumTileClusteredY;

		// Token: 0x040004FB RID: 1275
		public uint _CascadeShadowCount;

		// Token: 0x040004FC RID: 1276
		public int _DebugSingleShadowIndex;

		// Token: 0x040004FD RID: 1277
		public int _EnvSliceSize;

		// Token: 0x040004FE RID: 1278
		public int _RaytracedIndirectDiffuse;

		// Token: 0x020001FA RID: 506
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <_ShadowAtlasSize>e__FixedBuffer
		{
			// Token: 0x0400135D RID: 4957
			public float FixedElementField;
		}

		// Token: 0x020001FB RID: 507
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <_CascadeShadowAtlasSize>e__FixedBuffer
		{
			// Token: 0x0400135E RID: 4958
			public float FixedElementField;
		}

		// Token: 0x020001FC RID: 508
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <_AreaShadowAtlasSize>e__FixedBuffer
		{
			// Token: 0x0400135F RID: 4959
			public float FixedElementField;
		}

		// Token: 0x020001FD RID: 509
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 2048)]
		public struct <_Env2DCaptureVP>e__FixedBuffer
		{
			// Token: 0x04001360 RID: 4960
			public float FixedElementField;
		}

		// Token: 0x020001FE RID: 510
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 384)]
		public struct <_Env2DCaptureForward>e__FixedBuffer
		{
			// Token: 0x04001361 RID: 4961
			public float FixedElementField;
		}

		// Token: 0x020001FF RID: 511
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 384)]
		public struct <_Env2DAtlasScaleOffset>e__FixedBuffer
		{
			// Token: 0x04001362 RID: 4962
			public float FixedElementField;
		}
	}
}

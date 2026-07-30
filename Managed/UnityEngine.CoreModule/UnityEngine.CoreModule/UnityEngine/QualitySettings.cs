using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000EB RID: 235
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[StaticAccessor("GetQualitySettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/QualitySettings.h")]
	public sealed class QualitySettings : Object
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x0000C554 File Offset: 0x0000A754
		public static void IncreaseLevel([DefaultValue("false")] bool applyExpensiveChanges)
		{
			QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() + 1, applyExpensiveChanges);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0000C565 File Offset: 0x0000A765
		public static void DecreaseLevel([DefaultValue("false")] bool applyExpensiveChanges)
		{
			QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() - 1, applyExpensiveChanges);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000C576 File Offset: 0x0000A776
		public static void SetQualityLevel(int index)
		{
			QualitySettings.SetQualityLevel(index, true);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000C581 File Offset: 0x0000A781
		public static void IncreaseLevel()
		{
			QualitySettings.IncreaseLevel(false);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000C58B File Offset: 0x0000A78B
		public static void DecreaseLevel()
		{
			QualitySettings.DecreaseLevel(false);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0000C598 File Offset: 0x0000A798
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0000C576 File Offset: 0x0000A776
		[Obsolete("Use GetQualityLevel and SetQualityLevel", false)]
		public static QualityLevel currentLevel
		{
			get
			{
				return (QualityLevel)QualitySettings.GetQualityLevel();
			}
			set
			{
				QualitySettings.SetQualityLevel((int)value, true);
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		private QualitySettings()
		{
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060007DA RID: 2010
		// (set) Token: 0x060007DB RID: 2011
		public static extern int pixelLightCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060007DC RID: 2012
		// (set) Token: 0x060007DD RID: 2013
		[NativeProperty("ShadowQuality")]
		public static extern ShadowQuality shadows
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007DE RID: 2014
		// (set) Token: 0x060007DF RID: 2015
		public static extern ShadowProjection shadowProjection
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007E0 RID: 2016
		// (set) Token: 0x060007E1 RID: 2017
		public static extern int shadowCascades
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007E2 RID: 2018
		// (set) Token: 0x060007E3 RID: 2019
		public static extern float shadowDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007E4 RID: 2020
		// (set) Token: 0x060007E5 RID: 2021
		[NativeProperty("ShadowResolution")]
		public static extern ShadowResolution shadowResolution
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007E6 RID: 2022
		// (set) Token: 0x060007E7 RID: 2023
		[NativeProperty("ShadowmaskMode")]
		public static extern ShadowmaskMode shadowmaskMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007E8 RID: 2024
		// (set) Token: 0x060007E9 RID: 2025
		public static extern float shadowNearPlaneOffset
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007EA RID: 2026
		// (set) Token: 0x060007EB RID: 2027
		public static extern float shadowCascade2Split
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0000C5C5 File Offset: 0x0000A7C5
		public static Vector3 shadowCascade4Split
		{
			get
			{
				Vector3 vector;
				QualitySettings.get_shadowCascade4Split_Injected(out vector);
				return vector;
			}
			set
			{
				QualitySettings.set_shadowCascade4Split_Injected(ref value);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007EE RID: 2030
		// (set) Token: 0x060007EF RID: 2031
		[NativeProperty("LODBias")]
		public static extern float lodBias
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007F0 RID: 2032
		// (set) Token: 0x060007F1 RID: 2033
		[NativeProperty("AnisotropicTextures")]
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007F2 RID: 2034
		// (set) Token: 0x060007F3 RID: 2035
		public static extern int masterTextureLimit
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007F4 RID: 2036
		// (set) Token: 0x060007F5 RID: 2037
		public static extern int maximumLODLevel
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007F6 RID: 2038
		// (set) Token: 0x060007F7 RID: 2039
		public static extern int particleRaycastBudget
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007F8 RID: 2040
		// (set) Token: 0x060007F9 RID: 2041
		public static extern bool softParticles
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007FA RID: 2042
		// (set) Token: 0x060007FB RID: 2043
		public static extern bool softVegetation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060007FC RID: 2044
		// (set) Token: 0x060007FD RID: 2045
		public static extern int vSyncCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007FE RID: 2046
		// (set) Token: 0x060007FF RID: 2047
		public static extern int antiAliasing
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000800 RID: 2048
		// (set) Token: 0x06000801 RID: 2049
		public static extern int asyncUploadTimeSlice
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000802 RID: 2050
		// (set) Token: 0x06000803 RID: 2051
		public static extern int asyncUploadBufferSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000804 RID: 2052
		// (set) Token: 0x06000805 RID: 2053
		public static extern bool asyncUploadPersistentBuffer
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000806 RID: 2054
		// (set) Token: 0x06000807 RID: 2055
		public static extern bool realtimeReflectionProbes
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000808 RID: 2056
		// (set) Token: 0x06000809 RID: 2057
		public static extern bool billboardsFaceCameraPosition
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600080A RID: 2058
		// (set) Token: 0x0600080B RID: 2059
		public static extern float resolutionScalingFixedDPIFactor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600080C RID: 2060
		// (set) Token: 0x0600080D RID: 2061
		[NativeName("RenderPipeline")]
		private static extern ScriptableObject INTERNAL_renderPipeline
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public static RenderPipelineAsset renderPipeline
		{
			get
			{
				return QualitySettings.INTERNAL_renderPipeline as RenderPipelineAsset;
			}
			set
			{
				QualitySettings.INTERNAL_renderPipeline = value;
			}
		}

		// Token: 0x06000810 RID: 2064
		[NativeName("GetRenderPipelineAssetAt")]
		[MethodImpl(4096)]
		internal static extern ScriptableObject InternalGetRenderPipelineAssetAt(int index);

		// Token: 0x06000811 RID: 2065 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		public static RenderPipelineAsset GetRenderPipelineAssetAt(int index)
		{
			bool flag = index < 0 || index >= QualitySettings.names.Length;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("{0} is out of range [0..{1}[", "index", QualitySettings.names.Length));
			}
			return QualitySettings.InternalGetRenderPipelineAssetAt(index) as RenderPipelineAsset;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000812 RID: 2066
		// (set) Token: 0x06000813 RID: 2067
		[Obsolete("blendWeights is obsolete. Use skinWeights instead (UnityUpgradable) -> skinWeights", true)]
		public static extern BlendWeights blendWeights
		{
			[NativeName("GetSkinWeights")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetSkinWeights")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000814 RID: 2068
		// (set) Token: 0x06000815 RID: 2069
		public static extern SkinWeights skinWeights
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000816 RID: 2070
		// (set) Token: 0x06000817 RID: 2071
		public static extern bool streamingMipmapsActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000818 RID: 2072
		// (set) Token: 0x06000819 RID: 2073
		public static extern float streamingMipmapsMemoryBudget
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600081A RID: 2074
		public static extern int streamingMipmapsRenderersPerFrame
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600081B RID: 2075
		// (set) Token: 0x0600081C RID: 2076
		public static extern int streamingMipmapsMaxLevelReduction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600081D RID: 2077
		// (set) Token: 0x0600081E RID: 2078
		public static extern bool streamingMipmapsAddAllCameras
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600081F RID: 2079
		// (set) Token: 0x06000820 RID: 2080
		public static extern int streamingMipmapsMaxFileIORequests
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000821 RID: 2081
		// (set) Token: 0x06000822 RID: 2082
		[StaticAccessor("QualitySettingsScripting", StaticAccessorType.DoubleColon)]
		public static extern int maxQueuedFrames
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000823 RID: 2083
		[NativeName("GetCurrentIndex")]
		[MethodImpl(4096)]
		public static extern int GetQualityLevel();

		// Token: 0x06000824 RID: 2084
		[NativeName("SetCurrentIndex")]
		[MethodImpl(4096)]
		public static extern void SetQualityLevel(int index, [DefaultValue("true")] bool applyExpensiveChanges);

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000825 RID: 2085
		[NativeProperty("QualitySettingsNames")]
		public static extern string[] names
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000826 RID: 2086
		public static extern ColorSpace desiredColorSpace
		{
			[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
			[NativeName("GetColorSpace")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000827 RID: 2087
		public static extern ColorSpace activeColorSpace
		{
			[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
			[NativeName("GetColorSpace")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000828 RID: 2088
		[MethodImpl(4096)]
		private static extern void get_shadowCascade4Split_Injected(out Vector3 ret);

		// Token: 0x06000829 RID: 2089
		[MethodImpl(4096)]
		private static extern void set_shadowCascade4Split_Injected(ref Vector3 value);
	}
}

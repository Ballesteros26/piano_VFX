using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000382 RID: 898
	public class SupportedRenderingFeatures
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0003546C File Offset: 0x0003366C
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x00035499 File Offset: 0x00033699
		public static SupportedRenderingFeatures active
		{
			get
			{
				bool flag = SupportedRenderingFeatures.s_Active == null;
				if (flag)
				{
					SupportedRenderingFeatures.s_Active = new SupportedRenderingFeatures();
				}
				return SupportedRenderingFeatures.s_Active;
			}
			set
			{
				SupportedRenderingFeatures.s_Active = value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x000354A2 File Offset: 0x000336A2
		// (set) Token: 0x06001F3C RID: 7996 RVA: 0x000354AA File Offset: 0x000336AA
		public SupportedRenderingFeatures.ReflectionProbeModes reflectionProbeModes { get; set; } = SupportedRenderingFeatures.ReflectionProbeModes.None;

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x000354B3 File Offset: 0x000336B3
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x000354BB File Offset: 0x000336BB
		public SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes { get; set; } = SupportedRenderingFeatures.LightmapMixedBakeModes.None;

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x000354C4 File Offset: 0x000336C4
		// (set) Token: 0x06001F40 RID: 8000 RVA: 0x000354CC File Offset: 0x000336CC
		public SupportedRenderingFeatures.LightmapMixedBakeModes mixedLightingModes { get; set; } = SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly | SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive | SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask;

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000354D5 File Offset: 0x000336D5
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x000354DD File Offset: 0x000336DD
		public LightmapBakeType lightmapBakeTypes { get; set; } = LightmapBakeType.Realtime | LightmapBakeType.Baked | LightmapBakeType.Mixed;

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x000354E6 File Offset: 0x000336E6
		// (set) Token: 0x06001F44 RID: 8004 RVA: 0x000354EE File Offset: 0x000336EE
		public LightmapsMode lightmapsModes { get; set; } = LightmapsMode.CombinedDirectional;

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x000354F7 File Offset: 0x000336F7
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x000354FF File Offset: 0x000336FF
		public bool enlighten { get; set; } = true;

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00035508 File Offset: 0x00033708
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x00035510 File Offset: 0x00033710
		public bool lightProbeProxyVolumes { get; set; } = true;

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00035519 File Offset: 0x00033719
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x00035521 File Offset: 0x00033721
		public bool motionVectors { get; set; } = true;

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0003552A File Offset: 0x0003372A
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x00035532 File Offset: 0x00033732
		public bool receiveShadows { get; set; } = true;

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0003553B File Offset: 0x0003373B
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x00035543 File Offset: 0x00033743
		public bool reflectionProbes { get; set; } = true;

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0003554C File Offset: 0x0003374C
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x00035554 File Offset: 0x00033754
		public bool rendererPriority { get; set; } = false;

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0003555D File Offset: 0x0003375D
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x00035565 File Offset: 0x00033765
		public bool terrainDetailUnsupported { get; set; } = false;

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0003556E File Offset: 0x0003376E
		// (set) Token: 0x06001F54 RID: 8020 RVA: 0x00035576 File Offset: 0x00033776
		public bool rendersUIOverlay { get; set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x0003557F File Offset: 0x0003377F
		// (set) Token: 0x06001F56 RID: 8022 RVA: 0x00035587 File Offset: 0x00033787
		public bool overridesEnvironmentLighting { get; set; } = false;

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x00035590 File Offset: 0x00033790
		// (set) Token: 0x06001F58 RID: 8024 RVA: 0x00035598 File Offset: 0x00033798
		public bool overridesFog { get; set; } = false;

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x000355A1 File Offset: 0x000337A1
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x000355A9 File Offset: 0x000337A9
		public bool overridesOtherLightingSettings { get; set; } = false;

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000355B2 File Offset: 0x000337B2
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x000355BA File Offset: 0x000337BA
		public bool editableMaterialRenderQueue { get; set; } = true;

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x000355C3 File Offset: 0x000337C3
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x000355CB File Offset: 0x000337CB
		public bool overridesLODBias { get; set; } = false;

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x000355D4 File Offset: 0x000337D4
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x000355DC File Offset: 0x000337DC
		public bool overridesMaximumLODLevel { get; set; } = false;

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000355E5 File Offset: 0x000337E5
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x000355ED File Offset: 0x000337ED
		public bool rendererProbes { get; set; } = true;

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x000355F6 File Offset: 0x000337F6
		// (set) Token: 0x06001F64 RID: 8036 RVA: 0x000355FE File Offset: 0x000337FE
		public bool particleSystemInstancing { get; set; } = true;

		// Token: 0x06001F65 RID: 8037 RVA: 0x00035608 File Offset: 0x00033808
		internal unsafe static MixedLightingMode FallbackMixedLightingMode()
		{
			MixedLightingMode mixedLightingMode;
			SupportedRenderingFeatures.FallbackMixedLightingModeByRef(new IntPtr((void*)(&mixedLightingMode)));
			return mixedLightingMode;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0003562C File Offset: 0x0003382C
		[RequiredByNativeCode]
		internal unsafe static void FallbackMixedLightingModeByRef(IntPtr fallbackModePtr)
		{
			MixedLightingMode* ptr = (MixedLightingMode*)(void*)fallbackModePtr;
			bool flag = SupportedRenderingFeatures.active.defaultMixedLightingModes != SupportedRenderingFeatures.LightmapMixedBakeModes.None && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.active.defaultMixedLightingModes) == SupportedRenderingFeatures.active.defaultMixedLightingModes;
			if (flag)
			{
				SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes = SupportedRenderingFeatures.active.defaultMixedLightingModes;
				if (defaultMixedLightingModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive)
				{
					if (defaultMixedLightingModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask)
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
					else
					{
						*ptr = MixedLightingMode.Shadowmask;
					}
				}
				else
				{
					*ptr = MixedLightingMode.Subtractive;
				}
			}
			else
			{
				bool flag2 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Shadowmask);
				if (flag2)
				{
					*ptr = MixedLightingMode.Shadowmask;
				}
				else
				{
					bool flag3 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Subtractive);
					if (flag3)
					{
						*ptr = MixedLightingMode.Subtractive;
					}
					else
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
				}
			}
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000356C4 File Offset: 0x000338C4
		internal unsafe static bool IsMixedLightingModeSupported(MixedLightingMode mixedMode)
		{
			bool flag;
			SupportedRenderingFeatures.IsMixedLightingModeSupportedByRef(mixedMode, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000356E8 File Offset: 0x000338E8
		[RequiredByNativeCode]
		internal unsafe static void IsMixedLightingModeSupportedByRef(MixedLightingMode mixedMode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			bool flag = !SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Mixed);
			if (flag)
			{
				*ptr = false;
			}
			else
			{
				*ptr = (mixedMode == MixedLightingMode.IndirectOnly && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) == SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) || (mixedMode == MixedLightingMode.Subtractive && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) == SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) || (mixedMode == MixedLightingMode.Shadowmask && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask) == SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask);
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00035750 File Offset: 0x00033950
		internal unsafe static bool IsLightmapBakeTypeSupported(LightmapBakeType bakeType)
		{
			bool flag;
			SupportedRenderingFeatures.IsLightmapBakeTypeSupportedByRef(bakeType, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00035774 File Offset: 0x00033974
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapBakeTypeSupportedByRef(LightmapBakeType bakeType, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			bool flag = bakeType == LightmapBakeType.Mixed;
			if (flag)
			{
				bool flag2 = SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Baked);
				bool flag3 = !flag2 || SupportedRenderingFeatures.active.mixedLightingModes == SupportedRenderingFeatures.LightmapMixedBakeModes.None;
				if (flag3)
				{
					*ptr = false;
					return;
				}
			}
			*ptr = (SupportedRenderingFeatures.active.lightmapBakeTypes & bakeType) == bakeType;
			bool flag4 = bakeType == LightmapBakeType.Realtime && !SupportedRenderingFeatures.active.enlighten;
			if (flag4)
			{
				*ptr = false;
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x000357E8 File Offset: 0x000339E8
		internal unsafe static bool IsLightmapsModeSupported(LightmapsMode mode)
		{
			bool flag;
			SupportedRenderingFeatures.IsLightmapsModeSupportedByRef(mode, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0003580C File Offset: 0x00033A0C
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapsModeSupportedByRef(LightmapsMode mode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = (SupportedRenderingFeatures.active.lightmapsModes & mode) == mode;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x00035834 File Offset: 0x00033A34
		internal unsafe static bool IsLightmapperSupported(int lightmapper)
		{
			bool flag;
			SupportedRenderingFeatures.IsLightmapperSupportedByRef(lightmapper, new IntPtr((void*)(&flag)));
			return flag;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00035858 File Offset: 0x00033A58
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapperSupportedByRef(int lightmapper, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = ((lightmapper == 0 && !SupportedRenderingFeatures.active.enlighten) ? false : true);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00035884 File Offset: 0x00033A84
		[RequiredByNativeCode]
		internal unsafe static void IsUIOverlayRenderedBySRP(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = SupportedRenderingFeatures.active.rendersUIOverlay;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000358A8 File Offset: 0x00033AA8
		internal unsafe static int FallbackLightmapper()
		{
			int num;
			SupportedRenderingFeatures.FallbackLightmapperByRef(new IntPtr((void*)(&num)));
			return num;
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000358CC File Offset: 0x00033ACC
		[RequiredByNativeCode]
		internal unsafe static void FallbackLightmapperByRef(IntPtr lightmapperPtr)
		{
			int* ptr = (int*)(void*)lightmapperPtr;
			*ptr = 1;
		}

		// Token: 0x04000B24 RID: 2852
		private static SupportedRenderingFeatures s_Active = new SupportedRenderingFeatures();

		// Token: 0x02000383 RID: 899
		[Flags]
		public enum ReflectionProbeModes
		{
			// Token: 0x04000B3B RID: 2875
			None = 0,
			// Token: 0x04000B3C RID: 2876
			Rotation = 1
		}

		// Token: 0x02000384 RID: 900
		[Flags]
		public enum LightmapMixedBakeModes
		{
			// Token: 0x04000B3E RID: 2878
			None = 0,
			// Token: 0x04000B3F RID: 2879
			IndirectOnly = 1,
			// Token: 0x04000B40 RID: 2880
			Subtractive = 2,
			// Token: 0x04000B41 RID: 2881
			Shadowmask = 4
		}
	}
}

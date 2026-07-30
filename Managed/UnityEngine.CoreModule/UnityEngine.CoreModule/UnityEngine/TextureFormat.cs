using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000127 RID: 295
	public enum TextureFormat
	{
		// Token: 0x0400035D RID: 861
		Alpha8 = 1,
		// Token: 0x0400035E RID: 862
		ARGB4444,
		// Token: 0x0400035F RID: 863
		RGB24,
		// Token: 0x04000360 RID: 864
		RGBA32,
		// Token: 0x04000361 RID: 865
		ARGB32,
		// Token: 0x04000362 RID: 866
		RGB565 = 7,
		// Token: 0x04000363 RID: 867
		R16 = 9,
		// Token: 0x04000364 RID: 868
		DXT1,
		// Token: 0x04000365 RID: 869
		DXT5 = 12,
		// Token: 0x04000366 RID: 870
		RGBA4444,
		// Token: 0x04000367 RID: 871
		BGRA32,
		// Token: 0x04000368 RID: 872
		RHalf,
		// Token: 0x04000369 RID: 873
		RGHalf,
		// Token: 0x0400036A RID: 874
		RGBAHalf,
		// Token: 0x0400036B RID: 875
		RFloat,
		// Token: 0x0400036C RID: 876
		RGFloat,
		// Token: 0x0400036D RID: 877
		RGBAFloat,
		// Token: 0x0400036E RID: 878
		YUY2,
		// Token: 0x0400036F RID: 879
		RGB9e5Float,
		// Token: 0x04000370 RID: 880
		BC4 = 26,
		// Token: 0x04000371 RID: 881
		BC5,
		// Token: 0x04000372 RID: 882
		BC6H = 24,
		// Token: 0x04000373 RID: 883
		BC7,
		// Token: 0x04000374 RID: 884
		DXT1Crunched = 28,
		// Token: 0x04000375 RID: 885
		DXT5Crunched,
		// Token: 0x04000376 RID: 886
		PVRTC_RGB2,
		// Token: 0x04000377 RID: 887
		PVRTC_RGBA2,
		// Token: 0x04000378 RID: 888
		PVRTC_RGB4,
		// Token: 0x04000379 RID: 889
		PVRTC_RGBA4,
		// Token: 0x0400037A RID: 890
		ETC_RGB4,
		// Token: 0x0400037B RID: 891
		EAC_R = 41,
		// Token: 0x0400037C RID: 892
		EAC_R_SIGNED,
		// Token: 0x0400037D RID: 893
		EAC_RG,
		// Token: 0x0400037E RID: 894
		EAC_RG_SIGNED,
		// Token: 0x0400037F RID: 895
		ETC2_RGB,
		// Token: 0x04000380 RID: 896
		ETC2_RGBA1,
		// Token: 0x04000381 RID: 897
		ETC2_RGBA8,
		// Token: 0x04000382 RID: 898
		ASTC_4x4,
		// Token: 0x04000383 RID: 899
		ASTC_5x5,
		// Token: 0x04000384 RID: 900
		ASTC_6x6,
		// Token: 0x04000385 RID: 901
		ASTC_8x8,
		// Token: 0x04000386 RID: 902
		ASTC_10x10,
		// Token: 0x04000387 RID: 903
		ASTC_12x12,
		// Token: 0x04000388 RID: 904
		[Obsolete("Nintendo 3DS is no longer supported.")]
		ETC_RGB4_3DS = 60,
		// Token: 0x04000389 RID: 905
		[Obsolete("Nintendo 3DS is no longer supported.")]
		ETC_RGBA8_3DS,
		// Token: 0x0400038A RID: 906
		RG16,
		// Token: 0x0400038B RID: 907
		R8,
		// Token: 0x0400038C RID: 908
		ETC_RGB4Crunched,
		// Token: 0x0400038D RID: 909
		ETC2_RGBA8Crunched,
		// Token: 0x0400038E RID: 910
		ASTC_HDR_4x4,
		// Token: 0x0400038F RID: 911
		ASTC_HDR_5x5,
		// Token: 0x04000390 RID: 912
		ASTC_HDR_6x6,
		// Token: 0x04000391 RID: 913
		ASTC_HDR_8x8,
		// Token: 0x04000392 RID: 914
		ASTC_HDR_10x10,
		// Token: 0x04000393 RID: 915
		ASTC_HDR_12x12,
		// Token: 0x04000394 RID: 916
		[EditorBrowsable(1)]
		[Obsolete("Enum member TextureFormat.ASTC_RGB_4x4 has been deprecated. Use ASTC_4x4 instead (UnityUpgradable) -> ASTC_4x4")]
		ASTC_RGB_4x4 = 48,
		// Token: 0x04000395 RID: 917
		[Obsolete("Enum member TextureFormat.ASTC_RGB_5x5 has been deprecated. Use ASTC_5x5 instead (UnityUpgradable) -> ASTC_5x5")]
		[EditorBrowsable(1)]
		ASTC_RGB_5x5,
		// Token: 0x04000396 RID: 918
		[Obsolete("Enum member TextureFormat.ASTC_RGB_6x6 has been deprecated. Use ASTC_6x6 instead (UnityUpgradable) -> ASTC_6x6")]
		[EditorBrowsable(1)]
		ASTC_RGB_6x6,
		// Token: 0x04000397 RID: 919
		[EditorBrowsable(1)]
		[Obsolete("Enum member TextureFormat.ASTC_RGB_8x8 has been deprecated. Use ASTC_8x8 instead (UnityUpgradable) -> ASTC_8x8")]
		ASTC_RGB_8x8,
		// Token: 0x04000398 RID: 920
		[EditorBrowsable(1)]
		[Obsolete("Enum member TextureFormat.ASTC_RGB_10x10 has been deprecated. Use ASTC_10x10 instead (UnityUpgradable) -> ASTC_10x10")]
		ASTC_RGB_10x10,
		// Token: 0x04000399 RID: 921
		[EditorBrowsable(1)]
		[Obsolete("Enum member TextureFormat.ASTC_RGB_12x12 has been deprecated. Use ASTC_12x12 instead (UnityUpgradable) -> ASTC_12x12")]
		ASTC_RGB_12x12,
		// Token: 0x0400039A RID: 922
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_4x4 has been deprecated. Use ASTC_4x4 instead (UnityUpgradable) -> ASTC_4x4")]
		[EditorBrowsable(1)]
		ASTC_RGBA_4x4,
		// Token: 0x0400039B RID: 923
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_5x5 has been deprecated. Use ASTC_5x5 instead (UnityUpgradable) -> ASTC_5x5")]
		[EditorBrowsable(1)]
		ASTC_RGBA_5x5,
		// Token: 0x0400039C RID: 924
		[EditorBrowsable(1)]
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_6x6 has been deprecated. Use ASTC_6x6 instead (UnityUpgradable) -> ASTC_6x6")]
		ASTC_RGBA_6x6,
		// Token: 0x0400039D RID: 925
		[EditorBrowsable(1)]
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_8x8 has been deprecated. Use ASTC_8x8 instead (UnityUpgradable) -> ASTC_8x8")]
		ASTC_RGBA_8x8,
		// Token: 0x0400039E RID: 926
		[EditorBrowsable(1)]
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_10x10 has been deprecated. Use ASTC_10x10 instead (UnityUpgradable) -> ASTC_10x10")]
		ASTC_RGBA_10x10,
		// Token: 0x0400039F RID: 927
		[Obsolete("Enum member TextureFormat.ASTC_RGBA_12x12 has been deprecated. Use ASTC_12x12 instead (UnityUpgradable) -> ASTC_12x12")]
		[EditorBrowsable(1)]
		ASTC_RGBA_12x12
	}
}

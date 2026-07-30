using System;
using System.ComponentModel;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003D8 RID: 984
	public enum GraphicsFormat
	{
		// Token: 0x04000C68 RID: 3176
		None,
		// Token: 0x04000C69 RID: 3177
		R8_SRGB,
		// Token: 0x04000C6A RID: 3178
		R8G8_SRGB,
		// Token: 0x04000C6B RID: 3179
		R8G8B8_SRGB,
		// Token: 0x04000C6C RID: 3180
		R8G8B8A8_SRGB,
		// Token: 0x04000C6D RID: 3181
		R8_UNorm,
		// Token: 0x04000C6E RID: 3182
		R8G8_UNorm,
		// Token: 0x04000C6F RID: 3183
		R8G8B8_UNorm,
		// Token: 0x04000C70 RID: 3184
		R8G8B8A8_UNorm,
		// Token: 0x04000C71 RID: 3185
		R8_SNorm,
		// Token: 0x04000C72 RID: 3186
		R8G8_SNorm,
		// Token: 0x04000C73 RID: 3187
		R8G8B8_SNorm,
		// Token: 0x04000C74 RID: 3188
		R8G8B8A8_SNorm,
		// Token: 0x04000C75 RID: 3189
		R8_UInt,
		// Token: 0x04000C76 RID: 3190
		R8G8_UInt,
		// Token: 0x04000C77 RID: 3191
		R8G8B8_UInt,
		// Token: 0x04000C78 RID: 3192
		R8G8B8A8_UInt,
		// Token: 0x04000C79 RID: 3193
		R8_SInt,
		// Token: 0x04000C7A RID: 3194
		R8G8_SInt,
		// Token: 0x04000C7B RID: 3195
		R8G8B8_SInt,
		// Token: 0x04000C7C RID: 3196
		R8G8B8A8_SInt,
		// Token: 0x04000C7D RID: 3197
		R16_UNorm,
		// Token: 0x04000C7E RID: 3198
		R16G16_UNorm,
		// Token: 0x04000C7F RID: 3199
		R16G16B16_UNorm,
		// Token: 0x04000C80 RID: 3200
		R16G16B16A16_UNorm,
		// Token: 0x04000C81 RID: 3201
		R16_SNorm,
		// Token: 0x04000C82 RID: 3202
		R16G16_SNorm,
		// Token: 0x04000C83 RID: 3203
		R16G16B16_SNorm,
		// Token: 0x04000C84 RID: 3204
		R16G16B16A16_SNorm,
		// Token: 0x04000C85 RID: 3205
		R16_UInt,
		// Token: 0x04000C86 RID: 3206
		R16G16_UInt,
		// Token: 0x04000C87 RID: 3207
		R16G16B16_UInt,
		// Token: 0x04000C88 RID: 3208
		R16G16B16A16_UInt,
		// Token: 0x04000C89 RID: 3209
		R16_SInt,
		// Token: 0x04000C8A RID: 3210
		R16G16_SInt,
		// Token: 0x04000C8B RID: 3211
		R16G16B16_SInt,
		// Token: 0x04000C8C RID: 3212
		R16G16B16A16_SInt,
		// Token: 0x04000C8D RID: 3213
		R32_UInt,
		// Token: 0x04000C8E RID: 3214
		R32G32_UInt,
		// Token: 0x04000C8F RID: 3215
		R32G32B32_UInt,
		// Token: 0x04000C90 RID: 3216
		R32G32B32A32_UInt,
		// Token: 0x04000C91 RID: 3217
		R32_SInt,
		// Token: 0x04000C92 RID: 3218
		R32G32_SInt,
		// Token: 0x04000C93 RID: 3219
		R32G32B32_SInt,
		// Token: 0x04000C94 RID: 3220
		R32G32B32A32_SInt,
		// Token: 0x04000C95 RID: 3221
		R16_SFloat,
		// Token: 0x04000C96 RID: 3222
		R16G16_SFloat,
		// Token: 0x04000C97 RID: 3223
		R16G16B16_SFloat,
		// Token: 0x04000C98 RID: 3224
		R16G16B16A16_SFloat,
		// Token: 0x04000C99 RID: 3225
		R32_SFloat,
		// Token: 0x04000C9A RID: 3226
		R32G32_SFloat,
		// Token: 0x04000C9B RID: 3227
		R32G32B32_SFloat,
		// Token: 0x04000C9C RID: 3228
		R32G32B32A32_SFloat,
		// Token: 0x04000C9D RID: 3229
		B8G8R8_SRGB = 56,
		// Token: 0x04000C9E RID: 3230
		B8G8R8A8_SRGB,
		// Token: 0x04000C9F RID: 3231
		B8G8R8_UNorm,
		// Token: 0x04000CA0 RID: 3232
		B8G8R8A8_UNorm,
		// Token: 0x04000CA1 RID: 3233
		B8G8R8_SNorm,
		// Token: 0x04000CA2 RID: 3234
		B8G8R8A8_SNorm,
		// Token: 0x04000CA3 RID: 3235
		B8G8R8_UInt,
		// Token: 0x04000CA4 RID: 3236
		B8G8R8A8_UInt,
		// Token: 0x04000CA5 RID: 3237
		B8G8R8_SInt,
		// Token: 0x04000CA6 RID: 3238
		B8G8R8A8_SInt,
		// Token: 0x04000CA7 RID: 3239
		R4G4B4A4_UNormPack16,
		// Token: 0x04000CA8 RID: 3240
		B4G4R4A4_UNormPack16,
		// Token: 0x04000CA9 RID: 3241
		R5G6B5_UNormPack16,
		// Token: 0x04000CAA RID: 3242
		B5G6R5_UNormPack16,
		// Token: 0x04000CAB RID: 3243
		R5G5B5A1_UNormPack16,
		// Token: 0x04000CAC RID: 3244
		B5G5R5A1_UNormPack16,
		// Token: 0x04000CAD RID: 3245
		A1R5G5B5_UNormPack16,
		// Token: 0x04000CAE RID: 3246
		E5B9G9R9_UFloatPack32,
		// Token: 0x04000CAF RID: 3247
		B10G11R11_UFloatPack32,
		// Token: 0x04000CB0 RID: 3248
		A2B10G10R10_UNormPack32,
		// Token: 0x04000CB1 RID: 3249
		A2B10G10R10_UIntPack32,
		// Token: 0x04000CB2 RID: 3250
		A2B10G10R10_SIntPack32,
		// Token: 0x04000CB3 RID: 3251
		A2R10G10B10_UNormPack32,
		// Token: 0x04000CB4 RID: 3252
		A2R10G10B10_UIntPack32,
		// Token: 0x04000CB5 RID: 3253
		A2R10G10B10_SIntPack32,
		// Token: 0x04000CB6 RID: 3254
		A2R10G10B10_XRSRGBPack32,
		// Token: 0x04000CB7 RID: 3255
		A2R10G10B10_XRUNormPack32,
		// Token: 0x04000CB8 RID: 3256
		R10G10B10_XRSRGBPack32,
		// Token: 0x04000CB9 RID: 3257
		R10G10B10_XRUNormPack32,
		// Token: 0x04000CBA RID: 3258
		A10R10G10B10_XRSRGBPack32,
		// Token: 0x04000CBB RID: 3259
		A10R10G10B10_XRUNormPack32,
		// Token: 0x04000CBC RID: 3260
		[EditorBrowsable(1)]
		[Obsolete("Enum member GraphicsFormat.RGB_DXT1_SRGB has been deprecated. Use GraphicsFormat.RGBA_DXT1_SRGB instead (UnityUpgradable) -> RGBA_DXT1_SRGB", true)]
		RGB_DXT1_SRGB = 96,
		// Token: 0x04000CBD RID: 3261
		RGBA_DXT1_SRGB = 96,
		// Token: 0x04000CBE RID: 3262
		[EditorBrowsable(1)]
		[Obsolete("Enum member GraphicsFormat.RGB_DXT1_UNorm has been deprecated. Use GraphicsFormat.RGBA_DXT1_UNorm instead (UnityUpgradable) -> RGBA_DXT1_UNorm", true)]
		RGB_DXT1_UNorm,
		// Token: 0x04000CBF RID: 3263
		RGBA_DXT1_UNorm = 97,
		// Token: 0x04000CC0 RID: 3264
		RGBA_DXT3_SRGB,
		// Token: 0x04000CC1 RID: 3265
		RGBA_DXT3_UNorm,
		// Token: 0x04000CC2 RID: 3266
		RGBA_DXT5_SRGB,
		// Token: 0x04000CC3 RID: 3267
		RGBA_DXT5_UNorm,
		// Token: 0x04000CC4 RID: 3268
		R_BC4_UNorm,
		// Token: 0x04000CC5 RID: 3269
		R_BC4_SNorm,
		// Token: 0x04000CC6 RID: 3270
		RG_BC5_UNorm,
		// Token: 0x04000CC7 RID: 3271
		RG_BC5_SNorm,
		// Token: 0x04000CC8 RID: 3272
		RGB_BC6H_UFloat,
		// Token: 0x04000CC9 RID: 3273
		RGB_BC6H_SFloat,
		// Token: 0x04000CCA RID: 3274
		RGBA_BC7_SRGB,
		// Token: 0x04000CCB RID: 3275
		RGBA_BC7_UNorm,
		// Token: 0x04000CCC RID: 3276
		RGB_PVRTC_2Bpp_SRGB,
		// Token: 0x04000CCD RID: 3277
		RGB_PVRTC_2Bpp_UNorm,
		// Token: 0x04000CCE RID: 3278
		RGB_PVRTC_4Bpp_SRGB,
		// Token: 0x04000CCF RID: 3279
		RGB_PVRTC_4Bpp_UNorm,
		// Token: 0x04000CD0 RID: 3280
		RGBA_PVRTC_2Bpp_SRGB,
		// Token: 0x04000CD1 RID: 3281
		RGBA_PVRTC_2Bpp_UNorm,
		// Token: 0x04000CD2 RID: 3282
		RGBA_PVRTC_4Bpp_SRGB,
		// Token: 0x04000CD3 RID: 3283
		RGBA_PVRTC_4Bpp_UNorm,
		// Token: 0x04000CD4 RID: 3284
		RGB_ETC_UNorm,
		// Token: 0x04000CD5 RID: 3285
		RGB_ETC2_SRGB,
		// Token: 0x04000CD6 RID: 3286
		RGB_ETC2_UNorm,
		// Token: 0x04000CD7 RID: 3287
		RGB_A1_ETC2_SRGB,
		// Token: 0x04000CD8 RID: 3288
		RGB_A1_ETC2_UNorm,
		// Token: 0x04000CD9 RID: 3289
		RGBA_ETC2_SRGB,
		// Token: 0x04000CDA RID: 3290
		RGBA_ETC2_UNorm,
		// Token: 0x04000CDB RID: 3291
		R_EAC_UNorm,
		// Token: 0x04000CDC RID: 3292
		R_EAC_SNorm,
		// Token: 0x04000CDD RID: 3293
		RG_EAC_UNorm,
		// Token: 0x04000CDE RID: 3294
		RG_EAC_SNorm,
		// Token: 0x04000CDF RID: 3295
		RGBA_ASTC4X4_SRGB,
		// Token: 0x04000CE0 RID: 3296
		RGBA_ASTC4X4_UNorm,
		// Token: 0x04000CE1 RID: 3297
		RGBA_ASTC5X5_SRGB,
		// Token: 0x04000CE2 RID: 3298
		RGBA_ASTC5X5_UNorm,
		// Token: 0x04000CE3 RID: 3299
		RGBA_ASTC6X6_SRGB,
		// Token: 0x04000CE4 RID: 3300
		RGBA_ASTC6X6_UNorm,
		// Token: 0x04000CE5 RID: 3301
		RGBA_ASTC8X8_SRGB,
		// Token: 0x04000CE6 RID: 3302
		RGBA_ASTC8X8_UNorm,
		// Token: 0x04000CE7 RID: 3303
		RGBA_ASTC10X10_SRGB,
		// Token: 0x04000CE8 RID: 3304
		RGBA_ASTC10X10_UNorm,
		// Token: 0x04000CE9 RID: 3305
		RGBA_ASTC12X12_SRGB,
		// Token: 0x04000CEA RID: 3306
		RGBA_ASTC12X12_UNorm
	}
}

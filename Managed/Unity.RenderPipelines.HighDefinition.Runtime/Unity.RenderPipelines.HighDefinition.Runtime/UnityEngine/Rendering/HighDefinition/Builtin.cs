using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000AB RID: 171
	internal class Builtin
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x00034705 File Offset: 0x00032905
		public static GraphicsFormat GetLightingBufferFormat()
		{
			return GraphicsFormat.B10G11R11_UFloatPack32;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00034709 File Offset: 0x00032909
		public static GraphicsFormat GetShadowMaskBufferFormat()
		{
			return GraphicsFormat.R8G8B8A8_UNorm;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0003470C File Offset: 0x0003290C
		public static GraphicsFormat GetMotionVectorFormat()
		{
			return GraphicsFormat.R16G16_SFloat;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00034710 File Offset: 0x00032910
		public static GraphicsFormat GetDistortionBufferFormat()
		{
			return GraphicsFormat.R16G16B16A16_SFloat;
		}

		// Token: 0x02000225 RID: 549
		[GenerateHLSL(PackingRules.Exact, false, false, true, 100, false, false)]
		public struct BuiltinData
		{
			// Token: 0x04001411 RID: 5137
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Alpha)]
			[SurfaceDataAttributes("Opacity", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float opacity;

			// Token: 0x04001412 RID: 5138
			[SurfaceDataAttributes("Bake Diffuse Lighting", false, true, FieldPrecision.Real)]
			public Vector3 bakeDiffuseLighting;

			// Token: 0x04001413 RID: 5139
			[SurfaceDataAttributes("Back Bake Diffuse Lighting", false, true, FieldPrecision.Real)]
			public Vector3 backBakeDiffuseLighting;

			// Token: 0x04001414 RID: 5140
			[SurfaceDataAttributes("Shadowmask 0", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float shadowMask0;

			// Token: 0x04001415 RID: 5141
			[SurfaceDataAttributes("Shadowmask 1", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float shadowMask1;

			// Token: 0x04001416 RID: 5142
			[SurfaceDataAttributes("Shadowmask 2", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float shadowMask2;

			// Token: 0x04001417 RID: 5143
			[SurfaceDataAttributes("Shadowmask 3", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float shadowMask3;

			// Token: 0x04001418 RID: 5144
			[SurfaceDataAttributes("Emissive Color", false, false, FieldPrecision.Real)]
			public Vector3 emissiveColor;

			// Token: 0x04001419 RID: 5145
			[SurfaceDataAttributes("MotionVector", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public Vector2 motionVector;

			// Token: 0x0400141A RID: 5146
			[SurfaceDataAttributes("Distortion", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public Vector2 distortion;

			// Token: 0x0400141B RID: 5147
			[SurfaceDataAttributes("Distortion Blur", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float distortionBlur;

			// Token: 0x0400141C RID: 5148
			[SurfaceDataAttributes("RenderingLayers", false, false, FieldPrecision.Default)]
			public uint renderingLayers;

			// Token: 0x0400141D RID: 5149
			[SurfaceDataAttributes("Depth Offset", false, false, FieldPrecision.Default)]
			public float depthOffset;
		}

		// Token: 0x02000226 RID: 550
		[GenerateHLSL(PackingRules.Exact, false, false, false, 1, false, false)]
		public struct LightTransportData
		{
			// Token: 0x0400141E RID: 5150
			[SurfaceDataAttributes("", false, true, FieldPrecision.Real)]
			public Vector3 diffuseColor;

			// Token: 0x0400141F RID: 5151
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public Vector3 emissiveColor;
		}
	}
}

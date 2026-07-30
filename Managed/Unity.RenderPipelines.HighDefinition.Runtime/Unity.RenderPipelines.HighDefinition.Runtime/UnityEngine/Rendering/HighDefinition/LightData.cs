using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200005D RID: 93
	[GenerateHLSL(PackingRules.Exact, false, false, false, 1, false, false)]
	internal struct LightData
	{
		// Token: 0x040002CE RID: 718
		public Vector3 positionRWS;

		// Token: 0x040002CF RID: 719
		public uint lightLayers;

		// Token: 0x040002D0 RID: 720
		public float lightDimmer;

		// Token: 0x040002D1 RID: 721
		public float volumetricLightDimmer;

		// Token: 0x040002D2 RID: 722
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public float angleScale;

		// Token: 0x040002D3 RID: 723
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public float angleOffset;

		// Token: 0x040002D4 RID: 724
		public Vector3 forward;

		// Token: 0x040002D5 RID: 725
		public GPULightType lightType;

		// Token: 0x040002D6 RID: 726
		public Vector3 right;

		// Token: 0x040002D7 RID: 727
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public float range;

		// Token: 0x040002D8 RID: 728
		public Vector3 up;

		// Token: 0x040002D9 RID: 729
		public float rangeAttenuationScale;

		// Token: 0x040002DA RID: 730
		public Vector3 color;

		// Token: 0x040002DB RID: 731
		public float rangeAttenuationBias;

		// Token: 0x040002DC RID: 732
		public CookieMode cookieMode;

		// Token: 0x040002DD RID: 733
		public int cookieIndex;

		// Token: 0x040002DE RID: 734
		public int shadowIndex;

		// Token: 0x040002DF RID: 735
		public Vector4 cookieScaleOffset;

		// Token: 0x040002E0 RID: 736
		public int contactShadowMask;

		// Token: 0x040002E1 RID: 737
		public Vector3 shadowTint;

		// Token: 0x040002E2 RID: 738
		public float shadowDimmer;

		// Token: 0x040002E3 RID: 739
		public float volumetricShadowDimmer;

		// Token: 0x040002E4 RID: 740
		public int nonLightMappedOnly;

		// Token: 0x040002E5 RID: 741
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public float minRoughness;

		// Token: 0x040002E6 RID: 742
		public int screenSpaceShadowIndex;

		// Token: 0x040002E7 RID: 743
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public Vector4 shadowMaskSelector;

		// Token: 0x040002E8 RID: 744
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public Vector4 size;

		// Token: 0x040002E9 RID: 745
		public float diffuseDimmer;

		// Token: 0x040002EA RID: 746
		public float specularDimmer;

		// Token: 0x040002EB RID: 747
		public float isRayTracedContactShadow;

		// Token: 0x040002EC RID: 748
		public float penumbraTint;

		// Token: 0x040002ED RID: 749
		public Vector3 padding;

		// Token: 0x040002EE RID: 750
		public float boxLightSafeExtent;
	}
}

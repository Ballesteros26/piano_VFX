using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200005C RID: 92
	[GenerateHLSL(PackingRules.Exact, false, false, false, 1, false, false)]
	internal struct DirectionalLightData
	{
		// Token: 0x040002AE RID: 686
		public Vector3 positionRWS;

		// Token: 0x040002AF RID: 687
		public uint lightLayers;

		// Token: 0x040002B0 RID: 688
		public float lightDimmer;

		// Token: 0x040002B1 RID: 689
		public float volumetricLightDimmer;

		// Token: 0x040002B2 RID: 690
		public Vector3 forward;

		// Token: 0x040002B3 RID: 691
		public CookieMode cookieMode;

		// Token: 0x040002B4 RID: 692
		public Vector4 cookieScaleOffset;

		// Token: 0x040002B5 RID: 693
		public Vector3 right;

		// Token: 0x040002B6 RID: 694
		public int shadowIndex;

		// Token: 0x040002B7 RID: 695
		public Vector3 up;

		// Token: 0x040002B8 RID: 696
		public int contactShadowIndex;

		// Token: 0x040002B9 RID: 697
		public Vector3 color;

		// Token: 0x040002BA RID: 698
		public int contactShadowMask;

		// Token: 0x040002BB RID: 699
		public Vector3 shadowTint;

		// Token: 0x040002BC RID: 700
		public float shadowDimmer;

		// Token: 0x040002BD RID: 701
		public float volumetricShadowDimmer;

		// Token: 0x040002BE RID: 702
		public int nonLightMappedOnly;

		// Token: 0x040002BF RID: 703
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public float minRoughness;

		// Token: 0x040002C0 RID: 704
		public int screenSpaceShadowIndex;

		// Token: 0x040002C1 RID: 705
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public Vector4 shadowMaskSelector;

		// Token: 0x040002C2 RID: 706
		public float diffuseDimmer;

		// Token: 0x040002C3 RID: 707
		public float specularDimmer;

		// Token: 0x040002C4 RID: 708
		public float penumbraTint;

		// Token: 0x040002C5 RID: 709
		public float isRayTracedContactShadow;

		// Token: 0x040002C6 RID: 710
		public float distanceFromCamera;

		// Token: 0x040002C7 RID: 711
		public float angularDiameter;

		// Token: 0x040002C8 RID: 712
		public float flareFalloff;

		// Token: 0x040002C9 RID: 713
		public float __unused__;

		// Token: 0x040002CA RID: 714
		public Vector3 flareTint;

		// Token: 0x040002CB RID: 715
		public float flareSize;

		// Token: 0x040002CC RID: 716
		public Vector3 surfaceTint;

		// Token: 0x040002CD RID: 717
		public Vector4 surfaceTextureScaleOffset;
	}
}

using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000AD RID: 173
	internal class Decal
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x000348A2 File Offset: 0x00032AA2
		public static int GetMaterialDBufferCount()
		{
			return 4;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000348A5 File Offset: 0x00032AA5
		public static void GetMaterialDBufferDescription(out GraphicsFormat[] RTFormat)
		{
			RTFormat = Decal.m_RTFormat;
		}

		// Token: 0x040006B7 RID: 1719
		private static GraphicsFormat[] m_RTFormat = new GraphicsFormat[]
		{
			GraphicsFormat.R8G8B8A8_SRGB,
			GraphicsFormat.R8G8B8A8_UNorm,
			GraphicsFormat.R8G8B8A8_UNorm,
			GraphicsFormat.R8G8_UNorm
		};

		// Token: 0x02000227 RID: 551
		[GenerateHLSL(PackingRules.Exact, false, false, false, 1, false, false)]
		public struct DecalSurfaceData
		{
			// Token: 0x04001420 RID: 5152
			[SurfaceDataAttributes("Base Color", false, true, FieldPrecision.Default)]
			public Vector4 baseColor;

			// Token: 0x04001421 RID: 5153
			[SurfaceDataAttributes("Normal", true, false, FieldPrecision.Default)]
			public Vector4 normalWS;

			// Token: 0x04001422 RID: 5154
			[SurfaceDataAttributes("Mask", true, false, FieldPrecision.Default)]
			public Vector4 mask;

			// Token: 0x04001423 RID: 5155
			[SurfaceDataAttributes("Emissive", false, false, FieldPrecision.Default)]
			public Vector3 emissive;

			// Token: 0x04001424 RID: 5156
			[SurfaceDataAttributes("AOSBlend", true, false, FieldPrecision.Default)]
			public Vector2 MAOSBlend;

			// Token: 0x04001425 RID: 5157
			[SurfaceDataAttributes("HTileMask", false, false, FieldPrecision.Default)]
			public uint HTileMask;
		}

		// Token: 0x02000228 RID: 552
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum DBufferMaterial
		{
			// Token: 0x04001427 RID: 5159
			Count = 4
		}

		// Token: 0x02000229 RID: 553
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum DBufferHTileBit
		{
			// Token: 0x04001429 RID: 5161
			Diffuse = 1,
			// Token: 0x0400142A RID: 5162
			Normal,
			// Token: 0x0400142B RID: 5163
			Mask = 4
		}

		// Token: 0x0200022A RID: 554
		[Flags]
		public enum MaskBlendFlags
		{
			// Token: 0x0400142D RID: 5165
			Metal = 1,
			// Token: 0x0400142E RID: 5166
			AO = 2,
			// Token: 0x0400142F RID: 5167
			Smoothness = 4
		}
	}
}

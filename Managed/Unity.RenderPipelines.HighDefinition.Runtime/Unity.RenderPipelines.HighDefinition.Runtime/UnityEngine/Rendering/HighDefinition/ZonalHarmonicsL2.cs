using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200009D RID: 157
	internal struct ZonalHarmonicsL2
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x000330A4 File Offset: 0x000312A4
		public static ZonalHarmonicsL2 GetHenyeyGreensteinPhaseFunction(float anisotropy)
		{
			ZonalHarmonicsL2 zonalHarmonicsL = new ZonalHarmonicsL2
			{
				coeffs = new float[3]
			};
			zonalHarmonicsL.coeffs[0] = 0.5f * Mathf.Sqrt(0.31830987f);
			zonalHarmonicsL.coeffs[1] = 0.5f * Mathf.Sqrt(0.95492965f) * anisotropy;
			zonalHarmonicsL.coeffs[2] = 0.5f * Mathf.Sqrt(1.5915494f) * anisotropy * anisotropy;
			return zonalHarmonicsL;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00033118 File Offset: 0x00031318
		public static void GetCornetteShanksPhaseFunction(ZonalHarmonicsL2 zh, float anisotropy)
		{
			zh.coeffs[0] = 0.282095f;
			zh.coeffs[1] = 0.293162f * anisotropy * (4f + anisotropy * anisotropy) / (2f + anisotropy * anisotropy);
			zh.coeffs[2] = (0.126157f + 1.44179f * (anisotropy * anisotropy) + 0.324403f * (anisotropy * anisotropy) * (anisotropy * anisotropy)) / (2f + anisotropy * anisotropy);
		}

		// Token: 0x0400065D RID: 1629
		public float[] coeffs;
	}
}

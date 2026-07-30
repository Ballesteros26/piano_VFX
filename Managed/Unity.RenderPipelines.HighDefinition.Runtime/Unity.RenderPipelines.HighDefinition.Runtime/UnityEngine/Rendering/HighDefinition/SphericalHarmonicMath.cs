using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200009E RID: 158
	internal class SphericalHarmonicMath
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x00033188 File Offset: 0x00031388
		public static SphericalHarmonicsL2 Convolve(SphericalHarmonicsL2 sh, ZonalHarmonicsL2 zh)
		{
			for (int i = 0; i <= 2; i++)
			{
				float num = Mathf.Sqrt(12.566371f / (float)(2 * i + 1));
				float num2 = zh.coeffs[i];
				float num3 = num * num2;
				for (int j = -i; j <= i; j++)
				{
					int num4 = i * (i + 1) + j;
					for (int k = 0; k < 3; k++)
					{
						ref SphericalHarmonicsL2 ptr = ref sh;
						int num5 = k;
						int num6 = num4;
						ptr[num5, num6] *= num3;
					}
				}
			}
			return sh;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0003320C File Offset: 0x0003140C
		public static SphericalHarmonicsL2 UndoCosineRescaling(SphericalHarmonicsL2 sh)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					ref SphericalHarmonicsL2 ptr = ref sh;
					int num = i;
					int num2 = j;
					ptr[num, num2] *= SphericalHarmonicMath.invNormConsts[j];
				}
			}
			return sh;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00033258 File Offset: 0x00031458
		public static SphericalHarmonicsL2 PremultiplyCoefficients(SphericalHarmonicsL2 sh)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					ref SphericalHarmonicsL2 ptr = ref sh;
					int num = i;
					int num2 = j;
					ptr[num, num2] *= SphericalHarmonicMath.ks[j];
				}
			}
			return sh;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000332A4 File Offset: 0x000314A4
		public static SphericalHarmonicsL2 RescaleCoefficients(SphericalHarmonicsL2 sh, float scalar)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					ref SphericalHarmonicsL2 ptr = ref sh;
					int num = i;
					int num2 = j;
					ptr[num, num2] *= scalar;
				}
			}
			return sh;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000332E8 File Offset: 0x000314E8
		public static void PackCoefficients(Vector4[] packedCoeffs, SphericalHarmonicsL2 sh)
		{
			for (int i = 0; i < 3; i++)
			{
				packedCoeffs[i].Set(sh[i, 3], sh[i, 1], sh[i, 2], sh[i, 0] - sh[i, 6]);
			}
			for (int j = 0; j < 3; j++)
			{
				packedCoeffs[3 + j].Set(sh[j, 4], sh[j, 5], sh[j, 6] * 3f, sh[j, 7]);
			}
			packedCoeffs[6].Set(sh[0, 8], sh[1, 8], sh[2, 8], 1f);
		}

		// Token: 0x0400065E RID: 1630
		private const float c0 = 0.2820948f;

		// Token: 0x0400065F RID: 1631
		private const float c1 = 0.325735f;

		// Token: 0x04000660 RID: 1632
		private const float c2 = 0.27313712f;

		// Token: 0x04000661 RID: 1633
		private const float c3 = 0.07884789f;

		// Token: 0x04000662 RID: 1634
		private const float c4 = 0.13656856f;

		// Token: 0x04000663 RID: 1635
		private static float[] invNormConsts = new float[] { 3.5449076f, -3.0699801f, 3.0699801f, -3.0699801f, 3.6611648f, -3.6611648f, 12.682647f, -3.6611648f, 7.3223295f };

		// Token: 0x04000664 RID: 1636
		private const float k0 = 0.2820948f;

		// Token: 0x04000665 RID: 1637
		private const float k1 = 0.48860252f;

		// Token: 0x04000666 RID: 1638
		private const float k2 = 1.0925485f;

		// Token: 0x04000667 RID: 1639
		private const float k3 = 0.31539157f;

		// Token: 0x04000668 RID: 1640
		private const float k4 = 0.54627424f;

		// Token: 0x04000669 RID: 1641
		private static float[] ks = new float[] { 0.2820948f, -0.48860252f, 0.48860252f, -0.48860252f, 1.0925485f, -1.0925485f, 0.31539157f, -1.0925485f, 0.54627424f };
	}
}

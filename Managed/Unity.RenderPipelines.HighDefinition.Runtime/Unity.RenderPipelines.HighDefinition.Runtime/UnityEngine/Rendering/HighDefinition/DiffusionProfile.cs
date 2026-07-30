using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000B5 RID: 181
	[Serializable]
	internal class DiffusionProfile : IEquatable<DiffusionProfile>
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00035E8E File Offset: 0x0003408E
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x00035E96 File Offset: 0x00034096
		public Vector3 shapeParam { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00035E9F File Offset: 0x0003409F
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00035EA7 File Offset: 0x000340A7
		public float maxRadius { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00035EB0 File Offset: 0x000340B0
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00035EB8 File Offset: 0x000340B8
		public Vector2[] filterKernelNearField { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00035EC1 File Offset: 0x000340C1
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x00035EC9 File Offset: 0x000340C9
		public Vector2[] filterKernelFarField { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00035ED2 File Offset: 0x000340D2
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x00035EDA File Offset: 0x000340DA
		public Vector4 halfRcpWeightedVariances { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00035EE3 File Offset: 0x000340E3
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00035EEB File Offset: 0x000340EB
		public Vector4[] filterKernelBasic { get; private set; }

		// Token: 0x060006C9 RID: 1737 RVA: 0x00035EF4 File Offset: 0x000340F4
		public DiffusionProfile(bool dontUseDefaultConstructor)
		{
			this.scatteringDistance = Color.grey;
			this.transmissionTint = Color.white;
			this.texturingMode = DiffusionProfile.TexturingMode.PreAndPostScatter;
			this.transmissionMode = DiffusionProfile.TransmissionMode.ThinObject;
			this.thicknessRemap = new Vector2(0f, 5f);
			this.worldScale = 1f;
			this.ior = 1.4f;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00035F58 File Offset: 0x00034158
		internal void Validate()
		{
			this.thicknessRemap.y = Mathf.Max(this.thicknessRemap.y, 0f);
			this.thicknessRemap.x = Mathf.Clamp(this.thicknessRemap.x, 0f, this.thicknessRemap.y);
			this.worldScale = Mathf.Max(this.worldScale, 0.001f);
			this.ior = Mathf.Clamp(this.ior, 1f, 2f);
			this.UpdateKernel();
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00035FE8 File Offset: 0x000341E8
		private void UpdateKernel()
		{
			if (this.filterKernelNearField == null || this.filterKernelNearField.Length != 55)
			{
				this.filterKernelNearField = new Vector2[55];
			}
			if (this.filterKernelFarField == null || this.filterKernelFarField.Length != 21)
			{
				this.filterKernelFarField = new Vector2[21];
			}
			this.shapeParam = new Vector3(1f / this.scatteringDistance.r, 1f / this.scatteringDistance.g, 1f / this.scatteringDistance.b);
			this.shapeParam = Vector3.Min(this.shapeParam, float.MaxValue * Vector3.one);
			float num = Mathf.Min(new float[]
			{
				this.shapeParam.x,
				this.shapeParam.y,
				this.shapeParam.z
			});
			int i = 0;
			int num2 = 55;
			while (i < num2)
			{
				float num3 = DiffusionProfile.DisneyProfileCdfInverse(((float)i + 0.5f) * (1f / (float)num2), num);
				this.filterKernelNearField[i].x = num3;
				this.filterKernelNearField[i].y = 1f / DiffusionProfile.DisneyProfilePdf(num3, num);
				i++;
			}
			int j = 0;
			int num4 = 21;
			while (j < num4)
			{
				float num5 = DiffusionProfile.DisneyProfileCdfInverse(((float)j + 0.5f) * (1f / (float)num4), num);
				this.filterKernelFarField[j].x = num5;
				this.filterKernelFarField[j].y = 1f / DiffusionProfile.DisneyProfilePdf(num5, num);
				j++;
			}
			this.maxRadius = this.filterKernelFarField[20].x;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0003619E File Offset: 0x0003439E
		private static float DisneyProfile(float r, float s)
		{
			return s * (Mathf.Exp(-r * s) + Mathf.Exp(-r * s * 0.33333334f)) / (25.132742f * r);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000361C3 File Offset: 0x000343C3
		private static float DisneyProfilePdf(float r, float s)
		{
			return r * DiffusionProfile.DisneyProfile(r, s);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000361CE File Offset: 0x000343CE
		private static float DisneyProfileCdf(float r, float s)
		{
			return 1f - 0.25f * Mathf.Exp(-r * s) - 0.75f * Mathf.Exp(-r * s * 0.33333334f);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000361FB File Offset: 0x000343FB
		private static float DisneyProfileCdfDerivative1(float r, float s)
		{
			return 0.25f * s * Mathf.Exp(-r * s) * (1f + Mathf.Exp(r * s * 0.6666667f));
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00036223 File Offset: 0x00034423
		private static float DisneyProfileCdfDerivative2(float r, float s)
		{
			return -0.083333336f * s * s * Mathf.Exp(-r * s) * (3f + Mathf.Exp(r * s * 0.6666667f));
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00036250 File Offset: 0x00034450
		private static float DisneyProfileCdfInverse(float p, float s)
		{
			float num = (Mathf.Pow(10f, p) - 1f) / s;
			float num2 = float.MaxValue;
			for (;;)
			{
				float num3 = DiffusionProfile.DisneyProfileCdf(num, s) - p;
				float num4 = DiffusionProfile.DisneyProfileCdfDerivative1(num, s);
				float num5 = DiffusionProfile.DisneyProfileCdfDerivative2(num, s);
				float num6 = num3 / (num4 * (1f - num3 * num5 / (2f * num4 * num4)));
				if (Mathf.Abs(num6) >= num2)
				{
					break;
				}
				num -= num6;
				num2 = Mathf.Abs(num6);
			}
			return num;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000362C8 File Offset: 0x000344C8
		public bool Equals(DiffusionProfile other)
		{
			return other != null && (this.scatteringDistance == other.scatteringDistance && this.transmissionTint == other.transmissionTint && this.texturingMode == other.texturingMode && this.transmissionMode == other.transmissionMode && this.thicknessRemap == other.thicknessRemap && this.worldScale == other.worldScale && this.ior == other.ior && this.shapeParam == other.shapeParam && this.maxRadius == other.maxRadius && this.filterKernelNearField == other.filterKernelNearField && this.filterKernelFarField == other.filterKernelFarField && this.halfRcpWeightedVariances == other.halfRcpWeightedVariances) && this.filterKernelBasic == other.filterKernelBasic;
		}

		// Token: 0x04000703 RID: 1795
		[ColorUsage(false, true)]
		public Color scatteringDistance;

		// Token: 0x04000704 RID: 1796
		[ColorUsage(false, true)]
		public Color transmissionTint;

		// Token: 0x04000705 RID: 1797
		public DiffusionProfile.TexturingMode texturingMode;

		// Token: 0x04000706 RID: 1798
		public DiffusionProfile.TransmissionMode transmissionMode;

		// Token: 0x04000707 RID: 1799
		public Vector2 thicknessRemap;

		// Token: 0x04000708 RID: 1800
		public float worldScale;

		// Token: 0x04000709 RID: 1801
		public float ior;

		// Token: 0x04000710 RID: 1808
		public uint hash;

		// Token: 0x02000236 RID: 566
		public enum TexturingMode : uint
		{
			// Token: 0x04001476 RID: 5238
			PreAndPostScatter,
			// Token: 0x04001477 RID: 5239
			PostScatter
		}

		// Token: 0x02000237 RID: 567
		public enum TransmissionMode : uint
		{
			// Token: 0x04001479 RID: 5241
			Regular,
			// Token: 0x0400147A RID: 5242
			ThinObject
		}
	}
}

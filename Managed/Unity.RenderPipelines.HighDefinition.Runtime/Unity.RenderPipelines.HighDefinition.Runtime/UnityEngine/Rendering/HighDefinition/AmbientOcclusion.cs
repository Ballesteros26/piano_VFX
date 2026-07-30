using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000088 RID: 136
	[VolumeComponentMenu("Lighting/Ambient Occlusion")]
	[Serializable]
	public sealed class AmbientOcclusion : VolumeComponentWithQuality
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0002ED39 File Offset: 0x0002CF39
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0002ED65 File Offset: 0x0002CF65
		public int stepCount
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_StepCount.value;
				}
				return VolumeComponentWithQuality.GetLightingQualitySettings().AOStepCount[this.quality.value];
			}
			set
			{
				this.m_StepCount.value = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0002ED73 File Offset: 0x0002CF73
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0002ED9F File Offset: 0x0002CF9F
		public bool fullResolution
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_FullResolution.value;
				}
				return VolumeComponentWithQuality.GetLightingQualitySettings().AOFullRes[this.quality.value];
			}
			set
			{
				this.m_FullResolution.value = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0002EDAD File Offset: 0x0002CFAD
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0002EDD9 File Offset: 0x0002CFD9
		public int maximumRadiusInPixels
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_MaximumRadiusInPixels.value;
				}
				return VolumeComponentWithQuality.GetLightingQualitySettings().AOMaximumRadiusPixels[this.quality.value];
			}
			set
			{
				this.m_MaximumRadiusInPixels.value = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0002EDE7 File Offset: 0x0002CFE7
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0002EE13 File Offset: 0x0002D013
		public bool bilateralUpsample
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_BilateralUpsample.value;
				}
				return VolumeComponentWithQuality.GetLightingQualitySettings().AOBilateralUpsample[this.quality.value];
			}
			set
			{
				this.m_BilateralUpsample.value = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0002EE21 File Offset: 0x0002D021
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0002EE4D File Offset: 0x0002D04D
		public int directionCount
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_DirectionCount.value;
				}
				return VolumeComponentWithQuality.GetLightingQualitySettings().AODirectionCount[this.quality.value];
			}
			set
			{
				this.m_DirectionCount.value = value;
			}
		}

		// Token: 0x0400058A RID: 1418
		public BoolParameter rayTracing = new BoolParameter(false, false);

		// Token: 0x0400058B RID: 1419
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 4f, false);

		// Token: 0x0400058C RID: 1420
		public ClampedFloatParameter directLightingStrength = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x0400058D RID: 1421
		public ClampedFloatParameter radius = new ClampedFloatParameter(2f, 0.25f, 5f, false);

		// Token: 0x0400058E RID: 1422
		public BoolParameter temporalAccumulation = new BoolParameter(true, false);

		// Token: 0x0400058F RID: 1423
		public ClampedFloatParameter ghostingReduction = new ClampedFloatParameter(0.5f, 0f, 1f, false);

		// Token: 0x04000590 RID: 1424
		public ClampedFloatParameter blurSharpness = new ClampedFloatParameter(0.1f, 0f, 1f, false);

		// Token: 0x04000591 RID: 1425
		public LayerMaskParameter layerMask = new LayerMaskParameter(-1, false);

		// Token: 0x04000592 RID: 1426
		public ClampedFloatParameter rayLength = new ClampedFloatParameter(0.5f, 0f, 50f, false);

		// Token: 0x04000593 RID: 1427
		public ClampedIntParameter sampleCount = new ClampedIntParameter(4, 1, 64, false);

		// Token: 0x04000594 RID: 1428
		public BoolParameter denoise = new BoolParameter(false, false);

		// Token: 0x04000595 RID: 1429
		public ClampedFloatParameter denoiserRadius = new ClampedFloatParameter(0.5f, 0.001f, 1f, false);

		// Token: 0x04000596 RID: 1430
		[SerializeField]
		[FormerlySerializedAs("stepCount")]
		private ClampedIntParameter m_StepCount = new ClampedIntParameter(6, 2, 32, false);

		// Token: 0x04000597 RID: 1431
		[SerializeField]
		[FormerlySerializedAs("fullResolution")]
		private BoolParameter m_FullResolution = new BoolParameter(false, false);

		// Token: 0x04000598 RID: 1432
		[SerializeField]
		[FormerlySerializedAs("maximumRadiusInPixels")]
		private ClampedIntParameter m_MaximumRadiusInPixels = new ClampedIntParameter(40, 16, 256, false);

		// Token: 0x04000599 RID: 1433
		[SerializeField]
		[FormerlySerializedAs("bilateralUpsample")]
		private BoolParameter m_BilateralUpsample = new BoolParameter(true, false);

		// Token: 0x0400059A RID: 1434
		[SerializeField]
		[FormerlySerializedAs("directionCount")]
		private ClampedIntParameter m_DirectionCount = new ClampedIntParameter(2, 1, 6, false);
	}
}

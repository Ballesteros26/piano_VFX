using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000089 RID: 137
	[VolumeComponentMenu("Lighting/Screen Space Reflection")]
	[Serializable]
	public class ScreenSpaceReflection : VolumeComponentWithQuality
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0002EFC3 File Offset: 0x0002D1C3
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0002EFEF File Offset: 0x0002D1EF
		public int rayMaxIterations
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_RayMaxIterations.value;
				}
				return VolumeComponentWithQuality.GetLightingQualitySettings().SSRMaxRaySteps[this.quality.value];
			}
			set
			{
				this.m_RayMaxIterations.value = value;
			}
		}

		// Token: 0x0400059B RID: 1435
		public BoolParameter rayTracing = new BoolParameter(false, false);

		// Token: 0x0400059C RID: 1436
		public ClampedFloatParameter minSmoothness = new ClampedFloatParameter(0.9f, 0f, 1f, false);

		// Token: 0x0400059D RID: 1437
		public ClampedFloatParameter smoothnessFadeStart = new ClampedFloatParameter(0.9f, 0f, 1f, false);

		// Token: 0x0400059E RID: 1438
		public BoolParameter reflectSky = new BoolParameter(true, false);

		// Token: 0x0400059F RID: 1439
		public ClampedFloatParameter depthBufferThickness = new ClampedFloatParameter(0.01f, 0f, 1f, false);

		// Token: 0x040005A0 RID: 1440
		public ClampedFloatParameter screenFadeDistance = new ClampedFloatParameter(0.1f, 0f, 1f, false);

		// Token: 0x040005A1 RID: 1441
		public LayerMaskParameter layerMask = new LayerMaskParameter(-1, false);

		// Token: 0x040005A2 RID: 1442
		public ClampedFloatParameter rayLength = new ClampedFloatParameter(10f, 0.001f, 50f, false);

		// Token: 0x040005A3 RID: 1443
		public ClampedFloatParameter clampValue = new ClampedFloatParameter(1f, 0.001f, 10f, false);

		// Token: 0x040005A4 RID: 1444
		public BoolParameter denoise = new BoolParameter(false, false);

		// Token: 0x040005A5 RID: 1445
		public ClampedIntParameter denoiserRadius = new ClampedIntParameter(8, 1, 32, false);

		// Token: 0x040005A6 RID: 1446
		public RayTracingModeParameter mode = new RayTracingModeParameter(RayTracingMode.Quality, false);

		// Token: 0x040005A7 RID: 1447
		public IntParameter upscaleRadius = new ClampedIntParameter(2, 2, 6, false);

		// Token: 0x040005A8 RID: 1448
		public BoolParameter fullResolution = new BoolParameter(false, false);

		// Token: 0x040005A9 RID: 1449
		public ClampedIntParameter sampleCount = new ClampedIntParameter(1, 1, 32, false);

		// Token: 0x040005AA RID: 1450
		public ClampedIntParameter bounceCount = new ClampedIntParameter(1, 1, 31, false);

		// Token: 0x040005AB RID: 1451
		[SerializeField]
		[FormerlySerializedAs("rayMaxIterations")]
		private IntParameter m_RayMaxIterations = new IntParameter(32, false);
	}
}

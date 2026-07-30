using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200008C RID: 140
	[VolumeComponentMenu("Shadowing/Contact Shadows")]
	[Serializable]
	public class ContactShadows : VolumeComponentWithQuality
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0002F1D8 File Offset: 0x0002D3D8
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0002F211 File Offset: 0x0002D411
		public int sampleCount
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_SampleCount.value;
				}
				int value = this.quality.value;
				return VolumeComponentWithQuality.GetLightingQualitySettings().ContactShadowSampleCount[value];
			}
			set
			{
				this.m_SampleCount.value = value;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0002F220 File Offset: 0x0002D420
		private ContactShadows()
		{
			base.displayName = "Contact Shadows";
		}

		// Token: 0x040005B6 RID: 1462
		public BoolParameter enable = new BoolParameter(false, false);

		// Token: 0x040005B7 RID: 1463
		public ClampedFloatParameter length = new ClampedFloatParameter(0.15f, 0f, 1f, false);

		// Token: 0x040005B8 RID: 1464
		public ClampedFloatParameter opacity = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x040005B9 RID: 1465
		public ClampedFloatParameter distanceScaleFactor = new ClampedFloatParameter(0.5f, 0f, 1f, false);

		// Token: 0x040005BA RID: 1466
		public MinFloatParameter maxDistance = new MinFloatParameter(50f, 0f, false);

		// Token: 0x040005BB RID: 1467
		public MinFloatParameter fadeDistance = new MinFloatParameter(5f, 0f, false);

		// Token: 0x040005BC RID: 1468
		[SerializeField]
		[FormerlySerializedAs("sampleCount")]
		private NoInterpClampedIntParameter m_SampleCount = new NoInterpClampedIntParameter(8, 4, 64, false);
	}
}

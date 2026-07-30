using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000D2 RID: 210
	[VolumeComponentMenu("Post-processing/Depth Of Field")]
	[Serializable]
	public sealed class DepthOfField : VolumeComponentWithQuality, IPostProcessComponent
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000383D8 File Offset: 0x000365D8
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x00038416 File Offset: 0x00036616
		public int nearSampleCount
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_NearSampleCount.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().NearBlurSampleCount[item];
			}
			set
			{
				this.m_NearSampleCount.value = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00038424 File Offset: 0x00036624
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x00038462 File Offset: 0x00036662
		public float nearMaxBlur
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_NearMaxBlur.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().NearBlurMaxRadius[item];
			}
			set
			{
				this.m_NearMaxBlur.value = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00038470 File Offset: 0x00036670
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x000384AE File Offset: 0x000366AE
		public int farSampleCount
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_FarSampleCount.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().FarBlurSampleCount[item];
			}
			set
			{
				this.m_FarSampleCount.value = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x000384BC File Offset: 0x000366BC
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x000384FA File Offset: 0x000366FA
		public float farMaxBlur
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_FarMaxBlur.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().FarBlurMaxRadius[item];
			}
			set
			{
				this.m_FarMaxBlur.value = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00038508 File Offset: 0x00036708
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x00038546 File Offset: 0x00036746
		public bool highQualityFiltering
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_HighQualityFiltering.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().DoFHighQualityFiltering[item];
			}
			set
			{
				this.m_HighQualityFiltering.value = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00038554 File Offset: 0x00036754
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x00038592 File Offset: 0x00036792
		public DepthOfFieldResolution resolution
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_Resolution.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().DoFResolution[item];
			}
			set
			{
				this.m_Resolution.value = value;
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000385A0 File Offset: 0x000367A0
		public bool IsActive()
		{
			return this.focusMode.value != DepthOfFieldMode.Off && (this.IsNearLayerActive() || this.IsFarLayerActive());
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000385C1 File Offset: 0x000367C1
		public bool IsNearLayerActive()
		{
			return this.nearMaxBlur > 0f && this.nearFocusEnd.value > 0f;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000385E4 File Offset: 0x000367E4
		public bool IsFarLayerActive()
		{
			return this.farMaxBlur > 0f;
		}

		// Token: 0x04000788 RID: 1928
		[Tooltip("Specifies the mode that HDRP uses to set the focus for the depth of field effect.")]
		public DepthOfFieldModeParameter focusMode = new DepthOfFieldModeParameter(DepthOfFieldMode.Off, false);

		// Token: 0x04000789 RID: 1929
		[Tooltip("Sets the distance to the focus point from the Camera.")]
		public MinFloatParameter focusDistance = new MinFloatParameter(10f, 0.1f, false);

		// Token: 0x0400078A RID: 1930
		[Tooltip("Sets the distance from the Camera at which the near field blur begins to decrease in intensity.")]
		public MinFloatParameter nearFocusStart = new MinFloatParameter(0f, 0f, false);

		// Token: 0x0400078B RID: 1931
		[Tooltip("Sets the distance from the Camera at which the near field does not blur anymore.")]
		public MinFloatParameter nearFocusEnd = new MinFloatParameter(4f, 0f, false);

		// Token: 0x0400078C RID: 1932
		[Tooltip("Sets the distance from the Camera at which the far field starts blurring.")]
		public MinFloatParameter farFocusStart = new MinFloatParameter(10f, 0f, false);

		// Token: 0x0400078D RID: 1933
		[Tooltip("Sets the distance from the Camera at which the far field blur reaches its maximum blur radius.")]
		public MinFloatParameter farFocusEnd = new MinFloatParameter(20f, 0f, false);

		// Token: 0x0400078E RID: 1934
		[Tooltip("Sets the number of samples to use for the near field.")]
		[SerializeField]
		[FormerlySerializedAs("nearSampleCount")]
		private ClampedIntParameter m_NearSampleCount = new ClampedIntParameter(5, 3, 8, false);

		// Token: 0x0400078F RID: 1935
		[SerializeField]
		[FormerlySerializedAs("nearMaxBlur")]
		[Tooltip("Sets the maximum radius the near blur can reach.")]
		private ClampedFloatParameter m_NearMaxBlur = new ClampedFloatParameter(4f, 0f, 8f, false);

		// Token: 0x04000790 RID: 1936
		[Tooltip("Sets the number of samples to use for the far field.")]
		[SerializeField]
		[FormerlySerializedAs("farSampleCount")]
		private ClampedIntParameter m_FarSampleCount = new ClampedIntParameter(7, 3, 16, false);

		// Token: 0x04000791 RID: 1937
		[Tooltip("Sets the maximum radius the far blur can reach.")]
		[SerializeField]
		[FormerlySerializedAs("farMaxBlur")]
		private ClampedFloatParameter m_FarMaxBlur = new ClampedFloatParameter(8f, 0f, 16f, false);

		// Token: 0x04000792 RID: 1938
		[Tooltip("When enabled, HDRP uses bicubic filtering instead of bilinear filtering for the depth of field effect.")]
		[SerializeField]
		[FormerlySerializedAs("highQualityFiltering")]
		private BoolParameter m_HighQualityFiltering = new BoolParameter(true, false);

		// Token: 0x04000793 RID: 1939
		[Tooltip("Specifies the resolution at which HDRP processes the depth of field effect.")]
		[SerializeField]
		[FormerlySerializedAs("resolution")]
		private DepthOfFieldResolutionParameter m_Resolution = new DepthOfFieldResolutionParameter(DepthOfFieldResolution.Half, false);
	}
}

using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	public struct DensityVolumeArtistParameters
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00003AC0 File Offset: 0x00001CC0
		[Obsolete("Never worked correctly due to having engine working in percent. Will be removed soon.")]
		public bool advancedFade
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000333DC File Offset: 0x000315DC
		internal void MigrateToFixUniformBlendDistanceToBeMetric()
		{
			if (!this.m_EditorAdvancedFade)
			{
				this.m_EditorAdvancedFade = true;
				this.negativeFade = (this.positiveFade = this.m_EditorUniformFade * Vector3.one);
				this.m_EditorUniformFade = 0f;
			}
			this.m_EditorPositiveFade = this.positiveFade;
			this.m_EditorNegativeFade = this.negativeFade;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0003343C File Offset: 0x0003163C
		public DensityVolumeArtistParameters(Color color, float _meanFreePath, float _anisotropy)
		{
			this.albedo = color;
			this.meanFreePath = _meanFreePath;
			this.anisotropy = _anisotropy;
			this.volumeMask = null;
			this.textureIndex = -1;
			this.textureScrollingSpeed = Vector3.zero;
			this.textureTiling = Vector3.one;
			this.textureOffset = this.textureScrollingSpeed;
			this.size = Vector3.one;
			this.positiveFade = Vector3.zero;
			this.negativeFade = Vector3.zero;
			this.invertFade = false;
			this.distanceFadeStart = 10000f;
			this.distanceFadeEnd = 10000f;
			this.m_EditorPositiveFade = Vector3.zero;
			this.m_EditorNegativeFade = Vector3.zero;
			this.m_EditorUniformFade = 0f;
			this.m_EditorAdvancedFade = false;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000334F4 File Offset: 0x000316F4
		internal void Update(bool animate, float time)
		{
			if (this.volumeMask != null)
			{
				float num = (animate ? time : 0f);
				this.textureOffset = this.textureScrollingSpeed * num;
				this.textureOffset.x = -this.textureOffset.x;
				this.textureOffset.y = -this.textureOffset.y;
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0003355C File Offset: 0x0003175C
		internal void Constrain()
		{
			this.albedo.r = Mathf.Clamp01(this.albedo.r);
			this.albedo.g = Mathf.Clamp01(this.albedo.g);
			this.albedo.b = Mathf.Clamp01(this.albedo.b);
			this.albedo.a = 1f;
			this.meanFreePath = Mathf.Clamp(this.meanFreePath, 1f, float.MaxValue);
			this.anisotropy = Mathf.Clamp(this.anisotropy, -1f, 1f);
			this.textureOffset = Vector3.zero;
			this.distanceFadeStart = Mathf.Max(0f, this.distanceFadeStart);
			this.distanceFadeEnd = Mathf.Max(this.distanceFadeStart, this.distanceFadeEnd);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00033638 File Offset: 0x00031838
		internal DensityVolumeEngineData ConvertToEngineData()
		{
			DensityVolumeEngineData densityVolumeEngineData = default(DensityVolumeEngineData);
			densityVolumeEngineData.extinction = VolumeRenderingUtils.ExtinctionFromMeanFreePath(this.meanFreePath);
			densityVolumeEngineData.scattering = VolumeRenderingUtils.ScatteringFromExtinctionAndAlbedo(densityVolumeEngineData.extinction, this.albedo);
			densityVolumeEngineData.textureIndex = this.textureIndex;
			densityVolumeEngineData.textureScroll = this.textureOffset;
			densityVolumeEngineData.textureTiling = this.textureTiling;
			Vector3 vector = this.positiveFade;
			Vector3 vector2 = this.negativeFade;
			densityVolumeEngineData.rcpPosFaceFade.x = Mathf.Min(1f / vector.x, float.MaxValue);
			densityVolumeEngineData.rcpPosFaceFade.y = Mathf.Min(1f / vector.y, float.MaxValue);
			densityVolumeEngineData.rcpPosFaceFade.z = Mathf.Min(1f / vector.z, float.MaxValue);
			densityVolumeEngineData.rcpNegFaceFade.y = Mathf.Min(1f / vector2.y, float.MaxValue);
			densityVolumeEngineData.rcpNegFaceFade.x = Mathf.Min(1f / vector2.x, float.MaxValue);
			densityVolumeEngineData.rcpNegFaceFade.z = Mathf.Min(1f / vector2.z, float.MaxValue);
			densityVolumeEngineData.invertFade = (this.invertFade ? 1 : 0);
			float num = Mathf.Max(this.distanceFadeEnd - this.distanceFadeStart, 1.526E-05f);
			densityVolumeEngineData.rcpDistFadeLen = 1f / num;
			densityVolumeEngineData.endTimesRcpDistFadeLen = this.distanceFadeEnd * densityVolumeEngineData.rcpDistFadeLen;
			return densityVolumeEngineData;
		}

		// Token: 0x0400066A RID: 1642
		public Color albedo;

		// Token: 0x0400066B RID: 1643
		public float meanFreePath;

		// Token: 0x0400066C RID: 1644
		[FormerlySerializedAs("asymmetry")]
		public float anisotropy;

		// Token: 0x0400066D RID: 1645
		public Texture3D volumeMask;

		// Token: 0x0400066E RID: 1646
		public Vector3 textureScrollingSpeed;

		// Token: 0x0400066F RID: 1647
		public Vector3 textureTiling;

		// Token: 0x04000670 RID: 1648
		[FormerlySerializedAs("m_PositiveFade")]
		public Vector3 positiveFade;

		// Token: 0x04000671 RID: 1649
		[FormerlySerializedAs("m_NegativeFade")]
		public Vector3 negativeFade;

		// Token: 0x04000672 RID: 1650
		[SerializeField]
		[FormerlySerializedAs("m_UniformFade")]
		internal float m_EditorUniformFade;

		// Token: 0x04000673 RID: 1651
		[SerializeField]
		internal Vector3 m_EditorPositiveFade;

		// Token: 0x04000674 RID: 1652
		[SerializeField]
		internal Vector3 m_EditorNegativeFade;

		// Token: 0x04000675 RID: 1653
		[SerializeField]
		[FormerlySerializedAs("advancedFade")]
		[FormerlySerializedAs("m_AdvancedFade")]
		internal bool m_EditorAdvancedFade;

		// Token: 0x04000676 RID: 1654
		public Vector3 size;

		// Token: 0x04000677 RID: 1655
		public bool invertFade;

		// Token: 0x04000678 RID: 1656
		public float distanceFadeStart;

		// Token: 0x04000679 RID: 1657
		public float distanceFadeEnd;

		// Token: 0x0400067A RID: 1658
		[SerializeField]
		internal int textureIndex;

		// Token: 0x0400067B RID: 1659
		[SerializeField]
		[FormerlySerializedAs("volumeScrollingAmount")]
		public Vector3 textureOffset;
	}
}

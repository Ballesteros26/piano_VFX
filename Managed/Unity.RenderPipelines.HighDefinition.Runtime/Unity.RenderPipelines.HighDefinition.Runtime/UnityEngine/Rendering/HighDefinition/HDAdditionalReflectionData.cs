using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000076 RID: 118
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Reflection-Probe.html")]
	[RequireComponent(typeof(ReflectionProbe))]
	public sealed class HDAdditionalReflectionData : HDProbe, IVersionable<HDAdditionalReflectionData.ReflectionProbeVersion>
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0002B275 File Offset: 0x00029475
		private ReflectionProbe reflectionProbe
		{
			get
			{
				if (this.m_LegacyProbe == null || this.m_LegacyProbe.Equals(null))
				{
					this.m_LegacyProbe = base.GetComponent<ReflectionProbe>();
				}
				return this.m_LegacyProbe;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0002B2A8 File Offset: 0x000294A8
		public override void PrepareCulling()
		{
			base.PrepareCulling();
			InfluenceVolume influence = base.settings.influence;
			Transform transform = base.transform;
			Vector3 position = transform.position;
			ReflectionProbe reflectionProbe = this.reflectionProbe;
			InfluenceShape shape = influence.shape;
			if (shape != InfluenceShape.Box)
			{
				if (shape == InfluenceShape.Sphere)
				{
					reflectionProbe.size = Vector3.one * (2f * influence.sphereRadius);
					reflectionProbe.center = Vector3.zero;
				}
			}
			else
			{
				reflectionProbe.size = influence.boxSize;
				reflectionProbe.center = Vector3.zero;
			}
			transform.position = position;
			reflectionProbe.mode = ReflectionProbeMode.Custom;
			reflectionProbe.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0002B344 File Offset: 0x00029544
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x0002B34C File Offset: 0x0002954C
		HDAdditionalReflectionData.ReflectionProbeVersion IVersionable<HDAdditionalReflectionData.ReflectionProbeVersion>.version
		{
			get
			{
				return (HDAdditionalReflectionData.ReflectionProbeVersion)this.m_ReflectionProbeVersion;
			}
			set
			{
				this.m_ReflectionProbeVersion = (int)value;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0002B358 File Offset: 0x00029558
		private void Awake()
		{
			base.type = ProbeSettings.ProbeType.ReflectionProbe;
			HDAdditionalReflectionData.k_ReflectionProbeMigration.Migrate(this);
		}

		// Token: 0x040004FF RID: 1279
		private ReflectionProbe m_LegacyProbe;

		// Token: 0x04000500 RID: 1280
		private static readonly MigrationDescription<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData> k_ReflectionProbeMigration = MigrationDescription.New<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>(new MigrationStep<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>[]
		{
			MigrationStep.New<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>(HDAdditionalReflectionData.ReflectionProbeVersion.RemoveUsageOfLegacyProbeParamsForStocking, delegate(HDAdditionalReflectionData t)
			{
				t.m_ObsoleteBlendDistancePositive = (t.m_ObsoleteBlendDistanceNegative = Vector3.one * t.reflectionProbe.blendDistance);
				t.m_ObsoleteWeight = (float)t.reflectionProbe.importance;
				t.m_ObsoleteMultiplier = t.reflectionProbe.intensity;
				ReflectionProbeRefreshMode refreshMode = t.reflectionProbe.refreshMode;
				if (refreshMode != ReflectionProbeRefreshMode.OnAwake)
				{
					if (refreshMode == ReflectionProbeRefreshMode.EveryFrame)
					{
						t.realtimeMode = ProbeSettings.RealtimeMode.EveryFrame;
						return;
					}
				}
				else
				{
					t.realtimeMode = ProbeSettings.RealtimeMode.OnEnable;
				}
			}),
			MigrationStep.New<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>(HDAdditionalReflectionData.ReflectionProbeVersion.UseInfluenceVolume, delegate(HDAdditionalReflectionData t)
			{
				t.m_ObsoleteInfluenceVolume = t.m_ObsoleteInfluenceVolume ?? new InfluenceVolume();
				t.m_ObsoleteInfluenceVolume.boxSize = t.reflectionProbe.size;
				t.m_ObsoleteInfluenceVolume.obsoleteOffset = t.reflectionProbe.center;
				t.m_ObsoleteInfluenceVolume.sphereRadius = t.m_ObsoleteInfluenceSphereRadius;
				t.m_ObsoleteInfluenceVolume.shape = t.m_ObsoleteInfluenceShape;
				t.m_ObsoleteInfluenceVolume.boxBlendDistancePositive = t.m_ObsoleteBlendDistancePositive;
				t.m_ObsoleteInfluenceVolume.boxBlendDistanceNegative = t.m_ObsoleteBlendDistanceNegative;
				t.m_ObsoleteInfluenceVolume.boxBlendNormalDistancePositive = t.m_ObsoleteBlendNormalDistancePositive;
				t.m_ObsoleteInfluenceVolume.boxBlendNormalDistanceNegative = t.m_ObsoleteBlendNormalDistanceNegative;
				t.m_ObsoleteInfluenceVolume.boxSideFadePositive = t.m_ObsoleteBoxSideFadePositive;
				t.m_ObsoleteInfluenceVolume.boxSideFadeNegative = t.m_ObsoleteBoxSideFadeNegative;
			}),
			MigrationStep.New<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>(HDAdditionalReflectionData.ReflectionProbeVersion.MergeEditors, delegate(HDAdditionalReflectionData t)
			{
				t.m_ObsoleteInfiniteProjection = !t.reflectionProbe.boxProjection;
				t.reflectionProbe.boxProjection = false;
			}),
			MigrationStep.New<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>(HDAdditionalReflectionData.ReflectionProbeVersion.AddCaptureSettingsAndFrameSettings, delegate(HDAdditionalReflectionData t)
			{
				t.m_ObsoleteCaptureSettings = t.m_ObsoleteCaptureSettings ?? new ObsoleteCaptureSettings();
				t.m_ObsoleteCaptureSettings.cullingMask = t.reflectionProbe.cullingMask;
				t.m_ObsoleteCaptureSettings.nearClipPlane = t.reflectionProbe.nearClipPlane;
				t.m_ObsoleteCaptureSettings.farClipPlane = t.reflectionProbe.farClipPlane;
			}),
			MigrationStep.New<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>(HDAdditionalReflectionData.ReflectionProbeVersion.ModeAndTextures, delegate(HDAdditionalReflectionData t)
			{
				t.m_ObsoleteMode = (ProbeSettings.Mode)t.reflectionProbe.mode;
				t.SetTexture(ProbeSettings.Mode.Baked, t.reflectionProbe.bakedTexture);
				t.SetTexture(ProbeSettings.Mode.Custom, t.reflectionProbe.customBakedTexture);
			}),
			MigrationStep.New<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>(HDAdditionalReflectionData.ReflectionProbeVersion.ProbeSettings, delegate(HDAdditionalReflectionData t)
			{
				HDProbe.k_Migration.ExecuteStep(t, HDProbe.Version.ProbeSettings);
				Vector3 position = t.transform.position;
				Matrix4x4 matrix4x = Matrix4x4.TRS(t.transform.position, t.transform.rotation, Vector3.one);
				t.transform.position = matrix4x.MultiplyPoint(t.influenceVolume.obsoleteOffset);
				Vector3 vector = t.proxyToWorld.inverse.MultiplyPoint(position);
				t.m_ProbeSettings.proxySettings.capturePositionProxySpace = vector;
			}),
			MigrationStep.New<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>(HDAdditionalReflectionData.ReflectionProbeVersion.SeparatePassThrough, delegate(HDAdditionalReflectionData t)
			{
				HDProbe.k_Migration.ExecuteStep(t, HDProbe.Version.SeparatePassThrough);
			}),
			MigrationStep.New<HDAdditionalReflectionData.ReflectionProbeVersion, HDAdditionalReflectionData>(HDAdditionalReflectionData.ReflectionProbeVersion.UpgradeFrameSettingsToStruct, delegate(HDAdditionalReflectionData t)
			{
				HDProbe.k_Migration.ExecuteStep(t, HDProbe.Version.UpgradeFrameSettingsToStruct);
			})
		});

		// Token: 0x04000501 RID: 1281
		[SerializeField]
		[FormerlySerializedAs("version")]
		[FormerlySerializedAs("m_Version")]
		private int m_ReflectionProbeVersion;

		// Token: 0x04000502 RID: 1282
		[SerializeField]
		[FormerlySerializedAs("influenceShape")]
		[Obsolete("influenceShape is deprecated, use influenceVolume parameters instead")]
		private InfluenceShape m_ObsoleteInfluenceShape;

		// Token: 0x04000503 RID: 1283
		[SerializeField]
		[FormerlySerializedAs("influenceSphereRadius")]
		[Obsolete("influenceSphereRadius is deprecated, use influenceVolume parameters instead")]
		private float m_ObsoleteInfluenceSphereRadius = 3f;

		// Token: 0x04000504 RID: 1284
		[SerializeField]
		[FormerlySerializedAs("blendDistancePositive")]
		[Obsolete("blendDistancePositive is deprecated, use influenceVolume parameters instead")]
		private Vector3 m_ObsoleteBlendDistancePositive = Vector3.zero;

		// Token: 0x04000505 RID: 1285
		[SerializeField]
		[FormerlySerializedAs("blendDistanceNegative")]
		[Obsolete("blendDistanceNegative is deprecated, use influenceVolume parameters instead")]
		private Vector3 m_ObsoleteBlendDistanceNegative = Vector3.zero;

		// Token: 0x04000506 RID: 1286
		[SerializeField]
		[FormerlySerializedAs("blendNormalDistancePositive")]
		[Obsolete("blendNormalDistancePositive is deprecated, use influenceVolume parameters instead")]
		private Vector3 m_ObsoleteBlendNormalDistancePositive = Vector3.zero;

		// Token: 0x04000507 RID: 1287
		[SerializeField]
		[FormerlySerializedAs("blendNormalDistanceNegative")]
		[Obsolete("blendNormalDistanceNegative is deprecated, use influenceVolume parameters instead")]
		private Vector3 m_ObsoleteBlendNormalDistanceNegative = Vector3.zero;

		// Token: 0x04000508 RID: 1288
		[SerializeField]
		[FormerlySerializedAs("boxSideFadePositive")]
		[Obsolete("boxSideFadePositive is deprecated, use influenceVolume parameters instead")]
		private Vector3 m_ObsoleteBoxSideFadePositive = Vector3.one;

		// Token: 0x04000509 RID: 1289
		[SerializeField]
		[FormerlySerializedAs("boxSideFadeNegative")]
		[Obsolete("boxSideFadeNegative is deprecated, use influenceVolume parameters instead")]
		private Vector3 m_ObsoleteBoxSideFadeNegative = Vector3.one;

		// Token: 0x02000200 RID: 512
		private enum ReflectionProbeVersion
		{
			// Token: 0x04001364 RID: 4964
			First,
			// Token: 0x04001365 RID: 4965
			RemoveUsageOfLegacyProbeParamsForStocking,
			// Token: 0x04001366 RID: 4966
			HDProbeChild,
			// Token: 0x04001367 RID: 4967
			UseInfluenceVolume,
			// Token: 0x04001368 RID: 4968
			MergeEditors,
			// Token: 0x04001369 RID: 4969
			AddCaptureSettingsAndFrameSettings,
			// Token: 0x0400136A RID: 4970
			ModeAndTextures,
			// Token: 0x0400136B RID: 4971
			ProbeSettings,
			// Token: 0x0400136C RID: 4972
			SeparatePassThrough,
			// Token: 0x0400136D RID: 4973
			UpgradeFrameSettingsToStruct
		}
	}
}

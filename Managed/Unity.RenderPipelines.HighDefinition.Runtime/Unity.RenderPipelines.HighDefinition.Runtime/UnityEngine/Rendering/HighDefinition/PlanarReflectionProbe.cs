using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200007E RID: 126
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Planar-Reflection-Probe.html")]
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Planar Reflection Probe")]
	public sealed class PlanarReflectionProbe : HDProbe, IVersionable<PlanarReflectionProbe.PlanarProbeVersion>
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0002C356 File Offset: 0x0002A556
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0002C35E File Offset: 0x0002A55E
		PlanarReflectionProbe.PlanarProbeVersion IVersionable<PlanarReflectionProbe.PlanarProbeVersion>.version
		{
			get
			{
				return (PlanarReflectionProbe.PlanarProbeVersion)this.m_PlanarProbeVersion;
			}
			set
			{
				this.m_PlanarProbeVersion = (int)value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0002C367 File Offset: 0x0002A567
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x0002C36F File Offset: 0x0002A56F
		public Vector3 localReferencePosition
		{
			get
			{
				return this.m_LocalReferencePosition;
			}
			set
			{
				this.m_LocalReferencePosition = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0002C378 File Offset: 0x0002A578
		public Vector3 referencePosition
		{
			get
			{
				return base.transform.TransformPoint(this.m_LocalReferencePosition);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002C38C File Offset: 0x0002A58C
		private void Awake()
		{
			base.type = ProbeSettings.ProbeType.PlanarProbe;
			PlanarReflectionProbe.k_PlanarProbeMigration.Migrate(this);
		}

		// Token: 0x04000533 RID: 1331
		[SerializeField]
		[FormerlySerializedAs("version")]
		[FormerlySerializedAs("m_Version")]
		private int m_PlanarProbeVersion;

		// Token: 0x04000534 RID: 1332
		private static readonly MigrationDescription<PlanarReflectionProbe.PlanarProbeVersion, PlanarReflectionProbe> k_PlanarProbeMigration = MigrationDescription.New<PlanarReflectionProbe.PlanarProbeVersion, PlanarReflectionProbe>(new MigrationStep<PlanarReflectionProbe.PlanarProbeVersion, PlanarReflectionProbe>[]
		{
			MigrationStep.New<PlanarReflectionProbe.PlanarProbeVersion, PlanarReflectionProbe>(PlanarReflectionProbe.PlanarProbeVersion.CaptureSettings, delegate(PlanarReflectionProbe p)
			{
				if (p.m_ObsoleteCaptureSettings == null)
				{
					p.m_ObsoleteCaptureSettings = new ObsoleteCaptureSettings();
				}
				if (p.m_ObsoleteOverrideFieldOfView)
				{
					p.m_ObsoleteCaptureSettings.overrides |= ObsoleteCaptureSettingsOverrides.FieldOfview;
				}
				p.m_ObsoleteCaptureSettings.fieldOfView = p.m_ObsoleteFieldOfViewOverride;
				p.m_ObsoleteCaptureSettings.nearClipPlane = p.m_ObsoleteCaptureNearPlane;
				p.m_ObsoleteCaptureSettings.farClipPlane = p.m_ObsoleteCaptureFarPlane;
			}),
			MigrationStep.New<PlanarReflectionProbe.PlanarProbeVersion, PlanarReflectionProbe>(PlanarReflectionProbe.PlanarProbeVersion.ProbeSettings, delegate(PlanarReflectionProbe p)
			{
				HDProbe.k_Migration.ExecuteStep(p, HDProbe.Version.ProbeSettings);
				Vector3 position = p.transform.position;
				Matrix4x4 matrix4x = Matrix4x4.TRS(p.transform.position, p.transform.rotation, Vector3.one);
				p.transform.position = matrix4x.MultiplyPoint(p.influenceVolume.obsoleteOffset);
				Quaternion quaternion = p.transform.rotation * Quaternion.Euler(-90f, 0f, 0f);
				Matrix4x4 inverse = p.proxyToWorld.inverse;
				Vector3 vector = inverse.MultiplyPoint(position);
				Quaternion quaternion2 = inverse.rotation * quaternion;
				p.m_ProbeSettings.proxySettings.mirrorPositionProxySpace = vector;
				p.m_ProbeSettings.proxySettings.mirrorRotationProxySpace = quaternion2;
				p.m_LocalReferencePosition = Quaternion.Euler(-90f, 0f, 0f) * -p.m_LocalReferencePosition;
			}),
			MigrationStep.New<PlanarReflectionProbe.PlanarProbeVersion, PlanarReflectionProbe>(PlanarReflectionProbe.PlanarProbeVersion.SeparatePassThrough, delegate(PlanarReflectionProbe t)
			{
				HDProbe.k_Migration.ExecuteStep(t, HDProbe.Version.SeparatePassThrough);
			}),
			MigrationStep.New<PlanarReflectionProbe.PlanarProbeVersion, PlanarReflectionProbe>(PlanarReflectionProbe.PlanarProbeVersion.UpgradeFrameSettingsToStruct, delegate(PlanarReflectionProbe t)
			{
				HDProbe.k_Migration.ExecuteStep(t, HDProbe.Version.UpgradeFrameSettingsToStruct);
			})
		});

		// Token: 0x04000535 RID: 1333
		[SerializeField]
		[FormerlySerializedAs("m_CaptureNearPlane")]
		[Obsolete("For data migration")]
		private float m_ObsoleteCaptureNearPlane = ObsoleteCaptureSettings.@default.nearClipPlane;

		// Token: 0x04000536 RID: 1334
		[SerializeField]
		[FormerlySerializedAs("m_CaptureFarPlane")]
		[Obsolete("For data migration")]
		private float m_ObsoleteCaptureFarPlane = ObsoleteCaptureSettings.@default.farClipPlane;

		// Token: 0x04000537 RID: 1335
		[SerializeField]
		[FormerlySerializedAs("m_OverrideFieldOfView")]
		[Obsolete("For data migration")]
		private bool m_ObsoleteOverrideFieldOfView;

		// Token: 0x04000538 RID: 1336
		[SerializeField]
		[FormerlySerializedAs("m_FieldOfViewOverride")]
		[Obsolete("For data migration")]
		private float m_ObsoleteFieldOfViewOverride = ObsoleteCaptureSettings.@default.fieldOfView;

		// Token: 0x04000539 RID: 1337
		[SerializeField]
		private Vector3 m_LocalReferencePosition = -Vector3.forward;

		// Token: 0x02000205 RID: 517
		private enum PlanarProbeVersion
		{
			// Token: 0x0400137F RID: 4991
			Initial,
			// Token: 0x04001380 RID: 4992
			First = 2,
			// Token: 0x04001381 RID: 4993
			CaptureSettings,
			// Token: 0x04001382 RID: 4994
			ProbeSettings,
			// Token: 0x04001383 RID: 4995
			SeparatePassThrough,
			// Token: 0x04001384 RID: 4996
			UpgradeFrameSettingsToStruct
		}
	}
}

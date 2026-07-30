using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200017A RID: 378
	[Serializable]
	public struct ProbeCapturePositionSettings
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x00053A5D File Offset: 0x00051C5D
		public static ProbeCapturePositionSettings NewDefault()
		{
			return new ProbeCapturePositionSettings(Vector3.zero, Quaternion.identity, Vector3.zero, Quaternion.identity, Matrix4x4.identity);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00053A7D File Offset: 0x00051C7D
		public ProbeCapturePositionSettings(Vector3 proxyPosition, Quaternion proxyRotation, Matrix4x4 influenceToWorld)
		{
			this.proxyPosition = proxyPosition;
			this.proxyRotation = proxyRotation;
			this.referencePosition = Vector3.zero;
			this.referenceRotation = Quaternion.identity;
			this.influenceToWorld = influenceToWorld;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00053AAA File Offset: 0x00051CAA
		public ProbeCapturePositionSettings(Vector3 proxyPosition, Quaternion proxyRotation, Vector3 referencePosition, Quaternion referenceRotation, Matrix4x4 influenceToWorld)
		{
			this.proxyPosition = proxyPosition;
			this.proxyRotation = proxyRotation;
			this.referencePosition = referencePosition;
			this.referenceRotation = referenceRotation;
			this.influenceToWorld = influenceToWorld;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00053AD4 File Offset: 0x00051CD4
		public static ProbeCapturePositionSettings ComputeFrom(HDProbe probe, Transform reference)
		{
			Vector3 vector = Vector3.zero;
			Quaternion quaternion = Quaternion.identity;
			if (reference != null)
			{
				vector = reference.position;
				quaternion = reference.rotation;
			}
			else if (probe.type == ProbeSettings.ProbeType.PlanarProbe)
			{
				PlanarReflectionProbe planarReflectionProbe = (PlanarReflectionProbe)probe;
				return ProbeCapturePositionSettings.ComputeFromMirroredReference(planarReflectionProbe, planarReflectionProbe.referencePosition);
			}
			return ProbeCapturePositionSettings.ComputeFrom(probe, vector, quaternion);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00053B2C File Offset: 0x00051D2C
		public static ProbeCapturePositionSettings ComputeFromMirroredReference(HDProbe probe, Vector3 referencePosition)
		{
			ProbeCapturePositionSettings probeCapturePositionSettings = ProbeCapturePositionSettings.ComputeFrom(probe, referencePosition, Quaternion.identity);
			Vector3 vector = Matrix4x4.TRS(probeCapturePositionSettings.proxyPosition, probeCapturePositionSettings.proxyRotation, Vector3.one).MultiplyPoint(probe.settings.proxySettings.mirrorPositionProxySpace);
			probeCapturePositionSettings.referenceRotation = Quaternion.LookRotation(vector - probeCapturePositionSettings.referencePosition);
			return probeCapturePositionSettings;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00053B90 File Offset: 0x00051D90
		public Hash128 ComputeHash()
		{
			Hash128 hash = default(Hash128);
			Hash128 hash2 = default(Hash128);
			HashUtilities.QuantisedVectorHash(ref this.proxyPosition, ref hash);
			HashUtilities.QuantisedVectorHash(ref this.referencePosition, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			Vector3 vector = this.proxyRotation.eulerAngles;
			HashUtilities.QuantisedVectorHash(ref vector, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			vector = this.referenceRotation.eulerAngles;
			HashUtilities.QuantisedVectorHash(ref vector, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			return hash;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00053C10 File Offset: 0x00051E10
		private static ProbeCapturePositionSettings ComputeFrom(HDProbe probe, Vector3 referencePosition, Quaternion referenceRotation)
		{
			ProbeCapturePositionSettings probeCapturePositionSettings = default(ProbeCapturePositionSettings);
			Matrix4x4 proxyToWorld = probe.proxyToWorld;
			probeCapturePositionSettings.proxyPosition = proxyToWorld.GetColumn(3);
			probeCapturePositionSettings.proxyRotation = proxyToWorld.rotation;
			probeCapturePositionSettings.referencePosition = referencePosition;
			probeCapturePositionSettings.referenceRotation = referenceRotation;
			probeCapturePositionSettings.influenceToWorld = probe.influenceToWorld;
			return probeCapturePositionSettings;
		}

		// Token: 0x0400103F RID: 4159
		[Obsolete("Since 2019.3, use ProbeCapturePositionSettings.NewDefault() instead.")]
		public static readonly ProbeCapturePositionSettings @default;

		// Token: 0x04001040 RID: 4160
		public Vector3 proxyPosition;

		// Token: 0x04001041 RID: 4161
		public Quaternion proxyRotation;

		// Token: 0x04001042 RID: 4162
		public Vector3 referencePosition;

		// Token: 0x04001043 RID: 4163
		public Quaternion referenceRotation;

		// Token: 0x04001044 RID: 4164
		public Matrix4x4 influenceToWorld;
	}
}

using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200017E RID: 382
	public static class ProbeSettingsUtilities
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x00053DA8 File Offset: 0x00051FA8
		public static void ApplySettings(ref ProbeSettings settings, ref ProbeCapturePositionSettings probePosition, ref CameraSettings cameraSettings, ref CameraPositionSettings cameraPosition, float referenceFieldOfView = 90f, float referenceAspect = 1f)
		{
			cameraSettings = settings.cameraSettings;
			ProbeSettings.ProbeType type = settings.type;
			ProbeSettingsUtilities.PositionMode positionMode;
			bool flag;
			if (type != ProbeSettings.ProbeType.ReflectionProbe)
			{
				if (type != ProbeSettings.ProbeType.PlanarProbe)
				{
					throw new ArgumentOutOfRangeException();
				}
				positionMode = ProbeSettingsUtilities.PositionMode.MirrorReferenceTransformWithProbePlane;
				flag = true;
				ProbeSettingsUtilities.ApplyPlanarFrustumHandling(ref settings, ref probePosition, ref cameraSettings, ref cameraPosition, referenceFieldOfView, referenceAspect);
			}
			else
			{
				positionMode = ProbeSettingsUtilities.PositionMode.UseProbeTransform;
				flag = false;
				cameraSettings.frustum.mode = CameraSettings.Frustum.Mode.ComputeProjectionMatrix;
				cameraSettings.frustum.aspect = 1f;
				cameraSettings.frustum.fieldOfView = 90f;
			}
			if (positionMode != ProbeSettingsUtilities.PositionMode.UseProbeTransform)
			{
				if (positionMode == ProbeSettingsUtilities.PositionMode.MirrorReferenceTransformWithProbePlane)
				{
					cameraPosition.mode = CameraPositionSettings.Mode.UseWorldToCameraMatrixField;
					ProbeSettingsUtilities.ApplyMirroredReferenceTransform(ref settings, ref probePosition, ref cameraSettings, ref cameraPosition);
				}
			}
			else
			{
				cameraPosition.mode = CameraPositionSettings.Mode.ComputeWorldToCameraMatrix;
				Matrix4x4 matrix4x = Matrix4x4.TRS(probePosition.proxyPosition, probePosition.proxyRotation, Vector3.one);
				cameraPosition.position = matrix4x.MultiplyPoint(settings.proxySettings.capturePositionProxySpace);
				cameraPosition.rotation = matrix4x.rotation * settings.proxySettings.captureRotationProxySpace;
				if (settings.type == ProbeSettings.ProbeType.ReflectionProbe)
				{
					cameraPosition.rotation = Quaternion.identity;
				}
			}
			if (flag)
			{
				ProbeSettingsUtilities.ApplyObliqueNearClipPlane(ref settings, ref probePosition, ref cameraSettings, ref cameraPosition);
			}
			cameraSettings.probeRangeCompressionFactor = settings.lighting.rangeCompressionFactor;
			switch (settings.mode)
			{
			case ProbeSettings.Mode.Baked:
			case ProbeSettings.Mode.Custom:
				cameraSettings.defaultFrameSettings = FrameSettingsRenderType.CustomOrBakedReflection;
				break;
			default:
				cameraSettings.defaultFrameSettings = FrameSettingsRenderType.RealtimeReflection;
				break;
			}
			if (settings.type == ProbeSettings.ProbeType.ReflectionProbe)
			{
				cameraSettings.customRenderingSettings = true;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00053EF4 File Offset: 0x000520F4
		internal static void ApplyMirroredReferenceTransform(ref ProbeSettings settings, ref ProbeCapturePositionSettings probePosition, ref CameraSettings cameraSettings, ref CameraPositionSettings cameraPosition)
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(probePosition.proxyPosition, probePosition.proxyRotation, Vector3.one);
			Vector3 vector = matrix4x.MultiplyPoint(settings.proxySettings.mirrorPositionProxySpace);
			Vector3 vector2 = matrix4x.MultiplyVector(settings.proxySettings.mirrorRotationProxySpace * Vector3.forward);
			Matrix4x4 matrix4x2 = GeometryUtils.CalculateReflectionMatrix(vector, vector2);
			Matrix4x4 matrix4x3 = GeometryUtils.CalculateWorldToCameraMatrixRHS(probePosition.referencePosition, probePosition.referenceRotation);
			cameraPosition.worldToCameraMatrix = matrix4x3 * matrix4x2;
			cameraSettings.invertFaceCulling = true;
			cameraPosition.position = matrix4x2.MultiplyPoint(probePosition.referencePosition);
			Vector3 vector3 = matrix4x2.MultiplyVector(probePosition.referenceRotation * Vector3.forward);
			Vector3 vector4 = matrix4x2.MultiplyVector(probePosition.referenceRotation * Vector3.up);
			cameraPosition.rotation = Quaternion.LookRotation(vector3, vector4);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00053FC8 File Offset: 0x000521C8
		internal static void ApplyPlanarFrustumHandling(ref ProbeSettings settings, ref ProbeCapturePositionSettings probePosition, ref CameraSettings cameraSettings, ref CameraPositionSettings cameraPosition, float referenceFieldOfView, float referenceAspect)
		{
			Vector3 vector = Matrix4x4.TRS(probePosition.proxyPosition, probePosition.proxyRotation, Vector3.one).MultiplyPoint(settings.proxySettings.mirrorPositionProxySpace);
			cameraSettings.frustum.aspect = referenceAspect;
			switch (settings.frustum.fieldOfViewMode)
			{
			case ProbeSettings.Frustum.FOVMode.Fixed:
				cameraSettings.frustum.fieldOfView = settings.frustum.fixedValue;
				return;
			case ProbeSettings.Frustum.FOVMode.Viewer:
				cameraSettings.frustum.fieldOfView = Mathf.Min(referenceFieldOfView * settings.frustum.viewerScale, 170f);
				return;
			case ProbeSettings.Frustum.FOVMode.Automatic:
				cameraSettings.frustum.fieldOfView = Mathf.Min(settings.influence.ComputeFOVAt(probePosition.referencePosition, vector, probePosition.influenceToWorld) * settings.frustum.automaticScale, 170f);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000540A0 File Offset: 0x000522A0
		internal static void ApplyObliqueNearClipPlane(ref ProbeSettings settings, ref ProbeCapturePositionSettings probePosition, ref CameraSettings cameraSettings, ref CameraPositionSettings cameraPosition)
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(probePosition.proxyPosition, probePosition.proxyRotation, Vector3.one);
			Vector3 vector = matrix4x.MultiplyPoint(settings.proxySettings.mirrorPositionProxySpace);
			Vector3 vector2 = matrix4x.MultiplyVector(settings.proxySettings.mirrorRotationProxySpace * Vector3.forward);
			Vector4 vector3 = GeometryUtils.CameraSpacePlane(cameraPosition.worldToCameraMatrix, vector, vector2, 1f, 0f);
			Matrix4x4 matrix4x2 = GeometryUtils.CalculateObliqueMatrix(Matrix4x4.Perspective(HDUtils.ClampFOV(cameraSettings.frustum.fieldOfView), cameraSettings.frustum.aspect, cameraSettings.frustum.nearClipPlane, cameraSettings.frustum.farClipPlane), vector3);
			cameraSettings.frustum.mode = CameraSettings.Frustum.Mode.UseProjectionMatrixField;
			cameraSettings.frustum.projectionMatrix = matrix4x2;
		}

		// Token: 0x020002A1 RID: 673
		internal enum PositionMode
		{
			// Token: 0x04001726 RID: 5926
			UseProbeTransform,
			// Token: 0x04001727 RID: 5927
			MirrorReferenceTransformWithProbePlane
		}
	}
}

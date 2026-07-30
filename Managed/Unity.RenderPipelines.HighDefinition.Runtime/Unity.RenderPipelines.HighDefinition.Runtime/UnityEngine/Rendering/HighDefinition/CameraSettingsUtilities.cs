using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000176 RID: 374
	public static class CameraSettingsUtilities
	{
		// Token: 0x06000ABD RID: 2749 RVA: 0x000530E4 File Offset: 0x000512E4
		public unsafe static void ApplySettings(this Camera cam, CameraSettings settings)
		{
			HDAdditionalCameraData hdadditionalCameraData = cam.GetComponent<HDAdditionalCameraData>() ?? cam.gameObject.AddComponent<HDAdditionalCameraData>();
			hdadditionalCameraData.defaultFrameSettings = settings.defaultFrameSettings;
			*hdadditionalCameraData.renderingPathCustomFrameSettings = settings.renderingPathCustomFrameSettings;
			hdadditionalCameraData.renderingPathCustomFrameSettingsOverrideMask = settings.renderingPathCustomFrameSettingsOverrideMask;
			cam.nearClipPlane = settings.frustum.nearClipPlane;
			cam.farClipPlane = settings.frustum.farClipPlane;
			cam.fieldOfView = settings.frustum.fieldOfView;
			cam.aspect = settings.frustum.aspect;
			cam.projectionMatrix = settings.frustum.GetUsedProjectionMatrix();
			cam.useOcclusionCulling = settings.culling.useOcclusionCulling;
			cam.cullingMask = settings.culling.cullingMask;
			cam.overrideSceneCullingMask = settings.culling.sceneCullingMaskOverride;
			hdadditionalCameraData.clearColorMode = settings.bufferClearing.clearColorMode;
			hdadditionalCameraData.backgroundColorHDR = settings.bufferClearing.backgroundColorHDR;
			hdadditionalCameraData.clearDepth = settings.bufferClearing.clearDepth;
			hdadditionalCameraData.volumeLayerMask = settings.volumes.layerMask;
			hdadditionalCameraData.volumeAnchorOverride = settings.volumes.anchorOverride;
			hdadditionalCameraData.customRenderingSettings = settings.customRenderingSettings;
			hdadditionalCameraData.flipYMode = settings.flipYMode;
			hdadditionalCameraData.invertFaceCulling = settings.invertFaceCulling;
			hdadditionalCameraData.probeCustomFixedExposure = settings.probeRangeCompressionFactor;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00053243 File Offset: 0x00051443
		public static void ApplySettings(this Camera cam, CameraPositionSettings settings)
		{
			cam.transform.position = settings.position;
			cam.transform.rotation = settings.rotation;
			cam.worldToCameraMatrix = settings.GetUsedWorldToCameraMatrix();
		}
	}
}

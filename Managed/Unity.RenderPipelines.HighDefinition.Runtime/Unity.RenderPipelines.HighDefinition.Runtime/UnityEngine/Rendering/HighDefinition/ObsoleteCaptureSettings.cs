using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200012E RID: 302
	[Obsolete]
	[Serializable]
	internal class ObsoleteCaptureSettings
	{
		// Token: 0x04000DF6 RID: 3574
		public static ObsoleteCaptureSettings @default = new ObsoleteCaptureSettings();

		// Token: 0x04000DF7 RID: 3575
		public ObsoleteCaptureSettingsOverrides overrides;

		// Token: 0x04000DF8 RID: 3576
		public HDAdditionalCameraData.ClearColorMode clearColorMode;

		// Token: 0x04000DF9 RID: 3577
		[ColorUsage(true, true)]
		public Color backgroundColorHDR = new Color32(6, 18, 48, 0);

		// Token: 0x04000DFA RID: 3578
		public bool clearDepth = true;

		// Token: 0x04000DFB RID: 3579
		public LayerMask cullingMask = -1;

		// Token: 0x04000DFC RID: 3580
		public bool useOcclusionCulling = true;

		// Token: 0x04000DFD RID: 3581
		public LayerMask volumeLayerMask = 1;

		// Token: 0x04000DFE RID: 3582
		public Transform volumeAnchorOverride;

		// Token: 0x04000DFF RID: 3583
		public CameraProjection projection;

		// Token: 0x04000E00 RID: 3584
		public float nearClipPlane = 0.3f;

		// Token: 0x04000E01 RID: 3585
		public float farClipPlane = 1000f;

		// Token: 0x04000E02 RID: 3586
		public float fieldOfView = 90f;

		// Token: 0x04000E03 RID: 3587
		public float orthographicSize = 5f;

		// Token: 0x04000E04 RID: 3588
		public int renderingPath;

		// Token: 0x04000E05 RID: 3589
		public float shadowDistance = 100f;
	}
}

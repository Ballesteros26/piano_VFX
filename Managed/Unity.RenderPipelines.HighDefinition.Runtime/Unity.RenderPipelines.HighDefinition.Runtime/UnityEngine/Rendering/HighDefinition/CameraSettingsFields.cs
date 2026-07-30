using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000173 RID: 371
	[Flags]
	public enum CameraSettingsFields
	{
		// Token: 0x04001018 RID: 4120
		none = 0,
		// Token: 0x04001019 RID: 4121
		bufferClearColorMode = 2,
		// Token: 0x0400101A RID: 4122
		bufferClearBackgroundColorHDR = 4,
		// Token: 0x0400101B RID: 4123
		bufferClearClearDepth = 8,
		// Token: 0x0400101C RID: 4124
		volumesLayerMask = 16,
		// Token: 0x0400101D RID: 4125
		volumesAnchorOverride = 32,
		// Token: 0x0400101E RID: 4126
		frustumMode = 64,
		// Token: 0x0400101F RID: 4127
		frustumAspect = 128,
		// Token: 0x04001020 RID: 4128
		frustumFarClipPlane = 256,
		// Token: 0x04001021 RID: 4129
		frustumNearClipPlane = 512,
		// Token: 0x04001022 RID: 4130
		frustumFieldOfView = 1024,
		// Token: 0x04001023 RID: 4131
		frustumProjectionMatrix = 2048,
		// Token: 0x04001024 RID: 4132
		cullingUseOcclusionCulling = 4096,
		// Token: 0x04001025 RID: 4133
		cullingCullingMask = 8192,
		// Token: 0x04001026 RID: 4134
		cullingInvertFaceCulling = 16384,
		// Token: 0x04001027 RID: 4135
		customRenderingSettings = 32768,
		// Token: 0x04001028 RID: 4136
		flipYMode = 65536,
		// Token: 0x04001029 RID: 4137
		frameSettings = 131072,
		// Token: 0x0400102A RID: 4138
		probeLayerMask = 262144
	}
}

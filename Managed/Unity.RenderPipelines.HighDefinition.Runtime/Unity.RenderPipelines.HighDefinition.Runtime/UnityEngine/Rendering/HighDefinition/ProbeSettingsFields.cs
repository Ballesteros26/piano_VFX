using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200017B RID: 379
	[Flags]
	public enum ProbeSettingsFields
	{
		// Token: 0x04001046 RID: 4166
		none = 0,
		// Token: 0x04001047 RID: 4167
		type = 1,
		// Token: 0x04001048 RID: 4168
		mode = 2,
		// Token: 0x04001049 RID: 4169
		lightingMultiplier = 4,
		// Token: 0x0400104A RID: 4170
		lightingWeight = 8,
		// Token: 0x0400104B RID: 4171
		lightingLightLayer = 16,
		// Token: 0x0400104C RID: 4172
		lightingRangeCompression = 32,
		// Token: 0x0400104D RID: 4173
		proxyUseInfluenceVolumeAsProxyVolume = 64,
		// Token: 0x0400104E RID: 4174
		proxyCapturePositionProxySpace = 128,
		// Token: 0x0400104F RID: 4175
		proxyCaptureRotationProxySpace = 256,
		// Token: 0x04001050 RID: 4176
		proxyMirrorPositionProxySpace = 512,
		// Token: 0x04001051 RID: 4177
		proxyMirrorRotationProxySpace = 1024,
		// Token: 0x04001052 RID: 4178
		frustumFieldOfViewMode = 2048,
		// Token: 0x04001053 RID: 4179
		frustumFixedValue = 4096,
		// Token: 0x04001054 RID: 4180
		frustumAutomaticScale = 8192,
		// Token: 0x04001055 RID: 4181
		frustumViewerScale = 16384,
		// Token: 0x04001056 RID: 4182
		lightingFadeDistance = 32768,
		// Token: 0x04001057 RID: 4183
		resolution = 65536
	}
}

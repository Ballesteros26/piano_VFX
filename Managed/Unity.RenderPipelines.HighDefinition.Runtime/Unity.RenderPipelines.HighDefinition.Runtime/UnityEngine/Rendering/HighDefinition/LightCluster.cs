using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000116 RID: 278
	[VolumeComponentMenu("Ray Tracing/Light Cluster (Preview)")]
	[Serializable]
	public sealed class LightCluster : VolumeComponent
	{
		// Token: 0x04000D76 RID: 3446
		[Tooltip("Controls the maximal number lights in a cell.")]
		public ClampedIntParameter maxNumLightsPercell = new ClampedIntParameter(10, 0, 24, false);

		// Token: 0x04000D77 RID: 3447
		[Tooltip("Controls the range of the cluster around the camera.")]
		public ClampedFloatParameter cameraClusterRange = new ClampedFloatParameter(10f, 0.001f, 50f, false);
	}
}

using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000A8 RID: 168
	[VolumeComponentDeprecated]
	internal class VolumetricLightingController : VolumeComponent
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x000340E8 File Offset: 0x000322E8
		private VolumetricLightingController()
		{
			base.displayName = "Volumetric Fog Quality (Deprecated)";
		}

		// Token: 0x040006A0 RID: 1696
		[Tooltip("Sets the distance (in meters) from the Camera's Near Clipping Plane to the back of the Camera's volumetric lighting buffer.")]
		public MinFloatParameter depthExtent = new MinFloatParameter(64f, 0.1f, false);

		// Token: 0x040006A1 RID: 1697
		[Tooltip("Controls the distribution of slices along the Camera's focal axis. 0 is exponential distribution and 1 is linear distribution.")]
		[FormerlySerializedAs("depthDistributionUniformity")]
		public ClampedFloatParameter sliceDistributionUniformity = new ClampedFloatParameter(0.75f, 0f, 1f, false);
	}
}

using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200003D RID: 61
	[VolumeComponentDeprecated]
	internal class ExponentialFog : AtmosphericScattering
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00002646 File Offset: 0x00000846
		internal override void PushShaderParameters(HDCamera hdCamera, CommandBuffer cmd)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000AEC4 File Offset: 0x000090C4
		private ExponentialFog()
		{
			base.displayName = "Exponential Fog (Deprecated)";
		}

		// Token: 0x0400018F RID: 399
		private static readonly int m_ExpFogParam = Shader.PropertyToID("_ExpFogParameters");

		// Token: 0x04000190 RID: 400
		[Tooltip("Sets the distance from the Camera at which the fog reaches its maximum thickness.")]
		public MinFloatParameter fogDistance = new MinFloatParameter(200f, 0f, false);

		// Token: 0x04000191 RID: 401
		[Tooltip("Sets the height, in world space, at which HDRP begins to decrease the fog density from 1.0.")]
		public FloatParameter fogBaseHeight = new FloatParameter(0f, false);

		// Token: 0x04000192 RID: 402
		[Tooltip("Controls the falloff of height fog attenuation, larger values result in sharper attenuation.")]
		public ClampedFloatParameter fogHeightAttenuation = new ClampedFloatParameter(0.2f, 0f, 1f, false);
	}
}

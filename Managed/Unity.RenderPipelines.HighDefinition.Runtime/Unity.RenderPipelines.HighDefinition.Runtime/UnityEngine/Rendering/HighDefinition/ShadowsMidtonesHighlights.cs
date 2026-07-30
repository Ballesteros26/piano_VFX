using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000E5 RID: 229
	[VolumeComponentMenu("Post-processing/Shadows, Midtones, Highlights")]
	[Serializable]
	public sealed class ShadowsMidtonesHighlights : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x00038BE4 File Offset: 0x00036DE4
		public bool IsActive()
		{
			Vector4 vector = new Vector4(1f, 1f, 1f, 0f);
			return this.shadows != vector || this.midtones != vector || this.highlights != vector;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00038C38 File Offset: 0x00036E38
		private ShadowsMidtonesHighlights()
		{
			base.displayName = "Shadows, Midtones, Highlights";
		}

		// Token: 0x040007CF RID: 1999
		[Tooltip("Controls the darkest portions of the render.")]
		public Vector4Parameter shadows = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040007D0 RID: 2000
		[Tooltip("Controls the power function that handles mid-range tones.")]
		public Vector4Parameter midtones = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040007D1 RID: 2001
		[Tooltip("Controls the lightest portions of the render.")]
		public Vector4Parameter highlights = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040007D2 RID: 2002
		[Tooltip("Sets the start point of the transition between shadows and midtones.")]
		public MinFloatParameter shadowsStart = new MinFloatParameter(0f, 0f, false);

		// Token: 0x040007D3 RID: 2003
		[Tooltip("Sets the end point of the transition between shadows and midtones.")]
		public MinFloatParameter shadowsEnd = new MinFloatParameter(0.3f, 0f, false);

		// Token: 0x040007D4 RID: 2004
		[Tooltip("Sets the start point of the transition between midtones and highlights.")]
		public MinFloatParameter highlightsStart = new MinFloatParameter(0.55f, 0f, false);

		// Token: 0x040007D5 RID: 2005
		[Tooltip("Sets the end point of the transition between midtones and highlights.")]
		public MinFloatParameter highlightsEnd = new MinFloatParameter(1f, 0f, false);
	}
}

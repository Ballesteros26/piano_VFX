using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000151 RID: 337
	internal struct XRPassCreateInfo
	{
		// Token: 0x04000F37 RID: 3895
		public int multipassId;

		// Token: 0x04000F38 RID: 3896
		public int cullingPassId;

		// Token: 0x04000F39 RID: 3897
		public RenderTexture renderTarget;

		// Token: 0x04000F3A RID: 3898
		public ScriptableCullingParameters cullingParameters;

		// Token: 0x04000F3B RID: 3899
		public XRPass.CustomMirrorView customMirrorView;
	}
}

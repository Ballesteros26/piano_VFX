using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200032A RID: 810
	public enum PassType
	{
		// Token: 0x0400091F RID: 2335
		Normal,
		// Token: 0x04000920 RID: 2336
		Vertex,
		// Token: 0x04000921 RID: 2337
		VertexLM,
		// Token: 0x04000922 RID: 2338
		[Obsolete("VertexLMRGBM PassType is obsolete. Please use VertexLM PassType together with DecodeLightmap shader function.")]
		VertexLMRGBM,
		// Token: 0x04000923 RID: 2339
		ForwardBase,
		// Token: 0x04000924 RID: 2340
		ForwardAdd,
		// Token: 0x04000925 RID: 2341
		LightPrePassBase,
		// Token: 0x04000926 RID: 2342
		LightPrePassFinal,
		// Token: 0x04000927 RID: 2343
		ShadowCaster,
		// Token: 0x04000928 RID: 2344
		Deferred = 10,
		// Token: 0x04000929 RID: 2345
		Meta,
		// Token: 0x0400092A RID: 2346
		MotionVectors,
		// Token: 0x0400092B RID: 2347
		ScriptableRenderPipeline,
		// Token: 0x0400092C RID: 2348
		ScriptableRenderPipelineDefaultUnlit
	}
}

using System;

namespace UnityEngine.Rendering.HighDefinition.Attributes
{
	// Token: 0x02000183 RID: 387
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum DebugViewVarying
	{
		// Token: 0x0400106A RID: 4202
		None,
		// Token: 0x0400106B RID: 4203
		Texcoord0,
		// Token: 0x0400106C RID: 4204
		Texcoord1,
		// Token: 0x0400106D RID: 4205
		Texcoord2,
		// Token: 0x0400106E RID: 4206
		Texcoord3,
		// Token: 0x0400106F RID: 4207
		VertexTangentWS,
		// Token: 0x04001070 RID: 4208
		VertexBitangentWS,
		// Token: 0x04001071 RID: 4209
		VertexNormalWS,
		// Token: 0x04001072 RID: 4210
		VertexColor,
		// Token: 0x04001073 RID: 4211
		VertexColorAlpha
	}
}

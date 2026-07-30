using System;

namespace UnityEngine.Rendering.HighDefinition.Attributes
{
	// Token: 0x02000185 RID: 389
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum DebugViewProperties
	{
		// Token: 0x0400107D RID: 4221
		None,
		// Token: 0x0400107E RID: 4222
		Tessellation = 16,
		// Token: 0x0400107F RID: 4223
		PixelDisplacement,
		// Token: 0x04001080 RID: 4224
		VertexDisplacement,
		// Token: 0x04001081 RID: 4225
		TessellationDisplacement,
		// Token: 0x04001082 RID: 4226
		DepthOffset,
		// Token: 0x04001083 RID: 4227
		Lightmap,
		// Token: 0x04001084 RID: 4228
		Instancing
	}
}

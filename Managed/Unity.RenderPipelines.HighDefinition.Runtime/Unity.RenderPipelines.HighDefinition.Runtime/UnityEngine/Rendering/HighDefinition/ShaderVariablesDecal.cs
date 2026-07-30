using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000B2 RID: 178
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, needAccessors = false, omitStructDeclaration = true)]
	internal struct ShaderVariablesDecal
	{
		// Token: 0x040006F0 RID: 1776
		public Vector2 _DecalAtlasResolution;

		// Token: 0x040006F1 RID: 1777
		public uint _EnableDecals;

		// Token: 0x040006F2 RID: 1778
		public uint _DecalCount;
	}
}

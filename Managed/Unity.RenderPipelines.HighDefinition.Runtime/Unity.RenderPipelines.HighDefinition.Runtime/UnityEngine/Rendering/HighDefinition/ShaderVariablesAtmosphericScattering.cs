using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000042 RID: 66
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, needAccessors = false, omitStructDeclaration = true)]
	internal struct ShaderVariablesAtmosphericScattering
	{
		// Token: 0x040001AB RID: 427
		public int _FogEnabled;

		// Token: 0x040001AC RID: 428
		public int _PBRFogEnabled;

		// Token: 0x040001AD RID: 429
		public float _MaxFogDistance;

		// Token: 0x040001AE RID: 430
		public float _FogColorMode;

		// Token: 0x040001AF RID: 431
		public float _SkyTextureMipCount;

		// Token: 0x040001B0 RID: 432
		public Vector4 _FogColor;

		// Token: 0x040001B1 RID: 433
		public Vector4 _MipFogParameters;

		// Token: 0x040001B2 RID: 434
		public float _VBufferLastSliceDist;

		// Token: 0x040001B3 RID: 435
		public int _EnableVolumetricFog;
	}
}

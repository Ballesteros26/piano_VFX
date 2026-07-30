using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class FalseColorDebugSettings
	{
		// Token: 0x040000F1 RID: 241
		public bool falseColor;

		// Token: 0x040000F2 RID: 242
		public float colorThreshold0;

		// Token: 0x040000F3 RID: 243
		public float colorThreshold1 = 2f;

		// Token: 0x040000F4 RID: 244
		public float colorThreshold2 = 10f;

		// Token: 0x040000F5 RID: 245
		public float colorThreshold3 = 20f;
	}
}

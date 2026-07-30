using System;

namespace UnityEngine
{
	// Token: 0x0200004B RID: 75
	[Flags]
	public enum ParticleSystemSubEmitterProperties
	{
		// Token: 0x04000131 RID: 305
		InheritNothing = 0,
		// Token: 0x04000132 RID: 306
		InheritEverything = 31,
		// Token: 0x04000133 RID: 307
		InheritColor = 1,
		// Token: 0x04000134 RID: 308
		InheritSize = 2,
		// Token: 0x04000135 RID: 309
		InheritRotation = 4,
		// Token: 0x04000136 RID: 310
		InheritLifetime = 8,
		// Token: 0x04000137 RID: 311
		InheritDuration = 16
	}
}

using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x02000014 RID: 20
	[UsedByNativeCode]
	internal enum ProceduralTextureStackRequestStatus
	{
		// Token: 0x0400003E RID: 62
		StatusFree = 65535,
		// Token: 0x0400003F RID: 63
		StatusRequested,
		// Token: 0x04000040 RID: 64
		StatusProcessing,
		// Token: 0x04000041 RID: 65
		StatusComplete,
		// Token: 0x04000042 RID: 66
		StatusDropped
	}
}

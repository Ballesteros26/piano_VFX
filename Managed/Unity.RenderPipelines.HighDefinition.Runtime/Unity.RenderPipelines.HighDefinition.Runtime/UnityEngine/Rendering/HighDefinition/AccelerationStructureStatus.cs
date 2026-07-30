using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000111 RID: 273
	internal enum AccelerationStructureStatus
	{
		// Token: 0x04000D54 RID: 3412
		Clear,
		// Token: 0x04000D55 RID: 3413
		Added,
		// Token: 0x04000D56 RID: 3414
		Excluded,
		// Token: 0x04000D57 RID: 3415
		TransparencyIssue = 4,
		// Token: 0x04000D58 RID: 3416
		NullMaterial = 8,
		// Token: 0x04000D59 RID: 3417
		MissingMesh = 16
	}
}

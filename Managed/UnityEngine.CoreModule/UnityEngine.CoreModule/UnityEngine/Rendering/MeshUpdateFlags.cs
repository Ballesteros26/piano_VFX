using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000315 RID: 789
	[Flags]
	public enum MeshUpdateFlags
	{
		// Token: 0x04000845 RID: 2117
		Default = 0,
		// Token: 0x04000846 RID: 2118
		DontValidateIndices = 1,
		// Token: 0x04000847 RID: 2119
		DontResetBoneBounds = 2,
		// Token: 0x04000848 RID: 2120
		DontNotifyMeshUsers = 4,
		// Token: 0x04000849 RID: 2121
		DontRecalculateBounds = 8
	}
}

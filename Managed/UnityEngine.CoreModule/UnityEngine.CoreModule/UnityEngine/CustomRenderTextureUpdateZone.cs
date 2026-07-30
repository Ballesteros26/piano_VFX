using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000154 RID: 340
	[UsedByNativeCode]
	[Serializable]
	public struct CustomRenderTextureUpdateZone
	{
		// Token: 0x04000434 RID: 1076
		public Vector3 updateZoneCenter;

		// Token: 0x04000435 RID: 1077
		public Vector3 updateZoneSize;

		// Token: 0x04000436 RID: 1078
		public float rotation;

		// Token: 0x04000437 RID: 1079
		public int passIndex;

		// Token: 0x04000438 RID: 1080
		public bool needSwap;
	}
}

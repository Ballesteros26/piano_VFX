using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200016A RID: 362
	[UsedByNativeCode]
	public struct GradientAlphaKey
	{
		// Token: 0x06001070 RID: 4208 RVA: 0x00017DCA File Offset: 0x00015FCA
		public GradientAlphaKey(float alpha, float time)
		{
			this.alpha = alpha;
			this.time = time;
		}

		// Token: 0x040005B5 RID: 1461
		public float alpha;

		// Token: 0x040005B6 RID: 1462
		public float time;
	}
}

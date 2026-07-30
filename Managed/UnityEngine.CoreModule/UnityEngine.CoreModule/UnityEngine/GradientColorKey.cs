using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000169 RID: 361
	[UsedByNativeCode]
	public struct GradientColorKey
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x00017DB9 File Offset: 0x00015FB9
		public GradientColorKey(Color col, float time)
		{
			this.color = col;
			this.time = time;
		}

		// Token: 0x040005B3 RID: 1459
		public Color color;

		// Token: 0x040005B4 RID: 1460
		public float time;
	}
}

using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200022F RID: 559
	internal class GradientRemapPool : LinkedPool<GradientRemap>
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x00043F6C File Offset: 0x0004216C
		public GradientRemapPool()
			: base(() => new GradientRemap(), delegate(GradientRemap gradientRemap)
			{
				gradientRemap.Reset();
			}, 10000)
		{
		}
	}
}

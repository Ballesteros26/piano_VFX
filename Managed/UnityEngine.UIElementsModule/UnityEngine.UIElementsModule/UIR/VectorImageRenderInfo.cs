using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200022E RID: 558
	internal class VectorImageRenderInfo : LinkedPoolItem<VectorImageRenderInfo>
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x00043F44 File Offset: 0x00042144
		public void Reset()
		{
			this.useCount = 0;
			this.firstGradientRemap = null;
			this.gradientSettingsAlloc = default(Alloc);
		}

		// Token: 0x0400077F RID: 1919
		public int useCount;

		// Token: 0x04000780 RID: 1920
		public GradientRemap firstGradientRemap;

		// Token: 0x04000781 RID: 1921
		public Alloc gradientSettingsAlloc;
	}
}

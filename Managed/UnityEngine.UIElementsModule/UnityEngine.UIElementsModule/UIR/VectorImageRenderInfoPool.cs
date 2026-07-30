using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200022C RID: 556
	internal class VectorImageRenderInfoPool : LinkedPool<VectorImageRenderInfo>
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x00043ED0 File Offset: 0x000420D0
		public VectorImageRenderInfoPool()
			: base(() => new VectorImageRenderInfo(), delegate(VectorImageRenderInfo vectorImageInfo)
			{
				vectorImageInfo.Reset();
			}, 10000)
		{
		}
	}
}

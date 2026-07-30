using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000142 RID: 322
	[UsedByNativeCode]
	public struct LOD
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0000F6BD File Offset: 0x0000D8BD
		public LOD(float screenRelativeTransitionHeight, Renderer[] renderers)
		{
			this.screenRelativeTransitionHeight = screenRelativeTransitionHeight;
			this.fadeTransitionWidth = 0f;
			this.renderers = renderers;
		}

		// Token: 0x04000418 RID: 1048
		public float screenRelativeTransitionHeight;

		// Token: 0x04000419 RID: 1049
		public float fadeTransitionWidth;

		// Token: 0x0400041A RID: 1050
		public Renderer[] renderers;
	}
}

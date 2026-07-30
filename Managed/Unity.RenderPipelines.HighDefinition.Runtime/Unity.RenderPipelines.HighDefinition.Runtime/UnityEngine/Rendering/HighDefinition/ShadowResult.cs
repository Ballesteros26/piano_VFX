using System;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200008E RID: 142
	internal struct ShadowResult
	{
		// Token: 0x040005DA RID: 1498
		public RenderGraphResource punctualShadowResult;

		// Token: 0x040005DB RID: 1499
		public RenderGraphResource directionalShadowResult;

		// Token: 0x040005DC RID: 1500
		public RenderGraphResource areaShadowResult;
	}
}

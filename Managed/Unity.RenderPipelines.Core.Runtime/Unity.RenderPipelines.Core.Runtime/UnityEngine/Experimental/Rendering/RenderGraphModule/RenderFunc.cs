using System;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200000B RID: 11
	// (Invoke) Token: 0x0600002A RID: 42
	public delegate void RenderFunc<PassData>(PassData data, RenderGraphContext renderGraphContext) where PassData : class, new();
}

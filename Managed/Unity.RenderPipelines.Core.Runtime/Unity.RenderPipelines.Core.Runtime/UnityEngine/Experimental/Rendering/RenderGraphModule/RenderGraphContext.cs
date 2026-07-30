using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000008 RID: 8
	public ref struct RenderGraphContext
	{
		// Token: 0x04000020 RID: 32
		public ScriptableRenderContext renderContext;

		// Token: 0x04000021 RID: 33
		public CommandBuffer cmd;

		// Token: 0x04000022 RID: 34
		public RenderGraphObjectPool renderGraphPool;

		// Token: 0x04000023 RID: 35
		public RenderGraphResourceRegistry resources;
	}
}

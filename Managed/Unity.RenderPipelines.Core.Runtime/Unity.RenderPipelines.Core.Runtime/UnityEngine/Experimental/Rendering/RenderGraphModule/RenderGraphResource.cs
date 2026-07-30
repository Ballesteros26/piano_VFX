using System;
using System.Diagnostics;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000013 RID: 19
	[DebuggerDisplay("{type} ({handle})")]
	public struct RenderGraphResource
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003395 File Offset: 0x00001595
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000339D File Offset: 0x0000159D
		internal int handle { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000033A6 File Offset: 0x000015A6
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000033AE File Offset: 0x000015AE
		internal RenderGraphResourceType type { get; private set; }

		// Token: 0x06000060 RID: 96 RVA: 0x000033B7 File Offset: 0x000015B7
		internal RenderGraphResource(RenderGraphMutableResource mutableResource)
		{
			this.handle = mutableResource.handle;
			this.type = mutableResource.type;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000033D3 File Offset: 0x000015D3
		internal RenderGraphResource(int handle, RenderGraphResourceType type)
		{
			this.handle = handle;
			this.type = type;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000033E3 File Offset: 0x000015E3
		public bool IsValid()
		{
			return this.type > RenderGraphResourceType.Invalid;
		}
	}
}

using System;
using System.Diagnostics;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000014 RID: 20
	[DebuggerDisplay("{type} ({handle})")]
	public struct RenderGraphMutableResource
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000033EE File Offset: 0x000015EE
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000033F6 File Offset: 0x000015F6
		internal int handle { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000033FF File Offset: 0x000015FF
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003407 File Offset: 0x00001607
		internal RenderGraphResourceType type { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003410 File Offset: 0x00001610
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003418 File Offset: 0x00001618
		internal int version { get; private set; }

		// Token: 0x06000069 RID: 105 RVA: 0x00003421 File Offset: 0x00001621
		internal RenderGraphMutableResource(int handle, RenderGraphResourceType type)
		{
			this.handle = handle;
			this.type = type;
			this.version = 0;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003438 File Offset: 0x00001638
		internal RenderGraphMutableResource(RenderGraphMutableResource other)
		{
			this.handle = other.handle;
			this.type = other.type;
			this.version = other.version + 1;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003463 File Offset: 0x00001663
		public static implicit operator RenderGraphResource(RenderGraphMutableResource handle)
		{
			return new RenderGraphResource(handle);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000346B File Offset: 0x0000166B
		internal bool IsValid()
		{
			return this.type > RenderGraphResourceType.Invalid;
		}
	}
}

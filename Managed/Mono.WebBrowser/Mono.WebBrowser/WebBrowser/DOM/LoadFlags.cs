using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x0200002F RID: 47
	[Flags]
	public enum LoadFlags : uint
	{
		// Token: 0x04000073 RID: 115
		None = 0U,
		// Token: 0x04000074 RID: 116
		AsMetaRefresh = 16U,
		// Token: 0x04000075 RID: 117
		AsLinkClick = 32U,
		// Token: 0x04000076 RID: 118
		BypassHistory = 64U,
		// Token: 0x04000077 RID: 119
		ReplaceHistory = 128U,
		// Token: 0x04000078 RID: 120
		BypassLocalCache = 256U,
		// Token: 0x04000079 RID: 121
		BypassProxy = 512U,
		// Token: 0x0400007A RID: 122
		CharsetChange = 1024U
	}
}

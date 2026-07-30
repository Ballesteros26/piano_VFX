using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000033 RID: 51
	public interface IStylesheet
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000164 RID: 356
		string Type { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000165 RID: 357
		// (set) Token: 0x06000166 RID: 358
		bool Disabled { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000167 RID: 359
		INode OwnerNode { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000168 RID: 360
		IStylesheet ParentStyleSheet { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000169 RID: 361
		string Href { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600016A RID: 362
		string Title { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600016B RID: 363
		IMediaList Media { get; }
	}
}

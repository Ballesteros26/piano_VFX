using System;

namespace System.Xml.Xsl
{
	// Token: 0x020004BA RID: 1210
	internal interface ISourceLineInfo
	{
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x0600311C RID: 12572
		string Uri { get; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x0600311D RID: 12573
		bool IsNoSource { get; }

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x0600311E RID: 12574
		Location Start { get; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600311F RID: 12575
		Location End { get; }
	}
}

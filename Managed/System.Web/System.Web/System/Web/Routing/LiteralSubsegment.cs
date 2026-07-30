using System;

namespace System.Web.Routing
{
	// Token: 0x020004E4 RID: 1252
	internal sealed class LiteralSubsegment : PathSubsegment
	{
		// Token: 0x0600385C RID: 14428 RVA: 0x000976D0 File Offset: 0x000958D0
		public LiteralSubsegment(string literal)
		{
			this.Literal = literal;
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x000976DF File Offset: 0x000958DF
		// (set) Token: 0x0600385E RID: 14430 RVA: 0x000976E7 File Offset: 0x000958E7
		public string Literal { get; private set; }
	}
}

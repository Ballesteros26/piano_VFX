using System;

namespace System.Web.Routing
{
	// Token: 0x020004DC RID: 1244
	internal class BoundUrl
	{
		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x000974F1 File Offset: 0x000956F1
		// (set) Token: 0x06003846 RID: 14406 RVA: 0x000974F9 File Offset: 0x000956F9
		public string Url { get; set; }

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06003847 RID: 14407 RVA: 0x00097502 File Offset: 0x00095702
		// (set) Token: 0x06003848 RID: 14408 RVA: 0x0009750A File Offset: 0x0009570A
		public RouteValueDictionary Values { get; set; }
	}
}

using System;

namespace System.Web.Compilation
{
	// Token: 0x02000658 RID: 1624
	internal interface ILocation
	{
		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x060045A9 RID: 17833
		string Filename { get; }

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x060045AA RID: 17834
		int BeginLine { get; }

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x060045AB RID: 17835
		int EndLine { get; }

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x060045AC RID: 17836
		int BeginColumn { get; }

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x060045AD RID: 17837
		int EndColumn { get; }

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x060045AE RID: 17838
		string PlainText { get; }

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x060045AF RID: 17839
		string FileText { get; }
	}
}

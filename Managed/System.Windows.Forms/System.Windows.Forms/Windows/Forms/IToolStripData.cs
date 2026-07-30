using System;

namespace System.Windows.Forms
{
	// Token: 0x020001F3 RID: 499
	internal interface IToolStripData
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001F03 RID: 7939
		bool IsCurrentlyDragging { get; }

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001F04 RID: 7940
		// (set) Token: 0x06001F05 RID: 7941
		bool Stretch { get; set; }
	}
}

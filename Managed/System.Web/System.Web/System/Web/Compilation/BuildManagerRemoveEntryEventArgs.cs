using System;

namespace System.Web.Compilation
{
	// Token: 0x02000642 RID: 1602
	internal class BuildManagerRemoveEntryEventArgs : EventArgs
	{
		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x060044D7 RID: 17623 RVA: 0x000BCBB0 File Offset: 0x000BADB0
		// (set) Token: 0x060044D8 RID: 17624 RVA: 0x000BCBB8 File Offset: 0x000BADB8
		public string EntryName { get; private set; }

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x000BCBC1 File Offset: 0x000BADC1
		// (set) Token: 0x060044DA RID: 17626 RVA: 0x000BCBC9 File Offset: 0x000BADC9
		public HttpContext Context { get; private set; }

		// Token: 0x060044DB RID: 17627 RVA: 0x000BCBD2 File Offset: 0x000BADD2
		public BuildManagerRemoveEntryEventArgs(string entryName, HttpContext context)
		{
			this.EntryName = entryName;
			this.Context = context;
		}
	}
}

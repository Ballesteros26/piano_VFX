using System;

namespace System.Threading
{
	// Token: 0x02000024 RID: 36
	internal class ReaderWriterCount
	{
		// Token: 0x040001D0 RID: 464
		public long lockID;

		// Token: 0x040001D1 RID: 465
		public int readercount;

		// Token: 0x040001D2 RID: 466
		public int writercount;

		// Token: 0x040001D3 RID: 467
		public int upgradecount;

		// Token: 0x040001D4 RID: 468
		public ReaderWriterCount next;
	}
}

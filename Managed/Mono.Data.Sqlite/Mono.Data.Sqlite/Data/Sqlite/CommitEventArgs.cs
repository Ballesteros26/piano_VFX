using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000015 RID: 21
	public class CommitEventArgs : EventArgs
	{
		// Token: 0x06000154 RID: 340 RVA: 0x000082AD File Offset: 0x000064AD
		internal CommitEventArgs()
		{
		}

		// Token: 0x0400006E RID: 110
		public bool AbortTransaction;
	}
}

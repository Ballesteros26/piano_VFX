using System;

namespace System.Net
{
	// Token: 0x02000551 RID: 1361
	internal enum ReadState
	{
		// Token: 0x0400230A RID: 8970
		None,
		// Token: 0x0400230B RID: 8971
		Status,
		// Token: 0x0400230C RID: 8972
		Headers,
		// Token: 0x0400230D RID: 8973
		Content,
		// Token: 0x0400230E RID: 8974
		Aborted
	}
}

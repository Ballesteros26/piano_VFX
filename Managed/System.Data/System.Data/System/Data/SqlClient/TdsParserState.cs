using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000218 RID: 536
	internal enum TdsParserState
	{
		// Token: 0x0400114D RID: 4429
		Closed,
		// Token: 0x0400114E RID: 4430
		OpenNotLoggedIn,
		// Token: 0x0400114F RID: 4431
		OpenLoggedIn,
		// Token: 0x04001150 RID: 4432
		Broken
	}
}

using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000009 RID: 9
	[Flags]
	internal enum SQLiteOpenFlagsEnum
	{
		// Token: 0x0400004A RID: 74
		None = 0,
		// Token: 0x0400004B RID: 75
		ReadOnly = 1,
		// Token: 0x0400004C RID: 76
		ReadWrite = 2,
		// Token: 0x0400004D RID: 77
		Create = 4,
		// Token: 0x0400004E RID: 78
		Default = 6,
		// Token: 0x0400004F RID: 79
		FileProtectionComplete = 1048576,
		// Token: 0x04000050 RID: 80
		FileProtectionCompleteUnlessOpen = 2097152,
		// Token: 0x04000051 RID: 81
		FileProtectionCompleteUntilFirstUserAuthentication = 3145728,
		// Token: 0x04000052 RID: 82
		FileProtectionNone = 4194304
	}
}

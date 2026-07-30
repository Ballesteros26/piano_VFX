using System;

namespace System
{
	// Token: 0x02000103 RID: 259
	[Flags]
	internal enum UnescapeMode
	{
		// Token: 0x04000CE9 RID: 3305
		CopyOnly = 0,
		// Token: 0x04000CEA RID: 3306
		Escape = 1,
		// Token: 0x04000CEB RID: 3307
		Unescape = 2,
		// Token: 0x04000CEC RID: 3308
		EscapeUnescape = 3,
		// Token: 0x04000CED RID: 3309
		V1ToStringFlag = 4,
		// Token: 0x04000CEE RID: 3310
		UnescapeAll = 8,
		// Token: 0x04000CEF RID: 3311
		UnescapeAllOrThrow = 24
	}
}

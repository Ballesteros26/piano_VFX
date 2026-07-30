using System;

namespace System.Windows.Forms
{
	// Token: 0x02000409 RID: 1033
	[Flags]
	internal enum ChangeWindowFlags
	{
		// Token: 0x0400206F RID: 8303
		CWX = 1,
		// Token: 0x04002070 RID: 8304
		CWY = 2,
		// Token: 0x04002071 RID: 8305
		CWWidth = 4,
		// Token: 0x04002072 RID: 8306
		CWHeight = 8,
		// Token: 0x04002073 RID: 8307
		CWBorderWidth = 16,
		// Token: 0x04002074 RID: 8308
		CWSibling = 32,
		// Token: 0x04002075 RID: 8309
		CWStackMode = 64
	}
}

using System;

namespace System.Web.Compilation
{
	// Token: 0x02000664 RID: 1636
	internal enum BuildResultTypeCode
	{
		// Token: 0x0400251C RID: 9500
		Unknown,
		// Token: 0x0400251D RID: 9501
		AppCodeSubFolder,
		// Token: 0x0400251E RID: 9502
		Handler,
		// Token: 0x0400251F RID: 9503
		PageOrControl,
		// Token: 0x04002520 RID: 9504
		AppCode = 6,
		// Token: 0x04002521 RID: 9505
		Global = 8,
		// Token: 0x04002522 RID: 9506
		TopLevelAssembly
	}
}

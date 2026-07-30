using System;

namespace System.Net
{
	// Token: 0x02000484 RID: 1156
	internal enum WebParseErrorCode
	{
		// Token: 0x04001EAF RID: 7855
		Generic,
		// Token: 0x04001EB0 RID: 7856
		InvalidHeaderName,
		// Token: 0x04001EB1 RID: 7857
		InvalidContentLength,
		// Token: 0x04001EB2 RID: 7858
		IncompleteHeaderLine,
		// Token: 0x04001EB3 RID: 7859
		CrLfError,
		// Token: 0x04001EB4 RID: 7860
		InvalidChunkFormat,
		// Token: 0x04001EB5 RID: 7861
		UnexpectedServerResponse
	}
}

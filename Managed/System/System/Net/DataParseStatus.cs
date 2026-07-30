using System;

namespace System.Net
{
	// Token: 0x02000481 RID: 1153
	internal enum DataParseStatus
	{
		// Token: 0x04001E9F RID: 7839
		NeedMoreData,
		// Token: 0x04001EA0 RID: 7840
		ContinueParsing,
		// Token: 0x04001EA1 RID: 7841
		Done,
		// Token: 0x04001EA2 RID: 7842
		Invalid,
		// Token: 0x04001EA3 RID: 7843
		DataTooBig
	}
}

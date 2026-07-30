using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000212 RID: 530
	internal struct SNIErrorDetails
	{
		// Token: 0x0400112B RID: 4395
		public string errorMessage;

		// Token: 0x0400112C RID: 4396
		public uint nativeError;

		// Token: 0x0400112D RID: 4397
		public uint sniErrorNumber;

		// Token: 0x0400112E RID: 4398
		public int provider;

		// Token: 0x0400112F RID: 4399
		public uint lineNumber;

		// Token: 0x04001130 RID: 4400
		public string function;

		// Token: 0x04001131 RID: 4401
		public Exception exception;
	}
}

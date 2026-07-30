using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000245 RID: 581
	internal class SNIError
	{
		// Token: 0x060019C9 RID: 6601 RVA: 0x00082DD4 File Offset: 0x00080FD4
		public SNIError(SNIProviders provider, uint nativeError, uint sniErrorCode, string errorMessage)
		{
			this.lineNumber = 0U;
			this.function = string.Empty;
			this.provider = provider;
			this.nativeError = nativeError;
			this.sniError = sniErrorCode;
			this.errorMessage = errorMessage;
			this.exception = null;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00082E14 File Offset: 0x00081014
		public SNIError(SNIProviders provider, uint sniErrorCode, Exception sniException)
		{
			this.lineNumber = 0U;
			this.function = string.Empty;
			this.provider = provider;
			this.nativeError = 0U;
			this.sniError = sniErrorCode;
			this.errorMessage = string.Empty;
			this.exception = sniException;
		}

		// Token: 0x0400128C RID: 4748
		public readonly SNIProviders provider;

		// Token: 0x0400128D RID: 4749
		public readonly string errorMessage;

		// Token: 0x0400128E RID: 4750
		public readonly uint nativeError;

		// Token: 0x0400128F RID: 4751
		public readonly uint sniError;

		// Token: 0x04001290 RID: 4752
		public readonly string function;

		// Token: 0x04001291 RID: 4753
		public readonly uint lineNumber;

		// Token: 0x04001292 RID: 4754
		public readonly Exception exception;
	}
}

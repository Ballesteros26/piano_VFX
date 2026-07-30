using System;

namespace System.Net
{
	// Token: 0x02000044 RID: 68
	internal struct SecurityStatusPal
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000E239 File Offset: 0x0000C439
		public SecurityStatusPal(SecurityStatusPalErrorCode errorCode, Exception exception = null)
		{
			this.ErrorCode = errorCode;
			this.Exception = exception;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000E24C File Offset: 0x0000C44C
		public override string ToString()
		{
			if (this.Exception != null)
			{
				return string.Format("{0}={1}, {2}={3}", new object[] { "ErrorCode", this.ErrorCode, "Exception", this.Exception });
			}
			return string.Format("{0}={1}", "ErrorCode", this.ErrorCode);
		}

		// Token: 0x0400048D RID: 1165
		public readonly SecurityStatusPalErrorCode ErrorCode;

		// Token: 0x0400048E RID: 1166
		public readonly Exception Exception;
	}
}

using System;

namespace System.Net
{
	// Token: 0x02000518 RID: 1304
	internal class FtpStatus
	{
		// Token: 0x0600272A RID: 10026 RVA: 0x000970BD File Offset: 0x000952BD
		public FtpStatus(FtpStatusCode statusCode, string statusDescription)
		{
			this.statusCode = statusCode;
			this.statusDescription = statusDescription;
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000970D3 File Offset: 0x000952D3
		public FtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x000970DB File Offset: 0x000952DB
		public string StatusDescription
		{
			get
			{
				return this.statusDescription;
			}
		}

		// Token: 0x0400213D RID: 8509
		private readonly FtpStatusCode statusCode;

		// Token: 0x0400213E RID: 8510
		private readonly string statusDescription;
	}
}

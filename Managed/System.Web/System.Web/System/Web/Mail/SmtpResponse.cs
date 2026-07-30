using System;

namespace System.Web.Mail
{
	// Token: 0x020000FF RID: 255
	internal class SmtpResponse
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x00002050 File Offset: 0x00000250
		protected SmtpResponse()
		{
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0002485C File Offset: 0x00022A5C
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x00024864 File Offset: 0x00022A64
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0002486D File Offset: 0x00022A6D
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x00024875 File Offset: 0x00022A75
		public string RawResponse
		{
			get
			{
				return this.rawResponse;
			}
			set
			{
				this.rawResponse = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0002487E File Offset: 0x00022A7E
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x00024886 File Offset: 0x00022A86
		public string[] Parts
		{
			get
			{
				return this.parts;
			}
			set
			{
				this.parts = value;
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00024890 File Offset: 0x00022A90
		public static SmtpResponse Parse(string line)
		{
			SmtpResponse smtpResponse = new SmtpResponse();
			if (line.Length < 4)
			{
				throw new SmtpException("Response is to short " + line.Length + ".");
			}
			if (line[3] != ' ' && line[3] != '-')
			{
				throw new SmtpException("Response format is wrong.(" + line + ")");
			}
			smtpResponse.StatusCode = int.Parse(line.Substring(0, 3));
			smtpResponse.RawResponse = line;
			smtpResponse.Parts = line.Substring(0, 3).Split(new char[] { ';' });
			return smtpResponse;
		}

		// Token: 0x04001154 RID: 4436
		private string rawResponse;

		// Token: 0x04001155 RID: 4437
		private int statusCode;

		// Token: 0x04001156 RID: 4438
		private string[] parts;
	}
}

using System;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000B5 RID: 181
	internal class InvalidContentTypeException : Exception
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0001620D File Offset: 0x0001440D
		internal InvalidContentTypeException(string message, string contentType)
			: base(message)
		{
			this.contentType = contentType;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001621D File Offset: 0x0001441D
		internal string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x0400035C RID: 860
		private string contentType;
	}
}

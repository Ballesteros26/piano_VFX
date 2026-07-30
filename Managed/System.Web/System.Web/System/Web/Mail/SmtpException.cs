using System;
using System.IO;

namespace System.Web.Mail
{
	// Token: 0x020000FD RID: 253
	internal class SmtpException : IOException
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x00024774 File Offset: 0x00022974
		public SmtpException(string message)
			: base(message)
		{
		}
	}
}

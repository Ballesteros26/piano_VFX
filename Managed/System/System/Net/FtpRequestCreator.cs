using System;

namespace System.Net
{
	// Token: 0x02000517 RID: 1303
	internal class FtpRequestCreator : IWebRequestCreate
	{
		// Token: 0x06002728 RID: 10024 RVA: 0x000970B5 File Offset: 0x000952B5
		public WebRequest Create(Uri uri)
		{
			return new FtpWebRequest(uri);
		}
	}
}

using System;

namespace System.Net
{
	// Token: 0x02000529 RID: 1321
	internal class HttpRequestCreator : IWebRequestCreate
	{
		// Token: 0x06002879 RID: 10361 RVA: 0x000020EB File Offset: 0x000002EB
		internal HttpRequestCreator()
		{
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0009BF02 File Offset: 0x0009A102
		public WebRequest Create(Uri uri)
		{
			return new HttpWebRequest(uri);
		}
	}
}

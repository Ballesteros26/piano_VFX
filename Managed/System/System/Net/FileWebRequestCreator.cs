using System;

namespace System.Net
{
	// Token: 0x020004BF RID: 1215
	internal class FileWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x06002403 RID: 9219 RVA: 0x000020EB File Offset: 0x000002EB
		internal FileWebRequestCreator()
		{
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x0008CA34 File Offset: 0x0008AC34
		public WebRequest Create(Uri uri)
		{
			return new FileWebRequest(uri);
		}
	}
}

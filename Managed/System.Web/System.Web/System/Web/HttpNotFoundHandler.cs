using System;

namespace System.Web
{
	// Token: 0x0200009A RID: 154
	internal sealed class HttpNotFoundHandler : IHttpHandler
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x00011310 File Offset: 0x0000F510
		public void ProcessRequest(HttpContext context)
		{
			string path = context.Request.Path;
			throw new HttpException(404, "Path '" + path + "' was not found.", path);
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00008B66 File Offset: 0x00006D66
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}

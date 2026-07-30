using System;

namespace System.Web
{
	// Token: 0x02000098 RID: 152
	internal class HttpMethodNotAllowedHandler : IHttpHandler
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public void ProcessRequest(HttpContext context)
		{
			throw new HttpException(405, "Method not allowed");
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00008B66 File Offset: 0x00006D66
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}

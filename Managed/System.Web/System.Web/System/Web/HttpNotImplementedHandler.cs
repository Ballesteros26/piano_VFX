using System;

namespace System.Web
{
	// Token: 0x0200009B RID: 155
	internal sealed class HttpNotImplementedHandler : IHttpHandler
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x00011344 File Offset: 0x0000F544
		public void ProcessRequest(HttpContext context)
		{
			HttpRequest request = context.Request;
			throw new HttpException(501, request.HttpMethod + " " + request.Path + " is not implemented.");
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00008B66 File Offset: 0x00006D66
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}

using System;

namespace System.Web.Routing
{
	// Token: 0x020004FB RID: 1275
	internal sealed class UrlAuthFailureHandler : IHttpHandler
	{
		// Token: 0x06003900 RID: 14592 RVA: 0x00003A1F File Offset: 0x00001C1F
		public void ProcessRequest(HttpContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x00008B66 File Offset: 0x00006D66
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}

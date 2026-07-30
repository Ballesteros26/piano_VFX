using System;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000091 RID: 145
	internal class NopHandler : IHttpHandler
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00002B51 File Offset: 0x00000D51
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000210D File Offset: 0x0000030D
		public void ProcessRequest(HttpContext context)
		{
		}
	}
}

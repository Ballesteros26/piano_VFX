using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x02000224 RID: 548
	internal class SimpleHandlerFactory : IHttpHandlerFactory
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x0003C0B3 File Offset: 0x0003A2B3
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
		{
			return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(IHttpHandler)) as IHttpHandler;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}

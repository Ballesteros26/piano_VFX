using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Provides a way to specify that ASP.NET routing should not handle requests for a URL pattern.</summary>
	// Token: 0x020004FA RID: 1274
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class StopRoutingHandler : IRouteHandler
	{
		/// <summary>Returns the object that processes the request.</summary>
		/// <returns>An object that processes the request.</returns>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		// Token: 0x060038FD RID: 14589 RVA: 0x00003A01 File Offset: 0x00001C01
		protected virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns the object that processes the request.</summary>
		/// <returns>An object that processes the request.</returns>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		// Token: 0x060038FE RID: 14590 RVA: 0x000999E6 File Offset: 0x00097BE6
		IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
		{
			return this.GetHttpHandler(requestContext);
		}
	}
}

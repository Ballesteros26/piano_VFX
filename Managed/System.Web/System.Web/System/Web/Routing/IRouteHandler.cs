using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Defines the contract that a class must implement in order to process a request for a matching route pattern.</summary>
	// Token: 0x020004E3 RID: 1251
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public interface IRouteHandler
	{
		/// <summary>Provides the object that processes the request.</summary>
		/// <returns>An object that processes the request.</returns>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		// Token: 0x0600385B RID: 14427
		IHttpHandler GetHttpHandler(RequestContext requestContext);
	}
}

using System;

namespace System.Web
{
	/// <summary>Defines the contract that class factories must implement to create new <see cref="T:System.Web.IHttpHandler" /> objects.</summary>
	// Token: 0x02000048 RID: 72
	public interface IHttpHandlerFactory
	{
		/// <summary>Returns an instance of a class that implements the <see cref="T:System.Web.IHttpHandler" /> interface.</summary>
		/// <returns>A new <see cref="T:System.Web.IHttpHandler" /> object that processes the request.</returns>
		/// <param name="context">An instance of the <see cref="T:System.Web.HttpContext" /> class that provides references to intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
		/// <param name="requestType">The HTTP data transfer method (GET or POST) that the client uses. </param>
		/// <param name="url">The <see cref="P:System.Web.HttpRequest.RawUrl" /> of the requested resource. </param>
		/// <param name="pathTranslated">The <see cref="P:System.Web.HttpRequest.PhysicalApplicationPath" /> to the requested resource. </param>
		// Token: 0x060003C7 RID: 967
		IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated);

		/// <summary>Enables a factory to reuse an existing handler instance.</summary>
		/// <param name="handler">The <see cref="T:System.Web.IHttpHandler" /> object to reuse. </param>
		// Token: 0x060003C8 RID: 968
		void ReleaseHandler(IHttpHandler handler);
	}
}

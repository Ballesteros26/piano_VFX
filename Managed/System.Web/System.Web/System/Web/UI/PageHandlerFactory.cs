using System;

namespace System.Web.UI
{
	/// <summary>Creates instances of classes that inherit from the <see cref="T:System.Web.UI.Page" /> class and implement the <see cref="T:System.Web.IHttpHandler" /> interface. Instances are created dynamically to handle requests for ASP.NET files. The <see cref="T:System.Web.UI.PageHandlerFactory" /> class is the default handler factory implementation for ASP.NET pages.</summary>
	// Token: 0x02000210 RID: 528
	public class PageHandlerFactory : IHttpHandlerFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageHandlerFactory" /> class.</summary>
		// Token: 0x0600158B RID: 5515 RVA: 0x00002050 File Offset: 0x00000250
		protected internal PageHandlerFactory()
		{
		}

		/// <summary>Returns an instance of the <see cref="T:System.Web.IHttpHandler" /> interface to process the requested resource.</summary>
		/// <returns>A new <see cref="T:System.Web.IHttpHandler" /> that processes the request; otherwise, null.</returns>
		/// <param name="context">An instance of the <see cref="T:System.Web.HttpContext" /> class that provides references to intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
		/// <param name="requestType">The HTTP data transfer method (GET or POST) that the client uses.</param>
		/// <param name="virtualPath">The virtual path to the requested resource.</param>
		/// <param name="path">The <see cref="P:System.Web.HttpRequest.PhysicalApplicationPath" /> property to the requested resource.</param>
		// Token: 0x0600158C RID: 5516 RVA: 0x0003A4DB File Offset: 0x000386DB
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
		{
			return PageParser.GetCompiledPageInstance(virtualPath, path, context);
		}

		/// <summary>Enables a factory to reuse an existing instance of a handler.</summary>
		/// <param name="handler">The <see cref="T:System.Web.IHttpHandler" /> to reuse.</param>
		// Token: 0x0600158D RID: 5517 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}

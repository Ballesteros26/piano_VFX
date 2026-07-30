using System;

namespace System.Web
{
	/// <summary>Defines the contract that ASP.NET implements to synchronously process HTTP Web requests using custom HTTP handlers.</summary>
	// Token: 0x02000047 RID: 71
	public interface IHttpHandler
	{
		/// <summary>Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.</summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
		// Token: 0x060003C5 RID: 965
		void ProcessRequest(HttpContext context);

		/// <summary>Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.</summary>
		/// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060003C6 RID: 966
		bool IsReusable { get; }
	}
}

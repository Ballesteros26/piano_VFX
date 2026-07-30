using System;

namespace System.Web
{
	/// <summary>Defines the contract that HTTP asynchronous handler objects must implement.</summary>
	// Token: 0x02000046 RID: 70
	public interface IHttpAsyncHandler : IHttpHandler
	{
		/// <summary>Initiates an asynchronous call to the HTTP handler.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process.</returns>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
		/// <param name="cb">The <see cref="T:System.AsyncCallback" /> to call when the asynchronous method call is complete. If <paramref name="cb" /> is null, the delegate is not called. </param>
		/// <param name="extraData">Any extra data needed to process the request. </param>
		// Token: 0x060003C3 RID: 963
		IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData);

		/// <summary>Provides an asynchronous process End method when the process ends.</summary>
		/// <param name="result">An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process. </param>
		// Token: 0x060003C4 RID: 964
		void EndProcessRequest(IAsyncResult result);
	}
}

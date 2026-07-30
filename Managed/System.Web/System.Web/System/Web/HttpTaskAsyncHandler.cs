using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace System.Web
{
	/// <summary>Provides methods that a derived task handler class can implement in order to process an asynchronous task.</summary>
	// Token: 0x020000B8 RID: 184
	public abstract class HttpTaskAsyncHandler : IHttpAsyncHandler, IHttpHandler
	{
		/// <summary>When overridden in a derived class, gets a value that indicates whether the task handler class instance can be reused for another asynchronous task.</summary>
		/// <returns>true if the handler can be reused; otherwise, false.  The default is false.</returns>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, provides code that handles a synchronous task.</summary>
		/// <param name="context">The HTTP context.</param>
		/// <exception cref="T:System.NotSupportedException">The method is implemented but does not provide any default handling for synchronous tasks.</exception>
		// Token: 0x06000A1C RID: 2588 RVA: 0x0001896C File Offset: 0x00016B6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ProcessRequest(HttpContext context)
		{
			throw new NotSupportedException("This handler cannot be executed synchronously.");
		}

		/// <summary>When overridden in a derived class, provides code that handles an asynchronous task.</summary>
		/// <returns>The asynchronous task.</returns>
		/// <param name="context">The HTTP context.</param>
		// Token: 0x06000A1D RID: 2589
		public abstract Task ProcessRequestAsync(HttpContext context);

		/// <summary>Initiates asynchronous processing of a task in an HTTP task handler.</summary>
		/// <returns>An object that contains status data about the asynchronous operation.</returns>
		/// <param name="context">The HTTP context.</param>
		/// <param name="cb">The callback method to invoke when the method returns.</param>
		/// <param name="extraData">Additional data for processing the task.</param>
		// Token: 0x06000A1E RID: 2590 RVA: 0x00018978 File Offset: 0x00016B78
		IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			return TaskAsyncResult.GetAsyncResult(this.ProcessRequestAsync(context), cb, extraData);
		}

		/// <summary>Ends asynchronous processing of a task in an HTTP task handler.</summary>
		/// <param name="result">The status of the asynchronous operation.</param>
		// Token: 0x06000A1F RID: 2591 RVA: 0x00018988 File Offset: 0x00016B88
		void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
		{
			TaskAsyncResult.Wait(result);
		}
	}
}

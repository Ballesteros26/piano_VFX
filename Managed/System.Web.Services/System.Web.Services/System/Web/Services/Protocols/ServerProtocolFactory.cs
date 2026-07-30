using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols
{
	/// <summary>The .NET Framework uses classes that are derived from the <see cref="T:System.Web.Services.Protocols.ServerProtocolFactory" /> class to process XML Web service requests.</summary>
	// Token: 0x02000056 RID: 86
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class ServerProtocolFactory
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00009280 File Offset: 0x00007480
		internal ServerProtocol Create(Type type, HttpContext context, HttpRequest request, HttpResponse response, out bool abortProcessing)
		{
			ServerProtocol serverProtocol = null;
			abortProcessing = false;
			serverProtocol = this.CreateIfRequestCompatible(request);
			ServerProtocol serverProtocol2;
			try
			{
				if (serverProtocol != null)
				{
					serverProtocol.SetContext(type, context, request, response);
				}
				serverProtocol2 = serverProtocol;
			}
			catch (Exception ex)
			{
				abortProcessing = true;
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "Create", ex);
				}
				if (serverProtocol != null && !serverProtocol.WriteException(ex, serverProtocol.Response.OutputStream))
				{
					throw new InvalidOperationException(Res.GetString("UnableToHandleRequest0"), ex);
				}
				serverProtocol2 = null;
			}
			return serverProtocol2;
		}

		/// <summary>Returns a <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> that can be used to process the XML Web service request specified by the <paramref name="request" /> parameter.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> that can be used to process the XML Web service request specified by the <paramref name="request" /> parameter.</returns>
		/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> that represents the Web service request.</param>
		// Token: 0x060001F1 RID: 497
		protected abstract ServerProtocol CreateIfRequestCompatible(HttpRequest request);
	}
}

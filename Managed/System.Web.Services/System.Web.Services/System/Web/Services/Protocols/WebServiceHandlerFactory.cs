using System;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Web.UI;

namespace System.Web.Services.Protocols
{
	/// <summary>Dynamically manufactures Web service handler instances, whose type or types implement the <see cref="T:System.Web.IHttpHandler" /> interface.</summary>
	// Token: 0x0200008F RID: 143
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class WebServiceHandlerFactory : IHttpHandlerFactory
	{
		/// <summary>Returns an <see cref="T:System.Web.IHttpHandler" /> instance.</summary>
		/// <returns>An <see cref="T:System.Web.IHttpHandler" /> instance.</returns>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that provides references to intrinsic server objects (For example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />) used to service HTTP requests.</param>
		/// <param name="verb">The HTTP data transfer method (GET or POST) that the client uses.</param>
		/// <param name="url">The raw URL of the requested resource.</param>
		/// <param name="filePath">The file-system path of the requested resource.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Web.AspNetHostingPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Level="Medium" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003C3 RID: 963 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public IHttpHandler GetHandler(HttpContext context, string verb, string url, string filePath)
		{
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "GetHandler", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter("IHttpHandlerFactory.GetHandler", traceMethod, Tracing.Details(context.Request));
			}
			new AspNetHostingPermission(AspNetHostingPermissionLevel.Minimal).Demand();
			Type compiledType = this.GetCompiledType(url, context);
			IHttpHandler httpHandler = this.CoreGetHandler(compiledType, context, context.Request, context.Response);
			if (Tracing.On)
			{
				Tracing.Exit("IHttpHandlerFactory.GetHandler", traceMethod);
			}
			return httpHandler;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00011DCF File Offset: 0x0000FFCF
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private Type GetCompiledType(string url, HttpContext context)
		{
			return WebServiceParser.GetCompiledType(url, context);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00011DD8 File Offset: 0x0000FFD8
		internal IHttpHandler CoreGetHandler(Type type, HttpContext context, HttpRequest request, HttpResponse response)
		{
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "CoreGetHandler", Array.Empty<object>()) : null);
			ServerProtocolFactory[] serverProtocolFactories = this.GetServerProtocolFactories();
			ServerProtocol serverProtocol = null;
			bool flag = false;
			for (int i = 0; i < serverProtocolFactories.Length; i++)
			{
				try
				{
					serverProtocol = serverProtocolFactories[i].Create(type, context, request, response, out flag);
					if ((serverProtocol != null && serverProtocol.GetType() != typeof(UnsupportedRequestProtocol)) || flag)
					{
						break;
					}
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					throw Tracing.ExceptionThrow(traceMethod, new InvalidOperationException(Res.GetString("FailedToHandleRequest0"), ex));
				}
			}
			if (flag)
			{
				return new NopHandler();
			}
			if (serverProtocol == null)
			{
				if (request.PathInfo != null && request.PathInfo.Length != 0)
				{
					throw Tracing.ExceptionThrow(traceMethod, new InvalidOperationException(Res.GetString("WebUnrecognizedRequestFormatUrl", new object[] { request.PathInfo })));
				}
				throw Tracing.ExceptionThrow(traceMethod, new InvalidOperationException(Res.GetString("WebUnrecognizedRequestFormat")));
			}
			else
			{
				if (serverProtocol is UnsupportedRequestProtocol)
				{
					throw Tracing.ExceptionThrow(traceMethod, new HttpException(((UnsupportedRequestProtocol)serverProtocol).HttpCode, Res.GetString("WebUnrecognizedRequestFormat")));
				}
				bool isAsync = serverProtocol.MethodInfo.IsAsync;
				bool enableSession = serverProtocol.MethodAttribute.EnableSession;
				if (isAsync)
				{
					if (enableSession)
					{
						return new AsyncSessionHandler(serverProtocol);
					}
					return new AsyncSessionlessHandler(serverProtocol);
				}
				else
				{
					if (enableSession)
					{
						return new SyncSessionHandler(serverProtocol);
					}
					return new SyncSessionlessHandler(serverProtocol);
				}
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00011F5C File Offset: 0x0001015C
		[PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
		private ServerProtocolFactory[] GetServerProtocolFactories()
		{
			return WebServicesSection.Current.ServerProtocolFactories;
		}

		/// <summary>Releases the <see cref="T:System.Web.IHttpHandler" /> instance.</summary>
		/// <param name="handler">The <see cref="T:System.Web.IHttpHandler" /> instance to release.</param>
		// Token: 0x060003C7 RID: 967 RVA: 0x0000210D File Offset: 0x0000030D
		public void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}

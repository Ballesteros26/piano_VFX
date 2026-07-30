using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Serves as base class for classes that enable you to customize how ASP.NET routing processes a request.</summary>
	// Token: 0x020004FC RID: 1276
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class UrlRoutingHandler : IHttpHandler
	{
		/// <summary>Gets a value that indicates whether another request can use the <see cref="T:System.Web.Routing.UrlRoutingHandler" /> instance.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x00008A69 File Offset: 0x00006C69
		protected virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the collection of defined routes for the ASP.NET application.</summary>
		/// <returns>An object that contains the routes.</returns>
		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000999EF File Offset: 0x00097BEF
		// (set) Token: 0x06003905 RID: 14597 RVA: 0x00099A0A File Offset: 0x00097C0A
		public RouteCollection RouteCollection
		{
			get
			{
				if (this._routeCollection == null)
				{
					this._routeCollection = RouteTable.Routes;
				}
				return this._routeCollection;
			}
			set
			{
				this._routeCollection = value;
			}
		}

		/// <summary>Processes an HTTP request that matches a route.</summary>
		/// <param name="httpContext">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
		/// <exception cref="T:System.Web.HttpException">The request does not match any route.</exception>
		/// <exception cref="T:System.InvalidOperationException">No handler is defined for the route.</exception>
		// Token: 0x06003906 RID: 14598 RVA: 0x00099A13 File Offset: 0x00097C13
		protected virtual void ProcessRequest(HttpContext httpContext)
		{
			this.ProcessRequest(new HttpContextWrapper(httpContext));
		}

		/// <summary>Processes an HTTP request that matches a route.</summary>
		/// <param name="httpContext">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
		/// <exception cref="T:System.Web.HttpException">The request does not match any route.</exception>
		/// <exception cref="T:System.InvalidOperationException">No handler is defined for the route.</exception>
		// Token: 0x06003907 RID: 14599 RVA: 0x00099A24 File Offset: 0x00097C24
		protected virtual void ProcessRequest(HttpContextBase httpContext)
		{
			RouteData routeData = this.RouteCollection.GetRouteData(httpContext);
			if (routeData == null)
			{
				throw new HttpException(404, global::SR.GetString("The incoming request does not match any route."));
			}
			IRouteHandler routeHandler = routeData.RouteHandler;
			if (routeHandler == null)
			{
				throw new InvalidOperationException(global::SR.GetString("A RouteHandler must be specified for the selected route."));
			}
			RequestContext requestContext = new RequestContext(httpContext, routeData);
			IHttpHandler httpHandler = routeHandler.GetHttpHandler(requestContext);
			if (httpHandler == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route handler '{0}' did not return an IHttpHandler from its GetHttpHandler() method."), routeHandler.GetType()));
			}
			this.VerifyAndProcessRequest(httpHandler, httpContext);
		}

		/// <summary>When overridden in a derived class, validates the HTTP handler and performs the steps that are required to process the request.</summary>
		/// <param name="httpHandler">The object that is used to process an HTTP request.</param>
		/// <param name="httpContext">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
		// Token: 0x06003908 RID: 14600
		protected abstract void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext);

		/// <summary>Gets a value that indicates whether another request can use the <see cref="T:System.Web.Routing.UrlRoutingHandler" /> instance.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x00099AAB File Offset: 0x00097CAB
		bool IHttpHandler.IsReusable
		{
			get
			{
				return this.IsReusable;
			}
		}

		/// <summary>Processes an HTTP request that matches a route.</summary>
		/// <param name="context">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
		// Token: 0x0600390A RID: 14602 RVA: 0x00099AB3 File Offset: 0x00097CB3
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			this.ProcessRequest(context);
		}

		// Token: 0x04001F07 RID: 7943
		private RouteCollection _routeCollection;
	}
}

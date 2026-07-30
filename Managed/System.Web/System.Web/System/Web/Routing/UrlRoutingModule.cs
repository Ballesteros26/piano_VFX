using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web.Security;

namespace System.Web.Routing
{
	/// <summary>Matches a URL request to a defined route.</summary>
	// Token: 0x020004FD RID: 1277
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class UrlRoutingModule : IHttpModule
	{
		/// <summary>Gets or sets the collection of defined routes for the ASP.NET application.</summary>
		/// <returns>An object that contains the routes.</returns>
		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x00099ABC File Offset: 0x00097CBC
		// (set) Token: 0x0600390D RID: 14605 RVA: 0x00099AD7 File Offset: 0x00097CD7
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

		/// <summary>Disposes of the resources (other than memory) that are used by the module.</summary>
		// Token: 0x0600390E RID: 14606 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void Dispose()
		{
		}

		/// <summary>Initializes a module and prepares it to handle requests.</summary>
		/// <param name="application">An object that provides access to the methods, properties, and events common to all application objects in an ASP.NET application.</param>
		// Token: 0x0600390F RID: 14607 RVA: 0x00099AE0 File Offset: 0x00097CE0
		protected virtual void Init(HttpApplication application)
		{
			if (application.Context.Items[UrlRoutingModule._contextKey] != null)
			{
				return;
			}
			application.Context.Items[UrlRoutingModule._contextKey] = UrlRoutingModule._contextKey;
			application.PostResolveRequestCache += this.OnApplicationPostResolveRequestCache;
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x00099B34 File Offset: 0x00097D34
		private void OnApplicationPostResolveRequestCache(object sender, EventArgs e)
		{
			HttpContextBase httpContextBase = new HttpContextWrapper(((HttpApplication)sender).Context);
			this.PostResolveRequestCache(httpContextBase);
		}

		/// <summary>Assigns the HTTP handler for the current request to the context.</summary>
		/// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Routing.RouteData.RouteHandler" /> property for the route is null.</exception>
		// Token: 0x06003911 RID: 14609 RVA: 0x0000393A File Offset: 0x00001B3A
		[Obsolete("This method is obsolete. Override the Init method to use the PostMapRequestHandler event.")]
		public virtual void PostMapRequestHandler(HttpContextBase context)
		{
		}

		/// <summary>Matches the HTTP request to a route, retrieves the handler for that route, and sets the handler as the HTTP handler for the current request.</summary>
		/// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
		// Token: 0x06003912 RID: 14610 RVA: 0x00099B5C File Offset: 0x00097D5C
		public virtual void PostResolveRequestCache(HttpContextBase context)
		{
			RouteData routeData = this.RouteCollection.GetRouteData(context);
			if (routeData == null)
			{
				return;
			}
			IRouteHandler routeHandler = routeData.RouteHandler;
			if (routeHandler == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A RouteHandler must be specified for the selected route."), Array.Empty<object>()));
			}
			if (routeHandler is StopRoutingHandler)
			{
				return;
			}
			RequestContext requestContext = new RequestContext(context, routeData);
			context.Request.RequestContext = requestContext;
			IHttpHandler httpHandler = routeHandler.GetHttpHandler(requestContext);
			if (httpHandler == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route handler '{0}' did not return an IHttpHandler from its GetHttpHandler() method."), routeHandler.GetType()));
			}
			if (!(httpHandler is UrlAuthFailureHandler))
			{
				context.RemapHandler(httpHandler);
				return;
			}
			if (FormsAuthenticationModule.FormsAuthRequired)
			{
				UrlAuthorizationModule.ReportUrlAuthorizationFailure(HttpContext.Current, this);
				return;
			}
			throw new HttpException(401, global::SR.GetString("An error occurred while accessing the resources required to serve this request. You might not have permission to view the requested resources."));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.IHttpModule.Dispose" />.</summary>
		// Token: 0x06003913 RID: 14611 RVA: 0x00099C22 File Offset: 0x00097E22
		void IHttpModule.Dispose()
		{
			this.Dispose();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.IHttpModule.Init" />.</summary>
		/// <param name="application">An object that provides access to the methods, properties, and events that are common to all application objects in an ASP.NET application.</param>
		// Token: 0x06003914 RID: 14612 RVA: 0x00099C2A File Offset: 0x00097E2A
		void IHttpModule.Init(HttpApplication application)
		{
			this.Init(application);
		}

		// Token: 0x04001F08 RID: 7944
		private static readonly object _contextKey = new object();

		// Token: 0x04001F09 RID: 7945
		private static readonly object _requestDataKey = new object();

		// Token: 0x04001F0A RID: 7946
		private RouteCollection _routeCollection;
	}
}

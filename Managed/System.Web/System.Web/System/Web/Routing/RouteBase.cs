using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Serves as the base class for all classes that represent an ASP.NET route.</summary>
	// Token: 0x020004EE RID: 1262
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class RouteBase
	{
		/// <summary>When overridden in a derived class, returns route information about the request.</summary>
		/// <returns>An object that contains the values from the route definition if the route matches the current request, or null if the route does not match the request.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		// Token: 0x0600389E RID: 14494
		public abstract RouteData GetRouteData(HttpContextBase httpContext);

		/// <summary>When overridden in a derived class, checks whether the route matches the specified values, and if so, generates a URL and retrieves information about the route.</summary>
		/// <returns>An object that contains the generated URL and information about the route, or null if the route does not match <paramref name="values" />.</returns>
		/// <param name="requestContext">An object that encapsulates information about the requested route.</param>
		/// <param name="values">An object that contains the parameters for a route.</param>
		// Token: 0x0600389F RID: 14495
		public abstract VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values);

		/// <summary>Gets or sets a value that indicates whether ASP.NET routing should handle URLs that match an existing file.</summary>
		/// <returns>true if ASP.NET routing handles all requests, even those that match an existing file; otherwise, false. The default value is false.</returns>
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x000989E8 File Offset: 0x00096BE8
		// (set) Token: 0x060038A1 RID: 14497 RVA: 0x000989F0 File Offset: 0x00096BF0
		public bool RouteExistingFiles
		{
			get
			{
				return this._routeExistingFiles;
			}
			set
			{
				this._routeExistingFiles = value;
			}
		}

		// Token: 0x04001EF3 RID: 7923
		private bool _routeExistingFiles = true;
	}
}

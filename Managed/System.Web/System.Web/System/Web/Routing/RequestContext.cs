using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Encapsulates information about an HTTP request that matches a defined route.</summary>
	// Token: 0x020004EC RID: 1260
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RequestContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RequestContext" /> class.</summary>
		// Token: 0x06003886 RID: 14470 RVA: 0x00002050 File Offset: 0x00000250
		public RequestContext()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RequestContext" /> class. </summary>
		/// <param name="httpContext">An object that contains information about the HTTP request.</param>
		/// <param name="routeData">An object that contains information about the route that matched the current request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpContext" /> or <paramref name="routeData" /> is null.</exception>
		// Token: 0x06003887 RID: 14471 RVA: 0x000985DC File Offset: 0x000967DC
		public RequestContext(HttpContextBase httpContext, RouteData routeData)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (routeData == null)
			{
				throw new ArgumentNullException("routeData");
			}
			this.HttpContext = httpContext;
			this.RouteData = routeData;
		}

		/// <summary>Gets information about the HTTP request.</summary>
		/// <returns>An object that contains information about the HTTP request.</returns>
		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x0009860E File Offset: 0x0009680E
		// (set) Token: 0x06003889 RID: 14473 RVA: 0x00098616 File Offset: 0x00096816
		public virtual HttpContextBase HttpContext { get; set; }

		/// <summary>Gets information about the requested route.</summary>
		/// <returns>An object that contains information about the requested route.</returns>
		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x0600388A RID: 14474 RVA: 0x0009861F File Offset: 0x0009681F
		// (set) Token: 0x0600388B RID: 14475 RVA: 0x00098627 File Offset: 0x00096827
		public virtual RouteData RouteData { get; set; }
	}
}

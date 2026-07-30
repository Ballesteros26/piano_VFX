using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Enables you to define which HTTP verbs are allowed when ASP.NET routing determines whether a URL matches a route.</summary>
	// Token: 0x020004DF RID: 1247
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpMethodConstraint : IRouteConstraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.HttpMethodConstraint" /> class by using the HTTP verbs that are allowed for the route. </summary>
		/// <param name="allowedMethods">The HTTP verbs that are valid for the route.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="allowedMethods" /> parameter is null.</exception>
		// Token: 0x06003851 RID: 14417 RVA: 0x00097582 File Offset: 0x00095782
		public HttpMethodConstraint(params string[] allowedMethods)
		{
			if (allowedMethods == null)
			{
				throw new ArgumentNullException("allowedMethods");
			}
			this.AllowedMethods = allowedMethods.ToList<string>().AsReadOnly();
		}

		/// <summary>Gets the collection of allowed HTTP verbs for the route.</summary>
		/// <returns>A collection of allowed HTTP verbs for the route.</returns>
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x000975A9 File Offset: 0x000957A9
		// (set) Token: 0x06003853 RID: 14419 RVA: 0x000975B1 File Offset: 0x000957B1
		public ICollection<string> AllowedMethods { get; private set; }

		/// <summary>Determines whether the request was made with an HTTP verb that is one of the allowed verbs for the route.</summary>
		/// <returns>When ASP.NET routing is processing a request, true if the request was made by using an allowed HTTP verb; otherwise, false. When ASP.NET routing is constructing a URL, true if the supplied values contain an HTTP verb that matches one of the allowed HTTP verbs; otherwise, false. The default is true.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		/// <param name="route">The object that is being checked to determine whether it matches the URL.</param>
		/// <param name="parameterName">The name of the parameter that is being checked.</param>
		/// <param name="values">An object that contains the parameters for a route.</param>
		/// <param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is processed or when a URL is generated.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more of the following parameters is null: <paramref name="httpContext" />, <paramref name="route" />, <paramref name="parameterName" />, or <paramref name="values" />.</exception>
		// Token: 0x06003854 RID: 14420 RVA: 0x000975BC File Offset: 0x000957BC
		protected virtual bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (route == null)
			{
				throw new ArgumentNullException("route");
			}
			if (parameterName == null)
			{
				throw new ArgumentNullException("parameterName");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (routeDirection == RouteDirection.IncomingRequest)
			{
				return this.AllowedMethods.Any((string method) => string.Equals(method, httpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase));
			}
			if (routeDirection != RouteDirection.UrlGeneration)
			{
				return true;
			}
			object obj;
			if (!values.TryGetValue(parameterName, out obj))
			{
				return true;
			}
			string parameterValueString = obj as string;
			if (parameterValueString == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The constraint for route parameter '{0}' on the route with URL '{1}' must have a string value in order to use an HttpMethodConstraint."), parameterName, route.Url));
			}
			return this.AllowedMethods.Any((string method) => string.Equals(method, parameterValueString, StringComparison.OrdinalIgnoreCase));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.Routing.IRouteConstraint.Match(System.Web.HttpContextBase,System.Web.Routing.Route,System.String,System.Web.Routing.RouteValueDictionary,System.Web.Routing.RouteDirection)" />. </summary>
		/// <returns>true if the request was made by using an allowed HTTP verb; otherwise, false. The default is true.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		/// <param name="route">The object that is being checked to determine whether it matches the URL.</param>
		/// <param name="parameterName">The name of the parameter that is being checked.</param>
		/// <param name="values">An object that contains the parameters for a route.</param>
		/// <param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is handled or when a URL is generated.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more of the following parameters is null: <paramref name="httpContext" />, <paramref name="route" />, <paramref name="parameterName" />, or <paramref name="values" />.</exception>
		// Token: 0x06003855 RID: 14421 RVA: 0x00097699 File Offset: 0x00095899
		bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return this.Match(httpContext, route, parameterName, values, routeDirection);
		}
	}
}

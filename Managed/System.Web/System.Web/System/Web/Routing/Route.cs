using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace System.Web.Routing
{
	/// <summary>Provides properties and methods for defining a route and for obtaining information about the route.</summary>
	// Token: 0x020004ED RID: 1261
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class Route : RouteBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern and handler class. </summary>
		/// <param name="url">The URL pattern for the route.</param>
		/// <param name="routeHandler">The object that processes requests for the route.</param>
		// Token: 0x0600388C RID: 14476 RVA: 0x00098630 File Offset: 0x00096830
		public Route(string url, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern, default parameter values, and handler class. </summary>
		/// <param name="url">The URL pattern for the route.</param>
		/// <param name="defaults">The values to use for any parameters that are missing in the URL.</param>
		/// <param name="routeHandler">The object that processes requests for the route.</param>
		// Token: 0x0600388D RID: 14477 RVA: 0x00098646 File Offset: 0x00096846
		public Route(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.Defaults = defaults;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern, default parameter values, constraints, and handler class. </summary>
		/// <param name="url">The URL pattern for the route.</param>
		/// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
		/// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
		/// <param name="routeHandler">The object that processes requests for the route.</param>
		// Token: 0x0600388E RID: 14478 RVA: 0x00098663 File Offset: 0x00096863
		public Route(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.Defaults = defaults;
			this.Constraints = constraints;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern, default parameter values, constraints, custom values, and handler class. </summary>
		/// <param name="url">The URL pattern for the route.</param>
		/// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
		/// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
		/// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. These values are passed to the route handler, where they can be used for processing the request.</param>
		/// <param name="routeHandler">The object that processes requests for the route.</param>
		// Token: 0x0600388F RID: 14479 RVA: 0x00098688 File Offset: 0x00096888
		public Route(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.Defaults = defaults;
			this.Constraints = constraints;
			this.DataTokens = dataTokens;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Gets or sets a dictionary of expressions that specify valid values for a URL parameter.</summary>
		/// <returns>An object that contains the parameter names and expressions.</returns>
		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000986B5 File Offset: 0x000968B5
		// (set) Token: 0x06003891 RID: 14481 RVA: 0x000986BD File Offset: 0x000968BD
		public RouteValueDictionary Constraints { get; set; }

		/// <summary>Gets or sets custom values that are passed to the route handler, but which are not used to determine whether the route matches a URL pattern.</summary>
		/// <returns>An object that contains custom values.</returns>
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000986C6 File Offset: 0x000968C6
		// (set) Token: 0x06003893 RID: 14483 RVA: 0x000986CE File Offset: 0x000968CE
		public RouteValueDictionary DataTokens { get; set; }

		/// <summary>Gets or sets the values to use if the URL does not contain all the parameters.</summary>
		/// <returns>An object that contains the parameter names and default values.</returns>
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x000986D7 File Offset: 0x000968D7
		// (set) Token: 0x06003895 RID: 14485 RVA: 0x000986DF File Offset: 0x000968DF
		public RouteValueDictionary Defaults { get; set; }

		/// <summary>Gets or sets the object that processes requests for the route.</summary>
		/// <returns>The object that processes the request.</returns>
		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06003896 RID: 14486 RVA: 0x000986E8 File Offset: 0x000968E8
		// (set) Token: 0x06003897 RID: 14487 RVA: 0x000986F0 File Offset: 0x000968F0
		public IRouteHandler RouteHandler { get; set; }

		/// <summary>Gets or sets the URL pattern for the route.</summary>
		/// <returns>The pattern for matching the route to a URL.</returns>
		/// <exception cref="T:System.ArgumentException">Any of the following:The value starts with ~ or /.The value contains a ? character.The catch-all parameter is not last.</exception>
		/// <exception cref="T:System.Exception">URL segments are not separated by a delimiter or a literal constant.</exception>
		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06003898 RID: 14488 RVA: 0x000986F9 File Offset: 0x000968F9
		// (set) Token: 0x06003899 RID: 14489 RVA: 0x0009870A File Offset: 0x0009690A
		public string Url
		{
			get
			{
				return this._url ?? string.Empty;
			}
			set
			{
				this._parsedRoute = RouteParser.Parse(value);
				this._url = value;
			}
		}

		/// <summary>Returns information about the requested route.</summary>
		/// <returns>An object that contains the values from the route definition.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		// Token: 0x0600389A RID: 14490 RVA: 0x00098720 File Offset: 0x00096920
		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			string text = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
			RouteValueDictionary routeValueDictionary = this._parsedRoute.Match(text, this.Defaults);
			if (routeValueDictionary == null)
			{
				return null;
			}
			RouteData routeData = new RouteData(this, this.RouteHandler);
			if (!this.ProcessConstraints(httpContext, routeValueDictionary, RouteDirection.IncomingRequest))
			{
				return null;
			}
			foreach (KeyValuePair<string, object> keyValuePair in routeValueDictionary)
			{
				routeData.Values.Add(keyValuePair.Key, keyValuePair.Value);
			}
			if (this.DataTokens != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair2 in this.DataTokens)
				{
					routeData.DataTokens[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			return routeData;
		}

		/// <summary>Returns information about the URL that is associated with the route.</summary>
		/// <returns>An object that contains information about the URL that is associated with the route.</returns>
		/// <param name="requestContext">An object that encapsulates information about the requested route.</param>
		/// <param name="values">An object that contains the parameters for a route.</param>
		// Token: 0x0600389B RID: 14491 RVA: 0x00098834 File Offset: 0x00096A34
		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			BoundUrl boundUrl = this._parsedRoute.Bind(requestContext.RouteData.Values, values, this.Defaults, this.Constraints);
			if (boundUrl == null)
			{
				return null;
			}
			if (!this.ProcessConstraints(requestContext.HttpContext, boundUrl.Values, RouteDirection.UrlGeneration))
			{
				return null;
			}
			VirtualPathData virtualPathData = new VirtualPathData(this, boundUrl.Url);
			if (this.DataTokens != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.DataTokens)
				{
					virtualPathData.DataTokens[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return virtualPathData;
		}

		/// <summary>Determines whether a parameter value matches the constraint for that parameter.</summary>
		/// <returns>true if the parameter value matches the constraint; otherwise, false.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		/// <param name="constraint">The regular expression or object to use to test <paramref name="parameterName" />.</param>
		/// <param name="parameterName">The name of the parameter to test.</param>
		/// <param name="values">The values to test.</param>
		/// <param name="routeDirection">A value that specifies whether URL routing is processing an incoming request or constructing a URL.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="constraint" /> is not a string that contains a regular expression.</exception>
		// Token: 0x0600389C RID: 14492 RVA: 0x000988F0 File Offset: 0x00096AF0
		protected virtual bool ProcessConstraint(HttpContextBase httpContext, object constraint, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			IRouteConstraint routeConstraint = constraint as IRouteConstraint;
			if (routeConstraint != null)
			{
				return routeConstraint.Match(httpContext, this, parameterName, values, routeDirection);
			}
			string text = constraint as string;
			if (text == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The constraint entry '{0}' on the route with URL '{1}' must have a string value or be of a type which implements IRouteConstraint."), parameterName, this.Url));
			}
			object obj;
			values.TryGetValue(parameterName, out obj);
			string text2 = Convert.ToString(obj, CultureInfo.InvariantCulture);
			string text3 = "^(" + text + ")$";
			return Regex.IsMatch(text2, text3, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x00098974 File Offset: 0x00096B74
		private bool ProcessConstraints(HttpContextBase httpContext, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (this.Constraints != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.Constraints)
				{
					if (!this.ProcessConstraint(httpContext, keyValuePair.Value, keyValuePair.Key, values, routeDirection))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x04001EEC RID: 7916
		private const string HttpMethodParameterName = "httpMethod";

		// Token: 0x04001EED RID: 7917
		private string _url;

		// Token: 0x04001EEE RID: 7918
		private ParsedRoute _parsedRoute;
	}
}

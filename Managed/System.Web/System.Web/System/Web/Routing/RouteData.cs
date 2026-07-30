using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Encapsulates information about a route.</summary>
	// Token: 0x020004F3 RID: 1267
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteData" /> class. </summary>
		// Token: 0x060038C9 RID: 14537 RVA: 0x00099105 File Offset: 0x00097305
		public RouteData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteData" /> class by using the specified route and route handler. </summary>
		/// <param name="route">An object that defines the route.</param>
		/// <param name="routeHandler">An object that processes the request.</param>
		// Token: 0x060038CA RID: 14538 RVA: 0x00099123 File Offset: 0x00097323
		public RouteData(RouteBase route, IRouteHandler routeHandler)
		{
			this.Route = route;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Gets a collection of custom values that are passed to the route handler but are not used when ASP.NET routing determines whether the route matches a request.</summary>
		/// <returns>An object that contains custom values.</returns>
		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060038CB RID: 14539 RVA: 0x0009914F File Offset: 0x0009734F
		public RouteValueDictionary DataTokens
		{
			get
			{
				return this._dataTokens;
			}
		}

		/// <summary>Gets or sets the object that represents a route.</summary>
		/// <returns>An object that represents the route definition.</returns>
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x00099157 File Offset: 0x00097357
		// (set) Token: 0x060038CD RID: 14541 RVA: 0x0009915F File Offset: 0x0009735F
		public RouteBase Route { get; set; }

		/// <summary>Gets or sets the object that processes a requested route.</summary>
		/// <returns>An object that processes the route request.</returns>
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x00099168 File Offset: 0x00097368
		// (set) Token: 0x060038CF RID: 14543 RVA: 0x00099170 File Offset: 0x00097370
		public IRouteHandler RouteHandler
		{
			get
			{
				return this._routeHandler;
			}
			set
			{
				this._routeHandler = value;
			}
		}

		/// <summary>Gets a collection of URL parameter values and default values for the route.</summary>
		/// <returns>An object that contains values that are parsed from the URL and from default values.</returns>
		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x00099179 File Offset: 0x00097379
		public RouteValueDictionary Values
		{
			get
			{
				return this._values;
			}
		}

		/// <summary>Retrieves the value with the specified identifier.</summary>
		/// <returns>The element in the <see cref="P:System.Web.Routing.RouteData.Values" /> property whose key matches <paramref name="valueName" />.</returns>
		/// <param name="valueName">The key of the value to retrieve.</param>
		/// <exception cref="T:System.InvalidOperationException">A value does not exist for <paramref name="valueName" />.</exception>
		// Token: 0x060038D1 RID: 14545 RVA: 0x00099184 File Offset: 0x00097384
		public string GetRequiredString(string valueName)
		{
			object obj;
			if (this.Values.TryGetValue(valueName, out obj))
			{
				string text = obj as string;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The RouteData must contain an item named '{0}' with a non-empty string value."), valueName));
		}

		// Token: 0x04001EFC RID: 7932
		private IRouteHandler _routeHandler;

		// Token: 0x04001EFD RID: 7933
		private RouteValueDictionary _values = new RouteValueDictionary();

		// Token: 0x04001EFE RID: 7934
		private RouteValueDictionary _dataTokens = new RouteValueDictionary();
	}
}

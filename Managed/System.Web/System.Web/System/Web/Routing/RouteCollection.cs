using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web.Hosting;
using System.Web.UI;

namespace System.Web.Routing
{
	/// <summary>Provides a collection of routes for ASP.NET routing.</summary>
	// Token: 0x020004EF RID: 1263
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteCollection : Collection<RouteBase>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteCollection" /> class. </summary>
		// Token: 0x060038A3 RID: 14499 RVA: 0x00098A08 File Offset: 0x00096C08
		public RouteCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteCollection" /> class by using the specified virtual path provider. </summary>
		/// <param name="virtualPathProvider">A provider for retrieving resources from a virtual file system.</param>
		// Token: 0x060038A4 RID: 14500 RVA: 0x00098A2B File Offset: 0x00096C2B
		public RouteCollection(VirtualPathProvider virtualPathProvider)
		{
			this.VPP = virtualPathProvider;
		}

		/// <summary>Gets or sets a value that indicates whether trailing slashes are added when virtual paths are normalized.</summary>
		/// <returns>true if trailing slashes are added; otherwise, false. The default is false.</returns>
		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x00098A55 File Offset: 0x00096C55
		// (set) Token: 0x060038A6 RID: 14502 RVA: 0x00098A5D File Offset: 0x00096C5D
		public bool AppendTrailingSlash { get; set; }

		/// <summary>Gets or sets a value that indicates whether URLs are converted to lower case when virtual paths are normalized.</summary>
		/// <returns>true to convert URLs to lower case; otherwise false. The default is false.</returns>
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x00098A66 File Offset: 0x00096C66
		// (set) Token: 0x060038A8 RID: 14504 RVA: 0x00098A6E File Offset: 0x00096C6E
		public bool LowercaseUrls { get; set; }

		/// <summary>Gets or sets a value that indicates whether ASP.NET routing should handle URLs that match an existing file.</summary>
		/// <returns>true if ASP.NET routing handles all requests, even those that match an existing file; otherwise, false. The default value is false.</returns>
		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x00098A77 File Offset: 0x00096C77
		// (set) Token: 0x060038AA RID: 14506 RVA: 0x00098A7F File Offset: 0x00096C7F
		public bool RouteExistingFiles { get; set; }

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x00098A88 File Offset: 0x00096C88
		// (set) Token: 0x060038AC RID: 14508 RVA: 0x00098A9E File Offset: 0x00096C9E
		private VirtualPathProvider VPP
		{
			get
			{
				if (this._vpp == null)
				{
					return HostingEnvironment.VirtualPathProvider;
				}
				return this._vpp;
			}
			set
			{
				this._vpp = value;
			}
		}

		/// <summary>Gets the route in the collection that has the specified name.</summary>
		/// <returns>An object that has the specified name, or null if <paramref name="name" /> is null, is an empty string, or does not match any route in the collection.</returns>
		/// <param name="name">The value that identifies the route to get.</param>
		// Token: 0x170011AA RID: 4522
		public RouteBase this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
				{
					return null;
				}
				RouteBase routeBase;
				if (this._namedMap.TryGetValue(name, out routeBase))
				{
					return routeBase;
				}
				return null;
			}
		}

		/// <summary>Adds a route to the end of the <see cref="T:System.Web.Routing.RouteCollection" /> object and assigns the specified name to the route.</summary>
		/// <param name="name">The value that identifies the route. The value can be null or an empty string.</param>
		/// <param name="item">The route to add to the end of the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is already used in the collection.</exception>
		// Token: 0x060038AE RID: 14510 RVA: 0x00098AD4 File Offset: 0x00096CD4
		public void Add(string name, RouteBase item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (!string.IsNullOrEmpty(name) && this._namedMap.ContainsKey(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("A route named '{0}' is already in the route collection. Route names must be unique."), name), "name");
			}
			base.Add(item);
			if (!string.IsNullOrEmpty(name))
			{
				this._namedMap[name] = item;
			}
			Route route = item as Route;
			if (route != null && route.RouteHandler != null)
			{
				TelemetryLogger.LogHttpHandler(route.RouteHandler.GetType());
			}
		}

		/// <summary>Provides a way to define routes for Web Forms applications.</summary>
		/// <returns>The route that is added to the route collection.</returns>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeUrl">The URL pattern for the route.</param>
		/// <param name="physicalFile">The physical URL for the route.</param>
		// Token: 0x060038AF RID: 14511 RVA: 0x00098B63 File Offset: 0x00096D63
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile)
		{
			return this.MapPageRoute(routeName, routeUrl, physicalFile, true, null, null, null);
		}

		/// <summary>Provides a way to define routes for Web Forms applications.</summary>
		/// <returns>The route that is added to the route collection.</returns>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeUrl">The URL pattern for the route.</param>
		/// <param name="physicalFile">The physical URL for the route.</param>
		/// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
		// Token: 0x060038B0 RID: 14512 RVA: 0x00098B72 File Offset: 0x00096D72
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess)
		{
			return this.MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, null, null, null);
		}

		/// <summary>Provides a way to define routes for Web Forms applications.</summary>
		/// <returns>The route that is added to the route collection.</returns>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeUrl">The URL pattern for the route.</param>
		/// <param name="physicalFile">The physical URL for the route.</param>
		/// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
		/// <param name="defaults">Default values for the route parameters.</param>
		// Token: 0x060038B1 RID: 14513 RVA: 0x00098B82 File Offset: 0x00096D82
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults)
		{
			return this.MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, defaults, null, null);
		}

		/// <summary>Provides a way to define routes for Web Forms applications.</summary>
		/// <returns>The route that is added to the route collection.</returns>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeUrl">The URL pattern for the route.</param>
		/// <param name="physicalFile">The physical URL for the route.</param>
		/// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
		/// <param name="defaults">Default values for the route.</param>
		/// <param name="constraints">Constraints that a URL request must meet in order to be processed as this route.</param>
		// Token: 0x060038B2 RID: 14514 RVA: 0x00098B93 File Offset: 0x00096D93
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints)
		{
			return this.MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, defaults, constraints, null);
		}

		/// <summary>Provides a way to define routes for Web Forms applications.</summary>
		/// <returns>The route that is added to the route collection.</returns>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeUrl">The URL pattern for the route.</param>
		/// <param name="physicalFile">The physical URL for the route.</param>
		/// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
		/// <param name="defaults">Default values for the route parameters.</param>
		/// <param name="constraints">Constraints that a URL request must meet in order to be processed as this route.</param>
		/// <param name="dataTokens">Values that are associated with the route that are not used to determine whether a route matches a URL pattern.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="routeUrl" /> parameter is null.</exception>
		// Token: 0x060038B3 RID: 14515 RVA: 0x00098BA8 File Offset: 0x00096DA8
		public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
		{
			if (routeUrl == null)
			{
				throw new ArgumentNullException("routeUrl");
			}
			Route route = new Route(routeUrl, defaults, constraints, dataTokens, new PageRouteHandler(physicalFile, checkPhysicalUrlAccess));
			this.Add(routeName, route);
			return route;
		}

		/// <summary>Removes all the elements from the <see cref="T:System.Web.Routing.RouteCollection" /> object.</summary>
		// Token: 0x060038B4 RID: 14516 RVA: 0x00098BE1 File Offset: 0x00096DE1
		protected override void ClearItems()
		{
			this._namedMap.Clear();
			base.ClearItems();
		}

		/// <summary>Provides an object for managing thread safety when you retrieve an object from the collection.</summary>
		/// <returns>An object that manages thread safety.</returns>
		// Token: 0x060038B5 RID: 14517 RVA: 0x00098BF4 File Offset: 0x00096DF4
		public IDisposable GetReadLock()
		{
			this._rwLock.EnterReadLock();
			return new RouteCollection.ReadLockDisposable(this._rwLock);
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x00098C0C File Offset: 0x00096E0C
		private RequestContext GetRequestContext(RequestContext requestContext)
		{
			if (requestContext != null)
			{
				return requestContext;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				throw new InvalidOperationException(global::SR.GetString("HttpContext.Current must be non-null when a RequestContext is not provided."));
			}
			return new RequestContext(new HttpContextWrapper(httpContext), new RouteData());
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x00098C3C File Offset: 0x00096E3C
		private bool IsRouteToExistingFile(HttpContextBase httpContext)
		{
			string appRelativeCurrentExecutionFilePath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
			return appRelativeCurrentExecutionFilePath != "~/" && this.VPP != null && (this.VPP.FileExists(appRelativeCurrentExecutionFilePath) || this.VPP.DirectoryExists(appRelativeCurrentExecutionFilePath));
		}

		/// <summary>Returns information about the route in the collection that matches the specified values.</summary>
		/// <returns>An object that contains the values from the route definition.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.HttpContextBase.Request" /> property of the object in the <paramref name="context" /> parameter is null.</exception>
		// Token: 0x060038B8 RID: 14520 RVA: 0x00098C88 File Offset: 0x00096E88
		public RouteData GetRouteData(HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (httpContext.Request == null)
			{
				throw new ArgumentException(global::SR.GetString("The context does not contain any request data."), "httpContext");
			}
			if (base.Count == 0)
			{
				return null;
			}
			bool flag = false;
			bool flag2 = false;
			if (!this.RouteExistingFiles)
			{
				flag = this.IsRouteToExistingFile(httpContext);
				flag2 = true;
				if (flag)
				{
					return null;
				}
			}
			using (this.GetReadLock())
			{
				foreach (RouteBase routeBase in this)
				{
					RouteData routeData = routeBase.GetRouteData(httpContext);
					if (routeData != null)
					{
						if (!routeBase.RouteExistingFiles)
						{
							if (!flag2)
							{
								flag = this.IsRouteToExistingFile(httpContext);
							}
							if (flag)
							{
								return null;
							}
						}
						return routeData;
					}
				}
			}
			return null;
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x00098D70 File Offset: 0x00096F70
		private string NormalizeVirtualPath(RequestContext requestContext, string virtualPath)
		{
			string text = Util.GetUrlWithApplicationPath(requestContext.HttpContext, virtualPath);
			if (this.LowercaseUrls || this.AppendTrailingSlash)
			{
				int num = text.IndexOfAny(new char[] { '?', '#' });
				string text2;
				string text3;
				if (num >= 0)
				{
					text2 = text.Substring(0, num);
					text3 = text.Substring(num);
				}
				else
				{
					text2 = text;
					text3 = "";
				}
				if (this.LowercaseUrls)
				{
					text2 = text2.ToLowerInvariant();
				}
				if (this.AppendTrailingSlash && !text2.EndsWith("/"))
				{
					text2 += "/";
				}
				text = text2 + text3;
			}
			return text;
		}

		/// <summary>Returns information about the URL path that is associated with the route, given the specified context and parameter values.</summary>
		/// <returns>An object that contains information about the URL path that is associated with the route.</returns>
		/// <param name="requestContext">An object that encapsulates information about the requested route.</param>
		/// <param name="values">An object that contains the parameters for a route.</param>
		// Token: 0x060038BA RID: 14522 RVA: 0x00098E0C File Offset: 0x0009700C
		public VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			requestContext = this.GetRequestContext(requestContext);
			using (this.GetReadLock())
			{
				foreach (RouteBase routeBase in this)
				{
					VirtualPathData virtualPath = routeBase.GetVirtualPath(requestContext, values);
					if (virtualPath != null)
					{
						virtualPath.VirtualPath = this.NormalizeVirtualPath(requestContext, virtualPath.VirtualPath);
						return virtualPath;
					}
				}
			}
			return null;
		}

		/// <summary>Returns information about the URL path that is associated with the named route, given the specified context, route name, and parameter values.</summary>
		/// <returns>An object that contains information about the URL path that is associated with the route.</returns>
		/// <param name="requestContext">An object that encapsulates information about the requested route.</param>
		/// <param name="name">The name of the route to use when information about the URL path is retrieved.</param>
		/// <param name="values">An object that contains the parameters for a route.</param>
		/// <exception cref="T:System.ArgumentException">No route could be found that has the name specified in the <paramref name="name" /> parameter.</exception>
		// Token: 0x060038BB RID: 14523 RVA: 0x00098E98 File Offset: 0x00097098
		public VirtualPathData GetVirtualPath(RequestContext requestContext, string name, RouteValueDictionary values)
		{
			requestContext = this.GetRequestContext(requestContext);
			if (string.IsNullOrEmpty(name))
			{
				return this.GetVirtualPath(requestContext, values);
			}
			RouteBase routeBase;
			bool flag;
			using (this.GetReadLock())
			{
				flag = this._namedMap.TryGetValue(name, out routeBase);
			}
			if (!flag)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("A route named '{0}' could not be found in the route collection."), name), "name");
			}
			VirtualPathData virtualPath = routeBase.GetVirtualPath(requestContext, values);
			if (virtualPath != null)
			{
				virtualPath.VirtualPath = this.NormalizeVirtualPath(requestContext, virtualPath.VirtualPath);
				return virtualPath;
			}
			return null;
		}

		/// <summary>Provides an object for managing thread safety when you add or remove elements in the collection.</summary>
		/// <returns>An object that manages thread safety.</returns>
		// Token: 0x060038BC RID: 14524 RVA: 0x00098F38 File Offset: 0x00097138
		public IDisposable GetWriteLock()
		{
			this._rwLock.EnterWriteLock();
			return new RouteCollection.WriteLockDisposable(this._rwLock);
		}

		/// <summary>Defines a URL pattern that should not be checked for matches against routes.</summary>
		/// <param name="url">The URL pattern to be ignored.</param>
		// Token: 0x060038BD RID: 14525 RVA: 0x00098F50 File Offset: 0x00097150
		public void Ignore(string url)
		{
			this.Ignore(url, null);
		}

		/// <summary>Defines a URL pattern that should not be checked for matches against routes if a request URL meets the specified constraints.</summary>
		/// <param name="url">The URL pattern to be ignored.</param>
		/// <param name="constraints">Additional criteria that determine whether a request that matches the URL pattern will be ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> parameter is null.</exception>
		// Token: 0x060038BE RID: 14526 RVA: 0x00098F5C File Offset: 0x0009715C
		public void Ignore(string url, object constraints)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			RouteCollection.IgnoreRouteInternal ignoreRouteInternal = new RouteCollection.IgnoreRouteInternal(url)
			{
				Constraints = new RouteValueDictionary(constraints)
			};
			base.Add(ignoreRouteInternal);
		}

		/// <summary>Inserts the specified route into the <see cref="T:System.Web.Routing.RouteCollection" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> is inserted.</param>
		/// <param name="item">The route to insert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is already in the collection.</exception>
		// Token: 0x060038BF RID: 14527 RVA: 0x00098F94 File Offset: 0x00097194
		protected override void InsertItem(int index, RouteBase item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (base.Contains(item))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The route provided already exists in the route collection. The collection may not contain duplicate routes."), Array.Empty<object>()), "item");
			}
			base.InsertItem(index, item);
		}

		/// <summary>Removes the route from the <see cref="T:System.Web.Routing.RouteCollection" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the route to remove.</param>
		// Token: 0x060038C0 RID: 14528 RVA: 0x00098FE4 File Offset: 0x000971E4
		protected override void RemoveItem(int index)
		{
			this.RemoveRouteName(index);
			base.RemoveItem(index);
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x00098FF4 File Offset: 0x000971F4
		private void RemoveRouteName(int index)
		{
			RouteBase routeBase = base[index];
			foreach (KeyValuePair<string, RouteBase> keyValuePair in this._namedMap)
			{
				if (keyValuePair.Value == routeBase)
				{
					this._namedMap.Remove(keyValuePair.Key);
					break;
				}
			}
		}

		/// <summary>Replaces the route at the specified index.</summary>
		/// <param name="index">The zero-based index of the route to replace.</param>
		/// <param name="item">The route to add at the specified index.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is already in the collection.</exception>
		// Token: 0x060038C2 RID: 14530 RVA: 0x00099068 File Offset: 0x00097268
		protected override void SetItem(int index, RouteBase item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (base.Contains(item))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The route provided already exists in the route collection. The collection may not contain duplicate routes."), Array.Empty<object>()), "item");
			}
			this.RemoveRouteName(index);
			base.SetItem(index, item);
		}

		// Token: 0x04001EF4 RID: 7924
		private Dictionary<string, RouteBase> _namedMap = new Dictionary<string, RouteBase>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001EF5 RID: 7925
		private VirtualPathProvider _vpp;

		// Token: 0x04001EF6 RID: 7926
		private ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

		// Token: 0x020004F0 RID: 1264
		private class ReadLockDisposable : IDisposable
		{
			// Token: 0x060038C3 RID: 14531 RVA: 0x000990BF File Offset: 0x000972BF
			public ReadLockDisposable(ReaderWriterLockSlim rwLock)
			{
				this._rwLock = rwLock;
			}

			// Token: 0x060038C4 RID: 14532 RVA: 0x000990CE File Offset: 0x000972CE
			void IDisposable.Dispose()
			{
				this._rwLock.ExitReadLock();
			}

			// Token: 0x04001EFA RID: 7930
			private ReaderWriterLockSlim _rwLock;
		}

		// Token: 0x020004F1 RID: 1265
		private class WriteLockDisposable : IDisposable
		{
			// Token: 0x060038C5 RID: 14533 RVA: 0x000990DB File Offset: 0x000972DB
			public WriteLockDisposable(ReaderWriterLockSlim rwLock)
			{
				this._rwLock = rwLock;
			}

			// Token: 0x060038C6 RID: 14534 RVA: 0x000990EA File Offset: 0x000972EA
			void IDisposable.Dispose()
			{
				this._rwLock.ExitWriteLock();
			}

			// Token: 0x04001EFB RID: 7931
			private ReaderWriterLockSlim _rwLock;
		}

		// Token: 0x020004F2 RID: 1266
		private sealed class IgnoreRouteInternal : Route
		{
			// Token: 0x060038C7 RID: 14535 RVA: 0x000990F7 File Offset: 0x000972F7
			public IgnoreRouteInternal(string url)
				: base(url, new StopRoutingHandler())
			{
			}

			// Token: 0x060038C8 RID: 14536 RVA: 0x00003BEA File Offset: 0x00001DEA
			public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary routeValues)
			{
				return null;
			}
		}
	}
}

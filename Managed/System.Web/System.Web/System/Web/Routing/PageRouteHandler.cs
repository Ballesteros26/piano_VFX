using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Compilation;
using System.Web.Security;
using System.Web.UI;

namespace System.Web.Routing
{
	/// <summary>Provides properties and methods for defining how a URL maps to a physical file.</summary>
	// Token: 0x020004E5 RID: 1253
	public class PageRouteHandler : IRouteHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.PageRouteHandler" /> class. </summary>
		/// <param name="virtualPath">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="virtualPath" /> parameter is null or is an empty string or does not start with "~/".</exception>
		// Token: 0x0600385F RID: 14431 RVA: 0x000976F0 File Offset: 0x000958F0
		public PageRouteHandler(string virtualPath)
			: this(virtualPath, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.PageRouteHandler" /> class. </summary>
		/// <param name="virtualPath">The virtual path of the physical file of this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
		/// <param name="checkPhysicalUrlAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="virtualPath" /> parameter is null or is an empty string or does not start with "~/".</exception>
		// Token: 0x06003860 RID: 14432 RVA: 0x000976FC File Offset: 0x000958FC
		public PageRouteHandler(string virtualPath, bool checkPhysicalUrlAccess)
		{
			if (string.IsNullOrEmpty(virtualPath) || !virtualPath.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(global::SR.GetString("VirtualPath must be a non-empty string starting with ~/."), "virtualPath");
			}
			this.VirtualPath = virtualPath;
			this.CheckPhysicalUrlAccess = checkPhysicalUrlAccess;
			this._useRouteVirtualPath = this.VirtualPath.Contains("{");
		}

		/// <summary>Gets the virtual path of the Web page that is associated with this route.</summary>
		/// <returns>The URL of the Web page, before substitutions have been applied for any replacement parameters.</returns>
		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x0009775E File Offset: 0x0009595E
		// (set) Token: 0x06003862 RID: 14434 RVA: 0x00097766 File Offset: 0x00095966
		public string VirtualPath { get; private set; }

		/// <summary>Gets a value that determines whether authorization rules are applied to the physical file's URL.</summary>
		/// <returns>true if authorization is checked for the URL of the physical file that is associated with the route; otherwise, false. The default is true.</returns>
		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x0009776F File Offset: 0x0009596F
		// (set) Token: 0x06003864 RID: 14436 RVA: 0x00097777 File Offset: 0x00095977
		public bool CheckPhysicalUrlAccess { get; private set; }

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x00097780 File Offset: 0x00095980
		private Route RouteVirtualPath
		{
			get
			{
				if (this._routeVirtualPath == null)
				{
					this._routeVirtualPath = new Route(this.VirtualPath.Substring(2), this);
				}
				return this._routeVirtualPath;
			}
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000977A8 File Offset: 0x000959A8
		private bool CheckUrlAccess(string virtualPath, RequestContext requestContext)
		{
			IPrincipal principal = requestContext.HttpContext.User;
			if (principal == null)
			{
				principal = new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), new string[0]);
			}
			return this.CheckUrlAccessWithAssert(virtualPath, requestContext, principal);
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000977E8 File Offset: 0x000959E8
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private bool CheckUrlAccessWithAssert(string virtualPath, RequestContext requestContext, IPrincipal user)
		{
			return UrlAuthorizationModule.CheckUrlAccessForPrincipal(virtualPath, user, requestContext.HttpContext.Request.HttpMethod);
		}

		/// <summary>Returns the object that processes the request.</summary>
		/// <returns>The object that processes the request.</returns>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext" /> parameter is null.</exception>
		// Token: 0x06003868 RID: 14440 RVA: 0x00097804 File Offset: 0x00095A04
		public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			string text = this.GetSubstitutedVirtualPath(requestContext);
			int num = text.IndexOf('?');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			if (this.CheckPhysicalUrlAccess && !this.CheckUrlAccess(text, requestContext))
			{
				return new UrlAuthFailureHandler();
			}
			return BuildManager.CreateInstanceFromVirtualPath(text, typeof(Page)) as Page;
		}

		/// <summary>Returns the virtual path of the physical file for the route after substitutions have been applied to any replacement parameters.</summary>
		/// <returns>The URL of the physical file that was generated from a route.</returns>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext" /> parameter is null.</exception>
		// Token: 0x06003869 RID: 14441 RVA: 0x0009786C File Offset: 0x00095A6C
		public string GetSubstitutedVirtualPath(RequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (!this._useRouteVirtualPath)
			{
				return this.VirtualPath;
			}
			VirtualPathData virtualPath = this.RouteVirtualPath.GetVirtualPath(requestContext, requestContext.RouteData.Values);
			if (virtualPath == null)
			{
				return this.VirtualPath;
			}
			return "~/" + virtualPath.VirtualPath;
		}

		// Token: 0x04001EDD RID: 7901
		private bool _useRouteVirtualPath;

		// Token: 0x04001EDE RID: 7902
		private Route _routeVirtualPath;
	}
}

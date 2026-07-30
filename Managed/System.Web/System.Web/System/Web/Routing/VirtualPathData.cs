using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Represents information about the route and virtual path that are the result of generating a URL with the ASP.NET routing framework.</summary>
	// Token: 0x020004FE RID: 1278
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class VirtualPathData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.VirtualPathData" /> class. </summary>
		/// <param name="route">The object that is used to generate the URL.</param>
		/// <param name="virtualPath">The generated URL.</param>
		// Token: 0x06003917 RID: 14615 RVA: 0x00099C49 File Offset: 0x00097E49
		public VirtualPathData(RouteBase route, string virtualPath)
		{
			this.Route = route;
			this.VirtualPath = virtualPath;
		}

		/// <summary>Gets the collection of custom values for the route definition.</summary>
		/// <returns>A collection of custom values for a route.</returns>
		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06003918 RID: 14616 RVA: 0x00099C6A File Offset: 0x00097E6A
		public RouteValueDictionary DataTokens
		{
			get
			{
				return this._dataTokens;
			}
		}

		/// <summary>Gets or sets the route that is used to create the URL.</summary>
		/// <returns>An object that represents the route that matched the parameters that were used to generate a URL.</returns>
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06003919 RID: 14617 RVA: 0x00099C72 File Offset: 0x00097E72
		// (set) Token: 0x0600391A RID: 14618 RVA: 0x00099C7A File Offset: 0x00097E7A
		public RouteBase Route { get; set; }

		/// <summary>Gets or sets the URL that was created from the route definition.</summary>
		/// <returns>The URL that was generated from a route.</returns>
		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x0600391B RID: 14619 RVA: 0x00099C83 File Offset: 0x00097E83
		// (set) Token: 0x0600391C RID: 14620 RVA: 0x00099C94 File Offset: 0x00097E94
		public string VirtualPath
		{
			get
			{
				return this._virtualPath ?? string.Empty;
			}
			set
			{
				this._virtualPath = value;
			}
		}

		// Token: 0x04001F0B RID: 7947
		private string _virtualPath;

		// Token: 0x04001F0C RID: 7948
		private RouteValueDictionary _dataTokens = new RouteValueDictionary();
	}
}

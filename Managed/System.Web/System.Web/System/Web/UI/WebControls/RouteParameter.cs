using System;
using System.ComponentModel;
using System.Data;
using System.Web.Routing;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of a URL segment to a parameter object.</summary>
	// Token: 0x02000404 RID: 1028
	[DefaultProperty("RouteKey")]
	public class RouteParameter : Parameter
	{
		/// <summary>Gets or sets the name of the route segment from which to retrieve the value for the route parameter.</summary>
		/// <returns>The name of the route segment that contains the value for the parameter.</returns>
		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x00078DD0 File Offset: 0x00076FD0
		// (set) Token: 0x06002D9A RID: 11674 RVA: 0x00078DD8 File Offset: 0x00076FD8
		[DefaultValue("")]
		public string RouteKey
		{
			get
			{
				return this.routeKey;
			}
			set
			{
				this.routeKey = value ?? string.Empty;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class. </summary>
		// Token: 0x06002D9B RID: 11675 RVA: 0x00078DEA File Offset: 0x00076FEA
		public RouteParameter()
		{
			this.RouteKey = string.Empty;
			base.Name = string.Empty;
			base.Type = TypeCode.Empty;
			base.Direction = ParameterDirection.Input;
			base.DefaultValue = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the values of the specified instance. </summary>
		/// <param name="original">An object from which the current instance is initialized.</param>
		// Token: 0x06002D9C RID: 11676 RVA: 0x00078E1D File Offset: 0x0007701D
		protected RouteParameter(RouteParameter original)
			: base(original)
		{
			this.RouteKey = original.RouteKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the specified name for the parameter and the specified key for route data. </summary>
		/// <param name="name">The name of the parameter instance.</param>
		/// <param name="routeKey">The name of the route segment that contains the value for the parameter.</param>
		// Token: 0x06002D9D RID: 11677 RVA: 0x00078E32 File Offset: 0x00077032
		public RouteParameter(string name, string routeKey)
			: base(name)
		{
			this.RouteKey = routeKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the specified name and database type for the parameter, and by using the specified key for the route data. </summary>
		/// <param name="name">The name of the parameter instance.</param>
		/// <param name="dbType">The database type of the parameter instance.</param>
		/// <param name="routeKey">The name of the route segment that contains the value for the parameter.</param>
		// Token: 0x06002D9E RID: 11678 RVA: 0x00078E42 File Offset: 0x00077042
		public RouteParameter(string name, DbType dbType, string routeKey)
			: base(name, dbType)
		{
			this.RouteKey = routeKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the specified name and type for the parameter, and by using the specified key for the route data. </summary>
		/// <param name="name">The name of the parameter instance.</param>
		/// <param name="type">The type that the parameter represents.</param>
		/// <param name="routeKey">The name of the route segment that contains the value for the parameter.</param>
		// Token: 0x06002D9F RID: 11679 RVA: 0x00078E53 File Offset: 0x00077053
		public RouteParameter(string name, TypeCode type, string routeKey)
			: base(name, type)
		{
			this.RouteKey = routeKey;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.RouteParameter" /> instance.</summary>
		/// <returns>An object that is a duplicate of the current one.</returns>
		// Token: 0x06002DA0 RID: 11680 RVA: 0x00078E64 File Offset: 0x00077064
		protected override Parameter Clone()
		{
			return new RouteParameter(this);
		}

		/// <summary>Evaluates the request URL and returns the value of the parameter.</summary>
		/// <returns>The current value of the parameter.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> instance of the request.</param>
		/// <param name="control">The control that the parameter is bound to.</param>
		// Token: 0x06002DA1 RID: 11681 RVA: 0x00078E6C File Offset: 0x0007706C
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || control == null)
			{
				return null;
			}
			Page page = control.Page;
			if (page == null)
			{
				throw new NullReferenceException(".NET emulation");
			}
			RouteData routeData = page.RouteData;
			if (routeData == null)
			{
				return null;
			}
			return routeData.Values[this.RouteKey];
		}

		// Token: 0x04001B81 RID: 7041
		private string routeKey;
	}
}

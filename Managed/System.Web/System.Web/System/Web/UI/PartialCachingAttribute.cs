using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that Web Forms user controls (.ascx files) use to indicate if and how their output is cached. This class cannot be inherited.</summary>
	// Token: 0x02000219 RID: 537
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PartialCachingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class with the specified duration assigned to the user control to be cached.</summary>
		/// <param name="duration">The amount of time, in seconds, a user control should remain in the output cache. </param>
		// Token: 0x06001611 RID: 5649 RVA: 0x0003B71F File Offset: 0x0003991F
		public PartialCachingAttribute(int duration)
		{
			this.duration = duration;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class, specifying the caching duration, any GET and POST values, control names, and custom output caching requirements used to vary the cache.</summary>
		/// <param name="duration">The amount of time, in seconds, that the user control is cached. </param>
		/// <param name="varyByParams">A semicolon-separated list of strings used to vary the output cache. By default, these strings correspond to a query string value sent with GET method attributes or to a parameter sent using the POST method. When this attribute is set to multiple parameters, the output cache contains a different version of the requested document for each specified parameter. Possible values include "none", "*", and any valid query string or POST parameter name. </param>
		/// <param name="varyByControls">A semicolon-separated list of strings used to vary the output cache. These strings represent fully qualified names of properties on a user control. When this parameter is used for a user control, the user control output is varied to the cache for each specified user control property. </param>
		/// <param name="varyByCustom">Any text that represents custom output caching requirements. If this parameter is given a value of "browser", the cache is varied by browser name and major version information. If a custom string is entered, you must override the <see cref="M:System.Web.HttpApplication.GetVaryByCustomString(System.Web.HttpContext,System.String)" /> method in your application's Global.asax file. </param>
		// Token: 0x06001612 RID: 5650 RVA: 0x0003B72E File Offset: 0x0003992E
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom)
		{
			this.duration = duration;
			this.varyByParams = varyByParams;
			this.varyByControls = varyByControls;
			this.varyByCustom = varyByCustom;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class, specifying the caching duration, any GET and POST values, control names, custom output caching requirements used to vary the cache, and whether the user control output can be shared with multiple pages.</summary>
		/// <param name="duration">The amount of time, in seconds, that the user control is cached.</param>
		/// <param name="varyByParams">A semicolon-separated list of strings used to vary the output cache. By default, these strings correspond to a query string value sent with GET method attributes, or a parameter sent using the POST method. When this attribute is set to multiple parameters, the output cache contains a different version of the requested document for each specified parameter. Possible values include "none", "*", and any valid query string or POST parameter name.</param>
		/// <param name="varyByControls">A semicolon-separated list of strings used to vary the output cache. These strings represent fully qualified names of properties on a user control. When this parameter is used for a user control, the user control output is varied to the cache for each specified user control property.</param>
		/// <param name="varyByCustom">Any text that represents custom output caching requirements. If this parameter is given a value of "browser", the cache is varied by browser name and major version information. If a custom string is entered, you must override the <see cref="M:System.Web.HttpApplication.GetVaryByCustomString(System.Web.HttpContext,System.String)" /> method in your application's Global.asax file.</param>
		/// <param name="shared">true to indicate that the user control output can be shared with multiple pages; otherwise, false. </param>
		// Token: 0x06001613 RID: 5651 RVA: 0x0003B753 File Offset: 0x00039953
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, bool shared)
		{
			this.duration = duration;
			this.varyByParams = varyByParams;
			this.varyByControls = varyByControls;
			this.varyByCustom = varyByCustom;
			this.shared = shared;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class, specifying the caching duration, any GET and POST values, control names, custom output caching requirements used to vary the cache, the database dependencies, and whether the user control output can be shared with multiple pages. </summary>
		/// <param name="duration">The amount of time, in seconds, that the user control is cached.</param>
		/// <param name="varyByParams">A semicolon-separated list of strings used to vary the output cache. By default, these strings correspond to a query string value sent with GET method attributes, or a parameter sent using the POST method. When this attribute is set to multiple parameters, the output cache contains a different version of the requested document for each specified parameter. Possible values include "none", "*", and any valid query string or POST parameter name.</param>
		/// <param name="varyByControls">A semicolon-separated list of strings used to vary the output cache. These strings represent fully qualified names of properties on a user control. When this parameter is used for a user control, the user control output is varied to the cache for each specified user control property.</param>
		/// <param name="varyByCustom">Any text that represents custom output caching requirements. If this parameter is given a value of "browser", the cache is varied by browser name and major version information. If a custom string is entered, you must override the <see cref="M:System.Web.HttpApplication.GetVaryByCustomString(System.Web.HttpContext,System.String)" /> method in your application's Global.asax file.</param>
		/// <param name="sqlDependency">A delimited list of database names and table names that, when changed, explicitly expire a cache entry in the ASP.NET cache. These database names match those SQL Server cache dependencies identified in your Web configuration section.</param>
		/// <param name="shared">true to indicate that the user control output can be shared with multiple pages; otherwise, false.</param>
		// Token: 0x06001614 RID: 5652 RVA: 0x0003B780 File Offset: 0x00039980
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, bool shared)
		{
			this.duration = duration;
			this.varyByParams = varyByParams;
			this.varyByControls = varyByControls;
			this.varyByCustom = varyByCustom;
			this.sqlDependency = sqlDependency;
			this.shared = shared;
		}

		/// <summary>Gets the amount of time, in seconds, that cached items should remain in the output cache.</summary>
		/// <returns>The amount of time, in seconds, a user control should remain in the output cache.</returns>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0003B7B5 File Offset: 0x000399B5
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public int Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the provider that is used to store the output-cached data for the associated control.</summary>
		/// <returns>The name of the provider.</returns>
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0003B7BD File Offset: 0x000399BD
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x0003B7C5 File Offset: 0x000399C5
		public string ProviderName { get; set; }

		/// <summary>Gets a list of query string or form POST parameters that the output cache will use to vary the user control.</summary>
		/// <returns>The list of query string or form POST parameters.</returns>
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0003B7CE File Offset: 0x000399CE
		// (set) Token: 0x06001622 RID: 5666 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string VaryByParams
		{
			get
			{
				return this.varyByParams;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a list of user control properties that the output cache uses to vary the user control.</summary>
		/// <returns>The list of user control properties.</returns>
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0003B7D6 File Offset: 0x000399D6
		// (set) Token: 0x06001620 RID: 5664 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string VaryByControls
		{
			get
			{
				return this.varyByControls;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a list of custom strings that the output cache will use to vary the user control.</summary>
		/// <returns>The list of custom strings.</returns>
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0003B7DE File Offset: 0x000399DE
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string VaryByCustom
		{
			get
			{
				return this.varyByCustom;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value indicating whether user control output can be shared with multiple pages.</summary>
		/// <returns>true if user control output can be shared between multiple pages; otherwise, false. The default is false.</returns>
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x0003B7E6 File Offset: 0x000399E6
		// (set) Token: 0x0600161E RID: 5662 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool Shared
		{
			get
			{
				return this.shared;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a delimited string that identifies one or more database and table name pairs that the cached user control is dependent on.</summary>
		/// <returns>A delimited string that identifies a set of database and table names that the user control cache entry is dependent on.</returns>
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0003B7EE File Offset: 0x000399EE
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string SqlDependency
		{
			get
			{
				return this.sqlDependency;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001546 RID: 5446
		private int duration;

		// Token: 0x04001547 RID: 5447
		private string varyByControls;

		// Token: 0x04001548 RID: 5448
		private string varyByCustom;

		// Token: 0x04001549 RID: 5449
		private string varyByParams;

		// Token: 0x0400154A RID: 5450
		private bool shared;

		// Token: 0x0400154B RID: 5451
		private string sqlDependency;
	}
}

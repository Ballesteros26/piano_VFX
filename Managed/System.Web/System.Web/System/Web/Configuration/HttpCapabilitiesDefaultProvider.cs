using System;
using System.Collections;

namespace System.Web.Configuration
{
	/// <summary>The default extension of the <see cref="T:System.Web.Configuration.HttpCapabilitiesProvider" /> class that is included with ASP.NET.</summary>
	// Token: 0x020005A7 RID: 1447
	public class HttpCapabilitiesDefaultProvider : HttpCapabilitiesProvider
	{
		/// <summary>Gets or sets the length of time in seconds to retain the <see cref="T:System.Web.HttpBrowserCapabilities" /> object in the cache.</summary>
		/// <returns>The length of time in seconds to retain the <see cref="T:System.Web.HttpBrowserCapabilities" /> object in the cache.</returns>
		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000A47C3 File Offset: 0x000A29C3
		// (set) Token: 0x06003DFE RID: 15870 RVA: 0x000A47CB File Offset: 0x000A29CB
		public TimeSpan CacheTime { get; set; }

		/// <summary>Gets or sets the type of the class that is used to hold the results from parsing the browserCap element of the Web.config file.</summary>
		/// <returns>The type of the class that is used to hold the results from parsing the browserCaps element of the Web.config file.</returns>
		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x000A47D4 File Offset: 0x000A29D4
		// (set) Token: 0x06003E00 RID: 15872 RVA: 0x000A47DC File Offset: 0x000A29DC
		public Type ResultType { get; set; }

		/// <summary>Gets or sets the number of characters from the user agent string to use for caching of the <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</summary>
		/// <returns>The number of characters from the supplied user agent string to use for caching of the <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</returns>
		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06003E01 RID: 15873 RVA: 0x000A47E5 File Offset: 0x000A29E5
		// (set) Token: 0x06003E02 RID: 15874 RVA: 0x000A47ED File Offset: 0x000A29ED
		public int UserAgentCacheKeyLength { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpCapabilitiesDefaultProvider" /> class.</summary>
		// Token: 0x06003E03 RID: 15875 RVA: 0x000A47F6 File Offset: 0x000A29F6
		public HttpCapabilitiesDefaultProvider()
		{
			this.UserAgentCacheKeyLength = 64;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpCapabilitiesDefaultProvider" /> class with the values of the specified instance.</summary>
		/// <param name="parent">The <see cref="T:System.Web.Configuration.HttpCapabilitiesDefaultProvider" /> instance to use for initializing a new instance.</param>
		// Token: 0x06003E04 RID: 15876 RVA: 0x000A4806 File Offset: 0x000A2A06
		public HttpCapabilitiesDefaultProvider(HttpCapabilitiesDefaultProvider parent)
		{
			this.CacheTime = parent.CacheTime;
			this.ResultType = parent.ResultType;
			this.UserAgentCacheKeyLength = parent.UserAgentCacheKeyLength;
		}

		/// <summary>Adds an HTTP request string to use to parse browser capability information.</summary>
		/// <param name="variable">The string to use to parse browser capability information.</param>
		// Token: 0x06003E05 RID: 15877 RVA: 0x00003A1F File Offset: 0x00001C1F
		public void AddDependency(string variable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a search string that modifies a browser definition.</summary>
		/// <param name="ruleList">The search string that modifies a browser definition.</param>
		// Token: 0x06003E06 RID: 15878 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddRuleList(ArrayList ruleList)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the specified <see cref="T:System.Web.HttpRequest" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the <see cref="T:System.Web.HttpRequest" /> object that was passed in.</returns>
		/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> object.</param>
		// Token: 0x06003E07 RID: 15879 RVA: 0x000A4832 File Offset: 0x000A2A32
		public override HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request)
		{
			return new HttpBrowserCapabilities
			{
				capabilities = HttpCapabilitiesBase.GetConfigCapabilities(null, request).Capabilities
			};
		}
	}
}

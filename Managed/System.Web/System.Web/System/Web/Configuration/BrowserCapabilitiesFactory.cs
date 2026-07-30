using System;
using System.Collections;
using System.Collections.Specialized;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Used internally by the configuration system to produce request-specific instances of the <see cref="T:System.Web.HttpBrowserCapabilities" /> class that are publicly accessed through the ASP.NET-intrinsic Request.Browser property.</summary>
	// Token: 0x02000779 RID: 1913
	public class BrowserCapabilitiesFactory : BrowserCapabilitiesFactoryBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BrowserCapabilitiesFactory" /> class. </summary>
		// Token: 0x06004D70 RID: 19824 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public BrowserCapabilitiesFactory()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Blackberry browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Blackberry browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D71 RID: 19825 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void BlackberryProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Blackberry gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D72 RID: 19826 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void BlackberryProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Google Chrome browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Crawler browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D73 RID: 19827 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void ChromeProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Google Chrome gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D74 RID: 19828 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void ChromeProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally by the configuration system to produce request-specific instances of the <see cref="T:System.Web.HttpBrowserCapabilities" /> class that are publicly accessed through the ASP.NET-intrinsic Request.Browser property.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D75 RID: 19829 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void ConfigureBrowserCapabilities(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Cpu browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Cpu browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D76 RID: 19830 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void CpuProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Cpu gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D77 RID: 19831 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void CpuProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Crawler browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Crawler browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D78 RID: 19832 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void CrawlerProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Crawler gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D79 RID: 19833 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void CrawlerProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the DefaultDefault browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Default browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D7A RID: 19834 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void DefaultDefaultProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Default browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Default browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D7B RID: 19835 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void DefaultProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Default gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D7C RID: 19836 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void DefaultProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the DefaultWml browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the DefaultWml browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D7D RID: 19837 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void DefaultWmlProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the DefaultXhtmlmp browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the DefaultXhtmlmp browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D7E RID: 19838 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void DefaultXhtmlmpProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mozilla Firefox35 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Firefox35 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D7F RID: 19839 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Firefox35ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Firefox35 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D80 RID: 19840 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Firefox35ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mozilla Firefox version 3 and later browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Mozilla Firefox version 3 and later browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D81 RID: 19841 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Firefox3plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mozilla Firefox version 3 and later gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D82 RID: 19842 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Firefox3plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mozilla Firefox 3 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Firefox3 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D83 RID: 19843 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Firefox3ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mozilla Firefox3 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D84 RID: 19844 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Firefox3ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mozilla Firefox browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Firefox browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D85 RID: 19845 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void FirefoxProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mozilla Firefox gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D86 RID: 19846 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void FirefoxProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Genericdownlevel browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Genericdownlevel browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D87 RID: 19847 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void GenericdownlevelProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Genericdownlevel gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D88 RID: 19848 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void GenericdownlevelProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Microsoft Internet Explorer version 10 and later browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Internet Explorer 10 and later browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D89 RID: 19849 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie10plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Microsoft Internet Explorer version 10 and later gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D8A RID: 19850 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie10plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Microsoft Internet Explorer version 6 and later browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Internet Explorer version 6 and later browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D8B RID: 19851 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie6plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Microsoft Internet Explorer version 6 and later gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D8C RID: 19852 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie6plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Ie6to9 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Ie6to9 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D8D RID: 19853 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie6to9ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Ie6to9 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D8E RID: 19854 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie6to9ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the IE7 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the IE7 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D8F RID: 19855 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie7ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the IE7 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D90 RID: 19856 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie7ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the IE8 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the IE8 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D91 RID: 19857 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie8ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the IE8 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D92 RID: 19858 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie8ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Microsoft Internet Explorer version 9 and later browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Internet Explorer version 9 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D93 RID: 19859 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie9ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Microsoft Internet Explorer version 9 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D94 RID: 19860 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Ie9ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Iebeta browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Iebeta browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D95 RID: 19861 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IebetaProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Iebeta gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D96 RID: 19862 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IebetaProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Iemobile browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Iemobile browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D97 RID: 19863 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IemobileProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Iemobile gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D98 RID: 19864 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IemobileProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Ie browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Ie browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D99 RID: 19865 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IeProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Ie gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D9A RID: 19866 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IeProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Internetexplorer browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Internetexplorer browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D9B RID: 19867 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void InternetexplorerProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Internetexplorer gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D9C RID: 19868 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void InternetexplorerProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple iPad browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Apple iPad browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D9D RID: 19869 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IpadProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple iPad gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D9E RID: 19870 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IpadProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Iphone browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Iephone browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004D9F RID: 19871 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IphoneProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Iephone gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA0 RID: 19872 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IphoneProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple Ipod browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Ipod browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA1 RID: 19873 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IpodProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple Ipod gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA2 RID: 19874 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void IpodProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mono browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Mono browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA3 RID: 19875 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void MonoProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mono gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA4 RID: 19876 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void MonoProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mozilla browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Mozilla browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA5 RID: 19877 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void MozillaProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Mozilla gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA6 RID: 19878 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void MozillaProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera10 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Opera10 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA7 RID: 19879 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Opera10ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera10 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA8 RID: 19880 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Opera10ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera version 8 and later browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Opera version 8 and later browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DA9 RID: 19881 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Opera8plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera version 8 and later gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DAA RID: 19882 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Opera8plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera8to9 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Opera8to9 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DAB RID: 19883 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Opera8to9ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera8to9 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DAC RID: 19884 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Opera8to9ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera Mini browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Opera Mini browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DAD RID: 19885 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OperaminiProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera Mini gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DAE RID: 19886 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OperaminiProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera Mobile browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Opera Mobile browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DAF RID: 19887 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OperamobileProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera Mobile gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB0 RID: 19888 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OperamobileProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Opera browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB1 RID: 19889 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OperaProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Opera gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB2 RID: 19890 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OperaProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the OS browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the OS browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB3 RID: 19891 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OSProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the OS gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB4 RID: 19892 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void OSProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Pixels browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Pixels browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB5 RID: 19893 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PixelsProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Pixels gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB6 RID: 19894 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PixelsProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformmac68k browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformmac68k browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB7 RID: 19895 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformmac68kProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformmac68k gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB8 RID: 19896 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformmac68kProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformmacppc browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformmacppc browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DB9 RID: 19897 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformmacppcProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformmacppc gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DBA RID: 19898 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformmacppcProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platform browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platform browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DBB RID: 19899 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platform gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DBC RID: 19900 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformunix browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformunix browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DBD RID: 19901 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformunixProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformunix gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DBE RID: 19902 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformunixProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwebtv browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformwebtv browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DBF RID: 19903 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformwebtvProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwebtv gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC0 RID: 19904 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformwebtvProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin16 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformwin16 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC1 RID: 19905 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin16ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin16 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC2 RID: 19906 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin16ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin2000a browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformwin2000a browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC3 RID: 19907 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin2000aProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin2000a gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC4 RID: 19908 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin2000aProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin2000b browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformwin2000b browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC5 RID: 19909 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin2000bProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin2000b gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC6 RID: 19910 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin2000bProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin95 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformwin95 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC7 RID: 19911 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin95ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin95 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC8 RID: 19912 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin95ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin98 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformwin98 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DC9 RID: 19913 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin98ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwin98 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DCA RID: 19914 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Platformwin98ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwince browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformwince browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DCB RID: 19915 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformwinceProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwince gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DCC RID: 19916 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformwinceProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwinnt browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformwinnt browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DCD RID: 19917 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformwinntProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwinnt gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DCE RID: 19918 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformwinntProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwinxp browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Platformwinxp browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DCF RID: 19919 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformwinxpProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Platformwinxp gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DD0 RID: 19920 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void PlatformwinxpProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally by the configuration system to produce request-specific instances of <see cref="T:System.Web.HttpBrowserCapabilities" /> that are publicly accessed through the ASP.NET-intrinsic Request.Browser property. </summary>
		/// <param name="dictionary">A collection of browser capabilities.</param>
		// Token: 0x06004DD1 RID: 19921 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void PopulateBrowserElements(IDictionary dictionary)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally by the configuration system to produce request-specific instances of <see cref="T:System.Web.HttpBrowserCapabilities" /> that are publicly accessed through the ASP.NET-intrinsic Request.Browser property.</summary>
		/// <param name="dictionary">A collection of request headers.</param>
		// Token: 0x06004DD2 RID: 19922 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void PopulateMatchedHeaders(IDictionary dictionary)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple Safari version 3 and later browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Apple Safari version 3 and later browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DD3 RID: 19923 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Safari3plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple Safari version 3 and later gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DD4 RID: 19924 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Safari3plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple Safari3to4 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Safari3to4 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DD5 RID: 19925 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Safari3to4ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple Safari3to4 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DD6 RID: 19926 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Safari3to4ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple Safari4 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Safari4 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DD7 RID: 19927 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Safari4ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Apple Safari4 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DD8 RID: 19928 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Safari4ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Safari browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Safari browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DD9 RID: 19929 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void SafariProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Safari gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DDA RID: 19930 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void SafariProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the UC Browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the UC Browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DDB RID: 19931 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void UcbrowserProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the UC Browser gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DDC RID: 19932 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void UcbrowserProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Voice browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Voice browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DDD RID: 19933 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void VoiceProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Voice gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DDE RID: 19934 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void VoiceProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the WebKit Mobile browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the WebKit Mobile browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DDF RID: 19935 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void WebkitmobileProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the WebKit Mobile gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE0 RID: 19936 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void WebkitmobileProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the WebKit browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the WebKit browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE1 RID: 19937 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void WebkitProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the WebKit gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE2 RID: 19938 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void WebkitProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Win16 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Win16 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE3 RID: 19939 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Win16ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Win16 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE4 RID: 19940 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Win16ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Win32 browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Win32 browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE5 RID: 19941 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Win32ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Win32 gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE6 RID: 19942 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Win32ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Windows Phone browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Windows Phone browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE7 RID: 19943 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void WindowsphoneProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Windows Phone gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE8 RID: 19944 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void WindowsphoneProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Win browser making the current request.</summary>
		/// <param name="ignoreApplicationBrowsers">true to ignore definitions for the Win browser in application-level browser definition files; otherwise, false.</param>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DE9 RID: 19945 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void WinProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpBrowserCapabilities" /> object that represents the capabilities of the Win gateway handling the current request.</summary>
		/// <param name="headers">The collection of headers included in the current request.</param>
		/// <param name="browserCaps">The <see cref="T:System.Web.HttpBrowserCapabilities" /> object to initialize.</param>
		// Token: 0x06004DEA RID: 19946 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void WinProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

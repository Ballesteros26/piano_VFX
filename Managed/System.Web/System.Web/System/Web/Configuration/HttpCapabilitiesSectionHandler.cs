using System;
using System.Configuration;
using System.Xml;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Assists in reading in the &lt;browserCaps&gt; section of a configuration file and creating an instance of the <see cref="T:System.Web.HttpBrowserCapabilities" /> class that contains the capabilities information for the client browser.</summary>
	// Token: 0x0200077B RID: 1915
	public class HttpCapabilitiesSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpCapabilitiesSectionHandler" /> class.</summary>
		// Token: 0x06004DF3 RID: 19955 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public HttpCapabilitiesSectionHandler()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.HttpBrowserCapabilities" /> class that contains the capabilities information for the client browser.</summary>
		/// <returns>An instance of <see cref="T:System.Web.HttpBrowserCapabilities" /> that contains the capabilities information for the client browser.</returns>
		/// <param name="parent">The parent configuration node.</param>
		/// <param name="configurationContext">The configuration context of the current configuration file.</param>
		/// <param name="section">The section of the configuration file that contains the information.</param>
		// Token: 0x06004DF4 RID: 19956 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public object Create(object parent, object configurationContext, XmlNode section)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}

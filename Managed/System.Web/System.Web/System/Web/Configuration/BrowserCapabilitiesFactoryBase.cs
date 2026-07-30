using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>The <see cref="T:System.Web.Configuration.BrowserCapabilitiesFactoryBase" /> class is the base class from which <see cref="T:System.Web.Configuration.BrowserCapabilitiesFactory" /> is derived. It is used internally at run time by the configuration system to create request-specific instances of the <see cref="T:System.Web.Configuration.HttpCapabilitiesBase" /> class, publicly accessed through the ASP.NET intrinsic Request.Browser property.</summary>
	// Token: 0x0200077A RID: 1914
	public class BrowserCapabilitiesFactoryBase
	{
		/// <summary>Used internally by the configuration system to create a new instance of this class.</summary>
		// Token: 0x06004DEB RID: 19947 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public BrowserCapabilitiesFactoryBase()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally by the configuration system to represent a collection of information relating to various browser capabilities.</summary>
		/// <returns>A dictionary of browser capabilities.</returns>
		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x06004DEC RID: 19948 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected IDictionary BrowserElements
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally by the configuration system to represent a collection of request-header values.</summary>
		/// <returns>A collection of request headers.</returns>
		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x06004DED RID: 19949 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected IDictionary MatchedHeaders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally at run time to configure an <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</summary>
		/// <param name="headers">A collection of request headers.</param>
		/// <param name="browserCaps">An <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</param>
		// Token: 0x06004DEE RID: 19950 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void ConfigureBrowserCapabilities(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally at run time to configure custom hierarchies of browser capabilities.</summary>
		/// <param name="headers">A collection of request headers.</param>
		/// <param name="browserCaps">An object that specifies the capabilities of the browser.</param>
		// Token: 0x06004DEF RID: 19951 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void ConfigureCustomCapabilities(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally by the configuration system to determine whether the browser represented by the specified <see cref="T:System.Web.HttpBrowserCapabilities" /> object does not have an available adapter.</summary>
		/// <returns>true if the represented browser does not have an available adapter; otherwise, false.</returns>
		/// <param name="browserCaps">An <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</param>
		// Token: 0x06004DF0 RID: 19952 RVA: 0x000CB35C File Offset: 0x000C955C
		protected bool IsBrowserUnknown(HttpCapabilitiesBase browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Used internally by the configuration system to populate a collection of browser capabilities based on the supported browser.</summary>
		/// <param name="dictionary">A collection of key/value pairs representing the browser capabilities.</param>
		// Token: 0x06004DF1 RID: 19953 RVA: 0x0000B3E4 File Offset: 0x000095E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void PopulateBrowserElements(IDictionary dictionary)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally by the configuration system to populate a collection of request headers based on the supported browser.</summary>
		/// <param name="dictionary">A collection of key/value pairs representing the browser capabilities.</param>
		// Token: 0x06004DF2 RID: 19954 RVA: 0x0000B3E4 File Offset: 0x000095E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void PopulateMatchedHeaders(IDictionary dictionary)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

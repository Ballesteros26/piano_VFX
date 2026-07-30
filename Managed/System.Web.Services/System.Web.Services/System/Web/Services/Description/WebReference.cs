using System;
using System.CodeDom;
using System.Collections.Specialized;
using System.Web.Services.Discovery;

namespace System.Web.Services.Description
{
	/// <summary>Describes a reference to a collection of XML Web services.</summary>
	// Token: 0x02000133 RID: 307
	public sealed class WebReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.WebReference" /> class with the given data.</summary>
		/// <param name="documents">A <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />  that specifies a collection of description documents.</param>
		/// <param name="proxyCode">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies a namespace for code compilation.</param>
		/// <param name="protocolName">The protocol used by the XML Web service.</param>
		/// <param name="appSettingUrlKey">The URL key of the Web reference.</param>
		/// <param name="appSettingBaseUrl">The base URL of the Web reference.</param>
		// Token: 0x06000950 RID: 2384 RVA: 0x00040E28 File Offset: 0x0003F028
		public WebReference(DiscoveryClientDocumentCollection documents, CodeNamespace proxyCode, string protocolName, string appSettingUrlKey, string appSettingBaseUrl)
		{
			if (documents == null)
			{
				throw new ArgumentNullException("documents");
			}
			if (proxyCode == null)
			{
				throw new ArgumentNullException("proxyCode");
			}
			if (appSettingBaseUrl != null && appSettingUrlKey == null)
			{
				throw new ArgumentNullException("appSettingUrlKey");
			}
			this.protocolName = protocolName;
			this.appSettingUrlKey = appSettingUrlKey;
			this.appSettingBaseUrl = appSettingBaseUrl;
			this.documents = documents;
			this.proxyCode = proxyCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.WebReference" /> class with the given description document collection and proxy code namespace.</summary>
		/// <param name="documents">A <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />  that specifies a collection of description documents.</param>
		/// <param name="proxyCode">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies a namespace for code compilation.</param>
		// Token: 0x06000951 RID: 2385 RVA: 0x00040E8F File Offset: 0x0003F08F
		public WebReference(DiscoveryClientDocumentCollection documents, CodeNamespace proxyCode)
			: this(documents, proxyCode, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.WebReference" /> class with the given data.</summary>
		/// <param name="documents">A <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />  that specifies a collection of description documents.</param>
		/// <param name="proxyCode">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies a namespace for code compilation.</param>
		/// <param name="appSettingUrlKey">The URL key of the Web reference.</param>
		/// <param name="appSettingBaseUrl">The base URL of the Web reference.</param>
		// Token: 0x06000952 RID: 2386 RVA: 0x00040E9C File Offset: 0x0003F09C
		public WebReference(DiscoveryClientDocumentCollection documents, CodeNamespace proxyCode, string appSettingUrlKey, string appSettingBaseUrl)
			: this(documents, proxyCode, null, appSettingUrlKey, appSettingBaseUrl)
		{
		}

		/// <summary>Gets the base URL of the Web reference.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the base URL of the Web reference.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00040EAA File Offset: 0x0003F0AA
		public string AppSettingBaseUrl
		{
			get
			{
				return this.appSettingBaseUrl;
			}
		}

		/// <summary>Gets the URL key of the web reference.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the URL key of the Web reference.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00040EB2 File Offset: 0x0003F0B2
		public string AppSettingUrlKey
		{
			get
			{
				return this.appSettingUrlKey;
			}
		}

		/// <summary>Gets the collection of description documents associated with the Web reference.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> used to initialize the <see cref="T:System.Web.Services.Description.WebReference" /> instance.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00040EBA File Offset: 0x0003F0BA
		public DiscoveryClientDocumentCollection Documents
		{
			get
			{
				return this.documents;
			}
		}

		/// <summary>Gets the code namespace associated with the Web reference.</summary>
		/// <returns>The <see cref="T:System.CodeDom.CodeNamespace" /> in which proxy code will be generated when the <see cref="M:System.Web.Services.Description.ServiceDescriptionImporter.GenerateWebReferences(System.Web.Services.Description.WebReferenceCollection,System.CodeDom.Compiler.CodeDomProvider,System.CodeDom.CodeCompileUnit,System.Web.Services.Description.WebReferenceOptions)" /> method is called.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00040EC2 File Offset: 0x0003F0C2
		public CodeNamespace ProxyCode
		{
			get
			{
				return this.proxyCode;
			}
		}

		/// <summary>Gets a collection of warnings generated when validating the description documents.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> of validation warning text.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00040ECA File Offset: 0x0003F0CA
		public StringCollection ValidationWarnings
		{
			get
			{
				if (this.validationWarnings == null)
				{
					this.validationWarnings = new StringCollection();
				}
				return this.validationWarnings;
			}
		}

		/// <summary>Gets a collection of warnings generated when importing Web Services Description Language (WSDL) service description documents.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> collection of warnings generated when importing WSDL service description documents.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00040EE5 File Offset: 0x0003F0E5
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00040EED File Offset: 0x0003F0ED
		public ServiceDescriptionImportWarnings Warnings
		{
			get
			{
				return this.warnings;
			}
			set
			{
				this.warnings = value;
			}
		}

		/// <summary>Gets or sets the protocol associated with the Web reference.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the protocol associated with the Web reference.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00040EF6 File Offset: 0x0003F0F6
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00040F0C File Offset: 0x0003F10C
		public string ProtocolName
		{
			get
			{
				if (this.protocolName != null)
				{
					return this.protocolName;
				}
				return string.Empty;
			}
			set
			{
				this.protocolName = value;
			}
		}

		// Token: 0x0400057A RID: 1402
		private CodeNamespace proxyCode;

		// Token: 0x0400057B RID: 1403
		private DiscoveryClientDocumentCollection documents;

		// Token: 0x0400057C RID: 1404
		private string appSettingUrlKey;

		// Token: 0x0400057D RID: 1405
		private string appSettingBaseUrl;

		// Token: 0x0400057E RID: 1406
		private string protocolName;

		// Token: 0x0400057F RID: 1407
		private ServiceDescriptionImportWarnings warnings;

		// Token: 0x04000580 RID: 1408
		private StringCollection validationWarnings;
	}
}

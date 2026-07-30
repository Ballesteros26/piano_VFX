using System;
using System.Configuration;
using System.Web.Services.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Allows the user to programmatically access the system.web group of the configuration file. This class cannot be inherited.</summary>
	// Token: 0x020005DD RID: 1501
	public sealed class SystemWebSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the anonymousIdentification section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AnonymousIdentificationSection" /> object.</returns>
		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x060040EF RID: 16623 RVA: 0x000AAB9A File Offset: 0x000A8D9A
		[ConfigurationProperty("anonymousIdentification")]
		public AnonymousIdentificationSection AnonymousIdentification
		{
			get
			{
				return (AnonymousIdentificationSection)base.Sections["anonymousIdentification"];
			}
		}

		/// <summary>Gets the authentication section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AuthenticationSection" /> object.</returns>
		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x000AABB1 File Offset: 0x000A8DB1
		[ConfigurationProperty("authentication")]
		public AuthenticationSection Authentication
		{
			get
			{
				return (AuthenticationSection)base.Sections["authentication"];
			}
		}

		/// <summary>Gets the authorization section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AuthorizationSection" /> object.</returns>
		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x000AABC8 File Offset: 0x000A8DC8
		[ConfigurationProperty("authorization")]
		public AuthorizationSection Authorization
		{
			get
			{
				return (AuthorizationSection)base.Sections["authorization"];
			}
		}

		/// <summary>Gets the browserCaps section.</summary>
		/// <returns>The <see cref="T:System.Configuration.DefaultSection" /> object.</returns>
		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x000AABDF File Offset: 0x000A8DDF
		[ConfigurationProperty("browserCaps")]
		public DefaultSection BrowserCaps
		{
			get
			{
				return (DefaultSection)base.Sections["browserCaps"];
			}
		}

		/// <summary>Gets the clientTarget section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ClientTargetSection" /> object.</returns>
		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x000AABF6 File Offset: 0x000A8DF6
		[ConfigurationProperty("clientTarget")]
		public ClientTargetSection ClientTarget
		{
			get
			{
				return (ClientTargetSection)base.Sections["clientTarget"];
			}
		}

		/// <summary>Gets the compilation section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CompilationSection" /> object.</returns>
		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060040F4 RID: 16628 RVA: 0x000AAC0D File Offset: 0x000A8E0D
		[ConfigurationProperty("compilation")]
		public CompilationSection Compilation
		{
			get
			{
				return (CompilationSection)base.Sections["compilation"];
			}
		}

		/// <summary>Gets the customErrors section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomErrorsSection" /> object.</returns>
		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060040F5 RID: 16629 RVA: 0x000AAC24 File Offset: 0x000A8E24
		[ConfigurationProperty("customErrors")]
		public CustomErrorsSection CustomErrors
		{
			get
			{
				return (CustomErrorsSection)base.Sections["customErrors"];
			}
		}

		/// <summary>Gets the deployment section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.DeploymentSection" /> object.</returns>
		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x000AAC3B File Offset: 0x000A8E3B
		[ConfigurationProperty("deployment")]
		public DeploymentSection Deployment
		{
			get
			{
				return (DeploymentSection)base.Sections["deployment"];
			}
		}

		/// <summary>Gets the deviceFilters section.</summary>
		/// <returns>The <see cref="T:System.Configuration.DefaultSection" /> object.</returns>
		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x000AAC52 File Offset: 0x000A8E52
		[ConfigurationProperty("deviceFilters")]
		public DefaultSection DeviceFilters
		{
			get
			{
				return (DefaultSection)base.Sections["deviceFilters"];
			}
		}

		/// <summary>Gets the globalization section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.GlobalizationSection" /> object.</returns>
		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x000AAC69 File Offset: 0x000A8E69
		[ConfigurationProperty("globalization")]
		public GlobalizationSection Globalization
		{
			get
			{
				return (GlobalizationSection)base.Sections["globalization"];
			}
		}

		/// <summary>Gets the healthMonitoring section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HealthMonitoringSection" /> object.</returns>
		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x000AAC80 File Offset: 0x000A8E80
		[ConfigurationProperty("healthMonitoring")]
		public HealthMonitoringSection HealthMonitoring
		{
			get
			{
				return (HealthMonitoringSection)base.Sections["healthMonitoring"];
			}
		}

		/// <summary>Gets the hostingEnvironment section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HostingEnvironmentSection" /> object refers to the hostingEnvironment section of the configuration file. </returns>
		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x000AAC97 File Offset: 0x000A8E97
		[ConfigurationProperty("hostingEnvironment")]
		public HostingEnvironmentSection HostingEnvironment
		{
			get
			{
				return (HostingEnvironmentSection)base.Sections["hostingEnvironment"];
			}
		}

		/// <summary>Gets the httpCookies section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpCookiesSection" /> object.</returns>
		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x060040FB RID: 16635 RVA: 0x000AACAE File Offset: 0x000A8EAE
		[ConfigurationProperty("httpCookies")]
		public HttpCookiesSection HttpCookies
		{
			get
			{
				return (HttpCookiesSection)base.Sections["httpCookies"];
			}
		}

		/// <summary>Gets the httpHandlers section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlersSection" /> object.</returns>
		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x000AACC5 File Offset: 0x000A8EC5
		[ConfigurationProperty("httpHandlers")]
		public HttpHandlersSection HttpHandlers
		{
			get
			{
				return (HttpHandlersSection)base.Sections["httpHandlers"];
			}
		}

		/// <summary>Gets the httpModules section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpModulesSection" /> object.</returns>
		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x000AACDC File Offset: 0x000A8EDC
		[ConfigurationProperty("httpModules")]
		public HttpModulesSection HttpModules
		{
			get
			{
				return (HttpModulesSection)base.Sections["httpModules"];
			}
		}

		/// <summary>Gets the httpRuntime section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpRuntimeSection" /> object.</returns>
		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x000AACF3 File Offset: 0x000A8EF3
		[ConfigurationProperty("httpRuntime")]
		public HttpRuntimeSection HttpRuntime
		{
			get
			{
				return (HttpRuntimeSection)base.Sections["httpRuntime"];
			}
		}

		/// <summary>Gets the identity section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.IdentitySection" /> object.</returns>
		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x000AAD0A File Offset: 0x000A8F0A
		[ConfigurationProperty("identity")]
		public IdentitySection Identity
		{
			get
			{
				return (IdentitySection)base.Sections["identity"];
			}
		}

		/// <summary>Gets the machineKey section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.MachineKeySection" /> object.</returns>
		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x000AAD21 File Offset: 0x000A8F21
		[ConfigurationProperty("machineKey")]
		public MachineKeySection MachineKey
		{
			get
			{
				return (MachineKeySection)base.Sections["machineKey"];
			}
		}

		/// <summary>Gets the membership section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.MembershipSection" /> object.</returns>
		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x06004101 RID: 16641 RVA: 0x000AAD38 File Offset: 0x000A8F38
		[ConfigurationProperty("membership")]
		public MembershipSection Membership
		{
			get
			{
				return (MembershipSection)base.Sections["membership"];
			}
		}

		/// <summary>Gets the mobileControls section.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object refers to the mobileControls section of the configuration file.</returns>
		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06004102 RID: 16642 RVA: 0x000AAD4F File Offset: 0x000A8F4F
		[ConfigurationProperty("mobileControls")]
		[Obsolete("System.Web.Mobile.dll is obsolete.")]
		public ConfigurationSection MobileControls
		{
			get
			{
				return base.Sections["MobileControls"];
			}
		}

		/// <summary>Gets the pages section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.PagesSection" /> object.</returns>
		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06004103 RID: 16643 RVA: 0x000AAD61 File Offset: 0x000A8F61
		[ConfigurationProperty("pages")]
		public PagesSection Pages
		{
			get
			{
				return (PagesSection)base.Sections["pages"];
			}
		}

		/// <summary>Gets the processModel section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProcessModelSection" /> object.</returns>
		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x000AAD78 File Offset: 0x000A8F78
		[ConfigurationProperty("processModel")]
		public ProcessModelSection ProcessModel
		{
			get
			{
				return (ProcessModelSection)base.Sections["processModel"];
			}
		}

		/// <summary>Gets the profile section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileSection" /> object.</returns>
		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x000AAD8F File Offset: 0x000A8F8F
		[ConfigurationProperty("profile")]
		public ProfileSection Profile
		{
			get
			{
				return (ProfileSection)base.Sections["profile"];
			}
		}

		/// <summary>Gets the protocols section.</summary>
		/// <returns>The <see cref="T:System.Configuration.DefaultSection" /> object refers to the protocols section of the configuration file. </returns>
		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x000AADA6 File Offset: 0x000A8FA6
		[ConfigurationProperty("protocols")]
		public DefaultSection Protocols
		{
			get
			{
				return (DefaultSection)base.Sections["protocols"];
			}
		}

		/// <summary>Gets the roleManager section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.RoleManagerSection" /> object.</returns>
		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x000AADBD File Offset: 0x000A8FBD
		[ConfigurationProperty("roleManager")]
		public RoleManagerSection RoleManager
		{
			get
			{
				return (RoleManagerSection)base.Sections["roleManager"];
			}
		}

		/// <summary>Gets the securityPolicy section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SecurityPolicySection" /> object.</returns>
		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06004108 RID: 16648 RVA: 0x000AADD4 File Offset: 0x000A8FD4
		[ConfigurationProperty("securityPolicy")]
		public SecurityPolicySection SecurityPolicy
		{
			get
			{
				return (SecurityPolicySection)base.Sections["securityPolicy"];
			}
		}

		/// <summary>Gets the sessionState section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SessionStateSection" /> object.</returns>
		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06004109 RID: 16649 RVA: 0x000AADEB File Offset: 0x000A8FEB
		[ConfigurationProperty("sessionState")]
		public SessionStateSection SessionState
		{
			get
			{
				return (SessionStateSection)base.Sections["sessionState"];
			}
		}

		/// <summary>Gets the siteMap section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SiteMapSection" /> object.</returns>
		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x0600410A RID: 16650 RVA: 0x000AAE02 File Offset: 0x000A9002
		[ConfigurationProperty("siteMap")]
		public SiteMapSection SiteMap
		{
			get
			{
				return (SiteMapSection)base.Sections["siteMap"];
			}
		}

		/// <summary>Gets the trace section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TraceSection" /> object.</returns>
		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x000AAE19 File Offset: 0x000A9019
		[ConfigurationProperty("trace")]
		public TraceSection Trace
		{
			get
			{
				return (TraceSection)base.Sections["trace"];
			}
		}

		/// <summary>Gets the trust section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TrustSection" /> object.</returns>
		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x0600410C RID: 16652 RVA: 0x000AAE30 File Offset: 0x000A9030
		[ConfigurationProperty("trust")]
		public TrustSection Trust
		{
			get
			{
				return (TrustSection)base.Sections["trust"];
			}
		}

		/// <summary>Gets the urlMappings section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.UrlMappingsSection" /> object refers to the urlMappings section of the configuration file. </returns>
		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x000AAE47 File Offset: 0x000A9047
		[ConfigurationProperty("urlMappings")]
		public UrlMappingsSection UrlMappings
		{
			get
			{
				return (UrlMappingsSection)base.Sections["urlMappings"];
			}
		}

		/// <summary>Gets the webControls section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.WebControlsSection" /> object refers to the webControls section of the configuration file. </returns>
		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x000AAE5E File Offset: 0x000A905E
		[ConfigurationProperty("webControls")]
		public WebControlsSection WebControls
		{
			get
			{
				return (WebControlsSection)base.Sections["webControls"];
			}
		}

		/// <summary>Gets the webParts section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.WebPartsSection" /> object refers to the webParts section of the configuration file.</returns>
		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x000AAE75 File Offset: 0x000A9075
		[ConfigurationProperty("webParts")]
		public WebPartsSection WebParts
		{
			get
			{
				return (WebPartsSection)base.Sections["webParts"];
			}
		}

		/// <summary>Gets the webServices section.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object refers to the webServices section of the configuration file.</returns>
		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x000AAE8C File Offset: 0x000A908C
		[ConfigurationProperty("webServices")]
		public WebServicesSection WebServices
		{
			get
			{
				return (WebServicesSection)base.Sections["webServices"];
			}
		}

		/// <summary>Gets the xhtmlConformance section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.XhtmlConformanceSection" /> object refers to the xhtmlConformance section of the configuration file. </returns>
		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x000AAEA3 File Offset: 0x000A90A3
		[ConfigurationProperty("xhtmlConformance")]
		public XhtmlConformanceSection XhtmlConformance
		{
			get
			{
				return (XhtmlConformanceSection)base.Sections["xhtmlConformance"];
			}
		}

		/// <summary>Gets the FullTrustAssemblies section of the configuration file. </summary>
		/// <returns>The FullTrustAssemblies section of the configuration file.</returns>
		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06004113 RID: 16659 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public FullTrustAssembliesSection FullTrustAssemblies
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the PartialTrustVisibleAssemblies section of the configuration file. </summary>
		/// <returns>The PartialTrustVisibleAssemblies section of the configuration file.</returns>
		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06004114 RID: 16660 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public PartialTrustVisibleAssembliesSection PartialTrustVisibleAssemblies
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}

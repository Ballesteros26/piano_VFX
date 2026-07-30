using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the authorization section contained in the webParts section of the configuration. This class cannot be inherited.</summary>
	// Token: 0x020005F6 RID: 1526
	public sealed class WebPartsPersonalizationAuthorization : ConfigurationElement
	{
		// Token: 0x06004243 RID: 16963 RVA: 0x000AD4B8 File Offset: 0x000AB6B8
		static WebPartsPersonalizationAuthorization()
		{
			WebPartsPersonalizationAuthorization.properties.Add(WebPartsPersonalizationAuthorization.Prop);
		}

		/// <summary>Gets a collection of rules used for personalization authorization related to Web Parts.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> object.</returns>
		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x06004244 RID: 16964 RVA: 0x000AD4F4 File Offset: 0x000AB6F4
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public AuthorizationRuleCollection Rules
		{
			get
			{
				return (AuthorizationRuleCollection)base[WebPartsPersonalizationAuthorization.Prop];
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x000AD506 File Offset: 0x000AB706
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsPersonalizationAuthorization.properties;
			}
		}

		// Token: 0x04002373 RID: 9075
		private static ConfigurationProperty Prop = new ConfigurationProperty("", typeof(AuthorizationRuleCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002374 RID: 9076
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

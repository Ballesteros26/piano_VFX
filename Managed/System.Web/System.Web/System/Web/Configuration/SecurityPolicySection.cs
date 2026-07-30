using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the security infrastructure of a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005D6 RID: 1494
	public sealed class SecurityPolicySection : ConfigurationSection
	{
		// Token: 0x0600408C RID: 16524 RVA: 0x000A9F51 File Offset: 0x000A8151
		static SecurityPolicySection()
		{
			SecurityPolicySection.properties.Add(SecurityPolicySection.Prop);
		}

		/// <summary>Gets the <see cref="P:System.Web.Configuration.SecurityPolicySection.TrustLevels" /> collection.</summary>
		/// <returns>A collection of <see cref="P:System.Web.Configuration.SecurityPolicySection.TrustLevels" /> objects. </returns>
		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x000A9F89 File Offset: 0x000A8189
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public TrustLevelCollection TrustLevels
		{
			get
			{
				return (TrustLevelCollection)base[SecurityPolicySection.Prop];
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x000A9F9B File Offset: 0x000A819B
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SecurityPolicySection.properties;
			}
		}

		// Token: 0x040022F6 RID: 8950
		private static ConfigurationProperty Prop = new ConfigurationProperty("", typeof(TrustLevelCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x040022F7 RID: 8951
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

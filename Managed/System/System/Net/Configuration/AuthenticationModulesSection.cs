using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for authentication modules. This class cannot be inherited.</summary>
	// Token: 0x02000693 RID: 1683
	public sealed class AuthenticationModulesSection : ConfigurationSection
	{
		// Token: 0x060034CE RID: 13518 RVA: 0x000C3BCE File Offset: 0x000C1DCE
		static AuthenticationModulesSection()
		{
			AuthenticationModulesSection.properties.Add(AuthenticationModulesSection.authenticationModulesProp);
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x000C3C04 File Offset: 0x000C1E04
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationModulesSection.properties;
			}
		}

		/// <summary>Gets the collection of authentication modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> that contains the registered authentication modules. </returns>
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x000C3C0B File Offset: 0x000C1E0B
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public AuthenticationModuleElementCollection AuthenticationModules
		{
			get
			{
				return (AuthenticationModuleElementCollection)base[AuthenticationModulesSection.authenticationModulesProp];
			}
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		protected override void InitializeDefault()
		{
		}

		// Token: 0x04002A54 RID: 10836
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A55 RID: 10837
		private static ConfigurationProperty authenticationModulesProp = new ConfigurationProperty("", typeof(AuthenticationModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}

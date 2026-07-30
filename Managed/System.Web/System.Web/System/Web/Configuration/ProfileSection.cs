using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>The <see cref="T:System.Web.Configuration.ProfileSection" /> class provides a way to programmatically access and modify the profile section of a configuration file. This class cannot be inherited.</summary>
	// Token: 0x020005CC RID: 1484
	public sealed class ProfileSection : ConfigurationSection
	{
		// Token: 0x0600400B RID: 16395 RVA: 0x000A8F30 File Offset: 0x000A7130
		static ProfileSection()
		{
			ProfileSection.properties.Add(ProfileSection.automaticSaveEnabledProp);
			ProfileSection.properties.Add(ProfileSection.defaultProviderProp);
			ProfileSection.properties.Add(ProfileSection.enabledProp);
			ProfileSection.properties.Add(ProfileSection.inheritsProp);
			ProfileSection.properties.Add(ProfileSection.propertySettingsProp);
			ProfileSection.properties.Add(ProfileSection.providersProp);
		}

		/// <summary>Gets or sets a value that determines whether changes to user-profile information are automatically saved on page exit.</summary>
		/// <returns>true if profile information is automatically saved on page exit; otherwise, false. The default is true.</returns>
		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000A904D File Offset: 0x000A724D
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x000A905F File Offset: 0x000A725F
		[ConfigurationProperty("automaticSaveEnabled", DefaultValue = true)]
		public bool AutomaticSaveEnabled
		{
			get
			{
				return (bool)base[ProfileSection.automaticSaveEnabledProp];
			}
			set
			{
				base[ProfileSection.automaticSaveEnabledProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the default profile provider. </summary>
		/// <returns>The name of a provider in the <see cref="P:System.Web.Configuration.ProfileSection.Providers" /> collection, or an empty string (""). The default is "AspNetSqlProfileProvider."</returns>
		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x000A9072 File Offset: 0x000A7272
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x000A9084 File Offset: 0x000A7284
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlProfileProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base[ProfileSection.defaultProviderProp];
			}
			set
			{
				base[ProfileSection.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET profile feature is enabled.</summary>
		/// <returns>true if the ASP.NET compilation system should generate a ProfileCommon class that can be used to access information about individual user profiles; otherwise, false. The default is true.</returns>
		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x000A9092 File Offset: 0x000A7292
		// (set) Token: 0x06004011 RID: 16401 RVA: 0x000A90A4 File Offset: 0x000A72A4
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base[ProfileSection.enabledProp];
			}
			set
			{
				base[ProfileSection.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets a type reference for a custom type derived from <see cref="T:System.Web.Profile.ProfileBase" />.</summary>
		/// <returns>A valid type reference, or an empty string (""). The default is an empty string.</returns>
		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x000A90B7 File Offset: 0x000A72B7
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x000A90C9 File Offset: 0x000A72C9
		[ConfigurationProperty("inherits", DefaultValue = "")]
		public string Inherits
		{
			get
			{
				return (string)base[ProfileSection.inheritsProp];
			}
			set
			{
				base[ProfileSection.inheritsProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> collection of <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object that contains all the properties defined within the properties subsection of the profile section of the configuration file.</returns>
		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x000A90D7 File Offset: 0x000A72D7
		[ConfigurationProperty("properties")]
		public RootProfilePropertySettingsCollection PropertySettings
		{
			get
			{
				return (RootProfilePropertySettingsCollection)base[ProfileSection.propertySettingsProp];
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the providers defined within the providers subsection of the profile section of the configuration file.</returns>
		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x000A90E9 File Offset: 0x000A72E9
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[ProfileSection.providersProp];
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000A90FB File Offset: 0x000A72FB
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileSection.properties;
			}
		}

		// Token: 0x040022C0 RID: 8896
		private static ConfigurationProperty automaticSaveEnabledProp = new ConfigurationProperty("automaticSaveEnabled", typeof(bool), true);

		// Token: 0x040022C1 RID: 8897
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlProfileProvider");

		// Token: 0x040022C2 RID: 8898
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x040022C3 RID: 8899
		private static ConfigurationProperty inheritsProp = new ConfigurationProperty("inherits", typeof(string), "");

		// Token: 0x040022C4 RID: 8900
		private static ConfigurationProperty propertySettingsProp = new ConfigurationProperty("properties", typeof(RootProfilePropertySettingsCollection));

		// Token: 0x040022C5 RID: 8901
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));

		// Token: 0x040022C6 RID: 8902
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

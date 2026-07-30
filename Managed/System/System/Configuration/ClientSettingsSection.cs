using System;

namespace System.Configuration
{
	/// <summary>Represents a group of user-scoped application settings in a configuration file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000164 RID: 356
	public sealed class ClientSettingsSection : ConfigurationSection
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x00039485 File Offset: 0x00037685
		static ClientSettingsSection()
		{
			ClientSettingsSection.properties.Add(ClientSettingsSection.settings_prop);
		}

		/// <summary>Gets the collection of client settings for the section.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingElementCollection" /> containing all the client settings found in the current configuration section.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x000394C3 File Offset: 0x000376C3
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public SettingElementCollection Settings
		{
			get
			{
				return (SettingElementCollection)base[ClientSettingsSection.settings_prop];
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x000394D5 File Offset: 0x000376D5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientSettingsSection.properties;
			}
		}

		// Token: 0x04000F76 RID: 3958
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000F77 RID: 3959
		private static ConfigurationProperty settings_prop = new ConfigurationProperty("", typeof(SettingElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}

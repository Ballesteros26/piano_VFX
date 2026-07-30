using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the group subsection of the profiles configuration file section.</summary>
	// Token: 0x020005C7 RID: 1479
	public sealed class ProfileGroupSettings : ConfigurationElement
	{
		// Token: 0x06003FBF RID: 16319 RVA: 0x000A88BC File Offset: 0x000A6ABC
		static ProfileGroupSettings()
		{
			ProfileGroupSettings.properties.Add(ProfileGroupSettings.propertySettingsProp);
			ProfileGroupSettings.properties.Add(ProfileGroupSettings.nameProp);
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x0009F629 File Offset: 0x0009D829
		internal ProfileGroupSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> class using default settings.</summary>
		/// <param name="name">The name of the new group.</param>
		// Token: 0x06003FC1 RID: 16321 RVA: 0x000A892B File Offset: 0x000A6B2B
		public ProfileGroupSettings(string name)
		{
			this.Name = name;
		}

		/// <param name="obj">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to compare to the current object.</param>
		// Token: 0x06003FC2 RID: 16322 RVA: 0x000A893C File Offset: 0x000A6B3C
		public override bool Equals(object obj)
		{
			ProfileGroupSettings profileGroupSettings = obj as ProfileGroupSettings;
			return profileGroupSettings != null && !(base.GetType() != profileGroupSettings.GetType()) && this.Name.Equals(profileGroupSettings.Name);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x000A897B File Offset: 0x000A6B7B
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x000A8988 File Offset: 0x000A6B88
		internal void DoDeserialize(XmlReader reader)
		{
			this.DeserializeElement(reader, false);
		}

		/// <summary>Gets or sets name of the group of <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects this object contains.</summary>
		/// <returns>A string containing the name of the group of <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects this object contains. The default value is an empty string ("").</returns>
		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x000A8992 File Offset: 0x000A6B92
		// (set) Token: 0x06003FC6 RID: 16326 RVA: 0x000A89A4 File Offset: 0x000A6BA4
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ProfileGroupSettings.nameProp];
			}
			internal set
			{
				base[ProfileGroupSettings.nameProp] = value;
			}
		}

		/// <summary>Gets the collection of profile property settings this object contains.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProfilePropertySettingsCollection" /> collection that contains all the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects contained in this group.</returns>
		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x000A89B2 File Offset: 0x000A6BB2
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ProfilePropertySettingsCollection PropertySettings
		{
			get
			{
				return (ProfilePropertySettingsCollection)base[ProfileGroupSettings.propertySettingsProp];
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x000A89C4 File Offset: 0x000A6BC4
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileGroupSettings.properties;
			}
		}

		// Token: 0x040022B2 RID: 8882
		private static ConfigurationProperty propertySettingsProp = new ConfigurationProperty(null, typeof(ProfilePropertySettingsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x040022B3 RID: 8883
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, null, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040022B4 RID: 8884
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

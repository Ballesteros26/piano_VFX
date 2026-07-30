using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> class provides a way to programmatically access and modify the profiles section of a configuration file. This class cannot be inherited.</summary>
	// Token: 0x020005CA RID: 1482
	public sealed class ProfilePropertySettings : ConfigurationElement
	{
		// Token: 0x06003FE1 RID: 16353 RVA: 0x000A8B0C File Offset: 0x000A6D0C
		static ProfilePropertySettings()
		{
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.allowAnonymousProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.customProviderDataProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.defaultValueProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.nameProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.providerProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.readOnlyProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.serializeAsProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.typeProp);
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x0009F629 File Offset: 0x0009D829
		internal ProfilePropertySettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> class with the specified name.</summary>
		/// <param name="name">A unique name for the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</param>
		// Token: 0x06003FE3 RID: 16355 RVA: 0x000A8CB4 File Offset: 0x000A6EB4
		public ProfilePropertySettings(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> class with the specified name and settings.</summary>
		/// <param name="name">A unique name for the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</param>
		/// <param name="readOnly">true to indicate that the associated property in the dynamically generated ProfileCommon class should be read-only; otherwise, false.</param>
		/// <param name="serializeAs">One of the <see cref="T:System.Web.Configuration.SerializationMode" /> values.</param>
		/// <param name="providerName">The name of a provider from the <see cref="P:System.Web.Configuration.ProfileSection.Providers" /> property, or an empty string ("").</param>
		/// <param name="defaultValue">A string containing the default value used for the named property in the generated page Profile class.</param>
		/// <param name="profileType">A valid type reference or an empty string.</param>
		/// <param name="allowAnonymous">true to indicate associated property in the dynamically generated ProfileCommon class should support anonymous users; otherwise, false, to indicate that anonymous users cannot change the named property.</param>
		/// <param name="customProviderData">A string containing provider-specific information used by the provider associated with the property.</param>
		// Token: 0x06003FE4 RID: 16356 RVA: 0x000A8CC4 File Offset: 0x000A6EC4
		public ProfilePropertySettings(string name, bool readOnly, SerializationMode serializeAs, string providerName, string defaultValue, string profileType, bool allowAnonymous, string customProviderData)
		{
			this.Name = name;
			this.ReadOnly = readOnly;
			this.SerializeAs = serializeAs;
			this.Provider = providerName;
			this.DefaultValue = defaultValue;
			this.Type = profileType;
			this.AllowAnonymous = allowAnonymous;
			this.CustomProviderData = customProviderData;
		}

		/// <summary>Gets or sets a value indicating whether the associated property in the dynamically generated ProfileCommon class can be set by anonymous users.</summary>
		/// <returns>true if the associated property in the ProfileCommon class can be set by anonymous users; otherwise, false, indicating that anonymous users cannot change the property value. The default is false.</returns>
		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x000A8D14 File Offset: 0x000A6F14
		// (set) Token: 0x06003FE6 RID: 16358 RVA: 0x000A8D26 File Offset: 0x000A6F26
		[ConfigurationProperty("allowAnonymous", DefaultValue = false)]
		public bool AllowAnonymous
		{
			get
			{
				return (bool)base[ProfilePropertySettings.allowAnonymousProp];
			}
			set
			{
				base[ProfilePropertySettings.allowAnonymousProp] = value;
			}
		}

		/// <summary>Gets or sets a string of custom data for the profile property provider.</summary>
		/// <returns>A string of custom data for the profile property provider. The default is null.</returns>
		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x000A8D39 File Offset: 0x000A6F39
		// (set) Token: 0x06003FE8 RID: 16360 RVA: 0x000A8D4B File Offset: 0x000A6F4B
		[ConfigurationProperty("customProviderData", DefaultValue = "")]
		public string CustomProviderData
		{
			get
			{
				return (string)base[ProfilePropertySettings.customProviderDataProp];
			}
			set
			{
				base[ProfilePropertySettings.customProviderDataProp] = value;
			}
		}

		/// <summary>Gets or sets the default value used for the associated property in the dynamically generated ProfileCommon class. </summary>
		/// <returns>A string containing the default value used for the associated property in the dynamically generated ProfileCommon class. The default is an empty string ("").</returns>
		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06003FE9 RID: 16361 RVA: 0x000A8D59 File Offset: 0x000A6F59
		// (set) Token: 0x06003FEA RID: 16362 RVA: 0x000A8D6B File Offset: 0x000A6F6B
		[ConfigurationProperty("defaultValue", DefaultValue = "")]
		public string DefaultValue
		{
			get
			{
				return (string)base[ProfilePropertySettings.defaultValueProp];
			}
			set
			{
				base[ProfilePropertySettings.defaultValueProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object and the associated property in the dynamically generated ProfileCommon class.</summary>
		/// <returns>A string containing the name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object. The default is null.</returns>
		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06003FEB RID: 16363 RVA: 0x000A8D79 File Offset: 0x000A6F79
		// (set) Token: 0x06003FEC RID: 16364 RVA: 0x000A8D8B File Offset: 0x000A6F8B
		[ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[ProfilePropertySettings.nameProp];
			}
			set
			{
				base[ProfilePropertySettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the name of a provider to use when serializing the named property.</summary>
		/// <returns>The name of a provider from the <see cref="P:System.Web.Configuration.ProfileSection.Providers" /> property, or an empty string (""). The default is an empty string.</returns>
		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06003FED RID: 16365 RVA: 0x000A8D99 File Offset: 0x000A6F99
		// (set) Token: 0x06003FEE RID: 16366 RVA: 0x000A8DAB File Offset: 0x000A6FAB
		[ConfigurationProperty("provider", DefaultValue = "")]
		public string Provider
		{
			get
			{
				return (string)base[ProfilePropertySettings.providerProp];
			}
			set
			{
				base[ProfilePropertySettings.providerProp] = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether the associated property in the dynamically generated ProfileCommon class is read-only.</summary>
		/// <returns>true if the associated property in the ProfileCommon class is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x000A8DB9 File Offset: 0x000A6FB9
		// (set) Token: 0x06003FF0 RID: 16368 RVA: 0x000A8DCB File Offset: 0x000A6FCB
		[ConfigurationProperty("readOnly", DefaultValue = false)]
		public bool ReadOnly
		{
			get
			{
				return (bool)base[ProfilePropertySettings.readOnlyProp];
			}
			set
			{
				base[ProfilePropertySettings.readOnlyProp] = value;
			}
		}

		/// <summary>Gets or sets the serialization method used for the associated property in the dynamically generated ProfileCommon class.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.SerializationMode" /> values. The default is <see cref="F:System.Web.Configuration.SerializationMode.ProviderSpecific" />.</returns>
		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x000A8DDE File Offset: 0x000A6FDE
		// (set) Token: 0x06003FF2 RID: 16370 RVA: 0x000A8DF0 File Offset: 0x000A6FF0
		[ConfigurationProperty("serializeAs", DefaultValue = "ProviderSpecific")]
		public SerializationMode SerializeAs
		{
			get
			{
				return (SerializationMode)base[ProfilePropertySettings.serializeAsProp];
			}
			set
			{
				base[ProfilePropertySettings.serializeAsProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the type of the associated property in the dynamically generated ProfileCommon class.</summary>
		/// <returns>A valid, fully qualified type reference, or an empty string (""). The default is an empty string.</returns>
		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x000A8E03 File Offset: 0x000A7003
		// (set) Token: 0x06003FF4 RID: 16372 RVA: 0x000A8E15 File Offset: 0x000A7015
		[ConfigurationProperty("type", DefaultValue = "string")]
		public string Type
		{
			get
			{
				return (string)base[ProfilePropertySettings.typeProp];
			}
			set
			{
				base[ProfilePropertySettings.typeProp] = value;
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x000A8E23 File Offset: 0x000A7023
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfilePropertySettings.properties;
			}
		}

		// Token: 0x040022B6 RID: 8886
		private static ConfigurationProperty allowAnonymousProp = new ConfigurationProperty("allowAnonymous", typeof(bool), false);

		// Token: 0x040022B7 RID: 8887
		private static ConfigurationProperty customProviderDataProp = new ConfigurationProperty("customProviderData", typeof(string), "");

		// Token: 0x040022B8 RID: 8888
		private static ConfigurationProperty defaultValueProp = new ConfigurationProperty("defaultValue", typeof(string), "");

		// Token: 0x040022B9 RID: 8889
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), new ProfilePropertyNameValidator(), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040022BA RID: 8890
		private static ConfigurationProperty providerProp = new ConfigurationProperty("provider", typeof(string), "");

		// Token: 0x040022BB RID: 8891
		private static ConfigurationProperty readOnlyProp = new ConfigurationProperty("readOnly", typeof(bool), false);

		// Token: 0x040022BC RID: 8892
		private static ConfigurationProperty serializeAsProp = new ConfigurationProperty("serializeAs", typeof(SerializationMode), SerializationMode.ProviderSpecific, new GenericEnumConverter(typeof(SerializationMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022BD RID: 8893
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "string");

		// Token: 0x040022BE RID: 8894
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

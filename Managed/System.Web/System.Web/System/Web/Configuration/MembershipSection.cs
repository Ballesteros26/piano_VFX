using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings to support the infrastructure for configuring and managing membership details. This class cannot be inherited.</summary>
	// Token: 0x020005BB RID: 1467
	public sealed class MembershipSection : ConfigurationSection
	{
		// Token: 0x06003EE1 RID: 16097 RVA: 0x000A6A18 File Offset: 0x000A4C18
		static MembershipSection()
		{
			MembershipSection.properties.Add(MembershipSection.defaultProviderProp);
			MembershipSection.properties.Add(MembershipSection.hashAlgorithmTypeProp);
			MembershipSection.properties.Add(MembershipSection.providersProp);
			MembershipSection.properties.Add(MembershipSection.userIsOnlineTimeWindowProp);
		}

		/// <summary>Gets or sets the name of the default provider that is used to manage roles. </summary>
		/// <returns>The name of a provider in <see cref="P:System.Web.Configuration.MembershipSection.Providers" />. The default is AspNetSqlRoleProvider.</returns>
		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x000A6B21 File Offset: 0x000A4D21
		// (set) Token: 0x06003EE3 RID: 16099 RVA: 0x000A6B33 File Offset: 0x000A4D33
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlMembershipProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base[MembershipSection.defaultProviderProp];
			}
			set
			{
				base[MembershipSection.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets or sets the type of encryption that is used for sensitive membership information.</summary>
		/// <returns>The type of encryption used to encrypt sensitive membership information.</returns>
		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x000A6B41 File Offset: 0x000A4D41
		// (set) Token: 0x06003EE5 RID: 16101 RVA: 0x000A6B53 File Offset: 0x000A4D53
		[ConfigurationProperty("hashAlgorithmType", DefaultValue = "")]
		public string HashAlgorithmType
		{
			get
			{
				return (string)base[MembershipSection.hashAlgorithmTypeProp];
			}
			set
			{
				base[MembershipSection.hashAlgorithmTypeProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> object of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the provider's settings, defined within the providers subsection of the membership section of the configuration file.</returns>
		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x000A6B61 File Offset: 0x000A4D61
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[MembershipSection.providersProp];
			}
		}

		/// <summary>Gets or sets the length of time, in minutes, before a user is no longer considered to be online.</summary>
		/// <returns>A length of time in minutes.</returns>
		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x000A6B73 File Offset: 0x000A4D73
		// (set) Token: 0x06003EE8 RID: 16104 RVA: 0x000A6B85 File Offset: 0x000A4D85
		[TypeConverter(typeof(TimeSpanMinutesConverter))]
		[TimeSpanValidator(MinValueString = "00:01:00")]
		[ConfigurationProperty("userIsOnlineTimeWindow", DefaultValue = "00:15:00")]
		public TimeSpan UserIsOnlineTimeWindow
		{
			get
			{
				return (TimeSpan)base[MembershipSection.userIsOnlineTimeWindowProp];
			}
			set
			{
				base[MembershipSection.userIsOnlineTimeWindowProp] = value;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x000A6B98 File Offset: 0x000A4D98
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return MembershipSection.properties;
			}
		}

		// Token: 0x04002255 RID: 8789
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlMembershipProvider", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002256 RID: 8790
		private static ConfigurationProperty hashAlgorithmTypeProp = new ConfigurationProperty("hashAlgorithmType", typeof(string), "");

		// Token: 0x04002257 RID: 8791
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002258 RID: 8792
		private static ConfigurationProperty userIsOnlineTimeWindowProp = new ConfigurationProperty("userIsOnlineTimeWindow", typeof(TimeSpan), TimeSpan.FromMinutes(15.0), PropertyHelper.TimeSpanMinutesConverter, new TimeSpanValidator(new TimeSpan(0, 1, 0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04002259 RID: 8793
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

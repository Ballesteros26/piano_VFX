using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005BC RID: 1468
	internal sealed class MonoSettingsSection : ConfigurationSection
	{
		// Token: 0x06003EEB RID: 16107 RVA: 0x000A6BA0 File Offset: 0x000A4DA0
		static MonoSettingsSection()
		{
			MonoSettingsSection.properties.Add(MonoSettingsSection.compilersCompatibilityProp);
			MonoSettingsSection.properties.Add(MonoSettingsSection.useCompilersCompatibilityProp);
			MonoSettingsSection.properties.Add(MonoSettingsSection.verificationCompatibilityProp);
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x000A6C43 File Offset: 0x000A4E43
		[ConfigurationProperty("compilersCompatibility")]
		public CompilerCollection CompilersCompatibility
		{
			get
			{
				return (CompilerCollection)base[MonoSettingsSection.compilersCompatibilityProp];
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x000A6C55 File Offset: 0x000A4E55
		// (set) Token: 0x06003EEE RID: 16110 RVA: 0x000A6C67 File Offset: 0x000A4E67
		[ConfigurationProperty("useCompilersCompatibility", DefaultValue = "True")]
		public bool UseCompilersCompatibility
		{
			get
			{
				return (bool)base[MonoSettingsSection.useCompilersCompatibilityProp];
			}
			set
			{
				base[MonoSettingsSection.useCompilersCompatibilityProp] = value;
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000A6C7A File Offset: 0x000A4E7A
		// (set) Token: 0x06003EF0 RID: 16112 RVA: 0x000A6C8C File Offset: 0x000A4E8C
		[ConfigurationProperty("verificationCompatibility", DefaultValue = "0")]
		public int VerificationCompatibility
		{
			get
			{
				return (int)base[MonoSettingsSection.verificationCompatibilityProp];
			}
			set
			{
				base[MonoSettingsSection.verificationCompatibilityProp] = value;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06003EF1 RID: 16113 RVA: 0x000A6C9F File Offset: 0x000A4E9F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return MonoSettingsSection.properties;
			}
		}

		// Token: 0x0400225A RID: 8794
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400225B RID: 8795
		private static ConfigurationProperty compilersCompatibilityProp = new ConfigurationProperty("compilersCompatibility", typeof(CompilerCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400225C RID: 8796
		private static ConfigurationProperty useCompilersCompatibilityProp = new ConfigurationProperty("useCompilersCompatibility", typeof(bool), true);

		// Token: 0x0400225D RID: 8797
		private static ConfigurationProperty verificationCompatibilityProp = new ConfigurationProperty("verificationCompatibility", typeof(int), 0);
	}
}

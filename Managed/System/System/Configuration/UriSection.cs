using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents the Uri section within a configuration file.</summary>
	// Token: 0x020001A5 RID: 421
	public sealed class UriSection : ConfigurationSection
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x0003D23C File Offset: 0x0003B43C
		static UriSection()
		{
			UriSection.properties.Add(UriSection.idn_prop);
			UriSection.properties.Add(UriSection.iriParsing_prop);
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IdnElement" /> object that contains the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0003D2A5 File Offset: 0x0003B4A5
		[ConfigurationProperty("idn")]
		public IdnElement Idn
		{
			get
			{
				return (IdnElement)base[UriSection.idn_prop];
			}
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IriParsingElement" /> object that contains the configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0003D2B7 File Offset: 0x0003B4B7
		[ConfigurationProperty("iriParsing")]
		public IriParsingElement IriParsing
		{
			get
			{
				return (IriParsingElement)base[UriSection.iriParsing_prop];
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0003D2C9 File Offset: 0x0003B4C9
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UriSection.properties;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SchemeSettingElementCollection" /> object that contains the configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class</returns>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		public SchemeSettingElementCollection SchemeSettings
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04001006 RID: 4102
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001007 RID: 4103
		private static ConfigurationProperty idn_prop = new ConfigurationProperty("idn", typeof(IdnElement), null);

		// Token: 0x04001008 RID: 4104
		private static ConfigurationProperty iriParsing_prop = new ConfigurationProperty("iriParsing", typeof(IriParsingElement), null);
	}
}

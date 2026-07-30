using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Resource Identifier (IRI) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x02000180 RID: 384
	public sealed class IriParsingElement : ConfigurationElement
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0003BD1A File Offset: 0x00039F1A
		static IriParsingElement()
		{
			IriParsingElement.properties.Add(IriParsingElement.enabled_prop);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IriParsingElement" /> configuration setting.</summary>
		/// <returns>A Boolean that indicates if International Resource Identifier (IRI) processing is enabled. </returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0003BD55 File Offset: 0x00039F55
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0003BD67 File Offset: 0x00039F67
		[ConfigurationProperty("enabled", DefaultValue = false, Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public bool Enabled
		{
			get
			{
				return (bool)base[IriParsingElement.enabled_prop];
			}
			set
			{
				base[IriParsingElement.enabled_prop] = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0003BD7A File Offset: 0x00039F7A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IriParsingElement.properties;
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003BD84 File Offset: 0x00039F84
		public override bool Equals(object o)
		{
			IriParsingElement iriParsingElement = o as IriParsingElement;
			return iriParsingElement != null && iriParsingElement.Enabled == this.Enabled;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003BDAB File Offset: 0x00039FAB
		public override int GetHashCode()
		{
			return Convert.ToInt32(this.Enabled) ^ 127;
		}

		// Token: 0x04000FCC RID: 4044
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000FCD RID: 4045
		private static ConfigurationProperty enabled_prop = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}

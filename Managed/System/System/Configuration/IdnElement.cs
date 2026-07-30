using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x0200017E RID: 382
	public sealed class IdnElement : ConfigurationElement
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x0003BC79 File Offset: 0x00039E79
		static IdnElement()
		{
			IdnElement.properties.Add(IdnElement.enabled_prop);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IdnElement" /> configuration setting. </summary>
		/// <returns>A <see cref="T:System.UriIdnScope" /> that contains the current configuration setting for IDN processing.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0003BCBC File Offset: 0x00039EBC
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0003BCCE File Offset: 0x00039ECE
		[ConfigurationProperty("enabled", DefaultValue = UriIdnScope.None, Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public UriIdnScope Enabled
		{
			get
			{
				return (UriIdnScope)base[IdnElement.enabled_prop];
			}
			set
			{
				base[IdnElement.enabled_prop] = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0003BCE1 File Offset: 0x00039EE1
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IdnElement.properties;
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003BCE8 File Offset: 0x00039EE8
		public override bool Equals(object o)
		{
			IdnElement idnElement = o as IdnElement;
			return idnElement != null && idnElement.Enabled == this.Enabled;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0003BD0F File Offset: 0x00039F0F
		public override int GetHashCode()
		{
			return (int)(this.Enabled ^ (UriIdnScope)127);
		}

		// Token: 0x04000FC9 RID: 4041
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000FCA RID: 4042
		private static ConfigurationProperty enabled_prop = new ConfigurationProperty("enabled", typeof(UriIdnScope), UriIdnScope.None, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000FCB RID: 4043
		internal const UriIdnScope EnabledDefaultValue = UriIdnScope.None;
	}
}

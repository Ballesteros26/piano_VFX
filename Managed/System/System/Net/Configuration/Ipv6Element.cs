using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Determines whether Internet Protocol version 6 is enabled on the local computer. This class cannot be inherited.</summary>
	// Token: 0x020006A1 RID: 1697
	public sealed class Ipv6Element : ConfigurationElement
	{
		// Token: 0x0600353D RID: 13629 RVA: 0x000C4AA0 File Offset: 0x000C2CA0
		static Ipv6Element()
		{
			Ipv6Element.properties.Add(Ipv6Element.enabledProp);
		}

		/// <summary>Gets or sets a Boolean value that indicates whether Internet Protocol version 6 is enabled on the local computer.</summary>
		/// <returns>true if IPv6 is enabled; otherwise, false.</returns>
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000C4ADA File Offset: 0x000C2CDA
		// (set) Token: 0x06003540 RID: 13632 RVA: 0x000C4AEC File Offset: 0x000C2CEC
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[Ipv6Element.enabledProp];
			}
			set
			{
				base[Ipv6Element.enabledProp] = value;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000C4AFF File Offset: 0x000C2CFF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Ipv6Element.properties;
			}
		}

		// Token: 0x04002A71 RID: 10865
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A72 RID: 10866
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);
	}
}

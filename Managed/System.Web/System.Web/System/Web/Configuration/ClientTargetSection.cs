using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the clientTarget section. This class cannot be inherited.</summary>
	// Token: 0x02000590 RID: 1424
	public sealed class ClientTargetSection : ConfigurationSection
	{
		// Token: 0x06003C30 RID: 15408 RVA: 0x000A0A61 File Offset: 0x0009EC61
		static ClientTargetSection()
		{
			ClientTargetSection.properties.Add(ClientTargetSection.clientTargetsProp);
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Configuration.ClientTarget" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ClientTargetCollection" /> that contains the <see cref="T:System.Web.Configuration.ClientTarget" /> objects.</returns>
		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x000A0A93 File Offset: 0x0009EC93
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection | ConfigurationPropertyOptions.IsRequired)]
		public ClientTargetCollection ClientTargets
		{
			get
			{
				return (ClientTargetCollection)base[ClientTargetSection.clientTargetsProp];
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x000A0AA5 File Offset: 0x0009ECA5
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientTargetSection.properties;
			}
		}

		// Token: 0x040020B0 RID: 8368
		private static ConfigurationProperty clientTargetsProp = new ConfigurationProperty(null, typeof(ClientTargetCollection), null, ConfigurationPropertyOptions.IsDefaultCollection | ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020B1 RID: 8369
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

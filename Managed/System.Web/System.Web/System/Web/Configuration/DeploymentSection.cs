using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines a configuration setting that is typically used on a production server to override application-level settings that are appropriate only on development computers. </summary>
	// Token: 0x02000599 RID: 1433
	public sealed class DeploymentSection : ConfigurationSection
	{
		// Token: 0x06003CBD RID: 15549 RVA: 0x000A1897 File Offset: 0x0009FA97
		static DeploymentSection()
		{
			DeploymentSection.properties.Add(DeploymentSection.retailProp);
		}

		/// <summary>Gets or sets a value that specifies whether Web applications on the computer are deployed in retail mode.</summary>
		/// <returns>true if Web applications are deployed in retail mode; otherwise, false. The default is false.</returns>
		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x000A18D1 File Offset: 0x0009FAD1
		// (set) Token: 0x06003CBF RID: 15551 RVA: 0x000A18E3 File Offset: 0x0009FAE3
		[ConfigurationProperty("retail", DefaultValue = "False")]
		public bool Retail
		{
			get
			{
				return (bool)base[DeploymentSection.retailProp];
			}
			set
			{
				base[DeploymentSection.retailProp] = value;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x000A18F6 File Offset: 0x0009FAF6
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DeploymentSection.properties;
			}
		}

		// Token: 0x040020D9 RID: 8409
		private static ConfigurationProperty retailProp = new ConfigurationProperty("retail", typeof(bool), false);

		// Token: 0x040020DA RID: 8410
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

using System;
using System.Configuration;

namespace System.Web.Services.Configuration
{
	/// <summary>Represents the WsiProfiles element in the configuration file.</summary>
	// Token: 0x0200014C RID: 332
	public sealed class WsiProfilesElement : ConfigurationElement
	{
		/// <summary>Initializes and instance of the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> class.</summary>
		// Token: 0x06000A45 RID: 2629 RVA: 0x000453E4 File Offset: 0x000435E4
		public WsiProfilesElement()
		{
			this.properties.Add(this.name);
		}

		/// <summary>Initializes and instance of the <see cref="T:System.Web.Services.Configuration.WsiProfilesElement" /> class, using the specified <see cref="T:System.Web.Services.WsiProfiles" /> enumeration value.</summary>
		/// <param name="name">A <see cref="T:System.Web.Services.WsiProfiles" /> object that specifies whether the Web service conforms to the WSI Basic Profile version 1.1.</param>
		// Token: 0x06000A46 RID: 2630 RVA: 0x00045434 File Offset: 0x00043634
		public WsiProfilesElement(WsiProfiles name)
			: this()
		{
			this.Name = name;
		}

		/// <summary>Gets or sets whether the Web service conforms to the WSI Basic Profile version 1.1.</summary>
		/// <returns>A <see cref="T:System.Web.Services.WsiProfiles" /> object that specifies whether the Web service conforms to the WSI Basic Profile version 1.1.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00045443 File Offset: 0x00043643
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x00045456 File Offset: 0x00043656
		[ConfigurationProperty("name", IsKey = true, DefaultValue = WsiProfiles.None)]
		public WsiProfiles Name
		{
			get
			{
				return (WsiProfiles)base[this.name];
			}
			set
			{
				if (!this.IsValidWsiProfilesValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base[this.name] = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0004547E File Offset: 0x0004367E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00045486 File Offset: 0x00043686
		private bool IsValidWsiProfilesValue(WsiProfiles value)
		{
			return Enum.IsDefined(typeof(WsiProfiles), value);
		}

		// Token: 0x040005D2 RID: 1490
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040005D3 RID: 1491
		private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(WsiProfiles), WsiProfiles.None, ConfigurationPropertyOptions.IsKey);
	}
}

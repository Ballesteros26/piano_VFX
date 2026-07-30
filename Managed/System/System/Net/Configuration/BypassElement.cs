using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the address information for resources that are not retrieved using a proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000694 RID: 1684
	public sealed class BypassElement : ConfigurationElement
	{
		// Token: 0x060034D4 RID: 13524 RVA: 0x000C3C1D File Offset: 0x000C1E1D
		static BypassElement()
		{
			BypassElement.properties.Add(BypassElement.addressProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class. </summary>
		// Token: 0x060034D5 RID: 13525 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		public BypassElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class with the specified type information.</summary>
		/// <param name="address">A string that identifies the address of a resource.</param>
		// Token: 0x060034D6 RID: 13526 RVA: 0x000C3C53 File Offset: 0x000C1E53
		public BypassElement(string address)
		{
			this.Address = address;
		}

		/// <summary>Gets or sets the addresses of resources that bypass the proxy server.</summary>
		/// <returns>A string that identifies a resource.</returns>
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000C3C62 File Offset: 0x000C1E62
		// (set) Token: 0x060034D8 RID: 13528 RVA: 0x000C3C74 File Offset: 0x000C1E74
		[ConfigurationProperty("address", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Address
		{
			get
			{
				return (string)base[BypassElement.addressProp];
			}
			set
			{
				base[BypassElement.addressProp] = value;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000C3C82 File Offset: 0x000C1E82
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BypassElement.properties;
			}
		}

		// Token: 0x04002A56 RID: 10838
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A57 RID: 10839
		private static ConfigurationProperty addressProp = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}

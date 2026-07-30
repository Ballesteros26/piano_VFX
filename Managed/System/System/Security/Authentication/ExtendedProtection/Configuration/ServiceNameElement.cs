using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> class represents a configuration element for a service name used in a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
	// Token: 0x0200038B RID: 907
	public sealed class ServiceNameElement : ConfigurationElement
	{
		// Token: 0x06001B7A RID: 7034 RVA: 0x0006D6F3 File Offset: 0x0006B8F3
		static ServiceNameElement()
		{
			ServiceNameElement.properties.Add(ServiceNameElement.name);
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the representation of SPN for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0006D727 File Offset: 0x0006B927
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x0006D739 File Offset: 0x0006B939
		[ConfigurationProperty("name")]
		public string Name
		{
			get
			{
				return (string)base[ServiceNameElement.name];
			}
			set
			{
				base[ServiceNameElement.name] = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0006D747 File Offset: 0x0006B947
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServiceNameElement.properties;
			}
		}

		// Token: 0x040018D7 RID: 6359
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040018D8 RID: 6360
		private static ConfigurationProperty name = ConfigUtil.BuildProperty(typeof(ServiceNameElement), "Name");
	}
}

using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for connection management. This class cannot be inherited.</summary>
	// Token: 0x0200069B RID: 1691
	public sealed class ConnectionManagementSection : ConfigurationSection
	{
		// Token: 0x0600350A RID: 13578 RVA: 0x000C415E File Offset: 0x000C235E
		static ConnectionManagementSection()
		{
			ConnectionManagementSection.properties.Add(ConnectionManagementSection.connectionManagementProp);
		}

		/// <summary>Gets the collection of connection management objects in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementElementCollection" /> that contains the connection management information for the local computer. </returns>
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x000C4194 File Offset: 0x000C2394
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionManagementElementCollection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementElementCollection)base[ConnectionManagementSection.connectionManagementProp];
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000C41A6 File Offset: 0x000C23A6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementSection.properties;
			}
		}

		// Token: 0x04002A5D RID: 10845
		private static ConfigurationProperty connectionManagementProp = new ConfigurationProperty("ConnectionManagement", typeof(ConnectionManagementElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002A5E RID: 10846
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

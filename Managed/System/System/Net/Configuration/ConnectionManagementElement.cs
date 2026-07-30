using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum number of connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x02000696 RID: 1686
	public sealed class ConnectionManagementElement : ConfigurationElement
	{
		// Token: 0x060034E8 RID: 13544 RVA: 0x000C3CC0 File Offset: 0x000C1EC0
		static ConnectionManagementElement()
		{
			ConnectionManagementElement.properties.Add(ConnectionManagementElement.addressProp);
			ConnectionManagementElement.properties.Add(ConnectionManagementElement.maxConnectionProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> class. </summary>
		// Token: 0x060034E9 RID: 13545 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		public ConnectionManagementElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> class with the specified address and connection limit information.</summary>
		/// <param name="address">A string that identifies the address of a remote computer.</param>
		/// <param name="maxConnection">An integer that identifies the maximum number of connections allowed to <paramref name="address" /> from the local computer.</param>
		// Token: 0x060034EA RID: 13546 RVA: 0x000C3D30 File Offset: 0x000C1F30
		public ConnectionManagementElement(string address, int maxConnection)
		{
			this.Address = address;
			this.MaxConnection = maxConnection;
		}

		/// <summary>Gets or sets the address for remote computers.</summary>
		/// <returns>A string that contains a regular expression describing an IP address or DNS name.</returns>
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x000C3D46 File Offset: 0x000C1F46
		// (set) Token: 0x060034EC RID: 13548 RVA: 0x000C3D58 File Offset: 0x000C1F58
		[ConfigurationProperty("address", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Address
		{
			get
			{
				return (string)base[ConnectionManagementElement.addressProp];
			}
			set
			{
				base[ConnectionManagementElement.addressProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of connections that can be made to a remote computer.</summary>
		/// <returns>An integer that specifies the maximum number of connections.</returns>
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x000C3D66 File Offset: 0x000C1F66
		// (set) Token: 0x060034EE RID: 13550 RVA: 0x000C3D78 File Offset: 0x000C1F78
		[ConfigurationProperty("maxconnection", DefaultValue = "6", Options = ConfigurationPropertyOptions.IsRequired)]
		public int MaxConnection
		{
			get
			{
				return (int)base[ConnectionManagementElement.maxConnectionProp];
			}
			set
			{
				base[ConnectionManagementElement.maxConnectionProp] = value;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000C3D8B File Offset: 0x000C1F8B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementElement.properties;
			}
		}

		// Token: 0x04002A58 RID: 10840
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A59 RID: 10841
		private static ConfigurationProperty addressProp = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002A5A RID: 10842
		private static ConfigurationProperty maxConnectionProp = new ConfigurationProperty("maxconnection", typeof(int), 1, ConfigurationPropertyOptions.IsRequired);
	}
}

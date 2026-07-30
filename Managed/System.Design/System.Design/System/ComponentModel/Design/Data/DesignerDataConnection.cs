using System;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Represents a connection to a data store in a design tool. This class cannot be inherited. </summary>
	// Token: 0x02000168 RID: 360
	public sealed class DesignerDataConnection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" /> class with the specified name, data provider, and connection string. </summary>
		/// <param name="name">The name associated with this connection.</param>
		/// <param name="providerName">The name of the provider object used to access the underlying data store</param>
		/// <param name="connectionString">The string that specifies how to connect to the data store.</param>
		// Token: 0x06000AD4 RID: 2772 RVA: 0x000164E8 File Offset: 0x000146E8
		[MonoTODO]
		public DesignerDataConnection(string name, string providerName, string connectionString)
			: this(name, providerName, connectionString, false)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" /> class with the specified name, data provider, and connection string, and indicates whether the connection was loaded from a configuration file. </summary>
		/// <param name="name">The name associated with this connection.</param>
		/// <param name="providerName">The name of the provider object used to access the underlying data store.</param>
		/// <param name="connectionString">The string that specifies how to connect to the data store.</param>
		/// <param name="isConfigured">true to indicate the connection was created from information stored in the application's configuration file; otherwise, false.</param>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public DesignerDataConnection(string name, string providerName, string connectionString, bool isConfigured)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the name of the data connection.</summary>
		/// <returns>The name of the data connection.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x000164F9 File Offset: 0x000146F9
		[MonoTODO]
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the name of the provider used to access the underlying data store.</summary>
		/// <returns>The name of the provider used to access the underlying data store.</returns>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00016501 File Offset: 0x00014701
		[MonoTODO]
		public string ProviderName
		{
			get
			{
				return this.provider_name;
			}
		}

		/// <summary>Gets the application connection string defined for the connection.</summary>
		/// <returns>The application connection string defined for the connection.</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00016509 File Offset: 0x00014709
		[MonoTODO]
		public string ConnectionString
		{
			get
			{
				return this.connection_string;
			}
		}

		/// <summary>Gets a value indicating whether the connection information is in the application's configuration file.</summary>
		/// <returns>true if the connection is defined in the application's configuration file; otherwise, false.</returns>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00016511 File Offset: 0x00014711
		[MonoTODO]
		public bool IsConfigured
		{
			get
			{
				return this.is_configured;
			}
		}

		// Token: 0x04000282 RID: 642
		private string name;

		// Token: 0x04000283 RID: 643
		private string provider_name;

		// Token: 0x04000284 RID: 644
		private string connection_string;

		// Token: 0x04000285 RID: 645
		private bool is_configured;
	}
}

using System;
using System.Configuration;

namespace System.Transactions.Configuration
{
	/// <summary>Represents a configuration section that encapsulates and allows traversal of all the transaction configuration XML elements and attributes that are within this configuration section. This class cannot be inherited.</summary>
	// Token: 0x0200002F RID: 47
	public class TransactionsSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Provides static access to a <see cref="T:System.Transactions.Configuration.TransactionsSectionGroup" />.</summary>
		/// <returns>A <see cref="T:System.Transactions.Configuration.TransactionsSectionGroup" /> object.</returns>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> representing the configuration settings that apply to a particular computer, application, or resource.</param>
		// Token: 0x060000DE RID: 222 RVA: 0x0000326B File Offset: 0x0000146B
		public static TransactionsSectionGroup GetSectionGroup(Configuration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return config.GetSectionGroup("system.transactions") as TransactionsSectionGroup;
		}

		/// <summary>Gets the default settings used to initialize the elements and attributes in a transactions section.</summary>
		/// <returns>A <see cref="T:System.Transactions.Configuration.DefaultSettingsSection" /> that represents the default settings. The default is a <see cref="T:System.Transactions.Configuration.DefaultSettingsSection" /> that is populated with default values.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000328B File Offset: 0x0000148B
		[ConfigurationProperty("defaultSettings")]
		public DefaultSettingsSection DefaultSettings
		{
			get
			{
				return (DefaultSettingsSection)base.Sections["defaultSettings"];
			}
		}

		/// <summary>Gets the configuration settings set at the machine level.</summary>
		/// <returns>A <see cref="T:System.Transactions.Configuration.MachineSettingsSection" /> that represents the configuration settings at the machine level. The default is a <see cref="T:System.Transactions.Configuration.MachineSettingsSection" /> that is populated with default values.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000032A2 File Offset: 0x000014A2
		[ConfigurationProperty("machineSettings")]
		public MachineSettingsSection MachineSettings
		{
			get
			{
				return (MachineSettingsSection)base.Sections["machineSettings"];
			}
		}
	}
}

using System;
using System.Configuration;

namespace System.Transactions.Configuration
{
	/// <summary>Represents an XML section in a configuration file that contains default values of a transaction. This class cannot be inherited.</summary>
	// Token: 0x0200002D RID: 45
	public class DefaultSettingsSection : ConfigurationSection
	{
		/// <summary>Gets or sets a default time after which a transaction times out.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object. The default property is 00:01:00. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt to set this property to negative values.</exception>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000031F9 File Offset: 0x000013F9
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000320B File Offset: 0x0000140B
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("timeout", DefaultValue = "00:01:00")]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base["timeout"];
			}
			set
			{
				base["timeout"] = value;
			}
		}

		/// <summary>Gets the name of the transaction manager.</summary>
		/// <returns>The name of the transaction manager. The default value is an empty string.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt to set this property to fully qualified domain names or IP addresses.</exception>
		/// <exception cref="T:System.Transactions.TransactionAbortedException">An attempt to set this property to localhost.</exception>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000321E File Offset: 0x0000141E
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003230 File Offset: 0x00001430
		[ConfigurationProperty("distributedTransactionManagerName", DefaultValue = "")]
		public string DistributedTransactionManagerName
		{
			get
			{
				return base["distributedTransactionManagerName"] as string;
			}
			set
			{
				base["distributedTransactionManagerName"] = value;
			}
		}
	}
}

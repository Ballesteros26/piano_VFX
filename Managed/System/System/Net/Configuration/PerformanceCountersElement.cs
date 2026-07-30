using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the performance counter element in the System.Net configuration file that determines whether networking performance counters are enabled. This class cannot be inherited.</summary>
	// Token: 0x020006A7 RID: 1703
	public sealed class PerformanceCountersElement : ConfigurationElement
	{
		// Token: 0x06003557 RID: 13655 RVA: 0x000C4F8D File Offset: 0x000C318D
		static PerformanceCountersElement()
		{
			PerformanceCountersElement.properties.Add(PerformanceCountersElement.enabledProp);
		}

		/// <summary>Gets or sets whether performance counters are enabled.</summary>
		/// <returns>true if performance counters are enabled; otherwise, false.</returns>
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x000C4FC7 File Offset: 0x000C31C7
		// (set) Token: 0x06003559 RID: 13657 RVA: 0x000C4FD9 File Offset: 0x000C31D9
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[PerformanceCountersElement.enabledProp];
			}
			set
			{
				base[PerformanceCountersElement.enabledProp] = value;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x000C4FEC File Offset: 0x000C31EC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerformanceCountersElement.properties;
			}
		}

		// Token: 0x04002A75 RID: 10869
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x04002A76 RID: 10870
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

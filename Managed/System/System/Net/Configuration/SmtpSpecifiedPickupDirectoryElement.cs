using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents an SMTP pickup directory configuration element.</summary>
	// Token: 0x020006B1 RID: 1713
	public sealed class SmtpSpecifiedPickupDirectoryElement : ConfigurationElement
	{
		// Token: 0x060035B2 RID: 13746 RVA: 0x000C58B7 File Offset: 0x000C3AB7
		static SmtpSpecifiedPickupDirectoryElement()
		{
			SmtpSpecifiedPickupDirectoryElement.properties.Add(SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp);
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the SMTP server.</summary>
		/// <returns>A string that specifies the pickup directory for e-mail messages.</returns>
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060035B3 RID: 13747 RVA: 0x000C58EB File Offset: 0x000C3AEB
		// (set) Token: 0x060035B4 RID: 13748 RVA: 0x000C58FD File Offset: 0x000C3AFD
		[ConfigurationProperty("pickupDirectoryLocation")]
		public string PickupDirectoryLocation
		{
			get
			{
				return (string)base[SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp];
			}
			set
			{
				base[SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp] = value;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000C590B File Offset: 0x000C3B0B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SmtpSpecifiedPickupDirectoryElement.properties;
			}
		}

		// Token: 0x04002A9E RID: 10910
		private static ConfigurationProperty pickupDirectoryLocationProp = new ConfigurationProperty("pickupDirectoryLocation", typeof(string));

		// Token: 0x04002A9F RID: 10911
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

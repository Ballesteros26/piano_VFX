using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the sessionPageState section. This class cannot be inherited. </summary>
	// Token: 0x020005D7 RID: 1495
	public sealed class SessionPageStateSection : ConfigurationSection
	{
		// Token: 0x06004090 RID: 16528 RVA: 0x000A9FA4 File Offset: 0x000A81A4
		static SessionPageStateSection()
		{
			SessionPageStateSection.properties.Add(SessionPageStateSection.historySizeProp);
		}

		/// <summary>Gets or sets the size of the page history.</summary>
		/// <returns>The size of the page history.</returns>
		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x000AA005 File Offset: 0x000A8205
		// (set) Token: 0x06004092 RID: 16530 RVA: 0x000AA017 File Offset: 0x000A8217
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("historySize", DefaultValue = "9")]
		public int HistorySize
		{
			get
			{
				return (int)base[SessionPageStateSection.historySizeProp];
			}
			set
			{
				base[SessionPageStateSection.historySizeProp] = value;
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06004093 RID: 16531 RVA: 0x000AA02A File Offset: 0x000A822A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SessionPageStateSection.properties;
			}
		}

		// Token: 0x040022F8 RID: 8952
		private static ConfigurationProperty historySizeProp = new ConfigurationProperty("historySize", typeof(int), 9, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040022F9 RID: 8953
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		/// <summary>Defines the size of the page history.</summary>
		// Token: 0x040022FA RID: 8954
		public const int DefaultHistorySize = 9;
	}
}
